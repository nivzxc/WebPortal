using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_ATW_ATWDetails : System.Web.UI.Page
{

 private void LoadDetails()
 {
  using (clsATW atw = new clsATW())
  {
   atw.ATWCode = Request.QueryString["atwcode"].ToString();
   atw.Fill();
   txtATWCode.Text = atw.ATWCode;
   txtDateFiled.Text = atw.DateFile.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(atw.Username);
   txtReason.Text = atw.Reason;
   txtApproverH.Text = clsUsers.GetName(atw.ApproverHeadName);
   txtStatusH.Text = clsATW.ToATWStatus(atw.ApproverHeadStatus);
   txtRemarksH.Text = atw.ApproverHeadRemarks;
   txtApproverD.Text = clsUsers.GetName(atw.ApproverDivisionName);
   txtStatusD.Text = clsATW.ToATWStatus(atw.ApproverDivisionStatus);
   txtRemarksD.Text = atw.ApproverDivisionRemarks;
   txtStatus.Text = clsATW.ToATWStatus(atw.Status);

   btnCancel.Visible = clsATW.ToATWStatusDesc(atw.Status) == ATWStatus.ForApproval;
  }
 }

 protected void LoadSchedule()
 {
  string strWrite = "";
  DataTable tblATWDetails = clsATWDetails.GetDSGScheduleHTML(Request.QueryString["atwcode"]);
  foreach (DataRow drw in tblATWDetails.Rows)
  {
   strWrite = strWrite + "<tr>" +
                          "<td class='GridRows' style='text-align:center;'><img src='../../../Support/" + (drw["status"].ToString() == "1" ? "check16.png" : "close16.png") + "' /></td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["datestrt"].ToString()).ToString("MM/dd/yy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + clsValidator.CheckDate(drw["dateend"].ToString()).ToString("MM/dd/yy hh:mm tt") + "</td>" +
                          "<td class='GridRows'>" + drw["reason"].ToString() + "</td>" +
                          "<td class='GridRows'>" + drw["remarks"].ToString() + "</td>" +
                         "</tr>";
  }
  Response.Write(strWrite);
 }

 //////////////////////////////
 ///////// Page Event /////////
 ////////////////////////////// 

 protected void Page_Load(object sender, EventArgs e)
 {
    clsSpeedo.Authenticate();
  if (!clsATW.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["atwcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   LoadDetails();
  }
 }

 protected void btnCancel_Click(object sender, ImageClickEventArgs e)
 {
  using (clsATW atw = new clsATW())
  {
   atw.ATWCode = Request.QueryString["atwcode"].ToString();
   atw.Cancel();
  }
  Response.Redirect("ATWMenu.aspx");
 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("ATWMenu.aspx");
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
     Response.Redirect("ATWMenu.aspx");
 }
 protected void btnCancel_Click(object sender, EventArgs e)
 {
     using (clsATW atw = new clsATW())
     {
         atw.ATWCode = Request.QueryString["atwcode"].ToString();
         atw.Cancel();
     }
     Response.Redirect("ATWMenu.aspx");
 }
}