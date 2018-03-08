using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;
using STIeForms;
using System.Data.SqlClient;
using System.Configuration;

public partial class CIS_RFI_RFIMain : System.Web.UI.Page
{

 protected void LoadRFI()
 {
  string strWrite = "";
  int intCtr = 0;

  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
  {
   clsMRCF mrcf = new clsMRCF();
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT TOP 10 rficode,intended,datereq,apprcode,apprstat,proccode,procstat,status FROM CIS.RFI WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' ORDER BY datereq DESC";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   while (dr.Read())
   {
    strWrite += "<tr>" +
                 "<td class='GridRows'>" +
                  "<a href='RFIDetails.aspx?rficode=" + dr["rficode"] + "'><img src='../../Support/" + RFI.GetRequestStatusIcon(dr["status"].ToString()) + "' alt='' /></a>" +
                 "</td>" +
                 "<td class='GridRows'>" +
                  "<a href='RFIDetails.aspx?rficode=" + dr["rficode"] + "' style='font-size:small;'>" + dr["intended"] + "</a><br>" +
                  "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd,yyyy") +
                 "</td>" +
                 "<td class='GridRows'>" + RFI.GetRequestStatusRemarks(dr["status"].ToString(), dr["apprcode"].ToString(), dr["apprstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "</td>" +
                "</tr>";
    intCtr++;
   }
   dr.Close();
  }

  Response.Write(strWrite);
  if (intCtr == 0)
   Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
  else
   Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
 }

 ///////////////////////////////
 ///////// Page Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {

 }

 protected void btnNewRequest_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("RFINew.aspx");
 }

}