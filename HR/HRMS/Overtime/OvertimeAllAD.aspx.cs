using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class HR_HRMS_Overtime_OvertimeAllAD : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeAll(OvertimeUsers.ApproverDivision, clsValidator.CheckInteger(Request.QueryString["page"]), Request.Cookies["Speedo"]["UserName"], ddlStatus.SelectedValue);
  foreach (DataRow drw in tblOvertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAD.aspx?otcode=" + drw["otcode"] + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["otstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAD.aspx?otcode=" + drw["otcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                           "Sent by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                           clsOvertime.GetChargeTypeDesc(drw["chartype"].ToString()) + "<br>" +
                           "[" + Convert.ToDateTime(drw["datestrt"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "] - [" + Convert.ToDateTime(drw["dateend"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "]<br>" +
                           "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                          "</td>" +
                          "<td class='GridRows'>" + clsOvertime.GetRequestStatusRemarks(drw["otstat"].ToString(), drw["chartype"].ToString(), drw["apprname"].ToString(), drw["apprstat"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString(), drw["appcname"].ToString(), drw["appcstat"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblOvertime.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblOvertime.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadPaging()
 {
  Response.Write(clsOvertime.GetPaging(OvertimeUsers.ApproverDivision, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlStatus.SelectedValue, "OvertimeAllAD"));
 }

 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OvertimeMenu.aspx");
 }

}