using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class HR_HRMS_Reports_rptResignedEmployee : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  int intCtr = 1;

  DataTable tblResignedEmployees = clsEmployee.DSRResignedEmployees(clsDateTime.GetDateOnly(dtpStart.Date), clsDateTime.GetDateOnly(dtpEnd.Date));
  foreach (DataRow drw in tblResignedEmployees.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows'>" + intCtr.ToString() + ".</td>" +
                "<td class='GridRows'>" + drw["EmployeeName"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["Position"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["BirthDate"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["Age"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["DateHired"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["DateEnd"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["EmploymentStatus"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + clsValidator.CheckFloat(drw["Tenure"].ToString()).ToString("##0.00") + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["ResignationReason"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["ResignationRemarks"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["ResignationDesired"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["Billable"].ToString() + "</td>" +
               "</tr>";
   intCtr += 1;
  }

  if (strWrite == "")
   strWrite = "<tr><td class='GridRows' colspan='13'>&nbsp;No resigned employees found within the selected date range.</td></tr>";
  Response.Write(strWrite);
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

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