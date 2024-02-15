using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace Assignment
{
    public partial class AdminInterface : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string query = "SELECT user_id, first_name, last_name, email FROM userinfo WHERE user_type = 'chef' AND is_active = 0";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }

        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            int userId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values["user_id"]);

            string email = "";
            string firstname = "";
            string lastname = "";
            GetUserInfo(userId, out email, out firstname, out lastname);

            if (e.CommandName == "Approve")
            {
                // Call a method to approve the chef account based on userId
                ApproveChefAccount(userId,email,firstname,lastname);
            }
            else if (e.CommandName == "Deny")
            {
               
                // Call a method to deny the chef account based on userId
                DenyChefAccount(userId,email,firstname,lastname);
            }
            else if (e.CommandName == "View")
            {
                
                // Redirect to view chef account details page passing the userId as parameter
                Response.Redirect($"ViewChefAccount.aspx?userId={userId}");
            }

            // Rebind the gridview after command execution
            BindGridView();
        }

        private void ApproveChefAccount(int userId,string email,string firstname, string lastname)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string updateAcc = "UPDATE userinfo SET is_active = 1 WHERE user_id = @userId";
                
                using (SqlCommand cmdUpdate = new SqlCommand(updateAcc, con))
                {
                    cmdUpdate.Parameters.AddWithValue("@userId", userId);
                    cmdUpdate.ExecuteNonQuery();
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email.ToString());
                    mail.From = new MailAddress("sambrunsen01@gmail.com");
                    mail.Subject = "Your account has been successfully registered";

                    string emailBody = "";

                    emailBody += "<h1>Hello " + firstname.ToString() + "</h1>";
                    emailBody += "Congratulations on Successfully getting registered as a Chef.<br/>";
                   emailBody += "Thank you";

                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new NetworkCredential("sambrunsen01@gmail.com", "hwam yjud dkof nipb");
                    smtp.Send(mail);


                    //lblErrorMsg.Text = "Please check your email for activation link";
                    //lblErrorMsg.ForeColor = System.Drawing.Color.Green;
                }
            }

           
        }

        private void DenyChefAccount(int userId, string email, string firstname, string lastname)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string updateAcc = "UPDATE userinfo SET is_active = 0 WHERE user_id = @userId";

                using (SqlCommand cmdUpdate = new SqlCommand(updateAcc, con))
                {
                    cmdUpdate.Parameters.AddWithValue("@userId", userId);
                    cmdUpdate.ExecuteNonQuery();
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email.ToString());
                    mail.From = new MailAddress("sambrunsen01@gmail.com");
                    mail.Subject = "Your account has been rejected";

                    string emailBody = "";

                    emailBody += "<h1>Hello " + firstname.ToString() + "</h1>";
                    emailBody += "Unfortunately you weren't approved to be a chef<br/>";
                    emailBody += "Thank you";

                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new NetworkCredential("sambrunsen01@gmail.com", "hwam yjud dkof nipb");
                    smtp.Send(mail);


                    //lblErrorMsg.Text = "Please check your email for activation link";
                    //lblErrorMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        private void GetUserInfo(int userId, out string email, out string firstname, out string lastname)
        {
            email = "";
            firstname = "";
            lastname = "";

            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string query = "SELECT email, first_name, last_name FROM userinfo WHERE user_id = @userId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        email = reader["email"].ToString();
                        firstname = reader["first_name"].ToString();
                        lastname = reader["last_name"].ToString();
                    }
                    reader.Close();
                }
            }
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            // Redirect to the login page
            Response.Redirect("~/Login.aspx");
        }
    }
}