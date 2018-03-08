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

public partial class CMD_WIRE_rptTopNS : System.Web.UI.Page
{

 //protected void Page_Load(object sender, EventArgs e)
 //{
 // if (!Page.IsPostBack)
 // {
 //  clsWIRE.AuthenticateUser(Request.Cookies["Speedo"]["UserName"], clsWIRE.WireUsers.EliteUsers);

 //  ddlSchlCat.SelectedValue = Request.QueryString["schltype"];
 //  ddlOwning.SelectedValue = Request.QueryString["schlown"];

 //  lblTableHead.Text = (Request.QueryString["schltype"] == "A" ? "(College and EC" : (Request.QueryString["schltype"] == "C" ? "(College Schools" : "(EC Schools")) + " / " + (Request.QueryString["schlown"] == "A" ? " HQ Owned and Non-HQ Owned)" : (Request.QueryString["schlown"] == "H" ? " HQ Owned)" : " Non-HQ Owned)"));

 //  string strLastUpdate = "";
 //  string strRowClass = "";
 //  DateTime dteAppDate = DateTime.Now;

 //  int intTyReg = 0;
 //  int intTyEnr = 0;
 //  int intLyEnr = 0;

 //  DataTable tblTY = new DataTable();
 //  int intCtr = 1;

 //  int intRed = 0;
 //  int intBlue = 0;
 //  int intGreen = 0;

 //  using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 //  {
 //   MySqlCommand cmd = cn.CreateCommand();
 //   MySqlDataReader dr;

 //   string strOrder = "";
 //   string strWhere = "";
 //   int intTyTNsEnr = 0;
 //   int intLyTNsEnr = 0;

 //   switch (Request.QueryString["sort"])
 //   {
 //    case "name":
 //     strOrder = " ORDER BY schlname";
 //     break;
 //    case "lastup":
 //     strOrder = " ORDER BY lastnsup DESC";
 //     break;
 //    case "reg":
 //     strOrder = " ORDER BY tns_reg DESC, schlname";
 //     break;
 //    case "tyenr":
 //     strOrder = " ORDER BY tns_enr DESC, tns_reg DESC, schlname";
 //     break;
 //   }

 //   if (Request.QueryString["schltype"] == "A")
 //   {
 //    if (Request.QueryString["schlown"] == "A")
 //     strWhere = "";
 //    else if (Request.QueryString["schlown"] == "H")
 //     strWhere = " AND hqowned='1'";
 //    else if (Request.QueryString["schlown"] == "N")
 //     strWhere = " AND hqowned='0'";
 //   }
 //   else if (Request.QueryString["schltype"] == "C")
 //   {
 //    if (Request.QueryString["schlown"] == "A")
 //     strWhere = " AND schools.schltype='C'";
 //    else if (Request.QueryString["schlown"] == "H")
 //     strWhere = " AND schools.schltype='C' AND hqowned='1'";
 //    else if (Request.QueryString["schlown"] == "N")
 //     strWhere = " AND schools.schltype='C' AND hqowned='0'";
 //   }
 //   else if (Request.QueryString["schltype"] == "E")
 //   {
 //    if (Request.QueryString["schlown"] == "A")
 //     strWhere = " AND schools.schltype='E'";
 //    else if (Request.QueryString["schlown"] == "H")
 //     strWhere = " AND schools.schltype='E' AND hqowned='1'";
 //    else if (Request.QueryString["schlown"] == "N")
 //     strWhere = " AND schools.schltype='E' AND hqowned='0'";
 //   }

 //   cmd.CommandText = "SELECT SUM(ns_enr) AS tns_enr,SUM(ns_reg) AS tns_reg,schlname,schools.schlcode,schools.hqowned FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode WHERE schools.active='1'" + strWhere + " GROUP BY schlname,schlcode" + strOrder;
 //   MySqlDataAdapter da = new MySqlDataAdapter(cmd);
 //   cn.Open();
 //   da.Fill(tblTY);

 //   foreach (DataRow drow in tblTY.Rows)
 //   {
 //    intTyTNsEnr = clsValidator.CheckInteger(drow["tns_enr"].ToString());

 //    cmd.CommandText = "SELECT appdate FROM " + clsWIRE.TableNameEncodeDates + " WHERE schlcode='" + drow["schlcode"] + "' AND nsencode='1' ORDER BY appdate DESC LIMIT 1";
 //    dr = cmd.ExecuteReader();
 //    if (dr.Read())
 //    {
 //     dteAppDate = Convert.ToDateTime(dr["appdate"].ToString());
 //     strLastUpdate = dteAppDate.ToString("MMMM dd, yyyy");
 //    }
 //    else
 //    {
 //     dteAppDate = Convert.ToDateTime(clsWIRE.StartDate);
 //     strLastUpdate = "No Update History";
 //    }
 //    dr.Close();

 //    cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR FROM " + clsWIRE.TableNameLy + " WHERE schlcode='" + drow["schlcode"] + "' AND appdate <= '" + dteAppDate.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //    dr = cmd.ExecuteReader();
 //    dr.Read();
 //    intLyTNsEnr = clsValidator.CheckInteger(dr["tns_enr"].ToString());

 //    if (intTyTNsEnr >= intLyTNsEnr)
 //    {
 //     strRowClass = "#efffdd";
 //     intGreen += 1;
 //    }
 //    else if (intTyTNsEnr >= intLyTNsEnr)
 //    {
 //     strRowClass = "#f0f8ff";
 //     intBlue += 1;
 //    }
 //    else
 //    {
 //     strRowClass = "#ffe4e1";
 //     intRed += 1;
 //    }

 //    intTyEnr += clsValidator.CheckInteger(drow["tns_enr"].ToString());
 //    intTyReg += clsValidator.CheckInteger(drow["tns_reg"].ToString());
 //    intLyEnr += intLyTNsEnr;

 //    HtmlTableRow trow = new HtmlTableRow();
 //    trow.BgColor = strRowClass;

 //    HtmlTableCell tcell1 = new HtmlTableCell();
 //    tcell1.Align = "center";
 //    tcell1.InnerText = "#" + intCtr;
 //    trow.Cells.Add(tcell1);

 //    HtmlTableCell tcell2 = new HtmlTableCell();
 //    tcell2.Align = "left";
 //    tcell2.InnerHtml = "<table cellspacing='0' cellpadding='0'>" +
 //                        "<tr>" +
 //                        "<td><img src='../../Support/" + (drow["hqowned"].ToString() == "1" ? "bookmark16" : "star16") + ".png' ></td>" +
 //                         "<td>&nbsp;&nbsp;<a href='../SIS/SchoolsDirectoryDetails.aspx?schlcode=" + drow["schlcode"] + "&schlname=" + drow["schlname"] + "'>" + drow["schlname"] + "</a></td>" +
 //                        "</tr>" +
 //                       "</table>";
 //    trow.Cells.Add(tcell2);

 //    HtmlTableCell tcell3 = new HtmlTableCell();
 //    tcell3.Align = "left";
 //    tcell3.InnerText = strLastUpdate;
 //    trow.Cells.Add(tcell3);

 //    HtmlTableCell tcell4 = new HtmlTableCell();
 //    tcell4.Align = "right";
 //    tcell4.InnerText = clsValidator.ZeroToDash(clsValidator.CheckInteger(drow["tns_reg"].ToString()));
 //    trow.Cells.Add(tcell4);

 //    HtmlTableCell tcell5 = new HtmlTableCell();
 //    tcell5.Align = "right";
 //    tcell5.InnerText = clsValidator.ZeroToDash(clsValidator.CheckInteger(drow["tns_enr"].ToString()));
 //    trow.Cells.Add(tcell5);

 //    HtmlTableCell tcell6 = new HtmlTableCell();
 //    tcell6.Align = "right";
 //    tcell6.InnerText = clsValidator.ZeroToDash(intLyTNsEnr);
 //    trow.Cells.Add(tcell6);

 //    HtmlTableCell tcell7 = new HtmlTableCell();
 //    tcell7.Align = "right";
 //    tcell7.InnerText = (intLyTNsEnr == 0 ? "-" : ((-intLyTNsEnr + intTyTNsEnr) / intLyTNsEnr) * 100 + "%");
 //    trow.Cells.Add(tcell7);

 //    tblReports.Rows.Add(trow);

 //    dr.Close();
 //    intCtr++;
 //   }

 //   HtmlTableRow trowT = new HtmlTableRow();
 //   trowT.BgColor = strRowClass;
 //   trowT.Style.Add("font-weight", "bold");

 //   HtmlTableCell tcell1T = new HtmlTableCell();
 //   tcell1T.InnerText = "";
 //   trowT.Cells.Add(tcell1T);

 //   HtmlTableCell tcell2T = new HtmlTableCell();
 //   tcell2T.Align = "left";
 //   tcell2T.InnerText = "Total";
 //   trowT.Cells.Add(tcell2T);

 //   HtmlTableCell tcell3T = new HtmlTableCell();
 //   tcell3T.InnerText = "";
 //   trowT.Cells.Add(tcell3T);

 //   HtmlTableCell tcell4T = new HtmlTableCell();
 //   tcell4T.Align = "right";
 //   tcell4T.InnerText = intTyReg.ToString();
 //   trowT.Cells.Add(tcell4T);

 //   HtmlTableCell tcell5T = new HtmlTableCell();
 //   tcell5T.Align = "right";
 //   tcell5T.InnerText = intTyEnr.ToString();
 //   trowT.Cells.Add(tcell5T);

 //   HtmlTableCell tcell6T = new HtmlTableCell();
 //   tcell6T.Align = "right";
 //   tcell6T.InnerText = intLyEnr.ToString();
 //   trowT.Cells.Add(tcell6T);

 //   HtmlTableCell tcell7T = new HtmlTableCell();
 //   tcell7T.Align = "right";
 //   tcell7T.InnerText = (intLyEnr == 0 ? "-" : ((-intLyEnr + intTyEnr) / intLyEnr) * 100 + "%");
 //   trowT.Cells.Add(tcell7T);

 //   tblReports.Rows.Add(trowT);

 //   lblGreen.Text = Math.Round(Convert.ToDouble(((intGreen / (intCtr - 1)) * 100)), 2) + "%";
 //   lblBlue.Text = Math.Round(Convert.ToDouble(((intBlue / (intCtr - 1)) * 100)), 2) + "%";
 //   lblRed.Text = Math.Round(Convert.ToDouble(((intRed / (intCtr - 1)) * 100)), 2) + "%";
 //  }
 // }
 //}

 protected void btnSubmit_Click(object sender, EventArgs e)
 {
  Response.Redirect("rptTopNS.aspx?schltype=" + ddlSchlCat.SelectedValue + "&schlown=" + ddlOwning.SelectedValue + "&sort=" + Request.QueryString["sort"]);
 }
}
