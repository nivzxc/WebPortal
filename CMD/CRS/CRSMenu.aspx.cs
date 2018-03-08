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
using WebChart;
using System.Drawing;
using HRMS;
public partial class CMD_CRS_CRSMenu : System.Web.UI.Page
{

   protected void LoadCMRecentDispatch()
   {
      int intCtr = 0;
      string strWrite = "";

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT TOP 10 CM.Crs.schlcode,crsettle,yearterm,CM.CrsDetails.pstatus,CM.CrsDetailsDispatch.datentry FROM CM.Crs INNER JOIN (CM.CrsDetails INNER JOIN CM.CrsDetailsDispatch ON CM.CrsDetails.crsecode = CM.CrsDetailsDispatch.crsecode) ON CM.Crs.crscode = CM.CrsDetails.crscode WHERE cmname='" + Request.Cookies["Speedo"]["UserName"] + "' AND (CM.CrsDetails.pstatus='P' OR CM.CrsDetails.pstatus='C') ORDER BY CM.CrsDetailsDispatch.datentry DESC";
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         while (dr.Read())
         {
            strWrite = "<tr>" +
                                                                        "<td class='GridRows' style='text-align:center;'><img src='../../Support/" + clsCRS.GetCrsDetailsStatusIcon(dr["pstatus"].ToString()) + "' alt='' /></td>" +
                                                                        "<td class='GridRows'>" +
                                                                             clsSchool.GetSchoolName(dr["schlcode"].ToString()) + "<br>" +
                                                                             dr["crsettle"].ToString() +
                                                                        "</td>" +
                                                                        "<td class='GridRows'>" + dr["datentry"].ToString() + "</td>" +
                                                                        "<td class='GridRows'>" + clsCRS.ToCrsDetailsStatusDesc(dr["pstatus"].ToString()) + "</td>" +
                                                                   "</tr>";
            Response.Write(strWrite);
            intCtr++;
         }
         if (intCtr == 0)
            Response.Write("<tr><td colspan='4' class='GridRows'>No record found</td></tr>");
         else
            Response.Write("<tr><td colspan='4' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
      }
   }

   protected void LoadMenuCM()
   {
      string strWrite = "";
      int intTotal = 0;
      int intForApproval = 0;
      int intDisapproved = 0;
      int intForProcessing = 0;
      int intPartial = 0;
      int intComplete = 0;

      DataTable tblCRS = new DataTable();
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlDataReader dr;
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT TOP 10 crscode,schlcode,datereq FROM CM.Crs WHERE cmname='" + Request.Cookies["Speedo"]["UserName"] + "' ORDER BY datereq DESC";
         cn.Open();
         SqlDataAdapter da = new SqlDataAdapter(cmd);
         da.Fill(tblCRS);

         foreach (DataRow drow in tblCRS.Rows)
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
                                                                                                                          "<td class='GridRows' style='text-align:center;'>" +
                                                                                                                               "<a href='CRSDetails.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'><img src='../../Support/" + clsCRS.GetRequestStatusIcon(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "' alt='' /></a>" +
                                                                                                                          "</td>" +
                                                                                                                          "<td class='GridRows'>" +
                                                                                                                               "<a href='CRSDetailsCM.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'>" + clsSchool.GetSchoolName(drow["schlcode"].ToString()) + "</a><br>" +
                                                                                                                               "Date Requested: " + Convert.ToDateTime(drow["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
                                                                                                                          "</td>" +
                                                                                                                          "<td class='GridRows'>" + clsCRS.GetRequestStatusRemarks(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "</td>" +
                                                                                                                     "</tr>";
         }

         Response.Write(strWrite);
         if (tblCRS.Rows.Count == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
      }
   }

   protected void LoadMenuCC()
   {
      string strWrite = "";
      int intTotal = 0;
      int intForApproval = 0;
      int intDisapproved = 0;
      int intForProcessing = 0;
      int intPartial = 0;
      int intComplete = 0;

      DataTable tblCRS = new DataTable();
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlDataReader dr;
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT TOP 10 crscode,schlcode,datereq,cmname FROM CM.CRS WHERE ccname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='E' OR pstatus='P') ORDER BY datereq DESC";
         cn.Open();
         SqlDataAdapter da = new SqlDataAdapter(cmd);
         da.Fill(tblCRS);

         foreach (DataRow drow in tblCRS.Rows)
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
                                       "<td class='GridRows' style='text-align:center;'>" +
                                                                                                                                    "<a href='CRSDetailsCC.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'><img src='../../Support/" + clsCRS.GetRequestStatusIcon(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "' alt='' /></a>" +
                                                                                                                               "</td>" +
                                                                                                                               "<td class='GridRows'>" +
                                                                                                                                    "<a href='CRSDetailsCC.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'>" + clsSchool.GetSchoolName(drow["schlcode"].ToString()) + "</a><br>" +
                                                                                                                                    "Requested by: " + drow["cmname"] + "<br>" +
                                                                                                                                    "Date Requested: " + Convert.ToDateTime(drow["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
                                                                                                                               "</td>" +
                                                                                                                               "<td class='GridRows'>" + clsCRS.GetRequestStatusRemarks(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "</td>" +
                                                                                                                          "</tr>";
         }

         Response.Write(strWrite);
         if (tblCRS.Rows.Count == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
         else
            Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblCRS.Rows.Count + " records found ]</td></tr>");
      }
   }

   protected void LoadMenuCMHead()
   {
      string strWrite = "";
      int intTotal = 0;
      int intForApproval = 0;
      int intDisapproved = 0;
      int intForProcessing = 0;
      int intPartial = 0;
      int intComplete = 0;

      DataTable tblCRS = new DataTable();
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlDataReader dr;
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT TOP 10 crscode,schlcode,datereq,cmname FROM CM.CRS WHERE cmhname='" + Request.Cookies["Speedo"]["UserName"] + "' AND crscode IN (SELECT DISTINCT crscode FROM CM.CrsDetails WHERE pstatus='F') ORDER BY datereq DESC";
         cn.Open();
         SqlDataAdapter da = new SqlDataAdapter(cmd);
         da.Fill(tblCRS);

         foreach (DataRow drow in tblCRS.Rows)
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
                                                                                                                               "<td class='GridRows' style='text-align:center;'>" +
                                                                                                                                    "<a href='CRSDetailsCMH.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'><img src='../../Support/" + clsCRS.GetRequestStatusIcon(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "' alt='' /></a>" +
                                                                                                                               "</td>" +
                                                                                                                               "<td class='GridRows'>" +
                                                                                                                                    "<a href='CRSDetailsCMH.aspx?crscode=" + drow["crscode"] + "' style='font-size:small;'>" + clsSchool.GetSchoolName(drow["schlcode"].ToString()) + "</a><br>" +
                                                                                                                                    "Requested by: " + drow["cmname"] + "<br>" +
                                                                                                                                    "Date Requested: " + Convert.ToDateTime(drow["datereq"]).ToString("MMMM dd, yyyy") + "<br>" +
                                                                                                                               "</td>" +
                                                                                                                               "<td class='GridRows'>" + clsCRS.GetRequestStatusRemarks(intTotal, intForApproval, intDisapproved, intForProcessing, intPartial, intComplete) + "</td>" +
                                                                                                                          "</tr>";
         }

         Response.Write(strWrite);
         if (tblCRS.Rows.Count == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
         else
            Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblCRS.Rows.Count + " records found ]</td></tr>");
      }
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      clsCRS.CRSUserType cutUserLevel = clsCRS.GetUserLevel(Request.Cookies["Speedo"]["UserName"].ToString());

      if (!Page.IsPostBack)
      {
         if (cutUserLevel == clsCRS.CRSUserType.ChannelManager)
         {
            ddlSchools.DataSource = clsSchool.GetSchoolCmDdlDataTable(Request.Cookies["Speedo"]["UserName"].ToString());
            ddlSchools.DataValueField = "pvalue";
            ddlSchools.DataTextField = "ptext";
            ddlSchools.DataBind();
         }

         if (cutUserLevel == clsCRS.CRSUserType.EliteUsers)
         {
            int intCtr = 0;
            ColumnChart ccCR;
            DataTable tblCR = new DataTable();
            tblCR.Columns.Add("month", System.Type.GetType("System.String"));
            tblCR.Columns.Add("tcount", System.Type.GetType("System.Int32"));

            DataRow rowCR;
            Color[] clrColor = new Color[] { Color.Yellow, Color.Purple, Color.DarkGreen, Color.Orange, Color.Red };
            string[] strLabel = new string[] { "Completed", "Partial Process", "For Processing (Endorsed)", "For Endorsement", "Disapproved" };
            string[] strStatus = new string[] { "C", "P", "E", "F", "D" };
            //cmd.CommandText = "SELECT DATEPART(mm,datereq) AS pmonth,COUNT(crsecode) AS tcrscode FROM CM.CrsDetails INNER JOIN CM.Crs ON Cm.CrsDetails.crscode = CM.Crs.crscode WHERE DATEPART(mm,datereq) BETWEEN " + DateTime.Now.AddMonths(-2).Month + " AND " + DateTime.Now.Month + " AND DATEPART(yy,datereq) = '" + DateTime.Now.Year + "' GROUP BY DATEPART(mm,datereq) ORDER BY DATEPART(mm,datereq)";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
               SqlCommand cmd = cn.CreateCommand();
               cn.Open();
               SqlDataReader dr;

               for (intCtr = 0; intCtr < 5; intCtr++)
               {
                  rowCR = tblCR.NewRow();
                  tblCR.Rows.Clear();

                  rowCR["month"] = DateTime.Now.AddMonths(-2).ToString("MMMM");
                  cmd.CommandText = "SELECT COUNT(crsecode) AS tcrsecode FROM CM.CrsDetails WHERE crscode IN (SELECT crscode FROM CM.Crs WHERE DATEPART(mm,datereq) = " + DateTime.Now.AddMonths(-2).Month + " AND DATEPART(yy,datereq) = '" + DateTime.Now.Year + "') AND pstatus='" + strStatus[intCtr] + "'";
                  dr = cmd.ExecuteReader();
                  if (dr.Read())
                     rowCR["tcount"] = dr["tcrsecode"].ToString();
                  else
                     rowCR["tcount"] = dr["tcrsecode"].ToString();
                  dr.Close();
                  tblCR.Rows.Add(rowCR);

                  rowCR = tblCR.NewRow();
                  rowCR["month"] = DateTime.Now.AddMonths(-1).ToString("MMMM");
                  cmd.CommandText = "SELECT COUNT(crsecode) AS tcrsecode FROM CM.CrsDetails WHERE crscode IN (SELECT crscode FROM CM.Crs WHERE DATEPART(mm,datereq) = " + DateTime.Now.AddMonths(-1).Month + " AND DATEPART(yy,datereq) = '" + DateTime.Now.Year + "') AND pstatus='" + strStatus[intCtr] + "'";
                  dr = cmd.ExecuteReader();
                  if (dr.Read())
                     rowCR["tcount"] = dr["tcrsecode"].ToString();
                  else
                     rowCR["tcount"] = dr["tcrsecode"].ToString();
                  dr.Close();
                  tblCR.Rows.Add(rowCR);

                  rowCR = tblCR.NewRow();
                  rowCR["month"] = DateTime.Now.ToString("MMMM");
                  cmd.CommandText = "SELECT COUNT(crsecode) AS tcrsecode FROM CM.CrsDetails WHERE crscode IN (SELECT crscode FROM CM.Crs WHERE DATEPART(mm,datereq) = " + DateTime.Now.Month + " AND DATEPART(yy,datereq) = '" + DateTime.Now.Year + "') AND pstatus='" + strStatus[intCtr] + "'";
                  dr = cmd.ExecuteReader();
                  if (dr.Read())
                     rowCR["tcount"] = dr["tcrsecode"].ToString();
                  else
                     rowCR["tcount"] = dr["tcrsecode"].ToString();
                  dr.Close();
                  tblCR.Rows.Add(rowCR);

                  ccCR = new ColumnChart();
                  ccCR.Shadow.Visible = true;
                  ccCR.MaxColumnWidth = 15;
                  ccCR.Fill.Color = System.Drawing.Color.FromArgb(90, clrColor[intCtr]);
                  ccCR.DataLabels.Visible = true;
                  ccCR.DataLabels.Background.Color = System.Drawing.Color.White;
                  ccCR.Legend = strLabel[intCtr];
                  ccCR.DataSource = tblCR.DefaultView;
                  ccCR.DataXValueField = "month";
                  ccCR.DataYValueField = "tcount";
                  ccCR.DataBind();

                  chaCRSSummary.Charts.Add(ccCR);
               }
            }

            chaCRSSummary.ChartTitle.Text = "Courseware Request Summary";
            chaCRSSummary.RedrawChart();
         }
      }


      if (cutUserLevel == clsCRS.CRSUserType.ChannelManager)
      {
         lblE.Text = "0";
         lblP.Text = "0";
         using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(crsecode) AS te FROM CM.Crs INNER JOIN CM.CrsDetails ON CM.Crs.crscode = CM.CrsDetails.crscode WHERE cmname='" + Request.Cookies["Speedo"]["UserName"] + "' AND CM.CrsDetails.pstatus='F'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
               if (!Convert.IsDBNull(dr["te"].ToString()))
                  lblE.Text = dr["te"].ToString();
            dr.Close();

            cmd.CommandText = "SELECT COUNT(crsecode) AS tp FROM CM.Crs INNER JOIN CM.CrsDetails ON CM.Crs.crscode = CM.CrsDetails.crscode WHERE cmname='" + Request.Cookies["Speedo"]["UserName"] + "' AND CM.CrsDetails.pstatus='P'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
               if (!Convert.IsDBNull(dr["tp"].ToString()))
                  lblP.Text = dr["tp"].ToString();
            dr.Close();
         }
      }

   }

   protected void btnNew_Click(object sender, ImageClickEventArgs e)
   {
      Response.Redirect("CRSNew.aspx?schlcode=" + ddlSchools.SelectedValue);
   }

}
