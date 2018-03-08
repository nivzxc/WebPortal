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

public partial class HR_HRMS_Overtime_OvertimeDetailsAR : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsOvertime.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["otcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsOvertime.AuthenticateAccessForm(OvertimeUsers.ApproverRequestor, Request.Cookies["Speedo"]["UserName"], Request.QueryString["otcode"].ToString());
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
   txtHDateProcess.Text = clsDateTime.CheckMinDate(ot.ApproverHeadDate);
   txtHRemarks.Text = ot.ApproverHeadRemarks;
   hdnRApprover.Value = ot.ApproverRequestorName;
   txtRApprover.Text = clsUsers.GetName(ot.ApproverRequestorName);
   hdnRStatus.Value = ot.ApproverRequestorStatus;
   txtRStatus.Text = clsOvertime.ToOvertimeStatusDesc(ot.ApproverRequestorStatus);
   txtRDateProcess.Text = clsDateTime.CheckMinDate(ot.ApproverRequestorDate);
   txtRRemarks.Text = ot.ApproverRequestorRemarks;

   if (ot.ApproverRequestorStatus == "F")
   {
    txtRRemarks.ReadOnly = false;
    txtRRemarks.BackColor = System.Drawing.Color.White;
    btnApprove.Visible = true;
    btnDisapprove.Visible = true;
   }
   else
   {
    btnApprove.Visible = false;
    btnDisapprove.Visible = false;
   }
  }
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  using (clsOvertime ot = new clsOvertime())
  {
   ot.OvertimeCode = Request.QueryString["otcode"];
   ot.Fill();
   ot.ApproverRequestorRemarks = txtRRemarks.Text;
   ot.ApproverRequestorDate = DateTime.Now;
   ot.ApproveRequestor();           
  }   
   //ADDED by CALVIN CAVITE FEB 15, 2018
   ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalSuccess();", true);
 }

 protected void btnDisapprove_Click(object sender, EventArgs e)
 {
  using (clsOvertime ot = new clsOvertime())
  {
   ot.OvertimeCode = Request.QueryString["otcode"];
   ot.Fill();
   ot.ApproverRequestorRemarks = txtRRemarks.Text;
   ot.ApproverRequestorDate = DateTime.Now;
   ot.DisapproveRequestor();
           
  }
   //ADDED by CALVIN CAVITE FEB 15, 2018
   ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalDisapprove();", true);
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OvertimeMenu.aspx");
 }

}