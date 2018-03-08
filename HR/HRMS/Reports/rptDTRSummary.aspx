<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/TwoPanel.master" CodeFile="rptDTRSummary.aspx.cs" Inherits="HR_HRMS_Reports_rptDTRSummary" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="rptDTRSummary.aspx" class="SiteMap">Attendance Report</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Attendance Report</span></b>
     <br />
     <br />     
     
     <div class="GridBorder" style="width:500px; float: left;"> 	          
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
     &nbsp;</div><br />
          
        <div style="float: right; width: 78px;">
            <%--<asp:ImageButton runat="server" ID="btnBack0" ImageUrl="~/Support/btnBack.jpg" 
                onclick="btnBack_Click" />--%><asp:Button ID="btnBack0" runat="server" Text="Back" onclick="btnBack_Click" />
        </div>
          
     <br />

     <div style="text-align:center;">
      <web:chartcontrol id="chaDTRSummary" runat="server" borderstyle="Outset" borderwidth="1px" height="400px" width="1000px" ChartFormat="Jpg" LeftChartPadding="20" YValuesInterval="10" YCustomEnd="120" YCustomStart="0" BottomChartPadding="9" Padding="10" GridLines="Both" Border-Color="cornflowerblue">
       <ChartTitle Font="Verdana,10pt,style=Bold" ForeColor="white" Text="Daily Time Record Summary (Absolute Value)"></ChartTitle>
       <Background Color="SkyBlue" Type="LinearGradient" EndPoint="1400,1400"></Background>
       <YAxisFont StringFormat="Far,Near,Character,LineLimit"></YAxisFont>
       <XTitle StringFormat="Center,Near,Character,LineLimit"></XTitle>         
       <XAxisFont StringFormat="Center,Near,Character,LineLimit"></XAxisFont>         
       <YTitle StringFormat="Center,Near,Character,LineLimit"></YTitle>
       <Legend Position="Bottom" Width="70">
        <Background CenterColor="LightYellow" />
       </Legend>
      </web:chartcontrol>
     </div>
     
     <div style="text-align:center;">
      <web:chartcontrol id="chaDTRSummaryPercentage" runat="server" borderstyle="Outset" borderwidth="1px" height="400px" width="1000px" ChartFormat="Jpg" LeftChartPadding="20" YValuesInterval="10" YCustomEnd="100" YCustomStart="0" BottomChartPadding="9" Padding="10" GridLines="Both" Border-Color="cornflowerblue" Visible="false">
       <ChartTitle Font="Verdana,10pt,style=Bold" ForeColor="white" Text="Daily Time Record Summary (Percentage)"></ChartTitle>
       <Background Color="SkyBlue" Type="LinearGradient" EndPoint="1400,1400"></Background>
       <YAxisFont StringFormat="Far,Near,Character,LineLimit"></YAxisFont>
       <XTitle StringFormat="Center,Near,Character,LineLimit"></XTitle>         
       <XAxisFont StringFormat="Center,Near,Character,LineLimit"></XAxisFont>         
       <YTitle StringFormat="Center,Near,Character,LineLimit"></YTitle>
       <Legend Position="Bottom" Width="70">
        <Background CenterColor="LightYellow" />
       </Legend>
      </web:chartcontrol>
     </div>
     
     <br /> 
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr><td colspan="15" class="GridColumns" style="text-align:center;"><b>Attendance Report</b></td></tr>
       <tr>
        <td class="GridColumns" style="width:2%;" rowspan="2">&nbsp;</td>
        <td class="GridColumns" style="width:15%;font-size:x-small;text-align:center;" rowspan="2"><b>Employee Name</b></td>
        <td class="GridColumns" style="width:23%;font-size:x-small;text-align:center;" rowspan="2"><b>Department</b></td>
        <td class="GridColumns" style="width:11%;font-size:x-small;text-align:center;" colspan="2"><b>Tardiness</b></td>
        <td class="GridColumns" style="width:9%;font-size:x-small;text-align:center;" colspan="2"><b>Undertime</b></td>
        <td class="GridColumns" style="width:9%;font-size:x-small;text-align:center;" colspan="2"><b>Absent</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;" colspan="2"><b>Leave w/ Pay</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;" colspan="2"><b>Leave w/o Pay</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;" colspan="2"><b>Total Work</b></td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Count</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Min</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Count</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Min</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Count</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Day</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Count</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Day</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Count</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Day</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Day</b></td>
        <td class="GridColumns" style="width:5%;font-size:x-small;text-align:center;"><b>Hr</b></td>                                                
       </tr>
       <% LoadRecords(); %>
      </table>
     </div>     
     <br />
     <div style="width:100%; text-align:center;">
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" /><%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%></div>      
    </div>     
    </td>
   </tr>
     
 </table>  
</asp:Content>