using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class HR_HRMS_Reports_rptProjectBasedContractualConsultant : System.Web.UI.Page
{

 protected void LoadRecords()
 {
  string strWrite = "";
  int intCtr = 1;

  DataTable tblPCC = clsEmployee.DSRProjectBasedConsultantContractual(clsDateTime.GetDateOnly(dtpAsOf.Date));
  foreach (DataRow drw in tblPCC.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows'>" + intCtr.ToString() + ".</td>" +
                "<td class='GridRows'>" + drw["RC"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["EmployeeName"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["Position"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["DateStart"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["DateEnd"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["Tenure"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["BirthDate"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["Age"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["Gender"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["EmploymentStatus"].ToString() + "</td>" +                
                "<td class='GridRows' style='text-align:center;'>" + drw["Division"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["Department"].ToString() + "</td>" +
                "<td class='GridRows'>" + drw["Remarks"].ToString() + "</td>" +
               "</tr>";
   intCtr += 1;
  }

  if (strWrite == "")
   strWrite = "<tr><td class='GridRows' colspan='13'>&nbsp;No record found.</td></tr>";
  Response.Write(strWrite);
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
   dtpAsOf.Date = DateTime.Now;
 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("~/HR/HRMS/HRMS.aspx");
 }

}