using System;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;


namespace Assignment
{
    public partial class ChefRegistration : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void chefRegBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstname.Text) || string.IsNullOrEmpty(lastname.Text) ||
                string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text) ||
                string.IsNullOrEmpty(phoneNumber.Text) || string.IsNullOrEmpty(yearsExperience.Text))
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

            // Check for file uploads
            string certificationPath = SaveFile(fileCertifications, "certifications");
            string resumePath = SaveFile(fileResume, "resumes");

            // Open database connection
            con.Open();

            // Prepare SQL query to insert chef registration details into the database
            string insertQuery = "INSERT INTO [userinfo] (first_name, last_name, email, password, phone_number, years_of_experience, certification_path, resume_path,is_active,user_type) " +
                                 "VALUES (@fname, @lname, @email, @pwd, @phone, @experience, @certificationPath, @resumePath,@is_active, @user_type)";

            // Create SqlCommand object
            SqlCommand cmd = new SqlCommand(insertQuery, con);

            // Add parameters to the query
            cmd.Parameters.AddWithValue("@fname", firstname.Text);
            cmd.Parameters.AddWithValue("@lname", lastname.Text);
            cmd.Parameters.AddWithValue("@email", email.Text);
            cmd.Parameters.AddWithValue("@pwd", password.Text);
            cmd.Parameters.AddWithValue("@phone", phoneNumber.Text);
            cmd.Parameters.AddWithValue("@experience", yearsExperience.Text);
            cmd.Parameters.AddWithValue("@certificationPath", certificationPath);
            cmd.Parameters.AddWithValue("@resumePath", resumePath);
            cmd.Parameters.AddWithValue("@is_active", 0);
            cmd.Parameters.AddWithValue("@user_type", "chef");

            // Execute the query
            cmd.ExecuteNonQuery();

            // Close database connection
            con.Close();

            // Display success message
            lblErrorMsg.Text = "Chef registration submitted for approval. Files uploaded.";
            lblErrorMsg.ForeColor = System.Drawing.Color.Green;
        }

        private string SaveFile(FileUpload fileUploadControl, string folder)
        {
            if (fileUploadControl.HasFile)
            {
                // Ensure the directory exists
                string uploadFolderPath = Server.MapPath($"~/uploads/{folder}/");
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                // Generate a unique file name to prevent overwriting
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUploadControl.FileName);
                string filePath = Path.Combine(uploadFolderPath, fileName);

                // Save the file
                fileUploadControl.SaveAs(filePath);
                return $"~/uploads/{folder}/{fileName}"; // Save relative path in DB
            }
            return null; // Or handle this case as needed
        }

        // Method to validate email format using regular expression
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}