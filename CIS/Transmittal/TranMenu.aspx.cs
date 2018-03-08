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

public partial class CIS_Transmittal_TranMenu : System.Web.UI.Page
{

	protected void LoadApproverMenu()
	{
		string strWrite = "";
		int intCtr = 0;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT TOP 10 trancode,itemdesc,username,dateneed,datereq FROM CIS.Transmittal WHERE (disptype='S' OR disptype='H') AND status='F' AND grphstat='A' ORDER BY datereq DESC";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = "<tr>" +
																"<td class='GridRows'>" +
																	"<a href='TranDetailsSA.aspx?trancode=" + dr["trancode"] + "'><img src='../../Support/approval.png' alt='' /></a>" +
																"</td>" +
																"<td class='GridRows'>" +
																	"<a href='TranDetailsSA.aspx?trancode=" + dr["trancode"] + "' style='font-size:small;'>" + dr["itemdesc"] + "</a>" +
																	"<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a>" +
																	"<br>Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") +
																	"<br>Date Needed: " + Convert.ToDateTime(dr["dateneed"]).ToString("MMMM dd, yyyy") +
																"</td>" +
																"<td class='GridRows'>For your approval</td>" +
															"</tr>";
				Response.Write(strWrite);
				intCtr++;
			}
			dr.Close();
		}
		if (intCtr == 0)
			Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
		else
			Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " record(s) found ]</td></tr>");
	}

	protected void LoadGroupHeadMenu()
	{
		string strWrite = "";
		int intCtr = 0;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT TOP 10 trancode,itemdesc,username,dateneed,datereq FROM CIS.Transmittal WHERE grphname='" + Request.Cookies["Speedo"]["UserName"] + "' AND (disptype='S' OR disptype='H') AND status='F' AND grphstat='F' ORDER BY datereq DESC";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = "<tr>" +
																"<td class='GridRows'>" +
																	"<a href='TranDetailsGH.aspx?trancode=" + dr["trancode"] + "'><img src='../../Support/approval.png' alt='' /></a>" +
																"</td>" +
																"<td class='GridRows'>" +
																	"<a href='TranDetailsGH.aspx?trancode=" + dr["trancode"] + "' style='font-size:small;'>" + dr["itemdesc"] + "</a>" +
																	"<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a>" +
																	"<br>Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") +
																	"<br>Date Needed: " + Convert.ToDateTime(dr["dateneed"]).ToString("MMMM dd, yyyy") +
																"</td>" +
																"<td class='GridRows'>For your approval</td>" +
															"</tr>";
				Response.Write(strWrite);
				intCtr++;
			}
			dr.Close();
		}
		if (intCtr == 0)
			Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
		else
			Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " record(s) found ]</td></tr>");
	}

	protected void LoadTransmittal()
	{
		string strWrite = "";
		int intCtr = 0;		

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT TOP 10 trancode,itemdesc,username,disptype,dateneed,datereq,grphname,grphstat,appname,appstat,status FROM CIS.Transmittal WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' ORDER BY datereq DESC";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{				
				strWrite = strWrite + "<tr>" +
					                      "<td class='GridRows'>" +
																											 "<a href='TranDetails.aspx?trancode=" + dr["trancode"] + "'><img src='../../Support/" + clsTransmittal.GetRequestStatusIcon(dr["disptype"].ToString(), dr["status"].ToString(), dr["grphname"].ToString(), dr["grphstat"].ToString(), dr["appname"].ToString(), dr["appstat"].ToString()) +"' alt='' /></a>" +
																											"</td>" +
																											"<td class='GridRows'>" +
																											 "<a href='TranDetails.aspx?trancode=" + dr["trancode"] + "' style='font-size:small;'>" + dr["itemdesc"] + "</a><br>" +
																												"Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "</td>" +
																											"<td class='GridRows'>" + clsTransmittal.GetRequestStatusRemarks(dr["disptype"].ToString(), dr["status"].ToString(), dr["grphname"].ToString(), dr["grphstat"].ToString(), dr["appname"].ToString(), dr["appstat"].ToString()) +"</td>" +
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

	protected void Page_Load(object sender, EventArgs e) 
	{
  clsSpeedo.Authenticate();
 }

    protected void btnNew_Click(object sender, EventArgs e)
	{
		Response.Redirect("TranNew.aspx");
	}

}
