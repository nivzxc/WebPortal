<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/TwoPanel.master" CodeFile="SuppliesIssuanceDetailsReport.aspx.cs" Inherits="Finance_GP_SuppliesIssuanceDetailsReport" %>
<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../Finance.aspx" class="SiteMap">Finance</a> » 
     <a href="GPMain.aspx" class="SiteMap">Great Plains Online</a> » 
     <a href="SuppliesIssuanceReport.aspx" class="SiteMap">Office Supplies Issuance Report</a> » 
     <a href="SuppliesIssuanceDetailsReport.aspx" class="SiteMap">Office Supplies Details Issuance Report</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Supplies Issuance Report (Total per Department)</span></b>
     <br />
     <br />
     
     <br />
     <div style="text-align:center;">
      <web:chartcontrol id="chaBudCon" runat="server" borderstyle="Outset" borderwidth="1px" height="600px" width="900px" ChartFormat="Jpg" LeftChartPadding="20" YValuesInterval="10000" YCustomEnd="100000" YCustomStart="0" BottomChartPadding="9" Padding="9" GridLines="Both" Border-Color="cornflowerblue">
       <ChartTitle Font="Verdana,10pt,style=Bold" ForeColor="white"></ChartTitle>
       <Background Color="SkyBlue" Type="LinearGradient" EndPoint="900,900"></Background>
       <YAxisFont StringFormat="Far,Near,Character,LineLimit"></YAxisFont>
       <XTitle StringFormat="Center,Near,Character,LineLimit"></XTitle>         
       <XAxisFont StringFormat="Center,Near,Character,LineLimit"></XAxisFont>         
       <YTitle StringFormat="Center,Near,Character,LineLimit"></YTitle>
       <Legend Position="Bottom" Width="120">
        <Background CenterColor="LightYellow" />
       </Legend>
      </web:chartcontrol>            
     </div>
    
     <br />
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="14" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>Supplies Issuance Report Details (<%Response.Write(Request.QueryString["division"]); %> Division)</b></td>
          </tr>
         </table>           
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:23%"><b>Responsibility Center</b></td>
        <td class="GridColumns" style="width:6%"><b>Apr</b></td>
        <td class="GridColumns" style="width:6%"><b>May</b></td>
        <td class="GridColumns" style="width:6%"><b>Jun</b></td>
        <td class="GridColumns" style="width:6%"><b>Jul</b></td>
        <td class="GridColumns" style="width:6%"><b>Aug</b></td>
        <td class="GridColumns" style="width:6%"><b>Sep</b></td>
        <td class="GridColumns" style="width:6%"><b>Oct</b></td>
        <td class="GridColumns" style="width:6%"><b>Nov</b></td>
        <td class="GridColumns" style="width:6%"><b>Dec</b></td>
        <td class="GridColumns" style="width:6%"><b>Jan</b></td>
        <td class="GridColumns" style="width:6%"><b>Feb</b></td>
        <td class="GridColumns" style="width:6%"><b>Mar</b></td>
        <td class="GridColumns" style="width:5%"><b>Total</b></td>
       </tr>
       <% LoadRecords(); %>
      </table>
     </div>     
    </div>
   </td>
  </tr>
 </table>
</asp:Content>