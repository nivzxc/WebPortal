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

public partial class CMD_WIRE_SchlSubmit : System.Web.UI.Page
{

 //protected void Load_Submission()
 //{
 // string strWrite = "";
 // int intCtr = 0;
 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT schools.schlname,schools.cmuname,nsencode,MAX(appdate) AS MaxDate FROM schools INNER JOIN " + clsWIRE.TableNameEncodeDates + " ON schools.schlcode = " + clsWIRE.TableNameEncodeDates + ".schlcode WHERE nsencode='1' AND schools.active='1' GROUP BY schools.schlname,schools.cmuname UNION SELECT schools.schlname,schools.cmuname,nsencode,appdate AS MaxDate FROM schools INNER JOIN " + clsWIRE.TableNameEncodeDates + " ON schools.schlcode = " + clsWIRE.TableNameEncodeDates + ".schlcode WHERE nsencode='0' AND appdate='" + clsWIRE.StartDate + "' AND schools.active='1' GROUP BY schools.schlname,schools.cmuname ORDER BY maxdate,schlname";
 //  cn.Open();
 //  MySqlDataReader dr = cmd.ExecuteReader();

 //  while (dr.Read())
 //  {
 //   intCtr++;
 //   if (Request.QueryString["sub"] == "1")
 //   {
 //    if (Convert.ToDateTime(dr["MaxDate"].ToString()) <= DateTime.Now.AddDays(-1))
 //    {
 //     strWrite += "<tr>" +
 //                  "<td class='GridRows'>" + dr["schlname"] + "</td>" +
 //                  "<td class='GridRows'>" + dr["cmuname"] + "</td>" +
 //                  "<td class='GridRows'>" + (dr["nsencode"].ToString() == "1" ? Convert.ToDateTime(dr["maxdate"].ToString()).ToString("MMMM dd, yyyy") : "No Update History") + "</td>" +
 //                 "</tr>";
 //    }
 //   }
 //   else if (Request.QueryString["sub"] == "2")
 //   {
 //    if (Convert.ToDateTime(dr["MaxDate"].ToString()) <= DateTime.Now.AddDays(-2))
 //    {
 //     strWrite += "<tr>" +
 //                  "<td class='GridRows'>" + dr["schlname"] + "</td>" +
 //                  "<td class='GridRows'>" + dr["cmuname"] + "</td>" +
 //                  "<td class='GridRows'>" + Convert.ToDateTime(dr["maxdate"].ToString()).ToString("MMMM dd, yyyy") + "</td>" +
 //                 "</tr>";
 //    }
 //   }
 //   else if (Request.QueryString["sub"] == "3")
 //   {
 //    if (Convert.ToDateTime(dr["MaxDate"].ToString()) < DateTime.Now.AddDays(-2))
 //    {
 //     strWrite += "<tr>" +
 //                  "<td class='GridRows'>" + dr["schlname"] + "</td>" +
 //                  "<td class='GridRows'>" + dr["cmuname"] + "</td>" +
 //                  "<td class='GridRows'>" + Convert.ToDateTime(dr["maxdate"].ToString()).ToString("MMMM dd, yyyy") + "</td>" +
 //                 "</tr>";
 //    }
 //   }
 //  }
 //  dr.Close();
 // }
 // //strWrite += "<tr><td class='GridColumns' colspan='3' style='text-align:left;'><b>Total Records " + intCtr + "</b></td></tr>";
 // Response.Write(strWrite);
 //}

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   if (Request.QueryString["sub"] == "1")
    lblDetails.Text = "There are <b>" + Request.QueryString["percent"] + "%</b> schools who update their WIRE data today (" + DateTime.Now.ToString("MMMM dd, yyyy") + ").<br><br>There are <b>" + Math.Round(100 - Convert.ToDouble(Request.QueryString["percent"]), 2) + "%</b> schools who did not update their WIRE data today.";
   else if (Request.QueryString["sub"] == "2")
    lblDetails.Text = "There are <b>" + Request.QueryString["percent"] + "%</b> schools who update their WIRE data yesterday (" + DateTime.Now.AddDays(-1).ToString("MMMM dd, yyyy") + ").<br><br>There are <b>" + Math.Round(100 - Convert.ToDouble(Request.QueryString["percent"]), 2) + "%</b> schools who did not update their WIRE data yesterday.";
   else if (Request.QueryString["sub"] == "3")
    lblDetails.Text = "There are <b>" + Request.QueryString["percent"] + "%</b> schools who update their WIRE data for the last 3 days (" + DateTime.Now.AddDays(-2).ToString("MMMM dd, yyyy") + ").<br><br>There are <b>" + Math.Round(100 - Convert.ToDouble(Request.QueryString["percent"]), 2) + "%</b> schools who did not update their WIRE data for the last 3 days.";
  }
 }

}
