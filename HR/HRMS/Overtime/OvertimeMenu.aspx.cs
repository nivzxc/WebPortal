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

public partial class HR_HRMS_Overtime_OvertimeMenu : System.Web.UI.Page
{

 protected void LoadOvertimeApproverCOO()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeMenu(OvertimeUsers.ApproverCOO, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblOvertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAC.aspx?otcode=" + drw["otcode"] + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["otstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAC.aspx?otcode=" + drw["otcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
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

 protected void LoadOvertimeApproverDivision()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeMenu(OvertimeUsers.ApproverDivision, 9, Request.Cookies["Speedo"]["UserName"]);
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

 protected void LoadOvertimeApproverDivisionAffirmation()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeMenuAffirmation(Request.Cookies["Speedo"]["UserName"]);
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

 protected void LoadOvertimeApproverHead()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeMenu(OvertimeUsers.ApproverHead, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblOvertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAH.aspx?otcode=" + drw["otcode"] + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["otstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAH.aspx?otcode=" + drw["otcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
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

 protected void LoadOvertimeApproverRequestor()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeMenu(OvertimeUsers.ApproverRequestor, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblOvertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAR.aspx?otcode=" + drw["otcode"] + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["otstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetailsAR.aspx?otcode=" + drw["otcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                           "Sent By: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
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

 protected void LoadOvertime()
 {
  string strWrite = "";
  DataTable tblOvertime = clsOvertime.DSGOvertimeMenu(OvertimeUsers.Requestor, 9, Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblOvertime.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetails.aspx?otcode=" + drw["otcode"].ToString() + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["otstat"].ToString()) + "' alt='' /></a>" +
                          "</td>" +
                          "<td class='GridRows'>" +
                           "<a href='OvertimeDetails.aspx?otcode=" + drw["otcode"].ToString() + "' style='font-size:small;'>" + drw["reason"].ToString() + "</a><br>" +
                           "[" + clsValidator.CheckDate(drw["datestrt"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "] - [" + clsValidator.CheckDate(drw["dateend"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + "]<br>" +
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

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   string strUsername = Request.Cookies["Speedo"]["UserName"];
   if (clsModuleApprover.IsApprover(strUsername, clsModule.OvertimeModule, "1"))
   {
    trApproverWithin.Visible = true;
    trApproverWithinSpacer.Visible = true;
    trApproverOutside.Visible = true;
    trApproverOutsideSpace.Visible = true;
   }
   else
   {
    trApproverWithin.Visible = false;
    trApproverWithinSpacer.Visible = false;
    trApproverOutside.Visible = false;
    trApproverOutsideSpace.Visible = false;
   }

   if (clsModuleApprover.IsApprover(strUsername, clsModule.OvertimeModule, "2"))
   {
    trApproverDivision.Visible = true;
    trApproverDivisionSpacer.Visible = true;
   }
   else
   {
    trApproverDivision.Visible = false;
    trApproverDivisionSpacer.Visible = false;
   }

   if (clsModuleApprover.IsApprover(strUsername, clsModule.OvertimeModule, "3"))
   {
    trApproverCOO.Visible = true;
    trApproverCOOSpacer.Visible = true;
   }
   else
   {
    trApproverCOO.Visible = false;
    trApproverCOOSpacer.Visible = false;
   }

  }
 }

 protected void btnNewRequest_Click(object sender, EventArgs e)
 {
  Response.Redirect("OvertimeNew.aspx");
 }


}