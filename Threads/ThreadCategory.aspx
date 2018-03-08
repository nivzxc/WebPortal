<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ThreadCategory.aspx.cs" Inherits="Threads_ThreadCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <%--<div class="ChildPagePanel">
        <a href="../Default.aspx" class="SiteMap">Home</a> » 
        <a href="Forums.aspx" class="SiteMap">Forums</a> » 
		<asp:HyperLink runat="server" ID="lnkGroup" CssClass="SiteMap"></asp:HyperLink>
    </div>--%>
    <div class="ChildPagePanel" style="padding: 10px">
        <h2><asp:Literal runat="server" ID="litCategoryHeader"></asp:Literal></h2>
		<br />
        <br />
		<asp:Literal runat="server" ID="litCategory"></asp:Literal>
    </div>
</asp:Content>

