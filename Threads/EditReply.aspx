<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="EditReply.aspx.cs" Inherits="Threads_EditReply" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <%--<tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <a href="../Default.aspx" class="SiteMap">Home</a> » Edit Reply
                </div>
            </td>
        </tr>--%>
 <%--       <tr><td style="height: 9px;"></td></tr>--%>
        <tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <h2>Edit Reply</h2>
                    <br />
                    <br />
                    <div class="GridBorder" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                        <table width="100%">
                            <tr>
                                <td>
                                    <CKEditor:CKEditorControl ID="ckeContents" runat="server" CssClass="controls" Height="300px" BackColor="White" Width="98%" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqContents" ControlToValidate="ckeContents" ErrorMessage="<br>Required"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                   <%-- <asp:ImageButton ImageUrl="~/Support/btnSaveChanges.jpg" runat="server" ID="btnSaveChanges" OnClick="btnSaveChanges_Click" />--%>
                                    <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" OnClick="btnSaveChanges_Click"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>