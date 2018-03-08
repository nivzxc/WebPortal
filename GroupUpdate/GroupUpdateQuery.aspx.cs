using System;
using System.Net.NetworkInformation;
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
using STIeForms;
using HqWeb.GroupUpdate;

public partial class GroupUpdate_GroupUpdateQuery : System.Web.UI.Page
{
    protected void LoadDDl()
    {
        ddlDivision.DataSource = clsGroupUpdate.GetDSLDivision();
        ddlDivision.DataTextField = "pText";
        ddlDivision.DataValueField = "pValue";
        ddlDivision.DataBind();
        LoadDepartment();
    }

    protected void LoadDepartment()
    {
        
        if (ddlDivision.SelectedValue.ToString() == "ALL")
        {
            ddlDepartment.DataSource = clsGroupUpdate.GetDSLDepartment();
            ddlDepartment.DataTextField = "pText";
            ddlDepartment.DataValueField = "pValue";
            ddlDepartment.DataBind();
        }
        else
        {
            ddlDepartment.DataSource = clsGroupUpdate.GetDSLDepartment(ddlDivision.SelectedValue.ToString());
            ddlDepartment.DataTextField = "pText";
            ddlDepartment.DataValueField = "pValue";
            ddlDepartment.DataBind();
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDDl();
            dtDateStart.SelectedDate = DateTime.Parse("1/1/2012");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strDateStart = dtDateStart.SelectedDate.ToString("MMMddyyyy");
        string strDateEnd = dtDateEnd.SelectedDate.ToString("MMMddyyyy");

        Response.Redirect("GroupUpdateQueryResult.aspx?&DivisionCode=" + ddlDivision.SelectedValue.ToString() + "&DepartmentCode=" + ddlDepartment.SelectedValue.ToString() + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd + "&Keyword=" + txtKeyWord.Text);
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDepartment();
    }
}