using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Drawing;
using System.Data;

public partial class HR_HRMS_Reports_rptARPerfectAttendance : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  int intCtr = 1;
  DataTable tblPerfectAttendance = clsTimesheet.DSGARPerfectAttendance(clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date.AddDays(1)).AddSeconds(-1));
  foreach (DataRow drw in tblPerfectAttendance.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows'>" + intCtr.ToString("00") + ".</td>" +
                "<td class='GridRows'>" + drw["EmployeeName"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["Division"].ToString() + "</td>" +
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
               "</tr>";
   intCtr += 1;
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
 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("~/HR/HRMS/HRMS.aspx");
 }
}