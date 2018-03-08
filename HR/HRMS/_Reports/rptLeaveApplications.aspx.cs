using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_Reports_rptLeaveApplications : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  int intCtr = 1;
  DataTable tblDivision = clsDivision.DSAll();

  foreach (DataRow drwDivision in tblDivision.Rows)
  {
   intCtr = 1;
   strWrite += "<tr>" +
                "<td class='GridRowsRed' colspan='13' style='font-size:x-small;'>&nbsp;<b>" + drwDivision["division"].ToString() + "</b></td>" +
               "</tr>";
   DataTable tblLeaveApplications = clsLeave.DSRLeaveApplications(drwDivision["divicode"].ToString(), clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date.AddDays(1)).AddSeconds(-1));
   foreach (DataRow drw in tblLeaveApplications.Rows)
   {
    strWrite += "<tr>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + intCtr.ToString("00") + ".</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["Status"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["DateFiled"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["Employee"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["LeaveType"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["DateStart"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["DateEnd"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='text-align:center; vertical-align:top;'>" + drw["Duration"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["Approver"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["ApproverDate"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["Department"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["Reason"].ToString() + "</td>" +
                 "<td class='GridRows" + (intCtr % 2 == 0 ? "" : "2") + "' style='vertical-align:top;'>" + drw["Remarks"].ToString() + "</td>" +
                "</tr>";
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
 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("~/HR/HRMS/HRMS.aspx");
 }
}