<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptResignedEmployee.aspx.cs" MasterPageFile="~/App_Master/TwoPanel.master" Inherits="HR_HRMS_Reports_rptResignedEmployee" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="rptResignedEmployee.aspx" class="SiteMap">HRMS - Resigned Employees Report</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Resigned Employees Report</span></b>
     <br />
     <br />     
     
     <div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Start Date:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpStart" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
        <td class="GridRows" rowspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Search" /><%--<asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" />--%></td>
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
       <tr><td colspan="13" class="GridColumns" style="text-align:center;"><b>Resigned Employees</b></td></tr>
       <tr>
        <td class="GridColumns" style="width:3%;">&nbsp;</td>
        <td class="GridColumns" style="width:15%;font-size:x-small; text-align:center;"><b>Name</b></td>
        <td class="GridColumns" style="width:15%;font-size:x-small; text-align:center;"><b>Position</b></td>
        <td class="GridColumns" style="width:6%;font-size:x-small; text-align:center;"><b>Birth Date</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small; text-align:center;"><b>Age</b></td>
        <td class="GridColumns" style="width:6%;font-size:x-small; text-align:center;"><b>Date Hired</b></td>
        <td class="GridColumns" style="width:6%;font-size:x-small; text-align:center;"><b>End Date</b></td>
        <td class="GridColumns" style="width:8%;font-size:x-small; text-align:center;"><b>Status</b></td>
        <td class="GridColumns" style="width:4%;font-size:x-small; text-align:center;"><b>Tenure</b></td>
        <td class="GridColumns" style="width:10%;font-size:x-small; text-align:center;"><b>Reason for Leaving</b></td>
        <td class="GridColumns" style="width:11%;font-size:x-small; text-align:center;"><b>Remarks</b></td>
        <td class="GridColumns" style="width:6%;font-size:x-small; text-align:center;"><b>Category</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small; text-align:center;"><b>Billable</b></td>
       </tr>
       <% LoadRecords(); %>
      </table>
     </div>      
     <br />
     <div style="width:100%; text-align:center;">
         <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click"/>
         <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%></div>                                            
    </div>     
   </td>
  </tr>
     
 </table>  
</asp:Content>