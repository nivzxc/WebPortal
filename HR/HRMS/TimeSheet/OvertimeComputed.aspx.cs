using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_TimeSheet_OvertimeComputed : System.Web.UI.Page
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

 protected void LoadOvertime()
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
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["reguxcss"].ToString())) ? "-" : clsValidator.CheckFloat(drw["reguxcss"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["regunght"].ToString())) ? "-" : clsValidator.CheckFloat(drw["regunght"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["restover"].ToString())) ? "-" : clsValidator.CheckFloat(drw["restover"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["restxcss"].ToString())) ? "-" : clsValidator.CheckFloat(drw["restxcss"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["restnght"].ToString())) ? "-" : clsValidator.CheckFloat(drw["restnght"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["spclover"].ToString())) ? "-" : clsValidator.CheckFloat(drw["spclover"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["spclxcss"].ToString())) ? "-" : clsValidator.CheckFloat(drw["spclxcss"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["spclnght"].ToString())) ? "-" : clsValidator.CheckFloat(drw["spclnght"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["leglover"].ToString())) ? "-" : clsValidator.CheckFloat(drw["leglover"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["leglxcss"].ToString())) ? "-" : clsValidator.CheckFloat(drw["leglxcss"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["leglnght"].ToString())) ? "-" : clsValidator.CheckFloat(drw["leglnght"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["shrdover"].ToString())) ? "-" : clsValidator.CheckFloat(drw["shrdover"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["shrdxcss"].ToString())) ? "-" : clsValidator.CheckFloat(drw["shrdxcss"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["shrdnght"].ToString())) ? "-" : clsValidator.CheckFloat(drw["shrdnght"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["lerdover"].ToString())) ? "-" : clsValidator.CheckFloat(drw["lerdover"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["lerdxcss"].ToString())) ? "-" : clsValidator.CheckFloat(drw["lerdxcss"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>" + (IsZero(clsValidator.CheckFloat(drw["lerdnght"].ToString())) ? "-" : clsValidator.CheckFloat(drw["lerdnght"].ToString()).ToString("###0.00")) + "</td>" +
                "<td class='" + strCSSRow + "' style='text-align:right;'>&nbsp;</td>" +
               "</tr>";
  }
  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   DateTime dtpFirstDayNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1);
   dtpStart.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
   dtpEnd.Date = new DateTime(DateTime.Now.Year, dtpFirstDayNextMonth.AddDays(-1).Month, dtpFirstDayNextMonth.AddDays(-1).Day);
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
     Response.Redirect("~/Userpage/ControlPanel.aspx");
 }

}