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

public partial class Admin_ApproverSettingsRequ : System.Web.UI.Page
{

	private void BindDivision()
	{
		ddlDivision.DataSource = clsDivision.GetDdlDs().DefaultView;
		ddlDivision.DataValueField = "pvalue";
		ddlDivision.DataTextField = "ptext";
		ddlDivision.DataBind();

		if (Request.QueryString["divicode"] != "")
		{
			foreach (ListItem itm in ddlDivision.Items)
			{
    if (itm.Value == Request.QueryString["divicode"])
    {
     itm.Selected = true; break;
    }
			}
		}
	}

	private void BindRC()
	{
		ddlRC.DataSource = clsRC.GetDdlDsByDivisionCode(ddlDivision.SelectedValue);
		ddlRC.DataValueField = "pvalue";
		ddlRC.DataTextField = "ptext";
		ddlRC.DataBind();
	}

	private void BindDataGrid()
	{
		DataTable tblApprovers = new DataTable();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT HR.Rc.rccode,rcname,Users.Users.username,firname + ' ' + lastname AS pname FROM Users.Users INNER JOIN (CIS.RequisitionApprover INNER JOIN HR.Rc ON CIS.RequisitionApprover.rccode = Hr.Rc.rccode) ON Users.Users.username = CIS.RequisitionApprover.username WHERE CIS.RequisitionApprover.division='" + ddlDivision.SelectedValue + "' AND userlvl='sprv' UNION SELECT HR.Rc.rccode,rcname,'' AS username,'' AS pname FROM CIS.RequisitionApprover INNER JOIN HR.Rc ON CIS.RequisitionApprover.rccode = Hr.Rc.rccode WHERE CIS.RequisitionApprover.division='" + ddlDivision.SelectedValue + "' AND HR.Rc.rccode NOT IN (SELECT rccode FROM CIS.RequisitionApprover WHERE CIS.RequisitionApprover.division='" + ddlDivision.SelectedValue + "' AND userlvl='sprv') ORDER BY rcname,pname";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(tblApprovers);
		}
		dgApprovers.DataSource = tblApprovers;
		dgApprovers.DataBind();

		foreach (DataGridItem itm in dgApprovers.Items)
		{
			HiddenField phdnUsername = (HiddenField)itm.FindControl("hdnUsername");
			ImageButton pbtnDelete = (ImageButton)itm.FindControl("btnDelete");
			if (phdnUsername.Value == "")
				pbtnDelete.Visible = false;
		}
	}

	private void BindApprovers()
	{
		DataTable tblApprovers = new DataTable();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT username,firname + ' ' + lastname AS pname FROM Users.Users WHERE username NOT IN (SELECT username FROM CIS.RequisitionApprover WHERE rccode='" + ddlRC.SelectedValue + "') AND pstatus='1' ORDER BY firname";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(tblApprovers);
		}
		ddlApprover.DataSource = tblApprovers.DefaultView;
		ddlApprover.DataValueField = "username";
		ddlApprover.DataTextField = "pname";
		ddlApprover.DataBind();
	}

	protected void Page_Load(object sender, EventArgs e)
 {
     if (clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"]) != "023")
     {
         Response.Redirect("~/AccessDenied.aspx");
     }
		if (!Page.IsPostBack)
		{
			BindDivision();
			BindRC();
			BindApprovers();
			BindDataGrid();
		}
 }

	protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindRC();
		BindDataGrid();
	}

	protected void dgApprovers_DeleteCommand(object source, DataGridCommandEventArgs e)
	{
		HiddenField phdnUsername = (HiddenField)e.Item.FindControl("hdnUsername");
		HiddenField phdnRcCode = (HiddenField)e.Item.FindControl("hdnRCCode");
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "DELETE FROM CIS.RequisitionApprover WHERE rccode='" + phdnRcCode.Value + "' AND username='" + phdnUsername.Value + "' AND userlvl='sprv'";
			cn.Open();
			cmd.ExecuteNonQuery();
		}
		dgApprovers.EditItemIndex = -1;
		if (dgApprovers.Items.Count == 1)
			dgApprovers.CurrentPageIndex = dgApprovers.CurrentPageIndex - 1;
		BindDataGrid();
	}

	protected void ddlRC_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindApprovers();
	}

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "INSERT INTO CIS.RequisitionApprover VALUES('" + ddlApprover.SelectedValue + "','" + ddlDivision.SelectedValue + "','" + ddlRC.SelectedValue + "','sprv','1','1','1')";
			cn.Open();
			cmd.ExecuteNonQuery();
		}
		Response.Redirect("ApproverSettingsRequ.aspx?divicode=" + ddlDivision.SelectedValue);
	}

}
