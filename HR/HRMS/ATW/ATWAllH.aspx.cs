using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class HR_HRMS_ATW_ATWAllH : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  DataTable tblATW = clsATW.GetPageRecords(ATWUsers.ApproverHead, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlStatus.SelectedValue);
  foreach (DataRow drw in tblATW.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='ATWDetailsH.aspx?atwcode=" + drw["atwcode"] + "'><img src='../../../Support/" + clsATW.GetRequestStatusIcon(drw["status"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='ATWDetailsH.aspx?atwcode=" + drw["atwcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                           "Sent by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                           "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                          "</td>" +
                          "<td class='GridRows'>" + clsATW.GetRequestStatusRemarks(drw["status"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString()) + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
  if (tblATW.Rows.Count == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblATW.Rows.Count + " records found ]</td></tr>");
 }

 protected void LoadPaging()
 {
  Response.Write(clsATW.GetPaging(ATWUsers.ApproverHead, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlStatus.SelectedValue, "ATWAllH"));
 }

 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("ATWMenu.aspx");
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
     Response.Redirect("ATWMenu.aspx");
 }
}