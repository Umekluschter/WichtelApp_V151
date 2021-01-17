<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeMailPassword.aspx.cs" Inherits="WichtelApp_V151.ChangeMailPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Mail or Password</title>
    <link rel="Stylesheet" href="css/style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Change Mail or Password</h1>
            <br />
            <label>E-Mail</label>
            <asp:TextBox id="txt_mail" runat="server"/>
            <br />
            <label>Password</label>
            <asp:TextBox id="txt_password" type="Password" runat="server"/>
            <br />
            <label>Password again</label>
            <asp:TextBox id="txt_password_again" type="Password" runat="server"/>
            <br />
            <asp:Label id="lbl_warning" runat="server"/>
            <br />
            <asp:Button id="btn_change" text="Change" onclick="btn_change_Click" runat="server"/>
        </div>
    </form>
</body>
</html>
