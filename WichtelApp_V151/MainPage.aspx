<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WichtelApp_V151.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wishlist</title>
    <link rel="Stylesheet" href="css/style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label id="lbl_title" runat="server" />
            <br />
            <br />
            <asp:Button id="btn_add_wish" text="Add Wish" OnClick="btn_add_wish_Click" runat="server"/>
            <asp:Button id="btn_show_other_users" text="Show other Users" OnClick="btn_show_other_users_Click" runat="server"/>
            <br />
            <asp:Button id="btn_wishes_granted_by_you" text="Wishes granted by you" OnClick="btn_wishes_granted_by_you_Click" runat="server"/>
            <br />
            <asp:Label id="lbl_warning_delete" runat="server" />
            <br />
            <asp:GridView id="gridview_wishes" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None" 
                OnRowCancelingEdit="gridview_wishes_RowCancelingEdit" 
                OnRowDeleting="gridview_wishes_RowDeleting" 
                OnRowEditing="gridview_wishes_RowEditing" 
                OnRowUpdating="gridview_wishes_RowUpdating">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="FIRSTNAME" HeaderText="Vorname" ReadOnly="True" />
                    <asp:BoundField DataField="WISH" HeaderText="Wish" />
                    <asp:BoundField DataField="COMMENT" HeaderText="Comment" />
                    <asp:BoundField DataField="FULFILLED" HeaderText="Fulfilled" ReadOnly="True" />
                    <asp:CommandField ButtonType="Button" ShowEditButton="True" CausesValidation="false" />
                    <asp:CommandField ShowDeleteButton="True" />
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
            <asp:Button id="btn_change_mail_pw" text="Change Mail or Password" OnClick="btn_change_mail_pw_Click" runat="server"/>
            <asp:Button id="btn_logout" text="Logout" OnClick="btn_logout_Click" runat="server"/>
        </div>

        <div id="modal_addWish" class="modal" runat="server">
            <h1>Add new Wish</h1>
            <label>Wish</label>
            <asp:TextBox id="txt_wish" runat="server"/>
            <asp:Label id="lbl_warning" runat="server"/>
            <br />
            <br />
            <asp:Button id="btn_add_wish_modal" text="Propose Wish" OnClick="btn_add_wish_modal_Click" runat="server"/>
            <asp:Button id="btn_close_modal" text="Discard" OnClick="btn_close_modal_Click" runat="server"/>
        </div>
    </form>
</body>
</html>
