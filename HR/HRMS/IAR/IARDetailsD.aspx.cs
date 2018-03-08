using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_IAR_IARDetailsD : System.Web.UI.Page
{

 private void LoadDetails()
 {
  if (!clsIAR.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["iarcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsIAR iar = new clsIAR();
   iar.IARCode = Request.QueryString["iarcode"].ToString();
   iar.Fill();
   txtIARCode.Text = iar.IARCode;
   txtDateFiled.Text = iar.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtDateStart.Text = iar.DateStart.ToString("MMM dd, yyyy hh:mm tt");
   txtDateEnd.Text = iar.DateEnd.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(iar.Username);
   txtReason.Text = iar.Reason;
   txtStatus.Text = clsIAR.ToIARStatus(iar.Status);
   hdnStatus.Value = iar.Status;
   txtApproverH.Text = clsUsers.GetName(iar.ApproverHeadName);
   hdnApproverH.Value = iar.ApproverHeadName;
   hdnStatusH.Value = iar.ApproverHeadStatus;
   txtStatusH.Text = clsIAR.ToIARStatus(iar.ApproverHeadStatus);
   txtProcessDateH.Text = clsDateTime.CheckMinDate(iar.ApproverHeadDate);
   txtRemarksH.Text = iar.ApproverHeadRemarks;
   hdnApproverD.Value = iar.ApproverDivisionName;
   txtApproverD.Text = clsUsers.GetName(iar.ApproverDivisionName);
   hdnStatusD.Value = iar.ApproverDivisionStatus;
   txtStatusD.Text = clsIAR.ToIARStatus(iar.ApproverDivisionStatus);
   txtProcessDateD.Text = clsDateTime.CheckMinDate(iar.ApproverDivisionDate);
   txtRemarksD.Text = iar.ApproverDivisionRemarks;

   if (iar.ApproverDivisionStatus == "F" && iar.Status == "F")
   {
    txtRemarksD.ReadOnly = false;
    txtRemarksD.BackColor = System.Drawing.Color.White;
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

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
  if (!Page.IsPostBack)
  {
   LoadDetails();
  }
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  string strErrorMessage = "";

  if (strErrorMessage.Length == 0)
  {
   using (clsIAR iar = new clsIAR())
   {
    iar.IARCode = Request.QueryString["iarcode"];
    iar.Fill();
    iar.ApproverDivisionDate = DateTime.Now;
    iar.ApproverDivisionRemarks = txtRemarksD.Text;
    iar.ApproveDivision();
   }
   Response.Redirect("IARMenu.aspx");
  }
  else
  {
   divError.Visible = true;
   lblErrMsg.Text = strErrorMessage;
  }
 }

 protected void btnDisapprove_Click(object sender, EventArgs e)
 {
  using (clsIAR iar = new clsIAR())
  {
   iar.IARCode = Request.QueryString["iarcode"];
   iar.Fill();
   iar.ApproverDivisionRemarks = txtRemarksD.Text;
   iar.ApproverDivisionDate = DateTime.Now;
   iar.DisapproveDivision();
  }
  Response.Redirect("IARMenu.aspx");
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("IARMenu.aspx");
 }

}