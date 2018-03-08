<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true"
    CodeFile="EventScheduleDetails.aspx.cs" Inherits="Synergy_EventScheduleDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
     <%--   <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <a href="../Default.aspx" class="SiteMap">Home</a> » 
                    <a href="Synergy.aspx" class="SiteMap">Sports Fest</a> » 
                    <a href="EventMenu.aspx" class="SiteMap">Events</a> » 
                    <a href="EventDetails.aspx?eventid=<%Response.Write(Request.QueryString["eventid"]); %>" class="SiteMap">Event Details</a> » 
                    <a href="EventScheduleDetails.aspx?gameid=<%Response.Write(Request.QueryString["gameid"]); %>&eventid=<%Response.Write(Request.QueryString["eventid"]); %>" class="SiteMap">Schedule Details</a>
                </div>
            </td>
        </tr>
        <tr><td style="height: 9px;"></td></tr>--%>
        <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <b><span class="HeaderText">Event Schedule Details</span></b>
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
                 <%--           <tr><td colspan="2" class="GridText">&nbsp;<b>Event Schedule Details</b></td></tr>--%>
                            <tr>
                                <td class="GridRows">Event:</td>
                                <td class="GridRows">
                                    <asp:HiddenField runat="server" ID="hdnEventID" />
                                    <asp:HiddenField runat="server" ID="hdnGameID" />
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
                                    <asp:TextBox runat="server" ID="txtStartYear" CssClass="controls" Width="40px" MaxLength="4" ValidationGroup="InsertSchedule" BackColor="White"></asp:TextBox>
                                    <asp:CompareValidator runat="server" ID="comStartYear" 
                                        ControlToValidate="txtStartYear" 
                                        Operator="DataTypeCheck" Type="Integer" 
                                        ErrorMessage="<br>[Invalid year]" 
                                        Display="Dynamic"
                                        ValidationGroup="UpdateSchedule" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqStartYear" 
                                        ControlToValidate="txtStartYear"
                                        ErrorMessage="[Year is required]" 
                                        Display="Dynamic" 
                                        ValidationGroup="UpdateSchedule" />
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
                                    <asp:DropDownList runat="server" ID="ddlEndMonth" CssClass="controls" BackColor="White">
                                    </asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddlEndDay" CssClass="controls" BackColor="White">
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtEndYear" CssClass="controls" Width="40px" MaxLength="4" BackColor="White"></asp:TextBox>
                                    <asp:CompareValidator runat="server" ID="comEndYear" 
                                        ControlToValidate="txtEndYear" 
                                        Operator="DataTypeCheck" 
                                        Type="Integer" 
                                        ErrorMessage="<br>[Invalid year]" 
                                        Display="Dynamic"
                                        ValidationGroup="UpdateSchedule" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqEndYear" 
                                        ControlToValidate="txtEndYear"
                                        ErrorMessage="[Year is required]" 
                                        Display="Dynamic" 
                                        ValidationGroup="UpdateSchedule" />
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
                                        ValidationGroup="UpdateSchedule" />
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
                            <tr>
                                <td runat="server" id="trSaveSchedule" colspan="2" class="GridRows" style="text-align: right;">
                                    <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" ValidationGroup="UpdateSchedule" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="GridBorder">
                        <table width="100%" cellpadding="0">
                            <tr><td class="GridColumns">&nbsp;&nbsp;<b>Game Officials</b></td>
                            </tr>
                            <tr>
                                <td class="GridRows">
                                    <div class="GridBorder" runat="server" id="divOfficials">
                                        <asp:DataGrid runat="server" ID="dgOfficials" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid" OnDeleteCommand="dgOfficials_DeleteCommand">
                                            <HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
                                            <ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
                                            <AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Game Official" ItemStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:Label runat="server" ID="lblUsername" Text='<%#DataBinder.Eval(Container.DataItem, "OfficialID")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" AlternateText="Delete Record"
                                                            ImageUrl="~/Support/delete16.png"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                    <asp:Label runat="server" ID="lblNoOfficial" Text="&nbsp;[No event schedule]" Font-Size="Small"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="trOfficialsAdd">
                                <td class="GridRows">
                                    <table>
                                        <tr>
                                            <td><asp:DropDownList runat="server" ID="ddlCommittee" CssClass="controls" BackColor="White"></asp:DropDownList></td>
                                            <td><asp:ImageButton runat="server" ID="btnAddCommittee" ImageUrl="~/Support/btnSmallAdd.jpg" OnClick="btnAddCommittee_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="GridBorder">
                        <table width="100%" cellpadding="0">
                            <tr><td class="GridColumns">&nbsp;&nbsp;<b>Competing Teams</b></td></tr>
                            <tr>
                                <td class="GridRows">
                                    <div class="GridBorder">
                                        <asp:DataGrid runat="server" ID="dgCompetingTeams" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid" OnDeleteCommand="dgCompetingTeams_DeleteCommand" OnItemCommand="dgCompetingTeams_ItemCommand">
                                            <HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
                                            <ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
                                            <AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Team" ItemStyle-Width="25%" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdnTeamID" Value='<%#DataBinder.Eval(Container.DataItem, "TeamID")%>' />
                                                        <div style="padding: 5px;">
                                                            <asp:Label runat="server" ID="lblTeamName" Text='<%#DataBinder.Eval(Container.DataItem, "TeamName")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Players" ItemStyle-Width="45%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <table style="width: 98%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DataGrid runat="server" ID="dgPlayers" AutoGenerateColumns="false" Width="100%"
                                                                            OnDeleteCommand="dgPlayers_DeleteCommand" BorderStyle="Solid">
                                                                            <Columns>
                                                                                <asp:TemplateColumn>
                                                                                    <ItemTemplate>
                                                                                        <div style="padding: 5px;">
                                                                                            <asp:Label runat="server" ID="lblUserName" Text='<%#DataBinder.Eval(Container.DataItem, "Username")%>'></asp:Label>
                                                                                            <asp:HiddenField runat="server" ID="hdnTeamIDP" Value='<%#DataBinder.Eval(Container.DataItem, "TeamID")%>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" AlternateText="Delete Record" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td><asp:DropDownList runat="server" ID="ddlPlayers" CssClass="controls" BackColor="White"></asp:DropDownList></td>
                                                                                <td><asp:ImageButton runat="server" ID="btnAddPlayer" ImageUrl="~/Support/btnSmallAdd.jpg" CommandName="AddPlayer" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Rank" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:TextBox runat="server" ID="txtRank" CssClass="controls" BackColor="White" Width="40" Text='<%#DataBinder.Eval(Container.DataItem, "Rank")%>'></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Score" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:TextBox runat="server" ID="txtScore" CssClass="controls" BackColor="White" Width="40" Text='<%#DataBinder.Eval(Container.DataItem, "Score")%>'></asp:TextBox>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" AlternateText="Delete Record" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr runat="server" id="trSaveEventTeamScore">
                                <td class="GridRows">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 50%;">
                                                <table>
                                                    <tr>
                                                        <td><asp:DropDownList runat="server" ID="ddlTeams" CssClass="controls" BackColor="White"></asp:DropDownList></td>
                                                        <td><asp:ImageButton runat="server" ID="btnAddTeam" ImageUrl="~/Support/btnSmallAdd.jpg" OnClick="btnAddTeam_Click" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%; text-align: right;">
                                                <asp:ImageButton runat="server" ID="btnSaveEventTeamScore" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSaveEventTeamScore_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div style="text-align: center;">
                        <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
