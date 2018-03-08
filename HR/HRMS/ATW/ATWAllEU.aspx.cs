using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_ATW_ATWAllEU : System.Web.UI.Page
{

 protected void LoadRecords_()
 {
  string strWrite = "";
  DataTable tblATW = clsATW.GetDSG(clsDateTime.GetDateOnly(dtpDateStart.Date), new DateTime(dtpDateEnd.Date.Year, dtpDateEnd.Date.Month, dtpDateEnd.Date.Day, 23, 59, 59));
  foreach (DataRow drw in tblATW.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>&nbsp;</td>" +
                          "<td class='GridRows'>" + clsEmployee.GetName(drw["username"].ToString(), EmployeeNameFormat.LastFirst) + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["datestrt"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["dateend"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + drw["reason"].ToString() + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblATW.Rows.Count == 0)
      Response.Write("<tr><td colspan='5' class='BrowseAll' style='text-align:left;'>No record found</td></tr>");
  else
      Response.Write("<tr><td colspan='5' class='BrowseAll' style='text-align:left;'>[ " + tblATW.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadRecords()
 {
  string strWrite = "";
  DataTable tblATW = clsATW.GetDSG(clsDateTime.GetDateOnly(dtpDateStart.Date), new DateTime(dtpDateEnd.Date.Year, dtpDateEnd.Date.Month, dtpDateEnd.Date.Day, 23, 59, 59));
  foreach (DataRow drw in tblATW.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>&nbsp;</td>" +
                          "<td class='GridRows'>" + clsEmployee.GetName(drw["username"].ToString(), EmployeeNameFormat.LastFirst) + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["datestrt"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["dateend"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + drw["reason"].ToString() + "</td>" +
                          "<td class='GridRows'>" + clsATW.ToATWStatus(drw["status"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblATW.Rows.Count == 0)
   Response.Write("<tr><td colspan='6' class='BrowseAll' style='text-align:left;'>No record found</td></tr>");
  else
      Response.Write("<tr><td colspan='6' class='BrowseAll' style='text-align:left;'>[ " + tblATW.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadOvertimeRecords()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.GetDSG(clsDateTime.GetDateOnly(dtpDateStart.Date), new DateTime(dtpDateEnd.Date.Year, dtpDateEnd.Date.Month, dtpDateEnd.Date.Day, 23, 59, 59));
  foreach (DataRow drw in tblOvertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>&nbsp;</td>" +
                          "<td class='GridRows'>" + clsEmployee.GetName(drw["username"].ToString(), EmployeeNameFormat.LastFirst) + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["datestrt"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["dateend"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + drw["reason"].ToString() + "</td>" +
                          "<td class='GridRows'>" + clsATW.ToATWStatus(drw["otstat"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblOvertime.Rows.Count == 0)
      Response.Write("<tr><td colspan='6' class='BrowseAll' style='text-align:left;'>No record found</td></tr>");
  else
      Response.Write("<tr><td colspan='6' class='BrowseAll' style='text-align:left;'>[ " + tblOvertime.Rows.Count + " records found ]</td></tr>");
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
    if (!clsSystemModule.HasAccess("021", Request.Cookies["Speedo"]["Username"]))
   Response.Redirect("~/AccessDenied.aspx");
 }

 protected void btnSearch_Click(object sender, EventArgs e)
 {

 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
     Response.Redirect("../HRMS.aspx");
 }
}