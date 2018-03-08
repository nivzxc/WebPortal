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

public partial class HR_HRMS_Leave_LeaveMenu : System.Web.UI.Page
{

 protected void LoadLeaveA()
 {
  string strWrite = "";
  DataTable tblLeave = clsLeave.GetTopRecords(LeaveUsers.Approver, 9, Request.Cookies["Speedo"]["UserName"]);
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

 protected void LoadLeave()
 {
  string strWrite = "";
  DataTable tblLeave = clsLeave.GetTopRecords(LeaveUsers.Requestor, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drow in tblLeave.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='LeaveDetails.aspx?leavcode=" + drow["leavcode"] + "'><img src='../../../Support/" + clsLeave.GetRequestStatusIcon(drow["leavstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='LeaveDetails.aspx?leavcode=" + drow["leavcode"] + "' style='font-size:small;'>" + clsString.CutString(drow["reason"].ToString(), 40) + "</a><br>" +
                           clsLeaveType.GetDescription(drow["leavtype"].ToString()) + "<br>" +
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

 protected void LoadLeaveBalance()
 {
  string strWrite = "";
  DataTable tblLeaveBalance = clsLeaveBalance.GetActiveLeaveBalance(Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drow in tblLeaveBalance.Rows)
  {
   strWrite = strWrite + "<tr>" +
    "<td class='GridRows'><img src='../../../Support/" + (drow["hasbal"].ToString() == "1" ? "flgApp2.png" : "flgCancel.png") + "' alt='' /></td>" +
                          "<td class='GridRows'>" + drow["ltdesc"] + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["maxbal"] : "-") + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["pused"] : "-") + "</td>" +
                          "<td class='GridRows' style='text-align:right;'>" + (drow["hasbal"].ToString() == "1" ? drow["pbalance"] : "-") + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

 protected void btnNewRequest_Click(object sender, EventArgs e)
 {
  Response.Redirect("LeaveNew.aspx");
 }
}
