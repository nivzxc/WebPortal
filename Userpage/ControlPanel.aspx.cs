using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using STIeForms;
using HRMS;
using HqWeb.GroupUpdate;
using HqWeb.Reward;

public partial class Userpage_ControlPanel : System.Web.UI.Page
{
 protected void Page_Load(object sender, EventArgs e)
 {
      string strUsername = Request.Cookies["Speedo"]["UserName"].ToString();
      divEFormSettings.Visible = (clsMRCF.GetUserType(strUsername) == clsMRCF.MRCFUserType.DivisionHead) || (clsRequisition.GetUserType(strUsername) == clsRequisition.RequisitionUserType.DivisionHead);
      divReward.Visible = clsSystemModule.HasAccess("REWARD", strUsername);
      divGroupUpdate.Visible = clsGroupUpdate.HasAccess(strUsername) || clsGroupUpdate.IsGroupApprover(strUsername) || clsGroupUpdate.IsDivisionApprover(strUsername);
      divCMDChecklist.Visible = clsCMDChecklist.HasAccess(strUsername);
      // divSchoolMaintenace.Visible = clsSIS.IsUserValid(clsSIS.SISUsers.Encoder, strUsername);
      divSchoolMaintenace.Visible = clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString());
        int EmployeeCount = clsTimesheetAccess.DSLEmployee(strUsername).Rows.Count;
      divTimesheetEmployees.Visible = false;

      if (EmployeeCount >= 1)
      {
          divTimesheetEmployees.Visible = true;
      }
      /*
      divBudgetManagement.Visible = false;
      divBudgetManagement.Visible = clsSystemModule.HasAccess("BudMan", Request.Cookies["Speedo"]["UserName"].ToString());
      divDivisionBudget.Visible = DivisionViewer.HasAccess(Request.Cookies["Speedo"]["UserName"].ToString());
      */
      if (divDivisionBudget.Visible == false)
      {
          divDepartmentBudget.Visible = true;
      }
      else
      {
          divDepartmentBudget.Visible = false;
      }
      divPCASCashierProcessing.Visible = clsSystemModule.HasAccess("PETTYC", Request.Cookies["Speedo"]["UserName"].ToString());
      divFPCPCASProcessing.Visible = clsSystemModule.HasAccess("PETTYFPC", Request.Cookies["Speedo"]["UserName"].ToString());
      divHRMSReport.Visible = clsSystemModule.HasAccess("013", Request.Cookies["Speedo"]["Username"]) || clsSystemModule.HasAccess("020", Request.Cookies["Speedo"]["Username"]);
      //divSportsFest.Visible = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);
      divSupplyCustodian.Visible = clsRequisition.IsApprover(clsRequisition.RequisitionUserType.SuppliesCustodian, Request.Cookies["Speedo"]["UserName"].ToString());
      divAdmin.Visible = clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString());
      divEforms.Visible = clsSystemModule.HasAccess("EFORM", Request.Cookies["Speedo"]["UserName"].ToString());
      divCataProcessing.Visible = clsSystemModule.HasAccess("CATA", Request.Cookies["Speedo"]["UserName"].ToString());
      divMaintenance.Visible = divEFormSettings.Visible || divReward.Visible || divReward.Visible || divGroupUpdate.Visible || divCMDChecklist.Visible || divSchoolMaintenace.Visible || divTimesheetEmployees.Visible || divHRMSReport.Visible || divSportsFest.Visible || divSupplyCustodian.Visible || divEforms.Visible || divCataProcessing.Visible || divAdmin.Visible || divPCASCashierProcessing.Visible;

 //rosssss
      divAirlines.Visible = clsSystemModule.HasAccess("PROC", Request.Cookies["Speedo"]["UserName"].ToString());

 }
}
