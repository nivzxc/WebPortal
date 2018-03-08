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

public partial class CIS_Transmittal_TranDetails : System.Web.UI.Page
{

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			DataTable tblItems = new DataTable("Items");
			txtTransmittalCode.Text = Request.QueryString["trancode"];
   txtEmployeeName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

			clsTransmittal transmittal = new clsTransmittal();
   transmittal.TransmittalCode = txtTransmittalCode.Text;
			transmittal.Fill();
			txtDateRequested.Text = transmittal.DateRequested.ToString();
			txtItemDescription.Text = transmittal.ItemDescription; ;
			txtUnit.Text = transmittal.Unit; ;
			txtRemarks.Text = transmittal.Remarks; ;
			txtDispatchType.Text = transmittal.DispatchTypeDescription;
			hdnStatus.Value = transmittal.Status;
			txtStatus.Text = transmittal.StatusDescription;
			if (transmittal.DispatchType == "H" || transmittal.DispatchType == "S")
			{
				if (transmittal.DispatchType == "H")
					txtChargeTo.Text = clsRC.GetRCName(transmittal.ChargeTo);
				else if (transmittal.DispatchType == "S")
					txtChargeTo.Text = clsSchool.GetSchoolName(transmittal.ChargeTo);
    txtGroupHeadName.Text = clsUsers.GetName(transmittal.GroupHead);
				txtGroupHeadRemarks.Text = transmittal.GroupHeadRemarks;
				txtDateNeeded.Text = transmittal.DateNeeded.ToString();
				if (transmittal.GroupHeadStatus == "A" && transmittal.ApproverStatus == "P")
				{
     txtApproverName.Text = clsUsers.GetName(transmittal.Approver);
					txtApproverRemarks.Text = transmittal.ApproverRemarks;
				}
				else
				{
					txtApproverName.Text = "";
					txtApproverRemarks.Text = "";
				}

				trChargeTo.Visible = true;
				trDateNeeded.Visible = true;
				trGroupHead.Visible = true;
				trGroupHeadRem.Visible = true;
				trApproverName.Visible = true;
				trApproverRemarks.Visible = true;
			}

			dgItems.DataSource = transmittal.DSGItems().DefaultView;
			dgItems.DataBind();

			if (hdnStatus.Value == "F" || hdnStatus.Value == "A")
				divButtons.Visible = true;
			else
				divButtons.Visible = false;

			if (hdnStatus.Value == "C" || hdnStatus.Value == "V")
			{
				btnDelete.Visible = false;
				dgItems.Columns[3].Visible = false;
			}

			foreach (DataGridItem ditm in dgItems.Items)
			{
				CheckBox pchkDelete = (CheckBox)ditm.FindControl("chkDelete");
				HiddenField phdnItemStatus = (HiddenField)ditm.FindControl("hdnItemStatus");
				if (phdnItemStatus.Value == "0")
				{
					pchkDelete.Visible = true;					
				}
				else
				{
					pchkDelete.Visible = false;
					divButtons.Visible = false;
				}
			}


		}
 }

    protected void btnVoid_Click(object sender, EventArgs e)
	{
		clsTransmittal transmittal = new clsTransmittal();
  transmittal.TransmittalCode = txtTransmittalCode.Text;
		if(transmittal.Void())
			Response.Redirect("TranMenu.aspx");
	}

    protected void btnDelete_Click(object sender, EventArgs e)
	{
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "DELETE FROM CIS.TransmittalDetails WHERE trancode='" + txtTransmittalCode.Text + "' AND schlcode=@schlcode";
			cmd.Parameters.Add("@schlcode", SqlDbType.Char, 3);
			cn.Open();
			foreach (DataGridItem ditm in dgItems.Items)
			{
				HiddenField phdnItemStatus = (HiddenField)ditm.FindControl("hdnItemStatus");
				HiddenField phdnSchlCode = (HiddenField)ditm.FindControl("hdnSchlCode");
				CheckBox pchkDelete = (CheckBox)ditm.FindControl("chkDelete");
				if (phdnItemStatus.Value == "0" && pchkDelete.Checked)
				{
					cmd.Parameters["@schlcode"].Value = phdnSchlCode.Value;
					if(clsTransmittal.GetItemStatus(txtTransmittalCode.Text,phdnSchlCode.Value) == "0")
						cmd.ExecuteNonQuery();
				}
			}
		}
		clsTransmittal transmittal = new clsTransmittal();
  transmittal.TransmittalCode = txtTransmittalCode.Text;
		dgItems.DataSource = transmittal.DSGItems().DefaultView;
		dgItems.DataBind();
		foreach (DataGridItem ditm in dgItems.Items)
		{
			CheckBox pchkDelete = (CheckBox)ditm.FindControl("chkDelete");
			HiddenField phdnItemStatus = (HiddenField)ditm.FindControl("hdnItemStatus");
			pchkDelete.Visible = (phdnItemStatus.Value == "0" ? true : false);
		}
	}

}
