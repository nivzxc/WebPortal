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

public partial class CIS_Requisition_RequAll : System.Web.UI.Page
{

	protected void LoadRequisition()
	{
		string strWrite = "";
		int intCtr = 0;
		int intPage = Convert.ToInt32(Request.QueryString["page"]);
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
		int intStart = ((intPage - 1) * intPageSize) + 1;
		int intEnd = intPage * intPageSize;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM (SELECT requcode,userrem,datereq,sprvcode,sprvstat,headcode,headstat,suppcode,suppstat,status,username,totcost,ROW_NUMBER() OVER(ORDER BY datereq) AS RowNum FROM CIS.Requisition WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
					                      "<td class='GridRows'>" +
																											 "<a href='RequDetails.aspx?requcode=" + dr["requcode"] + "'><img src='../../Support/" + clsRequisition.GetRequestStatusIcon(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["suppcode"].ToString(), dr["suppstat"].ToString()) + "' alt='' /></a>" +
																											"</td>" +
																											"<td class='GridRows'>" +
																												"<a href='RequDetails.aspx?requcode=" + dr["requcode"] + "' style='font-size:small;'>" + dr["userrem"] + "</a><br>" +
																												"Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
																												"Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
																												"Total Cost: P " + Convert.ToDouble(dr["totcost"]).ToString("###,##0.00") +
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
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
		int intTRows = 0;
		int intTRowsTemp = 0;
		int intPage = 1;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(requcode) FROM CIS.Requisition WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
			cn.Open();
			try
			{
				intTRows = (int)cmd.ExecuteScalar();
			}
			catch
			{
				intTRows = 0;
			}
		}

		intTRowsTemp = intTRows;

		while (intTRowsTemp > 0)
		{
			if (Convert.ToInt32(Request.QueryString["page"]) == intPage)
				Response.Write((intPage == 1 ? "" : ",") + "&nbsp;" + intPage + "");
			else
				Response.Write("&nbsp;&nbsp;<a href='RequAll.aspx?page=" + intPage + "'>" + intPage + "</a>");
			intPage++;
			intTRowsTemp -= intPageSize;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
 }

}
