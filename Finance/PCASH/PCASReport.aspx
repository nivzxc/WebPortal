<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/NoRightPanel.master" AutoEventWireup="true" CodeFile="PCASReport.aspx.cs" Inherits="Finance_PCASH_PCASReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <div>
  <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr id="none">
   <td>
    <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <a href="../../Default.aspx" class="SiteMap">Home</a> » <a href="../FinanceMain.aspx" class="SiteMap">
       Finance</a> » <a href="FinanceCataMenu.aspx" class="SiteMap">Request for CATA</a>
      » <a href="FinanceNewCataRequest.aspx" class="SiteMap">Create New Request</a>
      </div>       
   </td>
  </tr>
  <tr id ="div3"><td style="height:9px;"></td></tr>--%>
  <tr>
   <td>   
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     
     <b><span id="div1" class="HeaderText">Print Request for Petty Cash</span></b>
     <br />
     <br />
     <%--<asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" />--%>
        <asp:Button ID="btnBack" runat="server" Text="Back"  OnClick="btnBack_Click" />
     <br />
     <br />
     <div>
      <b><a href="CATAHowToPrintReport.aspx" target="_blank" shape="rect" 
       style="font-size: medium; color: #FF0000; text-decoration: underline;">Please read this link for the printing procedure</a></b>
     </div>
     <br />
     <div class="GridBorder" style="text-align:center;">
       <table width="100%" cellpadding="3" class="Grid" style="text-align:center;" > 
        <tr>
         <td id ="div2" style="text-align:justify;width:10%;"></td>
         <td id="container" style="text-align:justify;width:80%;" >
           <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" EnableDrillDown="False" 
           AutoDataBind="True" DisplayGroupTree="False" HasCrystalLogo="False" 
           HasDrillUpButton="False" HasGotoPageButton="False" HasSearchButton="False" 
           HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False" 
           ReportSourceID="CrystalReportSource1" ToolbarStyle-BackColor="White" Toolbarstyle-width = "775px"
           Width="775px" PrintMode="Pdf" BorderStyle="None" Height="1039px" 
  HasPageNavigationButtons="False" />
       <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="../../Support/Report/PCASNew.rpt">
        </Report>
       </CR:CrystalReportSource>
         </td>
         <td id ="div5" style="text-align:justify;width:10%;"></td>
        </tr>
       </table>
     </div>
    </div>
   </td>
  </tr> 

 </table>
 </div>
</asp:Content>

