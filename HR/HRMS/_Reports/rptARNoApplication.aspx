<%@ Page Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptARNoApplication.aspx.cs" Inherits="HR_HRMS_Reports_rptARNoApplication" %>
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
     <a href="rptARNoApplication.aspx" class="SiteMap">Attendance Report - Absence without Official Leave</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Attendance Report - Absence without Official Leave</span></b>
     <br />
     <br />     
     
     <div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Start Date:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpStart" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
        <td class="GridRows" rowspan="2"><asp:ImageButton runat="server" ID="btnSearch" 
          ImageUrl="~/Support/btnSearch.jpg"/></td>
       </tr>
       <tr>
        <td class="GridRows">End Date:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpEnd" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
       </tr>       
      </table>
     </div>
          
     <br />    
     <br />
      
     <div class="GridBorder" style="width:800px;">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr><td colspan="5" class="GridText" style="text-align:center;"><b>Attendance Report - No Application</b></td></tr>
       <tr>
        <td class="GridText" style="width:4%;" rowspan="2">&nbsp;</td>
        <td class="GridText" style="width:35%;font-size:x-small;text-align:center;" rowspan="2"><b>Employee Name</b></td>
        <td class="GridText" style="width:35%;font-size:x-small;text-align:center;" rowspan="2"><b>Division</b></td>
        <td class="GridText" style="font-size:x-small;text-align:center;" colspan="2"><b>Absent</b></td>
       </tr>
       <tr>
        <td class="GridText" style="width:13%;font-size:x-small;text-align:center;"><b>Count</b></td>
        <td class="GridText" style="width:13%;font-size:x-small;text-align:center;"><b>Days</b></td>                                              
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