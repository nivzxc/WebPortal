using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;
using System.Data.SqlClient;

public partial class HR_HRMS_TimeSheet_TimeCardACM : System.Web.UI.Page
{
 protected void LoadTimeCard()
 {
  string strWrite = "";
  DataTable tblMonthlyTimeSheet = clsTimeCard.GetEmloyeeMonthlyTimeCard(Request.Cookies["Speedo"]["UserName"], dtpStart.Date, dtpEnd.Date);

  foreach (DataRow drow in tblMonthlyTimeSheet.Rows)
  {
      strWrite += "<tr class='GridRows2'>" +
                "<td class='GridRows2'>&nbsp;</td>" +
                "<td class='GridRows2'>" + drow["DateApp"].ToString() + "</td>" +
                "<td class='GridRows2'>" + drow["DateIn"].ToString() + "</td>" +
                "<td class='GridRows2'>" + drow["DateOut"].ToString() + "</td>" +
                "<td class='GridRows2'>&nbsp;</td>" +
               "</tr>";
  }
  Response.Write(strWrite);
 }

 //public static bool IsACMUp()
 //{
 //    bool blnReturn = false;
 //    using (SqlConnection testConn = new SqlConnection(clsHrms.ACMConnectionString))
 //    {
 //        try
 //        {
 //            testConn.Open();
 //            blnReturn = true;
 //            testConn.Close();
 //        }
 //        catch (SqlException)
 //        {
 //            blnReturn = false;
 //        }

 //    }
 //    return blnReturn;
 //}

 protected void Page_Load(object sender, EventArgs e)
 {
     //if (!IsACMUp())
     //{
     //    Response.Redirect("ACMDatabaseProblem.aspx");
     //}

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