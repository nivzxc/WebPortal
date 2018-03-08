<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ThreadTypes.aspx.cs" Inherits="Threads_ThreadTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <%--<div class="ChildPagePanel">
        <a href="../Default.aspx" class="SiteMap">Home</a> » 
        <a href="Forums.aspx" class="SiteMap">Forums</a> » 
        <a href="ThreadTypes.aspx" class="SiteMap">Thread Type Assignment</a>
    </div>--%>
	<div class="ChildPagePanel">
		<h2>Thread Type Assignment</h2>
		<br />
		<br />
		<div class="GridBorder" style="width:600px;"> 	          
			<table width="100%" cellpadding="5">
				<tr>
					<td class="GridRows">Thread Type:</td>
					<td class="GridRows">
						<asp:DropDownList runat="server" ID="ddlThreadType" CssClass="controls" 
                         BackColor="White" AutoPostBack="true" 
                        ></asp:DropDownList>
					</td>     
					<td class="GridRows" rowspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click"/><%--<asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" onclick="btnSearch_Click" />--%></td>  
				</tr>
			</table>
		</div> 
		<br />		
		<div class="GridBorder">
		<table width="100%" cellpadding="3" cellspacing="1">
			<tr>
				<td colspan="3" class="GridText">
					&nbsp;<b>Thread Type Association</b>
				</td>
			</tr>
			<tr>
				<td class="GridRows" style="text-align: center; font-size: small; width: 45%">
					Included List
				</td>
				<td class="GridRows" style="width: 10%">
					&nbsp;
				</td>
				<td class="GridRows" style="text-align: center; font-size: small; width: 45%">
					Employee List
				</td>
			</tr>
			<tr>
				<td class="GridRows" style="text-align: center;">
					<div class="controls" style="width: 280px; height: 350px; overflow: auto; text-align: left;">
						<asp:CheckBoxList ID="cblThreadTypeUsers" runat="server" Width="90%" RepeatLayout="Table"
							Font-Size="x-Small" BackColor="white">
						</asp:CheckBoxList>
					</div>
				</td>
				<td class="GridRows">
					&nbsp;
				</td>
				<td class="GridRows" style="text-align: center;">
					<div class="controls" style="width: 280px; height: 350px; overflow: auto; text-align: left;">
						<asp:CheckBoxList ID="cblEmployeeList" runat="server" Width="90%" RepeatLayout="Table"
							Font-Size="x-Small" BackColor="white">
						</asp:CheckBoxList>
					</div>
				</td>
			</tr>
			<tr>
				<td class="GridRows" style="text-align: center;">
					<asp:ImageButton runat="server" ID="btnRemove" ImageUrl="~/Support/btnExclude.jpg"
						OnClick="btnRemove_Click" />
				</td>
				<td class="GridRows">
					&nbsp;
				</td>
				<td class="GridRows" style="text-align: center;">
					<asp:ImageButton runat="server" ID="btnInclude" ImageUrl="~/Support/btnInclude.jpg"
						OnClick="btnInclude_Click" />
				</td>
			</tr>
		</table>
	</div>		
	</div>
</asp:Content>

