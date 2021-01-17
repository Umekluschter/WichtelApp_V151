using System;
using System.Data.SqlClient;
using System.Configuration;

namespace WichtelApp_V151
{
    public partial class Register : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            var firstname = txt_firstname.Text;
            var lastname = txt_lastname.Text;
            var mail = txt_mail.Text;
            var password = txt_password.Text;
            var password_again = txt_password_again.Text;

            if (password.Equals(password_again))
            {
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand select_command = new SqlCommand(@"SELECT * FROM USERS
                                                             WHERE MAIL = @mail", con);

                select_command.Parameters.AddWithValue("@mail", mail);

                con.Open();

                var reader = select_command.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    SqlCommand insert_command = new SqlCommand(@"INSERT INTO USERS (FIRSTNAME, LASTNAME, MAIL, PASSWORD)
                                                                 VALUES (@firstname, @lastname, @mail, @password)", con);

                    insert_command.Parameters.AddWithValue("@firstname", firstname);
                    insert_command.Parameters.AddWithValue("@lastname", lastname);
                    insert_command.Parameters.AddWithValue("@mail", mail);
                    insert_command.Parameters.AddWithValue("@password", password);

                    insert_command.ExecuteNonQuery();

                    Response.Redirect("MainPage.aspx");
                }
                else
                {
                    lbl_warning.Text = "This user already exists!";
                    lbl_warning.ForeColor = System.Drawing.Color.Red;

                    txt_mail.Text = "";
                    txt_password.Text = "";
                    txt_password_again.Text = "";
                }
            }
            else
            {
                lbl_warning.Text = "Passwords do not match!";
                lbl_warning.ForeColor = System.Drawing.Color.Red;

                txt_password.Text = "";
                txt_password_again.Text = "";
            }
        }
    }
}