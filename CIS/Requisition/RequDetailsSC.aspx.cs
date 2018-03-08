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
using System.Text.RegularExpressions;
using HRMS;
using STIeForms;

public partial class CIS_Requisition_RequDetailsSC : System.Web.UI.Page
{

	protected void BindItems()
	{
		clsRequisition requisition = new clsRequisition(txtRequisitionCode.Text);
  dgItems.DataSource = requisition.DSGItems();
		dgItems.DataBind();

		bool blnIssuedAll = false;
		foreach (DataGridItem ditm in dgItems.Items)
		{
			TextBox ptxtBalance = (TextBox)ditm.FindControl("txtBalance");
			TextBox ptxtSOQty = (TextBox)ditm.FindControl("txtSOQty");
			Label plblSOQtyAll = (Label)ditm.FindControl("lblSOQtyAll");
			TextBox ptxtSuppRem = (TextBox)ditm.FindControl("txtSuppRem");

			blnIssuedAll = (ptxtBalance.Text == "0" ? true : false);

			ptxtSOQty.Visible = !blnIssuedAll;
			plblSOQtyAll.Visible = blnIssuedAll;
			ptxtSuppRem.ReadOnly = !blnIssuedAll;
			if (blnIssuedAll)
				ptxtSuppRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			bool blnReadOnly;
			clsRequisition.AuthenticateUser(clsRequisition.RequisitionUserType.SuppliesCustodian, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["requcode"].ToString());

			txtRequisitionCode.Text = Request.QueryString["requcode"].ToString();
			txtSuppName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

   using (clsRequisition requisition = new clsRequisition(txtRequisitionCode.Text))
   {
    requisition.Fill();
    hdnUserCode.Value = requisition.Username;
    hdnChargeTo.Value = requisition.ChargeTo;
    lblTotalPrice.Text = "P " + requisition.TotalCost.ToString("###,###.00");
    txtStat.Text = requisition.StatusDescription;
    txtIntended.Text = requisition.Intended;
    txtDateReq.Text = requisition.DateRequested.ToString("MMMM dd,yyyy");
    hdnGrpHeadCode.Value = requisition.GroupHead;
    txtGrpHeadRem.Text = requisition.GroupHeadRemarks;
    hdnDiviHeadCode.Value = requisition.DivisionHead;
    txtDiviHeadRem.Text = requisition.DivisionHeadRemarks;
    txtSuppRem.Text = requisition.SuppliesCustodianRemarks;
    blnReadOnly = (requisition.Status == "C" ? true : false);
    divButtons.Visible = !blnReadOnly;
    txtSuppRem.ReadOnly = blnReadOnly;
   }
			if (blnReadOnly)
				txtSuppRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");

   hdnRCCode.Value = clsEmployee.GetRCCode(hdnUserCode.Value);
   txtRCName.Text = clsRC.GetRCName(hdnRCCode.Value);
   txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);

			BindItems();

			txtUserName.Text = clsUsers.GetName(hdnUserCode.Value);
   hdnUserMail.Value = clsUsers.GetEmail(hdnUserCode.Value);
   txtGrpHeadName.Text = clsUsers.GetName(hdnGrpHeadCode.Value);
   txtDiviHeadName.Text = clsUsers.GetName(hdnDiviHeadCode.Value);

			txtChargeToBudget.Text = "P " + clsRequisition.GetCurrentBudget(hdnChargeTo.Value).ToString("#,###,##0.00");
			foreach (DataGridItem itm in dgItems.Items)
			{
				TextBox txtpSuppRem = (TextBox)itm.FindControl("txtSuppRem");
				TextBox txtpSOQty = (TextBox)itm.FindControl("txtSOQty");
				txtpSuppRem.ReadOnly = blnReadOnly;
				txtpSOQty.ReadOnly = blnReadOnly;
			}

		}
 }

    protected void btnApprove_Click(object sender, EventArgs e)
	{
		clsRequisition requisition = new clsRequisition();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cn.Open();
			foreach (DataGridItem itm in dgItems.Items)
			{
				TextBox txtpSuppRem = (TextBox)itm.FindControl("txtSuppRem");
				TextBox txtpSOQty = (TextBox)itm.FindControl("txtSOQty");
				Label lblpItemCode = (Label)itm.FindControl("lblItemCode");
				cmd.CommandText = "UPDATE CIS.RequisitionDetails SET supprem='" + txtpSuppRem.Text + "',soqty=soqty+" + txtpSOQty.Text + " WHERE itemcode='" + lblpItemCode.Text + "' AND requcode='" + Request.QueryString["requcode"] + "'";
				cmd.ExecuteNonQuery();
			}

			if (clsRequisition.IsIssuedAll(Request.QueryString["requcode"].ToString()))
			{
				cmd.CommandText = "UPDATE CIS.Requisition SET suppdate='" + DateTime.Now + "',status='C',suppstat='A',supprem=@supprem WHERE requcode='" + Request.QueryString["requcode"] + "'";
				cmd.Parameters.Add("@supprem", SqlDbType.VarChar, 200);
				cmd.Parameters["@supprem"].Value = txtSuppRem.Text;
				cmd.ExecuteNonQuery();
			}
			else
			{
				cmd.CommandText = "UPDATE CIS.Requisition SET suppdate='" + DateTime.Now + "',status='P',supprem=@supprem WHERE requcode='" + Request.QueryString["requcode"] + "'";
				cmd.Parameters.Add("@supprem", SqlDbType.VarChar, 200);
				cmd.Parameters["@supprem"].Value = txtSuppRem.Text;
				cmd.ExecuteNonQuery();
			}
		}

		string strBody = "Hi " + txtUserName.Text + ",<br><br>" +
																			"Your requisition has been processed by " + txtSuppName.Text + ".<br><br>" +
																			"The following items are ready for pick-up <br><br>" +
																			"<table border='1' style='width:600px;'>" +
																				"<tr>" +
																					"<td style='text-align:center'>Requested Item</td>" +
																					"<td style='text-align:center'>Ordered Qty</td>" +
																					"<td style='text-align:center'>Previously Issued Qty</td>" +
																					"<td style='text-align:center'>Issued Now</td>" +
																					"<td style='text-align:center'>Remaining</td>" +
																				"</tr>";

		foreach (DataGridItem ditm in dgItems.Items)
		{
			Label plblItemDesc = (Label)ditm.FindControl("lblItemDesc");
			TextBox ptxtQty = (TextBox)ditm.FindControl("txtQty");
			TextBox ptxtIssued = (TextBox)ditm.FindControl("txtIssued");
			TextBox ptxtSOQty = (TextBox)ditm.FindControl("txtSOQty");
			int intRemaining = Convert.ToInt32(ptxtQty.Text) - (Convert.ToInt32(ptxtIssued.Text) + Convert.ToInt32(ptxtSOQty.Text));
			if (ptxtSOQty.Text != "0")
				strBody = strBody + "<tr>" +
																									"<td>" + plblItemDesc.Text + "</td>" +
																									"<td style='text-align:center'>" + ptxtQty.Text + "</td>" +
																									"<td style='text-align:center'>" + ptxtIssued.Text + "</td>" +
																									"<td style='text-align:center'>" + ptxtSOQty.Text + "</td>" +
																									"<td style='text-align:center'>" + intRemaining + "</td>" +
																								"</tr>";
		}

		strBody = strBody + "</table>";

		strBody = strBody + "<br><a href='" + ConfigurationManager.AppSettings["SpeedoUrl"] + "/CIS/Requisition/RequDetails.aspx?requcode=" + txtRequisitionCode.Text + "'>Click here to review the request</a><br><br>" +
																						"If you can't click on the above link,<br>" +
																						"you can also review the request by copying and pasting into your browser the following address:<br><i>" + ConfigurationManager.AppSettings["SpeedoUrl"] + "/CIS/Requisition/RequDetails.aspx?requcode=" + txtRequisitionCode.Text + "</i><br><br>" +
																						"All the best,<br>E-Forms Administrator";
  clsSpeedo.SendMail(hdnUserMail.Value, "Requisition Item Issuance", strBody);

		Response.Redirect("RequMenu.aspx");
	}

}
