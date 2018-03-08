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
using System.Data.SqlClient;

public partial class CMD_WIRE_rptWIRESummary : System.Web.UI.Page
{

 //protected void Load_NS()
 //{
 // string strWrite = "";
 // int intINQ = 0;
 // int intREG = 0;
 // int intENR = 0;

 // int intI = 0;
 // int intR = 0;
 // int intE = 0;
 // double dblC = 0;

 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT ((SUM(ns_enr) / SUM(ns_inq)) * 100) AS ConvPer, SUM(ns_inq) AS TNS_INQ, SUM(NS_REG) AS TNS_REG, SUM(NS_ENR) AS TNS_ENR, progname FROM " + clsWIRE.TableNameTy + " INNER JOIN programs ON " + clsWIRE.TableNameTy + ".progcode = programs.progcode WHERE schlcode='" + ddlSchools.SelectedValue + "' GROUP BY progname ORDER BY progname";
 //  cn.Open();
 //  MySqlDataReader dr = cmd.ExecuteReader();
 //  while (dr.Read())
 //  {
 //   intI = clsValidator.CheckInteger(dr["TNS_INQ"].ToString());
 //   intR = clsValidator.CheckInteger(dr["TNS_REG"].ToString());
 //   intE = clsValidator.CheckInteger(dr["TNS_ENR"].ToString());
 //   dblC = clsValidator.CheckDouble(dr["ConvPer"].ToString());
 //   strWrite += "<tr>" +
 //                "<td class='GridRows'>" + dr["progname"] + "</td>" +
 //                "<td class='GridRows' align='right'>" + (intI == 0 ? "-" : intI.ToString("##,###")) + "</td>" +
 //                "<td class='GridRows' align='right'>" + (intR == 0 ? "-" : intR.ToString("##,###")) + "</td>" +
 //                "<td class='GridRows' align='right'>" + (intE == 0 ? "-" : intE.ToString("##,###")) + "</td>" +
 //                "<td class='GridRows' align='right'>" + (dblC == 0 ? "0 %" : dblC.ToString("##,###") + " %") + "</td>" +
 //               "</tr>";
 //   intINQ += intI;
 //   intREG += intR;
 //   intENR += intE;
 //  }
 //  dr.Close();
 // }

 // double dblTotal = 0;
 // dblTotal = (Convert.ToDouble(intENR) / Convert.ToDouble(intINQ)) * 100;

 // strWrite += "<tr>" +
 //              "<td class='GridColumns'><b>Total</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + intINQ + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + intREG + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + intENR + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + Math.Round(dblTotal,2) + " %</b></td>" +
 //             "</tr>";
 // Response.Write(strWrite);
 //}

 //protected void Load_OS()
 //{
 // string strWrite = "";
 // int intREG = 0;
 // int intENR = 0;
 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT SUM(fir_reg) AS tfir_reg, SUM(fir_enr) AS tfir_enr, SUM(sec_reg) AS tsec_reg, SUM(sec_enr) AS tsec_enr, SUM(thi_reg) AS tthi_reg, SUM(thi_enr) AS tthi_enr, SUM(fou_reg) AS tfou_reg, SUM(fou_enr) AS tfou_enr, SUM(fif_reg) AS tfif_reg, SUM(fif_enr) AS TFIF_ENR, progname FROM " + clsWIRE.TableNameTy + " INNER JOIN programs ON " + clsWIRE.TableNameTy + ".progcode = programs.progcode WHERE schlcode='" + ddlSchools.SelectedValue + "' GROUP BY progname ORDER BY progname";
 //  cn.Open();
 //  MySqlDataReader dr = cmd.ExecuteReader();
 //  while (dr.Read())
 //  {
 //   strWrite += "<tr>" +
 //                 "<td align='left' class='GridRows' colspan='4'><b>" + dr["progname"] + "</b></td>" +
 //                "</tr>" +
 //                "<tr>" +
 //                 "<td class='GridRows' align='left'>1st Year</td>" +
 //                 "<td class='GridRows' align='right'>NA</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TFIR_REG"].ToString())) + "</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TFIR_ENR"].ToString())) + "</td>" +
 //                "</tr>" +
 //                "<tr>" +
 //                 "<td class='GridRows' align='left'>2nd Year</td>" +
 //                 "<td class='GridRows' align='right'>NA</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TSEC_REG"].ToString())) + "</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TSEC_ENR"].ToString())) + "</td>" +
 //                "</tr>" +
 //                "<tr>" +
 //                 "<td class='GridRows' align='left'>3rd Year</td>" +
 //                 "<td class='GridRows' align='right'>NA</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TTHI_REG"].ToString())) + "</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TTHI_ENR"].ToString())) + "</td>" +
 //                "</tr>" +
 //                "<tr>" +
 //                 "<td class='GridRows' align='left'>4th Year</td>" +
 //                 "<td class='GridRows' align='right'>NA</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TFOU_REG"].ToString())) + "</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TFOU_ENR"].ToString())) + "</td>" +
 //                "</tr>" +
 //                "<tr>" +
 //                 "<td class='GridRows' align='left'>5th Year</td>" +
 //                 "<td class='GridRows' align='right'>NA</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TFIF_REG"].ToString())) + "</td>" +
 //                 "<td class='GridRows' align='right'>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(dr["TFIF_ENR"].ToString())) + "</td>" +
 //                "</tr>";
 //   intREG += clsValidator.CheckInteger(dr["tfir_reg"].ToString()) + clsValidator.CheckInteger(dr["tsec_reg"].ToString()) + clsValidator.CheckInteger(dr["tthi_reg"].ToString()) + clsValidator.CheckInteger(dr["tfou_reg"].ToString()) + clsValidator.CheckInteger(dr["tfif_reg"].ToString());
 //   intENR += clsValidator.CheckInteger(dr["tfir_enr"].ToString()) + clsValidator.CheckInteger(dr["tsec_enr"].ToString()) + clsValidator.CheckInteger(dr["tthi_enr"].ToString()) + clsValidator.CheckInteger(dr["tfou_enr"].ToString()) + clsValidator.CheckInteger(dr["tfif_enr"].ToString());
 //  }
 //  dr.Close();
 // }
 // strWrite += "<tr>" +
 //              "<td class='GridColumns'><b>Total</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>NA</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + intREG + "</b></td>" +
 //              "<td class='GridColumns' style='text-align:right;'><b>" + intENR + "</b></td>" +
 //             "</tr>";
 // Response.Write(strWrite);
 //}

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   clsWIRE.AuthenticateUser(Request.Cookies["Speedo"]["UserName"], clsWIRE.WireUsers.EliteUsers);
   DataTable tblPrograms = new DataTable();
   using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT schlcode,schlnam2 FROM CM.Schools WHERE pstatus='1' ORDER BY schlnam2";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblPrograms);
   }

   ddlSchools.DataValueField = "schlcode";
   ddlSchools.DataTextField = "schlnam2";
   ddlSchools.DataSource = tblPrograms;
   ddlSchools.DataBind();
  }
 }

}
