using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HRMS;

public partial class HR_HRMS_Leave_LeaveAllA : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  DataTable tblLeave = clsLeave.GetPageRecords(LeaveUsers.Approver, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlRequestStatus.SelectedValue, "ALL");
  foreach (DataRow drow in tblLeave.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='LeaveDetailsA.aspx?leavcode=" + drow["leavcode"] + "'><img src='../../../Support/" + clsLeave.GetRequestStatusIcon(drow["leavstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='LeaveDetailsA.aspx?leavcode=" + drow["leavcode"] + "' style='font-size:small;'>" + clsString.CutString(drow["reason"].ToString(), 40) + "</a><br>" +
                           clsLeaveType.GetDescription(drow["leavtype"].ToString()) + "<br>" +
                           "Submitted by: <a href='../../../Userpage/UserPage.aspx?username=" + drow["username"] + "'>" + drow["username"] + "</a><br>" +
                           "[" + Convert.ToDateTime(drow["datestrt"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + " - " + Convert.ToDateTime(drow["dateend"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "]<br>" +
                           "Date Filed: " + Convert.ToDateTime(drow["datefile"].ToString()).ToString("MMM dd, yyyy") +
                          "</td>" +
                          "<td class='GridRows'>" + clsLeave.GetRequestStatusRemarks(drow["leavstat"].ToString(), drow["apphname"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblLeave.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblLeave.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadPaging()
 {
  Response.Write(clsLeave.GetPaging(LeaveUsers.Approver, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlRequestStatus.SelectedValue, "LeaveAllA"));
 }

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("LeaveMenu.aspx");
 }

}