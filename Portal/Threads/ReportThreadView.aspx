<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ReportThreadView.aspx.cs" Inherits="Threads_ReportThreadView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="ChildPagePanel">
        <a href="../Default.aspx" class="SiteMap">Home</a> » 
        <a href="Forums.aspx" class="SiteMap">Forums</a> » 
		<a href="Reports.aspx" class="SiteMap">Reports</a> » 
		<a href="ReportThreadView.aspx" class="SiteMap">Thread View Report</a>
    </div>
    <div class="ChildPagePanel">
        <h2>Report: Thread View</h2>
		<br />
        <br />
		<div class="GridBorder" style="width:600px;"> 	          
			<table width="100%" cellpadding="5">
				<tr>
					<td class="GridRows">Thread Category:</td>
					<td class="GridRows"><asp:DropDownList runat="server" ID="ddlThreadCategory" CssClass="controls" BackColor="White" AutoPostBack="true" onselectedindexchanged="ddlThreadCategory_SelectedIndexChanged"></asp:DropDownList></td>     
					<td class="GridRows" rowspan="2"><asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" onclick="btnSearch_Click" /></td>  
				</tr>
				<tr>
					<td class="GridRows">Thread:</td>
					<td class="GridRows"><asp:DropDownList runat="server" ID="ddlThread" CssClass="controls" BackColor="White"></asp:DropDownList></td>					
				</tr>
			</table>
		</div> 
		<br />
		<div class="GridBorder"> 
			<asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" Font-Size="Small">
				<Columns>
					<asp:BoundColumn HeaderText="Employee Name" DataField="Name" ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Total Views" DataField="TotalView" ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn> 
				</Columns>
			</asp:DataGrid>           
        </div>
	</div>
</asp:Content>

