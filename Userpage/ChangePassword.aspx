<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="ChangePassword.aspx.cs" Inherits="Userpage_ChangePassword" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true"> 
 <table width="100%" cellpadding="0" cellspacing="0">
<%--   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="ControlPanel.aspx" class="SiteMap">Control Panel</a> » 
     <a href="ChangePassword.aspx" class="SiteMap">Change Password</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>
  --%>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Change Password</span></b>
     <br />
     <br />
     <br />
     <span class="HeaderText">Current Password:</span>
     <br />
     <span style="color:#4682b4;">Enter your current password here:</span>         
     <br />
     <asp:TextBox runat="server" ID="txtCurrent" CssClass="controls" Font-Size="Small" Width="200px" TextMode="Password"></asp:TextBox>
     <br />
     <asp:Label runat="server" ID="lblErr" Font-Size="Small" ForeColor="red" Visible="false"></asp:Label>
     <br />
     <br />
     <span class="HeaderText">New Password:</span>
     <br />
     <asp:TextBox runat="server" ID="txtPass1" CssClass="controls" Font-Size="Small" Width="200px" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtPass1" ErrorMessage="New password is required." 
            ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
     <br />
     <br />
     <span class="HeaderText">Confirm New Password:</span>
     <br />
     <asp:TextBox runat="server" ID="txtPass2" CssClass="controls" Font-Size="Small" Width="200px" TextMode="Password"></asp:TextBox>     
     <br />
     <asp:CompareValidator runat="server" ID="cmpPassword" ErrorMessage="Error: Password mismatch" Display="Dynamic" Font-Size="Small" ControlToCompare="txtPass1" ControlToValidate="txtPass2"></asp:CompareValidator>
     <br />
     <br />
     <br />
     <br />     
     <asp:ImageButton runat="server" ID="btnSave" 
            ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSave_Click" 
            ValidationGroup="save" />
    </div>   
   </td>
  </tr>
 </table>
</asp:Content>
