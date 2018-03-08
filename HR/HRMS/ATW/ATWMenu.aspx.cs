using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class HR_HRMS_ATW_ATWMenu : System.Web.UI.Page
{

   protected void LoadApproverDivision()
   {
      string strWrite = "";
      DataTable tblATW = clsATW.GetDSGMenuDivision(Request.Cookies["Speedo"]["UserName"]);
      foreach (DataRow drw in tblATW.Rows)
      {
         strWrite = strWrite + "<tr>" +
                                "<td class='GridRows'>" +
                                 "<a href='ATWDetailsD.aspx?atwcode=" + drw["atwcode"] + "'><img src='../../../Support/" + clsATW.GetRequestStatusIcon(drw["status"].ToString()) + "' alt='' /></a>" +
                                "</td>" +
                                "<td class='GridRows'>" +
                                 "<a href='ATWDetailsD.aspx?atwcode=" + drw["atwcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                                 "Sent by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                                 "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                "</td>" +
                                "<td class='GridRows'>" + clsATW.GetRequestStatusRemarks(drw["status"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString()) + "</td>" +
                               "</tr>";
      }
      Response.Write(strWrite);
      if (tblATW.Rows.Count == 0)
         Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblATW.Rows.Count + " records found ]</td></tr>");
   }

   protected void LoadApproverHead()
   {
      string strWrite = "";
      DataTable tblATW = clsATW.GetDSGMenuHead(Request.Cookies["Speedo"]["UserName"]);
      foreach (DataRow drw in tblATW.Rows)
      {
         strWrite = strWrite + "<tr>" +
                                "<td class='GridRows'>" +
                                 "<a href='ATWDetailsH.aspx?atwcode=" + drw["atwcode"] + "'><img src='../../../Support/" + clsATW.GetRequestStatusIcon(drw["status"].ToString()) + "' alt='' /></a>" +
                                "</td>" +
                                "<td class='GridRows'>" +
                                 "<a href='ATWDetailsH.aspx?atwcode=" + drw["atwcode"] + "' style='font-size:small;'>" + clsString.CutString(drw["reason"].ToString(), 50) + "</a><br>" +
                                 "Sent by: <a href='../../../Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["username"] + "</a><br>" +
                                 "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                "</td>" +
                                "<td class='GridRows'>" + clsATW.GetRequestStatusRemarks(drw["status"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString()) + "</td>" +
                               "</tr>";
      }
      Response.Write(strWrite);
      if (tblATW.Rows.Count == 0)
         Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
      else
         Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblATW.Rows.Count + " records found ]</td></tr>");
   }

   protected void LoadATW()
   {
      string strWrite = "";
      DataTable tblOvertime = clsATW.GetDSGMenu(Request.Cookies["Speedo"]["UserName"]);
      foreach (DataRow drw in tblOvertime.Rows)
      {
         strWrite = strWrite + "<tr>" +
                                "<td class='GridRows'>" +
                                 "<a href='ATWDetails.aspx?atwcode=" + drw["atwcode"].ToString() + "'><img src='../../../Support/" + clsOvertime.GetRequestStatusIcon(drw["status"].ToString()) + "' alt='' /></a>" +
                                "</td>" +
                                "<td class='GridRows'>" +
                                 "<a href='ATWDetails.aspx?atwcode=" + drw["atwcode"].ToString() + "' style='font-size:small;'>" + drw["reason"].ToString() + "</a><br>" +
                                 "Date Filed: " + Convert.ToDateTime(drw["datefile"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                "</td>" +
                                "<td class='GridRows'>" + clsATW.GetRequestStatusRemarks(drw["status"].ToString(), drw["apphname"].ToString(), drw["apphstat"].ToString(), drw["appdname"].ToString(), drw["appdstat"].ToString()) + "</td>" +
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

         bool blnHasEliteUserAccess = clsSystemModule.HasAccess("021", strUsername);
         //trEliteUser.Visible = blnHasEliteUserAccess;
         //trEliteUserSpacer.Visible = blnHasEliteUserAccess;

         if (clsModuleApprover.IsApprover(strUsername, clsModule.ATWModule, "1"))
         {
            trApproverHead.Visible = true;
            trApproverHeadSpacer.Visible = true;
         }
         else
         {
            trApproverHead.Visible = false;
            trApproverHeadSpacer.Visible = false;
         }

         if (clsModuleApprover.IsApprover(strUsername, clsModule.ATWModule, "2"))
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

   protected void btnNewRequest_Click(object sender, ImageClickEventArgs e)
   {
      
   }

   protected void btnNewRequest_Click1(object sender, EventArgs e)
   {
       Response.Redirect("ATWNew.aspx");
   }
}