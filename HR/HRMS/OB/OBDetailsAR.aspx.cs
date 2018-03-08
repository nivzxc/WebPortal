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

public partial class HR_HRMS_OB_OBDetailsAR : System.Web.UI.Page
{

 protected void BindSchedule()
 {
  DataTable tblSchedule = clsOBDetails.GetDataTable(Request.QueryString["obcode"].ToString());
  dgSchedule.DataSource = tblSchedule.DefaultView;
  dgSchedule.DataBind();
  foreach (DataGridItem itm in dgSchedule.Items)
  {
   HiddenField phdnStatus = (HiddenField)itm.FindControl("hdnStatus");
   CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");
   pchkApprove.Checked = (phdnStatus.Value == "1" ? true : false);
  }
 }

 private int CountCheckedSchedule()
 {
  int intReturn = 0;
  foreach (DataGridItem itm in dgSchedule.Items)
  {
   CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");
   intReturn += (pchkApprove.Checked ? 1 : 0);
  }
  return intReturn;
 }

 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsOB.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["obcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsOB.AuthenticateAccessForm(OBUsers.ApproverRequestor, Request.Cookies["Speedo"]["UserName"], Request.QueryString["obcode"].ToString());

   clsOB ob = new clsOB(Request.QueryString["obcode"].ToString());
   ob.Fill();
   txtOBCode.Text = ob.OBCode;
   txtOBType.Text = clsOB.GetOBTypeDesc(ob.OBType);
   txtRequestorDepartment.Text = clsDepartment.GetName(clsEmployee.GetDepartmentCode(ob.Username));
   txtRenderedTo.Text = clsDepartment.GetName(ob.DepartmentCode);
   txtDateFiled.Text = ob.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(ob.Username);
   txtReason.Text = ob.Reason;
   txtStatus.Text = clsOB.ToOBStatus(ob.Status);
   hdnStatus.Value = ob.Status;
   txtHApprover.Text = clsUsers.GetName(ob.ApproverHeadName);
   hdnHApprover.Value = ob.ApproverHeadName;
   hdnHStatus.Value = ob.ApproverHeadStatus;
   txtHStatus.Text = clsOB.ToOBStatus(ob.ApproverHeadStatus);
   txtHRemarks.Text = ob.ApproverHeadRemarks;
   hdnRApprover.Value = ob.ApproverRequestorName;
   txtRApprover.Text = clsUsers.GetName(ob.ApproverRequestorName);
   hdnRStatus.Value = ob.ApproverRequestorStatus;
   txtRStatus.Text = clsOB.ToOBStatus(ob.ApproverRequestorStatus);
   txtRRemarks.Text = ob.ApproverRequestorRemarks;

   BindSchedule();

   if (ob.ApproverRequestorStatus == "F")
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
    foreach (DataGridItem itm in dgSchedule.Items)
    {
     CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");
     pchkApprove.Enabled = false;
    }
   }

  }
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  string strErrorMessage = "";

  if (CountCheckedSchedule() == 0)
   strErrorMessage += "<br>You should approve atleast 1 schedule.";

  if (strErrorMessage.Length == 0)
  {
   using (clsOB ob = new clsOB())
   {
    ob.OBCode = Request.QueryString["obcode"];
    ob.Fill();
    ob.ApproverRequestorRemarks = txtRRemarks.Text;
    ob.ApproverRequestorDate = DateTime.Now;
    ob.ApproveRequestor();

    foreach (DataGridItem itm in dgSchedule.Items)
    {
     HiddenField phdnFocusDate = (HiddenField)itm.FindControl("hdnFocusDate");
     HiddenField phdnKeyInDate = (HiddenField)itm.FindControl("hdnKeyInDate");
     HiddenField phdnKeyOutDate = (HiddenField)itm.FindControl("hdnKeyOutDate");
     CheckBox pchkApprove = (CheckBox)itm.FindControl("chkApprove");

     if (!pchkApprove.Checked)
     {
      clsOBDetails obdetails = new clsOBDetails();
      obdetails.OBCode = ob.OBCode;
      obdetails.FocusDate = clsValidator.CheckDate(phdnFocusDate.Value);
      obdetails.KeyIn = clsValidator.CheckDate(phdnKeyInDate.Value);
      obdetails.KeyOut = clsValidator.CheckDate(phdnKeyOutDate.Value);
      obdetails.Status = (pchkApprove.Checked ? "1" : "0");
      obdetails.UpdateBy = Request.Cookies["Speedo"]["UserName"];
      obdetails.UpdateOn = DateTime.Now;
      obdetails.UpdateStatus();
     }
    }
    //ADDED by CALVIN CAVITE FEB 15, 2018
    ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalSuccess();", true);
   }
  
  }
  else
  {
   divError.Visible = true;
   lblErrMsg.Text = strErrorMessage;
  }

 }

 protected void btnDisapprove_Click(object sender, EventArgs e)
 {
  using (clsOB ob = new clsOB())
  {
   ob.OBCode = Request.QueryString["obcode"];
   ob.Fill();
   ob.ApproverRequestorRemarks = txtRRemarks.Text;
   ob.ApproverRequestorDate = DateTime.Now;
   ob.DisapproveRequestor();
            //ADDED by CALVIN CAVITE FEB 15, 2018
            ScriptManager.RegisterStartupScript(this, GetType(), "Disapproved!", "ModalDisapprove();", true);
  }
 
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OBMenu.aspx");
 }

}