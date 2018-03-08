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
using STIeForms;
using Oracles;

public partial class CIS_MRCF_MRCFMenu : System.Web.UI.Page
{



 protected void LoadMenuPM()
 {
  string strWrite = "";
  int intCtr = 0;
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT TOP 10 mrcfcode,sprvcode,username,intended,datereq,status FROM CIS.Mrcf WHERE proccode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND ((sprvstat='A' AND headstat='N') OR (headstat='A')) AND procstat='F' ORDER BY datereq DESC";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   while (dr.Read())
   {
    strWrite = strWrite + "<tr>" +
                           "<td class='GridRows'>" +
                            "<a href='MRCFDetailsPM.aspx?mrcfcode=" + dr["mrcfcode"] + "'><img src='../../Support/approval.png' alt='You need to process this request' /></a>" +
                           "</td>" +
                           "<td class='GridRows'>" +
                            "<a href='MRCFDetailsPM.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["intended"] + "</a>" +
                            "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a>" +
                            "<br>Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") +
                           "</td>" +
                           "<td class='GridRows'>For your approval</td>" +
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

 protected void LoadMenuDH()
 {
  string strWrite = "";
  int intCtr = 0;
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT TOP 10 mrcfcode,reqtype,username,intended,datereq,status FROM CIS.Mrcf WHERE headcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND headstat='F' AND sprvstat IN ('A','X','N') ORDER BY datereq DESC";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   while (dr.Read())
   {
    strWrite = strWrite + "<tr>" +
                              "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetailsDH.aspx?mrcfcode=" + dr["mrcfcode"] + "'><img src='../../Support/approval.png' alt='' /></a>" +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetailsDH.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["intended"] + "</a><br>" +
                                                                                                    "Request Type: " + clsMRCF.GetRequestTypeDesc(dr["reqtype"].ToString()) + "<br>" +
                                                                                                    "Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a><br>" +
                                                                                                    "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>For your approval</td>" +
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

 protected void LoadMenuGH()
 {
  string strWrite = "";
  int intCtr = 0;
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT TOP 10 mrcfcode,username,intended,datereq,status FROM CIS.Mrcf WHERE sprvcode='" + Request.Cookies["Speedo"]["UserName"] + "' AND status='F' AND sprvstat='F' ORDER BY datereq DESC";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   while (dr.Read())
   {
    strWrite = strWrite + "<tr>" +
                              "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetailsGH.aspx?mrcfcode=" + dr["mrcfcode"] + "'><img src='../../Support/approval.png' alt='' /></a>" +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetailsGH.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["intended"] + "</a>" +
                                                                                                    "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a>" +
                                                                                                    "<br>Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd, yyyy") +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>For your approval</td>" +
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

 protected void LoadMRCF()
 {
  string strWrite = "";
  int intCtr = 0;

  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
  {
   clsMRCF mrcf = new clsMRCF();
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT TOP 10 mrcfcode,intended,datereq,reqtype,sprvcode,sprvstat,headcode,headstat,proccode,procstat,status FROM CIS.Mrcf WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "' ORDER BY datereq DESC";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   while (dr.Read())
   {
    strWrite = strWrite + "<tr>" +
                              "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetails.aspx?mrcfcode=" + dr["mrcfcode"] + "'><img src='../../Support/" + clsMRCF.GetRequestStatusIcon(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "' alt='' /></a>" +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>" +
                                                                                                 "<a href='MRCFDetails.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["intended"] + "</a><br>" +
                                                                                                    "Request Type: " + clsMRCF.GetRequestTypeDesc(dr["reqtype"].ToString()) + "<br>" +
                                                                                                    "Date Requested: " + Convert.ToDateTime(dr["datereq"]).ToString("MMMM dd,yyyy") +
                                                                                                "</td>" +
                                                                                                "<td class='GridRows'>" + clsMRCF.GetRequestStatusRemarks(dr["status"].ToString(), dr["sprvcode"].ToString(), dr["sprvstat"].ToString(), dr["headcode"].ToString(), dr["headstat"].ToString(), dr["proccode"].ToString(), dr["procstat"].ToString()) + "</td>" +
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
  clsSpeedo.Authenticate();
  
 if (!clsOracleMrcf.IsOracleUp())
  {
  
      Response.Redirect("OracleDatabaseProblem.aspx");
  }
  
 }

 protected void btnNewRequest_Click(object sender, EventArgs e)
 {
  Response.Redirect("MRCFNew.aspx");
 }

 protected void btnProcess_Click(object sender, EventArgs e)
 {
     Response.Redirect("MRCFViewOracle.aspx");
 }

 protected void LoadListAssigned()
 {
    
     clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
     string strWrite = "";
     int intCtr = 0;
     using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
     {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT Top 10 (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE assignto = '" + Request.Cookies["Speedo"]["UserName"] + "' AND isactive = '1' and statcode <> '001' AND statcode <> '000' ORDER BY hdlrcode DESC";
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         while (dr.Read())
         {
             //Response.Cookies["MRCF"]["MRCFCODE"] = dr["mrcfcode"].ToString();
             strWrite = strWrite + "<tr>" +
    "<td runat='server' class='GridRows'>" +
    "<a href='MRCFUpdateStatus.aspx?mrcfcode=" + dr["mrcfcode"] + "' runat='server' onClick=''><img src='../../Support/approval.png' alt='' /></a>" +
    "<a href='MRCFPrint.aspx?mrcfcode=" + dr["mrcfcode"] + "' runat='server' onClick=''><img src='../../Support/print32.png' alt='' /></a>" +
        "<td class='GridRows'>" +
        "<a href='MRCFUpdateStatus.aspx?mrcfcode=" + dr["mrcfcode"] + "&updatecode=" + "000" +"' style='font-size:small;'>" + dr["IntendedFor"] + "</a><br>" +
        "Assigned by: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignby"] + "'>" + dr["assignby"] + "</a><br>" +
        "Date Assigned: " + Convert.ToDateTime(dr["DateAssign"]).ToString("MMMM dd, yyyy") + "<br>" +
        "Remarks: " + dr["Remarks"] +
        "</td>" +
        "<td class='GridRows'>" + dr["StatusDescription"].ToString() +
        "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'  border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='#1874cd' style='border-radius:1px; ' width= '" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>" +
        "</td>" +
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

protected void lbtnClose_Click(object sender, EventArgs e)
 {
   //  ModalPopupExtender1.Hide();
 }

 protected void btnUpdate_Click(object sender, EventArgs e)
 {

 }



}
