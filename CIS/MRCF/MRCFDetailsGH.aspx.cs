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

public partial class CIS_MRCF_MRCFDetailsGH : System.Web.UI.Page
{

	protected void SpecificationVisibility(bool pShow)
	{
		foreach (DataGridItem itm in dgItems.Items)
		{
			TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
			ptxtItemSpec.Visible = pShow;
		}
	}

	protected void BindItems()
	{
		dgItems.DataSource = clsMRCF.GetMrcfItems(Request.QueryString["mrcfcode"]);
		dgItems.DataBind();
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		divErr.Visible = false;

		if (!Page.IsPostBack)
		{
            string strProcessScript = "this.value='Processing...';this.disabled=true;";
            btnApprove.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnApprove, "").ToString());
            btnApprove2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnApprove2, "").ToString());

           // string strProcessScript = "this.value='Processing...';this.disabled=true;";
            btnDisApprove.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnDisApprove, "").ToString());
            btnDisapprove2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnDisapprove2, "").ToString());
            btnModification2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnModification2, "").ToString());
            btnModify.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnModify, "").ToString());
            
			clsMRCF.AuthenticateUser(clsMRCF.MRCFUserType.GroupHead, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["mrcfcode"].ToString());

			bool blnReadOnly = false;
			txtMrcfCode.Text = Request.QueryString["mrcfcode"];
   txtGrpHeadName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

			clsMRCF mrcf = new clsMRCF(txtMrcfCode.Text);
			mrcf.Fill();
			hdnRequestor.Value = mrcf.Username;
			hdnReqType.Value = mrcf.RequestType;
			txtReqType.Text = mrcf.RequestTypeDesc;
			txtDateReq.Text = Convert.ToDateTime(mrcf.DateRequested).ToString("MMMM dd,yyyy");
			txtIntended.Text = mrcf.Intended;
			hdnChargeTo.Value = mrcf.ChargeTo;
			hdnGrpHeadStat.Value = mrcf.GroupHeadStatus;
			txtGrpHeadRem.Text = mrcf.GroupHeadRemarks;
			hdnDiviHeadCode.Value = mrcf.DivisionHead;
			txtDiviHeadRem.Text = mrcf.DivisionHeadRemarks;
			hdnProcMngrCode.Value = mrcf.ProcurementManager;
			txtProcMngrRem.Text = mrcf.ProcurementManagerRemarks;
			txtStat.Text = mrcf.StatusDescription;
			blnReadOnly = (hdnGrpHeadStat.Value == "F" ? false : true);

   txtRCName.Text = clsEmployee.GetRCName(hdnRequestor.Value);
   txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);

   using (clsUsers users = new clsUsers())
   {
    users.Username = hdnRequestor.Value;
    users.Fill();
    txtRequestorName.Text = users.FullName;
    hdnRequestorMail.Value = users.Email;

    users.Username = hdnDiviHeadCode.Value;
    users.Fill();
    txtDiviHeadName.Text = users.FullName;
    hdnDiviHeadMail.Value = users.Email;

    users.Username = hdnProcMngrCode.Value;
    users.Fill();
    txtProcMngrName.Text = users.FullName;
    hdnProcMngrMail.Value = users.Email;
   }

			BindItems();

			divButtons.Visible = !blnReadOnly;
			divButtons2.Visible = !blnReadOnly;
			txtGrpHeadRem.ReadOnly = blnReadOnly;
			txtIntended.ReadOnly = blnReadOnly;

			if (blnReadOnly)
			{
				foreach (DataGridItem itm in dgItems.Items)
				{
					TextBox ptxtItemDesc = (TextBox)itm.FindControl("txtItemDesc");
					TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
					TextBox ptxtQty = (TextBox)itm.FindControl("txtQty");
     GrayMatterSoft.GMDatePicker pdteDateNeeded = (GrayMatterSoft.GMDatePicker)itm.FindControl("dteDateNeeded");
					ptxtItemDesc.ReadOnly = blnReadOnly;
					ptxtItemDesc.BackColor = System.Drawing.Color.FromName("#f0f8ff");
					ptxtItemSpec.ReadOnly = blnReadOnly;
					ptxtItemSpec.BackColor = System.Drawing.Color.FromName("#f0f8ff");
					ptxtQty.ReadOnly = blnReadOnly;
					ptxtQty.BackColor = System.Drawing.Color.FromName("#f0f8ff");
					pdteDateNeeded.Enabled = false;
					pdteDateNeeded.BackColor = System.Drawing.Color.FromName("#f0f8ff");
				}
				dgItems.Columns[4].Visible = false;
                txtIntended.BackColor = System.Drawing.Color.FromName("#f0f8ff");
                txtGrpHeadRem.BackColor = System.Drawing.Color.FromName("#f0f8ff");
			}
			else
			{
				foreach (DataGridItem itm in dgItems.Items)
				{
     GrayMatterSoft.GMDatePicker pdteDateNeeded = (GrayMatterSoft.GMDatePicker)itm.FindControl("dteDateNeeded");
					pdteDateNeeded.MinDate = clsMRCF.GetMinimumDateNeeded(hdnReqType.Value, Convert.ToDateTime(txtDateReq.Text));
				}
			}
		}
 }

	protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
	{
		if (hdnGrpHeadStat.Value == "F")
		{
			if (dgItems.Items.Count > 1)
			{
				HiddenField phdnMitmCode = (HiddenField)e.Item.FindControl("hdnMitmCode");
				clsMRCF.RemoveItem(phdnMitmCode.Value);
				dgItems.EditItemIndex = -1;
				if (dgItems.Items.Count == 1)
					dgItems.CurrentPageIndex = dgItems.CurrentPageIndex - 1;
				BindItems();
				divErr.Visible = false;
			}
			else
			{
				lblErrMsg.Text = "&nbsp;! Cannot delete this item.<br>";
				divErr.Visible = true;
			}
		}
	}

    protected void btnApprove_Click(object sender, EventArgs e)
	{
		bool blnHeadRequired = clsMRCF.IsHeadApprovalRequired(hdnChargeTo.Value, hdnDiviHeadCode.Value);

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (blnHeadRequired)
				cmd.CommandText = "UPDATE CIS.Mrcf SET sprvstat='A',sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem,intended=@intended WHERE mrcfcode='" + Request.QueryString["mrcfcode"] + "'";
			else
				cmd.CommandText = "UPDATE CIS.Mrcf SET sprvstat='A',headstat='N',sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem,intended=@intended WHERE mrcfcode='" + Request.QueryString["mrcfcode"] + "'";
			cmd.Parameters.Add("@sprvrem", SqlDbType.VarChar, 200);
			cmd.Parameters["@sprvrem"].Value = txtGrpHeadRem.Text;
			cmd.Parameters.Add("@intended", SqlDbType.VarChar, 100);
			cmd.Parameters["@intended"].Value = txtIntended.Text;
			cn.Open();
			cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();

			cmd.CommandText = "UPDATE CIS.MrcfDetails SET itemdesc=@itemdesc,itemspec=@itemspec,qty=@qty,dateneed=@dateneed WHERE mitmcode=@mitmcode";
			cmd.Parameters.Add("@itemdesc", SqlDbType.VarChar, 100);
			cmd.Parameters.Add("@itemspec", SqlDbType.VarChar, 5000);
			cmd.Parameters.Add("@qty", SqlDbType.Int);
			cmd.Parameters.Add("@dateneed", SqlDbType.DateTime);
			cmd.Parameters.Add("@mitmcode", SqlDbType.BigInt);
			foreach (DataGridItem itm in dgItems.Items)
			{
				HiddenField phdnMitmCode = (HiddenField)itm.FindControl("hdnMitmCode");
				TextBox ptxtItemDesc = (TextBox)itm.FindControl("txtItemDesc");
				TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
				TextBox ptxtQty = (TextBox)itm.FindControl("txtQty");
    GrayMatterSoft.GMDatePicker pdteDateNeeded = (GrayMatterSoft.GMDatePicker)itm.FindControl("dteDateNeeded");

				cmd.Parameters["@itemdesc"].Value = ptxtItemDesc.Text;
				cmd.Parameters["@itemspec"].Value = ptxtItemSpec.Text;
				cmd.Parameters["@qty"].Value = ptxtQty.Text;
				cmd.Parameters["@dateneed"].Value = pdteDateNeeded.Date;
				cmd.Parameters["@mitmcode"].Value = phdnMitmCode.Value;
				cmd.ExecuteNonQuery();
			}

			if (!blnHeadRequired)
			{
				clsMRCF mrcf = new clsMRCF(txtMrcfCode.Text);
				mrcf.CheckDateNeeded();
			}
		}

		if (blnHeadRequired)
			clsMRCF.SendNotification(clsMRCF.MRCFMailType.SentToApproverDH, txtRequestorName.Text, txtDiviHeadName.Text, hdnDiviHeadMail.Value, txtMrcfCode.Text);
		else
			clsMRCF.SendNotification(clsMRCF.MRCFMailType.SentToApproverPM, txtRequestorName.Text, txtProcMngrName.Text, hdnProcMngrMail.Value, txtMrcfCode.Text);
		clsMRCF.SendNotification(clsMRCF.MRCFMailType.ApproveToRequestor, txtRequestorName.Text, txtGrpHeadName.Text, hdnRequestorMail.Value, txtMrcfCode.Text);
		clsMRCF.SendNotification(clsMRCF.MRCFMailType.ApproveToApproverGH, txtRequestorName.Text, txtGrpHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtMrcfCode.Text);

		Response.Redirect("MRCFMenu.aspx");
	}

    protected void btnDisApprove_Click(object sender, EventArgs e)
	{
		clsMRCF mrcf = new clsMRCF(txtMrcfCode.Text);
		mrcf.DisapproveGH(txtGrpHeadRem.Text);

		clsMRCF.SendNotification(clsMRCF.MRCFMailType.DisapproveToRequestor, txtRequestorName.Text, txtGrpHeadName.Text, hdnRequestorMail.Value, txtMrcfCode.Text);
  clsMRCF.SendNotification(clsMRCF.MRCFMailType.DisapproveToApproverGH, txtRequestorName.Text, txtGrpHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtMrcfCode.Text);

		Response.Redirect("MRCFMenu.aspx");
	}

    protected void btnModify_Click(object sender, EventArgs e)
	{
		clsMRCF mrcf = new clsMRCF(txtMrcfCode.Text);
		mrcf.ModificationGH(txtGrpHeadRem.Text);

		clsMRCF.SendNotification(clsMRCF.MRCFMailType.ModificationToRequestor, txtRequestorName.Text, txtGrpHeadName.Text, hdnRequestorMail.Value, txtMrcfCode.Text);
  clsMRCF.SendNotification(clsMRCF.MRCFMailType.ModificationToApproverGH, txtRequestorName.Text, txtGrpHeadName.Text, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtMrcfCode.Text);

		Response.Redirect("MRCFMenu.aspx");
	}

	protected void chkShowSpecification_CheckedChanged(object sender, EventArgs e)
	{
		SpecificationVisibility(chkShowSpecification.Checked);
	}

}
