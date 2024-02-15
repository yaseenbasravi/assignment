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
    public partial class RecipeDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string chefName = Session["UserName"] as string;
                if (chefName == null)
                {
                    Response.Redirect("Login.aspx");
                }
                // Retrieve recipe details and comments
                int recipeId = Convert.ToInt32(Request.QueryString["recipeId"]);
                PopulateRecipeDetails(recipeId);
                PopulateComments(recipeId);
            }
        }

        private void PopulateRecipeDetails(int recipeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string query = "SELECT title, description FROM recipe WHERE recipe_id = @RecipeId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTitle.Text = reader["title"].ToString();
                            lblDescription.Text = reader["description"].ToString();
                        }
                    }
                }
            }
        }

        private void PopulateComments(int recipeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string query = "SELECT username, comment_text FROM comments WHERE recipe_id = @RecipeId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        rptComments.DataSource = reader;
                        rptComments.DataBind();
                    }
                }
            }
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            int recipeId = Convert.ToInt32(Request.QueryString["recipeId"]);
            string username = Session["Username"].ToString(); // Assuming username is stored in session
            string commentText = txtNewComment.Text.Trim();

            // Insert the comment into the database
            InsertComment(recipeId, username, commentText);

            // Refresh the comments section
            PopulateComments(recipeId);

            // Clear the comment textbox
            txtNewComment.Text = "";
        }

        private void InsertComment(int recipeId, string username, string commentText)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string query = "INSERT INTO comments (recipe_id, username, comment_text) VALUES (@RecipeId, @Username, @CommentText)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@CommentText", commentText);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Increment the number of comments in the recipe table
            IncrementCommentCount(recipeId);
        }

        private void IncrementCommentCount(int recipeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string query = "UPDATE recipe SET num_comments = num_comments + 1 WHERE recipe_id = @RecipeId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                    con.Open();
                    cmd.ExecuteNonQuery();
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