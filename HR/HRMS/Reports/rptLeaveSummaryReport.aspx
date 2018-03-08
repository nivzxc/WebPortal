<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptLeaveSummaryReport.aspx.cs" Inherits="HR_HRMS_Reports_rptLeaveSummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
     <div class="ChildPagePanel">
		<a href="../../../Default.aspx" class="SiteMap">Home</a> » 
		<a href="#" class="SiteMap">CIS</a> » 
		<a href="../../HR.aspx" class="SiteMap">HR</a> » 
		<a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
		<a href="rptLeaveSummaryReport.aspx" class="SiteMap">Leave Summary Report</a>
    </div>
	<div class="ChildPagePanel">
		<h2>Leave Summary Report</h2>
		<br />
          
        <div style="float: right; width: 78px;">
            <%--<asp:ImageButton runat="server" ID="btnBack0" ImageUrl="~/Support/btnBack.jpg" 
                onclick="btnBack_Click" />--%><asp:Button ID="btnBack0" runat="server" Text="Back" onclick="btnBack_Click" />
        </div>
        Fiscal Year: &nbsp;<asp:DropDownList ID="ddlFiscalYear" runat="server" 
            AutoPostBack="True">
            <asp:ListItem>2009-2010</asp:ListItem>
            <asp:ListItem>2011-2012</asp:ListItem>
            <asp:ListItem>2012-2013</asp:ListItem>
            <asp:ListItem>2013-2014</asp:ListItem>
            <asp:ListItem>2014-2015</asp:ListItem>
            <asp:ListItem>2015-2016</asp:ListItem>
            <asp:ListItem>2016-2017</asp:ListItem>
            <asp:ListItem>2017-2018</asp:ListItem>
            <asp:ListItem Selected="True">2018-2019</asp:ListItem>
        </asp:DropDownList>
        <br />
        
		<br />
		<asp:Literal runat="server" ID="litReport"></asp:Literal>
	    <br />
        <br />
          
        <div style="float: none; width: inherit;" align="center">
      
            <br />
        </div>

        <div class="panel panel-info">
            <div class="panel-heading">
                <h4>LEAVE SUMMARY REPORT</h4>
             </div>
            <div class="panel-info">
                <table class="table-striped" width="100%">          
                       <tr style="text-align:center; background-color:;">
                            <th style="text-align:center;">Division</th>
                            <th style="text-align:center;">Employee</th>
                            <th colspan="2">Total
                                <table class="table table-striped">
                                    <tr>
                                        <th>Count</th>
                                        <th>Day(s)</th>
                                   </tr>                                   
                                </table>
                            </th>                            
                        </tr>                                                                              
                
                        <tr>
                            <td style="text-align:center;">test1</td>
                            <td style="text-align:center;">test2</td>
                            <td>test3</td>
                            <td>test4</td>
                        </tr>
                   
                </table>

            </div>
        </div>
	</div>
</asp:Content>

