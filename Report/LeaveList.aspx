<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="LeaveList.aspx.cs" Inherits="Report_LeaveList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:ScriptManager runat="server" ID="smP">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr runat="server" id="trEncoder">
                    <td>
                        <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;">
                            <div class="GridBorder">
                                <table width="100%" cellpadding="5" cellspacing="1">
                                    <tr>
                                        <td class="masterpanel" style="width:100%;text-align:left">
                                            Leave Result</td>
                                    </tr>
                                    <asp:Label ID="lblPage" runat="server" Text="Page:"></asp:Label>
                                    <asp:Label ID="lblQueryResult" runat="server" Text="" Visible = "false"></asp:Label>
                                </table>
                                <div id="pagination" style="clear: both;">
                                    
                                    <asp:DropDownList ID="ddlPages" runat="server" CssClass="controls2" 
              AutoPostBack="True" onselectedindexchanged="ddlPages_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="text-align: center;">
						&nbsp;<asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
                            Text="Search Again" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

