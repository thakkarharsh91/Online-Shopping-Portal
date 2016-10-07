<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="OnlineShopping.Home" %>
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
    
        This application is basically an Online shopping system where the users can signup and login into the website and can get the prodcuts. There are various features provided as track delivery, add products to cart. Apart from the normal users accessing the website as the end user, there are also other users like admin, staff which maintains the website.
        <br />
        <br />
        The normal users can click on the member login button and can register themself to the website if they are first time users or else can directly login to the website if they have already an account associated with the website.<br />
        <br />
        The Staff login functionality is for internal users of the website. Two types of users are eligible here :<br />
        <br />
        1. Administrator - The Administrator of the website can click on the staff login button, validate himself and then login to access the various functionalities of the application. The admin functionalities include addition of staff members to the application and search for any particular member who has an account with the application.<br />
        <br />
        2. Staff - The staff member of the website can be added by the administrator and the same can be used to load the cart.<br />
        <br />
        <br />
    
    </div>
        <asp:Button ID="member" runat="server" OnClick="member_Click" Text="Member login" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="staff" runat="server" OnClick="staff_Click" Text="Staff login" />
&nbsp;<div style="margin-left: 120px">
        </div>
    </form>
</body>
</html>
