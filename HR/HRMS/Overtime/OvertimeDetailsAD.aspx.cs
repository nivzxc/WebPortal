using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;

public partial class HR_HRMS_Overtime_OvertimeDetailsAD : System.Web.UI.Page
{
 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsOvertime.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["otcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsOvertime.AuthenticateAccessForm(OvertimeUsers.ApproverDivision, Request.Cookies["Speedo"]["UserName"], Request.QueryString["otcode"].ToString());
   clsOvertime ot = new clsOvertime(Request.QueryString["otcode"].ToString());
   ot.Fill();
   txtOTCode.Text = ot.OvertimeCode;
   txtChargeType.Text = clsOvertime.GetChargeTypeDesc(ot.ChargeType);
   txtRequestorDepartment.Text = clsDepartment.GetName(clsEmployee.GetDepartmentCode(ot.Username));
   txtRenderedTo.Text = clsDepartment.GetName(ot.DepartmentCode);
   txtDateFiled.Text = ot.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtDateFrom.Text = ot.DateStart.ToString("MMM dd, yyyy hh:mm tt");
   txtDateTo.Text = ot.DateEnd.ToString("MMM dd, yyyy hh:mm tt");
   txtHours.Text = ot.Units.ToString();
   txtRequestorName.Text = clsUsers.GetName(ot.Username);
   txtReason.Text = ot.Reason;
   txtStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.Status);
   hdnStatus.Value = ot.Status;
   txtHApprover.Text = clsUsers.GetName(ot.ApproverHeadName);
   hdnHApprover.Value = ot.ApproverHeadName;
   hdnHStatus.Value = ot.ApproverHeadStatus;
   txtHStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverHeadStatus);
   txtHProcessDate.Text = clsDateTime.CheckMinDate(ot.ApproverHeadDate);
   txtHRemarks.Text = ot.ApproverHeadRemarks;
   if (ot.ChargeType == "1")
   {
    hdnRApprover.Value = ot.ApproverRequestorName;
    txtRApprover.Text = clsUsers.GetName(ot.ApproverRequestorName);
    hdnRStatus.Value = ot.ApproverRequestorStatus;
    txtRStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverRequestorStatus);
    txtRDateProcessed.Text = clsDateTime.CheckMinDate(ot.ApproverRequestorDate);
    txtRRemarks.Text = ot.ApproverRequestorRemarks;
    trRApprover.Visible = true;
    trRStatus.Visible = true;
    trRRemarks.Visible = true;
   }

   hdnDApprover.Value = ot.ApproverDivisionName;
   txtDApprover.Text = clsUsers.GetName(ot.ApproverDivisionName);
   hdnDStatus.Value = ot.ApproverDivisionStatus;
   txtDStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverDivisionStatus);
   txtDProcessDate.Text = clsDateTime.CheckMinDate(ot.ApproverDivisionDate);
   txtDRemarks.Text = ot.ApproverDivisionRemarks;

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

   if (ot.ApproverDivisionStatus == "F")
   {
    txtDRemarks.ReadOnly = false;
    txtDRemarks.BackColor = System.Drawing.Color.White;
    btnApprove.Visible = true;
    btnDisapprove.Visible = true;
   }
   else
   {    
    if ((clsValidator.CheckDate(txtDateFrom.Text) <= DateTime.Now) || (hdnCStatus.Value == "A" || hdnCStatus.Value == "D") || (hdnStatus.Value == "C" || hdnStatus.Value == "D") || (hdnDStatus.Value == "A" || hdnDStatus.Value == "D"))
    {
     btnDisapprove.Visible = false;
     btnApprove.Visible = false;
    }
   }

  }
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  using (clsOvertime ot = new clsOvertime())
  {
   ot.OvertimeCode = Request.QueryString["otcode"];
   ot.Fill();
   ot.ApproverDivisionRemarks = txtDRemarks.Text;
   ot.ApproverDivisionDate = DateTime.Now;
   ot.ApproveDivision();
            //ADDED by CALVIN CAVITE FEB 15, 2018
            ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalSuccess();", true);
  }       
 }

 protected void btnDisapprove_Click(object sender, EventArgs e)
 {
  using (clsOvertime ot = new clsOvertime())
  {
   ot.OvertimeCode = Request.QueryString["otcode"];
   ot.Fill();
   ot.ApproverDivisionRemarks = txtDRemarks.Text;
   ot.ApproverDivisionDate = DateTime.Now;
   ot.DisapproveDivision();
  }
        //ADDED by CALVIN CAVITE FEB 15, 2018
        ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalDisapprove();", true);
    }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OvertimeMenu.aspx");
 }

}