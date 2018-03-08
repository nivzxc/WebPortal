<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="SERMain.aspx.cs" Inherits="CMD_SER_SERMain" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">CMD</a> » 
     <a href="SERMain.aspx" class="SiteMap">SER</a>    
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>

  <tr runat="server" id="trSERReports" visible="false">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Summary of Enrollment Report</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="2">
       <tr>
        <td class="GridColumns"><b>Report</b></td>
        <td class="GridColumns"><b>Downlaod</b></td>
       </tr>
       <tr>
        <td class="GridRows">&nbsp;<a href="rptSemestralCourses.aspx" style="font-size:small;">Semestral Courses Report</a></td>
        <td class="GridRows" style="text-align:center;"><a href="../../UploadedFiles/SER/SemestralCourses0910.xls"><img src="../../Support/excel22.png" alt="Download Excel File" /></a></td>
       </tr>
      </table>
     </div>     
    </div>
   </td>
  </tr>
     
 </table>
 </asp:Content>