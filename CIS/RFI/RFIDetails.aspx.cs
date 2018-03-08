using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STIeForms;
using System.Data;
using HRMS;

public partial class CIS_RFI_RFIDetails : System.Web.UI.Page
{



 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!Page.IsPostBack)
  {
   RFI.AuthenticateUser(RFI.RFIUserType.Requestor, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["rficode"].ToString());

   txtRFICode.Text = Request.QueryString["rficode"].ToString();
   txtRequestorName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"].ToString());

   RFI objRFI = new RFI();
   objRFI.RFICode = txtRFICode.Text;
   objRFI.Fill();
   txtIntended.Text = objRFI.Intended;
   txtDateReq.Text = objRFI.DateRequested.ToString("MMMM dd, yyyy");
   txtApproverName.Text = clsEmployee.GetName(objRFI.Approver, EmployeeNameFormat.LastFirst);
   txtApproverRem.Text = objRFI.ApproverRemarks;
   txtPMName.Text = clsEmployee.GetName(objRFI.ProcurementManager, EmployeeNameFormat.LastFirst);
   txtPMRem.Text = objRFI.ProcurementManagerRemarks;
   hdnStatus.Value = objRFI.Status;
   txtStat.Text = objRFI.StatusDescription;


   dgItems.DataSource = RFIDetails.GetDSGPageRFIDetails(Request.QueryString["rficode"]);
   dgItems.DataBind();
  }
 }

 protected void chkShowSpecification_CheckedChanged(object sender, EventArgs e)
 {

 }

 protected void btnVoid_Click(object sender, ImageClickEventArgs e)
 {

 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("RFIMain.aspx");
 }

}