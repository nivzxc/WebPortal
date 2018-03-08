<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptLeaveSummaryReport.aspx.cs" Inherits="HR_HRMS_Reports_rptLeaveSummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   <%-- <div class="ChildPagePanel">
		<a href="../../../Default.aspx" class="SiteMap">Home</a> » 
		<a href="#" class="SiteMap">CIS</a> » 
		<a href="../../HR.aspx" class="SiteMap">HR</a> » 
		<a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
		<a href="rptLeaveSummaryReport.aspx" class="SiteMap">Leave Summary Report</a>
    </div>--%>
	<div class="ChildPagePanel">
		<h2>Leave Summary Report</h2>
		<br />
		<br />
		<asp:Literal runat="server" ID="litReport"></asp:Literal>
	</div>
</asp:Content>

