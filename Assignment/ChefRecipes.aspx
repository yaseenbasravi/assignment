<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChefRecipes.aspx.cs" Inherits="Assignment.ChefRecipes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chef Recipes Forum</title>
    <style>
        /* CSS for styling */
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }
        .header {
            background-color: #1C5E55;
            color: white;
            padding: 20px;
            text-align: center;
        }
        .container {
            max-width: 800px;
            margin: 20px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .recipe-card {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 20px;
            margin-bottom: 20px;
        }
        .recipe-title {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 10px;
        }
        .recipe-description {
            margin-bottom: 10px;
        }
        .comment-counter {
            position: relative;
            bottom: 0px;
            right: 0px;
            color: #888;
        }
        .add-comment-btn {
            background-color: #1C5E55;
            color: white;
            border: none;
            border-radius: 4px;
            padding: 10px 20px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .add-comment-btn:hover {
            background-color: #144a42;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
        <h1>Chef Recipes</h1>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn-logout" OnClick="btnLogout_Click" />
    </div>
    <div class="container">
        <asp:Repeater ID="rptRecipes" runat="server">
            <ItemTemplate>
                <div class="recipe-card">
                    <h2 class="recipe-title"><%# Eval("title") %></h2>
                    <p class="recipe-description"><%# Eval("description") %></p>
                    <asp:Button ID="btnAddComment" runat="server" CssClass="add-comment-btn" Text="Add Comment" OnClick="btnAddComment_Click" CommandArgument='<%# Eval("recipe_id") %>' />
                    <span class="comment-counter"><%# Eval("num_comments") %> Comments</span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
