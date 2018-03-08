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

public partial class HR_NominationVotee : System.Web.UI.Page
{

	protected void Load_Records()
	{
		string strWrite = "";
		DataTable tblAttributes = new DataTable();
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			string strUsername = Request.QueryString["username"].ToString();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM attributes ORDER BY attrname";
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			SqlDataReader dr;
			cn.Open();
			da.Fill(tblAttributes);
			foreach (DataRow drow in tblAttributes.Rows)
			{
				strWrite = "<br /><br />" +
															"<div class='GridBorder' style='width:40%'>" +
																"<table width='100%' class='grid'>" +
																	"<tr>" +
																		"<td class='GridColumns'><b>" + drow["attrname"] + "</b></td>" +
																	"</tr>";
				Response.Write(strWrite);
				cmd.CommandText = "SELECT firname + ' ' + lastname AS wname, divicode " +
																						"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.username) ON users_division.username = users.username " +
																						"WHERE attrcode='" + drow["attrcode"] + "' AND nomuname='" + strUsername + "' " +
																						"ORDER BY firname DESC";
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Response.Write("<tr><td class='GridRows'>" + dr["wname"] + " <i>" + dr["divicode"] + "</i></td></tr>");
				}
				dr.Close();
				Response.Write("</table></div>");
			}
		}
	}

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
