using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace WichtelApp_V151
{
    public partial class AllUsers : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string[] infos;

        protected void Page_Load(object sender, EventArgs e)
        {
            var session = Session["Logged_User"];
            if (session != null)
            {
                infos = session.ToString().Split(',');

                lbl_title.Text = "All Users";

                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand select_command = new SqlCommand(@"SELECT ID, FIRSTNAME FROM USERS
                                                             WHERE ID != @id", con);

                select_command.Parameters.AddWithValue("@id", infos[0]);

                SqlDataAdapter adapter = new SqlDataAdapter(select_command);

                DataTable dataTable_users = new DataTable();
                adapter.Fill(dataTable_users);

                gridview_users.DataSource = dataTable_users;
                gridview_users.DataBind();
            } else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btn_return_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void gridview_users_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = (GridViewRow)gridview_users.Rows[e.NewSelectedIndex];
            int id_user = Convert.ToInt32(row.Cells[0].Text);

            Session["Logged_User"] = $"{infos[0]},{infos[1]},{infos[2]},{id_user}";
            Response.Redirect("UserWishlist.aspx");
        }
    }
}