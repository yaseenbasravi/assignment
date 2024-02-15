<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivateAccount.aspx.cs" Inherits="Assignment.ActivateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Activate Account</h1><br />
            <asp:Label ID="errorMSG" runat="server" Text=""></asp:Label>
            <br />
             <asp:Button ID="btnBackToRegister" runat="server" Text="Back to Register" OnClick="btnBackToRegister_Click" />
            <asp:Button ID="btnProceedToLogin" runat="server" Text="Proceed to Login" OnClick="btnProceedToLogin_Click" />
        </div>
    </form>
</body>
</html>
