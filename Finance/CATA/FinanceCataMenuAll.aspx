<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="FinanceCataMenuAll.aspx.cs" Inherits="Finance_FinanceCataMenuAll" Title="The Official STI Head Office Website" %>
<%@ Import Namespace="STIeForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../FinanceMain.aspx" class="SiteMap">Finance</a> » 
     <a href="FinanceCataMenu.aspx" class="SiteMap">Request for CATA</a> »
     <a href="FinanceCataMenuAll.aspx" class="SiteMap">CATA Request List</a>
    </div>        
   </td>
  </tr>      
  <tr><td class="style1"></td></tr>--%>
  <tr>
   <td> 
   <%--For User--%>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id="divUser">    
     <b><span class="HeaderText">My Request for Travel Allowance</span></b>
     <br /><br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:35%;"><b>CATA Details</b></td>
         <td class="GridColumns" style="width:60%;"><b>Status</b></td>
       </tr>
       <% LoadMenuCATA(); %>
       <tr><td class="GridColumns" style="font-size:small;text-align:left;" colspan="3">&nbsp;</td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>


<asp:Content ID="Content2" runat="server" contentplaceholderid="cphHead">
    <style type="text/css">
        .style1
        {
            height: 9px;
        }
    </style>
</asp:Content>



