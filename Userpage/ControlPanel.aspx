<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="ControlPanel.aspx.cs" Inherits="Userpage_ControlPanel" %>

<asp:Content ID="conPao" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
    <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
<%--    <div id="div1" runat="server" class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px; width:200px">    
     <b><span class="HeaderText">Settings</span></b>
     <br /><br />
     <div id="div3" runat="server" class="masterpanelcontent"><a href="ChangePassword.aspx">Change Password</a></div>
     <div id="div4" runat="server" class="masterpanelcontent"><a href="EditImage.aspx">Modify Pictures/Avatar</a></div>
     <div id="div5" runat="server" class="masterpanelcontent"><a href="../HR/HRMS/TimeSheet/Timesheet.aspx">Timesheet</a></div>
     <div id="div6" runat="server" class="masterpanelcontent"><a href="../HR/HRMS/CV/MyCVDetails.aspx">Curriculum Vitae</a></div>
     <div id="div7" runat="server" class="masterpanelcontent"><a href="../HR/HRMS/TimeSheet/OvertimeComputed.aspx">Computed Overtime</a></div>
     <div id="div8" runat="server" class="masterpanelcontent"><a href="../HR/HRMS/TimeSheet/TimeCardACM.aspx">Time Card - ACM</a></div>
    </div>--%>


    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Settings</span></b>
     <br /><br />
     <table style="border-color: #FAFAFA; font-size:small; color:#4169e1;" frame="void">
      <tr>
       <%--<td style="width:10%"><img src="../Support/notes48.png" alt="" /></td>
       <td style="width:40%">&nbsp;<a href="EditProfile.aspx">Edit Personal Profile</a></td>--%>
           <td style="background-color:; width:200px;">&nbsp;
               <div class="settings" style="background-color:;">
                <a href="ChangePassword.aspx"> <img src="../Support/identity48.png" alt="" /> Change Password</a>
               </div>     
           </td>
           <td style="border-color: #FAFAFA; width:200px;">&nbsp;
               <div class="settings" style="background-color:;">
               <a href="../HR/HRMS/TimeSheet/TimeCardACM.aspx"> <img src="../Support/timecard.png" alt="" width="48" height="48"/> Time Card - ACM</a>
               </div>  
           </td>
          </tr>
    <%--      <tr>
           <td><img src="../Support/identity48.png" alt="" /></td>
           <td>&nbsp;<a href="ChangePassword.aspx">Change Password</a></td>
          </tr>--%>
          <tr>
           <td class="masterpanelcontent" style="border-color: #FAFAFA; width:200px;">&nbsp;
               <div class="settings" style="background-color:;">
               <a href="EditImage.aspx"> <img src="../Support/image48.png" alt="" /> Modify Pictures/Avatar</a>
               </div>
           </td> 
           <td style="border-color: #FAFAFA">&nbsp;
               <div class="settings" style="background-color:; width:200px;">
                <a href="../HR/HRMS/TimeSheet/Timesheet.aspx"> <img src="../Support/education48.png" alt="" /> Timesheet</a>
               </div> 
           </td>
      </tr>         
<%--    <tr>
       <td><img src="../Support/eforms48.png" alt="" /></td>
       <td>&nbsp;<a href="CSSettings.aspx">E-Forms Settings</a></td>
      </tr>  --%>
      <tr>     
       <td class="masterpanelcontent" style="border-color: #FAFAFA; width:200px;">&nbsp;
           <div class="settings" style="background-color:;">
            <a href="../HR/HRMS/CV/MyCVDetails.aspx"><img src="../Support/cv_ico.png" alt="" width="48" height="48" />Curriculum Vitae</a>
           </div>
       </td>
       <td style="border-color: #FAFAFA">&nbsp;
           <div class="settings" style="background-color:; width:200px;">
            <a href="../HR/HRMS/TimeSheet/OvertimeComputed.aspx"> <img src="../Support/contact48.png" alt="" /> Computed Overtime</a>
           </div>
       </td>
      </tr>  
<%--    <tr>
       <td class="style2" style="border-color: #FAFAFA"><img src="../Support/education48.png" alt="" /></td>
       <td colspan="3" style="border-color: #FAFAFA">&nbsp;<a 
               href="../HR/HRMS/TimeSheet/Timesheet.aspx" style="border-color: #FFFFFF">Timesheet</a></td>
      </tr> --%>
<%--      <tr>
     
       <td></td>
       <td>&nbsp;</td>
      </tr>--%>
<%--    <tr>
       <td><img src="../Support/contact48.png" alt="" /></td>
       <td>&nbsp;<a href="~/HR/HRMS/TimeSheet/OvertimeComputed.aspx">Computed Overtime</a></td>
      </tr> --%>
<%--    <tr>
       <td><img src="../Support/star48.png" alt="" /></td>
       <td>&nbsp;<a href="../HR/HRMS/TimeSheet/TimesheetEmployees.aspx">Timesheet Employees</a></td>
       <td>&nbsp;</td>
       <td>&nbsp;</td>
      </tr>  --%>
<%--     <tr>
       <td><img src="../Support/news48.png" alt="" /></td>
       <td>&nbsp;<a href="../GroupUpdate/GroupUpdateAdd.aspx">Group Updates</a></td>
      </tr>  --%>     
     </table>
    </div>

    <div id="divMaintenance" runat="server" class="" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px; width:200px; margin-left:10px">    
     <b><span class="HeaderText">Other Modules</span></b>
     <br /><br />
     <div id="divAdmin" runat="server" class="masterpanelcontent"><a href="../Admin/Admin.aspx">Administrator Module</a></div>
    
     
     <div id="divDepartmentBudget" runat="server" class="masterpanelcontent"><a href="../BudgetManagement/AccountBudgetDepartmentViewer.aspx">Department Budget</a></div>
     <div id="divCataProcessing" runat="server" class="masterpanelcontent"><a href="../Finance/CATA/FinanceCataMenuFinance.aspx">CATA Processing</a></div>
     <div id="divPCASCashierProcessing" runat="server" class="masterpanelcontent"><a href="../Finance/PCASH/PettyCashRequestCashierMenu.aspx">PCAS Processing</a></div>
     <div id="divFPCPCASProcessing" runat="server" class="masterpanelcontent"><a href="../Finance/PCASH/PettyCashRequestFinanceMenu.aspx">PCAS Processing(FPC)</a></div>
     <div id="divCMDChecklist" runat="server" class="masterpanelcontent"><a href="../CMD/Checklist/CheckListContent.aspx">CMD Checklist</a></div>     
     <div id="divSupplyCustodian" runat="server" class="masterpanelcontent"><a href="../CIS/Requisition/RequDepBudget.aspx">Department's Supplies Budget Settings</a></div>
     <div id="divEFormSettings" runat="server" class="masterpanelcontent"><a href="CSSettings.aspx">E-Forms Settings</a></div>
     <div id="divGroupUpdate" runat="server" class="masterpanelcontent"><a href="../GroupUpdate/GroupUpdateMain.aspx">Group Update</a></div>
     <div id="divHRMSReport" runat="server" class="masterpanelcontent"><a href="../HR/HRMS/HRMS.aspx">HRMS Report</a></div>
     <div id="divEforms" runat="server" class="masterpanelcontent"><a href="../Report/RequestFormMain.aspx">Processed E-Forms</a></div>
     <div id="divReward" runat="server" class="masterpanelcontent"><a href="../RewardPoint/TransactionMain.aspx">Reward Transaction</a></div>
     <div id="divSchoolMaintenace" runat="server" class="masterpanelcontent"><a href="../CMD/SIS/SISMenu.aspx">Branches Maintenance</a></div>
     <div id="divTimesheetEmployees" runat="server" class="masterpanelcontent"><a href="../HR/HRMS/TimeSheet/TimesheetEmployees.aspx">Timesheet Employees</a></div>
     <!--
         <div id="divDivisionBudget" runat="server" class="masterpanelcontent"><a href="../BudgetManagement/AccountBudgetListDivisionViewer.aspx">Division Budget</a></div>
          <div id="divBudgetManagement" runat="server" class="masterpanelcontent"><a href="../BudgetManagement/AccountBudgetList.aspx">Budget Management</a></div>
         <div id="divSportsFest" runat="server" class="masterpanelcontent"><a href="../Synergy/Synergy.aspx">Sports Fest</a></div>
         <div id="divAirlines" runat="server" class="masterpanelcontent"><a href="../CIS/MRCF/MRCFAirlines.aspx">MRCF Airlines Settings</a></div>     
     -->
	
    </div>
   </td>
  </tr>
 </table>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphHead">
   
    <style type="text/css">
        .style1
        {
            width: 252px;
        }
        .style2
        {
            width: 54px;
        }
    </style>
   
</asp:Content>
