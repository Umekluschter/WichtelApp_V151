<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WichtelApp_V151.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="Stylesheet" href="css/style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Login</h1>
            <br />
            <label>E-Mail</label>
            <asp:TextBox id="txt_mail" runat="server"/>
            <br />
            <label>Password</label>
            <asp:TextBox id="txt_password" type="Password" runat="server"/>
            <br />
            <asp:Label id="lbl_warning" runat="server"/>
            <br />
            <asp:Button id="btn_login" text="Login" onclick="btn_login_Click" runat="server"/>
            <asp:Button id="btn_register" text="Register" onclick="btn_register_Click" runat="server"/>
        </div>
    </form>
</body>
</html>
