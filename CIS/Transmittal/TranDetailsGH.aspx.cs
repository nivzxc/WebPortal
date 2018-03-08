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
using HRMS;
using STIeForms;

public partial class CIS_Transmittal_TranDetailsGH : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!Page.IsPostBack)
  {
   bool blnReadOnly = false;

   DataTable tblItems = new DataTable("Items");
   txtTransmittalCode.Text = Request.QueryString["trancode"];
   txtGroupHeadName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

   clsTransmittal transmittal = new clsTransmittal();
   transmittal.TransmittalCode = txtTransmittalCode.Text;
   transmittal.Fill();
   hdnRequestor.Value = transmittal.UserName;
   txtRequestor.Text = clsUsers.GetName(hdnRequestor.Value);
   txtDateReq.Text = transmittal.DateRequested.ToString();
   txtItemDesc.Text = transmittal.ItemDescription;
   txtUnit.Text = transmittal.Unit;
   txtRemarks.Text = transmittal.Remarks;
   txtDispType.Text = transmittal.DispatchTypeDescription;
   txtStat.Text = transmittal.StatusDescription;
   if (transmittal.DispatchType == "H")
    txtChargeTo.Text = clsRC.GetRCName(transmittal.ChargeTo);
   else if (transmittal.DispatchType == "S")
    txtChargeTo.Text = clsSchool.GetSchoolName(transmittal.ChargeTo);
   hdnGroupHead.Value = transmittal.GroupHead;
   txtGroupHeadRemarks.Text = transmittal.GroupHeadRemarks;
   txtDateNeeded.Text = transmittal.DateNeeded.ToString(); ;
   if (transmittal.GroupHeadStatus == "A" && (transmittal.ApproverStatus == "A" || transmittal.ApproverStatus == "D"))
   {
    hdnApprover.Value = transmittal.Approver;
    txtApproverName.Text = clsUsers.GetName(transmittal.Approver);
    txtApproverRemarks.Text = transmittal.ApproverRemarks;
   }
   else
   {
    trApprover.Visible = false;
    trApproverRem.Visible = false;
   }
   blnReadOnly = (transmittal.GroupHeadStatus == "F" ? false : true);

   dgItems.DataSource = transmittal.DSGItems().DefaultView;
   dgItems.DataBind();

   txtGroupHeadRemarks.ReadOnly = blnReadOnly;
   divButtons.Visible = !blnReadOnly;
   divButtons2.Visible = !blnReadOnly;
   if (blnReadOnly)
    txtGroupHeadRemarks.BackColor = System.Drawing.Color.FromName("#f0f8ff");
  }
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  clsTransmittal transmittal = new clsTransmittal();
  transmittal.TransmittalCode = txtTransmittalCode.Text;
  transmittal.ApproveGH(txtGroupHeadRemarks.Text);

  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.ApproveToRequestor, txtRequestor.Text, txtGroupHeadName.Text, clsUsers.GetEmail(hdnRequestor.Value), txtTransmittalCode.Text);
  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.ApproveToApproverGH, txtRequestor.Text, txtGroupHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtTransmittalCode.Text);
  //string strApprover = clsTransmittal.GetApprover();
  //string strApprover2 = clsTransmittal.GetApprover2();
  //clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.SentToApprover, txtRequestor.Text, clsUsers.GetName(strApprover), clsUsers.GetEmail(strApprover), txtTransmittalCode.Text);
  //clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.SentToApprover, txtRequestor.Text, clsUsers.GetName(strApprover2), clsUsers.GetEmail(strApprover2), txtTransmittalCode.Text);

  Response.Redirect("TranMenu.aspx");
 }

 protected void btnDisapprove_Click(object sender, EventArgs e)
 {
  clsTransmittal transmittal = new clsTransmittal();
  transmittal.TransmittalCode = txtTransmittalCode.Text;
  transmittal.DisapproveGH(txtGroupHeadRemarks.Text);

  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.DisapproveToRequestor, txtRequestor.Text, txtGroupHeadName.Text, clsUsers.GetEmail(hdnRequestor.Value), txtTransmittalCode.Text);
  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.DisapproveToApproverGH, txtRequestor.Text, txtGroupHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtTransmittalCode.Text);

  Response.Redirect("TranMenu.aspx");
 }

 protected void btnReset_Click(object sender, EventArgs e)
 {
  Response.Redirect("TranDetailsGH.aspx?trancode=" + Request.QueryString["trancode"]);
 }

}
