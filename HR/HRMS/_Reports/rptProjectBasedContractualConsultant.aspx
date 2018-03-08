<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/TwoPanel.master" CodeFile="rptProjectBasedContractualConsultant.aspx.cs" Inherits="HR_HRMS_Reports_rptProjectBasedContractualConsultant" %>
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
     <a href="rptProjectBasedContractualConsultant.aspx" class="SiteMap">Manpower Reports - Project Based/Contractual/Consultant</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Manpower Report - Project Based/Contractual/Consultant</span></b>
     <br />
     <br />     
     
     <div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">As Of:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpAsOf" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
        <td class="GridRows"><asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" /></td>
       </tr>
      </table>
     </div>
          
     <br />    
     <br />
      
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr><td colspan="17" class="GridText" style="text-align:center;"><b>Project Based/Contractual/Consultant Report</b></td></tr>
       <tr>
        <td class="GridText" style="width:2%;">&nbsp;</td>
        <td class="GridText" style="width:4%;font-size:x-small;text-align:center;"><b>RC</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Name</b></td>
        <td class="GridText" style="width:18%;font-size:x-small;text-align:center;"><b>Position</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Start</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>End</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>T</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Birth Date</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>A</b></td>
        <td class="GridText" style="width:2%;font-size:x-small;text-align:center;"><b>G</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Status</b></td>
        <td class="GridText" style="width:5%;font-size:x-small;text-align:center;"><b>Division</b></td>
        <td class="GridText" style="width:15%;font-size:x-small;text-align:center;"><b>Department</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Remarks</b></td>
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