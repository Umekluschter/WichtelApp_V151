using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace WichtelApp_V151
{
    public partial class UserWishlist : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string[] infos;

        protected void Page_Load(object sender, EventArgs e)
        {
            var session = Session["Logged_User"];
            if (session != null)
            {
                infos = session.ToString().Split(',');

                if (!IsPostBack)
                {
                    SqlConnection con = new SqlConnection(connectionString);

                    SqlCommand select_command_getUser = new SqlCommand(@"SELECT FIRSTNAME FROM USERS
                                                                     WHERE ID = @id", con);

                    select_command_getUser.Parameters.AddWithValue("@id", infos[3]);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(select_command_getUser);

                    DataTable dataTable_User = new DataTable();
                    adapter.Fill(dataTable_User);

                    lbl_title.Text = $"Wishes from {dataTable_User.Rows[0][0]}";

                    gridview_update();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void btn_return_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllUsers.aspx");
        }

        protected void gridview_update()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand select_command = new SqlCommand(@"SELECT ID, WISH, COMMENT, FULFILLED FROM WISHES
                                                         WHERE WISHER_ID = @id AND GRANTED_BY IS NULL AND HIDE = 0", con);

            select_command.Parameters.AddWithValue("@id", infos[3]);

            SqlDataAdapter adapter = new SqlDataAdapter(select_command);

            DataTable dataTable_users = new DataTable();
            adapter.Fill(dataTable_users);

            gridview_wishes_user.DataSource = dataTable_users;
            gridview_wishes_user.DataBind();
        }

        protected void gridview_wishes_user_SelectedIndexChanging(object sender, System.Web.UI.WebControls.GridViewSelectEventArgs e)
        {
            GridViewRow row = (GridViewRow)gridview_wishes_user.Rows[e.NewSelectedIndex];
            int id_wish = Convert.ToInt32(row.Cells[0].Text);

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand update_command = new SqlCommand(@"UPDATE WISHES
                                                         SET GRANTED_BY = @id_session_user, FULFILLED = 1
                                                         WHERE ID = @id_wish", con);

            update_command.Parameters.AddWithValue("@id_session_user", infos[0]);
            update_command.Parameters.AddWithValue("id_wish", id_wish);

            con.Open();

            update_command.ExecuteNonQuery();

            gridview_update();
        }

        protected void gridview_wishes_user_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridview_wishes_user.EditIndex = e.NewEditIndex;
            gridview_update();
        }

        protected void gridview_wishes_user_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gridview_wishes_user.Rows[e.RowIndex];
            int id_wish = Convert.ToInt32(row.Cells[0].Text);
            string comment = ((TextBox)gridview_wishes_user.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand update_command = new SqlCommand(@"UPDATE WISHES
                                                         SET COMMENT = @comment
                                                         WHERE ID = @id_wish", con);

            update_command.Parameters.AddWithValue("@comment", comment);
            update_command.Parameters.AddWithValue("@id_wish", id_wish);

            con.Open();

            update_command.ExecuteNonQuery();
            gridview_wishes_user.EditIndex = -1;

            gridview_update();
        }
    }
}