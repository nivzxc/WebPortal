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

public partial class CIS_Requisition_RequDetailsGH : System.Web.UI.Page
{

	protected bool HasEnoughBudget()
	{
		bool blnEnough = false;

		double dblBudget = clsRequisition.GetCurrentBudget(hdnChargeTo.Value);
		double dblTotalCost = clsRequisition.GetTotalCost(Request.QueryString["requcode"]);
		double dblRemaining = dblBudget - dblTotalCost;

		lblCurBudget.Text = dblBudget.ToString("###,##0.00");
		lblTotalCost.Text = dblTotalCost.ToString("###,##0.00");
		lblRemBudget.Text = dblRemaining.ToString("###,##0.00");

		if (dblRemaining >= 0)
		{
			imgMessage.ImageUrl = "~/Support/ok64.png";
			lblMessage.ForeColor = System.Drawing.Color.Green;
			lblMessage.Text = "The budget is enough.";
			btnApprove.Enabled = true;
			btnApprove2.Enabled = true;
			blnEnough = true;
		}
		else
		{
			imgMessage.ImageUrl = "~/Support/close64.png";
			lblMessage.ForeColor = System.Drawing.Color.Red;
			lblMessage.Text = "Not enough budget.";
			btnApprove.Enabled = false;
			btnApprove2.Enabled = false;
			blnEnough = false;
		}
		lblMessage.Visible = true;
		imgMessage.Visible = true;
		return blnEnough;
	}

	protected void BindItems()
	{
		double dblTotalPrice = 0.0;
		clsRequisition requisition = new clsRequisition(txtRequCode.Text);
		DataTable tblItems = requisition.DSGItems();

		dgItems.DataSource = tblItems.DefaultView;
		dblTotalPrice = Convert.ToDouble(tblItems.Compute("SUM(tprice)", String.Empty).ToString());
		dgItems.Columns[0].FooterText = "&nbsp;Total ordered items [" + tblItems.Rows.Count + "]";
		dgItems.Columns[4].FooterText = dblTotalPrice.ToString("###,##0.00") + "&nbsp;";
		dgItems.DataBind();
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			bool blnReadOnly;
			clsRequisition.AuthenticateUser(clsRequisition.RequisitionUserType.GroupHead, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["requcode"].ToString());

			txtChargeToBudget.Text = "P " + clsRequisition.GetCurrentBudget(hdnChargeTo.Value).ToString("#,###,##0.00");
			txtRequCode.Text = Request.QueryString["requcode"];
			txtGrpHeadName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

   using (clsRequisition requisition = new clsRequisition(txtRequCode.Text))
   {
    requisition.Fill();
    hdnRequestor.Value = requisition.Username;
    txtDateReq.Text = requisition.DateRequested.ToString();
    txtIntended.Text = requisition.Intended;
    txtGrpHeadRem.Text = requisition.GroupHeadRemarks;
    hdnDiviHeadCode.Value = requisition.DivisionHead;
    txtDiviHeadRem.Text = requisition.DivisionHeadRemarks;
    hdnSuppCode.Value = requisition.SuppliesCustodian;
    txtSuppRem.Text = requisition.SuppliesCustodianRemarks;
    hdnChargeTo.Value = requisition.ChargeTo;
    txtStat.Text = requisition.StatusDescription;
    hdnGrpHeadStat.Value = requisition.GroupHeadStatus;
    blnReadOnly = (hdnGrpHeadStat.Value == "F" ? false : true);
   }

   txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);
   using (clsUsers users = new clsUsers())
   {
    users.Username = hdnDiviHeadCode.Value;
    users.Fill();
    txtDiviHeadName.Text = users.FullName;
    hdnDiviHeadMail.Value = users.Email;

    users.Username = hdnRequestor.Value;
    users.Fill();
    txtRequestorName.Text = users.FullName;
    hdnRequestorMail.Value = users.Email;

    users.Username = hdnSuppCode.Value;
    users.Fill();
    txtSuppName.Text = users.FullName;
    hdnSuppMail.Value = users.Email;
   }

			BindItems();
			HasEnoughBudget();

			divButtons.Visible = !blnReadOnly;
			divButtons2.Visible = !blnReadOnly;
			divBudget.Visible = !blnReadOnly;
			txtGrpHeadRem.ReadOnly = blnReadOnly;
			txtIntended.ReadOnly = blnReadOnly;

			if (blnReadOnly)
			{
				txtGrpHeadRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");
				txtIntended.BackColor = System.Drawing.Color.FromName("#f0f8ff");
				//dgItems.Columns[6].Visible = false;
                //foreach (DataGridItem itm in dgItems.Items)
                //{
                //    Label ptxtQty = (Label)itm.FindControl("txtQty");
                //    ptxtQty.ReadOnly = blnReadOnly;
                //    ptxtQty.BackColor = System.Drawing.Color.FromName("#f0f8ff");
                //}
			}
		}
 }

    //protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    //{
    //    clsRequisition requisition = new clsRequisition(txtRequCode.Text);
    //    if (requisition.CountItem() > 1)
    //    {
    //        HiddenField phdnItemCode = (HiddenField)e.Item.FindControl("hdnItemCode");
    //        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
    //        {
    //            SqlCommand cmd = cn.CreateCommand();
    //            cmd.CommandText = "DELETE FROM CIS.RequisitionDetails WHERE itemcode='" + phdnItemCode.Value + "' AND requcode='" + Request.QueryString["requcode"] + "'";
    //            cn.Open();
    //            cmd.ExecuteNonQuery();
    //        }
    //        requisition.UpdateTotalCost();
    //        dgItems.EditItemIndex = -1;
    //        if (dgItems.Items.Count == 1)
    //            dgItems.CurrentPageIndex = dgItems.CurrentPageIndex - 1;
    //        BindItems();
    //        HasEnoughBudget();
    //        divErr.Visible = false;
    //    }
    //    else
    //    {
    //        lblErrMsg.Text = "&nbsp;! Cannot delete this item.<br>";
    //        divErr.Visible = true;
    //    }
    //}

    protected void btnApprove_Click(object sender, EventArgs e)
	{
  //clsSpeedo speedo = new clsSpeedo();
		bool blnHasBudget = true;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cn.Open();
			foreach (DataGridItem itm in dgItems.Items)
			{
				HiddenField phdnItemCode = (HiddenField)itm.FindControl("hdnItemCode");
                Label ptxtQty = (Label)itm.FindControl("txtQty");
				Label plblPrice = (Label)itm.FindControl("lblPrice");
				double dblTPrice = (Convert.ToDouble(ptxtQty.Text) * Convert.ToDouble(plblPrice.Text));
    if (clsRequisition.HasBudget(Request.QueryString["requcode"], dblTPrice, hdnChargeTo.Value, phdnItemCode.Value))
    {
     cmd.CommandText = "UPDATE CIS.RequisitionDetails SET qty='" + ptxtQty.Text + "',tprice='" + dblTPrice + "' WHERE itemcode='" + phdnItemCode.Value + "' AND requcode='" + Request.QueryString["requcode"] + "'";
     cmd.ExecuteNonQuery();
     divErr.Visible = false;
    }
    else
    {
     blnHasBudget = false;
     ptxtQty.BackColor = System.Drawing.Color.MistyRose;
     lblErrMsg.Text = "&nbsp;! Not Enought Budget.<br>";
     divErr.Visible = true;
    }
			}

			clsRequisition requisition = new clsRequisition(txtRequCode.Text);
			double dblTotalCost = requisition.UpdateTotalCost();

			if (blnHasBudget)
			{
				bool blnHeadApprovalRequired = clsRequisition.IsHeadApprovalRequired(hdnChargeTo.Value, hdnDiviHeadCode.Value);
				if (blnHeadApprovalRequired)
				{
					cmd.CommandText = "UPDATE CIS.Requisition SET userrem=@userrem,sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem,sprvstat='A' WHERE requcode='" + Request.QueryString["requcode"] + "'";
				}
				else
				{
					cmd.CommandText = "UPDATE CIS.Requisition SET userrem=@userrem,sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem,sprvstat='A',headstat='N',status='A' WHERE requcode='" + Request.QueryString["requcode"] + "'";
					clsRequisition.DeductBudget(hdnChargeTo.Value, dblTotalCost);
				}
				cmd.Parameters.Add("@sprvrem", SqlDbType.VarChar, 250);
				cmd.Parameters.Add("@userrem", SqlDbType.VarChar, 250);
				cmd.Parameters["@sprvrem"].Value = txtGrpHeadRem.Text;
				cmd.Parameters["@userrem"].Value = txtIntended.Text;
				cmd.ExecuteNonQuery();

				if (blnHeadApprovalRequired)
					clsRequisition.SendNotification(clsRequisition.RequisitionMailType.SentToApproverDH, txtRequestorName.Text, txtDiviHeadName.Text, hdnDiviHeadMail.Value, txtRequCode.Text);
				else
					clsRequisition.SendNotification(clsRequisition.RequisitionMailType.SentToApproverSC, txtRequestorName.Text, txtSuppName.Text, hdnSuppMail.Value, txtRequCode.Text);
				clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ApproveToApproverGH, txtRequestorName.Text, txtGrpHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtRequCode.Text);
				clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ApproveToRequestor, txtRequestorName.Text, txtGrpHeadName.Text, hdnRequestorMail.Value, txtRequCode.Text);
			}
		}

		if (blnHasBudget)
			Response.Redirect("RequMenu.aspx");
		else
			HasEnoughBudget();
	}

    protected void btnDisApprove_Click(object sender, EventArgs e)
	{
		clsRequisition requisition = new clsRequisition(txtRequCode.Text);
		requisition.DisapproveGH(txtGrpHeadRem.Text);

		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.DisapproveToRequestor, txtRequestorName.Text, txtGrpHeadName.Text, hdnRequestorMail.Value, txtRequCode.Text);
		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.DisapproveToApproverGH, txtRequestorName.Text, txtGrpHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtRequCode.Text);

		Response.Redirect("RequMenu.aspx");
	}

    protected void btnModify_Click(object sender, EventArgs e)
	{
		clsRequisition requisition = new clsRequisition(txtRequCode.Text);
		requisition.ModificationGH(txtGrpHeadRem.Text);

		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ModificationToRequestor, txtRequestorName.Text, txtGrpHeadName.Text, hdnRequestorMail.Value, txtRequCode.Text);
		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ModificationToApproverGH, txtRequestorName.Text, txtGrpHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtRequCode.Text);

		Response.Redirect("RequMenu.aspx");
	}

}
