<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RFPPrint.aspx.cs" Inherits="Finance_RFPPrint" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <div>
  <table width="100%" cellpadding="0" cellspacing="0"> 
  <tr id="none">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../Default.aspx" class="SiteMap">Finance</a> » 
     <a href="FinanceRequestMenu.aspx" class="SiteMap">Request for Payment</a> » 
     <a href="FinanceNewRequest.aspx" class="SiteMap">Create New Request</a> » 
     <%--<input type="button" value="Print this page" onClick="window.print()">--%>
    </div>        
   </td>
  </tr>
  <tr id ="div3"><td style="height:9px;"></td></tr>
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span id="div1" class="HeaderText">Print Request for Payment Form</span></b>
     <br />
     <br />
     <div class="GridBorder" style="text-align:center;">
       <table width="100%" cellpadding="3" class="Grid" style="text-align:center;" > 
        <tr>
         <td id ="div2" style="text-align:justify;width:10%;"></td>
         <td id="container" style="text-align:justify;width:80%;" >
           <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" ReportSourceID="CrystalReportSource1" 
            Width="100%" DisplayGroupTree="False" HasCrystalLogo="False" 
            HasDrillUpButton="False" HasGotoPageButton="False" HasSearchButton="False" 
            HasToggleGroupTreeButton="False" HasViewList="False" 
            HasExportButton="False" HasZoomFactorList="False" 
            ToolbarStyle-BackColor="White" EnableTheming="True" PrintMode="ActiveX" 
            ToolbarStyle-BorderColor="White" />
           <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="RFA.rpt">
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

