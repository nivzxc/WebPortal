<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EmployeeAchievement.aspx.cs" Inherits="Synergy_EmployeeAchievement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<%--    <div class="ChildPagePanel">
        <a href="../Default.aspx" class="SiteMap">Home</a> » 
        <a href="Synergy.aspx" class="SiteMap">Sports Fest</a> » 
        <a href="EmployeeAchievement.aspx" class="SiteMap">Employee Achievement</a>
    </div>--%>
	<div class="ChildPagePanel">
		<h2>Employee Achivement</h2>
		<br />
		<br />
        <div class="GridBorder">
            <table width="100%" cellpadding="3" cellspacing="1">
  <%--              <tr><td colspan="2" class="GridText">&nbsp;<b>Select Employee</b></td></tr>--%>
                <tr>
                    <td class="GridRows" style="width: 15%">Select Activity:</td>
                    <td class="GridRows" style="width: 85%">
                        <asp:DropDownList runat="server" ID="ddlActivity" CssClass="controls" BackColor="White"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="GridRows">Select Employee:</td>
                    <td class="GridRows">
                        <asp:DropDownList runat="server" ID="ddlTeamMember" CssClass="controls" 
							BackColor="White" onselectedindexchanged="ddlTeamMember_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="GridRows">Award/Achievement:</td>
                    <td class="GridRows">
                        <asp:TextBox runat="server" ID="txtAchievement" CssClass="controls" BackColor="White" MaxLength="50" Width="250px"></asp:TextBox>
						<asp:RequiredFieldValidator runat="server" ID="reqAchievement"
							ErrorMessage="(Required)"
							Display="Dynamic"
							ControlToValidate="txtAchievement"
							ValidationGroup="Insert" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="GridRows" style="text-align: center;">
                        <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" OnClick="btnSave_Click" ValidationGroup="Insert" />
                    </td>
                </tr>
				<tr>
					<td class="GridRows">Award/Achievement Received:</td>
					<td class="GridRows">
						<div class="GridBorder"> 
							<asp:DataGrid runat="server" ID="dgAchievements" AutoGenerateColumns="false" 
								Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" 
								BorderStyle="Solid" ondeletecommand="dgAchievements_DeleteCommand">
								<Columns>
									<asp:BoundColumn HeaderText="ActivityName" DataField="AchievementID" ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="ActivityName" DataField="ActivityName" ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Awards" DataField="Awards" ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn> 
									<asp:ButtonColumn Text="[Delete]" ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center" CommandName="Delete"></asp:ButtonColumn>
								</Columns>
							</asp:DataGrid>           
						</div>					
					</td>
				</tr>
            </table>
        </div>
	</div>
</asp:Content>

