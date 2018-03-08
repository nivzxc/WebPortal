<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="ControlPanel.aspx.cs" Inherits="CMD_ControlPanel" Title="The Official STI Head Office Website" %>

<asp:Content ID="conPao" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="ControlPanel.aspx" class="SiteMap">Control Panel</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
--%>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <table width="100%">
      <tr><td><b><span class="HeaderText">Channel Management: Control Panel</span></b></td></tr>
      <tr><td>&nbsp;</td></tr>
      <tr>
       <td>
        <table style="font-size:small;color:#4169e1;">
         <tr>
          <td colspan="2"><asp:ImageButton runat="server" ID="btnUpdateSD" ImageUrl="~/Support/btnUpdateSchoolDirectory.jpg" OnClick="btnUpdateSD_Click" /></td>
         </tr>         
        </table>
       </td>
      </tr>
     </table>
    </div>
   </td>
  </tr>
 </table>
</asp:Content>