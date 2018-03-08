<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EventScheduleNew.aspx.cs" Inherits="Synergy_EventScheduleNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
	<table width="100%" cellpadding="0" cellspacing="0">
		<%--<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<a href="../Default.aspx" class="SiteMap">Home</a> » 
					<a href="Synergy.aspx" class="SiteMap">Sports Fest</a> » 
					<a href="EventMenu.aspx" class="SiteMap">Events</a> » 
					<a href="EventDetails.aspx?evntcode=<%Response.Write(Request.QueryString["evntcode"]); %>" class="SiteMap">Event Details</a> » 
					<a href="EventScheduleNew.aspx?evntcode=<%Response.Write(Request.QueryString["evntcode"]); %>" class="SiteMap">New Schedule</a>
				</div>
			</td>
		</tr>
		<tr><td style="height: 9px;"></td></tr>--%>
		<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<b><span class="HeaderText">Add New Event Schedule</span></b>
					<br />
					<div runat="server" id="divError" visible="false">
						<br />
						<div class="ErrMsg">
							<b>Error during update. Please correct your data entries:</b><br />
							<asp:Label runat="server" ID="lblErrMsg"></asp:Label>
						</div>
					</div>
					<br />
					<br />
					<div class="GridBorder">
						<table width="100%" cellpadding="3" cellspacing="1">
							<tr><td colspan="2" class="GridText">&nbsp;<b>Event Schedule Details</b></td>
							</tr>
							<tr>
								<td class="GridRows">Event:</td>
								<td class="GridRows">
									<asp:HiddenField runat="server" ID="hdnEventID" />
									<asp:TextBox runat="server" ID="txtEvent" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="GridRows">Game Phase:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlGamePhase" CssClass="controls" BackColor="White"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="GridRows">Start:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlStartMonth" CssClass="controls" BackColor="White"></asp:DropDownList>
									<asp:DropDownList runat="server" ID="ddlStartDay" CssClass="controls" BackColor="White"></asp:DropDownList>
									<asp:TextBox runat="server" ID="txtStartYear" CssClass="controls" Width="40px" MaxLength="4" BackColor="White"></asp:TextBox>
									<asp:CompareValidator runat="server" ID="comStartYear" 
										ControlToValidate="txtStartYear"
										Operator="DataTypeCheck" 
										Type="Integer" 
										ErrorMessage="<br>[Invalid year]" 
										Display="Dynamic"
										ValidationGroup="InsertSchedule" />
									<asp:RequiredFieldValidator runat="server" ID="reqStartYear" 
										ControlToValidate="txtStartYear"
										ErrorMessage="[Year is required]" 
										Display="Dynamic" 
										ValidationGroup="InsertSchedule" />
									-&nbsp;
									<asp:DropDownList runat="server" ID="ddlStartHour" CssClass="controls" BackColor="White"></asp:DropDownList>
									:
									<asp:DropDownList runat="server" ID="ddlStartMinute" CssClass="controls" BackColor="White"></asp:DropDownList>
									<asp:DropDownList runat="server" ID="ddlStartTimePeriod" CssClass="controls" BackColor="White"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="GridRows">End:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlEndMonth" CssClass="controls" BackColor="White"></asp:DropDownList>
									<asp:DropDownList runat="server" ID="ddlEndDay" CssClass="controls" BackColor="White"></asp:DropDownList>
									<asp:TextBox runat="server" ID="txtEndYear" CssClass="controls" Width="40px" MaxLength="4" BackColor="White"></asp:TextBox>									
									<asp:CompareValidator runat="server" ID="comEndYear" 
										ControlToValidate="txtEndYear"
										Operator="DataTypeCheck" 
										Type="Integer" 
										ErrorMessage="<br>[Invalid year]" 
										Display="Dynamic"
										ValidationGroup="InsertSchedule" />
									<asp:RequiredFieldValidator runat="server" ID="reqEndYear" 
										ControlToValidate="txtEndYear"
										ErrorMessage="[Year is required]" 
										Display="Dynamic" 
										ValidationGroup="InsertSchedule" />
									-&nbsp;
									<asp:DropDownList runat="server" ID="ddlEndHour" CssClass="controls" BackColor="White"></asp:DropDownList>
									:
									<asp:DropDownList runat="server" ID="ddlEndMinute" CssClass="controls" BackColor="White"></asp:DropDownList>
									<asp:DropDownList runat="server" ID="ddlEndTimePeriod" CssClass="controls" BackColor="White"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="GridRows">Location:</td>
								<td class="GridRows">
									<asp:TextBox runat="server" ID="txtLocation" CssClass="controls" Width="250px" MaxLength="50" BackColor="White"></asp:TextBox>
									<asp:RequiredFieldValidator runat="server" ID="reqLocation" 
										ControlToValidate="txtLocation"
										ErrorMessage="<br>[Location is required]" 
										Display="Dynamic" 
										ValidationGroup="InsertSchedule" />
								</td>
							</tr>
							<tr>
								<td class="GridRows">Winner:</td>
								<td class="GridRows">
									<asp:DropDownList runat="server" ID="ddlWinner" CssClass="controls" BackColor="White"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="GridRows">&nbsp;</td>
								<td class="GridRows">
									<asp:CheckBox runat="server" ID="chkFinished" Text="Finished?" Font-Size="X-Small" />
								</td>
							</tr>
						</table>
					</div>
					<br />
					<div style="text-align: center;">
						<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" ValidationGroup="InsertSchedule" OnClick="btnSave_Click" />
						&nbsp;
						<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" />
					</div>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
