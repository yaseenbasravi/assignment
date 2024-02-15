using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Assignment
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Attempt to activate the account if activation parameters are present.
                string activationCode = Request.QueryString["activation_code"];
                string userEmail = Request.QueryString["email"];

                if (!string.IsNullOrEmpty(activationCode) && !string.IsNullOrEmpty(userEmail))
                {
                    ActivateAccount(activationCode, userEmail);
                }
            }
        }

        private void ActivateAccount(string activationCode, string userEmail)
        {
            if (IsAccountActivated(userEmail, activationCode))
            {
                UpdateActivationStatus(userEmail);
                errorMSG.Text = "Your account has been activated. Please log in.";
                errorMSG.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                errorMSG.Text = "Your activation link is invalid or has expired.";
                errorMSG.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool IsAccountActivated(string email, string activationCode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
            {
                con.Open();
                string query = "SELECT user_id FROM userinfo WHERE email = @Email AND activation_code = @ActivationCode AND is_active = 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        private void UpdateActivationStatus(string email)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString))
            {
                con.Open();
                string query = "UPDATE userinfo SET is_active = 1 WHERE email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        protected void lgnBtn_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(password.Text))
                {
                    errorMSG.Text = "Please fill in all the fields.";
                    errorMSG.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                con.Open();
                string checkUser = @"
                SELECT user_id, email, first_name, last_name, password, is_active, reset_token, token_expiry, user_type 
                FROM [userinfo] 
                WHERE email=@email";
                SqlCommand checkCmd = new SqlCommand(checkUser, con);
                checkCmd.Parameters.AddWithValue("@email", email.Text);
                SqlDataReader read = checkCmd.ExecuteReader();

                if (read.Read())
                {
                    // Check if there's a non-expired reset token
                    bool isTokenValid = read["reset_token"] != DBNull.Value &&
                                        read["token_expiry"] != DBNull.Value &&
                                        DateTime.Now < Convert.ToDateTime(read["token_expiry"]);

                    if (isTokenValid)
                    {
                        errorMSG.Text = "A password reset is in progress. Please check your email to complete the process.";
                        errorMSG.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (read["password"].ToString() == password.Text)
                    {
                        // Check if the account is active
                        bool isActive = read["is_active"] != DBNull.Value && Convert.ToBoolean(read["is_active"]);
                        if (isActive)
                        {
                            // If login is successful, store user's name in session
                            string Name = read["first_name"].ToString() + " " + read["last_name"].ToString();
                            Session["UserName"] = Name;
                            string role = read["user_type"].ToString();
                            // Redirect based on role
                            if (role == "user")
                            {
                                // Redirect to user interface
                                Response.Redirect("ChefRecipe.aspx");
                            }
                            else if (role == "chef")
                            {
                                // Redirect to chef interface
                                Response.Redirect("ChefHome.aspx");
                            }
                            else if (role == "admin")
                            {
                                // Redirect to admin interface
                                Response.Redirect("AdminInterface.aspx");
                            }
                            // Your session logic here
                            Response.Redirect("Home.aspx");
                        }
                        else
                        {
                            errorMSG.Text = "Your account is not activated.";
                            errorMSG.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        errorMSG.Text = "Incorrect password.";
                        errorMSG.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    errorMSG.Text = "The email is not registered with us.";
                    errorMSG.ForeColor = System.Drawing.Color.Red;
                }
                con.Close();
            }
        }

            protected void ForgotPwdBtn_Click(object sender, EventArgs e)
            {
            if (string.IsNullOrWhiteSpace(email.Text))
            {
                errorMSG.Text = "Please enter your email address.";
                errorMSG.ForeColor = System.Drawing.Color.Red;
                return;
            }

            con.Open();
            string checkUser = "select email from [userinfo] where email=@email";
            SqlCommand checkCmd = new SqlCommand(checkUser, con);
            checkCmd.Parameters.AddWithValue("@email", email.Text);
            SqlDataReader read = checkCmd.ExecuteReader();

            if (read.HasRows)
            {
                // Assuming you only want to avoid updating the email in the reset process
                // Generate reset token
                string resetToken = Guid.NewGuid().ToString(); // Using GUID for simplicity and uniqueness
                DateTime tokenExpiry = DateTime.Now.AddHours(1); // Token expires in 1 hour

                con.Close(); // Close the initial read connection
                con.Open(); // Open a new connection to update data
                string updateToken = "UPDATE [userinfo] SET reset_token=@resetToken, token_expiry=@tokenExpiry WHERE email=@email";
                SqlCommand updateCmd = new SqlCommand(updateToken, con);
                updateCmd.Parameters.AddWithValue("@resetToken", resetToken);
                updateCmd.Parameters.AddWithValue("@tokenExpiry", tokenExpiry);
                updateCmd.Parameters.AddWithValue("@email", email.Text);
                updateCmd.ExecuteNonQuery();

                // Send email with reset link
                string resetLink = "https://localhost:44370/ResetPassword.aspx?token=" + resetToken;
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("sambrunsen01@gmail.com"),
                    Subject = "Password Reset",
                    Body = $"Please click on the following link to reset your password: <a href='{resetLink}'>Reset Password</a>",
                    IsBodyHtml = true,
                };
                mail.To.Add(email.Text);

                SmtpClient smtp = new SmtpClient
                {
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    Credentials = new NetworkCredential("sambrunsen01@gmail.com", "hwam yjud dkof nipb")
                };
                smtp.Send(mail);

                errorMSG.Text = "Please check your email to reset password.";
                errorMSG.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                errorMSG.Text = "The email does not exist.";
                errorMSG.ForeColor = System.Drawing.Color.Red;
            }
            con.Close();
        }
    

    }
    
}