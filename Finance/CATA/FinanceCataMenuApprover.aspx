<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="FinanceCataMenuApprover.aspx.cs" Inherits="Finance_CATA_FinanceCataMenuApprover" %>

<%@ Import Namespace="STIeForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../FinanceMain.aspx" class="SiteMap">Finance</a> » 
     <a href="FinanceCataMenu.aspx" class="SiteMap">Request for CATA</a> »
     <a href="FinanceCataMenuApprover.aspx" class="SiteMap">CATA Request Approver List</a>
    </div>        
   </td>
  </tr>      
  <tr><td class="style1"></td></tr>--%>
  <tr>
   <td> 
    <%--For Approver--%>
    <%
     if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"].ToString()))
      {
    %>
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="div1">    
      <b><span class="HeaderText">Request for Travel Allowance (For Approval)</span></b>
      <br />
      <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="5" class="Grid">
        <%--<tr>
         <td colspan="4" align="center" class="GridText">
          <table>
           <tr>
            <td>&nbsp;<b>List of Request for CATA</b></td>
           </tr>
          </table>           
         </td>
        </tr>--%>
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:95%;"><b>Request for CATA Details</b></td>
        </tr>
        <% LoadMenuCATAApprover(); %>
       </table>
      </div>         
     </div><br />
     <%
    }
    %>
  
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id="divUser">    
     <b><span class="HeaderText">Previous Approved Travel Allowance Request</span></b>
        <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
      
       <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:95%;"><b>Request for CATA Details</b></td>
        </tr>
       <% LoadMenuCATAApprovedRequest(); %>
      </table>
     </div>         
    </div>
    <br /><br />
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id="div2">    
     <b><span class="HeaderText">Previous Disapproved Travel Allowance Request</span></b>
        <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
      
       <tr>
         <td class="GridColumns" style="width:5%; height: 9px;"></td>
         <td class="GridColumns" style="width:95%; height: 9px;"><b>Request for CATA Details</b></td>
        </tr>
       <% LoadMenuCATADisapprovedRequest(); %>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>


<asp:Content ID="Content2" runat="server" contentplaceholderid="cphHead">
    </asp:Content>
