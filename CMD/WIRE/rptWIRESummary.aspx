<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="rptWIRESummary.aspx.cs" Inherits="CMD_WIRE_rptWIRESummary" %>

<asp:Content ID="conWIRE" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="WIRE.aspx" class="SiteMap">WIRE</a> » 
     <a href="WireReports.aspx" class="SiteMap">Reports</a> » 
     <a href="rptWIRESummary.aspx" class="SiteMap">WIRE Summary Report</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">WIRE Summary Report</span></b>
     <br />
     <br />
			  <div style="border-right:olivedrab 1px solid; padding-right:10px; border-top:olivedrab 1px solid; padding-left:10px; font-size:xx-small; left:20px; padding-bottom:10px; border-left:olivedrab 1px solid; width:480px; padding-top: 10px; border-bottom:olivedrab 1px solid; font-family:verdana; top:150px; background-color:#edfff2">
			   <table>
			    <tr>
			     <td><b>Schools:</b></td>
			     <td>&nbsp;<asp:DropDownList Runat="server" ID="ddlSchools" AutoPostBack="True" CssClass="controls" BackColor="White"></asp:DropDownList></td>
			    </tr>  
			   </table>
			  </div>
			             
			  <br />

     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr><td colspan="5" align="center" class="GridText">&nbsp;<b>New Students</b></td></tr>
			    <tr>
        <td class="GridColumns"><b>Programs</b></td>
        <td class="GridColumns"><b>Inquiries</b></td>
        <td class="GridColumns"><b>Registrants</b></td>
        <td class="GridColumns"><b>Enrollees</b></td>
        <td class="GridColumns"><b>Inq - Enr Conversion</b></td>
       </tr>
 <%--      <% Load_NS(); %>--%>
      </table>
     </div>			  
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">        
			    <tr>
			     <td colspan="4" class="GridText">&nbsp;<b>OLD STUDENTS</b></td>
       </tr>
			    <tr>
        <td class="GridColumns"><b>Programs</b></td>
        <td class="GridColumns"><b>Inquiries</b></td>
        <td class="GridColumns"><b>Registrants</b></td>
        <td class="GridColumns"><b>Enrollees</b></td>
       </tr>
		<%--	    <%	Load_OS(); %>--%>
      </table>       
     </div>   
     
     <br />
     
     <div style="text-align:center">
      <asp:ImageButton runat="server" ID="btnExportExcel" ImageUrl="~/Support/btnExportToExcel.jpg" />
     </div>  
      
    </div>
   </td>
  </tr>
 </table>
</asp:Content>