<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EventDetails.aspx.cs" Inherits="Synergy_EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <a href="../Default.aspx" class="SiteMap">Home</a> » 
                    <a href="Synergy.aspx" class="SiteMap">Synergy</a> » 
                    <a href="EventMenu.aspx" class="SiteMap">Events</a> » 
					<asp:HyperLink runat="server" ID="lnkEventDetails" Font-Underline="true"></asp:HyperLink>
                </div>
            </td>
        </tr>
        <tr><td style="height: 9px;"></td></tr>
        <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px;
                    padding-bottom: 10px;">
                    <h2>Event Details</h2>
                    <br />
                    <br />
                    <br />
                    <div class="GridBorder">
                        <table width="100%" cellpadding="3" cellspacing="1">
                            <tr><td colspan="2" class="GridText">&nbsp;<b>Event Details</b></td></tr>
                            <tr>
                                <td class="GridRows" style="width: 15%">Event Name:</td>
                                <td class="GridRows" style="width: 85%">
                                    <asp:HiddenField runat="server" ID="hdnEventID" />
                                    <asp:TextBox runat="server" ID="txtEventName" CssClass="controls" Width="450px" MaxLength="50" BackColor="White"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="reqEventName" 
                                        ControlToValidate="txtEventName"
                                        ErrorMessage="<br>[Event name is required]" 
                                        Display="Dynamic" 
                                        ValidationGroup="InsertEvent" />
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">Category:</td>
                                <td class="GridRows">
                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="controls" BackColor="White"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">Division:</td>
                                <td class="GridRows">
                                    <asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" BackColor="White"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">Max Points:</td>
                                <td class="GridRows">
                                    <asp:TextBox runat="server" ID="txtMaxPoints" CssClass="controls" Width="50px" MaxLength="10" BackColor="White"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="reqMaxPoints" 
                                        ControlToValidate="txtMaxPoints"
                                        ErrorMessage="<br>[Maximum points is required]" 
                                        Display="Dynamic" 
                                        ValidationGroup="InsertEvent" />
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">Winner:</td>
                                <td class="GridRows">
                                    <asp:DropDownList runat="server" ID="ddlWinner" CssClass="controls" BackColor="White"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">Order:</td>
                                <td class="GridRows">
                                    <asp:TextBox runat="server" ID="txtOrder" CssClass="controls" Width="50px" MaxLength="5" BackColor="White"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="reqOrder" 
                                        ControlToValidate="txtOrder"
                                        ErrorMessage="<br>[Display order is required]" 
                                        Display="Dynamic" 
                                        ValidationGroup="InsertEvent" />
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">Created By:</td>
                                <td class="GridRows">
                                    <asp:TextBox runat="server" ID="txtCreateBy" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
                                    &nbsp; Date Created:
                                    <asp:TextBox runat="server" ID="txtCreateOn" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">Modified By:</td>
                                <td class="GridRows">
                                    <asp:TextBox runat="server" ID="txtModifyBy" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
                                    &nbsp; Date Modified:
                                    <asp:TextBox runat="server" ID="txtModifyOn" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="GridRows" style="text-align: right;">
                                    <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" ValidationGroup="InsertEvent" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <br />

                    <div class="GridBorder">
                        <table width="100%" cellpadding="0">
                            <tr><td class="GridText">&nbsp;&nbsp;<b>List of Schedules</b></td></tr>
                            <tr>
                                <td class="GridRows">
                                    <div runat="server" id="divSchedule" class="GridBorder">
                                        <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid" OnDeleteCommand="dgMatch_DeleteCommand">
                                            <HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
                                            <ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
                                            <AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdnGameID" Value='<%#DataBinder.Eval(Container.DataItem, "GameID")%>' />
                                                        <div style="padding: 5px;">
                                                            <a href='EventScheduleDetails.aspx?gameid=<%#DataBinder.Eval(Container.DataItem, "GameID")%>&eventid=<%Response.Write(Request.QueryString["EventID"]); %>'>
                                                                <img src="../Support/fileopen22.png" alt="[View Record]" />
                                                            </a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" AlternateText="Delete Record" ImageUrl="~/Support/delete16.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Schedule Details" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <a href='EventScheduleDetails.aspx?gameid=<%#DataBinder.Eval(Container.DataItem, "GameID")%>&eventid=<%#DataBinder.Eval(Container.DataItem, "EventID")%>'><%#DataBinder.Eval(Container.DataItem, "StartDate")%></a>
                                                            <br />
                                                            <asp:Label runat="server" ID="lblLocation" Text='<%#DataBinder.Eval(Container.DataItem, "Location")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Game Phase" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:HiddenField runat="server" ID="hdnGamePhase" Value='<%#DataBinder.Eval(Container.DataItem, "GamePhase")%>' />
                                                            <asp:Label runat="server" ID="lblGamePhase"></asp:Label>
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
                                                            <asp:Image runat="server" ID="imgWinner" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:HiddenField runat="server" ID="hdnFinished" Value='<%#DataBinder.Eval(Container.DataItem, "IsFinished")%>' />
                                                            <asp:Image runat="server" ID="imgFinished" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                    <asp:Label runat="server" ID="lblNoSchedule" Text="&nbsp;[No event schedule]" Font-Size="Small"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows" style="text-align: right;">
                                    <asp:ImageButton runat="server" ID="btnNewSchedule" ImageUrl="~/Support/btnNewSchedule.jpg"
                                        OnClick="btnNewSchedule_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="GridBorder">
                        <table width="100%" cellpadding="0">
                            <tr>
                                <td class="GridText">
                                    &nbsp;&nbsp;<b>Event Team Score</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows">
                                    <div class="GridBorder">
                                        <asp:DataGrid runat="server" ID="dgTeamEventScore" AutoGenerateColumns="false" Width="100%"
                                            BorderStyle="Solid">
                                            <HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
                                            <ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
                                            <AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Team" ItemStyle-Width="50%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:HiddenField runat="server" ID="hdnTeamID" Value='<%#DataBinder.Eval(Container.DataItem, "TeamID")%>' />
                                                            <asp:Label runat="server" ID="lblTeamName" Text='<%#DataBinder.Eval(Container.DataItem, "TeamName")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Rank" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:TextBox runat="server" ID="txtRank" CssClass="controls" BackColor="White" Width="50" Text='<%#DataBinder.Eval(Container.DataItem, "Rank")%>'></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Score" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:TextBox runat="server" ID="txtScore" CssClass="controls" BackColor="White" Width="50" Text='<%#DataBinder.Eval(Container.DataItem, "Score")%>'></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="GridRows" style="text-align: right;">
                                    <asp:ImageButton runat="server" ID="btnSaveEventTeamScore" ImageUrl="~/Support/btnSaveChanges.jpg"
                                        OnClick="btnSaveEventTeamScore_Click" />
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
