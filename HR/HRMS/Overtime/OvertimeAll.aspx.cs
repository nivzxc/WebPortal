﻿using System;
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

public partial class HR_HRMS_Overtime_OvertimeAll : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeAll(OvertimeUsers.Requestor, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlRequestStatus.SelectedValue);
  foreach (DataRow drw in tblOvertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetails.aspx?otcode=" + drw["otcode"] + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["otstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetails.aspx?otcode=" + drw["otcode"] + "' style='font-size:small;'>" + drw["reason"].ToString() + "</a><br>" +
                           clsOvertime.GetChargeTypeDesc(drw["chartype"].ToString()) + "<br>" +
                           "[" + Convert.ToDateTime(drw["datestrt"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + " - " + Convert.ToDateTime(drw["dateend"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "]<br>" +
                           "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy") +
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
  Response.Write(clsOvertime.GetPaging(OvertimeUsers.Requestor, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlRequestStatus.SelectedValue, "OvertimeAll"));
 }

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OvertimeMenu.aspx");
 }
}