using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Userpage_UserpageLog : System.Web.UI.Page
{

 protected void LoadLogList()
 {
  string strWrite = "";
  DataTable tblLogList = clsGuestbooksLog.DSGLogList(Request.Cookies["Speedo"]["UserName"]);
  foreach (DataRow drw in tblLogList.Rows)
   strWrite += "<tr><td>" + drw["viewedby"].ToString() + "</td><td>" + drw["viewedon"].ToString() + "</td></tr>";

  Response.Write("<table border='1'><tr><td>Viewed By</td><td>Viewed On</td></tr>" + strWrite + "</table>");
 }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
