<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ThreadList.aspx.cs" Inherits="Threads_ThreadList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 646px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <%--<tr>
            <td>
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <a href="../Default.aspx" class="SiteMap">Home</a> » 
                    <a href="Forums.aspx" class="SiteMap">Forums</a> »
                    <asp:HyperLink runat="server" ID="lnkForumCategory" Font-Underline="true"></asp:HyperLink> » 
                    <asp:HyperLink runat="server" ID="lnkForumThread" Font-Underline="true"></asp:HyperLink>
                </div>
            </td>
        </tr>--%>
<%--        <tr><td style="height: 9px;"></td></tr>--%>
        <tr>
            <td class="style1">
                <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                    <table width="100%" border="0">
                        <tr><td><h2><asp:Literal runat="server" ID="litCategoryName"></asp:Literal></h2></td></tr>
                        
                        <tr>
                            <td>
                                <div class="GridBorder">
                                    <!-- View all records -->
                                    <table width="100%" cellpadding="5" class="grid">
                                        <tr>
                                            <%--<td colspan="2" align="center" class="GridText">
                                                <table>
                                                    <tr>
                                                        <td><img src="../Support/AppHead.png" alt="" /></td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>--%>
                                            
                                        </tr>
                                        
                                        <tr>
                                            <td class="GridColumns"><b>List of Policies &amp; Procedures</b></td>
                                        </tr>
                                        <asp:Literal runat="server" ID="litThreads"></asp:Literal>
                                        <tr>
                                            <td class="BrowseAll" style="font-size: small; text-align: left;">
                                                &nbsp;Page<asp:Literal runat="server" ID="litPaging"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>
                            <asp:Button ID="btnNew" runat="server" Text="New Thread" OnClick="btnNew_Click"/><%--<asp:ImageButton runat="server" ID="btnNew" ImageUrl="~/Support/btnNewThread.jpg" OnClick="btnNew_Click" />--%></td></tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
