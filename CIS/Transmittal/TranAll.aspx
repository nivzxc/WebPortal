<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="TranAll.aspx.cs" Inherits="CIS_Transmittal_TranAll" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="TranMenu.aspx" class="SiteMap">Transmittal</a> » 
     <a href="TranAll.aspx?page=1" class="SiteMap">Sent Transmittal Request</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Sent Transmittal Request</span></b>
     <br />
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" class="grid">
    <%--   <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>All Transmittal Request Made</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Transmittal Request Details</b></td>
        <td class="GridColumns" style="width:200px;"><b>Status</b></td>
       </tr>
       <% LoadRecords();%>
       <tr><td class="BrowseAll" style="font-size:small;text-align:left;" colspan="3">&nbsp;<b>Page</b><% Load_Paging();%></td></tr>
      </table>           
     </div>
    </div>     
   </td>
  </tr> 

 </table>
</asp:Content>