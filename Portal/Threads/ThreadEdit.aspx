<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="ThreadEdit.aspx.cs" Inherits="Threads_ThreadEdit" %>
<%@Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
	<asp:ScriptManager runat="server" ID="smThreadEdit"></asp:ScriptManager>
	<div class="ChildPagePanel">
		<a href="../Default.aspx" class="SiteMap">Home</a> » 
		<a href="Forums.aspx" class="SiteMap">Forums</a> » 
		<asp:HyperLink runat="server" ID="lnkThreadList" Font-Underline="true"></asp:HyperLink> » 
		<asp:HyperLink runat="server" ID="lnkThread" Font-Underline="true"></asp:HyperLink> » 
		<asp:HyperLink runat="server" ID="lnkThreadEdit" Font-Underline="true" Text="Edit Thread"></asp:HyperLink>
	</div>
	<div class="ChildPagePanel">		
		<h2>Edit Thread Details</h2>
		<br />
		<br />
		<div class="GridBorder">
			<table width="100%" cellpadding="3" cellspacing="1">
				<tr><td colspan="2" class="GridText">&nbsp;<b>Thread Details</b></td></tr>
				<tr>
					<td class="GridRows" style="width: 15%">Title:</td>
					<td class="GridRows" style="width: 85%">
						<asp:TextBox runat="server" ID="txtTitle" CssClass="controls" Width="98%" MaxLength="100" BackColor="White" />
						<asp:RequiredFieldValidator runat="server" ID="reqTitle" ControlToValidate="txtTitle" ErrorMessage="<br>[Required]" Display="Dynamic" ForeColor="Red" />
					</td>
				</tr>
				<tr>
					<td class="GridRows">Thread Category:</td>
					<td class="GridRows">
						<asp:DropDownList runat="server" ID="ddlCategory" CssClass="controls" BackColor="White"></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td class="GridRows">Thread Type:</td>
					<td class="GridRows">
						<asp:DropDownList runat="server" ID="ddlType" CssClass="controls" BackColor="White"></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td class="GridRows">Description:</td>
					<td class="GridRows">
						<asp:TextBox runat="server" ID="txtDescription" CssClass="controls" Width="98%" MaxLength="200" BackColor="White" />
					</td>
				</tr>
				<tr>
					<td class="GridRows">Attachment Title:</td>
					<td class="GridRows">
						<asp:TextBox runat="server" ID="txtAttachment" CssClass="controls" Width="400px" MaxLength="50" BackColor="White" />
					</td>
				</tr>
				<tr>
					<td class="GridRows">Attachment:</td>
					<td class="GridRows">
						<asp:FileUpload runat="server" ID="fuAttachment" />
					</td>
				</tr>
				<tr runat="server" id="trPostHome" visible="false">
					<td class="GridRows" style="vertical-align: top;">&nbsp;</td>
					<td class="GridRows">
						<asp:CheckBox runat="server" ID="chkPostAnnouncement" Text="Post this announcement on home pages" />
					</td>
				</tr>
				<tr>
					<td class="GridRows" style="vertical-align: top;">&nbsp;</td>
					<td class="GridRows">
						<asp:CheckBox runat="server" ID="chkIsAllowReply" Text="Allow users to comment on this post" Checked="true" />
					</td>
				</tr>
				<tr>
					<td class="GridRows" style="vertical-align: top;">&nbsp;</td>
					<td class="GridRows">
						<asp:CheckBox runat="server" ID="chkHideThread" Text="Deactivate this post" Checked="true" />
					</td>
				</tr>
				<tr>
					<td class="GridRows" style="vertical-align: top;">&nbsp;</td>
					<td class="GridRows">
						<asp:UpdatePanel runat="server" ID="upPrivateThread">
							<ContentTemplate>
								<asp:CheckBox runat="server" ID="chkIsPrivate" Text="Private Thread" AutoPostBack="true" oncheckedchanged="chkIsPrivate_CheckedChanged" />
								<div runat="server" id="divPrivateList" visible="false">
									<br />
									<table>
										<tr>
											<td class="GridRows" style="text-align: center; font-size: small; width: 45%">Thread Members</td>
											<td class="GridRows" style="width: 10%">&nbsp;</td>
											<td class="GridRows" style="text-align: center; font-size: small; width: 45%">Employee List</td>
										</tr>
										<tr>
											<td class="GridRows" style="text-align: center;">
												<div class="controls" style="width: 280px; height: 350px; overflow: auto; text-align: left;">
													<asp:CheckBoxList ID="cblThreadMembers" runat="server" Width="90%" RepeatLayout="Table" Font-Size="x-Small" BackColor="white" />
												</div>
											</td>
											<td class="GridRows">&nbsp;</td>
											<td class="GridRows" style="text-align: center;">
												<div class="controls" style="width: 280px; height: 350px; overflow: auto; text-align: left;">
													<asp:CheckBoxList ID="cblEmployeeList" runat="server" Width="90%" RepeatLayout="Table" Font-Size="x-Small" />
												</div>
											</td>
										</tr>
										<tr>
											<td class="GridRows" style="text-align: center;">
												<asp:ImageButton runat="server" ID="btnRemove" ImageUrl="~/Support/btnExclude.jpg" OnClick="btnRemove_Click" />
											</td>
											<td class="GridRows">
												&nbsp;
											</td>
											<td class="GridRows" style="text-align: center;">
												<asp:ImageButton runat="server" ID="btnInclude" ImageUrl="~/Support/btnInclude.jpg" OnClick="btnInclude_Click" />
											</td>
										</tr>
									</table>
								</div>
							</ContentTemplate>
						</asp:UpdatePanel>									
					</td>
				</tr>
				<tr>
					<td class="GridRows" style="vertical-align: top;">Contents:</td>
					<td class="GridRows">
						<CKEditor:CKEditorControl ID="ckeContents" runat="server" CssClass="controls" Height="300px" BackColor="White" Width="98%" />
					</td>
				</tr>
			</table>
		</div>
		<br />
		<div style="text-align: center;">
			<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" OnClick="btnSave_Click" />
			&nbsp;
			<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" />
		</div>
		<asp:HiddenField runat="server" ID="hdnThreadID" />
	</div>
</asp:Content>

