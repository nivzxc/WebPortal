<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/TwoPanel.master" CodeFile="OvertimeComputed.aspx.cs" Inherits="HR_HRMS_TimeSheet_OvertimeComputed" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OvertimeComputed.aspx" class="SiteMap">Overtime Computed</a>
    </div>        
   </td>
  </tr>--%>
      
 <%-- <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Computed Overtime Details</span></b>
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
     <table>
      <tr>      
       <td>
        <table cellpadding="5" border="1" style="width:300px">
         <tr><td colspan="2"><b>Legend (OT Terms)</b></td></tr>
         <tr><td>OT</td><td>Overtime</td></tr>
         <tr><td>EXC</td><td>Excess</td></tr>
         <tr><td>ND</td><td>Night Differential</td></tr>
         <tr><td>RE</td><td>Regular OT</td></tr>
         <tr><td>RS</td><td>Rest Day OT</td></tr>
         <tr><td>SP</td><td>Special Holiday OT</td></tr>
         <tr><td>LG</td><td>Legal Holiday OT</td></tr>
         <tr><td>SPRS</td><td>Special Holiday Rest Day OT</td></tr>
         <tr><td>LGRS</td><td>Legal Holiday Rest Day OT</td></tr>         
        </table>       
       </td>              
       <td>&nbsp;</td>
       <td valign="top">
        <table cellpadding="5" border="1" style="width:300px">
         <tr><td colspan="2"><b>Legend:</b></td></tr>
         <tr><td class="GridRows" style="width:50%">Working Day</td><td class="GridRowsRed" style="width:50%">Absent</td></tr>
         <tr><td class="GridRowsGreen">Rest Day</td><td class="GridRowsYellow">Leave</td></tr>
         <tr><td class="GridRowsViolet">Non-Working Day</td><td>&nbsp;</td></tr>
        </table>
       </td>       
      </tr>      
     </table>
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridText">&nbsp;</td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Date</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Time In</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Time Out</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Shift In</b></td>
        <td class="GridText" style="width:7%;font-size:x-small;text-align:center;"><b>Shift Out</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>RE<br />OT</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>RE<br />ND</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>RS<br />OT</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>RS<br />EXC</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>RS<br />ND</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>SP<br />OT</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>SP<br />EXC</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>SP<br />ND</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>LG<br />OT</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>LG<br />EXC</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>LG<br />ND</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>SPRS<br />OT</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>SPRS<br />EXC</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>SPRS<br />ND</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>LGRS<br />OT</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>LGRS<br />EXC</b></td>
        <td class="GridText" style="width:3%;font-size:x-small;text-align:center;"><b>LGRS<br />ND</b></td>
        <td class="GridText" style="width:14%;font-size:x-small;text-align:center;"><b>Applications</b></td>
       </tr>
       <% LoadOvertime(); %>
      </table>
     </div>     
     <br />
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click" />
     </div>      
    </div>     
    </td>
   </tr>
     
 </table>  
</asp:Content>