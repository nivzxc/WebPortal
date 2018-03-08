<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EmployeeJournalEncode.aspx.cs" Inherits="EmployeeJournal_EmployeeJournalEncode" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:ScriptManager runat="server" ID="smThreadNew">
    </asp:ScriptManager>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;
                            padding-bottom: 10px;">
                            <div runat="server" id="divError" class="ErrMsg" visible="false">
                                <b>Error during update. Please correct your data entries:</b>
                                <br />
                                <br />
                                <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
                                <br />
                            </div>
                            <br />
                            <div class="">
                                <table width="100%" cellpadding="3" cellspacing="0" class="Grid">
                                    <tr>
                                        <td class="masterpanel">
                                            &nbsp;<b>Weekly Journal</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="GridRows" style="vertical-align: top">
                                            <asp:Label ID="lblWeekDates" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="GridRows">
                                            <CKEditor:CKEditorControl ID="ckeContents" runat="server" CssClass="controls" Height="300px"
                                                BackColor="White" Width="98%" ToolbarFull="Cut|Copy|Paste|PasteText|PasteFromWord|-|SpellChecker|Scayt
Undo|Redo|-|Find|Replace|-|Bold|Italic|Underline|Strike
Image|Table|HorizontalRule|Smiley|SpecialChar
/
NumberedList|BulletedList|-|Outdent|Indent|Blockquote|CreateDiv
JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock
Link|Unlink|Anchor
TextColor|BGColor
Subscript|Superscript
/
Styles|Format|Font|FontSize" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <asp:HiddenField ID="hndGroupUpdateCode" runat="server" />
                            <asp:HiddenField ID="hndIsPreview" runat="server" />
                            <div style="text-align: center;">
                                <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" 
                                    ValidationGroup="SaveRecord" onclick="btnSaveAsDraft_Click"  />
                                <asp:Button ID="btnPreview" runat="server" Text="Preview" ValidationGroup="SaveRecord"
                                    Visible="False" />
                                &nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="Submit" 
                                    ValidationGroup="SaveRecord" Visible="False" onclick="btnSave_Click" />
                                &nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Back" />&nbsp;
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
</asp:Content>
