using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_TimeSheet_TimeCard : System.Web.UI.Page
{

 protected void LoadTimeCard()
 {
  string strWrite = "";
  DataTable tblMonthlyTimeSheet = clsTimesheet.GetEmloyeeMonthlyTimeSheet(Request.Cookies["Speedo"]["UserName"], dtpStart.Date, dtpEnd.Date);

  foreach (DataRow drow in tblMonthlyTimeSheet.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows'>&nbsp;</td>" +
                "<td class='GridRows'>" + drow["DateApp"].ToString() + "</td>" +
                "<td class='GridRows'>" + drow["DateIn"].ToString() + "</td>" +
                "<td class='GridRows'>" + drow["DateOut"].ToString() + "</td>" +
                "<td class='GridRows'>&nbsp;</td>" +
               "</tr>";
  }
  Response.Write(strWrite);
 }


 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   dtpStart.Date = clsDateTime.GetFirstDayOfMonth(DateTime.Now);
   dtpEnd.Date = clsDateTime.GetLastDayOfMonth(DateTime.Now);
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
     Response.Redirect("~/Userpage/ControlPanel.aspx");
 }

}