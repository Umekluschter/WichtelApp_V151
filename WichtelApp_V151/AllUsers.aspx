<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllUsers.aspx.cs" Inherits="WichtelApp_V151.AllUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Users</title>
    <link rel="Stylesheet" href="css/style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label id="lbl_title" runat="server" />
            <br />
            <asp:Button id="btn_return" text="Return" OnClick="btn_return_Click" runat="server"/>
            <br />
            <br />
            <asp:GridView id="gridview_users" runat="server"
                AutoGenerateColumns="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None" OnSelectedIndexChanging="gridview_users_SelectedIndexChanging" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="FIRSTNAME" HeaderText="Vorname" ReadOnly="True" />
                    <asp:CommandField ButtonType="Button" SelectText="Go to Wishlist" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
