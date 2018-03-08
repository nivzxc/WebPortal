<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="Thread.aspx.cs" Inherits="Threads_Thread" %>
<%@Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
	<div class="ChildPagePanel">
        <a href="../Default.aspx" class="SiteMap">Home</a> » 
        <a href="Forums.aspx" class="SiteMap">Forums</a> »
        <asp:HyperLink runat="server" ID="lnkForumList" Font-Underline="true"></asp:HyperLink> »
        <asp:HyperLink runat="server" ID="lnkForumThread" Font-Underline="true"></asp:HyperLink>
	</div>
	<div class="ChildPagePanel">
		<div class="ChildPagePanel" style="background: aliceblue; font-size: small;">
			<h1><asp:Literal runat="server" ID="litThreadName"></asp:Literal></h1>
			By: <asp:HyperLink runat="server" ID="lnkCreatorName" Font-Size="Small"></asp:HyperLink>, <i><asp:Literal runat="server" ID="litPosition"></asp:Literal></i>
			<br />
			<asp:Literal runat="server" ID="litDatePosted"></asp:Literal>
			<br />
			<asp:HyperLink runat="server" ID="lnkEditThread" Text="[Edit Post Details]"></asp:HyperLink>
			<br />
			<asp:Literal runat="server" ID="litContent"></asp:Literal>
		</div>
		<br />
		<div class="ChildPagePanel" style="background: aliceblue; font-size: small;" runat="server" id="divAttachment" visible="false">
			Attachment: &nbsp;&nbsp;<asp:HyperLink runat="server" ID="lnkAttachment"></asp:HyperLink>
		</div>
		<br />
		<div runat="server" id="divReplies">
			<h2>Comments</h2>
			<br />
			<asp:Literal runat="server" ID="litReplies"></asp:Literal>	
			<br />
			<div class="ChildPagePanel" style="font-size: small; background: aliceblue;">		
				<asp:Literal runat="server" ID="litPaging"></asp:Literal>
			</div>
			<b><span class="HeaderText">Post a comment</span></b>
			<br />
			<br />
			<div>
				<CKEditor:CKEditorControl ID="ckeReply" runat="server" CssClass="controls" Height="300px" BackColor="White" Width="98%" />
			</div>
			<br />
			<div style="text-align: center;">
				<asp:ImageButton ImageUrl="~/Support/btnPostReply.jpg" runat="server" ID="btnPostReply"	OnClick="btnPostReply_Click" />
			</div>
		</div>
	</div>
	<asp:HiddenField runat="server" ID="hdnIsPrivate" />
</asp:Content>
