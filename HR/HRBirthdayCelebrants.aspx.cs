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

public partial class HR_HRBirthdayCelebrants : System.Web.UI.Page
{

	protected void LoadRecords()
	{
		string strWrite = "";
		int intCtr = 0;
		string strClass = "";

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if(Request.QueryString["month"] == null)
				cmd.CommandText = "SELECT username,firname,lastname,nickname,brthdate,gender,isnull(email,'noemail@stihq.net') AS pemail FROM Users.Users WHERE DATEPART(mm,brthdate)='" + DateTime.Now.Month + "' AND pstatus='1' ORDER BY DATEPART(dd,brthdate)";
			else
				cmd.CommandText = "SELECT username,firname,lastname,nickname,brthdate,gender,isnull(email,'noemail@stihq.net') AS pemail FROM Users.Users WHERE DATEPART(mm,brthdate)='" + Request.QueryString["month"] + "' AND pstatus='1' ORDER BY DATEPART(dd,brthdate)";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strClass = ((DateTime.Now.Day == Convert.ToDateTime(dr["brthdate"].ToString()).Day) && (DateTime.Now.Month == Convert.ToDateTime(dr["brthdate"].ToString()).Month) ? "GridRows2" : "GridRows");

				strWrite = strWrite + "<tr>" +
																											"<td class='" + strClass + "' style='font-size:medium;text-align:center;'>" +
																												Convert.ToDateTime(dr["brthdate"].ToString()).ToString("MMMM dd") + 
																											"</td>" +
																											"<td class='" + strClass + "'>" +
																											 "<table cellpadding='5'>" +
																													"<tr>" +
                                                                                                                        "<td  width='200' height='200'>" +
                               "<img src='http://hq.sti.edu/Pictures/realpicture/" + clsSpeedo.GetRealPicture(dr["username"].ToString()) + ".jpg' width='200' height='200'><br>" +
																														"</td>" +
																														"<td style='font-size:medium;'>" +
																															"<a href='../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["nickname"] + " " + dr["lastname"] + "</a>" +
																															((DateTime.Now.Day == Convert.ToDateTime(dr["brthdate"].ToString()).Day) && (DateTime.Now.Month == Convert.ToDateTime(dr["brthdate"].ToString()).Month) ? "<br><a href='mailto:" + dr["pemail"] + "' style='font-size:small;'>Drop him/her an email greeting</a>" : "") +
																														"</td>" +
																													"</tr>" + 
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
			if (Request.QueryString["month"] == null)
				ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
			else
				ddlMonth.SelectedValue = Request.QueryString["month"];
		}
 }

	protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
	{
		Response.Redirect("HRBirthdayCelebrants.aspx?month=" + ddlMonth.SelectedValue);
	}

}
