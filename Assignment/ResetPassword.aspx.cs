using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Assignment
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerifyToken();
            }
        }

        private void VerifyToken()
        {
            string token = Request.QueryString["token"];
            if (string.IsNullOrEmpty(token))
            {
                lblStatus.Text = "Invalid password reset token.";
                return;
            }

            con.Open();
            string query = "SELECT email, token_expiry FROM [userinfo] WHERE reset_token=@token";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@token", token);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (reader["token_expiry"] != DBNull.Value && DateTime.Now < Convert.ToDateTime(reader["token_expiry"]))
                {
                    // Token is valid and not expired
                    ViewState["email"] = reader["email"].ToString();
                }
                else
                {
                    // Token is expired
                    lblStatus.Text = "Your password reset token has expired.";
                }
            }
            else
            {
                // Token not found
                lblStatus.Text = "Invalid password reset token.";
            }
            con.Close();
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (newPassword != confirmPassword)
            {
                lblStatus.Text = "Passwords do not match.";
                return;
            }

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6) // Example: check for at least 6 characters
            {
                lblStatus.Text = "Password must be at least 6 characters long.";
                return;
            }

            // All checks passed, reset the password
            con.Open();
            string updateQuery = @"
                UPDATE [userinfo] SET 
                password=@newPassword, 
                reset_token=NULL, 
                token_expiry=NULL 
                WHERE email=@email AND reset_token=@token";

            SqlCommand updateCmd = new SqlCommand(updateQuery, con);
            updateCmd.Parameters.AddWithValue("@newPassword", newPassword); // Consider hashing the password
            updateCmd.Parameters.AddWithValue("@email", ViewState["email"]);
            updateCmd.Parameters.AddWithValue("@token", Request.QueryString["token"]);

            if (updateCmd.ExecuteNonQuery() > 0)
            {
                lblStatus.Text = "Your password has been reset successfully.";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                
                con.Close();

                string script = @"
                <script>
                    window.onload = function() {
                     document.getElementById('lblStatus').innerHTML = 'Your password has been reset successfully.';
                     document.getElementById('lblStatus').style.color = 'green';
                     setTimeout(function() {
                     window.location.href = 'Login.aspx';
                     }, 2000); // 2 seconds delay
                    }
                </script>";
                
                ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script);
            }
        
            else
            {
                lblStatus.Text = "Error resetting your password.";
            }
            con.Close();
        }
    }
}