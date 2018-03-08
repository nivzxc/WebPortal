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

public partial class HR_HRMS_Undertime_UndertimeDetailsA : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!clsUndertime.AuthenticateAccess(Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["utcode"].ToString()))
   Response.Redirect("~/AccessDenied.aspx");

  if (!Page.IsPostBack)
  {
   clsUndertime.AuthenticateAccessForm(UndertimeUsers.Approver, Request.Cookies["Speedo"]["UserName"], Request.QueryString["utcode"].ToString());
   clsUndertime ut = new clsUndertime(Request.QueryString["utcode"].ToString());
   ut.Fill();
   txtUTCode.Text = ut.UndertimeCode;
   txtDateFiled.Text = ut.DateFiled.ToString("MMM dd, yyyy hh:mm tt");
   txtRequestorName.Text = clsUsers.GetName(ut.Username);
   txtStatus.Text = clsUndertime.ToUndertimeStatusText(ut.Status);
   txtDateApplied.Text = ut.DateApplied.ToString("MMM dd, yyyy hh:mm tt");
   txtReason.Text = ut.Reason;
   txtApproverName.Text = clsUsers.GetName(ut.ApproverUsername);
   txtApproverDate.Text = (ut.ApproverDate == clsDateTime.SystemMinDate ? "" : ut.ApproverDate.ToString("MMM dd, yyyy hh:mm tt"));
   txtApproverRemarks.Text = ut.ApproverRemarks;

   btnApprove.Visible = (ut.Status == "F");
   btnDisApprove.Visible = (ut.Status == "F");
  }
 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
  using (clsUndertime undertime = new clsUndertime())
  {
   undertime.UndertimeCode = Request.QueryString["utcode"].ToString();
   undertime.Fill();
   undertime.ApproverRemarks = txtApproverRemarks.Text;
   undertime.ApproverDate = DateTime.Now;
   if (undertime.Approve() > 0)
            //ADDED by CALVIN CAVITE FEB 15, 2018
            ScriptManager.RegisterStartupScript(this, GetType(), "Approved!", "ModalSuccess();", true);         
  }
 }

 protected void btnDisApprove_Click(object sender, EventArgs e)
 {
  using (clsUndertime undertime = new clsUndertime())
  {
   undertime.UndertimeCode = Request.QueryString["utcode"].ToString();
   undertime.Fill();
   undertime.ApproverDate = DateTime.Now;
   undertime.ApproverRemarks = txtApproverRemarks.Text;
   if (undertime.Disapprove() > 0)
                //ADDED by CALVIN CAVITE FEB 15, 2018
                ScriptManager.RegisterStartupScript(this, GetType(), "disapproved!", "ModalDisapprove();", true);
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
  Response.Redirect("UndertimeMenu.aspx");
 }

}