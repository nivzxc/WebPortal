<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptAbsenceWithoutLeaveReport.aspx.cs" Inherits="HR_HRMS_Reports_rptAbsenceWithoutLeaveReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <%--    <div class="ChildPagePanel">
		<a href="../../../Default.aspx" class="SiteMap">Home</a> » 
		<a href="#" class="SiteMap">CIS</a> » 
		<a href="../../HR.aspx" class="SiteMap">HR</a> » 
		<a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
		<a href="rptAbsenceWithoutLeaveReport.aspx" class="SiteMap">Absense without Official Leave Report</a>
    </div>--%>
	<div class="ChildPagePanel">
		<h2>Absence without Official Leave</h2>
          
        <div style="float: right; width: 78px;">
            <%--<asp:ImageButton runat="server" ID="btnBack0" ImageUrl="~/Support/btnBack.jpg" 
                onclick="btnBack_Click" />--%>
            <asp:Button ID="btnBack0" runat="server" Text="Back" onclick="btnBack_Click" />
        </div>
         Fiscal Year: &nbsp;<asp:DropDownList ID="ddlFiscalYear" runat="server" 
            AutoPostBack="True">
            <asp:ListItem>2009-2010</asp:ListItem>
            <asp:ListItem>2011-2012</asp:ListItem>
            <asp:ListItem>2012-2013</asp:ListItem>
            <asp:ListItem>2013-2014</asp:ListItem>
            <asp:ListItem>2014-2015</asp:ListItem>
            <asp:ListItem Selected="True">2015-2016</asp:ListItem>
        </asp:DropDownList>
		<br />
		<br />
		<asp:Literal runat="server" ID="litReport"></asp:Literal>
	    <br />
        <br />
          
        <div style="float: none; width: inherit;" align="center">
            <%--<asp:ImageButton runat="server" ID="btnBack1" ImageUrl="~/Support/btnBack.jpg" 
                onclick="btnBack_Click" />--%>
            <asp:Button ID="btnBack1" runat="server" Text="Back" onclick="btnBack_Click" />
            <br />
        </div>
	</div>
</asp:Content>