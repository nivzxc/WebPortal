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

public partial class CMD_CRS_CRSDispatchDetailsCM : System.Web.UI.Page
{

   protected void LoadDispatch()
   {
      string strWrite = "";
      int intCtr = 0;
      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT dispdeta,disptype,CAST(DATEPART(mm,datedisp) AS VARCHAR) + '/' + CAST(DATEPART(dd,datedisp) AS VARCHAR) + '/' + CAST(DATEPART(yy,datedisp) AS VARCHAR) AS datedisp,CAST(DATEPART(mm,recdate) AS VARCHAR) + '/' + CAST(DATEPART(dd,recdate) AS VARCHAR) + '/' + CAST(DATEPART(yy,recdate) AS VARCHAR) AS recdate,recby FROM CM.CrsDetailsDispatch WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "' ORDER BY datentry DESC";
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         while (dr.Read())
         {
            strWrite += "<tr>" +
                                                                          "<td class='GridRows'>" +
                                                                                   dr["dispdeta"] + "<br>" +
                                                                                       clsCRS.ToDispatchTypeDesc(dr["disptype"].ToString()) + "<br>" +
                                                                                       "Date Dispatch: " + dr["datedisp"].ToString() +
                                                                                  "</td>" +
                                                                                  "<td class='GridRows'>" +
                                                                                   "<table>" +
                                                                                        "<tr>" +
                                                                                             "<td style='color:#4169e1'>Received by:</td>" +
                                                                                                 "<td>" + dr["recby"] + "</td>" +
                                                                                            "</tr>" +
                                                                                            "<tr>" +
                                                                                                 "<td style='color:#4169e1'>Received by:</td>" +
                                                                                                 "<td>" + dr["recdate"] + "</td>" +
                                                                                            "</tr>" +
                                                                                       "</table>" +
                                                                                  "</td>" +
                                                                             "</tr>";
            intCtr++;
         }
         dr.Close();
      }
      Response.Write(strWrite);

      if (intCtr == 0)
         Response.Write("<tr><td colspan='2' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='2' class='GridRows'>[" + intCtr + " record(s) found]</td></tr>");
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         txtCrseCode.Text = Request.QueryString["crsecode"].ToString();
         using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT crsettle,yearterm,currcode,datecomp,ordernum,pstatus FROM CM.CrsDetails WHERE crscode='" + Request.QueryString["crscode"] + "' AND crsecode='" + Request.QueryString["crsecode"] + "'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            txtCrseTtle.Text = dr["crsettle"].ToString();
            txtYearTerm.Text = dr["yearterm"].ToString();
            txtAvailability.Text = clsCRS.ToCAStatusDesc(clsCRS.GetCAStatus(txtCrseCode.Text));
            txtNoReq.Text = dr["ordernum"].ToString();
            txtStatus.Text = clsCRS.ToCrsDetailsStatusDesc(dr["pstatus"].ToString());
            dr.Close();
         }
      }
   }

   protected void btnClose_Click(object sender, ImageClickEventArgs e)
   {
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      sb.Append("window.close();");
      ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindowScript", sb.ToString(), true);
   }

}
