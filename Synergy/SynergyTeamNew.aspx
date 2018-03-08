<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="SynergyTeamNew.aspx.cs" Inherits="Synergy_SynergyTeamNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
	<table width="100%" cellpadding="0" cellspacing="0">
		<%--<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<a href="../Default.aspx" class="SiteMap">Home</a> » 
					<a href="Synergy.aspx" class="SiteMap">Sports Fest</a> » 
					<a href="SynergyTeamNew.aspx" class="SiteMap">Add New Team</a> 
				</div>
			</td>
		</tr>
		<tr><td style="height: 9px;"></td></tr>--%>
		<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<b><span class="HeaderText">Add New Team</span></b>
					<br />
					<div runat="server" id="divError" visible="false">
						<br />
						<div class="ErrMsg">
							<b>Error during update. Please correct your data entries:</b><br />
							<asp:Label runat="server" ID="lblErrMsg"></asp:Label>
						</div>
					</div>

					<div class="GridBorder">
						<table width="100%" cellpadding="3" cellspacing="1">
<%--							<tr><td colspan="2" class="GridText">&nbsp;<b>Team Details</b></td>--%>
							</tr>
							<tr>
								<td class="GridRows">Team Name:</td>
								<td class="GridRows">
									<asp:TextBox runat="server" ID="txtTeamName" CssClass="controls" Width="250px" 
                                        MaxLength="50" BackColor="White"></asp:TextBox>
									<asp:RequiredFieldValidator runat="server" ID="reqLocation" 
										ControlToValidate="txtTeamName"
										ErrorMessage="&lt;br&gt;[Team Name Required]" 
										Display="Dynamic" 
										ValidationGroup="InsertTeam" ForeColor="Red" />
								</td>
							</tr>
							<tr>
								<td class="GridRows">Captain:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlCaptain" CssClass="controls" BackColor="White"></asp:DropDownList>
								</td>
							</tr>
                            <tr>
								<td class="GridRows">Vice-Captain:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlViceCaptain" CssClass="controls" BackColor="White"></asp:DropDownList>
								</td>
							</tr>
                           
							<tr>
								<td class="GridRows">&nbsp;</td>
								<td class="GridRows">
									<asp:CheckBox runat="server" ID="chkActive" Text="Active" Font-Size="X-Small" />
								</td>
							</tr>
						</table>
					</div>
					<br />
					<div style="text-align: center;">
						<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" 
                            ValidationGroup="InsertTeam" OnClick="btnSave_Click" />
						&nbsp;
						<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" />
					</div>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
