using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ComposeMessage : System.Web.UI.Page
{


 ///////////////////////////////
 ///////// Form Events /////////
 ///////////////////////////////

 protected void Page_Load(object sender, EventArgs e)
 {
  if (!Page.IsPostBack)
  {
   txtFrom.Text = Request.Cookies["Speedo"]["UserName"].ToString();
  }
 }

 protected void btnSend_Click(object sender, ImageClickEventArgs e)
 {

 }

 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  Response.Redirect("Notifications.aspx");
 }

}