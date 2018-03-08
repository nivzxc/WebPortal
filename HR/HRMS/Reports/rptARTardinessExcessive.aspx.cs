﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;
using System.Drawing;

public partial class HR_HRMS_Reports_rptARTardinessExcessive : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  int intCtr = 1;

  DataTable tblTardinessExcessive = clsTimesheet.DSGARTardinessExcessiveSummary(clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date.AddDays(1)).AddSeconds(-1));
  foreach (DataRow drw in tblTardinessExcessive.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows' style='text-align:center;'>" + intCtr.ToString("00") + ".</td>" +
                "<td class='GridRows'>" + drw["EmployeeName"].ToString() + "</td>" +            "<td class='GridRows'>" + drw["Division"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["TardinessCount"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["TardinessMinute"].ToString() + "</td>" +
               "</tr>";
   intCtr += 1;
  }

  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   //string strCurrentTimesheetPeriod = clsTimeSheetPeriod.GetCurrentTimeSheetPeriod();
   //using (clsTimeSheetPeriod tsp = new clsTimeSheetPeriod())
   //{
   // tsp.TimeSheetPeriodCode = strCurrentTimesheetPeriod;
   // tsp.Fill();
   // dtpStart.Date = tsp.PeriodFrom;
   // dtpEnd.Date = tsp.PeriodTo;
   //}
      DateTime dtpFirstDayNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1);
      dtpStart.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
      dtpEnd.Date = new DateTime(DateTime.Now.Year, dtpFirstDayNextMonth.AddDays(-1).Month, dtpFirstDayNextMonth.AddDays(-1).Day);
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("~/HR/HRMS/HRMS.aspx");
 }

 protected void btnSearch_Click(object sender, EventArgs e)
 {

 }
}