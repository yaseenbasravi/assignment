using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Assignment
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

             
        }

        protected void regBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstname.Text) || string.IsNullOrEmpty(lastname.Text) ||
            string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                lblErrorMsg.Text = "Please fill in all the required fields.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                return; // Exit the method if any textbox is empty
            }

            // Email format validation
            if (!IsValidEmail(email.Text))
            {
                lblErrorMsg.Text = "Please enter a valid email address.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                return; // Exit the method if email format is invalid
            }
           
            if (password.Text.Length < 6)
            {
                lblErrorMsg.Text = "Password must be at least 6 characters long.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                return; // Exit the method if password length is invalid
            }

            con.Open();
            SqlCommand checkEmail = new SqlCommand("select email from [userinfo] where email='" + email.Text.ToString() + "'",con);
            checkEmail.Parameters.AddWithValue("@Email", email.Text);
            SqlDataReader read = checkEmail.ExecuteReader();


            if (read.HasRows)
            {
                lblErrorMsg.Text = "Email address already exist.";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                con.Close();
            }
            else
            {
                con.Close();
                Random random = new Random();
                int myRandom = random.Next(10000000, 99999999);
                string activation_code = myRandom.ToString();



                con.Open();
                string insertUser = "insert into [userinfo] (first_name,last_name,email,password,activation_code,is_active,user_type) values(@fname,@lname,@email_id,@pwd,@activation_code,@is_active, @user_type)";
                SqlCommand cmUser = new SqlCommand(insertUser, con);
                cmUser.Parameters.AddWithValue("@fname", firstname.Text.ToString());
                cmUser.Parameters.AddWithValue("@lname", lastname.Text.ToString());
                cmUser.Parameters.AddWithValue("@email_id", email.Text.ToString());
                cmUser.Parameters.AddWithValue("@pwd", password.Text.ToString());
                cmUser.Parameters.AddWithValue("@activation_code", activation_code);
                cmUser.Parameters.AddWithValue("@is_active", 0);
                cmUser.Parameters.AddWithValue("@user_type", "user");
                cmUser.ExecuteNonQuery();

                MailMessage mail = new MailMessage();
                mail.To.Add(email.Text.ToString());
                mail.From = new MailAddress("sambrunsen01@gmail.com");
                mail.Subject = "Your account has been successfully registered";

                string emailBody = "";

                emailBody += "<h1>Hello " + firstname.Text.ToString() + "</h1>";
                emailBody += "Click the link below to activate your account and proceed to login.<br/>";
                emailBody += "<p><a href=' "+ "https://localhost:44370/Login.aspx?activation_code=" +activation_code + "&email="+ email.Text.ToString() + " '>click here to activate your account and proceed to login.</a></P>";
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


                lblErrorMsg.Text = "Please check your email for activation link";
                lblErrorMsg.ForeColor = System.Drawing.Color.Green;
                con.Close();

            }

            bool IsValidEmail(string email)
            {
                // Regular expression pattern for email validation
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, emailPattern);
            }


        }

        protected void chefRegBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChefRegistration.aspx");
        }
    }
}