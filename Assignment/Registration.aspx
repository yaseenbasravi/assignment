<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Assignment.WebForm2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Page</title>
    <link rel="stylesheet" href="styles.css" />
    <style type="text/css">
        .auto-style1 {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 15px 30px;
            background-color: #e67e22;
            color: white;
            cursor: pointer;
            border-radius: 25px;
            font-size: 1.1em;
            transition: background-color 0.3s ease;
            position: absolute;
            bottom: 21px;
            left: 50%;
            transform: translateX(-50%);
            text-align: center;
            width: 90%; /* Adjust the width to fit the text on one line */
            white-space: nowrap; /* Prevents the text from wrapping */
        }
        .auto-style2 {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 15px 30px;
            background-color: #e67e22;
            color: white;
            cursor: pointer;
            border-radius: 25px;
            font-size: 1.1em;
            transition: background-color 0.3s ease;
            position: absolute;
            bottom: 98px;
            left: 50%;
            transform: translateX(-50%);
            text-align: center;
            width: 90%; /* Adjust the width to fit the text on one line */
            white-space: nowrap; /* Prevents the text from wrapping */
        }
    </style>
</head>
<body>
    <div class="cards-container">
        
        <div class="white-card">
            <h1>Welcome to Our <br />Recipe School</h1>
            <h2>Learn a new recipe now</h2>
            <asp:Image ID="recipeImage" runat="server" ImageUrl="~/Image/Hao-removebg-preview.png" AlternateText="Recipe Image" CssClass="recipe-image" />
            <button onclick="location.href='login.aspx'" class="login-button">Already have an account</button>
        </div>
        
        <div class="blue-card">
            <h1>Register Page</h1>
            <form id="form1" runat="server" class="registration-form">
                <asp:Label ID="lblErrorMsg" runat="server" CssClass="error-msg"></asp:Label>
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>
                    <asp:TextBox ID="firstname" runat="server" CssClass="input" Placeholder="Enter your First Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
                    <asp:TextBox ID="lastname" runat="server" CssClass="input" Placeholder="Enter your last name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="email" runat="server" CssClass="input" Placeholder="Enter your Email Address"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="password" runat="server" CssClass="input" Placeholder="Enter your Password" TextMode="Password"></asp:TextBox>
                </div>
                <asp:Button ID="regBtn" runat="server" CssClass="auto-style2" Text="Register" OnClick="regBtn_Click"/>
               <asp:Button ID="chefRegBtn" runat="server" CssClass="auto-style1" Text="Register as a Chef" OnClick="chefRegBtn_Click"/>
            </form>
        </div>
        
    </div>
</body>
</html>