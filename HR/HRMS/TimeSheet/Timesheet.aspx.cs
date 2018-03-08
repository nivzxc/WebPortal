using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_TimeSheet_Timesheet : System.Web.UI.Page
{

 private string CutSameDate(DateTime pDateFocus, DateTime pDateTime)
 {
  string strReturn = "";
  if (pDateFocus.ToString("MMM dd, yyyy") == pDateTime.ToString("MMM dd, yyyy"))
   strReturn = pDateTime.ToString("hh:mm tt");
  else
   strReturn = pDateTime.ToString("MM/dd/yyyy hh:mm tt");
  return strReturn;
 }

 private bool IsMinDate(DateTime pDateTime)
 {
  bool blnReturn = false;
  blnReturn = (pDateTime == clsDateTime.SystemMinDate);
  return blnReturn;
 }

 private bool IsZero(float pEntry)
 {
  bool blnReturn = false;
  blnReturn = pEntry == 0;
  return blnReturn;
 }

 protected void LoadTimeSheet()
 {
  string strWrite = "";
  string strCSSRow = "";
  DataTable tblTimeSheet = clsTimesheet.GetDataTableProcessed(dtpStart.Date, dtpEnd.Date, Request.Cookies["Speedo"]["UserName"]);

  foreach (DataRow drw in tblTimeSheet.Rows)
  {
   switch (drw["pstatus"].ToString())
   {
    case "W":
     strCSSRow = "GridRows";
     break;
    case "A":
     strCSSRow = "GridRowsRed";
     break;
    case "L":
     strCSSRow = "GridRowsYellow";
     break;
    case "N":
     strCSSRow = "GridRowsViolet";
     break;
    case "R":
     strCSSRow = "GridRowsGreen";
     break;
   }
   strWrite += "<tr>" +
                "<td class='" + strCSSRow + "'>" + drw["pstatus"].ToString() + "</td>" +
                "<td class='" + strCSSRow + "'>" + clsValidator.CheckDate(drw["focsdate"].ToString()).ToString("MM/dd/yyyy") + "</td>" +
                "<td class='" + strCSSRow + "'>" + (IsMinDate(clsValidator.CheckDate(drw["timein"].ToString())) ? "-" : clsValidator.CheckDate(drw["timein"].ToString()).ToString("hh:mm tt")) + "</td>" +
                "<td class='" + strCSSRow + "'>" + (IsMinDate(clsValidator.CheckDate(drw["timeout"].ToString())) ? "-" : CutSameDate(clsValidator.CheckDate(drw["focsdate"].ToString()), clsValidator.CheckDate(drw["timeout"].ToString()))) + "</td>" +
                "<td class='" + strCSSRow + "'>" + (IsMinDate(clsValidator.CheckDate(drw["shftin"].ToString())) ? "-" : clsValidator.CheckDate(drw["shftin"].ToString()).ToString("MM/dd/yyyy")) + "</td>" +
                "<td class='" + strCSSRow + "'>" + (IsMinDate(clsValidator.CheckDate(drw["shftout"].ToString())) ? "-" : clsValidator.CheckDate(drw["shftout"].ToString()).ToString("MM/dd/yyyy")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["workunit"].ToString())) ? "-" : clsValidator.CheckFloat(drw["workunit"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["absunit"].ToString())) ? "-" : clsValidator.CheckFloat(drw["absunit"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["lwithpay"].ToString())) ? "-" : clsValidator.CheckFloat(drw["lwithpay"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["lwoutpay"].ToString())) ? "-" : clsValidator.CheckFloat(drw["lwoutpay"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["lateunit"].ToString())) ? "-" : clsValidator.CheckFloat(drw["lateunit"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["undrunit"].ToString())) ? "-" : clsValidator.CheckFloat(drw["undrunit"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>&nbsp;</td>" +
               "</tr>";
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

 protected void btnBack_Click(object sender, EventArgs e)
 {
     Response.Redirect("~/Userpage/ControlPanel.aspx");
 }
}