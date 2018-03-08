<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="UsersIndex.aspx.cs" Inherits="UsersIndex" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true"> 
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="Default.aspx" class="SiteMap">Home</a> » 
     <a href="UsersIndex.aspx" class="SiteMap">Users Index</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>
     
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Users Index</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Users Index</b></td>
          </tr>
         </table>           
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:15%">&nbsp;</td>
        <td class="GridColumns" style="width:50%"><b>Username</b></td>
        <td class="GridColumns" style="width:35%"><b>Last Online</b></td>
       </tr>
       <% LoadUsers(); %>
      </table>
     </div>         
    </div>      
   </td>
  </tr>     
  
 </table> 
</asp:Content>
