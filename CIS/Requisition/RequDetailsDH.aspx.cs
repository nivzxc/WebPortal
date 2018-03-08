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

public partial class CIS_Requisition_RequDetailsDH : System.Web.UI.Page
{

	protected bool HasEnoughBudget()
	{
		bool blnReturn = false;

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
			blnReturn = true;
		}
		else
		{
			imgMessage.ImageUrl = "~/Support/close64.png";
			lblMessage.ForeColor = System.Drawing.Color.Red;
			lblMessage.Text = "Not enough budget.";
			btnApprove.Enabled = false;
			blnReturn = false;
		}
		lblMessage.Visible = true;
		imgMessage.Visible = true;
		return blnReturn;
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
			clsRequisition.AuthenticateUser(clsRequisition.RequisitionUserType.DivisionHead, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["requcode"].ToString());

			txtRequCode.Text = Request.QueryString["requcode"];
			txtDiviHeadName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

   using (clsRequisition requisition = new clsRequisition(txtRequCode.Text))
   {
    requisition.Fill();
    hdnRequestor.Value = requisition.Username;
    txtDateReq.Text = requisition.DateRequested.ToString();
    txtRemarks.Text = requisition.Intended;
    hdnGrpHeadCode.Value = requisition.GroupHead;
    txtGrpHeadRem.Text = requisition.GroupHeadRemarks;
    hdnGrpHeadStatus.Value = requisition.GroupHeadStatus;
    txtDiviHeadRem.Text = requisition.DivisionHeadRemarks;
    blnReadOnly = (requisition.DivisionHeadStatus == "F" ? false : true);
    hdnSuppCode.Value = requisition.SuppliesCustodian;
    txtSuppRem.Text = requisition.SuppliesCustodianRemarks;
    txtStat.Text = requisition.StatusDescription;
    hdnChargeTo.Value = requisition.ChargeTo;
   }

   txtRCName.Text = clsEmployee.GetRCName(hdnRequestor.Value);
   txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);

   using (clsUsers users = new clsUsers())
   {
    users.Username = hdnRequestor.Value;
    users.Fill();
    txtRequestorName.Text = users.FullName;
    hdnRequestorMail.Value = users.Email;

    users.Username = hdnGrpHeadCode.Value;
    users.Fill();
    txtGrpHeadName.Text = users.FullName;
    hdnGrpHeadMail.Value = users.Email;

    users.Username = hdnSuppCode.Value;
    users.Fill();
    txtSuppName.Text = users.FullName;
    hdnSuppMail.Value = users.Email;
   }

			txtChargeToBudget.Text = "P " + clsRequisition.GetCurrentBudget(hdnChargeTo.Value).ToString("#,###,##0.00");
			BindItems();
			HasEnoughBudget();

			divButton.Visible = !blnReadOnly;
			divButtons2.Visible = !blnReadOnly;
			divBudget.Visible = !blnReadOnly;
			txtDiviHeadRem.ReadOnly = blnReadOnly;
			txtRemarks.ReadOnly = blnReadOnly;

			if (blnReadOnly)
			{
				txtDiviHeadRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");
				txtRemarks.BackColor = System.Drawing.Color.FromName("#f0f8ff");
                dgItems.Columns[5].Visible = false;
                //foreach (DataGridItem itm in dgItems.Items)
                //{
                //    TextBox ptxtQty = (TextBox)itm.FindControl("txtQty");
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
    //            cmd.CommandText = "UPDATE CIS.Requisition SET totcost=(SELECT SUM(tprice) FROM CIS.RequisitionDetails WHERE requcode='" + Request.QueryString["requcode"] + "') WHERE requcode='" + Request.QueryString["requcode"] + "'";
    //            cmd.ExecuteNonQuery();
    //        }
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
				cmd.CommandText = "UPDATE CIS.Requisition SET userrem=@userrem,headdate='" + DateTime.Now + "',headrem=@headrem,status='A',headstat='A',sprvstat=@sprvstat WHERE requcode='" + Request.QueryString["requcode"] + "'";
				cmd.Parameters.Add("@headrem", SqlDbType.VarChar, 200);
				cmd.Parameters.Add("@userrem", SqlDbType.VarChar, 200);
				cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
				cmd.Parameters["@headrem"].Value = txtDiviHeadRem.Text;
				cmd.Parameters["@userrem"].Value = txtRemarks.Text;
				cmd.Parameters["@sprvstat"].Value = (hdnGrpHeadStatus.Value == "F" ? "N" : hdnGrpHeadStatus.Value);
				cmd.ExecuteNonQuery();

				clsRequisition.DeductBudget(hdnChargeTo.Value, dblTotalCost);
			}
		}

		if (blnHasBudget)
		{
			clsRequisition.SendNotification(clsRequisition.RequisitionMailType.SentToApproverSC, txtRequestorName.Text, txtSuppName.Text, hdnSuppMail.Value, txtRequCode.Text);
			clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ApproveToRequestor, txtRequestorName.Text, txtDiviHeadName.Text, hdnRequestorMail.Value, txtRequCode.Text);
			clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ApproveToApproverDH, txtRequestorName.Text, txtDiviHeadName.Text,clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtRequCode.Text);

			Response.Redirect("RequMenu.aspx");
		}
		else
		{
			HasEnoughBudget();
		}
	}

    protected void btnDisApprove_Click(object sender, EventArgs e)
	{
		clsRequisition requisition = new clsRequisition(txtRequCode.Text);
		requisition.DisapproveDH(txtDiviHeadRem.Text,hdnGrpHeadStatus.Value);

		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.DisapproveToRequestor, txtRequestorName.Text, txtDiviHeadName.Text, hdnRequestorMail.Value, txtRequCode.Text);
		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.DisapproveToApproverDH, txtRequestorName.Text, txtDiviHeadName.Text,clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtRequCode.Text);

		Response.Redirect("RequMenu.aspx");
	}

    protected void btnModify_Click(object sender, EventArgs e)
	{
		clsRequisition requisition = new clsRequisition(txtRequCode.Text);
		requisition.ModificationDH(txtDiviHeadRem.Text,hdnGrpHeadStatus.Value);

		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ModificationToRequestor, txtRequestorName.Text, txtDiviHeadName.Text, hdnRequestorMail.Value, txtRequCode.Text);
		clsRequisition.SendNotification(clsRequisition.RequisitionMailType.ModificationToApproverDH, txtRequestorName.Text, txtDiviHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtRequCode.Text);

		Response.Redirect("RequMenu.aspx");
	}

}
