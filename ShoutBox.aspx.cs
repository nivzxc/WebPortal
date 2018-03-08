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
using System.IO;

public partial class ShoutBox : System.Web.UI.Page
{

	protected void LoadShoutBox()
	{
		string strWrite;
		int intCtr = 0;
		int intPage = Convert.ToInt32(Request.QueryString["page"]);
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intStart = ((intPage - 1) * intPageSize) + 1;
		int intEnd = intPage * intPageSize;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM (SELECT username,msgbody,datesent,ROW_NUMBER() OVER(ORDER BY pinned DESC,datesent DESC) AS RowNum FROM Speedo.ShoutBox) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = "<tr>" +
																"<td class='GridRows' style='text-align:center;'>" +
																	"<img src='http://hq.sti.edu/Pictures/avatar/" + (File.Exists(Server.MapPath("~/pictures/avatar/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='50' height='50'>" +
																"</td>" +
																"<td class='GridRows'>" +
																	"<a href='' style='font-size:small;'>" +	dr["username"] + "</a>" + "<br>" +
																		"&nbsp;>" + dr["datesent"] +
                  "<br>&nbsp;>" + clsSpeedo.GetDateDetails(Convert.ToDateTime(dr["datesent"].ToString())) +	
																"</td>" +
																"<td class='GridRows' style='horizontal-align:middle;font-size:small;'>" + dr["msgbody"] + "</td>" +
															"</tr>";
				Response.Write(strWrite);
				intCtr++;
			}

			dr.Close();
		}
	}

	protected void LoadPaging()
	{
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intTRows = 0;
		int intTRowsTemp = 0;
		int intPage = 1;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(username) AS tcount FROM Speedo.ShoutBox";
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
				Response.Write("&nbsp;&nbsp;<a href='ShoutBox.aspx?page=" + intPage + "'>" + intPage + "</a>");
			intPage++;
			intTRowsTemp -= intPageSize;
		}
	}

 protected void Page_Load(object sender, EventArgs e)
 {

 }

}
