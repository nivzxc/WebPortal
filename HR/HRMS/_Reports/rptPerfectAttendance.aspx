<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptPerfectAttendance.aspx.cs" Inherits="HR_HRMS_Reports_rptPerfectAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   <%-- <div class="ChildPagePanel">
		<a href="../../../Default.aspx" class="SiteMap">Home</a> » 
		<a href="#" class="SiteMap">CIS</a> » 
		<a href="../../HR.aspx" class="SiteMap">HR</a> » 
		<a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
		<a href="rptPerfectAttendance.aspx" class="SiteMap">Perfect Attendance Report</a>
    </div>
   
     
     <br />--%>
	<div class="ChildPagePanel">
		<h2>Perfect Attendance Report</h2>
		<br />
<%--       <div class="GridBorder" style="width:500px;"> 	          
        <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows" rowspan="2">Level:</td>
        <td class="GridRows" rowspan="2"><asp:DropDownList runat="server" ID="ddlLevel" 
                CssClass="controls"></asp:DropDownList></td>
        <td class="GridRows" rowspan="2"><asp:ImageButton runat="server" ID="btnSearch" 
                ImageUrl="~/Support/btnSearch.jpg" onclick="btnSearch_Click" /></td>
       </tr>
      </table>
     </div>--%>
		<br />
		<asp:Literal runat="server" ID="litReport"></asp:Literal>
	</div>
</asp:Content>

