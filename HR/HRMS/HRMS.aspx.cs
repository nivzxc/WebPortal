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

public partial class HR_HRMS_HRMS : System.Web.UI.Page
{

 protected void LoadAgeBracketSummary()
 {
  string strWrite = "";
  int int20_29 = 0;
  int int30_39 = 0;
  int int40_49 = 0;
  int int50_59 = 0;
  int int60_69 = 0;
  int intTotal = 0;

  int20_29 = clsEmployee.CountEmployeeManpowerComplimentAge(20, 29);
  int30_39 = clsEmployee.CountEmployeeManpowerComplimentAge(30, 39);
  int40_49 = clsEmployee.CountEmployeeManpowerComplimentAge(40, 49);
  int50_59 = clsEmployee.CountEmployeeManpowerComplimentAge(50, 59);
  int60_69 = clsEmployee.CountEmployeeManpowerComplimentAge(60, 69);
  intTotal = int20_29 + int30_39 + int40_49 + int50_59 + int60_69;

  strWrite += "<tr>" +
               "<td class='GridRows'>20 - 29</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int20_29.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int20_29 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>30 - 39</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int30_39.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int30_39 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>40 - 49</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int40_49.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int40_49 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>50 - 59</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int50_59.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int50_59 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>60 - 69</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int60_69.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int60_69 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'><b>Total</b></td>" +
               "<td class='GridRows' style='text-align:center;'><b>" + intTotal.ToString() + "</b></td>" +
               "<td class='GridRows'>&nbsp;</td>" +
              "</tr>";

  Response.Write(strWrite);
 }

 protected void LoadTenureBracketSummary()
 {
  string strWrite = "";
  int int0 = 0;
  int int5 = 0;
  int int10 = 0;
  int int15 = 0;
  int int20 = 0;
  int int25 = 0;
  int int30 = 0;
  int intTotal = 0;

  int0 = clsEmployee.CountEmployeeManpowerComplimentTenure(0, 0);
  int5 = clsEmployee.CountEmployeeManpowerComplimentTenure(1, 5);
  int10 = clsEmployee.CountEmployeeManpowerComplimentTenure(6, 10);
  int15 = clsEmployee.CountEmployeeManpowerComplimentTenure(11, 15);
  int20 = clsEmployee.CountEmployeeManpowerComplimentTenure(16, 20);
  int25 = clsEmployee.CountEmployeeManpowerComplimentTenure(21, 25);
  int30 = clsEmployee.CountEmployeeManpowerComplimentTenure(26, 30);
  intTotal = int0 + int5 + int10 + int15 + int20 + int25 + int30;

  strWrite += "<tr>" +
               "<td class='GridRows'>< 1</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int0.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int0 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>1 - 5</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int5.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int5 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>6 - 10</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int10.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int10 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>11 - 15</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int15.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int15 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>16 - 20</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int20.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int20 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>21 - 25</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int25.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int25 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>26 - 30</td>" +
               "<td class='GridRows' style='text-align:center;'>" + int30.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)int30 / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'><b>Total</b></td>" +
               "<td class='GridRows' style='text-align:center;'><b>" + intTotal.ToString() + "</b></td>" +
               "<td class='GridRows'>&nbsp;</td>" +
              "</tr>";

  Response.Write(strWrite);
 }

 protected void LoadGenderSummary()
 {
  string strWrite = "";
  int intMale = 0;
  int intFemale = 0;
  int intTotal = 0;

  intMale = clsEmployee.CountEmployeeManpowerCompliment("M", "1");
  intFemale = clsEmployee.CountEmployeeManpowerCompliment("F", "1");
  intTotal = intMale + intFemale;

  strWrite += "<tr>" +
               "<td class='GridRows'>Male</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intMale.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)intMale / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'>Female</td>" +
               "<td class='GridRows' style='text-align:center;'>" + intFemale.ToString() + "</td>" +
               "<td class='GridRows' style='text-align:center;'>" + Math.Round(((float)intFemale / (float)intTotal) * 100, 2).ToString() + " %</td>" +
              "</tr>" +
              "<tr>" +
               "<td class='GridRows'><b>Total</b></td>" +
               "<td class='GridRows' style='text-align:center;'><b>" + intTotal.ToString() + "</b></td>" +
               "<td class='GridRows'>&nbsp;</td>" +
              "</tr>";

  Response.Write(strWrite);
 }

 protected void LoadDivisionCount()
 {
  string strWrite = "";
  int intTotal = 0;

  DataTable tblDivisionCount = clsDivision.DSREmployeeCountManpowerCompliment();

  foreach (DataRow drw in tblDivisionCount.Rows)
   intTotal += clsValidator.CheckInteger(drw["TotalEmployee"].ToString());

  foreach (DataRow drw in tblDivisionCount.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows'>" + drw["division"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["TotalEmployee"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + Math.Round((clsValidator.CheckFloat(drw["TotalEmployee"].ToString()) / (float)intTotal) * 100, 2).ToString() + " %</td>" +
               "</tr>";
  }

  strWrite += "<tr>" +
               "<td class='GridRows'><b>Total</b></td>" +
               "<td class='GridRows' style='text-align:center;'><b>" + intTotal.ToString() + "</b></td>" +
               "<td class='GridRows'>&nbsp;</td>" +
              "</tr>";

  Response.Write(strWrite);
 }

 protected void LoadJobClassificationSummary()
 {
  string strWrite = "";
  int intTotal = 0;

  DataTable tblJobClassification = clsJobGrade.DSRHeadCountJobClassification();

  foreach (DataRow drw in tblJobClassification.Rows)
   intTotal += clsValidator.CheckInteger(drw["TotalEmployee"].ToString());

  foreach (DataRow drw in tblJobClassification.Rows)
  {
   strWrite += "<tr>" +
                "<td class='GridRows'>" + drw["jgcname"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + drw["TotalEmployee"].ToString() + "</td>" +
                "<td class='GridRows' style='text-align:center;'>" + Math.Round((clsValidator.CheckFloat(drw["TotalEmployee"].ToString()) / (float)intTotal) * 100, 2).ToString() + " %</td>" +
               "</tr>";
  }

  strWrite += "<tr>" +
               "<td class='GridRows'><b>Total</b></td>" +
               "<td class='GridRows' style='text-align:center;'><b>" + intTotal.ToString() + "</b></td>" +
               "<td class='GridRows'>&nbsp;</td>" +
              "</tr>";

  Response.Write(strWrite);
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  bool blnHasManagementReportsAccess = clsSystemModule.HasAccess("013", Request.Cookies["Speedo"]["Username"]);
  trManagementReports.Visible = blnHasManagementReportsAccess;
  trManagementReportsSpace.Visible = blnHasManagementReportsAccess;

  bool blnHasManPowerReportAccess = clsSystemModule.HasAccess("020", Request.Cookies["Speedo"]["Username"]);
  trManpowerReportSpacer.Visible = blnHasManPowerReportAccess;
  trManpowerReport.Visible = blnHasManPowerReportAccess;
 }

}