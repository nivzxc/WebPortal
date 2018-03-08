<%@ Page Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptLeaveApplications.aspx.cs" Inherits="HR_HRMS_Reports_rptLeaveApplications" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="rptLeaveApplications.aspx" class="SiteMap">Attendance Report - Leave Applications</a>
    </div>        
   </td>
  </tr>--%>
<%--      
  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Attendance Report - Leave Applications</span></b>
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
      
     <div class="GridBorder" style="width:1500px;">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr><td colspan="13" class="GridText" style="text-align:center;"><b>Attendance Report - Leave Applications</b></td></tr>
       <tr>
        <td class="GridColumns" style="width:2%;">&nbsp;</td>
        <td class="GridColumns" style="width:5%;"><b>Status</b></td>
        <td class="GridColumns" style="width:6%;"><b>Date Filed</b></td>
        <td class="GridColumns" style="width:10%;"><b>Employee</b></td>
        <td class="GridColumns" style="width:8%;"><b>Leave Type</b></td>
        <td class="GridColumns" style="width:6%;"><b>Date Start</b></td>
        <td class="GridColumns" style="width:6%;"><b>Date End</b></td>
        <td class="GridColumns" style="width:3%;"><b>Duration</b></td>
        <td class="GridColumns" style="width:10%;"><b>Approver</b></td>
        <td class="GridColumns" style="width:6%;"><b>Date</b></td>
        <td class="GridColumns" style="width:10%;"><b>Department</b></td>
        <td class="GridColumns" style="width:15%;"><b>Reason</b></td>
        <td class="GridColumns" style="width:13%;"><b>Remarks</b></td>        
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