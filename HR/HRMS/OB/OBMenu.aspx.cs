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

public partial class HR_HRMS_OB_OBMenu : System.Web.UI.Page
{

 protected void LoadOBApproverWithin()
 {
  string strWrite = "";
  DataTable tblOB = clsOB.GetDataTableHATop(9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblOB.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OBDetailsA.aspx?obcode=" + drw["obcode"] + "'><img src='../../../Support/" + clsOB.GetRequestStatusIcon(drw["obstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OBDetailsA.aspx?obcode=" + drw["obcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                           "Sent by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                           "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy") +
                          "</td>" +
                          "<td class='GridRows'>" + clsOB.GetRequestStatusRemarks(drw["obstat"].ToString(), drw["obtype"].ToString(), drw["apprname"].ToString(), drw["apprstat"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblOB.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblOB.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadOBApproverOther()
 {
  string strWrite = "";
  DataTable tblOB = clsOB.GetDataTableRATop(9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblOB.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OBDetailsAR.aspx?obcode=" + drw["obcode"] + "'><img src='../../../Support/" + clsOB.GetRequestStatusIcon(drw["obstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OBDetailsAR.aspx?obcode=" + drw["obcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                           "Submitted by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                           "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy") +
                          "</td>" +
                          "<td class='GridRows'>" + clsOB.GetRequestStatusRemarks(drw["obstat"].ToString(), drw["obtype"].ToString(), drw["apprname"].ToString(), drw["apprstat"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblOB.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblOB.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadOB()
 {
  string strWrite = "";
  DataTable tblOB = clsOB.GetTopRecords(OBUsers.Requestor, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblOB.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OBDetails.aspx?obcode=" + drw["obcode"] + "'><img src='../../../Support/" + clsOB.GetRequestStatusIcon(drw["obstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OBDetails.aspx?obcode=" + drw["obcode"] + "' style='font-size:small;'>" + drw["reason"].ToString() + "</a><br>" +
                           clsOB.GetOBTypeDesc(drw["obtype"].ToString()) + "<br>" +
                           "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                          "</td>" +
                          "<td class='GridRows'>" + clsOB.GetRequestStatusRemarks(drw["obstat"].ToString(), drw["obtype"].ToString(), drw["apprname"].ToString(), drw["apprstat"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblOB.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblOB.Rows.Count + " records found ]</td></tr>");
 }

 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

 protected void btnNewRequest_Click(object sender, EventArgs e)
 {
  Response.Redirect("OBNew.aspx");
 }


}