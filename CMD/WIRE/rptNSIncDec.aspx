<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="rptNSIncDec.aspx.cs" Inherits="CMD_WIRE_rptNSIncDec" %>

<asp:Content ID="conWIRE" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="WIRE.aspx" class="SiteMap">WIRE</a> » 
     <a href="WireReports.aspx" class="SiteMap">Reports</a> » 
     <a href="rptNsIncDec.aspx?schltype=A" class="SiteMap">NS Increase/Decrease Report</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">NS Increase/Decrease Report</span></b>
     <br />
     <br />
     <div class="GridBorder" style="width:99%">
      <table cellpadding="5" class="grid" width="100%">
       <tr><td colspan="5" align="center" class="GridText">&nbsp;<b>Most Improved Schools (College Category)</b></td></tr>
       <tr>
        <td class="GridColumns">&nbsp;</td>
        <td class="GridColumns"><b>School Name</b></td>
        <td class="GridColumns"><b>TY</b></td>
        <td class="GridColumns"><b>LY</b></td>
        <td class="GridColumns"><b>Inc/Dec</b></td>
       </tr>
			 <%--   <% LoadNSInqDec(); %>--%>
      </table>
     </div>           
    </div>
   </td>
  </tr>
 </table>
</asp:Content>
