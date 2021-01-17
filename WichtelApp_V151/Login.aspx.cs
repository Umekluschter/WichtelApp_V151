using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WichtelApp_V151
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            var mail = txt_mail.Text;
            var password = txt_password.Text;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand select_command = new SqlCommand(@"SELECT * FROM USERS
                                                         WHERE MAIL = @mail AND PASSWORD = @password", con);

            select_command.Parameters.AddWithValue("@mail", mail);
            select_command.Parameters.AddWithValue("@password", password);

            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(select_command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                var id = dataTable.Rows[0][0];
                var firstname = dataTable.Rows[0][1];
                var lastname = dataTable.Rows[0][2];

                Session["Logged_User"] = $"{id},{firstname},{lastname},0";
                Response.Redirect("MainPage.aspx");
            }
            else
            {
                txt_mail.Text = "";
                txt_password.Text = "";

                lbl_warning.Text = "Wrong E-Mail or Password!";
                lbl_warning.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}