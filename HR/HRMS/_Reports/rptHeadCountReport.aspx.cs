using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class HR_HRMS_Reports_rptHeadCountReport : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  int intCtr = 1;

  int intTPCA = 0;
  int intTFCA = 0;
  int intTUCA = 0;
  int intTPCH = 0;
  int intTFCH = 0;
  int intTUCH = 0;
  int intTPCB = 0;
  int intTFCB = 0;
  int intTUCB = 0;

  DataTable tblHeadCountReport = clsReport.DSGHeadCountReport(clsDateTime.GetDateOnly(dtpFocusDate.Date));
  foreach (DataRow drw in tblHeadCountReport.Rows)
  {
   intTPCA += clsValidator.CheckInteger(drw["PCA"].ToString());
   intTFCA += clsValidator.CheckInteger(drw["FCA"].ToString());
   intTUCA += clsValidator.CheckInteger(drw["UCA"].ToString());
   intTPCH += clsValidator.CheckInteger(drw["PCH"].ToString());
   intTFCH += clsValidator.CheckInteger(drw["FCH"].ToString());
   intTUCH += clsValidator.CheckInteger(drw["UCH"].ToString());
   intTPCB += clsValidator.CheckInteger(drw["PCB"].ToString());
   intTFCB += clsValidator.CheckInteger(drw["FCB"].ToString());
   intTUCB += clsValidator.CheckInteger(drw["UCB"].ToString());

   strWrite += "<tr>" +
                "<td class='GridRows'>" + drw["JGCode"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["PCA"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["FCA"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["UCA"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["PCH"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["FCH"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["UCH"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["PCB"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["FCB"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["UCB"].ToString() + "</td>" +
               "<tr>";
   intCtr += 1;
  }

  strWrite += "<tr>" +
               "<td class='GridRows'><b>Total</b></td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTPCA.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTFCA.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTUCA.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTPCH.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTFCH.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTUCH.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTPCB.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTFCB.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intTUCB.ToString() + "</td>" +
              "<tr>";

  Response.Write(strWrite);
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {

 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("~/HR/HRMS/HRMS.aspx");
 }

}