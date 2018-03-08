<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="WIRE.aspx.cs" Inherits="CMD_WIRE_WIRE" %>
<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>

<asp:Content ID="conWIRE" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="WIRE.aspx" class="SiteMap">WIRE</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>

  <%
  if (clsWIRE.IsUser(clsWIRE.WireUsers.EliteUsers,Request.Cookies["Speedo"]["UserName"].ToString()))
  {
  %>      
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <span class="HeaderText" style="font-size:medium;"><b>WIRE System</b> v3.00</span>
     <br />
     &nbsp;<span class="HeaderText">School Year:<%Response.Write(clsWIRE.SchoolYear); %> Semester:<%Response.Write(clsWIRE.Semester); %></span>
     <br />
     <br />
     <a href="WireReports.aspx" style="font-size:small;">[View All Reports]</a>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid" style="font-size:small;">
       <tr><td class="GridText" colspan="3">&nbsp;<b>Enrollees Comparison</b></td></tr>
       <tr>
	    	  <td class="GridText">&nbsp;</td>
  	    	<td class="GridText" style="text-align:center;width:40%;"><b>TY as of today <% Response.Write(DateTime.Now.ToString("hh:mm tt")); %></b></td>
								<td class="GridText" style="text-align:center;width:40%;"><b>LY as of <asp:Label runat="server" ID="lblLyAsOf"></asp:Label></b></td>
							</tr>
							<tr>
							 <td class="GridRows" style="font-size:large;color:royalblue;text-align:center;width:10%">Enr:</td>
							 <td class="GridRows" style="font-size:xx-large;color:royalblue;text-align:center"><b><asp:Label runat="server" ID="lblTyEnr"></asp:Label></b></td>
								<td class="GridRows" style="font-size:xx-large;color:crimson;text-align:center"><b><asp:Label runat="server" ID="lblLyEnr"></asp:Label></b></td>
							</tr>
							<tr>
								<td class="GridRows" style="font-size:large;color:royalblue;text-align:center;width:10%">Reg:</td>
								<td class="GridRows" style="font-size:xx-large;color:royalblue;text-align:center"><b><asp:Label runat="server" ID="lblTyReg"></asp:Label></b></td>
								<td class="GridRows" style="font-size:xx-large;color:crimson;text-align:center"><b><asp:Label runat="server" ID="lblLyReg"></asp:Label></b></td>
							</tr>
						 <tr><td class="GridColumns" colspan="3" style="font-size:small;">There is a <b><asp:Label runat="server" ID="lblIncDec"></asp:Label></b> in this year's NS compared to last year's NS.</td></tr>
						</table>
						<table width="100%" cellpadding="5" class="Grid" style="font-size:small;">
						 <tr><td class="GridRows" colspan="3"><a href='#' onclick="window.open('SubRate.aspx',null,'height=500,width=500,status=yes,toolbar=no,menubar=no,location=no,scrollbars=1')"><b>Schools Submission Rate</b></a></td></tr>
						 <tr>
						  <td class="GridRows" style="text-align:center; width:33%"><asp:Label runat="server" ID="lblSchlSubmit1"></asp:Label></td>
				    <td class="GridRows" style="text-align:center; width:33%"><asp:Label runat="server" ID="lblSchlSubmit2"></asp:Label></td>
				    <td class="GridRows" style="text-align:center; width:33%"><asp:Label runat="server" ID="lblSchlSubmit3"></asp:Label></td>
				   </tr>
				  </table>
     </div>
     
     <br />
     
     <div class="GridBorder">  
			   <table width="100%" cellpadding="5" class="grid">
			    <tr><td class="GridText" style="text-align:center;" colspan="5"><b>WIRE Statistics</b></td></tr>
			    <tr>
			     <td class="GridColumns"><b>Date Encoded</b></td>
			     <td class="GridColumns" style="width:90px;"><b>Inquiries</b></td>
			     <td class="GridColumns" style="width:90px;"><b>Registrants</b></td>
			     <td class="GridColumns" style="width:90px;"><b>Enrollees</b></td>
			     <td class="GridColumns" style="width:90px;"><b>Last Year</b></td>
			    </tr>
			    <tr>
			     <td class="GridRows2">&nbsp;Overall Total:</td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblOTInq"></asp:Label></td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblOTReg"></asp:Label></td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblOTEnr"></asp:Label></td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblOTLy"></asp:Label></td>
			    </tr>
			    <tr>
			     <td class="GridRows2">&nbsp;This Week's Total:</td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblTyTwInq"></asp:Label></td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblTyTwReg"></asp:Label></td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblTyTwEnr"></asp:Label></td>
			     <td class="GridRowsNum2"><asp:Label runat="server" ID="lblLyTwEnr"></asp:Label></td>
			    </tr>
			    <tr>
			     <td class="GridRows">&nbsp;Monday:</td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwInq1"></asp:Label></td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwReg1"></asp:Label></td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwEnr1"></asp:Label></td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblLyTwEnr1"></asp:Label></td>
			    </tr>
			    <tr>
			     <td class="GridRows">&nbsp;Tuesday:</td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwInq2"></asp:Label></td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwReg2"></asp:Label></td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwEnr2"></asp:Label></td>
			     <td class="GridRowsNum"><asp:Label runat="server" ID="lblLyTwEnr2"></asp:Label></td>
		     </tr>
		     <tr>
		      <td class="GridRows">&nbsp;Wednesday:</td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwInq3"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwReg3"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwEnr3"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblLyTwEnr3"></asp:Label></td>
		     </tr>
		     <tr>
		      <td class="GridRows">&nbsp;Thursday:</td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwInq4"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwReg4"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwEnr4"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblLyTwEnr4"></asp:Label></td>
		     </tr>
		     <tr>
		      <td class="GridRows">&nbsp;Friday:</td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwInq5"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwReg5"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwEnr5"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblLyTwEnr5"></asp:Label></td>
		     </tr>
		     <tr>
		      <td class="GridRows">&nbsp;Saturday:</td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwInq6"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwReg6"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblTyTwEnr6"></asp:Label></td>
		      <td class="GridRowsNum"><asp:Label runat="server" ID="lblLyTwEnr6"></asp:Label></td>
		     </tr>
		     <tr>
		      <td class="GridRows2">&nbsp;Last Week"s Total:</td>
		      <td class="GridRowsNum2"><asp:Label runat="server" ID="lblTyLwInq"></asp:Label></td>
		      <td class="GridRowsNum2"><asp:Label runat="server" ID="lblTyLwReg"></asp:Label></td>
		      <td class="GridRowsNum2"><asp:Label runat="server" ID="lblTyLwEnr"></asp:Label></td>
		      <td class="GridRowsNum2"><asp:Label runat="server" ID="lblLyLwEnr"></asp:Label></td>
		     </tr>
		    </table>
		   </div>
    
     <br />
     
     <div style="text-align:center;">
      <web:chartcontrol id="chaNat" runat="server" borderstyle="Outset" borderwidth="1px" height="400px" width="600px" ChartFormat="Jpg" LeftChartPadding="20" YValuesInterval="100" YCustomEnd="1000" YCustomStart="0" BottomChartPadding="9" Padding="9" GridLines="Both" Border-Color="cornflowerblue">
       <ChartTitle Font="Verdana,10pt,style=Bold" ForeColor="white" Text="This Year NS vs Last Year NS By Area Comparison"></ChartTitle>
       <Background Color="SkyBlue" Type="LinearGradient" EndPoint="900,900"></Background>
       <YAxisFont StringFormat="Far,Near,Character,LineLimit"></YAxisFont>
       <XTitle StringFormat="Center,Near,Character,LineLimit"></XTitle>         
       <XAxisFont StringFormat="Center,Near,Character,LineLimit"></XAxisFont>         
       <YTitle StringFormat="Center,Near,Character,LineLimit"></YTitle>
       <Legend Position="Bottom" Width="30">
        <Background CenterColor="LightYellow" />
       </Legend>
      </web:chartcontrol>
      
      <br />
      
      <div class="GridBorder" style="width:600px;">
       <table width="100%" cellpadding="5" class="grid">
        <tr>
         <td class="GridColumns" style="width:15%;">&nbsp;</td>
         <td class="GridColumns" style="width:17%;"><b>Metro Manila</b></td>
         <td class="GridColumns" style="width:17%;"><b>Northern Luzon</b></td>
         <td class="GridColumns" style="width:17%;"><b>Southern Luzon</b></td>
         <td class="GridColumns" style="width:17%;"><b>Visayas</b></td>
         <td class="GridColumns" style="width:17%;"><b>Mindanao</b></td>
        </tr>      
        <asp:Label runat="server" ID="lblChart"></asp:Label>
       </table>
      </div>
      
      <br />
      
      <div class="GridBorder" style="width:100%">
       <table width="100%" cellpadding="5" class="grid">
        <tr>
         <td colspan="5" align="center" class="GridText">&nbsp;<b>TOP 5 NS Ranking (College Category)</b></td>
        </tr>
        <tr>
         <td class="GridColumns"><b>School Name</b></td>
         <td class="GridColumns"><b>REG</b></td>
         <td class="GridColumns"><b>TY</b></td>
         <td class="GridColumns"><b>LY</b></td>
         <td class="GridColumns"><b>Inc/Dec</b></td>
        </tr>
			    <%-- <% LoadTop5NSCollege();%>--%>
			     <tr><td colspan="5" class="GridColumns"><a href="rptTopNS.aspx?schltype=C&schlown=A&sort=tyenr" style="font-size:small;">More Details (School Ranking Report)</a></td></tr>
       </table>
      </div>
      
      <br />
      
      <div class="GridBorder" style="width:100%">
       <table width="100%" cellpadding="5" class="grid">
        <tr><td colspan="5" align="center" class="GridText">&nbsp;<b>TOP 5 NS Ranking (EC Category)</b></td></tr>
        <tr>
         <td class="GridColumns"><b>School Name</b></td>
         <td class="GridColumns"><b>REG</b></td>
         <td class="GridColumns"><b>TY</b></td>
         <td class="GridColumns"><b>LY</b></td>
         <td class="GridColumns"><b>Inc/Dec</b></td>
        </tr>
			    <%-- <% LoadTop5NSEC();%>  --%>      
			     <tr><td colspan="5" class="GridColumns"><a href="rptTopNS.aspx?schltype=E&schlown=A&sort=tyenr" style="font-size:small;">More Details (School Ranking Report)</a></td></tr>
       </table>
      </div>
      
      <br />
      
      <div class="GridBorder" style="width:100%">
       <table width="100%" cellpadding="5" class="grid">
        <tr><td colspan="5" align="center" class="GridText">&nbsp;<b>Most Improved Schools (College Category)</b></td></tr>
        <tr>
         <td class="GridColumns"><b>School Name</b></td>
         <td class="GridColumns"><b>TY</b></td>
         <td class="GridColumns"><b>LY</b></td>
         <td class="GridColumns"><b>Inc/Dec</b></td>
        </tr>
		<%--	     <% LoadMostImprovedSchoolCollege();%>  --%>      
			     <tr><td colspan="5" class="GridColumns"><a href="rptNSIncDec.aspx?schltype=C" style="font-size:small;">More Details (Schools NS Growth Report)</a></td></tr>
       </table>
      </div>      
      
      <br />
      
      <div class="GridBorder" style="width:100%">
       <table width="100%" cellpadding="5" class="grid">
        <tr><td colspan="5" align="center" class="GridText">&nbsp;<b>Most Improved Schools (EC Category)</b></td></tr>
        <tr>
         <td class="GridColumns"><b>School Name</b></td>
         <td class="GridColumns"><b>TY</b></td>
         <td class="GridColumns"><b>LY</b></td>
         <td class="GridColumns"><b>Inc/Dec</b></td>
        </tr>
			<%--     <% LoadMostImprovedSchoolEC();%>  --%>      
			     <tr><td colspan="5" class="GridColumns"><a href="rptNSIncDec.aspx?schltype=E" style="font-size:small;">More Details (Schools NS Growth Report)</a></td></tr>
       </table>
      </div>      
             
     </div>            
    </div>     
    </td>
   </tr>
   <%
   }
   %>
     
 </table>  
</asp:Content>