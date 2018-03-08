<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="GroupUpdatePreview.aspx.cs" Inherits="GroupUpdate_GroupUpdatePreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div id="site">
	<div class="center-wrapper">

		<div id="headerNews">

			<div id="navigation">
				
				<div id="main-nav">

					<ul class="tabbed">
						<% LoadTab(); %>
					</ul>

					<div class="clearer">&nbsp;</div>

				</div>

			</div>

		</div>

		<div class="main" id="main-two-columns">

			<div class="left" id="main-left">
				<div class="post">
					<div class="post-body">
                        <asp:Literal runat="server" ID="litContent"></asp:Literal>
					</div>
				
				</div>

				<div class="content-separator"></div>
			</div>

			<div class="right sidebar" id="sidebar">

				<div class="section">

					<div class="section-title">Divisional News</div>

					<div class="section-content">

						<ul class="nice-list">
							<%  LoadDivisionNews(); %>
  
						</ul>
						
					</div>

				</div>

                <div class="section">

					<div class="section-title">Latest News</div>

					<div class="section-content">

						<ul class="nice-list">
                           <% LoadLatestNews(); %>
                           <%--<li><a href="GroupUpdateQuery.aspx" class="more">Browse all &#187;</a></li>--%>
						</ul>

					</div>
				</div>
                <%--<div class="sectionSearch">

					<div class="section-title"><ul class="listingting">
                           <li><a href="GroupUpdateQuery.aspx" class="more">Search Group News &#187;</a></li>
						</ul></div>

				</div>--%>
			</div>
			<div class="clearer">&nbsp;</div>

		</div>
	</div>
</div>
</asp:Content>

