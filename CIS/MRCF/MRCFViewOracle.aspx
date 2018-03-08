<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="MRCFViewOracle.aspx.cs" Inherits="CIS_MRCF_MRCFViewOracle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="MRCFMenu.aspx" class="SiteMap">MRCF</a> » 
     <a href="MRCFViewOracle.aspx" class="SiteMap">View MRCF to Oracle</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>--%>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="5" align="center" class="">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>List of Approved MRCF for Oracle Import</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:10%;"><b>Batch</b></td>
        <td class="GridColumns" style="width:15%;"><b>MRCF #</b></td>
        <td class="GridColumns" style="width:30%;"><b>Intended For</b></td>
        <td class="GridColumns" style="width:15%;"><b>Requestor</b></td>
        <td class="GridColumns" style="width:20%;"><b>Date Requested</b></td>
       </tr>
       <% LoadMRCFForImport();%>
      </table>           
     </div>
    </div>     
   </td>
  </tr> 
<%--   <tr><td style="height:9px;"></td></tr>
     <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <br />
     <div class="GridBorder"> 	          
      <table width="70%" cellpadding="5" class="grid">
       <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>List of Unsuccessful MRCF to Oracle Import</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:40%;"><b>Batch Number</b></td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Number</b></td>
       </tr>
       <% LoadMRCFError();%>
      </table>           
     </div>
    </div>     
   </td>
  </tr> --%>
 </table>
</asp:Content>

