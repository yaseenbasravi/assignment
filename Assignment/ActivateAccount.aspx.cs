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
    public partial class ActivateAccount : Page
    {
        // Replace "Your_Connection_String_Here" with your actual connection string
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\conString.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActivateAccountFromQueryString();
            }
        }

        private void ActivateAccountFromQueryString()
        {
            string activationCode = Request.QueryString["activation_code"];
            string email = Request.QueryString["email"];

            if (!string.IsNullOrEmpty(activationCode) && !string.IsNullOrEmpty(email))
            {
                if (IsAccountActivated(email, activationCode))
                {
                    // Update the account activation status
                    UpdateActivationStatus(email);

                    errorMSG.Text = "Your account is activated. Kindly login to your account.";
                    errorMSG.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    errorMSG.Text = "Link has expired or is invalid.";
                    errorMSG.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                errorMSG.Text = "Activation code or email is missing.";
                errorMSG.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool IsAccountActivated(string email, string activationCode)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string checkActivation = "SELECT user_id FROM userinfo WHERE email = @email AND activation_code = @activationCode AND activation_code != 0 AND is_active != 1";

                using (SqlCommand cmd = new SqlCommand(checkActivation, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@activationCode", activationCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        private void UpdateActivationStatus(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string updateAcc = "UPDATE userinfo SET is_active = 1, activation_code = 0 WHERE email = @email";

                using (SqlCommand cmdUpdate = new SqlCommand(updateAcc, con))
                {
                    cmdUpdate.Parameters.AddWithValue("@email", email);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
        }

        protected void btnProceedToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnBackToRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
    }
}