<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="TestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 159px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Menu ID="Menu1" runat="server" BackColor="#FFFBD6" 
      DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" 
      ForeColor="#990000" StaticSubMenuIndent="10px">
      <StaticSelectedStyle BackColor="#FFCC66" />
      <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
      <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
      <DynamicMenuStyle BackColor="#FFFBD6" />
      <DynamicSelectedStyle BackColor="#FFCC66" />
      <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
      <StaticHoverStyle BackColor="#990000" ForeColor="White" />
      <Items>
       <asp:MenuItem Text="Sample 1" Value="Sample 1">
        <asp:MenuItem Text="Child 1" Value="Child 1"></asp:MenuItem>
        <asp:MenuItem Text="Child 2" Value="Child 2"></asp:MenuItem>
       </asp:MenuItem>
       <asp:MenuItem Text="Sample 2" Value="Sample 2">
        <asp:MenuItem Text="child 2.1" Value="child 2.1"></asp:MenuItem>
       </asp:MenuItem>
      </Items>
     </asp:Menu>
    </div>
    <table style="width:100%;">
        <tr>
            <td class="style1">
                Email to:</td>
            <td>
                <asp:TextBox ID="txtEmailTo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Message:</td>
            <td>
                <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Send!" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
