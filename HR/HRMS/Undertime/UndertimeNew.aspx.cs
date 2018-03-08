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
using System.Data.SqlClient;
using HRMS;

public partial class HR_HRMS_Undertime_UndertimeNew : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSend.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSend.Page.ClientScript.GetPostBackEventReference(btnSend, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSend.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSend, null) + ";");
        if (!Page.IsPostBack)
        {            
            //string strProcessScript = "this.value='" + clsString.Submit + "';this.disabled=true;";
           //btnSend.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSend, "").ToString());
            lblRequestorName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);
            ddlApprover.DataSource = clsDepartmentApprover.DSLApproverEmployee(Request.Cookies["Speedo"]["UserName"], EFormType.Undertime);
            ddlApprover.DataValueField = "pvalue";
            ddlApprover.DataTextField = "ptext";
            ddlApprover.DataBind();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UndertimeMenu.aspx");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (clsUndertime.HasExistingApplication(Request.Cookies["Speedo"]["UserName"], dtpAppliedDate.Date))
        {
            lblErrMsg.Text = "You already filed an undertime for this date.";
            divError.Visible = true;
        }
        else
        {
            clsUndertime ut = new clsUndertime();
            ut.Username = Request.Cookies["Speedo"]["UserName"];
            ut.DateFiled = DateTime.Now;
            ut.DateApplied = dtpAppliedDate.Date;
            ut.Reason = txtReason.Text;
            ut.ApproverUsername = ddlApprover.SelectedValue;
            ut.Insert();
            //Response.Redirect("UndertimeMenu.aspx");
            
            //ADDED by CALVIN CAVITE FEB 15, 2018
            ScriptManager.RegisterStartupScript(this, GetType(), "Success!", "ModalSuccess();", true);

        }
    }

}
