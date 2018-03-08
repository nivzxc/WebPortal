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

public partial class Academics_CWIMain : System.Web.UI.Page
{

	protected void Load_Records()
	{
		string strWrite = "";		
		int intCtr = 0;
		int intPage = (Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"]));
		int intPageSize = 100;
		int intEnd = intPage * intPageSize;

		SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString());
		SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString());
		SqlCommand cmdOmega = cnOmega.CreateCommand();
		SqlCommand cmd = cn.CreateCommand(); 
		SqlDataReader dr;
		cmdOmega.CommandText = "SELECT DISTINCT dbo_subject.subject_code,dbo_subject.subject_name " +
																									"FROM " +
																									"(" +
																									 "SELECT DISTINCT TOP 100 subject_code,subject_name " +
																									 "FROM " +
																									 "(" +
																									  "SELECT DISTINCT TOP " + intEnd + " subject_code,subject_name " +
																									  "FROM dbo_subject " +
																											"WHERE subject_code LIKE '%" + Request.QueryString["coursecode"] + "%' AND subject_name LIKE '%" + Request.QueryString["coursetitle"] + "%' " +
																									  "ORDER BY subject_name,subject_code" +
																									 ") AS m " +
																									 "ORDER BY subject_name DESC, subject_code DESC" +
																									") AS p " +
																									"INNER JOIN dbo_subject ON p.subject_code = dbo_subject.subject_code " +
																									"ORDER by dbo_subject.subject_name, dbo_subject.subject_code";
		cnOmega.Open();
		cn.Open();
		SqlDataReader drOmega = cmdOmega.ExecuteReader();
		while (drOmega.Read())
		{
			strWrite = "<tr>" +
														"<td class='GridRows'>" +
															"<a href='CoursewareView.aspx?subjcode=" + drOmega["subject_code"] + "'>" + drOmega["subject_name"] + "</a> (" + drOmega["subject_code"] + ")" +
														"</td>";
			cmd.CommandText = "SELECT cwdstat,datecomp FROM Academics.CoursewareInventory WHERE crsecode='" + drOmega["subject_code"] + "'";
			dr = cmd.ExecuteReader();
			if (dr.Read())
				strWrite = strWrite + "<td class='GridRows'>" + clsCRS.ToCAStatusDesc(dr["cwdstat"].ToString()) + "</td>" +
																										"<td class='GridRows' style='text-align:center;'>" + (dr["cwdstat"].ToString() != "C" ? "" : Convert.ToDateTime(dr["datecomp"]).ToString("MM-dd-yyyy")) + "</td>" +
																										"</tr>";
			else
				strWrite = strWrite + "<td class='GridRows'>No Data</td>" +
																										"<td class='GridRows'>&nbsp;</td>" +
																										"</tr>";
			dr.Close();
			Response.Write(strWrite);
			intCtr++;
		}

		drOmega.Close();
		cnOmega.Close();
		cn.Close();

		if (intCtr == 0)
			Response.Write("<tr><td colspan='3' class='GridRows'>No Record Found.</td></tr>");
	}

	protected void Load_Paging()
	{
		int intPageSize = 100;
		int intTRows = 0;
		int intTRowsTemp = 0;
		int intPage = 1;

		using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
		{
			SqlCommand cmdOmega = cnOmega.CreateCommand();
			cmdOmega.CommandText = "SELECT COUNT(DISTINCT subject_code) AS tcount FROM dbo_subject WHERE subject_code LIKE '%" + Request.QueryString["coursecode"] + "%' AND subject_name LIKE '%" + Request.QueryString["coursetitle"] + "%'";
			cnOmega.Open();
			SqlDataReader drOmega = cmdOmega.ExecuteReader();
			drOmega.Read();
			if (!Convert.IsDBNull(drOmega["tcount"]))
				intTRows = Convert.ToInt32(drOmega["tcount"]);
			drOmega.Close();
		}

		intTRowsTemp = intTRows;

		while (intTRowsTemp > 0)
		{
			if (Convert.ToInt32(Request.QueryString["page"]) == intPage)
				Response.Write((intPage == 1 ? "" : ",") + "&nbsp;" + intPage + "");
			else
				Response.Write((intPage == 1 ? "" : ",") + "&nbsp;<a href='CWIMain.aspx?page=" + intPage + "&coursecode=" + Request.QueryString["coursecode"] + "&coursetitle=" + Request.QueryString["coursetitle"] + "'>" + intPage + "</a>");
			intPage++;
			intTRowsTemp -= intPageSize;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {

		if (!Page.IsPostBack)
		{
			txtCourseCode.Text = Request.QueryString["coursecode"];
			txtCourseTitle.Text = Request.QueryString["coursetitle"];
		}
 }

	protected void btnSearch_Click(object sender, ImageClickEventArgs e)
	{
		Response.Redirect("CWIMain.aspx?page=1&coursecode=" + txtCourseCode.Text + "&coursetitle=" + txtCourseTitle.Text);
	}

}
