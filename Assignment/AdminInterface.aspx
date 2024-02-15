<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminInterface.aspx.cs" Inherits="Assignment.AdminInterface" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chef Approval-Admin</title>
        <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        .header {
            background-color: #1C5E55;
            color: white;
            padding: 10px 20px;
            text-align: center;
            margin-bottom: 20px;
        }

        .header h1 {
            margin: 0;
        }

        .logout-btn {
            background-color: #D81B60;
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            text-decoration: none;
            position:relative;
            right:0;
        }

        .logout-btn:hover {
            background-color: #C2185B;
        }

        .grid-container {
            background-color: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .grid-container h2 {
            margin-top: 0;
        }

        .grid-container .grid-view {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                <div class="container">
            <div class="header">
                <h1>Chef Account Approval</h1>
                
                <asp:Button ID="ForgotPwdBtn" runat="server" CssClass="logout-btn" Text="Log Out" OnClick="LogoutBtn_Click"/>
            </div>

            <div class="grid-container">
                <h2>Chef Account Details</h2>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="user_id" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" CssClass="grid-view">
                    <AlternatingRowStyle BackColor="#F2F2F2" />
                    <Columns>
                        <asp:BoundField DataField="user_id" HeaderText="User ID" InsertVisible="False" ReadOnly="True" SortExpression="user_id" />
                        <asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" />
                        <asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" />
                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                        <asp:ButtonField ButtonType="Button" CommandName="Approve" Text="Approve" />
                        <asp:ButtonField ButtonType="Button" CommandName="Deny" Text="Deny" />
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
