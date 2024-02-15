<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChefHome.aspx.cs" Inherits="Assignment.ChefHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chef-Home</title>
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
        .input-field {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .textarea-field {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            resize: vertical;
        }
        .btn-create {
            background-color: #1C5E55;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        .btn-create:hover {
            background-color: #14483F;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div>
             <div class="header">
    <h1>Welcome, Chef!</h1>   
    <asp:Button ID="ForgotPwdBtn" runat="server" CssClass="logout-btn" Text="Log Out" OnClick="LogoutBtn_Click"/>
</div>
            <h2>Create a New Recipe</h2>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input-field" placeholder="Enter title" /><br />
            <asp:TextBox ID="txtDescription" runat="server" CssClass="textarea-field" TextMode="MultiLine" Rows="4" placeholder="Enter description" /><br />
            <asp:Button ID="btnCreateRecipe" runat="server" CssClass="btn-create" Text="Create Recipe" OnClick="btnCreateRecipe_Click" />
        </div>
    </form>
</body>
</html>
