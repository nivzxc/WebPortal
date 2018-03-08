<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="EForms.aspx.cs" Inherits="EForms" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true"> 
 <table width="100%" cellpadding="0" cellspacing="0">
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="Default.aspx" class="SiteMap">Home</a> » 
     <a href="eForms.aspx" class="SiteMap">EForms</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>
     
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">E-Forms List</span></b>
     <br />
     <br />     
     <table>
      <tr>
       <td>
        <table>
         <tr>
          <td><img src="Support/EForms.png" alt="" /></td>
          <td>&nbsp;<a href="CIS/MRCF/MRCFMenu.aspx" style="font-size:small;">Material Request and Canvass Form</a></td>
         </tr>
        </table>
       </td>
      </tr>
      <tr>
       <td>
        <table>
         <tr>
          <td><img src="Support/EForms.png" alt="" /></td>
          <td>&nbsp;<a href="CIS/Requisition/RequMenu.aspx" style="font-size:small;">Office Supplies Requisition</a></td>
         </tr>
        </table>
       </td>
      </tr>
      <tr>
       <td>
        <table>
         <tr>
          <td><img src="Support/EForms.png" alt="" /></td>
          <td>&nbsp;<a href="CIS/Transmittal/TranMenu.aspx" style="font-size:small;">Transmittal</a></td>
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