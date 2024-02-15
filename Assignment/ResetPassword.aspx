<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Assignment.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <link rel="stylesheet"  href="ResetPassword.css"/>
</head>
<body>
<form id="form1" runat="server">
        <div>

            <h2>Reset Password</h2>
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="lblNewPassword" runat="server" Text="New Password:"></asp:Label>
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" />
            <br />
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm New Password:"></asp:Label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" />
            <br />
            <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click" />
        </div>
    </form>
</body>
</html>
