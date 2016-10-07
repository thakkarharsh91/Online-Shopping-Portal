<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffHomePage.aspx.cs" Inherits="OnlineShopping.StaffHomePage" %>
<%@ Register TagPrefix="headercontrol" TagName="image" Src="HeaderUserControl.ascx"   %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <headercontrol:image ID="Image1" runat ="server"/>
    <form id="form1" runat="server">
        <p>
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Logout</asp:LinkButton>
            <br />
        </p>
    <div>
    
        <asp:Label ID="printName" runat="server" Visible="False"></asp:Label>
        <br />
        <br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Load Cart</asp:LinkButton>
    </div>
    </form>
</body>

</html>
