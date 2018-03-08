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

public partial class HR_SurveyResult : System.Web.UI.Page
{
	/// <summary>
	/// Get the total participant in the survey
	/// </summary>
	/// <returns>Total participant in the survey</returns>
	protected int GetTotalParticipants()
	{
		int intResult;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(DISTINCT username) FROM survey_users_answers";
			cn.Open();
			try
			{
				intResult = (int)cmd.ExecuteScalar();
			}
			catch
			{
				intResult = 0;
			}
		}
		return intResult;
	}

	protected void Load_Result(string strSrvyCode)
	{
		int intTEmp = GetTotalParticipants();
		int intTotal;
		int intItemCount;
		string strWrite;
		DataTable tblCategory = new DataTable();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT scatcode,scatname FROM survey_category WHERE srvycode='" + strSrvyCode + "' ORDER by scatordr";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			SqlDataReader dr;
			cn.Open();
			da.Fill(tblCategory);
			foreach (DataRow drw in tblCategory.Rows)
			{
				intTotal = 0;
				intItemCount = 0;
				strWrite = "<tr>" +
																"<td class='GridRows2' style='font-size:small;'>&nbsp;<b>" + drw["scatname"] + "</b></td>" +
																"<td class='GridRows2' style='font-size:small;'>&nbsp;</td>" +
																"<td class='GridRows2' style='font-size:small;'>&nbsp;</td>" + 
															"</tr>";

				Response.Write(strWrite);
				cmd.CommandText = "SELECT itemdesc,itemnmbr,SUM(CAST(answer as INTEGER)) AS tans " +
																						"FROM survey_items INNER JOIN survey_users_answers ON survey_items.itemcode = survey_users_answers.itemcode " +
                      "WHERE survey_items.srvycode='" + strSrvyCode + "' AND scatcode='" + drw["scatcode"] + "' " + 
																						"GROUP BY itemnmbr,itemdesc " + 
																						"ORDER BY CAST(itemnmbr AS INTEGER)";
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					strWrite = "<tr>" +
																	"<td class='GridRows' style='font-size:small;'>&nbsp;" + dr["itemnmbr"] + ".&nbsp;" + dr["itemdesc"] + "</td>" +
																	"<td class='GridRows' style='text-align:center;'>" + dr["tans"] + "</td>" +
																	"<td class='GridRows' style='text-align:center;'>" + Convert.ToDouble((Convert.ToDouble(dr["tans"]) / (double)intTEmp)).ToString("##0.00") + "</td>" + 
																"</tr>";
					Response.Write(strWrite);
					intTotal += (int)dr["tans"];
					intItemCount++;
				}
				dr.Close();
				strWrite = "<tr>" +
																"<td class='GridRows2' style='font-size:small; text-align:left;'>&nbsp;<b>Category Total</b></td>" +
																"<td class='GridRows2' style='text-align:center;'><b>" + intTotal + "</b></td>" +
																"<td class='GridRows2' style='text-align:center;'><b>" + Convert.ToDouble(((double)intTotal / ((double)intTEmp * intItemCount))).ToString("##0.00") + "</b></td>" +
															"</tr>" +
				           "<tr><td class='GridRows' colspan='3'>&nbsp;</td></tr>";
				Response.Write(strWrite);
			}
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {

 }

}
