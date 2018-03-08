using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HRMS;

public partial class HR_HRMS_Undertime_UndertimeDetails : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsUndertime.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"], Request.QueryString["utcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsUndertime.AuthenticateAccessForm(UndertimeUsers.Requestor, Request.Cookies["Speedo"]["UserName"], Request.QueryString["utcode"].ToString());
   clsUndertime ut = new clsUndertime(Request.QueryString["utcode"].ToString());
   ut.Fill();
   txtUTCode.Text = ut.UndertimeCode;
   txtDateFiled.Text = ut.DateFiled.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(ut.Username);
   txtStatus.Text = clsUndertime.ToUndertimeStatusText(ut.Status);
   txtDateApplied.Text = ut.DateApplied.ToString("MMM dd, yyyy hh:mm tt");
   txtReason.Text = ut.Reason;
   txtApproverName.Text = clsUsers.GetName(ut.ApproverUsername);
   txtApproverDate.Text = (ut.ApproverDate == clsDateTime.SystemMinDate ? "" : ut.ApproverDate.ToString("MMM/dd/yy hh:mm tt"));
   txtApproverRemarks.Text = ut.ApproverRemarks;
   btnCancel.Visible = clsUndertime.ToUndertimeStatus(ut.Status) == UndertimeStatus.ForApproval;
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("UndertimeMenu.aspx");
 }

 protected void btnCancel_Click(object sender, EventArgs e)
 {
  clsUndertime ut = new clsUndertime(Request.QueryString["utcode"].ToString());
  ut.Cancel();
  Response.Redirect("UndertimeMenu.aspx");
 }

}