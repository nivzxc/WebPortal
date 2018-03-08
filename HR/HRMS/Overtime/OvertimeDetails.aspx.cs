using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HRMS;

public partial class HR_HRMS_Overtime_OvertimeDetails : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsOvertime.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["otcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsOvertime.AuthenticateAccessForm(OvertimeUsers.Requestor, Request.Cookies["Speedo"]["UserName"], Request.QueryString["otcode"].ToString());
   clsOvertime ot = new clsOvertime(Request.QueryString["otcode"].ToString());
   ot.Fill();
   txtOTCode.Text = ot.OvertimeCode;
   txtChargeType.Text = clsOvertime.GetChargeTypeDesc(ot.ChargeType);
   txtChargeTo.Text = clsDepartment.GetName(ot.DepartmentCode);
   txtDateFiled.Text = ot.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(ot.Username);
   txtDateFrom.Text = ot.DateStart.ToString("MMM dd, yyyy hh:mm tt");
   txtDateTo.Text = ot.DateEnd.ToString("MMM dd, yyyy hh:mm tt");
   txtHours.Text = ot.Units.ToString();
   txtReason.Text = ot.Reason;
   txtHApprover.Text = clsUsers.GetName(ot.ApproverHeadName);
   txtHStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverHeadStatus);
   txtHDateProcess.Text = clsDateTime.CheckMinDate(ot.ApproverHeadDate);
   txtHRemarks.Text = ot.ApproverHeadRemarks;
   txtStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.Status);
   if (ot.ChargeType == "1")
   {
    txtRApprover.Text = clsUsers.GetName(ot.ApproverRequestorName);
    txtRStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverRequestorStatus);
    txtRDateProcess.Text = clsDateTime.CheckMinDate(ot.ApproverRequestorDate);
    txtRRemarks.Text = ot.ApproverRequestorRemarks;
    
    trRApprover.Visible = true;
    trRStatus.Visible = true;
    trRRemarks.Visible = true;
   }

   if (ot.ApproverDivisionStatus != "X" && ot.ApproverDivisionStatus != "" && !Convert.IsDBNull(ot.ApproverDivisionStatus))
   {
    hdnDApprover.Value = ot.ApproverDivisionName;
    txtDApprover.Text = clsUsers.GetName(ot.ApproverDivisionName);
    hdnDStatus.Value = ot.ApproverDivisionStatus;
    txtDStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverDivisionStatus);
    txtDProcessDate.Text = clsDateTime.CheckMinDate(ot.ApproverDivisionDate);
    txtDRemarks.Text = ot.ApproverDivisionRemarks;
    trApproverDivision1.Visible = true;
    trApproverDivision2.Visible = true;
    trApproverDivision3.Visible = true;
   }

   if (ot.ApproverCOOStatus != "X" && ot.ApproverCOOStatus != "" && !Convert.IsDBNull(ot.ApproverCOOStatus))
   {
    hdnCApprover.Value = ot.ApproverCOOName;
    txtCApprover.Text = clsUsers.GetName(ot.ApproverCOOName);
    hdnCStatus.Value = ot.ApproverCOOStatus;
    txtCStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverCOOStatus);
    txtCProcessDate.Text = clsDateTime.CheckMinDate(ot.ApproverCOODate);
    txtCRemarks.Text = ot.ApproverCOORemarks;
    trApproverCOO1.Visible = true;
    trApproverCOO2.Visible = true;
    trApproverCOO3.Visible = true;
   }

   btnCancel.Visible = clsOvertime.ToOvertimeStatus(ot.Status) == OvertimeStatus.ForApproval;
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OvertimeMenu.aspx");
 }

 protected void btnCancel_Click(object sender, EventArgs e)
 {
  using (clsOvertime ot = new clsOvertime())
  {
   ot.OvertimeCode = Request.QueryString["otcode"].ToString();
   ot.Cancel();
  }
  Response.Redirect("OvertimeMenu.aspx");
 }

}