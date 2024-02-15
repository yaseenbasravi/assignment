using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment
{
    public partial class ChefHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string chefName = Session["UserName"] as string;
            if (chefName == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnCreateRecipe_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            string chefName = Session["UserName"] as string;
            if (chefName != null)
            {
                // Use chefName as needed

                // Insert the new recipe into the database
                string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
                string insertQuery = "INSERT INTO Recipe (chef_name, title, description, num_comments) VALUES (@ChefName, @Title, @Description, 0)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ChefName", chefName);
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", description);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                // Optionally, you can redirect the chef to a page showing their recipes
                Response.Redirect("ChefRecipes.aspx");
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