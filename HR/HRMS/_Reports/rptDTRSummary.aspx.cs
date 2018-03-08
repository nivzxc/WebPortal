using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;
using WebChart;
using System.Drawing;

public partial class HR_HRMS_Reports_rptDTRSummary : System.Web.UI.Page
{

 protected void LoadChart()
 {
  DataTable tblDivision = clsDivision.DSAll();
  foreach (DataRow drw in tblDivision.Rows)
  {
   ColumnChart ccValue = new ColumnChart();
   Color clrChart = Color.SkyBlue;
   switch (drw["divicode"].ToString())
   {
    case "ACDMCS":
     clrChart = Color.Green;
     break;
    case "AUDCOM":
     clrChart = Color.Gray;
     break;
    case "CISERV":
     clrChart = Color.Blue;
     break;
    case "CNLMGT":
     clrChart = Color.Yellow;
     break;
    case "COOEVP":
     clrChart = Color.Orange;
     break;
    case "FNANCE":
     clrChart = Color.Fuchsia;
     break;
    case "MKTING":
     clrChart = Color.Red;
     break;
    case "OOCOOP":
     clrChart = Color.SkyBlue;
     break;
   }
   ccValue.Shadow.Visible = true;
   ccValue.MaxColumnWidth = 15;
   ccValue.Fill.Color = Color.FromArgb(90, clrChart);
   ccValue.Legend = drw["division"].ToString();
   ccValue.DataSource = clsTimesheet.DSCDTRSummary(drw["divicode"].ToString(), clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date.AddDays(1))).DefaultView;
   ccValue.DataXValueField = "xvalue";
   ccValue.DataYValueField = "yvalue";
   ccValue.DataBind();
   chaDTRSummary.Charts.Add(ccValue);
  }  
  chaDTRSummary.RedrawChart();


  //foreach (DataRow drw in tblDivision.Rows)
  //{
  // ColumnChart ccPercentage = new ColumnChart();
  // Color clrChart = Color.SkyBlue;
  // switch (drw["divicode"].ToString())
  // {
  //  case "ACDMCS":
  //   clrChart = Color.Green;
  //   break;
  //  case "AUDCOM":
  //   clrChart = Color.Gray;
  //   break;
  //  case "CISERV":
  //   clrChart = Color.Blue;
  //   break;
  //  case "CNLMGT":
  //   clrChart = Color.Yellow;
  //   break;
  //  case "COOEVP":
  //   clrChart = Color.Orange;
  //   break;
  //  case "FNANCE":
  //   clrChart = Color.Fuchsia;
  //   break;
  //  case "MKTING":
  //   clrChart = Color.Red;
  //   break;
  //  case "OOCOOP":
  //   clrChart = Color.SkyBlue;
  //   break;
  // }
  // ccPercentage.Shadow.Visible = true;
  // ccPercentage.MaxColumnWidth = 15;
  // ccPercentage.Fill.Color = Color.FromArgb(90, clrChart);
  // ccPercentage.Legend = drw["division"].ToString();
  // ccPercentage.DataSource = clsTimesheet.DSCDTRSummaryPercentage(drw["divicode"].ToString(), clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date.AddDays(1))).DefaultView;
  // ccPercentage.DataXValueField = "xvalue";
  // ccPercentage.DataYValueField = "yvalue";
  // ccPercentage.DataBind();
  // chaDTRSummaryPercentage.Charts.Add(ccPercentage);
  //}

  //chaDTRSummary.RedrawChart();
 }

 protected void LoadRecords()
 {
  DataTable tblDivision = clsDivision.DSAll();
  string strWrite = "";
  int intCtr = 0;
  short intTardinessCount = 0;
  float fltTardinessMinute = 0;
  short intUndertimeCount = 0;
  float fltUndertimeMinute = 0;
  short intAbsentCount = 0;
  float fltAbsentDay = 0;
  short intLWPCount = 0;
  float fltLWPDay = 0;
  short intLWOPCount = 0;
  float fltLWOPDay = 0;
  float fltTotalWorkDay = 0;
  float fltTotalWorkHour = 0;
  foreach (DataRow drwDivision in tblDivision.Rows)
  {
   DataTable tblDTRSummary = clsTimesheet.DSGARSummary(drwDivision["divicode"].ToString(), clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date.AddDays(1)).AddSeconds(-1));
   intCtr = 1;
   intTardinessCount = 0;
   fltTardinessMinute = 0;
   intUndertimeCount = 0;
   fltUndertimeMinute = 0;
   intAbsentCount = 0;
   fltAbsentDay = 0;
   intLWPCount = 0;
   fltLWPDay = 0;
   intLWOPCount = 0;
   fltLWOPDay = 0;
   fltTotalWorkDay = 0;
   fltTotalWorkHour = 0;

   foreach (DataRow drw in tblDTRSummary.Rows)
   {
    intTardinessCount += clsValidator.CheckShort(drw["TardinessCount"].ToString());
    fltTardinessMinute += clsValidator.CheckFloat(drw["TardinessMinute"].ToString());
    intUndertimeCount += clsValidator.CheckShort(drw["UndertimeCount"].ToString());
    fltUndertimeMinute += clsValidator.CheckFloat(drw["UndertimeMinute"].ToString());
    intAbsentCount += clsValidator.CheckShort(drw["AbsentCount"].ToString());
    fltAbsentDay += clsValidator.CheckFloat(drw["AbsentDay"].ToString());
    intLWPCount += clsValidator.CheckShort(drw["LWPCount"].ToString());
    fltLWPDay += clsValidator.CheckFloat(drw["LWPDay"].ToString());
    intLWOPCount += clsValidator.CheckShort(drw["LWOPCount"].ToString());
    fltLWOPDay += clsValidator.CheckFloat(drw["LWOPDay"].ToString());
    fltTotalWorkDay += clsValidator.CheckFloat(drw["TotalWorkDay"].ToString());
    fltTotalWorkHour += clsValidator.CheckFloat(drw["TotalWorkHour"].ToString());
   }

   strWrite += "<tr>" +
                "<td class='GridRows2' colspan='3' style='font-size:small;'>&nbsp;<b>" + drwDivision["division"].ToString() + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(intTardinessCount) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(fltTardinessMinute) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(intUndertimeCount) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(fltUndertimeMinute) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(intAbsentCount) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(fltAbsentDay) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(intLWPCount) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(fltLWPDay) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(intLWOPCount) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(fltLWOPDay) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(fltTotalWorkDay) + "</b></td>" +
                "<td class='GridRows2' style='text-align:center;'><b>" + clsValidator.ZeroToDash(fltTotalWorkHour) + "</b></td>" +
               "</tr>";

   foreach (DataRow drw in tblDTRSummary.Rows)
   {
    strWrite += "<tr>" +
             "<td class='GridRows' style='text-align:center;'>" + intCtr.ToString("00") + ".</td>" +
             "<td class='GridRows'>" + drw["EmployeeName"].ToString() + "</td>" +
             "<td class='GridRows'>" + drw["Department"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["TardinessCount"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["TardinessMinute"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["UndertimeCount"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["UndertimeMinute"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["AbsentCount"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["AbsentDay"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["LWPCount"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["LWPDay"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["LWOPCount"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["LWOPDay"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["TotalWorkDay"].ToString() + "</td>" +
             "<td class='GridRows' style='text-align:center;'>" + drw["TotalWorkHour"].ToString() + "</td>" +
            "<tr>";
    intCtr += 1;
   }
  }

  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   string strCurrentTimesheetPeriod = clsTimeSheetPeriod.GetCurrentTimeSheetPeriod();
   using (clsTimeSheetPeriod tsp = new clsTimeSheetPeriod())
   {
    tsp.TimeSheetPeriodCode = strCurrentTimesheetPeriod;
    tsp.Fill();
    dtpStart.Date = tsp.PeriodFrom;
    dtpEnd.Date = tsp.PeriodTo;
   }   
  }
  LoadChart();
 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("~/HR/HRMS/HRMS.aspx");
 }
}