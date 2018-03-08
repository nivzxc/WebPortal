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
using STIeForms;
using Oracles;
public partial class CIS_MRCF_MRCFAllPM : System.Web.UI.Page
{

	protected void LoadMRCF()
	{
		string strWrite = "";
		int intCtr = 0;
		int intPage = (Convert.ToInt32(Request.QueryString["page"]) == 0 ? 1 : Convert.ToInt32(Request.QueryString["page"]));
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intStart = ((intPage - 1) * intPageSize) + 1;
		int intEnd = intPage * intPageSize;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (radModeAll.Checked)
                cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,username,intended,datereq,status, (CASE WHEN status = 'A' THEN (SELECT TOP(1) btchcode FROM CIS.MrcfBatch WHERE mrcfcode=CIS.Mrcf.mrcfcode ORDER BY mrcfcode DESC) ELSE '' END) AS btchcode,sprvcode,sprvstat,headcode,headstat,proccode,procstat,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status <> 'V' AND procstat <> 'X') as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			else if (radModeProcessed.Checked)
                cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,username,intended,datereq,status,(CASE WHEN status = 'A' THEN (SELECT TOP(1) btchcode FROM CIS.MrcfBatch WHERE mrcfcode=CIS.Mrcf.mrcfcode ORDER BY mrcfcode DESC) ELSE '' END) AS btchcode,sprvcode,sprvstat,headcode,headstat,proccode,procstat,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status <> 'V' AND procstat IN ('A','D','M')) as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			else if (radModeForApproval.Checked)
                cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,username,intended,datereq,status,(CASE WHEN status = 'A' THEN (SELECT TOP(1) btchcode FROM CIS.MrcfBatch WHERE mrcfcode=CIS.Mrcf.mrcfcode ORDER BY mrcfcode DESC) ELSE '' END) AS btchcode,sprvcode,sprvstat,headcode,headstat,proccode,procstat,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND ((sprvstat='A' AND headstat='N') OR (headstat='A')) AND procstat='F') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			else if (radModeForApprovalGHDH.Checked)
                cmd.CommandText = "SELECT * FROM (SELECT mrcfcode,username,intended,datereq,status,(CASE WHEN status = 'A' THEN (SELECT TOP(1) btchcode FROM CIS.MrcfBatch WHERE mrcfcode=CIS.Mrcf.mrcfcode ORDER BY mrcfcode DESC) ELSE '' END) AS btchcode,sprvcode,sprvstat,headcode,headstat,proccode,procstat,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND (sprvstat='F' OR headstat='F')) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
                int intBatchCode = dr["btchcode"].ToString().ToInt();
                if (intBatchCode > 0)
                {
                    strWrite = strWrite + "<tr>" +
                                                  "<td class='GridRows'>" +
                                                    "<a href='MRCFDetailsPM.aspx?mrcfcode=" + dr["mrcfcode"] + "'><img src='../../Support/" + clsMRCF.GetRequestStatusIcon(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "' alt='' /></a>" +
                                                    "</td>" +
                                                    "<td class='GridRows'>" +
                                                    "<a href='MRCFDetailsPM.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["intended"] + "</a><br>" +
                                                    "Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
                                                    "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "</a><br>" +
                                                    "MRCF Code: " + dr["mrcfcode"].ToString() + "</a><br>" +
                                                    "Batch Number: " + intBatchCode + "</td>" +
                                                    "<td class='GridRows'>" + clsMRCF.GetRequestStatusRemarks(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "</td>" +
                                                    "</tr>";
                    intCtr++;
                }
                else
                {
                    strWrite = strWrite + "<tr>" +
                                              "<td class='GridRows' style='width:25px;'>" +
                                                "<a href='MRCFDetailsPM.aspx?mrcfcode=" + dr["mrcfcode"] + "'><img src='../../Support/" + clsMRCF.GetRequestStatusIcon(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "' alt='' /></a>" +
                                                "</td>" +
                                                "<td class='GridRows' style='width:350px;'>" +
                                                "<a href='MRCFDetailsPM.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["intended"] + "</a><br>" +
                                                "Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
                                                "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") + "</a><br>" +
                                                "MRCF Code: " + dr["mrcfcode"].ToString() + "</td>" +
                                                "<td class='GridRows'>" + clsMRCF.GetRequestStatusRemarks(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "</td>" +
                                                "</tr>";
                    intCtr++;
                }
			}
			dr.Close();
		}

		Response.Write(strWrite);

		if (intCtr == 0)
			Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
	}

	protected void LoadPaging()
	{
		int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
		int intTRows = 0;
		int intTRowsTemp = 0;
		int intPage = 1;
		string strMode = "";
		if (radModeAll.Checked)
			strMode = "a";
		else if (radModeForApproval.Checked)
			strMode = "f";
		else if (radModeProcessed.Checked)
			strMode = "p";

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (radModeAll.Checked)
				cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status <> 'V' AND procstat <> 'X'";
			else if (radModeProcessed.Checked)
				cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status <> 'V' AND procstat IN ('A','D','M')";
			else if (radModeForApproval.Checked)
				cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND ((sprvstat='A' AND headstat='N') OR (headstat='A')) AND procstat='F'";
			else if (radModeForApprovalGHDH.Checked)
				cmd.CommandText = "SELECT COUNT(mrcfcode) AS tcount FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND (sprvstat='F' OR headstat='F')";
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
				Response.Write("&nbsp;&nbsp;<a href='MRCFAllPM.aspx?page=" + intPage + "&mode=" + strMode + "'>" + intPage + "</a>");
			intPage++;
			intTRowsTemp -= intPageSize;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			if (Request.QueryString["mode"] == "p")
				radModeProcessed.Checked = true;
			else if (Request.QueryString["mode"] == "f")
				radModeForApproval.Checked = true;
			else if (Request.QueryString["mode"] == "g")
				radModeForApprovalGHDH.Checked = true;
			else
				radModeAll.Checked = true;
		}
 }

}
