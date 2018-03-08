﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/TwoPanel.master" CodeFile="rptAROBForApprovalSummary.aspx.cs" Inherits="HR_HRMS_Reports_rptAROBForApprovalSummary" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <%--<tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="rptAROBForApprovalSummary.aspx" class="SiteMap">Attendance Report - OB For Approval</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Attendance Report - OB For Approval</span></b>
     <br />
     <br />     
     
     <div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Start Date:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpStart" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
        <td class="GridRows" rowspan="2"><asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" /></td>
       </tr>
       <tr>
        <td class="GridRows">End Date:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpEnd" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
       </tr>       
      </table>
     </div>
          
     <br />     
     <br /> 
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr><td colspan="7" class="GridText" style="text-align:center;"><b>Attendance Report - OB For Approval</b></td></tr>
       <tr>
        <td class="GridText" style="width:2%;">&nbsp;</td>
        <td class="GridText" style="width:15%;font-size:x-small;text-align:center;"><b>Employee Name</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Date Filed</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Date Start</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Date End</b></td>
        <td class="GridText" style="width:18%;font-size:x-small;text-align:center;"><b>Reason</b></td>
        <td class="GridText" style="width:15%;font-size:x-small;text-align:center;"><b>Approver</b></td>
       </tr>
       <% LoadRecords(); %>
      </table>
     </div>      
     <br />
     <div style="width:100%; text-align:center;"><asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" /></div>                                       
    </div>     
   </td>
  </tr>
     
 </table>  
</asp:Content>