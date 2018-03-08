using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_IAR_IARDetails : System.Web.UI.Page
{

 private void LoadDetails()
 {
  using (clsIAR iar = new clsIAR())
  {
   iar.IARCode = Request.QueryString["iarcode"].ToString();
   iar.Fill();
   txtIARCode.Text = iar.IARCode;
   txtDateFiled.Text = iar.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtDateStart.Text = iar.DateStart.ToString("MMM dd, yyyy hh:mm tt");
   txtDateEnd.Text = iar.DateEnd.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(iar.Username);
   txtReason.Text = iar.Reason;
   txtApproverH.Text = clsUsers.GetName(iar.ApproverHeadName);
   txtStatusH.Text = clsIAR.ToIARStatus(iar.ApproverHeadStatus);
   txtRemarksH.Text = iar.ApproverHeadRemarks;
   txtApproverD.Text = clsUsers.GetName(iar.ApproverDivisionName);
   txtStatusD.Text = clsIAR.ToIARStatus(iar.ApproverDivisionStatus);
   txtRemarksD.Text = iar.ApproverDivisionRemarks;
   txtStatus.Text = clsIAR.ToIARStatus(iar.Status);

   btnCancel.Visible = clsIAR.ToIARStatusDesc(iar.Status) == IARStatus.ForApproval;
  }
 }

 //////////////////////////////
 ///////// Page Event /////////
 ////////////////////////////// 

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
  if (!clsIAR.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["iarcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
   LoadDetails();
 }

 protected void btnCancel_Click(object sender, EventArgs e)
 {
  using (clsIAR iar = new clsIAR())
  {
   iar.IARCode = Request.QueryString["iarcode"].ToString();
   iar.Cancel();
  }
  Response.Redirect("IARMenu.aspx");
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("IARMenu.aspx");
 }

}