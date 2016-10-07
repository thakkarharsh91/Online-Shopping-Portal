<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffLogIn.aspx.cs" Inherits="OnlineShopping.StaffLogIn" %>
<%@ Register TagPrefix="headercontrol" TagName="image" Src="HeaderUserControl.ascx"   %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <headercontrol:image ID="Image1" runat ="server"/>
    <form id="form1" runat="server">
    <div>
    
        <br />
    
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Home</asp:LinkButton>
        <br />
        <br />
    
        Username : adminuser<br />
        Password : Universe123$<br />
        <br />
        <asp:Label ID="error" runat="server" Text="Label" Visible="False"></asp:Label>
    
    </div>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Enter username"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Enter password"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input id="password" type="password" runat="server"/><br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="loginadmin" runat="server" OnClick="loginadmin_Click" style="height: 26px" Text="Login" />
    </form>
</body>
</html>
