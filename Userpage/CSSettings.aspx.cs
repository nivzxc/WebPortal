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
using STIeForms;
using HRMS;

public partial class Userpage_CSSettings : System.Web.UI.Page
{

 private void BindOvertimeSettings()
 {
  dgOvertime.DataSource = clsModuleApprover.DSGLevel2(clsModule.OvertimeModule, Request.Cookies["Speedo"]["UserName"]);
  dgOvertime.DataBind();
  foreach (DataGridItem ditm in dgOvertime.Items)
  {
   HiddenField phdnDepartmentCode = (HiddenField)ditm.FindControl("hdnDepartmentCode");
   Label plblDepartmentName = (Label)ditm.FindControl("lblDepartmentName");
   HiddenField phdnApproval = (HiddenField)ditm.FindControl("hdnApproval");
   DropDownList pddlApproval = (DropDownList)ditm.FindControl("ddlApproval");

   plblDepartmentName.Text = clsDepartment.GetName(phdnDepartmentCode.Value);
   pddlApproval.SelectedValue = phdnApproval.Value;
  }
 }
 
	protected void Page_Load(object sender, EventArgs e)
 {
		if (!Page.IsPostBack)
		{
   BindOvertimeSettings();

			DataTable tblMRCFPermission = new DataTable();
			DataTable tblRequPermission = new DataTable();
			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandText = "SELECT division,CIS.MrcfApprover.rccode,rcname,email,approve FROM CIS.MrcfApprover INNER JOIN Hr.Rc ON CIS.MRCFApprover.rccode = Hr.Rc.rccode WHERE pstatus='1' AND username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head' ORDER BY division,rcname";
				SqlDataReader dr;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				cn.Open();
				da.Fill(tblMRCFPermission);
				dgRC.DataSource = tblMRCFPermission;
				dgRC.DataBind();

				cmd.CommandText = "SELECT division,CIS.RequisitionApprover.rccode,rcname,email,approve FROM CIS.RequisitionApprover INNER JOIN Hr.Rc ON CIS.RequisitionApprover.rccode = Hr.Rc.rccode WHERE pstatus='1' AND username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head' ORDER BY division,rcname";
				da.SelectCommand = cmd;
				da.Fill(tblRequPermission);
				dgRequRC.DataSource = tblRequPermission;
				dgRequRC.DataBind();

				foreach (DataGridItem ditm in dgRC.Items)
				{
					Label plblDivision = (Label)ditm.FindControl("lblDivision");
					HiddenField phdnRCCOde = (HiddenField)ditm.FindControl("hdnRCCOde");
					DropDownList pddlEmail = (DropDownList)ditm.FindControl("ddlEmail");
					DropDownList pddlApproval = (DropDownList)ditm.FindControl("ddlApproval");

					cmd.CommandText = "SELECT username FROM CIS.MrcfApprover WHERE division='" + plblDivision.Text + "' AND rccode='" + phdnRCCOde.Value + "' AND userlvl='sprv'";
					dr = cmd.ExecuteReader();
					if (!dr.Read())
					{
						pddlEmail.Enabled = false;
						pddlApproval.Enabled = false;
					}
					dr.Close();
				}

				foreach (DataGridItem ditm in dgRequRC.Items)
				{
					Label plblDivision = (Label)ditm.FindControl("lblDivision");
					HiddenField phdnRCCOde = (HiddenField)ditm.FindControl("hdnRCCOde");
					DropDownList pddlEmail = (DropDownList)ditm.FindControl("ddlEmail");
					DropDownList pddlApproval = (DropDownList)ditm.FindControl("ddlApproval");

					cmd.CommandText = "SELECT username FROM CIS.RequisitionApprover WHERE division='" + plblDivision.Text + "' AND rccode='" + phdnRCCOde.Value + "' AND userlvl='sprv'";
					dr = cmd.ExecuteReader();
					if (!dr.Read())
					{
						pddlEmail.Enabled = false;
						pddlApproval.Enabled = false;
					}
					dr.Close();
				}
			}

           trMRCF.Visible = (clsMRCF.GetUserType(Request.Cookies["Speedo"]["UserName"].ToString()) == clsMRCF.MRCFUserType.DivisionHead);
           trMRCFSpacer.Visible = trMRCF.Visible;

           trRequisition.Visible = (clsRequisition.GetUserType(Request.Cookies["Speedo"]["UserName"].ToString()) == clsRequisition.RequisitionUserType.DivisionHead);
           trRequisitionSpacer.Visible = trRequisitionSpacer.Visible;
		}
 }

	protected void dgRC_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			DropDownList pddlApproval = (DropDownList)e.Item.FindControl("ddlApproval");
			HiddenField phdnApproval = (HiddenField)e.Item.FindControl("hdnApproval");
			DropDownList pddlEmail= (DropDownList)e.Item.FindControl("ddlEmail");
			HiddenField phdnEmail = (HiddenField)e.Item.FindControl("hdnEmail");

			if (phdnApproval.Value == "0")
				pddlApproval.Items[0].Selected = true;
			else if (phdnApproval.Value == "1")
				pddlApproval.Items[1].Selected = true;

			if (phdnEmail.Value == "0")
				pddlEmail.Items[0].Selected = true;
			else if (phdnEmail.Value == "1")
				pddlEmail.Items[1].Selected = true;
		}
	}

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cn.Open();
			foreach (DataGridItem dgi in dgRC.Items)
			{
				HiddenField phdnRCCode = (HiddenField)dgi.FindControl("hdnRCCode");
				DropDownList pddlEmail= (DropDownList)dgi.FindControl("ddlEmail");
				DropDownList pddlApproval = (DropDownList)dgi.FindControl("ddlApproval");
				cmd.CommandText = "UPDATE CIS.MrcfApprover SET email='" + pddlEmail.SelectedValue + "',approve='" + pddlApproval.SelectedValue + "' WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head' and pstatus='1' AND rccode='" + phdnRCCode.Value + "'";
				cmd.ExecuteNonQuery();
			}
		}
		Response.Redirect("CSSettings.aspx");
	}

	protected void dgRequRC_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			DropDownList pddlApproval = (DropDownList)e.Item.FindControl("ddlApproval");
			HiddenField phdnApproval = (HiddenField)e.Item.FindControl("hdnApproval");
			DropDownList pddlEmail = (DropDownList)e.Item.FindControl("ddlEmail");
			HiddenField phdnEmail = (HiddenField)e.Item.FindControl("hdnEmail");

			if (phdnApproval.Value == "0")
				pddlApproval.Items[0].Selected = true;
			else if (phdnApproval.Value == "1")
				pddlApproval.Items[1].Selected = true;

			if (phdnEmail.Value == "0")
				pddlEmail.Items[0].Selected = true;
			else if (phdnEmail.Value == "1")
				pddlEmail.Items[1].Selected = true;
		}
	}
	protected void btnRequSave_Click(object sender, ImageClickEventArgs e)
	{
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cn.Open();
			foreach (DataGridItem dgi in dgRequRC.Items)
			{
				HiddenField phdnRCCode = (HiddenField)dgi.FindControl("hdnRCCode");
				DropDownList pddlEmail = (DropDownList)dgi.FindControl("ddlEmail");
				DropDownList pddlApproval = (DropDownList)dgi.FindControl("ddlApproval");
				cmd.CommandText = "UPDATE CIS.RequisitionApprover SET email='" + pddlEmail.SelectedValue + "',approve='" + pddlApproval.SelectedValue + "' WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' AND userlvl='head' and pstatus='1' AND rccode='" + phdnRCCode.Value + "'";
				cmd.ExecuteNonQuery();
			}
		}
		Response.Redirect("CSSettings.aspx");
	}

 protected void btnSaveOvertime_Click(object sender, ImageClickEventArgs e)
 {
  foreach (DataGridItem ditm in dgOvertime.Items)
  {
   HiddenField phdnMappCode = (HiddenField)ditm.FindControl("hdnMappCode");
   DropDownList pddlApproval = (DropDownList)ditm.FindControl("ddlApproval");
   clsModuleApprover.UpdateRequiredApprovalFlag(phdnMappCode.Value, pddlApproval.SelectedValue, Request.Cookies["Speedo"]["UserName"]);
  }
 }

}