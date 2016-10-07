<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadCart.aspx.cs" Inherits="OnlineShopping.LoadCart" %>
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
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
        </p>
    <div>
        You are logged in as:
        <asp:Label ID="printName" runat="server" Visible="False"></asp:Label>
        <p>Function: Given a csv containing information about products in a specified format, this service will Load the cart for the online shopping website</p>
        <ol>
            <li>
                This service allows you to view the statistics for all the products and for all the bifercations.
            </li>
            <li>
                This service allows you to view all items as per catagiories such as item, size, color etc.
            </li>
            <li>
                If we need to add new criterials for bifercation, it can be easily added in the code. Each column in the csv could be one unique bifercation criteria.
            </li>
            <li>
                Each row in the csv maps to one unique product in the store.
            </li>
            <li>
                URL of the service:
            </li>
            <li>
                Signature of methods:
                <ol>
                     <li>
                         List< string> loadCart()
                     </li>
                    <li>
                        string[] organizedItems()
                    </li>
                    <li>
                        string[] organizedItemsBySize();
                    </li>
                    <li>
                        string[] organizedItemsByColor();
                    </li>
                </ol>
            </li>
        </ol>
        <asp:Label ID="testLabel" runat="server" Text="Label"></asp:Label>
        <br />

    <p>This is the cart for our shopping website</p>
        <table>
            <tr>
                <asp:Button id="btn_getAllProducts" runat="server" OnClick="displayCart" Text="Display All Products"/>
            </tr>
            <tr>
                <asp:Button id="btn_getProductsStats" runat="server" OnClick="displayStats" Text="Display Statistics"/>
            </tr>
            <tr>
                <asp:Button id="btn_getProductsBySize" runat="server" OnClick="displayItemsBySize" Text="Display Products by Size"/>
            </tr>
            <tr>
                <asp:Button id="btn_getProductsByColor" runat="server" OnClick="displayItemsByColors" Text="Display Products by Colors"/>
            </tr>
        </table>
        
        <textarea id="txtArea_productList" runat="server" rows="30" cols="150" visible="false" readonly="readonly"></textarea>   <br />
        

        <asp:Button id="btn_displayEachSizedItems" runat="server" OnClick="displayEachSizedItems" Visible ="false" Text="Display Each sized Items"/>
        <asp:Button id="btn_displayEachColoredItems" runat="server" OnClick="displayEachColoredItems" Visible ="false" Text="Display Each Colored Items"/>         
        <br />
        <textarea id="txtArea_1" runat="server" rows="15" cols="25" visible="false"></textarea>
        <textarea id="txtArea_2" runat="server" rows="15" cols="25" visible="false"></textarea>
        <textarea id="txtArea_3" runat="server" rows="15" cols="25" visible="false"></textarea>
        <textarea id="txtArea_4" runat="server" rows="15" cols="25" visible="false"></textarea>
        <textarea id="txtArea_5" runat="server" rows="15" cols="25" visible="false"></textarea>
        <textarea id="txtArea_6" runat="server" rows="15" cols="25" visible="false"></textarea>

        <dialog id="ABC" ></dialog>
    </div>
    </form>
</body>
</html>
