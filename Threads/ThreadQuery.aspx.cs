using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Threads_ThreadQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDDl();
            dtDateStart.SelectedDate = DateTime.Parse("1/1/2005");
        }
    }

    protected void LoadDDl()
    {
        ddlCategory.DataSource = DALThread.GetThreadCategory();
        ddlCategory.DataValueField = "pValue";
        ddlCategory.DataTextField = "pText";
        ddlCategory.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strDateStart = dtDateStart.SelectedDate.ToString("MMMddyyyy");
        string strDateEnd = dtDateEnd.SelectedDate.ToString("MMMddyyyy");

        Response.Redirect("ThreadQueryResult.aspx?&Category=" + ddlCategory.SelectedValue.ToString() + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd + "&Keyword=" + txtKeyWord.Text);
    }
}