<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CWIMain.aspx.cs" Inherits="Academics_CWIMain" %>
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="Academics.aspx" class="SiteMap">Academics</a> » 
     <a href="Courseware.aspx" class="SiteMap">Courseware</a> » 
     <a href="CWIMain.aspx?page=1" class="SiteMap">Courseware Inventory</a>
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
     <b><span class="HeaderText">&nbsp;Courseware Inventory</span></b>
     <br />
     <br />
      
     <div class="GridBorder" style="width:70%">
      <table width="100%" cellpadding="5" class="Grid">       
       <tr>
        <td class="GridText" colspan="2">
         <table cellpadding="0" cellspacing="0">
          <tr>
           <td><img src="../Support/Search22.png" alt="Search Record" /></td>
           <td>&nbsp;<b>Search</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridRows">Course Code:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCourseCode" CssClass="controls" Font-Size="Small" BackColor="white"></asp:TextBox></td>
       </tr>          
       <tr>
        <td class="GridRows">Course Title:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCourseTitle" CssClass="controls" Font-Size="Small" BackColor="white" Width="300px"></asp:TextBox></td>
       </tr>         
       <tr><td colspan="2" class="GridRows" style="text-align:center;"><asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" OnClick="btnSearch_Click" /></td></tr>
      </table>
     </div>
     
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td class="GridColumns" style="width:65%"><b>Subject Description</b></td>
        <td class="GridColumns" style="width:20%"><b>Availability</b></td>
        <td class="GridColumns" style="width:15%"><b>Completed</b></td>
       </tr>
       <%Load_Records(); %>
      </table>
     </div>
     
     <br />
     
     <%Load_Paging(); %>
      
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
       <td valign="middle"><img src="../Support/alert64.png" alt="" /></td>
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
  
 </table>
</asp:Content>