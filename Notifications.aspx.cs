using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Notifications : System.Web.UI.Page
{

 private void BindInbox()
 {
  dgInbox.DataSource = clsMessage.DSGInbox(Request.Cookies["Speedo"]["UserName"]);
  dgInbox.DataBind();
  foreach(DataGridItem ditm in dgInbox.Items)
  {
   HiddenField phdnMessageCode = (HiddenField)ditm.FindControl("hdnMessageCode");
   HiddenField phdnDateSent = (HiddenField)ditm.FindControl("hdnDateSent");
   HiddenField phdnRead = (HiddenField)ditm.FindControl("hdnRead");
   Label plblDateSent = (Label)ditm.FindControl("lblDateSent");
   HyperLink plnkSubject = (HyperLink)ditm.FindControl("lnkSubject");
   HyperLink plnkSender = (HyperLink)ditm.FindControl("lnkSender");
   plnkSubject.NavigateUrl = "~/MessageDetail.aspx?msgcode=" + phdnMessageCode.Value;
   plblDateSent.Text = clsValidator.CheckDate(phdnDateSent.Value).ToString("MMM dd, yyyy hh:mm tt");
   plnkSender.NavigateUrl = "~/userpage/UserPage.aspx?username=" + plnkSender.Text; ;
   plnkSubject.Font.Bold = (phdnRead.Value == "0");
  }
 }

 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   BindInbox();
  }
 }

 protected void btnCompose_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("ComposeMessage.aspx");
 }

}