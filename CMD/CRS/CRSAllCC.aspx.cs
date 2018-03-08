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
public partial class CMD_CRS_CRSAllCC : System.Web.UI.Page
{

   protected void LoadCWR()
   {
      string strWrite = "";
      int intTotal = 0;
      int intForApproval = 0;
      int intDisapproved = 0;
      int intForProcessing = 0;
      int intPartial = 0;
      int intComplete = 0;

      int intCtr = 0;
      int intPage = (Convert.ToInt32(Request.QueryString["page"]) == 0 ? 1 : Convert.ToInt32(Request.QueryString["page"]));
      int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
      int intStart = ((intPage - 1) * intPageSize) + 1;
      int intEnd = intPage * intPageSize;

      DataTable tblCrs = new DataTable();
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlDataReader dr;
         SqlCommand cmd = cn.CreateCommand();
         if (radModeAll.Checked)
            cmd.CommandText = "SELECT * FROM (SELECT crscode,remarks,schlcode,datereq,cmname,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CM.Crs WHERE ccname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='E' OR pstatus='P' OR pstatus='C')) as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
         else if (radModeProcessed.Checked)
            cmd.CommandText = "SELECT * FROM (SELECT crscode,remarks,schlcode,datereq,cmname,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CM.Crs WHERE ccname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='C')) as pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
         else if (radModeForApproval.Checked)
            cmd.CommandText = "SELECT * FROM (SELECT crscode,remarks,schlcode,datereq,cmname,ROW_NUMBER() OVER(ORDER BY datereq DESC) AS RowNum FROM CM.Crs WHERE ccname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='E' OR pstatus='P')) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
         SqlDataAdapter da = new SqlDataAdapter(cmd);
         cn.Open();
         da.Fill(tblCrs);
         foreach (DataRow drow in tblCrs.Rows)
         {
            intTotal = 0;
            intForApproval = 0;
            intDisapproved = 0;
            intForProcessing = 0;
            intPartial = 0;
            intComplete = 0;

            cmd.CommandText = "SELECT pstatus FROM CM.CrsDetails WHERE crscode='" + drow["crscode"] + "'";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               intTotal++;
               if (dr["pstatus"].ToString() == "F")
                  intForApproval++;
               else if (dr["pstatus"].ToString() == "D")
                  intDisapproved++;
               else if (dr["pstatus"].ToString() == "E")
                  intForProcessing++;
               else if (dr["pstatus"].ToString() == "P")
                  intPartial++;
               else if (dr["pstatus"].ToString() == "C")
                  intComplete++;
            }
            dr.Close();

            strWrite = strWrite + "<tr>" +
                                       "<td class='GridRows'>" +
                                                                                                                                    "<a href='CRSDetailsCC.aspx?crscode=" + drow["crscode"] + "'><img src='../../Support/" + clsCRS.GetRequestStatusIcon(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "' alt='' /></a>" +
                                                                                                                               "</td>" +
                                                                                                                               "<td class='GridRows'>" +
                                                                                                                                "<a href='CRSDetailsCC.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'>" + clsSchool.GetSchoolName(drow["schlcode"].ToString()) + "</a><br>" +
                                                                                                                                    "Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drow["cmname"] + "'>" + drow["cmname"] + "</a><br>" +
                                                                                                                                    "Date Requested: " + Convert.ToDateTime(drow["datereq"]).ToString("MMMM dd, yyyy") + "</td>" +
                                                                                                                               "<td class='GridRows'>" + clsCRS.GetRequestStatusRemarks(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "</td>" +
                                                                                                                          "</tr>";
            intCtr++;
         }
         Response.Write(strWrite);
      }

      if (intCtr == 0)
         Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='3' class='GridRows'>[" + intCtr + " records found]</td></tr>");
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
            cmd.CommandText = "SELECT COUNT(crscode) AS tcount FROM CM.Crs WHERE ccname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='E' OR pstatus='P' OR pstatus='C')";
         else if (radModeProcessed.Checked)
            cmd.CommandText = "SELECT COUNT(crscode) AS tcount FROM CM.Crs WHERE ccname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='C')";
         else if (radModeForApproval.Checked)
            cmd.CommandText = "SELECT COUNT(crscode) AS tcount FROM CM.Crs WHERE ccname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='E' OR pstatus='P')";
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
            Response.Write("&nbsp;&nbsp;<a href='CRSAllCC.aspx?page=" + intPage + "&mode=" + strMode + "'>" + intPage + "</a>");
         intPage++;
         intTRowsTemp -= intPageSize;
      }
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();

      if (!Page.IsPostBack)
      {
         switch (Request.QueryString["mode"])
         {
            case "a":
               radModeAll.Checked = true;
               break;
            case "p":
               radModeProcessed.Checked = true;
               break;
            case "f":
               radModeForApproval.Checked = true;
               break;
            default:
               radModeAll.Checked = true;
               break;
         }
      }
   }

}
