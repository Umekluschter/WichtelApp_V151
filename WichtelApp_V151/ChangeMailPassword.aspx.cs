using System;
using System.Data.SqlClient;
using System.Configuration;

namespace WichtelApp_V151
{
    public partial class ChangeMailPassword : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string[] infos;
        protected void Page_Load(object sender, EventArgs e)
        {
            var session = Session["Logged_User"];
            if (session != null)
            {
                infos = session.ToString().Split(',');
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btn_change_Click(object sender, EventArgs e)
        {
            var mail = txt_mail.Text;
            var password = txt_password.Text;
            var password_again = txt_password_again.Text;

            if (mail.Equals(""))
            {
                if (password.Equals(password_again))
                {
                    SqlConnection con = new SqlConnection(connectionString);

                    SqlCommand insert_command = new SqlCommand(@"UPDATE USERS 
                                                                 SET PASSWORD = @password
                                                                 WHERE ID = @id", con);

                    insert_command.Parameters.AddWithValue("@password", password);
                    insert_command.Parameters.AddWithValue("@id", infos[0]);

                    con.Open();

                    insert_command.ExecuteNonQuery();

                    Response.Redirect("MainPage.aspx");
                }
                else
                {
                    lbl_warning.Text = "Passwords do not match!";
                    lbl_warning.ForeColor = System.Drawing.Color.Red;

                    txt_password.Text = "";
                    txt_password_again.Text = "";
                }
            }
            else if (password.Equals("") && password_again.Equals(""))
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
                    SqlCommand insert_command = new SqlCommand(@"UPDATE USERS 
                                                                 SET MAIL = @mail
                                                                 WHERE ID = @id", con);

                    insert_command.Parameters.AddWithValue("@mail", mail);
                    insert_command.Parameters.AddWithValue("@id", infos[0]);

                    insert_command.ExecuteNonQuery();

                    Response.Redirect("MainPage.aspx");
                }
                else
                {
                    lbl_warning.Text = "This E-Mail already exists!";
                    lbl_warning.ForeColor = System.Drawing.Color.Red;

                    txt_mail.Text = "";
                }
            }
            else
            {
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
                        SqlCommand insert_command = new SqlCommand(@"UPDATE USERS 
                                                                     SET MAIL = @mail, PASSWORD = @password
                                                                     WHERE ID = @id", con);

                        insert_command.Parameters.AddWithValue("@mail", mail);
                        insert_command.Parameters.AddWithValue("@password", password);
                        insert_command.Parameters.AddWithValue("@id", infos[0]);

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
}