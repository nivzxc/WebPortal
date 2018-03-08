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
public partial class CMD_CRS_CRSAllEU : System.Web.UI.Page
{

	protected void LoadCRS()
	{
		string strWrite = "";
		int intTotal = 0;
		int intForApproval = 0;
		int intDisapproved = 0;
		int intForProcessing = 0;
		int intPartial = 0;
		int intComplete = 0;

		int intPage = (Convert.ToInt32(Request.QueryString["page"]) == 0 ? 1 : Convert.ToInt32(Request.QueryString["page"]));

		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intStart = ((intPage - 1) * intPageSize) + 1;
		int intEnd = intPage * intPageSize;

		DataTable tblCRS = new DataTable();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlDataReader dr;
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM (SELECT crscode,schlcode,datereq,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CM.CRS) as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			cn.Open();
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(tblCRS);

			foreach (DataRow drow in tblCRS.Rows)
			{
				intTotal = 0;
				intForApproval = 0;
				intDisapproved = 0;
				intForProcessing = 0;
				intPartial = 0;
				intComplete = 0;

				cmd.CommandText = "SELECT pstatus FROM CM.CrsDetails WHERE crscode='" + drow["crscode"] + "'";
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					intTotal++;
					if (dr["pstatus"].ToString() == "F")
						intForApproval++;
					else if (dr["pstatus"].ToString() == "D")
						intDisapproved++;
					else if (dr["pstatus"].ToString() == "E")
						intForProcessing++;
					else if (dr["pstatus"].ToString() == "P")
						intPartial++;
					else if (dr["pstatus"].ToString() == "C")
						intComplete++;
				}
				dr.Close();

				strWrite = strWrite + "<tr>" +
																											"<td class='GridRows'>" +
																												"<a href='CRSDetailsEU.aspx?crscode=" + drow["crscode"] + "'><img src='../../Support/" + clsCRS.GetRequestStatusIcon(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "' alt='' /></a>" +
																											"</td>" +
																											"<td class='GridRows'>" +
																												"<a href='CRSDetailsEU.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'>" + clsSchool.GetSchoolName(drow["schlcode"].ToString()) + "</a><br>" +
																												"Date Requested: " + Convert.ToDateTime(drow["datereq"]).ToString("MMMM dd, yyyy") + "</td>" +
																											"<td class='GridRows'>" + clsCRS.GetRequestStatusRemarks(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "</td>" +
																										"</tr>";
			}
		}
		Response.Write(strWrite);

		if (tblCRS.Rows.Count == 0)
			Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
		else
			Response.Write("<tr><td colspan='3' class='GridRows'>[" + tblCRS.Rows.Count + " Records found]</td></tr>");
	}

	protected void LoadPaging()
	{
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intTRows = 0;
		int intTRowsTemp = 0;
		int intPage = 1;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(crscode) AS tcount FROM CM.CRS";
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
				Response.Write("&nbsp;&nbsp;<a href='CRSAllEU.aspx'>" + intPage + "</a>");
			intPage++;
			intTRowsTemp -= intPageSize;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

}
