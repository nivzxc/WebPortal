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

public partial class CIS_Requisition_RequAllSC : System.Web.UI.Page
{

	protected void LoadRequisition()
	{
		string strWrite = "";
		int intCtr = 0;
		int intPage = (Convert.ToInt32(Request.QueryString["page"]) == 0 ? 1 : Convert.ToInt32(Request.QueryString["page"]));
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intStart = ((intPage - 1) * intPageSize) + 1;
		int intEnd = intPage * intPageSize;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (radModeAll.Checked)
				cmd.CommandText = "SELECT * FROM (SELECT totcost,userrem,requcode,username,datereq,status,sprvstat,sprvcode,headstat,headcode,suppcode,suppstat,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Requisition WHERE suppcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status NOT IN ('V','D') AND status IN ('A','P','C')) as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			else if (radModeProcessed.Checked)
				cmd.CommandText = "SELECT * FROM (SELECT totcost,userrem,requcode,username,datereq,status,sprvstat,sprvcode,headstat,headcode,suppcode,suppstat,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Requisition WHERE suppcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='C') as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			else if (radModeForApproval.Checked)
				cmd.CommandText = "SELECT * FROM (SELECT totcost,userrem,requcode,username,datereq,status,sprvstat,sprvcode,headstat,headcode,suppcode,suppstat,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Requisition WHERE suppcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status IN ('A','P') ) as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
					                      "<td class='GridRows'>" +
																											 "<a href='RequDetailsSC.aspx?requcode=" + dr["requcode"] + "'><img src='../../Support/" + clsRequisition.GetRequestStatusIcon(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["suppcode"].ToString(), dr["suppstat"].ToString()) + "' alt='' /></a>" +
																											"</td>" +
																											"<td class='GridRows'>" +
																											 "<a href='RequDetailsSC.aspx?requcode=" + dr["requcode"] + "' style='font-size:small'>" + dr["userrem"] + "</a> by <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
																												"Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
																												"Cost: P " + Convert.ToDouble(dr["totcost"]).ToString("###,##0.00") +
																											"</td>" +
																											"<td class='GridRows'>" + clsRequisition.GetRequestStatusRemarks(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["suppcode"].ToString(), dr["suppstat"].ToString()) + "</td>" +
																										"</tr>";				
				intCtr++;
			}
			dr.Close();
		}
		Response.Write(strWrite);

		if (intCtr == 0)
			Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
		else
			Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " record(s) found ]</td></tr>");
	}

	protected void LoadPaging()
	{
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intTRows = 0;
		int intTRowsTemp = 0;
		int intPage = 1;
		string strMode = "";
		if (radModeAll.Checked)
			strMode = "a";
		else if (radModeForApproval.Checked)
			strMode = "f";
		else if (radModeProcessed.Checked)
			strMode = "p";

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (radModeAll.Checked)
				cmd.CommandText = "SELECT COUNT(requcode) AS tcount FROM CIS.Requisition WHERE suppcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status NOT IN ('V','D') AND status IN ('A','P','C')";
			else if (radModeProcessed.Checked)
				cmd.CommandText = "SELECT COUNT(requcode) AS tcount FROM CIS.Requisition WHERE suppcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status IN('P','C')";
			else if (radModeForApproval.Checked)
				cmd.CommandText = "SELECT COUNT(requcode) AS tcount FROM CIS.Requisition WHERE suppcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='A' AND suppstat='F'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			dr.Read();
			if (!Convert.IsDBNull(dr["tcount"]))
				intTRows = Convert.ToInt32(dr["tcount"]);
			dr.Close();
		}

		intTRowsTemp = intTRows;

		while (intTRowsTemp > 0)
		{
			if (Convert.ToInt32(Request.QueryString["page"]) == intPage)
				Response.Write((intPage == 1 ? "" : ",") + "&nbsp;" + intPage + "");
			else
				Response.Write("&nbsp;&nbsp;<a href='RequAllSC.aspx?page=" + intPage + "&mode=" + strMode + "'>" + intPage + "</a>");
			intPage++;
			intTRowsTemp -= intPageSize;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
		if (!Page.IsPostBack)
		{
			if (Request.QueryString["mode"] == "p")
				radModeProcessed.Checked = true;
			else if (Request.QueryString["mode"] == "f")
				radModeForApproval.Checked = true;
			else
				radModeAll.Checked = true;
		}
 }

}
