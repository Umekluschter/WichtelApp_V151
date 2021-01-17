using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WichtelApp_V151
{
    public partial class UserGrantedWishes : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string[] infos;

        protected void Page_Load(object sender, EventArgs e)
        {
            var session = Session["Logged_User"];
            if (session != null)
            {
                infos = session.ToString().Split(',');

                lbl_title.Text = $"Wishes that you have granted";

                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand select_command_granted_wishes = new SqlCommand(@"SELECT WISHES.ID, WISH, COMMENT
                                                                            FROM WISHES 
                                                                            INNER JOIN USERS ON WISHER_ID = USERS.ID 
                                                                            WHERE GRANTED_BY = @id;", con);

                select_command_granted_wishes.Parameters.AddWithValue("@id", infos[0]);

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(select_command_granted_wishes);

                DataTable dataTable_granted_wishes = new DataTable();
                adapter.Fill(dataTable_granted_wishes);

                gridview_granted_wishes.DataSource = dataTable_granted_wishes;
                gridview_granted_wishes.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btn_return_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }
    }
}