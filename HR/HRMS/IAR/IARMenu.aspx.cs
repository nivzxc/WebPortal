using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_IAR_IARMenu : System.Web.UI.Page
{

   protected void LoadApproverDivision()
   {
      string strWrite = "";
      DataTable tblIAR = clsIAR.GetDSGMenuDivision(Request.Cookies["Speedo"]["UserName"]);
      foreach (DataRow drw in tblIAR.Rows)
      {
         strWrite = strWrite + "<tr>" +
                                "<td class='GridRows'>" +
                                 "<a href='IARDetailsD.aspx?iarcode=" + drw["iarcode"] + "'><img src='../../../Support/" + clsIAR.GetRequestStatusIcon(drw["status"].ToString()) + "' alt='' /></a>" +
                                "</td>" +
                                "<td class='GridRows'>" +
                                 "<a href='IARDetailsD.aspx?iarcode=" + drw["iarcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                                 "Sent by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                                 "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                "</td>" +
                                "<td class='GridRows'>" + clsIAR.GetRequestStatusRemarks(drw["status"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString()) + "</td>" +
                               "</tr>";
      }
      Response.Write(strWrite);
      if (tblIAR.Rows.Count == 0)
         Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblIAR.Rows.Count + " records found ]</td></tr>");
   }

   protected void LoadApproverHead()
   {
      string strWrite = "";
      DataTable tblIAR = clsIAR.GetDSGMenuHead(Request.Cookies["Speedo"]["UserName"]);
      foreach (DataRow drw in tblIAR.Rows)
      {
         strWrite = strWrite + "<tr>" +
                                "<td class='GridRows'>" +
                                 "<a href='IARDetailsH.aspx?iarcode=" + drw["iarcode"] + "'><img src='../../../Support/" + clsIAR.GetRequestStatusIcon(drw["status"].ToString()) + "' alt='' /></a>" +
                                "</td>" +
                                "<td class='GridRows'>" +
                                 "<a href='IARDetailsH.aspx?iarcode=" + drw["iarcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                                 "Sent by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                                 "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                "</td>" +
                                "<td class='GridRows'>" + clsIAR.GetRequestStatusRemarks(drw["status"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString()) + "</td>" +
                               "</tr>";
      }
      Response.Write(strWrite);
      if (tblIAR.Rows.Count == 0)
         Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblIAR.Rows.Count + " records found ]</td></tr>");
   }

   protected void LoadIAR()
   {
      string strWrite = "";
      DataTable tblOvertime = clsIAR.GetDSGMenu(Request.Cookies["Speedo"]["UserName"]);
      foreach (DataRow drw in tblOvertime.Rows)
      {
         strWrite = strWrite + "<tr>" +
                                "<td class='GridRows'>" +
                                 "<a href='IARDetails.aspx?iarcode=" + drw["iarcode"].ToString() + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["status"].ToString()) + "' alt='' /></a>" +
                                "</td>" +
                                "<td class='GridRows'>" +
                                 "<a href='IARDetails.aspx?iarcode=" + drw["iarcode"].ToString() + "' style='font-size:small;'>" + drw["reason"].ToString() + "</a><br>" +
                                 "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                "</td>" +
                                "<td class='GridRows'>" + clsIAR.GetRequestStatusRemarks(drw["status"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString()) + "</td>" +
                               "</tr>";
      }
      Response.Write(strWrite);
      if (tblOvertime.Rows.Count == 0)
         Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblOvertime.Rows.Count + " records found ]</td></tr>");
   }

   ///////////////////////////////
   ///////// Page Events /////////
   ///////////////////////////////

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         string strUsername = Request.Cookies["Speedo"]["UserName"];

         bool blnHasEliteUserAccess = clsSystemModule.HasAccess("022", strUsername);
         trEliteUser.Visible = blnHasEliteUserAccess;
         trEliteUserSpacer.Visible = blnHasEliteUserAccess;

         if (clsModuleApprover.IsApprover(strUsername, clsModule.IARModule, "1"))
         {
            trApproverHead.Visible = true;
            trApproverHeadSpacer.Visible = true;
         }
         else
         {
            trApproverHead.Visible = false;
            trApproverHeadSpacer.Visible = false;
         }

         if (clsModuleApprover.IsApprover(strUsername, clsModule.IARModule, "2"))
         {
            trApproverDivision.Visible = true;
            trApproverDivisionSpacer.Visible = true;
         }
         else
         {
            trApproverDivision.Visible = false;
            trApproverDivisionSpacer.Visible = false;
         }
      }
   }

   protected void btnNewRequest_Click(object sender, EventArgs e)
   {
      Response.Redirect("IARNew.aspx");
   }

}