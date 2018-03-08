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

public partial class UsersIndex : System.Web.UI.Page
{

	protected void LoadUsers()
	{
		string strWrite = "";
		int intCtr = 0;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT username,firname + ' ' + lastname AS pname,lastlog FROM Users.Users WHERE pstatus='1' ORDER BY lastlog DESC,username ASC";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
																											"<td class='GridRows' style='text-align:center;'>" +
																										 	"<img src='http://hq.sti.edu/Pictures/avatar/" + (File.Exists(Server.MapPath("~/pictures/avatar/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='50' height='50'>" +
																										 "</td>" +
																											"<td class='GridRows' style='font-size:small;'><a href='Userpage/Userpage.aspx?username=" + dr["username"] + "'>" + dr["username"] + " (" + dr["pname"] + ")</a></td>" +
																											"<td class='GridRows' style='font-size:small;text-align:center;'>" + (Convert.IsDBNull(dr["lastlog"]) ? "" : Convert.ToDateTime(dr["lastlog"]).ToString("MMMM dd, yyyy hh:mm tt")) + "</td>" +
																								  "</tr>";				
				intCtr++;
			}
			dr.Close();
		}
		Response.Write(strWrite);
		if (intCtr == 0)
			Response.Write("<tr><td colspan='3' class='GridColumns'>No record found</td></tr>");
		else
			Response.Write("<tr><td colspan='3' class='GridColumns'>[ " + intCtr + " records found ]</td></tr>");
	}

	protected void Page_Load(object sender, EventArgs e)
 {

 }

}
