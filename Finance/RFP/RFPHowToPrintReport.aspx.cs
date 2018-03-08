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

public partial class Finance_RFP_RFPHowToPrintReport : System.Web.UI.Page
{
 protected void Page_Load(object sender, EventArgs e)
 {
  btnBack.Attributes.Add("onClick", "javascript:window.close();");
 }
 protected void btnBack_Click(object sender, ImageClickEventArgs e)
 {
  //Response.Write("<script language='javascript'> { self.close() }</script>");
 }
}
