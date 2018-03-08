<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EventMenu.aspx.cs" Inherits="Synergy_EventMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
       <%-- <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <a href="../Default.aspx" class="SiteMap">Home</a> » 
                    <a href="Synergy.aspx" class="SiteMap">Sports Fest</a> » 
                    <a href="EventMenu.aspx" class="SiteMap">Events</a>
                </div>
            </td>
        </tr>
        <tr><td style="height: 9px;"></td></tr>--%>
        <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <h2>Events List</h2>
                   
                    <div class="GridBorder">
                        <table width="100%" cellpadding="0">
          <%--                  <tr><td class="GridColumns">&nbsp;&nbsp;<b>List of Events</b></td></tr>--%>
                            <tr>
                                <td class="GridRows">
                                    <div class="GridBorder">
                                        <asp:DataGrid runat="server" ID="dgEventMenu" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid">
                                            <HeaderStyle Font-Bold="true" Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
                                            <ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
                                            <AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <a href='EventDetails.aspx?eventid=<%#DataBinder.Eval(Container.DataItem, "EventID")%>'>
                                                                <img src="../Support/fileopen22.png" alt="[View Record]" />
                                                            </a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Event Name" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:HiddenField runat="server" ID="hdnEventCode" Value='<%#DataBinder.Eval(Container.DataItem, "EventID")%>' />
                                                            <a href='EventDetails.aspx?eventid=<%#DataBinder.Eval(Container.DataItem, "EventID")%>'><%#DataBinder.Eval(Container.DataItem, "Name")%></a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Division" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:Label runat="server" ID="lblDivision" Text='<%#DataBinder.Eval(Container.DataItem, "EventDivisionName")%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Category" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:Label runat="server" ID="lblCategory" Text='<%#DataBinder.Eval(Container.DataItem, "EventCategoryName")%>' />
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
                                                            <asp:Image runat="server" ID="imgWinner" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "WinnerTeamLogo")%>' />
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
                        <asp:ImageButton runat="server" ID="btnNewRecord" ImageUrl="~/Support/btnNewEvent.jpg" OnClick="btnNewRecord_Click" />
                        &nbsp;
                        <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
