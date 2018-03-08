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

public partial class CMD_WIRE_SubRate : System.Web.UI.Page
{

 //protected void Load_Submission()
 //{
 // string strWrite = "";
 // DataTable tblCM = new DataTable();

 // using (MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Wire"].ToString()))
 // {
 //  cn.Open();
 //  MySqlCommand cmd = cn.CreateCommand();
 //  cmd.CommandText = "SELECT DISTINCT cmuname FROM schools ORDER BY cmuname";
 //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
 //  da.Fill(tblCM);

 //  MySqlDataReader dr;

 //  foreach (DataRow drow in tblCM.Rows)
 //  {
 //   strWrite += "<tr>" +
 //                "<td class='GridText'>" + drow["cmuname"] + "</td>" +
 //                "<td colspan='2' class='GridText'>&nbsp;</td>" +
 //               "</tr>";
 //   cmd.CommandText = "SELECT schools.schlname,schools.cmuname,MAX(appdate) AS MaxDate FROM schools INNER JOIN " + clsWIRE.TableNameEncodeDates + " ON schools.schlcode = " + clsWIRE.TableNameEncodeDates + ".schlcode WHERE nsencode='1' AND schools.active='1' AND cmuname='" + drow["cmuname"] + "' GROUP BY schools.schlname,schools.cmuname UNION SELECT schools.schlname,schools.cmuname,appdate AS MaxDate FROM schools INNER JOIN " + clsWIRE.TableNameEncodeDates + " ON schools.schlcode = " + clsWIRE.TableNameEncodeDates + ".schlcode WHERE nsencode='0' AND appdate='" + clsWIRE.StartDate + "' AND schools.active='1' AND cmuname='" + drow["cmuname"] + "' GROUP BY schools.schlname,schools.cmuname ORDER BY maxdate,schlname";
 //   dr = cmd.ExecuteReader();
 //   while (dr.Read())
 //   {
 //    if (Convert.ToDateTime(dr["MaxDate"].ToString()) <= DateTime.Now.AddDays(-2))
 //    {
 //     strWrite += "<tr>" +
 //                  "<td class='GridRowsRed' style='text-align:left'>&nbsp;</td>" +
 //                  "<td class='GridRowsRed' style='text-align:left'>" + dr["schlname"] + "</td>" +
 //                  "<td class='GridRowsRed' style='text-align:left'>" + Convert.ToDateTime(dr["maxdate"].ToString()).ToString("MMMM dd, yyyy") + "</td>" +
 //                 "</tr>";
 //    }
 //    else
 //    {
 //     strWrite += "<tr>" +
 //                  "<td class='GridRows' style='text-align:left'>&nbsp;</td>" +
 //                  "<td class='GridRows' style='text-align:left'>" + dr["schlname"] + "</td>" +
 //                  "<td class='GridRows' style='text-align:left'>" + (Convert.ToDateTime(dr["maxdate"].ToString()).ToString("MMMM dd, yyyy") == DateTime.Now.ToString("MMMM dd, yyyy") ? "Today" : Convert.ToDateTime(dr["maxdate"].ToString()).ToString("MMMM dd, yyyy")) + "</td>" +
 //                 "</tr>";
 //    }
 //   }
 //   dr.Close();
 //  }

 // }
 // Response.Write(strWrite);
 //}

 protected void Page_Load(object sender, EventArgs e)
 {

 }

}
