<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="TimeCard.aspx.cs" Inherits="HR_HRMS_TimeSheet_TimeCard" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <%--<tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="TimeCard.aspx" class="SiteMap">Time Card</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Daily Time Sheet</span></b>
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
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:20%;"><b>Date</b></td>
        <td class="GridColumns" style="width:25%;"><b>Time In</b></td>
        <td class="GridColumns" style="width:25%;"><b>Time Out</b></td>
        <td class="GridColumns" style="width:25%;"><b>Remarks</b></td>
       </tr>
       <%LoadTimeCard(); %>
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