<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="RequAll.aspx.cs" Inherits="CIS_Requisition_RequAll" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">Corporate Services</a> » 
     <a href="RequMenu.aspx" class="SiteMap">Requisition</a> » 
     <a href="RequAll.aspx?page=1" class="SiteMap">Sent Requisitions</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
   --%>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Sent Requisitions</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="All Requisition Request Made" /></td>
           <td>&nbsp;<b>List of Sent Requisitions</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:200px;"><b>Status</b></td>
       </tr>
       <% LoadRequisition(); %>
       <tr><td class="BrowseAll" style="font-size:small;text-align:left;" colspan="3">Page<% LoadPaging();%></td></tr>
      </table>
     </div>
    </div>     
   </td>
  </tr>
 
 </table>
</asp:Content> 