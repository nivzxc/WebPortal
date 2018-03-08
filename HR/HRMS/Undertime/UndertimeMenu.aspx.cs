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

public partial class HR_HRMS_Undertime_UndertimeMenu : System.Web.UI.Page
{

 protected void LoadUndertimeA()
 {
  string strWrite = "";
  DataTable tblUndertime = clsUndertime.GetTopRecords(UndertimeUsers.Approver, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drow in tblUndertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='UndertimeDetailsA.aspx?utcode=" + drow["utcode"] + "'><img src='../../../Support/" + clsUndertime.GetRequestStatusIcon(drow["utstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='UndertimeDetailsA.aspx?utcode=" + drow["utcode"] + "' style='font-size:small;'>" + Convert.ToDateTime(drow["dateapp"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</a>" + 
                           " by <a href='../../../Userpage/UserPage.aspx?username=" + drow["username"] + "'>" + drow["username"] + "</a><br>" +
                           "Date Filed: " + Convert.ToDateTime(drow["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                          "</td>" +
                          "<td class='GridRows'>" + clsUndertime.GetRequestStatusRemarks(drow["utstat"].ToString(), drow["apphname"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblUndertime.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblUndertime.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadUndertime()
 {
  string strWrite = "";
  DataTable tblUndertime = clsUndertime.GetTopRecords(UndertimeUsers.Requestor, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drow in tblUndertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='UndertimeDetails.aspx?utcode=" + drow["utcode"] + "'><img src='../../../Support/" + clsUndertime.GetRequestStatusIcon(drow["utstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='UndertimeDetails.aspx?utcode=" + drow["utcode"] + "' style='font-size:small;'>" + Convert.ToDateTime(drow["dateapp"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "</a><br>" +
                           "Date Filed: " + Convert.ToDateTime(drow["datefile"].ToString()).ToString("MMM dd, yyyy") +
                          "</td>" +
                          "<td class='GridRows'>" + clsUndertime.GetRequestStatusRemarks(drow["utstat"].ToString(), drow["apphname"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblUndertime.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblUndertime.Rows.Count + " records found ]</td></tr>");
 }

 protected void Page_Load(object sender, EventArgs e)
 {

 }

 protected void btnNewRequest_Click(object sender, EventArgs e)
 {
  Response.Redirect("UndertimeNew.aspx");
 }

}
