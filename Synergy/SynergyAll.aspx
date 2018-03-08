<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="SynergyAll.aspx.cs" Inherits="Synergy_SynergyAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="ChildPagePanel">
<table width="100%" cellpadding="0" cellspacing="0">
		<%--<tr>
			<td>
				<div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
					<a href="../Default.aspx" class="SiteMap">Home</a> » 
					<a href="Synergy.aspx" class="SiteMap">Sports Fest</a> » 
					<a href="SynergyAll.aspx" class="SiteMap">A</a><a 
      href="SynergyAll.aspx">ll Announcements, News and Updates</a>
				</div>
			</td>
		</tr>
		<tr><td style="height: 9px;"></td></tr>--%>
	</table>
</div>
 <div class="ChildPagePanel">
  <h2>
   STI Synergy Games 2011-12 Announcements, News and Updates</h2>
  <div style="width:100%;height:100%;overflow:auto;">
   <asp:Literal runat="server" ID="litAnnouncements"></asp:Literal>
  </div>
 </div>
</asp:Content>

