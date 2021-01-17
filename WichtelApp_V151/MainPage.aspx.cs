using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace WichtelApp_V151
{
    public partial class MainPage : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string[] infos;

        protected void Page_Load(object sender, EventArgs e)
        {
            var session = Session["Logged_User"];
            if (session != null)
            {
                infos = session.ToString().Split(',');

                lbl_title.Text = $"{infos[1]} - Wishlist";

                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand select_command = new SqlCommand(@"SELECT * FROM WISHES
                                                             WHERE WISHER_ID = @ID", con);

                select_command.Parameters.AddWithValue("@ID", infos[0]);

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(select_command);

                DataTable dataTable_Wishes = new DataTable();
                adapter.Fill(dataTable_Wishes);

                if (dataTable_Wishes.Rows.Count > 0)
                {
                    if (!IsPostBack)
                    {
                        gridview_update();
                    }
                }
                else
                {
                    modal_addWish.Style.Add("display", "block");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btn_add_wish_Click(object sender, EventArgs e)
        {
            modal_addWish.Style.Add("display", "block");
        }

        protected void btn_show_other_users_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllUsers.aspx");
        }

        protected void btn_close_modal_Click(object sender, EventArgs e)
        {
            txt_wish.Text = "";

            modal_addWish.Style.Add("display", "none");
        }

        protected void btn_change_mail_pw_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangeMailPassword.aspx");
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session["Logged_User"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void btn_add_wish_modal_Click(object sender, EventArgs e)
        {
            var wish = txt_wish.Text;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand select_command = new SqlCommand(@"SELECT * FROM WISHES
                                                         WHERE WISHER_ID = @wisher_id AND WISH = @wish", con);

            select_command.Parameters.AddWithValue("@wisher_id", infos[0]);
            select_command.Parameters.AddWithValue("@wish", wish);

            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(select_command);

            DataTable dataTable_checkWishes = new DataTable();
            adapter.Fill(dataTable_checkWishes);

            if (dataTable_checkWishes.Rows.Count < 1)
            {
                SqlCommand insert_command = new SqlCommand(@"INSERT INTO WISHES (WISHER_ID, WISH, COMMENT, FULFILLED, TIME_CREATED)
                                                             VALUES (@wisher_id, @wish, @comment, @fulfilled, @time_created)", con);

                insert_command.Parameters.AddWithValue("@wisher_id", infos[0]);
                insert_command.Parameters.AddWithValue("@wish", wish);
                insert_command.Parameters.AddWithValue("@comment", "");
                insert_command.Parameters.AddWithValue("@fulfilled", 0);
                insert_command.Parameters.AddWithValue("@time_created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                var ok = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’ HH:mm:ss");

                insert_command.ExecuteNonQuery();

                txt_wish.Text = "";
                modal_addWish.Style.Add("display", "none");

                gridview_update();
            }
            else
            {
                lbl_warning.Text = "You already wished for that!";
                lbl_warning.ForeColor = System.Drawing.Color.Red;

                txt_wish.Text = "";
            }
        }

        protected void gridview_update()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand select_command = new SqlCommand(@"SELECT WISHES.TIME_CREATED, WISHES.ID FROM WISHES
                                                         INNER JOIN USERS 
                                                         ON WISHES.WISHER_ID = USERS.ID
                                                         WHERE USERS.ID = @id", con);

            select_command.Parameters.AddWithValue("@id", infos[0]);

            SqlDataAdapter adapter = new SqlDataAdapter(select_command);

            DataTable dataTable_times = new DataTable();
            adapter.Fill(dataTable_times);

            con.Open();

            for (int i = 0; i < dataTable_times.Rows.Count; i++)
            {
                DataRow row = dataTable_times.Rows[i];
                DateTime date = Convert.ToDateTime(row[0]);
                int id = Convert.ToInt32(row[1]);

                TimeSpan maxTimeSpan = new TimeSpan(72, 0, 0);

                if (DateTime.Now.Subtract(date) > maxTimeSpan)
                {
                    SqlCommand update_command = new SqlCommand(@"UPDATE WISHES
                                                                 SET HIDE = 1
                                                                 WHERE ID = @id", con);

                    update_command.Parameters.AddWithValue("@id", id);

                    update_command.ExecuteNonQuery();
                }
            }

            select_command = new SqlCommand(@"SELECT WISHES.ID, USERS.FIRSTNAME, WISHES.WISH, WISHES.COMMENT, WISHES.FULFILLED FROM USERS
                                              INNER JOIN WISHES
                                              ON USERS.ID = WISHES.WISHER_ID
                                              WHERE USERS.ID = @id AND HIDE = 0" , con);

            select_command.Parameters.AddWithValue("@id", infos[0]);

            adapter = new SqlDataAdapter(select_command);

            DataTable dataTable_wishes = new DataTable();
            adapter.Fill(dataTable_wishes);

            gridview_wishes.DataSource = dataTable_wishes;
            gridview_wishes.DataBind();
        }

        protected void gridview_wishes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridview_wishes.EditIndex = e.NewEditIndex;
            gridview_update();
        }

        protected void gridview_wishes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gridview_wishes.Rows[e.RowIndex];
            int id_wish = Convert.ToInt32(row.Cells[0].Text);
            string wish = ((TextBox)gridview_wishes.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string comment = ((TextBox)gridview_wishes.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand update_command = new SqlCommand(@"UPDATE WISHES
                                                         SET WISH = @wish, COMMENT = @comment
                                                         WHERE ID = @id_wish", con);

            update_command.Parameters.AddWithValue("@wish", wish);
            update_command.Parameters.AddWithValue("@comment", comment);
            update_command.Parameters.AddWithValue("@id_wish", id_wish);

            con.Open();

            update_command.ExecuteNonQuery();
            gridview_wishes.EditIndex = -1;

            gridview_update();
        }

        protected void gridview_wishes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridview_wishes.EditIndex = -1;
            gridview_update();
        }

        protected void gridview_wishes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)gridview_wishes.Rows[e.RowIndex];
            int id_wish = Convert.ToInt32(row.Cells[0].Text);

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand delete_command = new SqlCommand(@"DELETE FROM WISHES
                                                         WHERE ID = @id AND FULFILLED != 1", con);

            delete_command.Parameters.AddWithValue("@id", id_wish);

            con.Open();

            if (delete_command.ExecuteNonQuery() == 1)
            {
                gridview_wishes.EditIndex = -1;

                gridview_update();
            } 
            else
            {
                lbl_warning_delete.Text = "This wish is already being fulfilled!";
                lbl_warning_delete.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_wishes_granted_by_you_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserGrantedWishes.aspx");
        }
    }
}