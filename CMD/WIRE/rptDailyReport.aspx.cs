using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class CMD_WIRE_rptDailyReport : System.Web.UI.Page
{

 //protected void LoadReports()
 //{
 // DataTable tblSchools = new DataTable();
 // DateTime dteAppDate = DateTime.Now;

 // // total items
 // int intCHEDInq = 0;
 // int intCHEDReg = 0;
 // int intCHEDEnr = 0;
 // int intTESDAInq = 0;
 // int intTESDAReg = 0;
 // int intTESDAEnr = 0;
 // int intTYInq = 0;
 // int intTYReg = 0;
 // int intTYEnr = 0;
 // int intLYInq = 0;
 // int intLYReg = 0;
 // int intLYEnr = 0;

 // // temporary items
 // int intTempChedInq = 0;
 // int intTempChedReg = 0;
 // int intTempChedEnr = 0;
 // int intTempTesdaInq = 0;
 // int intTempTesdaReg = 0;
 // int intTempTesdaEnr = 0;
 // int intTempTYInq = 0;
 // int intTempTYReg = 0;
 // int intTempTYEnr = 0;
 // int intTempLYInq = 0;
 // int intTempLYReg = 0;
 // int intTempLYEnr = 0;
 // string strRowClass = "";
 // string strWrite = "";

 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT schlcode,schlname,lastnsup FROM schools WHERE schools.active='1' ORDER BY schlname";
 //  MySqlDataReader dr;

 //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
 //  cn.Open();
 //  da.Fill(tblSchools);

 //  foreach (DataRow drowSchl in tblSchools.Rows)
 //  {
 //   // get the last ns update
 //   cmd.CommandText = "SELECT appdate FROM " + clsWIRE.TableNameEncodeDates + " WHERE schlcode='" + drowSchl["schlcode"] + "' AND nsencode='1' ORDER BY appdate DESC LIMIT 1";
 //   dr = cmd.ExecuteReader();
 //   if (dr.Read())
 //    dteAppDate = Convert.ToDateTime(dr["appdate"].ToString());
 //   else
 //    dteAppDate = Convert.ToDateTime(clsWIRE.StartDate);
 //   dr.Close();

 //   // all ched
 //   cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq, SUM(ns_reg) AS tns_reg, SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameTy + " INNER JOIN programs ON " + clsWIRE.TableNameTy + ".progcode = programs.progcode WHERE " + clsWIRE.TableNameTy + ".schlcode='" + drowSchl["schlcode"] + "' AND progclass='c'";
 //   dr = cmd.ExecuteReader();
 //   dr.Read();
 //   intTempChedInq = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //   intTempChedReg = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //   intTempChedEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //   dr.Close();

 //   intCHEDInq += intTempChedInq;
 //   intCHEDReg += intTempChedReg;
 //   intCHEDEnr += intTempChedEnr;


 //   // all tesda
 //   cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq, SUM(ns_reg) AS tns_reg, SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameTy + " INNER JOIN programs ON " + clsWIRE.TableNameTy + ".progcode = programs.progcode WHERE " + clsWIRE.TableNameTy + ".schlcode='" + drowSchl["schlcode"] + "' AND progclass='t'";
 //   dr = cmd.ExecuteReader();
 //   dr.Read();
 //   intTempTesdaInq = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //   intTempTesdaReg = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //   intTempTesdaEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //   dr.Close();

 //   intTESDAInq += intTempTesdaInq;
 //   intTESDAReg += intTempTesdaReg;
 //   intTESDAEnr += intTempTesdaEnr;

 //   // this year ire
 //   cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq, SUM(ns_reg) AS tns_reg, SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameTy + " INNER JOIN programs ON " + clsWIRE.TableNameTy + ".progcode = programs.progcode WHERE " + clsWIRE.TableNameTy + ".schlcode='" + drowSchl["schlcode"] + "'";
 //   dr = cmd.ExecuteReader();
 //   dr.Read();
 //   intTempTYInq = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //   intTempTYReg = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //   intTempTYEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //   dr.Close();

 //   intTYInq += intTempTYInq;
 //   intTYReg += intTempTYReg;
 //   intTYEnr += intTempTYEnr;


 //   // last year WIRE
 //   cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq, SUM(ns_reg) AS tns_reg, SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameLy + " INNER JOIN programs ON " + clsWIRE.TableNameLy + ".progcode = programs.progcode WHERE " + clsWIRE.TableNameLy + ".schlcode='" + drowSchl["schlcode"] + "' AND appdate <= '" + dteAppDate.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //   dr = cmd.ExecuteReader();
 //   dr.Read();
 //   intTempLYInq = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //   intTempLYReg = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //   intTempLYEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //   dr.Close();

 //   intLYInq += intTempLYInq;
 //   intLYReg += intTempLYReg;
 //   intLYEnr += intTempLYEnr;

 //   strRowClass = (intTempTYEnr >= intTempLYEnr ? "GridRows" : "GridRowsRed");
 //   strWrite += "<tr>" +
 //                "<td class='" + strRowClass + "' style='text-align:left;'><a href='CMIREReports.aspx?schlcode=" + drowSchl["schlcode"] + "'>" + drowSchl["schlname"] + "</a></td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempChedInq) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempChedReg) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempChedEnr) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempTesdaInq) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempTesdaReg) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempTesdaEnr) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempTYInq) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempTYReg) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempTYEnr) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempLYInq) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempLYReg) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:right;'>" + clsValidator.ZeroToDash(intTempLYEnr) + "</td>" +
 //                "<td class='" + strRowClass + "' style='text-align:center;'>" + dteAppDate.ToString("MM-dd-yyyy") + "</td>" +
 //               "</tr>";
 //  }

 // }
 // strWrite += "<tr>" +
 //              "<td class='GridColumns' style='text-align:left;'><b>Total CHED Programs:</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intCHEDInq) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intCHEDReg) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intCHEDEnr) + "</b></td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //             "</tr>" +
 //             "<tr>" +
 //              "<td class='GridColumns' style='text-align:left;'><b>Total TESDA Programs:</b></td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intTESDAInq) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intTESDAReg) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intTESDAEnr) + "</b></td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //             "</tr>" +
 //             "<tr>" +
 //              "<td class='GridColumns' style='text-align:left;'><b>Grand Total:</b></td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intTYInq) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intTYReg) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intTYEnr) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intLYInq) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intLYReg) + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + clsValidator.ZeroToDash(intLYEnr) + "</b></td>" +
 //              "<td class='GridColumns'>&nbsp;</td>" +
 //             "</tr>";

 // Response.Write(strWrite);
 //}

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
   clsWIRE.AuthenticateUser(Request.Cookies["Speedo"]["UserName"], clsWIRE.WireUsers.EliteUsers);
 }

 protected void btnExport_Click(object sender, ImageClickEventArgs e)
 {

 }

}
