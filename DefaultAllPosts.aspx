<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="DefaultAllPosts.aspx.cs" Inherits="DefaultAllPosts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server" />

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="ChildPagePanel">
        <a href="Default.aspx" class="SiteMap">Home</a> » 
        <a href="DefaultAllPosts.aspx" class="SiteMap">All Posts</a>
    </div>
	<asp:Literal runat="server" ID="litPostHome"></asp:Literal>
</asp:Content>

