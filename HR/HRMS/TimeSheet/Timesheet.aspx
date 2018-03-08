<%@ Page Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="Timesheet.aspx.cs" Inherits="HR_HRMS_TimeSheet_Timesheet" %>
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
     <a href="Timesheet.aspx" class="SiteMap">Timesheet</a>
    </div>        
   </td>
  </tr>--%>
      
  <%--<tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Time Sheet Details</span></b>
     <br />
     <br />
     <div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Start Date:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpStart" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
        <td class="GridRows">End Date</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpEnd" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>       
        <td class="GridRows">
            <asp:Button ID="btnSearch" runat="server" Text="Search" /><%--<asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" />--%></td>
       </tr>
      </table>
     </div>     
     <br />     
     <table cellpadding="5" border="1" style="width:300px">
      <tr><td colspan="2"><b>Legend:</b></td></tr>
      <tr><td class="GridRows" style="width:50%">Working Day</td><td class="GridRowsRed" style="width:50%">Absent</td></tr> 
      <tr><td class="GridRowsGreen">Rest Day</td><td class="GridRowsYellow">Leave</td></tr>
      <tr><td class="GridRowsViolet">Non-Working Day</td><td>&nbsp;</td></tr>
     </table>          
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridText">&nbsp;</td>
        <td class="GridText" style="width:9%;font-size:x-small;text-align:center;"><b>Date</b></td>
        <td class="GridText" style="width:11%;font-size:x-small;text-align:center;"><b>Time In</b></td>
        <td class="GridText" style="width:11%;font-size:x-small;text-align:center;"><b>Time Out</b></td>
        <td class="GridText" style="width:9%;font-size:x-small;text-align:center;"><b>Shift In</b></td>
        <td class="GridText" style="width:9%;font-size:x-small;text-align:center;"><b>Shift Out</b></td>
        <td class="GridText" style="width:5%;font-size:x-small;text-align:center;"><b>Work</b></td>
        <td class="GridText" style="width:5%;font-size:x-small;text-align:center;"><b>Abs</b></td>
        <td class="GridText" style="width:5%;font-size:x-small;text-align:center;"><b>L w/<br />Pay</b></td>
        <td class="GridText" style="width:5%;font-size:x-small;text-align:center;"><b>L w/o<br />Pay</b></td>
        <td class="GridText" style="width:5%;font-size:x-small;text-align:center;"><b>Late</b></td>
        <td class="GridText" style="width:5%;font-size:x-small;text-align:center;"><b>Under</b></td>
        <td class="GridText" style="width:21%;font-size:x-small;text-align:center;"><b>Remarks</b></td>
       </tr>
       <% LoadTimeSheet(); %>
      </table>
     </div>     
     <br />
     <div style="text-align:center;">
     <%-- <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back"   onclick="btnBack_Click"/>
     </div>      
    </div>     
    </td>
   </tr>
     
 </table>  
</asp:Content>