<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true"
	CodeFile="EventScheduleAll.aspx.cs" Inherits="Synergy_EventScheduleAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
		<%--<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<a href="../Default.aspx" class="SiteMap">Home</a> » 
					<a href="TeamDetails.aspx" class="SiteMap">Sports Fest</a> » 
					<a href="EventScheduleAll.aspx" class="SiteMap">All Schedule</a>
				</div>
			</td>
		</tr>
		<tr><td style="height: 9px;"></td></tr>--%>
		<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<h2>Schedule Masterlist</h2>
					
					<div class="GridBorder" style="width: 60%;">
						<table width="100%" cellpadding="5">
		<%--					<tr><td colspan="3" align="center" class="GridText">&nbsp;<b>Filter Record</b></td></tr>--%>
							<tr>
								<td class="GridRows">Team:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlTeam" CssClass="controls" BackColor="white"	AutoPostBack="true"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="GridRows">Event:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlEvent" CssClass="controls" BackColor="white" AutoPostBack="true"></asp:DropDownList>
								</td>
							</tr>
						</table>
					</div>
					<br />
					<div class="GridBorder">
						<table width="100%" cellpadding="0">
							<tr><td class="GridColumns">&nbsp;&nbsp;<b>List of Schedules</b></td></tr>
							<tr>
								<td class="GridRows">
									<div runat="server" id="divSchedule" class="GridBorder">
										<asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid">
											<HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
											<ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
											<AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
											<Columns>
												<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
													<ItemTemplate>
														<div style="padding: 5px;">
															<a href='EventScheduleDetails.aspx?gameid=<%#DataBinder.Eval(Container.DataItem, "GameID")%>&eventid=<%#DataBinder.Eval(Container.DataItem, "EventID") %>'>
																<img src="../Support/fileopen22.png" alt="[View Record]" />
															</a>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Event Details" ItemStyle-Width="25%">
													<ItemTemplate>
														<div style="padding: 5px;">
															<asp:HiddenField runat="server" ID="hdnGameID" Value='<%#DataBinder.Eval(Container.DataItem, "GameID")%>' />
															<asp:HiddenField runat="server" ID="hdnEventID" Value='<%#DataBinder.Eval(Container.DataItem, "EventID")%>' />
															<asp:HiddenField runat="server" ID="hdnGamePhase" Value='<%#DataBinder.Eval(Container.DataItem, "GamePhase")%>' />
															<a href='EventScheduleDetails.aspx?gameid=<%#DataBinder.Eval(Container.DataItem, "GameID")%>&eventid=<%#DataBinder.Eval(Container.DataItem, "EventID") %>'><%#DataBinder.Eval(Container.DataItem, "EventName") %></a>
															<br />
															<asp:Label runat="server" ID="lblGamePhase"></asp:Label>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Schedule Details" ItemStyle-Width="25%">
													<ItemTemplate>
														<div style="padding: 5px;">															
															<asp:HiddenField runat="server" ID="hdnDateStart" Value='<%#DataBinder.Eval(Container.DataItem, "StartDate")%>' />
															<asp:Label runat="server" ID="lblDateStart"></asp:Label>
															<br />
															<asp:Label runat="server" ID="lblLocation" Text='<%#DataBinder.Eval(Container.DataItem, "Location")%>'></asp:Label>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Teams" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35%">
													<ItemTemplate>
														<div style="padding: 5px;">
															<asp:Literal runat="server" ID="litTeams"></asp:Literal>
														</div>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Winner" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
													<ItemTemplate>
														<div style="padding: 5px;">
															<asp:HiddenField runat="server" ID="hdnWinner" Value='<%#DataBinder.Eval(Container.DataItem, "WinnerTeamID")%>' />
															<asp:Image runat="server" ID="imgWinner"  Width="50px" Height="50px"/>
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
						<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" /></div>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
