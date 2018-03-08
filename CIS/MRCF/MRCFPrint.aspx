<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/NoRightPanel.master" AutoEventWireup="true" CodeFile="MRCFPrint.aspx.cs" Inherits="CIS_MRCF_MRCFPrint" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:20px; ">
<center>
    <CR:CrystalReportSource ID="crsMRCF" runat="server">
    <Report FileName="../../CIS/MRCF/MRCFPrint.rpt">
    </Report>
    </CR:CrystalReportSource>
    <CR:CrystalReportViewer ID="crvMRCF"  runat="server" EnableDrillDown="False" 
           AutoDataBind="True" DisplayGroupTree="False" HasCrystalLogo="False" 
           HasDrillUpButton="False" HasGotoPageButton="True" HasSearchButton="False" 
           HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False" 
           ReportSourceID="crsMRCF" ToolbarStyle-BackColor="White" Toolbarstyle-width = "775px"
           Width="775px" PrintMode="Pdf" BorderStyle="None" Height="1039px" 
  HasPageNavigationButtons="False" />
</center>
</div>
</asp:Content>

