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
using System.Data.SqlClient;
using HRMS;

public partial class HR_HRMS_OB_OBAll : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  DataTable tblOB = clsOB.GetPageRecords(OBUsers.Requestor, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlStatus.SelectedValue);
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

 protected void LoadPaging()
 {
  Response.Write(clsOB. GetPaging(OBUsers.Requestor, int.Parse(Request.QueryString["page"].ToString()), Request.Cookies["Speedo"]["UserName"], ddlStatus.SelectedValue, "OBAll"));
 }

 ///////// Form Events /////////

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("OBMenu.aspx");
 }

}