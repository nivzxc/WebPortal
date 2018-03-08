<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="Synergy.aspx.cs" Inherits="Synergy_Synergy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <%--<div class="ChildPagePanel" id="dvVote" runat="server" visible="false">
		<a href="Survey01.aspx" 
            style="font-family: Calibri; font-size: large; font-weight: bold;">STI Synergy Games - Survey</a>
   </div>--%>
	<div class="ChildPagePanel" runat="server" id="divControlPanel" visible="false">
		<h2>Sports Fest 2012-13</h2>
<%--		<br />
		<br />
		<div class="ChildPagePanelBlue" style="font-size: small;">
			<a href="EmployeeAchievement.aspx">Set Employee Sportfest Awards</a>
		</div>
        <div class="ChildPagePanelBlue" style="font-size: small;">
			<a href="SurveyResult.aspx">Survey Result</a>
		</div>--%>
	</div>
	<%--<div class="ChildPagePanel">
         <div style="border: solid 2px cornflowerblue; background-color:aliceblue;">
          <table cellpadding="5" cellspacing="5">
           <tr><td style="font-size:small; color:cornflowerblue;"><b>STI Synergy Games 2011-12 - Slogan</b></td></tr>
           <tr><td style="font-size:medium;">Ignite the Will, Take the Victory, Speed through obstacles. Team STI!</td></tr>
          </table>      
         </div>
	</div>--%>

<%--	<div class="ChildPagePanel">
		<h2>STI Synergy Games 2011-12 Announcements, News and Updates</h2>
        <br />
		<br />
        <div style="width:100%;height:100%; overflow:auto;">
         <asp:Literal runat="server" ID="litAnnouncements"></asp:Literal>
        </div>
        <div style="padding: 5px;">
							<center><asp:HyperLink runat="server" ID="HyperLink1" Font-Size="Small" 
         Text="[View all announcements]" NavigateUrl="~/Synergy/SynergyAll.aspx" /></center>
         </div>
   </div>--%>
	<div class="ChildPagePanel">
		<h2>Schedules</h2>

		<div class="GridBorder">
			<table width="100%" cellpadding="0">
	<%--			<tr><td class="GridColumns">&nbsp;&nbsp;<b>List of Schedules</b></td></tr>--%>
				<tr>
					<td class="GridRows">
						<div runat="server" id="divSchedule" class="GridBorder">
							<asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%"
								BorderStyle="Solid">
								<HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
								<ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
								<AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
								<Columns>
									<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
										<ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnGameID" Value='<%#DataBinder.Eval(Container.DataItem, "GameID")%>' />
											<div style="padding: 5px;">
												<a href='EventScheduleDetails.aspx?schdcode=<%#DataBinder.Eval(Container.DataItem, "GameID")%>&evntcode=<%#DataBinder.Eval(Container.DataItem, "EventID") %>'>
													<img src="../Support/fileopen22.png" alt="[View Record]" />
                                                </a>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Event Details" ItemStyle-Width="30%">
										<ItemTemplate>
											<div style="padding: 5px;">
                                                <asp:HiddenField runat="server" ID="hdnGamePhase" Value='<%#DataBinder.Eval(Container.DataItem, "GamePhase")%>' />
                                                <a href='EventScheduleDetails.aspx?gameid=<%#DataBinder.Eval(Container.DataItem, "GameID")%>&eventid=<%#DataBinder.Eval(Container.DataItem, "EventID")%>'><%#DataBinder.Eval(Container.DataItem, "EventName")%></a>
                                                <br />															
												<asp:Label runat="server" ID="lblGamePhase"></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Schedule Details" ItemStyle-Width="25%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:Label runat="server" ID="lblDateStart" Text='<%#DataBinder.Eval(Container.DataItem, "StartDate")%>'></asp:Label>
                                                <br />
                                                <asp:Label runat="server" ID="lblLocation" Text='<%#DataBinder.Eval(Container.DataItem, "Location")%>'></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Teams" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
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
												<asp:Image runat="server" ID="imgWinner"   Width="50px" Height="50px" />
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid>
						</div>
					</td>
				</tr>
				<tr>
					<td class="BrowseAll" style="text-align: center;">
						<div style="padding: 5px;">
							<asp:HyperLink runat="server" ID="lnkScheduleAll" Font-Size="Small" Text="[View all schedule]" NavigateUrl="~/Synergy/EventScheduleAll.aspx" />
                        </div>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div class="ChildPagePanel">
		<h2>Finished Events</h2>
		<div class="GridBorder">
			<table width="100%" cellpadding="0">
<%--				<tr><td class="GridColumns">&nbsp;&nbsp;<b>List of Finished Events</b></td></tr>--%>
				<tr>
					<td class="GridRows">
						<div class="GridBorder">
							<asp:DataGrid runat="server" ID="dgFinishedEvents" AutoGenerateColumns="false" Width="100%"
								BorderStyle="Solid">
								<HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
								<ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
								<AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
								<Columns>
									<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<a href='EventDetails.aspx?eventid=<%#DataBinder.Eval(Container.DataItem, "EventID")%>'><img src="../Support/fileopen22.png" alt="[View Record]" /></a>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Event Name" ItemStyle-Width="40%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:HiddenField runat="server" ID="hdnEventID" Value='<%#DataBinder.Eval(Container.DataItem, "EventID")%>' />
												<a href='EventDetails.aspx?eventid=<%#DataBinder.Eval(Container.DataItem, "EventID")%>'><%#DataBinder.Eval(Container.DataItem, "Name")%></a>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Division" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:Label runat="server" ID="lblDivision" Text='<%#DataBinder.Eval(Container.DataItem, "EventDivisionName")%>'></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Category" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:Label runat="server" ID="lblCategory" Text='<%#DataBinder.Eval(Container.DataItem, "EventCategoryName")%>'></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Max Points" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:Label runat="server" ID="lblMaxPoint" Text='<%#DataBinder.Eval(Container.DataItem, "MaxPoint")%>'></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Winner" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:HiddenField runat="server" ID="hdnWinner" Value='<%#DataBinder.Eval(Container.DataItem, "WinnerTeamID")%>' />
												<asp:Image runat="server" ID="imgWinner" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "WinnerTeamLogo")%>'   Width="50px" Height="50px" />
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid>
						</div>
					</td>
				</tr>
				<tr><td class="BrowseAll" style="text-align: center;"><div style="padding: 5px;"><asp:HyperLink runat="server" ID="lnkEventsViewAll" Font-Size="Small" Text="[View all events]" NavigateUrl="~/Synergy/EventMenu.aspx"></asp:HyperLink></div></td></tr>
			</table>
		</div>
	</div>
	<div class="ChildPagePanel">
		<h2>Teams</h2>
		<br />
		<br />
		<div class="GridBorder">
			<table width="100%" cellpadding="0">
<%--				<tr><td class="GridText">&nbsp;&nbsp;<b>List Teams</b></td></tr>--%>
				<tr>
					<td class="GridRows">
						<div class="GridBorder">
							<asp:DataGrid runat="server" ID="dgTeams" AutoGenerateColumns="false" Width="100%"
								BorderStyle="Solid">
								<HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
								<ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
								<AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
								<Columns>
									<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<a href='SynergyTeamEdit.aspx?teamid=<%#DataBinder.Eval(Container.DataItem, "TeamID")%>'>
													<img src="../Support/fileopen22.png" alt="[View Record]" /></a>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Team Name" ItemStyle-Width="45%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:HiddenField runat="server" ID="hdnTeamID" Value='<%#DataBinder.Eval(Container.DataItem, "TeamID")%>' />
												<table>
													<tr>
														<td width="75px"><asp:Image runat="server"   Width="50px" Height="50px" ID="imgLogo" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "TeamLogo")%>' /></td>
														<td><a href='TeamDetails.aspx?teamid=<%#DataBinder.Eval(Container.DataItem, "TeamID")%>'><%#DataBinder.Eval(Container.DataItem, "Name")%></a></td>
													</tr>
												</table>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Captain" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:Label runat="server" ID="lblCaptain" Text='<%#DataBinder.Eval(Container.DataItem, "Captain")%>'></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Vice-Captain" ItemStyle-HorizontalAlign="Center"
										ItemStyle-Width="20%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:Label runat="server" ID="lblViceCaptain" Text='<%#DataBinder.Eval(Container.DataItem, "ViceCaptain")%>'></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Score" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<div style="padding: 5px;">
												<asp:Label runat="server" ID="lblScore" Text='<%#DataBinder.Eval(Container.DataItem, "Score")%>'></asp:Label>
											</div>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid>
						</div>
					</td>
				</tr>
                <tr><td class="BrowseAll" style="text-align: center;"><div id="divAddTeam" runat="server" style="padding: 5px;">
                    <asp:HyperLink runat="server" ID="HyperLink1" Font-Size="Small" 
                        Text="[Add New Team]" NavigateUrl="~/Synergy/SynergyTeamNew.aspx"></asp:HyperLink></div></td></tr>
			</table>
            
		</div>
	</div>

	<div class="ChildPagePanel" runat="server" visible="false">
		<h2>Scoring Matrix</h2>
		<div class="GridBorder">
			<table width="100%" cellpadding="6" cellspacing="1">
				<tr>
					<td class="GridColumns" style="width: 25%;"><b>Event Rank</b></td>
					<td class="GridColumns" style="width: 75%;" colspan="5"><b>Team Score Points</b></td>
				</tr>
				<tr>
					<td class="GridRows" style="text-align: center;">1st Place</td>
					<td class="GridRows" style="text-align: center; width: 15%;">5</td>
					<td class="GridRows" style="text-align: center; width: 15%;">10</td>
					<td class="GridRows" style="text-align: center; width: 15%;">15</td>
					<td class="GridRows" style="text-align: center; width: 15%;">20</td>
					<td class="GridRows" style="text-align: center; width: 15%;">25</td>
				</tr>
				<tr>
					<td class="GridRows" style="text-align: center;">2nd Place</td>
					<td class="GridRows" style="text-align: center;">3</td>
					<td class="GridRows" style="text-align: center;">6</td>
					<td class="GridRows" style="text-align: center;">9</td>
					<td class="GridRows" style="text-align: center;">12</td>
					<td class="GridRows" style="text-align: center;">15</td>
				</tr>
				<tr>
					<td class="GridRows" style="text-align: center;">3rd Place</td>
					<td class="GridRows" style="text-align: center;">2</td>
					<td class="GridRows" style="text-align: center;">4</td>
					<td class="GridRows" style="text-align: center;">6</td>
					<td class="GridRows" style="text-align: center;">8</td>
					<td class="GridRows" style="text-align: center;">10</td>
				</tr>
				<tr>
					<td class="GridRows" style="text-align: center;">4th Place</td>
					<td class="GridRows" style="text-align: center;">1</td>
					<td class="GridRows" style="text-align: center;">2</td>
					<td class="GridRows" style="text-align: center;">3</td>
					<td class="GridRows" style="text-align: center;">4</td>
					<td class="GridRows" style="text-align: center;">5</td>
				</tr>
			</table>
		</div>
	</div>
</asp:Content>