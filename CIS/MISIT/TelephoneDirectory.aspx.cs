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
using System.Text.RegularExpressions;

public partial class CIS_MISIT_TelephoneDirectory : System.Web.UI.Page
{
	protected void Load_Records()
	{
		int intCtr = 0;
		string strWrite = "";
		string strWhere = "";
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT firname + ' ' + lastname AS pname,username,email,dlnmbr,lcalnmbr FROM Users.Users WHERE firname LIKE '%" + Regex.Replace(txtFirstName.Text, "'", "") + "%' AND lastname LIKE '%" + Regex.Replace(txtLastName.Text, "'", "") + "%' AND pstatus='1'" + strWhere + " ORDER BY firname";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite += "<tr>" +
																	"<td class='GridRows'>" +
																		"<table>" +
																			"<tr><td><a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "' style='font-size:small;'>" + dr["pname"] + "</a></td></tr>" +
																			"<tr><td>Email: " + dr["email"] + "</td></tr>" +
                  "</table>" +
																	"</td>" +
																	"<td class='GridRows'>" +
																		"<table>" +
																			"<tr><td>Local # " + dr["lcalnmbr"] + "</td></tr>" +
																			"<tr><td>Direct Line # " + dr["dlnmbr"] + "</td></tr>" +
																		"</table>" +
																	"</td>" +
																"</tr>";
				intCtr++;
			}
			dr.Close();
		}
		Response.Write(strWrite);
	}

	protected void Page_Load(object sender, EventArgs e)
	{
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{

		}
	}

}
