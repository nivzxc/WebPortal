using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using HqWeb.Forums;
using HqWeb;
using STIeForms;
using HqWeb.Reward;
using HqWeb.GroupUpdate;

public partial class App_Master_TwoPanel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strpUserName = Request.Cookies["Speedo"]["UserName"].ToString();
        //imgpnlavatar.ImageUrl = "~/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg";
        lblDate.Text = Convert.ToDateTime(DateTime.Now).ToString("MMMM dd, yyyy");
        lblDay.Text = Convert.ToDateTime(DateTime.Now).ToString("dddd");
    }

    protected void LoadPicture()
    {
        string strWrite = "";
        string strpUserName = Request.Cookies["Speedo"]["UserName"].ToString();
        //imgpnlavatar.ImageUrl = "~/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg";

        strWrite = "<div id='headerUserImage'><a href='" + clsSystemConfigurations.PortalRootURL + "/Userpage/Userpage.aspx?username=" + strpUserName + "'><img id='imgpnlavatar' src='" + clsSystemConfigurations.PortalRootURL + "/pictures/realpicture/" + clsSpeedo.GetRealPicture(strpUserName) + ".jpg' style='height:90px;width:90px;' /></a></div>";
        Response.Write(strWrite);
    }


}
