using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using HRMS;

public partial class Admin_UserModuleEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strModuleCode = Request.QueryString["modlcode"];
        string strUsername = Request.QueryString["username"];

        if (!clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }

        clsModule.DeleteUser(strModuleCode, strUsername);
        Response.Redirect("UserModuleMain.aspx");
    }
}