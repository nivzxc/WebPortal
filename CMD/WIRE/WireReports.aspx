<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="WireReports.aspx.cs" Inherits="CMD_WIRE_WireReports" %>

<asp:Content ID="conWIRE" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="WIRE.aspx" class="SiteMap">WIRE</a> » 
     <a href="WireReports.aspx" class="SiteMap">Reports</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">WIRE Reports</span></b>
     <br />
     <br />
     <table>
      <tr>
       <td><img src="../../Support/star16.png" alt="" /></td>
       <td>&nbsp;<a href="rptNSIncDec.aspx?schltype=A" style="font-size:small;">NS Increase/Decrease Report</a></td>
      </tr>     
      <tr>
       <td><img src="../../Support/star16.png" alt="" /></td>
       <td>&nbsp;<a href="rptTopNS.aspx?schltype=A&schlown=A&sort=tyenr" style="font-size:small;">School Ranking Report</a></td>
      </tr>
      <tr>
       <td><img src="../../Support/star16.png" alt="" /></td>
       <td>&nbsp;<a href="rptDailyReport.aspx" style="font-size:small;">WIRE Daily Report</a></td>
      </tr>            
      <tr>
       <td><img src="../../Support/star16.png" alt="" /></td>
       <td>&nbsp;<a href="rptWIRESummary.aspx" style="font-size:small;">WIRE Summary Reports</a></td>
      </tr>
     </table>
    </div>
   </td>
  </tr>
 </table>
</asp:Content>
