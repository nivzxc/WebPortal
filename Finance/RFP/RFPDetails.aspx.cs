using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Finance_RFP_RFPDetails : System.Web.UI.Page
{
 public void LoadRFPDetails()
 {
  string strWrite = "";
  double dbltotal = 0.0;
  string strChargeTo = "";
  DataTable tblDetails = clsRFPRequestDetails.GetDSG(Request.QueryString["ControlNumber"]);
  foreach (DataRow drw in tblDetails.Rows)
  {
   if (drw["schlcode"].ToString().Trim() != "")
   { strChargeTo = clsSchool.GetSchoolName(drw["schlcode"].ToString().Trim()); }
   if (drw["rccode"].ToString().Trim() != "")
   { strChargeTo = clsRC.GetRCName(drw["rccode"].ToString().Trim()); }
   if (drw["others"].ToString().Trim() != "")
   { strChargeTo = drw["others"].ToString().Trim(); }

   double dblAmount = Double.Parse(drw["amount"].ToString());
   strWrite += "<tr>" +
                 "<td class='GridRows'>&nbsp;" + drw["itemdesc"].ToString() + "</td>" +
                 "<td class='GridRows'>&nbsp;" + strChargeTo + "</td>" +
                 "<td align='right' class='GridRows'>" + string.Format("{0:0,0.00}", dblAmount) + "</td>" +
               "</tr>";
   dbltotal += double.Parse(drw["amount"].ToString());
  }

  strWrite += "<tr>" +
                   "<td colspan='3'align='right' class='GridRows'><b>Total Amount:    P " + string.Format("{0:0,0.00}", dbltotal) + "</b></td>" +
              "</tr>";

  Response.Write(strWrite);
 }

 private void LoadDetails()
 {
  using (clsRFPRequest objRequest = new clsRFPRequest())
  {
   string strStatus = "";
   objRequest.ControlNumber = Request.QueryString["ControlNumber"];
   objRequest.Fill();
   lblControlNumber.Text = objRequest.ControlNumber;
   lblPayee.Text = objRequest.PayeeName;
   lblRequestType.Text = clsRFPRequestType.GetRequestTypeName(objRequest.RequestCode);
   lblRequestedBy.Text = clsEmployee.GetName(objRequest.CreatedBy);
   lblProjectTitle.Text = objRequest.ProjectTitle;
   lblReferenceRFANo.Text = objRequest.RFANumber;
   lblDateCheckNeeded.Text = objRequest.DateNeeded.ToLongDateString().ToString();
   lblDateCreated.Text = objRequest.CreatedOn.ToLongDateString().ToString();
   lblSupportingDocuments.Text = objRequest.SupportingDoument;
   lblRemarks.Text = objRequest.Remarks;

   if (objRequest.Status == "1")
   {
    lblStatus.Text = "( Approved )";
    if (objRequest.CreatedBy == Request.Cookies["Speedo"]["UserName"])
    { btnPrint.Visible = true; }
   }
   if (objRequest.Status == "M")
   {
       lblStatus.Text = "( Manual Approval )";
       if (objRequest.CreatedBy == Request.Cookies["Speedo"]["UserName"])
       { btnPrint.Visible = true; }
   }
   if (objRequest.Status == "0")
   {
       lblStatus.Text = "( Disapproved )";
       if (objRequest.CreatedBy == Request.Cookies["Speedo"]["UserName"])
       { btnPrint.Visible = false; }
   }
  }
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
  if (!clsRFPRequest.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["ControlNumber"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!IsPostBack)
  {
   LoadDetails();
  }
 }


 protected void btnPrint_Click(object sender, EventArgs e)
 {
  Response.Redirect("RFPPrint.aspx?ControlNumber=" + Request.QueryString["ControlNumber"]);
 }
 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("RFPMenu.aspx");
 }
}
