<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="GPMain.aspx.cs" Inherits="Finance_GP_GPMain" %>
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../Finance.aspx" class="SiteMap">Finance</a> »      
     <a href="GPMain.aspx" class="SiteMap">Great Plains Online</a>
    </div>        
   </td>
  </tr>
  
  <% 
   if (clsSpeedo.IsModuleAllowedAccess(clsSpeedo.SpeedoModules.GreatPlains, Request.Cookies["Speedo"]["UserName"]))
   {
  %>   
  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Great Plains Online</span></b>
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>List of Reports</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridRows">
         <table>
          <tr>
           <td><img src="../../Support/EForms.png" alt="" /></td>
           <td>&nbsp;<a href="SuppliesIssuanceReport.aspx" style="font-size:small;">Supplies Issuance Report</a></td>
          </tr>
         </table>         
        </td>
       </tr>
      </table>
     </div>
    </div>     
   </td>
  </tr>
  <%
  }
  else
  {  
  %>
  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <table>
      <tr>
       <td valign="middle"><img src="../../Support/alert64.png" alt="" /></td>
       <td valign="middle" style="font-size:small;color:#4169e1;">
        Sorry. You are not allowed to access this module.     
        <br />
        Please contact your system administrator to grant you an access rights.            
       </td>
      </tr>
     </table>
    </div>     
   </td>
  </tr>  
  <%
   }
  %>  

     
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <table>
      <tr><td><b><span class="HeaderText">Great Plains Online Main Page</span></b></td></tr>
      <tr><td>&nbsp;</td></tr>
      <tr>
       <td>
        <table>
         <tr>
          <td><img src="../../Support/notes48.png" alt="" /></td>
          <td valign="middle"><a href="" style="font-size:small;" target="_blank">View Policy</a></td>
         </tr>
        </table>
       </td>
      </tr>
      <tr>
       <td>
        <table>
         <tr>
          <td><img src="../../Support/user2.png" alt="" /></td>
          <td valign="middle" style="font-size:small;color:#4169e1;">
           For comments and concerns,<br />
           You may contact our Great Plains Administrator:<br />
           Rommel Ong<br />
           Email: rong@stihq.net<br />
           Contact no.: 8878447 loc. 6911<br />
          </td>
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