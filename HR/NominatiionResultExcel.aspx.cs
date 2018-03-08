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

public partial class HR_NominatiionResultExcel : System.Web.UI.Page
{

	protected void LoadRecords()
	{
		string strWrite = "";
		DataTable tblAttributes = new DataTable();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			string strDiviCode = Request.QueryString["divicode"].ToString();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM attributes ORDER BY attrname";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			SqlDataReader dr;
			cn.Open();
			da.Fill(tblAttributes);
			foreach (DataRow drow in tblAttributes.Rows)
			{
				strWrite = "<br /><br />" +
																"<table width='50%' class='grid' border='1'>" +
																	"<tr>" +
																		"<td class='GridColumns' style='width:70%px;text-align:center;background-color:#ffffe0'><b>Employee</b></td>" +
																		"<td class='GridColumns' style='width:30%px;text-align:center;background-color:#ffffe0'><b>" + drow["attrname"] + "</b></td>" +
																	"</tr>";
				Response.Write(strWrite);
				if (strDiviCode == "ALL")
				{
					if (Request.QueryString["filter"].ToString() == "3")
					{
						cmd.CommandText = "SELECT TOP 3 firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
																								"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																								"WHERE attrcode='" + drow["attrcode"] + "' " +
																								"GROUP BY firname,lastname,divicode ORDER BY tvote DESC";
					}
					else
					{
						cmd.CommandText = "SELECT firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
																								"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																								"WHERE attrcode='" + drow["attrcode"] + "' " +
																								"GROUP BY firname,lastname,divicode ORDER BY tvote DESC";
					}
				}
				else
				{
					if (Request.QueryString["filter"].ToString() == "3")
					{
						cmd.CommandText = "SELECT TOP 3 firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
																								"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																								"WHERE divicode='" + strDiviCode + "' AND attrcode='" + drow["attrcode"] + "' " +
																								"GROUP BY firname,lastname,divicode ORDER BY tvote DESC";
					}
					else
					{
						cmd.CommandText = "SELECT firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
																								"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																								"WHERE divicode='" + strDiviCode + "' AND attrcode='" + drow["attrcode"] + "' " +
																								"GROUP BY firname,lastname,divicode ORDER BY tvote DESC";
					}
				}
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Response.Write("<tr><td class='GridRows'>" + dr["wname"] + " <i>" + dr["divicode"] + "</i></td><td class='GridRows' style='text-align:center'>" + dr["tvote"] + "</td></tr>");
				}
				dr.Close();
				Response.Write("</table>");
			}
		}
	}

 protected void Page_Load(object sender, EventArgs e)
 {
		Response.Clear();
		Response.AddHeader("content-disposition", "attachment;filename=NominationResults.xls");
		Response.Charset = "";
		Response.Cache.SetCacheability(HttpCacheability.NoCache);
		Response.ContentType = "application/vnd.xls";
 }

}
