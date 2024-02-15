<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecipeDetails.aspx.cs" Inherits="Assignment.RecipeDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recipe Details</title>
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
        .btn-logout {
            background-color: #D62828;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .btn-logout:hover {
            background-color: #B91C1C;
        }
        .btn-back {
            background-color: #1C5E55;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
            text-decoration: none;
            display: inline-block;
            margin-right: 10px;
        }
        .btn-back:hover {
            background-color: #155147;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header">
                <h1>Recipe Details</h1>
                <asp:Button ID="btnLogout" runat="server" CssClass="btn-logout" Text="Logout" OnClick="LogoutBtn_Click"/>
                <asp:HyperLink ID="lnkBack" runat="server" CssClass="btn-back" NavigateUrl="~/ChefRecipes.aspx">Back to Recipes</asp:HyperLink>
            </div>
            <div>
                <h2><asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
                <p><asp:Label ID="lblDescription" runat="server" Text=""></asp:Label></p>
                <hr />
                <h3>Comments</h3>
                <asp:Repeater ID="rptComments" runat="server">
                    <ItemTemplate>
                        <p><asp:Label ID="lblUsername" runat="server" Text='<%# Eval("username") %>'></asp:Label>: <asp:Label ID="lblComment" runat="server" Text='<%# Eval("comment_text") %>'></asp:Label></p>
                    </ItemTemplate>
                </asp:Repeater>
                <hr />
                <h3>Add a Comment</h3>
                <asp:TextBox ID="txtNewComment" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                <br />
                <asp:Button ID="btnAddComment" runat="server" Text="Add Comment" OnClick="btnAddComment_Click" />
            </div>
        </div>
    </form>
</body>
</html>
