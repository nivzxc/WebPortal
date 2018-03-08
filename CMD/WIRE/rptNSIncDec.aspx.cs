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

public partial class CMD_WIRE_rptNSIncDec : System.Web.UI.Page
{

 //protected void LoadNSInqDec()
 //{
 // DateTime dteAppDate = DateTime.Now;

 // DataTable tblTY = new DataTable();
 // int intCtr = 1;
 // string strWrite = "";

 // // Define the Schema
 // DataTable tblGrowth = new DataTable();
 // DataColumn pcol;
 // DataRow prow;
 // DataView pview;

 // pcol = new DataColumn("schlcode",System.Type.GetType("System.String"));
 // tblGrowth.Columns.Add(pcol);

 // pcol = new DataColumn("schlname",System.Type.GetType("System.String"));
 // tblGrowth.Columns.Add(pcol);

 // pcol = new DataColumn("appdate",System.Type.GetType("System.DateTime"));
 // tblGrowth.Columns.Add(pcol);

 // pcol = new DataColumn("ty",System.Type.GetType("System.Int32"));
 // tblGrowth.Columns.Add(pcol);

 // pcol = new DataColumn("ly",System.Type.GetType("System.Int32"));
 // tblGrowth.Columns.Add(pcol);

 // pcol = new DataColumn("growth",System.Type.GetType("System.Double"));
 // tblGrowth.Columns.Add(pcol);

 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  MySqlCommand cmd = cn.CreateCommand();
 // if (Request.QueryString["schltype"] == "A")
 //  cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR,schlname,schools.schlcode FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode GROUP BY schlname,schlcode ORDER BY tns_enr desc,schlname";
 // else if (Request.QueryString["schltype"] == "C")
 //  cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR,schlname,schools.schlcode FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode WHERE schools.schltype='C' GROUP BY schlname,schlcode ORDER BY tns_enr desc,schlname";
 // else if (Request.QueryString["schltype"] == "E")
 //  cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR,schlname,schools.schlcode FROM " + clsWIRE.TableNameTy + " INNER JOIN schools ON " + clsWIRE.TableNameTy + ".schlcode = schools.schlcode WHERE schools.schltype='E' GROUP BY schlname,schlcode ORDER BY tns_enr desc,schlname";

 // MySqlDataReader dr;
 // MySqlDataAdapter da = new MySqlDataAdapter(cmd);
 // cn.Open();
 // da.Fill(tblTY);
 // foreach(DataRow drow in tblTY.Rows)
 // {
 //  cmd.CommandText = "SELECT appdate FROM " + clsWIRE.TableNameEncodeDates + " WHERE schlcode='" + drow["schlcode"] + "' AND nsencode='1' ORDER BY appdate DESC LIMIT 1";
 //  dr = cmd.ExecuteReader();
 //  if (dr.Read())
 //   dteAppDate = Convert.ToDateTime(dr["appdate"].ToString());
 //  else
 //   dteAppDate = Convert.ToDateTime(clsWIRE.StartDate);
 //  dr.Close();

 //  cmd.CommandText = "SELECT SUM(NS_ENR) AS TNS_ENR FROM " + clsWIRE.TableNameLy + " WHERE schlcode='" + drow["schlcode"] + "' AND appdate <= '" + dteAppDate.AddYears(-1).AddDays(clsWIRE.DayAdjustment).ToString("yyyy-MM-dd") + "'";
 //  dr = cmd.ExecuteReader();
 //  dr.Read();
 //  prow = tblGrowth.NewRow();
 //  prow["schlcode"] = drow["schlcode"].ToString();
 //  prow["schlname"] = drow["schlname"].ToString();
 //  prow["appdate"] = dteAppDate;
 //  prow["ty"] = clsValidator.CheckInteger(drow["tns_enr"].ToString());
 //  prow["ly"] = clsValidator.CheckInteger(dr["tns_enr"].ToString());
 //  if (clsValidator.CheckInteger(dr["tns_enr"].ToString()) == 0)
 //   prow["growth"] = "0";
 //  else
 //   prow["growth"] = ((-clsValidator.CheckDouble(dr["tns_enr"].ToString()) + clsValidator.CheckDouble(drow["tns_enr"].ToString())) / clsValidator.CheckDouble(dr["tns_enr"].ToString())) * 100;
 //  tblGrowth.Rows.Add(prow);
 //  dr.Close();
 // }
 // }

 // pview = new DataView(tblGrowth);
 // pview.Sort = "growth DESC";

 // foreach(DataRowView drowview in pview)
 // {
 //  strWrite += "<tr>" + 
 //               "<td class='GridRows' style='text-align:center'>#" + intCtr + "</td>" + 
 //               "<td class='GridRows'>" + drowview["schlname"] + "</td>" + 
 //               "<td class='GridRows' style='text-align:right'>" + drowview["ty"] + "</td>" + 
 //               "<td class='GridRows' style='text-align:right'>" + drowview["ly"] + "</td>" + 
 //               "<td class='GridRows' style='text-align:right'>" + drowview["growth"] + "%</td>" + 
 //              "</tr>";
 //  intCtr++;
 // }
 // Response.Write(strWrite);
 //}

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
   clsWIRE.AuthenticateUser(Request.Cookies["Speedo"]["UserName"], clsWIRE.WireUsers.EliteUsers);
 }

}
