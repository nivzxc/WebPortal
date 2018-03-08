using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CMD_SER_SERMain : System.Web.UI.Page
{
 protected void Page_Load(object sender, EventArgs e)
 {
  bool blnHasManagementReportsAccess = clsSystemModule.HasAccess("014", Request.Cookies["Speedo"]["Username"]);
  trSERReports.Visible = blnHasManagementReportsAccess;
 }
}
