<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Threads_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="ChildPagePanel">
        <a href="../Default.aspx" class="SiteMap">Home</a> » 
        <a href="Forums.aspx" class="SiteMap">Forums</a> » 
		<a href="Reports.aspx" class="SiteMap">Reports</a>
    </div>
    <div class="ChildPagePanel">
        <h2>Reports</h2>
		<br />
        <br />
		<div class="ChildPagePanelBlue" style="font-size: small;">
			<a href="ReportEmployeePost.aspx">Thread Post Report</a>
		</div>
		<div class="ChildPagePanelBlue" style="font-size: small;">
			<a href="ReportThreadView.aspx">Thread Views Report</a>
		</div>
    </div>
</asp:Content>

