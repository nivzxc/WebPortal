<%@ Page Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptHeadCountReport.aspx.cs" Inherits="HR_HRMS_Reports_rptHeadCountReport" %>
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
     <a href="rptHeadCountReport.aspx" class="SiteMap">Head Count Report</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">HRMS - Head Count Report</span></b>
     <br />
     <br />     
     
     <div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">As of:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpFocusDate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
        <td class="GridRows"><asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" /></td>
       </tr>
      </table>
     </div>
          
     <br />    
     <br />
      
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr><td colspan="10" class="GridText" style="text-align:center;"><b>Head Count Report</b></td></tr>
       <tr>
        <td class="GridText" style="width:10%; font-size:x-small; text-align:center;" rowspan="2"><b>Job Grade</b></td>
        <td class="GridText" style="font-size:x-small;text-align:center;" colspan="3"><b>All Employees</b> (HQ + Billable)</td>
        <td class="GridText" style="font-size:x-small;text-align:center;" colspan="3"><b>HQ</b></td>
        <td class="GridText" style="font-size:x-small;text-align:center;" colspan="3"><b>Billable</b></td>
       </tr>
       <tr>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Plantilla Position</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Actual Filled</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Actual Unfilled</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Plantilla Position</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Actual Filled</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Actual Unfilled</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Plantilla Position</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Actual Filled</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Actual Unfilled</b></td>        
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