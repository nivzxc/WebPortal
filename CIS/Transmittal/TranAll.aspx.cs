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

public partial class CIS_Transmittal_TranAll : System.Web.UI.Page
{

	protected void LoadRecords()
	{
		string strWrite ="";
		int intCtr = 0;
		int intPage = Convert.ToInt32(Request.QueryString["page"]);
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
		int intStart = ((intPage - 1) * intPageSize) + 1;
		int intEnd = intPage * intPageSize;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM (SELECT trancode,username,itemdesc,datereq,disptype,grphname,grphstat,appname,appstat,status,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Transmittal WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
					                      "<td class='GridRows'>" +
																											 "<a href='TranDetails.aspx?trancode=" + dr["trancode"] + "'><img src='../../Support/" + clsTransmittal.GetRequestStatusIcon(dr["disptype"].ToString(), dr["status"].ToString(), dr["grphname"].ToString(), dr["grphstat"].ToString(), dr["appname"].ToString(), dr["appstat"].ToString()) + "' alt='' /></a>" +
																											"</td>" +
																											"<td class='GridRows'>" +
																											 "<a href='TranDetails.aspx?trancode=" + dr["trancode"] + "' style='font-size:small;'>" + dr["itemdesc"] + "</a>" +
																												"<br>Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") +
																											"</td>" +
																											"<td class='GridRows'>" + clsTransmittal.GetRequestStatusRemarks(dr["disptype"].ToString(), dr["status"].ToString(), dr["grphname"].ToString(), dr["grphstat"].ToString(), dr["appname"].ToString(), dr["appstat"].ToString()) + "</td>" +
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

	protected void Load_Paging()
	{
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
		int intTRows = 0;
		int intTRowsTemp = 0;
		int intPage = 1;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(trancode) AS tcount FROM CIS.Transmittal WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
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
				Response.Write("&nbsp;&nbsp;<a href='TranAll.aspx?page=" + intPage + "'>" + intPage + "</a>");
			intPage++;
			intTRowsTemp -= intPageSize;
		}
	}
 
	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
 }

}
