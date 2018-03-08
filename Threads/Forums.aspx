<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="Forums.aspx.cs" Inherits="Threads_Forums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
   <%-- <div class="ChildPagePanel">
        <a href="../Default.aspx" class="SiteMap">Home</a> » 
        <a href="Forums.aspx" class="SiteMap">Forums</a>
    </div>--%>
    <div class="ChildPagePanel" runat="server" id="divControlPanel" visible="false" 
        style="padding: 10px">
        <h2>Forum Control Panel</h2>
		<br />
        <br />
		<div class="ChildPagePanelBlue" style="font-size: small;">
			<a href="ThreadTypes.aspx">Thread Type Assignment</a>
		</div>
		<div class="ChildPagePanelBlue" style="font-size: small;">
			<a href="Reports.aspx">Thread Reports</a>
		</div>
    </div>
    <div class="ChildPagePanel" style="padding: 10px">
        <h2>Forum Groups</h2>
		<br />
        <br />
		<asp:Literal runat="server" ID="litForums"></asp:Literal>
    </div>
</asp:Content>