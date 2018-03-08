using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using WebChart;

public partial class CMD_WIRE_WIRE : System.Web.UI.Page
{
 
 //   protected void LoadTop5NSEC()
 //   {
 // DateTime dteAppDate = DateTime.Now;
 // string strRowClass = "";

 // DataTable tblTY = new DataTable();
 // string strWrite = "";
 // bool blnHasRecord = false;
 // double dblTyTNsEnr = 0;
 //       double dblTyTNsReg = 0;
 //       double dblLyTNsEnr = 0;

 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 //       {
 //  MySqlDataReader dr;
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT SUM(ns_enr) AS tns_enr,SUM(ns_reg) AS tns_reg,schlname,schools.schlcode,schools.hqowned FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode WHERE schools.schltype='E' GROUP BY schlname,schlcode ORDER BY tns_enr DESC,tns_reg DESC,schlname LIMIT 5";
 //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
 //  cn.Open();
 //  da.Fill(tblTY);

 //  foreach(DataRow drow in tblTY.Rows)
 //           {
 //   dblTyTNsEnr = clsValidator.CheckDouble(drow["tns_enr"].ToString());
 //   dblTyTNsReg = clsValidator.CheckDouble(drow["tns_reg"].ToString());

 //   cmd.CommandText = "SELECT appdate FROM " + clsWIRE.TableNameEncodeDates + " WHERE schlcode='" + drow["schlcode"] + "' AND nsencode='1' ORDER BY appdate DESC LIMIT 1";
 //   dr = cmd.ExecuteReader();
 //   blnHasRecord = dr.Read();
 //   if (blnHasRecord)
 //    dteAppDate = Convert.ToDateTime(dr["appdate"].ToString());
 //   else
 //    dteAppDate = Convert.ToDateTime(clsWIRE.StartDate);
 //   dr.Close();

 //   cmd.CommandText = "SELECT SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameLy + " WHERE schlcode='" + drow["schlcode"] + "' AND appdate <= '" + dteAppDate.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //   dr = cmd.ExecuteReader();
 //   dr.Read();
 //   dblLyTNsEnr = clsValidator.CheckDouble(dr["tns_enr"].ToString());
 //               if (dblTyTNsEnr >= dblLyTNsEnr)
 //                   strRowClass = "GridRowsGreen";
 //               else if (dblTyTNsReg >= dblLyTNsEnr)
 //                   strRowClass = "GridRows";
 //               else
 //                   strRowClass = "GridRowsRed";

 //   strWrite = "<tr>" +
 //               "<td class='" + strRowClass + "' style='text-align:left'>" +
 //                "<table cellpadding='0' cellspacing='0'>" +
 //                 "<tr>" +
 //                  "<td><img src='../../Support/" + (drow["hqowned"].ToString() == "1" ? "bookmark16" : "star16") + ".png' ></td>" +
 //                  "<td>&nbsp;&nbsp;<a href='../SIS/SchoolsDirectoryDetails.aspx?schlcode=" + drow["schlcode"] + "&schlname=" + drow["schlname"] + "'>" + drow["schlname"] + "</a></td>" +
 //                 "</tr>" +
 //                "</table>" +
 //               "</td>" +
 //                                                               "<td class='" + strRowClass + "' style='text-align:right'>" + dblTyTNsReg + "</td>" +
 //                                                               "<td class='" + strRowClass + "' style='text-align:right'>" + dblTyTNsEnr + "</td>" +
 //                                                               "<td class='" + strRowClass + "' style='text-align:right'>" + dblLyTNsEnr + "</td>" +
 //                                                               "<td class='" + strRowClass + "' style='text-align:right'>" + (dblLyTNsEnr == 0 ? "0" : Math.Round(((-dblLyTNsEnr + dblTyTNsEnr) / dblLyTNsEnr) * 100,2).ToString()) + "%</td>" + 
 //              "</tr>";
 //   Response.Write(strWrite);
 //   dr.Close();
 //  }
 //    }
 //}

  //  protected void LoadTop5NSCollege()
  //  {
  //DateTime dteAppDate = DateTime.Now;
  //DataTable tblTY = new DataTable();
  //string strRowClass = "";
  //string strWrite = "";
  //double dblTyTNsEnr = 0;
  //      double dblTyTNsReg = 0;
  //      double dblLyTNsEnr = 0;
  //bool blnHasRecord = false;

  //using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
  //      {
  // MySqlDataReader dr;
  // MySqlCommand cmd = cn.CreateCommand();
  //          cmd.CommandText = "SELECT SUM(ns_enr) AS tns_enr,SUM(ns_reg) AS tns_reg,schlname,schools.schlcode,schools.hqowned FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode WHERE schools.schltype='C' GROUP BY schlname,schlcode ORDER BY tns_enr desc,schlname LIMIT 5";
  // MySqlDataAdapter da = new MySqlDataAdapter(cmd);
  // cn.Open();
  // da.Fill(tblTY);

  // foreach(DataRow drow in tblTY.Rows)
  //          {
  //  dblTyTNsEnr = clsValidator.CheckDouble(drow["tns_enr"].ToString());
  //  dblTyTNsReg = clsValidator.CheckDouble(drow["tns_reg"].ToString());

  //  cmd.CommandText = "SELECT appdate FROM " + clsWIRE.TableNameEncodeDates + " WHERE schlcode='" + drow["schlcode"] + "' AND nsencode='1' ORDER BY appdate DESC LIMIT 1";
  //  dr = cmd.ExecuteReader();
  //  blnHasRecord = dr.Read();
  //  if (blnHasRecord)
  //   dteAppDate = Convert.ToDateTime(dr["appdate"].ToString());
  //  dr.Close();

  //  if (blnHasRecord)
  //              {
  //   cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR FROM " + clsWIRE.TableNameLy + " WHERE schlcode='" + drow["schlcode"] + "' AND appdate <= '" + dteAppDate.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
  //   dr = cmd.ExecuteReader();
  //   dr.Read();
  //   dblLyTNsEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
  //   if(dblTyTNsEnr >= dblLyTNsEnr)
  //    strRowClass = "GridRowsGreen";
  //                  else if (dblTyTNsReg >= dblLyTNsEnr)
  //    strRowClass = "GridRows";
  //   else
  //                      strRowClass = "GridRowsRed";

  //   strWrite = "<tr>" +
  //               "<td class='" + strRowClass + "' style='text-align:left'>" +
  //                "<table cellpadding='0' cellspacing='0'>" +
  //                 "<tr>" +
  //                  "<td><img src='../../Support/" + (drow["hqowned"].ToString() == "1" ? "bookmark16" : "star16") + ".png' ></td>" +
  //                  "<td>&nbsp;&nbsp;<a href='../SIS/SchoolsDirectoryDetails.aspx?schlcode=" + drow["schlcode"] + "&schlname=" + drow["schlname"] + "'>" + drow["schlname"] + "</a></td>" +
  //                 "</tr>" +
  //                "</table>" +
  //               "</td>" +
  //               "<td class='" + strRowClass + "' style='text-align:right'>" + dblTyTNsReg + "</td>" +
  //               "<td class='" + strRowClass + "' style='text-align:right'>" + dblTyTNsEnr + "</td>" +
  //               "<td class='" + strRowClass + "' style='text-align:right'>" + dblLyTNsEnr + "</td>" +
  //                                                                  "<td class='" + strRowClass + "' style='text-align:right'>" + (dblLyTNsEnr == 0 ? "" : Math.Round(((-dblLyTNsEnr + dblTyTNsEnr) / dblLyTNsEnr) * 100,2) + "%") + "</td>" +
  //              "</tr>";
  //   Response.Write(strWrite);
  //   dr.Close();
  //           }
  // }
  //}
  //  }

 //protected void LoadMostImprovedSchoolCollege()
 //{
 // DateTime dteAppDate = DateTime.Now;

 // DataTable tblTY = new DataTable();

 // // Schema definition
 // DataTable tblGrowth = new DataTable();
 // DataColumn pcol;
 // DataRow prow;
 // pcol = new DataColumn("schlcode", System.Type.GetType("System.String"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("schlname", System.Type.GetType("System.String"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("appdate", System.Type.GetType("System.DateTime"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("ty", System.Type.GetType("System.Int32"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("ly", System.Type.GetType("System.Int32"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("growth", System.Type.GetType("System.Double"));
 // tblGrowth.Columns.Add(pcol);

 // int intTyTNsEnr = 0;
 // int intLyTNsEnr = 0;

 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  MySqlDataReader dr;
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR,schlname,schools.schlcode,schools.hqowned FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode WHERE schools.schltype='C' GROUP BY schlname,schlcode ORDER BY tns_enr desc,schlname";
 //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
 //  cn.Open();
 //  da.Fill(tblTY);
 //  foreach (DataRow drow in tblTY.Rows)
 //  {
 //   intTyTNsEnr = clsValidator.CheckInteger(drow["tns_enr"].ToString());

 //   cmd.CommandText = "SELECT appdate FROM " + clsWIRE.TableNameEncodeDates + " WHERE schlcode='" + drow["schlcode"] + "' AND nsencode='1' ORDER BY appdate DESC LIMIT 1";
 //   dr = cmd.ExecuteReader();
 //   if (dr.Read())
 //    dteAppDate = Convert.ToDateTime(dr["appdate"].ToString());
 //   else
 //    dteAppDate = Convert.ToDateTime(clsWIRE.StartDate);
 //   dr.Close();

 //   cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR FROM " + clsWIRE.TableNameLy + " WHERE schlcode='" + drow["schlcode"] + "' AND appdate <= '" + dteAppDate.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //   dr = cmd.ExecuteReader();
 //   dr.Read();
 //   intLyTNsEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //   prow = tblGrowth.NewRow();
 //   prow["schlcode"] = drow["schlcode"];
 //   prow["schlname"] = "<table cellpadding='0' cellspacing='0'>" +
 //                       "<tr>" +
 //                        "<td><img src='../../Support/" + (drow["hqowned"].ToString() == "1" ? "bookmark16" : "star16") + ".png' ></td>" +
 //                        "<td>&nbsp;&nbsp;<a href='../SIS/SchoolsDirectoryDetails.aspx?schlcode=" + drow["schlcode"] + "&schlname=" + drow["schlname"] + "'>" + drow["schlname"] + "</a></td>" +
 //                       "</tr>" +
 //                      "</table>";
 //   prow["appdate"] = dteAppDate;
 //   prow["ty"] = drow["tns_enr"];
 //   prow["ly"] = dr["tns_enr"];
 //   if (intLyTNsEnr == 0)
 //    prow["growth"] = "0";
 //   else
 //    prow["growth"] = ((-intLyTNsEnr + intTyTNsEnr) / intLyTNsEnr) * 100;
 //   tblGrowth.Rows.Add(prow);
 //   dr.Close();
 //  }
 // }

 // string strWrite = "";
 // int intCtr = 0;
 // DataView pview = new DataView(tblGrowth);
 // pview.Sort = "growth DESC";
 // foreach (DataRowView dvr in pview)
 // {
 //  strWrite = "<tr>" +
 //              "<td class='GridRows' style='text-align:left'>" + dvr["schlname"] + "</td>" +
 //              "<td class='GridRows' style='text-align:right'>" + dvr["ty"] + "</td>" +
 //              "<td class='GridRows' style='text-align:right'>" + clsValidator.CheckInteger(dvr["ly"].ToString()) + "</td>" +
 //              "<td class='GridRows' style='text-align:right'>" + dvr["growth"] + "%</td>" +
 //             "</tr>";
 //  Response.Write(strWrite);
 //  intCtr++;
 //  if (intCtr == 5)
 //   break;
 // }

 //}

 //protected void LoadMostImprovedSchoolEC()
 //{
 // DateTime dteAppDate = DateTime.Now;
 // DataTable tblTY = new DataTable();

 // // schema definition
 // DataTable tblGrowth = new DataTable();
 // DataColumn pcol;
 // DataRow prow;

 // pcol = new DataColumn("schlcode", System.Type.GetType("System.String"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("schlname", System.Type.GetType("System.String"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("appdate", System.Type.GetType("System.DateTime"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("ty", System.Type.GetType("System.Int32"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("ly", System.Type.GetType("System.Int32"));
 // tblGrowth.Columns.Add(pcol);
 // pcol = new DataColumn("growth", System.Type.GetType("System.Double"));
 // tblGrowth.Columns.Add(pcol);

 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  MySqlDataReader dr;
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR,schlname,schools.schlcode,schools.hqowned FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode WHERE schools.schltype='E' GROUP BY schlname,schlcode ORDER BY tns_enr desc,schlname";
 //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
 //  cn.Open();
 //  da.Fill(tblTY);
 //  foreach (DataRow drow in tblTY.Rows)
 //  {
 //   cmd.CommandText = "SELECT appdate FROM " + clsWIRE.TableNameEncodeDates + " WHERE schlcode='" + drow["schlcode"] + "' AND nsencode='1' ORDER BY appdate DESC LIMIT 1";
 //   dr = cmd.ExecuteReader();
 //   if (dr.Read())
 //    dteAppDate = Convert.ToDateTime(dr["appdate"].ToString());
 //   else
 //    dteAppDate = Convert.ToDateTime(clsWIRE.StartDate);
 //   dr.Close();

 //   cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR FROM " + clsWIRE.TableNameLy + " WHERE schlcode='" + drow["schlcode"] + "' AND appdate <= '" + dteAppDate.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //   dr = cmd.ExecuteReader();
 //   dr.Read();
 //   prow = tblGrowth.NewRow();
 //   prow["schlcode"] = drow["schlcode"];
 //   prow["schlname"] = "<table cellpadding='0' cellspacing='0'>" +
 //                       "<tr>" +
 //                        "<td><img src='../../Support/" + (drow["hqowned"].ToString() == "1" ? "bookmark16" : "star16") + ".png' ></td>" +
 //                        "<td>&nbsp;&nbsp;<a href='../SIS/SchoolsDirectoryDetails.aspx?schlcode=" + drow["schlcode"] + "&schlname=" + drow["schlname"] + "'>" + drow["schlname"] + "</a></td>" +
 //                       "</tr>" +
 //                      "</table>";
 //   prow["appdate"] = dteAppDate;
 //   prow["ty"] = drow["tns_enr"];
 //   prow["ly"] = dr["tns_enr"];
 //   if (clsValidator.CheckInteger(dr["tns_enr"].ToString()) == 0)
 //    prow["growth"] = "0";
 //   else
 //    prow["growth"] = (((-clsValidator.CheckInteger(dr["tns_enr"].ToString()) + clsValidator.CheckInteger(drow["tns_enr"].ToString())) / clsValidator.CheckInteger(dr["tns_enr"].ToString())) * 100);
 //   tblGrowth.Rows.Add(prow);
 //   dr.Close();
 //  }
 // }

 // string strWrite = "";
 // int intCtr = 0;
 // DataView dvGrowth = new DataView(tblGrowth);
 // dvGrowth.Sort = "growth DESC";
 // foreach (DataRowView dvr in dvGrowth)
 // {
 //  strWrite = "<tr>" +
 //              "<td class='GridRows' style='text-align:left;'>" + dvr["schlname"] + "</td>" +
 //              "<td class='GridRows' style='text-align:right;'>" + dvr["ty"] + "</td>" +
 //              "<td class='GridRows' style='text-align:right;'>" + dvr["ly"] + "</td>" +
 //              "<td class='GridRows' style='text-align:right;'>" + dvr["growth"] + "%</td>" +
 //             "</tr>";
 //  Response.Write(strWrite);
 //  intCtr++;
 //  if (intCtr == 5)
 //   break;
 // }
 //}

 //   protected void Page_Load(object sender, EventArgs e)
 //{
 //       if (!Page.IsPostBack)
 //       {
 //           if (clsWIRE.IsUser(clsWIRE.WireUsers.EliteUsers, Request.Cookies["Speedo"]["UserName"].ToString()))
 //           {
 //               string strWrite = "";
 //               double dblTYEnr = 0;
 //               double dblTYReg = 0;
 //               double dblLYEnr = 0;
 //               double dblLYReg = 0;

 //               DataTable tblTyEnr = new DataTable();
 //               DataTable tblTyReg = new DataTable();
 //               DataTable tblTyReg2 = new DataTable();
 //               DataTable tblLyEnr = new DataTable();

 //               using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 //               {
 //                   MySqlDataAdapter da = new MySqlDataAdapter();
 //                   MySqlCommand cmd = cn.CreateCommand();
 //                   cmd.CommandText = "SELECT SUM(ns_enr) AS TNS_ENR,SUM(ns_reg) AS TNS_REG FROM " + clsWIRE.TableNameTy;
 //                   cn.Open();
 //                   MySqlDataReader dr = cmd.ExecuteReader();
 //                   if (dr.Read())
 //                   {
 //     dblTYEnr = clsValidator.CheckInteger(dr["TNS_ENR"].ToString());
 //     dblTYReg = clsValidator.CheckInteger(dr["TNS_REG"].ToString());
 //                   }
 //                   dr.Close();

 //                   cmd.CommandText = "SELECT SUM(ns_enr) AS TNS_ENR,SUM(ns_reg) AS TNS_REG FROM " + clsWIRE.TableNameLy + " WHERE appdate <= '" + DateTime.Now.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //                   dr = cmd.ExecuteReader();
 //                   if (dr.Read())
 //                   {
 //     dblLYEnr += clsValidator.CheckInteger(dr["TNS_ENR"].ToString());
 //     dblLYReg += clsValidator.CheckInteger(dr["TNS_REG"].ToString());
 //                   }
 //                   dr.Close();

 //                   lblLyAsOf.Text = DateTime.Now.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("ddd MM/dd/yyyy");
 //                   lblTyEnr.Text = dblTYEnr.ToString("###,##0");
 //                   lblLyEnr.Text = dblLYEnr.ToString("###,##0");
 //                   lblTyReg.Text = dblTYReg.ToString("###,##0");
 //                   lblLyReg.Text = dblLYReg.ToString("###,##0");
 //                   lblIncDec.ForeColor = (dblLYEnr > dblTYEnr ? Color.Crimson : Color.RoyalBlue);
 //    if (dblLYEnr == 0)
 //     lblIncDec.Text = "0% increase";
 //    else
 //     lblIncDec.Text = Math.Round(Math.Abs(((-dblLYEnr + dblTYEnr) / dblLYEnr) * 100), 2) + (dblLYEnr > dblTYEnr ? "% decrease</b>" : "% increase</b>");

 //                   // Submission rate computation
 //                   int intToday = 0;
 //                   int intYesterday = 0;
 //                   int int3Day = 0;
 //                   cmd.CommandText = "SELECT schools.schlcode,MAX(appdate) AS MaxDate FROM schools INNER JOIN " + clsWIRE.TableNameEncodeDates + " ON schools.schlcode = " + clsWIRE.TableNameEncodeDates + ".schlcode WHERE nsencode='1' AND schools.active='1' GROUP BY schools.schlcode UNION SELECT schools.schlcode,appdate AS MaxDate FROM schools INNER JOIN " + clsWIRE.TableNameEncodeDates + " ON schools.schlcode = " + clsWIRE.TableNameEncodeDates + ".schlcode WHERE nsencode='0' AND appdate='" + clsWIRE.StartDate + "' AND schools.active='1' GROUP BY schools.schlcode";
 //                   dr = cmd.ExecuteReader();
 //                   while (dr.Read())
 //                   {
 //                       if (Convert.ToDateTime(dr["MaxDate"].ToString()).ToString("yyyy-MM-dd") == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"))
 //                           intToday += 1;
 //                       else if (Convert.ToDateTime(dr["MaxDate"].ToString()).ToString("yyyy-MM-dd") == DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"))
 //                       {
 //                           intYesterday += 1;
 //                           intToday += 1;
 //                       }
 //                       else if (Convert.ToDateTime(dr["MaxDate"].ToString()) < DateTime.Now.AddDays(-2))
 //                       {
 //                           int3Day += 1;
 //                           intYesterday += 1;
 //                           intToday += 1;
 //                       }
 //                   }
 //                   dr.Close();

 //    lblSchlSubmit1.Text = "<a href='#' onclick=window.open('SchlSubmit.aspx?sub=1&percent=" + Math.Round(100 - ((Convert.ToDouble(intToday) / Convert.ToDouble(clsSIS.TotalSchools)) * 100),2) + "',null,'height=435,width=460,status=yes,toolbar=no,menubar=no,location=no,scrollbars=1')>Today: " + Math.Round((100 - ((Convert.ToDouble(intToday) / Convert.ToDouble(clsSIS.TotalSchools)) * 100)), 2) + "%</a>";
 //    lblSchlSubmit2.Text = "<a href='#' onclick=window.open('SchlSubmit.aspx?sub=2&percent=" + Math.Round(100 - ((Convert.ToDouble(intYesterday) / Convert.ToDouble(clsSIS.TotalSchools)) * 100), 2) + "',null,'height=435,width=460,status=yes,toolbar=no,menubar=no,location=no,scrollbars=1')>Yesterday: " + Math.Round((100 - ((Convert.ToDouble(intYesterday) / Convert.ToDouble(clsSIS.TotalSchools)) * 100)), 2) + "%</a>";
 //    lblSchlSubmit3.Text = "<a href='#' onclick=window.open('SchlSubmit.aspx?sub=3&percent=" + Math.Round(100 - ((Convert.ToDouble(int3Day) / Convert.ToDouble(clsSIS.TotalSchools)) * 100), 2) + "',null,'height=435,width=460,status=yes,toolbar=no,menubar=no,location=no,scrollbars=1')>Last 3 days: " + Math.Round((100 - ((Convert.ToDouble(int3Day) / Convert.ToDouble(clsSIS.TotalSchools)) * 100)), 2) + "%</a>";

 //                   // Load the chart
 //                   cmd.CommandText = "SELECT natldist.natldist,SUM(ns_enr) AS tns_enr FROM natldist INNER JOIN (schools INNER JOIN " + clsWIRE.TableNameTy + " ON schools.schlcode = " + clsWIRE.TableNameTy + ".schlcode) ON natldist.natlcode = schools.natlcode GROUP BY natldist ORDER BY natlgrp";
 //                   da.SelectCommand = cmd;
 //                   da.Fill(tblTyEnr);
 //                   cmd.CommandText = "SELECT natldist.natldist,SUM(ns_reg) - SUM(ns_enr) AS tns_reg FROM natldist INNER JOIN (schools INNER JOIN " + clsWIRE.TableNameTy + " ON schools.schlcode = " + clsWIRE.TableNameTy + ".schlcode) ON natldist.natlcode = schools.natlcode GROUP BY natldist ORDER BY natlgrp";
 //                   da.Fill(tblTyReg);
 //                   cmd.CommandText = "SELECT natldist.natldist,SUM(ns_reg) AS tns_reg FROM natldist INNER JOIN (schools INNER JOIN " + clsWIRE.TableNameTy + " ON schools.schlcode = " + clsWIRE.TableNameTy + ".schlcode) ON natldist.natlcode = schools.natlcode GROUP BY natldist ORDER BY natlgrp";
 //                   da.Fill(tblTyReg2);
 //                   cmd.CommandText = "SELECT natldist.natldist,SUM(ns_enr) AS tns_enr FROM natldist INNER JOIN (schools INNER JOIN " + clsWIRE.TableNameLy + " ON schools.schlcode = " + clsWIRE.TableNameLy + ".schlcode) ON natldist.natlcode = schools.natlcode WHERE appdate <= '" + DateTime.Now.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "' GROUP BY natldist ORDER BY natlgrp";
 //                   da.Fill(tblLyEnr);

 //                   //Get the this week code and date bracket
 //                   int[] intTWInq = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
 //                   int[] intTWReg = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
 //                   int[] intTWEnr = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
 //                   int[] intLYTWEnr = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
 //                   int intCtr = 0;
 //                   int intTWeekInq = 0;
 //                   int intTWeekReg = 0;
 //                   int intTWeekEnr = 0;
 //                   int intTInq = 0;
 //                   int intTReg = 0;
 //                   int intTEnr = 0;
 //                   int intLWInq = 0;
 //                   int intLWReg = 0;
 //                   int intLWEnr = 0;
 //                   int intLyLWEnr = 0;
 //                   int intLyTWeekEnr = 0;
 //                   bool blnTemp;
 //                   string strThisWeekCode = "";
 //                   string strLastWeekCode = "";
 //                   DateTime dteTemp;
 //                   DateTime dteThisWeekStart = DateTime.Now;
 //                   DateTime dteThisWeekEnd = DateTime.Now;
 //                   DateTime dteLastWeekStart = DateTime.Now;
 //                   DateTime dteLastWeekEnd = DateTime.Now;

 //                   // Total WIRE as of today
 //                   cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq,SUM(ns_reg) AS tns_reg,SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameTy;
 //                   dr = cmd.ExecuteReader();
 //                   if (dr.Read())
 //                   {
 //     intTInq = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //     intTReg = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //     intTEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //                   }
 //                   dr.Close();

 //                   //Get the this week total
 //                   cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq,SUM(ns_reg) AS tns_reg,SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameTy + " WHERE appdate BETWEEN (SELECT fromdate FROM " + clsWIRE.TableNameTyWeek + " WHERE '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN fromdate AND todate) AND (SELECT todate FROM " + clsWIRE.TableNameTyWeek + " WHERE '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN fromdate AND todate)";
 //                   dr = cmd.ExecuteReader();
 //                   if (dr.Read())
 //                   {
 //     intTWeekInq = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //     intTWeekReg = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //     intTWeekEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //                   }
 //                   dr.Close();

 //                   cmd.CommandText = "SELECT weeknum,fromdate,todate FROM " + clsWIRE.TableNameTyWeek + " WHERE '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN fromdate AND todate";
 //                   dr = cmd.ExecuteReader();
 //                   blnTemp = dr.Read();
 //                   if (blnTemp)
 //                   {
 //                       strThisWeekCode = dr["weeknum"].ToString();
 //                       dteThisWeekStart = Convert.ToDateTime(dr["fromdate"].ToString());
 //                       dteThisWeekEnd = Convert.ToDateTime(dr["todate"].ToString());
 //                   }
 //                   dr.Close();

 //                   if (strThisWeekCode != "")
 //                   {
 //                       // Get the last week code and date bracket
 //                       strLastWeekCode = Convert.ToString(Convert.ToInt32(strThisWeekCode) - 1);
 //                       cmd.CommandText = "SELECT weeknum,fromdate,todate FROM " + clsWIRE.TableNameTyWeek + " WHERE weeknum='" + strLastWeekCode + "'";
 //                       dr = cmd.ExecuteReader();
 //                       if (dr.Read())
 //                       {
 //                           dteLastWeekStart = Convert.ToDateTime(dr["fromdate"].ToString());
 //                           dteLastWeekEnd = Convert.ToDateTime(dr["todate"].ToString());
 //                       }
 //                       dr.Close();

 //                       // get the total of last week data
 //                       cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq, SUM(ns_reg) AS tns_reg, SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameTy + " WHERE appdate BETWEEN '" + dteLastWeekStart.ToString("yyyy-MM-dd") + "' AND '" + dteLastWeekEnd.ToString("yyyy-MM-dd") + "'";
 //                       dr = cmd.ExecuteReader();
 //                       if (dr.Read())
 //                       {
 //      intLWInq = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //      intLWReg = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //      intLWEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //                       }
 //                       dr.Close();


 //                       //get the last year counterpart of last week
 //                       cmd.CommandText = "SELECT SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameLy + " WHERE appdate BETWEEN '" + dteLastWeekStart.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "' AND '" + dteLastWeekEnd.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //                       dr = cmd.ExecuteReader();
 //                       if (dr.Read())
 //      intLyLWEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //                       else
 //                           intLyLWEnr = 0;
 //                       dr.Close();

 //                       //get the last year counterpart of this week
 //                       cmd.CommandText = "SELECT SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameLy + " WHERE appdate BETWEEN '" + dteThisWeekStart.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "' AND '" + dteThisWeekEnd.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //                       dr = cmd.ExecuteReader();
 //                       if (dr.Read())
 //      intLyTWeekEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //                       else
 //                           intLyTWeekEnr = 0;
 //                       dr.Close();

 //                       // browse the wire from this week monday to saturday
 //                       intCtr = 0;
 //                       dteTemp = dteThisWeekStart;
 //                       while (dteTemp <= dteThisWeekEnd)
 //                       {
 //                           cmd.CommandText = "SELECT SUM(ns_inq) AS tns_inq, SUM(ns_reg) AS tns_reg, SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameTy + " WHERE appdate='" + dteTemp.ToString("yyyy-MM-dd") + "'";
 //                           dr = cmd.ExecuteReader();
 //                           if (dr.Read())
 //                           {
 //       intTWInq[intCtr] = clsValidator.CheckInteger(dr["tns_inq"].ToString());
 //       intTWReg[intCtr] = clsValidator.CheckInteger(dr["tns_reg"].ToString());
 //       intTWEnr[intCtr] = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //                           }
 //                           dr.Close();

 //                           cmd.CommandText = "SELECT SUM(ns_enr) AS tns_enr FROM " + clsWIRE.TableNameLy + " WHERE appdate='" + dteTemp.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //                           dr = cmd.ExecuteReader();
 //                           if (dr.Read())
 //       intLYTWEnr[intCtr] = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //                           else
 //                               intLYTWEnr[intCtr] = 0;
 //                           dr.Close();

 //                           intCtr++;
 //                           dteTemp = dteTemp.AddDays(1);
 //                       }
 //                   }

 //                   lblOTInq.Text = intTInq.ToString("###,##0");
 //                   lblOTReg.Text = intTReg.ToString("###,##0");
 //                   lblOTEnr.Text = intTEnr.ToString("###,##0");
 //                   lblOTLy.Text = dblLYEnr.ToString("###,##0");

 //                   lblTyTwInq.Text = intTWeekInq.ToString("###,##0");
 //                   lblTyTwReg.Text = intTWeekReg.ToString("###,##0");
 //                   lblTyTwEnr.Text = intTWeekEnr.ToString("###,##0");
 //                   lblLyTwEnr.Text = intLyTWeekEnr.ToString("###,##0");

 //                   lblTyTwInq1.Text = intTWInq[0].ToString("###,##0");
 //                   lblTyTwReg1.Text = intTWReg[0].ToString("###,##0");
 //                   lblTyTwEnr1.Text = intTWEnr[0].ToString("###,##0");
 //                   lblLyTwEnr1.Text = intLYTWEnr[0].ToString("###,##0");

 //                   lblTyTwInq2.Text = intTWInq[1].ToString("###,##0");
 //                   lblTyTwReg2.Text = intTWReg[1].ToString("###,##0");
 //                   lblTyTwEnr2.Text = intTWEnr[1].ToString("###,##0");
 //                   lblLyTwEnr2.Text = intLYTWEnr[1].ToString("###,##0");

 //                   lblTyTwInq3.Text = intTWInq[2].ToString("###,##0");
 //                   lblTyTwReg3.Text = intTWReg[2].ToString("###,##0");
 //                   lblTyTwEnr3.Text = intTWEnr[2].ToString("###,##0");
 //                   lblLyTwEnr3.Text = intLYTWEnr[2].ToString("###,##0");

 //                   lblTyTwInq4.Text = intTWInq[3].ToString("###,##0");
 //                   lblTyTwReg4.Text = intTWReg[3].ToString("###,##0");
 //                   lblTyTwEnr4.Text = intTWEnr[3].ToString("###,##0");
 //                   lblLyTwEnr4.Text = intLYTWEnr[3].ToString("###,##0");

 //                   lblTyTwInq5.Text = intTWInq[4].ToString("###,##0");
 //                   lblTyTwReg5.Text = intTWReg[4].ToString("###,##0");
 //                   lblTyTwEnr5.Text = intTWEnr[4].ToString("###,##0");
 //                   lblLyTwEnr5.Text = intLYTWEnr[4].ToString("###,##0");

 //                   lblTyTwInq6.Text = intTWInq[5].ToString("###,##0");
 //                   lblTyTwReg6.Text = intTWReg[5].ToString("###,##0");
 //                   lblTyTwEnr6.Text = intTWEnr[5].ToString("###,##0");
 //                   lblLyTwEnr6.Text = intLYTWEnr[5].ToString("###,##0");

 //                   lblTyLwInq.Text = intLWInq.ToString("###,##0");
 //                   lblTyLwReg.Text = intLWReg.ToString("###,##0");
 //                   lblTyLwEnr.Text = intLWEnr.ToString("###,##0");
 //                   lblLyLwEnr.Text = intLyLWEnr.ToString("###,##0");
 //               }

 //               //ColumnChart chartTyEnr = new ColumnChart();
 //               //ColumnChart chartTyReg = new ColumnChart();
 //               //ColumnChart chartLYEnr = new ColumnChart();
 //               StackedColumnChart chartTyEnr = new StackedColumnChart();
 //               StackedColumnChart chartTyReg = new StackedColumnChart();
 //               LineChart chartLYEnr = new LineChart();

 //               chartTyEnr.Shadow.Visible = true;
 //               chartTyEnr.MaxColumnWidth = 50;
 //               chartTyEnr.Fill.Color = Color.FromArgb(90, Color.Blue);
 //               //chartTyEnr.DataLabels.Visible = true;
 //               //chartTyEnr.DataLabels.Background.Color = Color.White;
 //               chartTyEnr.Legend = "This year Enrollees";
 //               chartTyEnr.DataSource = tblTyEnr.DefaultView;
 //               chartTyEnr.DataXValueField = "natldist";
 //               chartTyEnr.DataYValueField = "tns_enr";
 //               chartTyEnr.DataBind();

 //               chartTyReg.Shadow.Visible = true;
 //               chartTyReg.MaxColumnWidth = 15;
 //               chartTyReg.Fill.Color = Color.FromArgb(90, Color.Yellow);
 //               //chartTyReg.DataLabels.Visible = true;
 //   //chartTyReg.DataLabels.Position = DataLabelPosition.Center;
 //               //chartTyReg.DataLabels.Background.Color = Color.White;
 //               chartTyReg.Legend = "This Year Registrants";
 //               chartTyReg.DataSource = tblTyReg.DefaultView;
 //               chartTyReg.DataXValueField = "natldist";
 //               chartTyReg.DataYValueField = "tns_reg";
 //               chartTyReg.DataBind();

 //               //chartLYEnr.MaxColumnWidth = 15;
 //               chartLYEnr.Line.Width = 2;
 //               chartLYEnr.Line.Color = Color.Red;
 //               chartLYEnr.Shadow.Visible = true;
 //               chartLYEnr.Fill.Color = Color.FromArgb(90, Color.Red);
 //               //chartLYEnr.DataLabels.Visible = true;
 //               chartLYEnr.Legend = "Last Year Enrollees";
 //               chartLYEnr.DataSource = tblLyEnr.DefaultView;
 //               chartLYEnr.DataXValueField = "natldist";
 //               chartLYEnr.DataYValueField = "tns_enr";
 //               chartLYEnr.DataBind();

 //               chaNat.Charts.Add(chartTyEnr);
 //               chaNat.Charts.Add(chartTyReg);
 //               //chaNat.Charts.Add(chartTyEnr);
 //               chaNat.Charts.Add(chartLYEnr);
 //               chaNat.RedrawChart();

 //               strWrite = "<tr>" +
 //                                                               "<td class='GridRows'>TY Enr</td>" +
 //               "<td class='GridRows'>" + (tblTyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyEnr.Rows[0]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyEnr.Rows[1]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyEnr.Rows[2]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyEnr.Rows[3]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyEnr.Rows[4]["tns_enr"].ToString())) + "</td>" +
 //              "</tr>" +
 //              "<tr>" +
 //               "<td class='GridRows'>TY Reg</td>" +
 //               "<td class='GridRows'>" + (tblTyReg2.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyReg2.Rows[0]["tns_reg"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyReg2.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyReg2.Rows[1]["tns_reg"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyReg2.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyReg2.Rows[2]["tns_reg"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyReg2.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyReg2.Rows[3]["tns_reg"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblTyReg2.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblTyReg2.Rows[4]["tns_reg"].ToString())) + "</td>" +
 //              "</tr>" +
 //              "<tr>" +
 //               "<td class='GridRows'>LY Enr</td>" +
 //               "<td class='GridRows'>" + (tblLyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblLyEnr.Rows[0]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblLyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblLyEnr.Rows[1]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblLyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblLyEnr.Rows[2]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblLyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblLyEnr.Rows[3]["tns_enr"].ToString())) + "</td>" +
 //               "<td class='GridRows'>" + (tblLyEnr.Rows.Count == 0 ? 0 : clsValidator.CheckInteger(tblLyEnr.Rows[4]["tns_enr"].ToString())) + "</td>" +
 //                                                           "</tr>";
 //               lblChart.Text = strWrite;
 //           }
 //       }
 //   }

}
