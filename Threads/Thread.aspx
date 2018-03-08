<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="Thread.aspx.cs" Inherits="Threads_Thread" %>
<%@Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor"%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">	
    <div class="ChildPagePanel" style="padding: 10px">
		<div class="ChildPagePanel" font-size: small;">
			<h1 style="margin-bottom: 0px; float: left; width: 1027px;"><asp:Literal runat="server" ID="litThreadName"></asp:Literal>
            <div style="float: right; width: 78px; height: 33px; clip: rect(3px, auto, auto, auto); font-size: 5pt;">
                <br />
                <asp:Button 
                    ID="btnBack" runat="server" CausesValidation="False" Height="22px" 
                    onclick="btnBack_Click" Text="Back" Width="81px" /></div>
            </h1>
            <br />
            <div style="float: left; width: 1061px;">
                By: <asp:HyperLink runat="server" ID="lnkCreatorName" Font-Size="Small"></asp:HyperLink>, <i><asp:Literal runat="server" ID="litPosition"></asp:Literal>
            
                </i>
			<br />
			<asp:Literal runat="server" ID="litDatePosted"></asp:Literal>
			<br />
			<asp:HyperLink runat="server" ID="lnkEditThread" Text="[Edit Post Details]"></asp:HyperLink>
			<br />
			<asp:Literal runat="server" ID="litContent"></asp:Literal>
		        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		</div>
		<br />
		<div class="ChildPagePanel" style="background: aliceblue; font-size: small;" runat="server" id="divAttachment" visible="false">
			Attachment: &nbsp;&nbsp;<asp:HyperLink runat="server" ID="lnkAttachment"></asp:HyperLink>
		</div>
		<br />
	</div>
	<asp:HiddenField runat="server" ID="hdnIsPrivate" />
</asp:Content>
