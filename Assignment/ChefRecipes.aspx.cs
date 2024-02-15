using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Assignment
{
    public partial class ChefRecipes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate recipes in repeater
                BindRecipes();
            }
        }

        private void BindRecipes()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT recipe_id, title,description, num_comments FROM Recipe";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        rptRecipes.DataSource = dt;
                        rptRecipes.DataBind();
                    }
                }
            }

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            // Handle logout
            // Redirect to login page
            Response.Redirect("Login.aspx");
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            // Handle adding comment to recipe
            // You can use CommandArgument to get the RecipeId for the clicked button
            // For example:
            int recipeId = Convert.ToInt32((sender as Button).CommandArgument);
            Response.Redirect($"RecipeDetails.aspx?recipeId={recipeId}");
        }
    }
}