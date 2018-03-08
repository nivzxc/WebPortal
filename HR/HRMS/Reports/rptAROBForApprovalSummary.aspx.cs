using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Drawing;
using System.Data;

public partial class HR_HRMS_Reports_rptAROBForApprovalSummary : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  int intCtr = 1;
  DataTable tblOBForApproval = clsTimesheet.DSGAROBForApproval(clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date.AddDays(1)).AddSeconds(-1));
  foreach (DataRow drw in tblOBForApproval.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows'>" + intCtr.ToString("00") + ".</td>" +
                "<td class='GridRows'>" + drw["EmployeeName"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["DateFiled"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["DateStart"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["DateEnd"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["Reason"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["Approver"].ToString() + "</td>" +
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

      DateTime dtpFirstDayNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);
      dtpStart.Date = new DateTime(DateTime.Now.Year, dtpFirstDayNextMonth.Month, 1);
      try
      {
          dtpEnd.Date = new DateTime(DateTime.Now.Year, dtpFirstDayNextMonth.Month, dtpFirstDayNextMonth.AddDays(-1).Day);
      }
      catch
      { dtpEnd.Date = new DateTime(DateTime.Now.Year, dtpFirstDayNextMonth.Month, dtpFirstDayNextMonth.AddDays(-2).Day); }
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("~/HR/HRMS/HRMS.aspx");
 }
}