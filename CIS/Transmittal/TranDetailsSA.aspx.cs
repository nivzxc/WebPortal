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

public partial class CIS_Transmittal_TranDetailsSA : System.Web.UI.Page
{
 
	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			bool blnReadOnly;
			DataTable tblItems = new DataTable("Items");
			txtReqCode.Text = Request.QueryString["trancode"];
			txtAppName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

			clsTransmittal transmittal = new clsTransmittal();
   transmittal.TransmittalCode = txtReqCode.Text;
			transmittal.Fill();
			hdnUserCode.Value = transmittal.UserName;
			txtDateReq.Text = transmittal.DateRequested.ToString();
			txtDateNeed.Text = transmittal.DateNeeded.ToString();
			txtItemDesc.Text = transmittal.ItemDescription;
			txtUnit.Text = transmittal.Unit;
			txtRemarks.Text = transmittal.Remarks;
			hdnChargeTo.Value = transmittal.ChargeTo;
			txtDispType.Text = transmittal.DispatchTypeDescription;
			hdnGroupHead.Value = transmittal.GroupHead;
			txtGroupHeadRemarks.Text = transmittal.GroupHeadRemarks;
			txtAppRemarks.Text = transmittal.ApproverRemarks;
			txtStat.Text = transmittal.StatusDescription;
			blnReadOnly = (transmittal.Status == "F" ? false : true);

   txtUserName.Text = clsUsers.GetName(hdnUserCode.Value);
   txtGroupHeadName.Text = clsUsers.GetName(hdnGroupHead.Value);
			txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);

			dgItems.DataSource = transmittal.DSGItems().DefaultView;
			dgItems.DataBind();

			txtAppRemarks.ReadOnly = blnReadOnly;
			divButtons.Visible = !blnReadOnly;
			if (blnReadOnly)
				txtAppRemarks.BackColor = System.Drawing.Color.FromName("#f0f8ff");
		}
 }

    protected void btnApprove_Click(object sender, EventArgs e)
	{
		clsTransmittal transmittal = new clsTransmittal();
  transmittal.TransmittalCode = txtReqCode.Text;
		transmittal.ApproveSA(txtAppRemarks.Text, Request.Cookies["Speedo"]["UserName"].ToString());
  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.ApproveToRequestor, txtUserName.Text, txtAppName.Text, clsUsers.GetEmail(hdnUserCode.Value), txtReqCode.Text);
  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.ApproveToApprover, txtUserName.Text, txtAppName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtReqCode.Text);
		Response.Redirect("TranMenu.aspx");
	}

    protected void btnDisApprove_Click(object sender, EventArgs e)
	{
		clsTransmittal transmittal = new clsTransmittal();
  transmittal.TransmittalCode = txtReqCode.Text;
		transmittal.DisapproverSA(txtAppRemarks.Text, Request.Cookies["Speedo"]["UserName"].ToString());
  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.DisapproveToRequestor, txtUserName.Text, txtAppName.Text, clsUsers.GetEmail(hdnUserCode.Value), txtReqCode.Text);
  clsTransmittal.SendNotification(clsTransmittal.TransmittalMailType.DisapproveToApprover, txtUserName.Text, txtAppName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtReqCode.Text);

		Response.Redirect("TranMenu.aspx");
	}

}
