<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true"
    CodeFile="EventNew.aspx.cs" Inherits="Synergy_EventNew" %>

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
                    <a href="EventNew.aspx" class="SiteMap">Event New</a>
                </div>
            </td>
        </tr>
        <tr><td style="height: 9px;"></td>
        </tr>
        <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <h2>Add New Event</h2>
                    <br />
                    <br />
                    <div class="GridBorder">
                        <table width="100%" cellpadding="3" cellspacing="1">
                            <tr><td colspan="2" class="GridText">&nbsp;<b>Event Details</b></td></tr>
                            <tr>
                                <td class="GridRows" style="width: 15%">Event Name:</td>
                                <td class="GridRows" style="width: 85%">
                                    <asp:TextBox runat="server" ID="txtEventName" CssClass="controls" Width="450px" MaxLength="50" BackColor="White"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="reqEventName" ControlToValidate="txtEventName" ErrorMessage="<br>[Event name is required]" Display="Dynamic" ValidationGroup="InsertEvent" />
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
                                    <asp:RequiredFieldValidator runat="server" ID="reqMaxPoints" ControlToValidate="txtMaxPoints" ErrorMessage="<br>[Maximum points is required]" Display="Dynamic" ValidationGroup="InsertEvent"></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator runat="server" ID="reqOrder" ControlToValidate="txtOrder" ErrorMessage="<br>[Display order is required]" Display="Dynamic" ValidationGroup="InsertEvent"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div style="text-align: center;">
                        <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" ValidationGroup="InsertEvent" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" OnClick="btnBack_Click" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>