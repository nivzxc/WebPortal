<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="HRMS.aspx.cs" Inherits="HR_HRMS_HRMS" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 
    <table width="647px" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../HR.aspx" class="SiteMap">HR</a> » 
     <a href="HRMS.aspx" class="SiteMap">HRMS</a>     
    </div>        
   </td>
  </tr>--%>

  <tr runat="server" id="trManpowerReportSpacer" visible="false"><td style="height:9px;"></td></tr>
  <tr runat="server" id="trManpowerReport" visible="false">
   <td>
    <div class="border"  style="padding-top:10px;padding-left:10px;padding-bottom:10px; width:850px;">    
     <b><span class="HeaderText">HRMS Manpower Summary</span></b>
     <br />
     <br />    
     <table>
      <tr>
       <td style="vertical-align:top;">
        <div class="GridBorder" style="width:350px;">
         <table width="100%" cellpadding="5" cellspacing="1">
          <tr>
           <td class="GridColumns" style="width:64%;font-size:x-small;text-align:center;"><b>Division</b></td>
           <td class="GridColumns" style="width:18%;font-size:x-small;text-align:center;"><b>Count</b></td>
           <td class="GridColumns" style="width:18%;font-size:x-small;text-align:center;"><b>%</b></td>
          </tr>
          <% LoadDivisionCount(); %>
         </table>
        </div>       
       </td>      
       <td style="vertical-align:top;">
        <div class="GridBorder" style="width:200px;">
         <table width="100%" cellpadding="5" cellspacing="1">
          <tr>
           <td class="GridColumns" style="width:50%;font-size:x-small;text-align:center;"><b>Job Class</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>Count</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>%</b></td>
          </tr>
          <% LoadJobClassificationSummary(); %>
         </table>
        </div>       
       </td>
       <td style="vertical-align:top;">
        <div class="GridBorder" style="width:250px;">
         <table width="100%" cellpadding="5" cellspacing="1">
          <tr>
           <td class="GridColumns" style="width:50%;font-size:x-small;text-align:center;"><b>Gender</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>Count</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>%</b></td>
          </tr>
          <% LoadGenderSummary(); %>
         </table>
        </div>       
       </td>
      </tr>
     </table>
     <br />
     <table>
      <tr>
       <td style="vertical-align:top;">
        <div class="GridBorder" style="width:300px;">
         <table width="100%" cellpadding="5" cellspacing="1">
          <tr>
           <td class="GridColumns" style="width:50%;font-size:x-small;text-align:center;"><b>Tenure Bracket</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>Count</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>%</b></td>
          </tr>
          <% LoadTenureBracketSummary(); %>
         </table>
        </div>       
       </td>      
       <td style="vertical-align:top;">
        <div class="GridBorder" style="width:250px;">
         <table width="100%" cellpadding="5" cellspacing="1">
          <tr>
           <td class="GridColumns" style="width:50%;font-size:x-small;text-align:center;"><b>Age Bracket</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>Count</b></td>
           <td class="GridColumns" style="width:25%;font-size:x-small;text-align:center;"><b>%</b></td>
          </tr>
          <% LoadAgeBracketSummary(); %>
         </table>
        </div>  
       </td>
      </tr>      
     </table>

          
     
     <br />
         
     <div class="GridBorder">
      <table width="500px" cellpadding="5" cellspacing="2">
       <tr><td class="GridColumns" style="text-align:left; font-size:small;">&nbsp;<b>Manpower Reports</b></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptHeadCountReport.aspx" style="font-size:small;" target="_blank">Head Count</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptProjectBasedContractualConsultant.aspx" style="font-size:small;" target="_blank">Project Based/Contractual/Consultant</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptResignedEmployee.aspx" style="font-size:small;" target="_blank">Resigned Employees</a></td></tr>       
      </table>
     </div>    
    </div>     
   </td>
  </tr>
  
  <tr runat="server" id="trManagementReportsSpace" visible="false"><td style="height:9px;"></td></tr>
  <tr runat="server" id="trManagementReports" visible="false">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <div class="GridBorder">
      <table width="500px" cellpadding="5" cellspacing="2">
       <tr><td class="GridColumns" style="text-align:left; font-size:small;">&nbsp;<b>HRMS Reports</b></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptDTRSummary.aspx" style="font-size:small;" target="_blank">Attendance Summary Report</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptPerfectAttendance.aspx" style="font-size:small;" target="_blank">Perfect Attendance Report</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptTardinessSummaryReport.aspx" style="font-size:small;" target="_blank">Tardiness Report</a></td></tr> 
	   <tr><td class="GridRows">&nbsp;<a href="Reports/rptExcessiveTardinessReport.aspx" style="font-size:small;" target="_blank">Excessive Tardiness Report</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptAbsenceWithoutLeaveReport.aspx" style="font-size:small;" target="_blank">Absence without Leave Report</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptLeaveSummaryReport.aspx" style="font-size:small;" target="_blank">Leave Summary Report</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptARLeaveForApprovalSummary.aspx" style="font-size:small;" target="_blank">Leave for Approval</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptLeaveApplications.aspx" style="font-size:small;" target="_blank">Leave Application List</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Reports/rptAROBForApprovalSummary.aspx" style="font-size:small;" target="_blank">OB for Approval</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="ATW/ATWAllEU.aspx" style="font-size:small;" target="_blank">Authority to Work Application List</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Overtime/OvertimeAllEU.aspx" style="font-size:small;" target="_blank">Overtime Application List</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="IAR/IARAllEU.aspx" style="font-size:small;" target="_blank">Internet Access Request Application List</a></td></tr>

      </table>
     </div>    
    </div>     
   </td>
  </tr>  
      
<%--  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="GridColumns">Human Resources Management System</span></b>
     <div class="GridBorder">
      <table width="500px" cellpadding="5" cellspacing="2">
      <tr><td class="GridColumns" style="text-align:left; font-size:small;">&nbsp;<b>Human Resources Management System</b></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="TimeSheet/Timesheet.aspx" style="font-size:small;">Time Sheet</a></td></tr>       
       <tr><td class="GridRows">&nbsp;<a href="TimeSheet/TimeCardACM.aspx" style="font-size:small;">Time Card (ACM)</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="TimeSheet/OvertimeComputed.aspx" style="font-size:small;">Overtime Computed</a></td></tr>              
       <tr><td class="GridRows">&nbsp;<a href="Leave/LeaveMenu.aspx" style="font-size:small;">Application For Leave</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Undertime/UndertimeMenu.aspx" style="font-size:small;">Application For Undertime</a></td></tr>       
       <tr><td class="GridRows">&nbsp;<a href="OB/OBMenu.aspx" style="font-size:small;">Official Business</a></td></tr>
       <tr><td class="GridRows">&nbsp;<a href="Overtime/OvertimeMenu.aspx" style="font-size:small;">Application for Overtime</a></td></tr>
      </table>
     </div>    
     
     <br /><br />
    </div>     
   </td>
  </tr>--%>
     
 </table>  
</asp:Content>