<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin_AdministrationSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<%--<div class="border">
<table>
    <tr>
        <td><a href="ApproverSettingsMRCF.aspx">MRCF Approver Settings</a></td>
    </tr>

    <tr>
         <td><a href="ApproverSettingsRequ.aspx">Requisition Approver Settings</a></td>
    </tr>

    <tr>
        <td><a href="SpeedoModules.aspx">Module Settings</a></td>
    </tr>

    <tr>
        <td><a href="SpeedoModuleApprover.aspx">Module Approver Settings</a></td>
    </tr>

</table>
</div>--%>

 <table width="100%" cellpadding="0" cellspacing="0">
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> »&nbsp; 
     <a href="Admin.aspx" class="SiteMap">Administration</a>     
    </div>        
   </td>
  </tr>
--%>
    <tr runat="server" id="trManagementReportsSpace" visible="false"><td style="height:9px;"></td></tr>
  <tr runat="server" id="trManagementReports" visible="true">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Administration Settings</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="5">
       <tr><td class="GridColumns" style="text-align:left; font-size:small;">&nbsp;<b>Approver Settings</b></td></tr>

       <tr><td id="moduleList" class="GridRows">&nbsp;<a href="ApproverSettingsMRCF.aspx" style="font-size:small;">MRCF Approver Settings</a></td></tr>
       <tr><td id="moduleList" class="GridRows">&nbsp;<a href="ApproverSettingsRequ.aspx" style="font-size:small;">Requisition Approver Settings</a></td></tr> 
	   <tr><td id="moduleList" class="GridRows">&nbsp;<a href="SpeedoModules.aspx" style="font-size:small;">Modules Settings</a></td></tr>             
       <tr><td id="moduleList" class="GridRows">&nbsp;<a href="SpeedoModuleApprover.aspx" style="font-size:small;">Approver Settings</a></td></tr> 
       <tr><td id="moduleList" class="GridRows">&nbsp;<a href="UserModuleMain.aspx" style="font-size:small;">Module User</a></td></tr> 
       <tr><td id="moduleList" class="GridRows">&nbsp;<a href="HQOracleEmployeeChecking.aspx" style="font-size:small;">HQ Portal to Oracle Employee Checking</a></td></tr> 
      </table>
     </div>    
    </div>     
   </td>
  </tr>  
      
  <tr><td style="height:9px;"></td></tr>
 </table>
</asp:Content>

