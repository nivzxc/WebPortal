<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/TwoPanel.master" CodeFile="rptDailyReport.aspx.cs" Inherits="CMD_WIRE_rptDailyReport" %>

<asp:Content ID="conWIRE" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="WIRE.aspx" class="SiteMap">WIRE</a> » 
     <a href="WireReports.aspx" class="SiteMap">Reports</a> » 
     <a href="rptTopNS.aspx?schltype=A&schlown=A&sort=tyenr" class="SiteMap">CM Daily Report</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Channel Management Daily Report</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="14" class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>
            <table>
             <tr>
              <td>&nbsp;<img src="../../Support/viewtext22.png" alt="" /></td>
              <td>&nbsp;<b>Channel Manager Report</b></td>
             </tr>
            </table>           
           </td>
           <td style="text-align:right;"><asp:ImageButton runat="server" ID="btnExport" ImageUrl="~/Support/btnExportToExcel.jpg" onclick="btnExport_Click" /></td>
          </tr>
         </table>            
        </td>
       </tr>      
       <tr>
        <td class="GridText" rowspan="2" style="font-size:x-small;text-align:center;"><b>School Name</b></td>
        <td class="GridColumns" colspan="3"><b>CHED Students</b></td>
        <td class="GridColumns" colspan="3"><b>TESDA Students</b></td>
        <td class="GridColumns" colspan="3"><b>NS TY</b></td>
        <td class="GridColumns" colspan="3"><b>NS LY</b></td>
        <td class="GridText" rowspan="2" style="font-size:x-small;text-align:center;"><b>Last Update</b></td>
       </tr>
		     <tr>
			     <td class="GridColumns" style="width:40px"><b>INQ</b></td>
			     <td class="GridColumns" style="width:40px"><b>REG</b></td>
			     <td class="GridColumns" style="width:40px"><b>ENR</b></td>
			     <td class="GridColumns" style="width:40px"><b>INQ</b></td>
			     <td class="GridColumns" style="width:40px"><b>REG</b></td>
			     <td class="GridColumns" style="width:40px"><b>ENR</b></td>
			     <td class="GridColumns" style="width:40px"><b>INQ</b></td>
			     <td class="GridColumns" style="width:40px"><b>REG</b></td>
			     <td class="GridColumns" style="width:40px"><b>ENR</b></td>
			     <td class="GridColumns" style="width:40px"><b>INQ</b></td>
			     <td class="GridColumns" style="width:40px"><b>REG</b></td>
			     <td class="GridColumns" style="width:40px"><b>ENR</b></td>
		     </tr>
		<%--     <% LoadReports(); %>    --%>    
      </table>
     </div>           
    </div>
   </td>
  </tr>
 </table>
</asp:Content>