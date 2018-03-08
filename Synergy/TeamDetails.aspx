<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true"
	CodeFile="TeamDetails.aspx.cs" Inherits="Synergy_TeamDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 5%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
		<%--<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<a href="../Default.aspx" class="SiteMap">Home</a> » 
					<a href="SynergyHome.aspx" class="SiteMap">Sports Fest</a> » 
					<a href="TeamDetails.aspx?teamid=<%Response.Write(Request.QueryString["teamid"]); %>" class="SiteMap">Team Details</a>
				</div>
			</td>
		</tr>
		<tr>
			<td style="height: 9px;">
			</td>
		</tr>--%>
		<tr>
			<td>
				<div class="border">
					<h2>Team Details</h2>
					<br />
					<br />
					<div class="GridBorder">
						<table width="100%" cellpadding="3" cellspacing="1">
	<%--						<tr><td colspan="3" class="GridText">&nbsp;<b>Team Details</b></td></tr>--%>
							<tr>
								<td class="GridRows" style="width: 150px" rowspan="4">
                                    <asp:Image runat="server" ID="imgpnlavatar" Width="130px" Height="150px" />
                                </td>
								<td class="GridRows" style="width: 30%">Team Name:</td>
								<td class="GridRows" style="width: 70%">
									<asp:HiddenField runat="server" ID="hdnTeamID" />
									<asp:TextBox runat="server" ID="txtTeamName" CssClass="controls" Width="386px" 
                                        ReadOnly="true"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="GridRows">Captain:</td>
								<td class="GridRows"><asp:TextBox runat="server" ID="txtCaptain" CssClass="controls" Width="250px" ReadOnly="true" /></td>
							</tr>
							<tr>
								<td class="GridRows">Vice-Captain:</td>
								<td class="GridRows"><asp:TextBox runat="server" ID="txtViceCaptain" CssClass="controls" Width="250px" ReadOnly="true" /></td>
							</tr>
                            <tr>
								<td class="GridRows"></td>
                                <td class="GridRows"></td>
							</tr>
						</table>
					</div>
					<br />
                    <br />
					<div class="GridBorder" runat="server" id="trTeamCompositionEditor">
						<table width="100%" cellpadding="3" cellspacing="1">
							<%--<tr>
								<td colspan="3" class="GridText">
									&nbsp;<b>Team Composition Editor</b>
								</td>
							</tr>--%>
							<tr>
								<td class="GridColumns" style="text-align: center; font-size: small; width: 45%">
									Team Members
								</td>
								<td class="style1" rowspan="3">
									&nbsp;
									&nbsp;
									&nbsp;
								</td>
								<td class="GridColumns" style="text-align: center; font-size: small; width: 45%">
									Employee List
								</td>
							</tr>
							<tr>
								<td class="GridRows" style="text-align: center;">
									<div class="controls" style="width: 280px; height: 350px; overflow: auto; text-align: left;">
										<asp:CheckBoxList ID="cblTeamMembers" runat="server" Width="90%" RepeatLayout="Table"
											Font-Size="x-Small" BackColor="white">
										</asp:CheckBoxList>
									</div>
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
								<td class="GridRows" style="text-align: center;">
									<asp:ImageButton runat="server" ID="btnInclude" ImageUrl="~/Support/btnInclude.jpg"
										OnClick="btnInclude_Click" />
								</td>
							</tr>
						</table>
					</div>
					<br />
					<div class="GridBorder">
						<table width="100%" cellpadding="0">
							<tr><td class="GridColumns">&nbsp;&nbsp;<b>Team Composition</b></td></tr>
							<tr>
								<td class="GridRows">
									<div class="GridBorder">
										<asp:DataGrid runat="server" ID="dgTeamComposition" AutoGenerateColumns="false" Width="100%"
											BorderStyle="Solid">
											<HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
											<ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
											<AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
											<Columns>
												<asp:TemplateColumn HeaderText="Member Details" ItemStyle-Width="35%">
													<ItemTemplate>
														<div style="padding: 5px;">
															<asp:HiddenField runat="server" ID="hdnUsername" Value='<%#DataBinder.Eval(Container.DataItem, "username")%>' />
															<asp:HiddenField runat="server" ID="hdnDivision" Value='<%#DataBinder.Eval(Container.DataItem, "diviname")%>' />
															<asp:HiddenField runat="server" ID="hdnDepartment" Value='<%#DataBinder.Eval(Container.DataItem, "deptname")%>' />
															<table style="width: 100%;">
																<tr>
																	<td valign="top">
																		<asp:Image runat="server" ID="imgPicture" Height="80" Width="80" />
																	</td>
																	
																	<td style="width: 99%;">
																		<table style="width: 98%;border-color:#FAFAFA">
																			<tr>
																				<td style="border-color:#FAFAFA">Name:</td>
																				
																				<td style="width: 99%;border-color:#FAFAFA">
																					<asp:Label runat="server" ID="lblName" Text='<%#DataBinder.Eval(Container.DataItem, "fullname")%>'></asp:Label>
																				</td>
																			</tr>
																			<tr>
																				<td style="border-color:#FAFAFA">Nick Name:</td>
																			
																				<td style="border-color:#FAFAFA">
																					<asp:Label runat="server" ID="lblNickName" Text='<%#DataBinder.Eval(Container.DataItem, "nickname")%>'></asp:Label>
																				</td>
																			</tr>
																			<tr>
																				<td style="border-color:#FAFAFA">Position:</td>
																			
																				<td style="border-color:#FAFAFA">
																					<asp:Label runat="server" ID="lblPosition" Text='<%#DataBinder.Eval(Container.DataItem, "position")%>'></asp:Label>
																				</td>
																			</tr>
																			<tr>
																				<td style="border-color:#FAFAFA">Department:</td>
																
																				<td style="border-color:#FAFAFA">
																					<asp:Label runat="server" ID="lblDepartment"></asp:Label>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:DataGrid>
									</div>
								</td>
							</tr>
						</table>
					</div>
					<br />
					<div style="text-align: center;">
						<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg"
							OnClick="btnBack_Click" /></div>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
