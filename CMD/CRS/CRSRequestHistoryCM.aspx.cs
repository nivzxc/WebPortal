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
using System.Data.SqlClient;
using System.Text;

public partial class CMD_CRS_CRSRequestHistoryCM : System.Web.UI.Page
{

   protected void LoadRecords()
   {
      string strWrite = "";
      int intCtr = 0;
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT dispdeta,trancode,disptype FROM CM.CrsDetailsDispatch WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "' ORDER BY datentry DESC";
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         while (dr.Read())
         {
            strWrite = "<tr>" +
                                                                        "<td class='GridRows'>" +
                                                                             dr["dispdeta"] + "<br>" +
                                                                             "Transmittal Code: " + dr["trancode"] +
                                                                        "</td>" +
                                                                        "<td class='GridRows' style='text-align:center;'>" +
                                                                             clsCRS.ToDispatchTypeDesc(dr["disptype"].ToString()) +
                                                                        "</td>" +
                                                                   "</tr>";
            Response.Write(strWrite);
            intCtr++;
         }
         dr.Close();
      }
      if (intCtr == 0)
         Response.Write("<tr><td colspan='2' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='2' class='GridRows'>[" + intCtr + " record(s) found]</td></tr>");
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
   }

   protected void btnClose_Click(object sender, ImageClickEventArgs e)
   {
      StringBuilder sb = new StringBuilder();
      sb.Append("window.close();");
      ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindowScript", sb.ToString(), true);
   }

}
