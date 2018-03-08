<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="Courseware.aspx.cs" Inherits="Academics_Courseware" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="Academics.aspx" class="SiteMap">Academics</a> » 
     <a href="Courseware.aspx" class="SiteMap">Courseware</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>
 
  <tr>  
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <table>
      <tr>
       <td><img src="../Support/notes48.png" alt="Courseware Inventory"/></td>
       <td style="font-size:small;"><a href="CWIMain.aspx?page=1">View Courseware Inventory</a></td>
      </tr>        
     </table>
    </div>
   </td>
  </tr>
  
 </table>
</asp:Content>