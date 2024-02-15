<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment.WebForm3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link rel="stylesheet" href="login.css" />
</head>
<body>
    <form id="form1" runat="server"> <!-- The form starts here -->
        <div class="cards-container">
            <div class="card">
                <div class="card-content">
                <h1>Login to Your Account</h1>
                <asp:Label ID="errorMSG" runat="server" CssClass="error-msg" Text=""></asp:Label>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="email" runat="server" CssClass="input" Placeholder="Enter your Email Address"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        ID="emailRegExValidator" 
                        runat="server" 
                        ControlToValidate="email" 
                        ErrorMessage="Please enter a valid email address." 
                        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
                        ForeColor="Red" 
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="password" runat="server" CssClass="input" TextMode="Password" Placeholder="Enter your Password"></asp:TextBox>
                </div>                
                <asp:Button ID="lgnBtn" runat="server" CssClass="button" Text="Login" OnClick="lgnBtn_Click"/>
                <asp:Button ID="ForgotPwdBtn" runat="server" CssClass="button" Text="Forgot Password" OnClick="ForgotPwdBtn_Click"/>
            </div>
            <div class="card">
                <h2>Register New Account</h2>
                <div class="join-us">Join us now!</div>
                <asp:Image ID="LearnImage" runat="server" ImageUrl="~/Image/823214.png" CssClass="learn-image" AlternateText="Learn More" />
                <asp:Button ID="registerBtn" runat="server" CssClass="button" Text="Register an account" PostBackUrl="Registration.aspx"/>
            </div>
        </div>
    </form>
</body>
</html>