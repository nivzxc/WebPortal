using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_RequestFormMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dtDateStart.SelectedValue = clsDateTime.GetFirstDayOfMonth(DateTime.Now);
            dtDateEnd.SelectedValue = clsDateTime.GetLastDayOfMonth(DateTime.Now);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strDateStart = dtDateStart.SelectedDate.ToString("MMMddyyyy");
        string strDateEnd = dtDateEnd.SelectedDate.ToString("MMMddyyyy");

        if (ddlRequestForm.SelectedValue.ToString() == "RFP")
        {
            Response.Redirect("RFPList.aspx?&RequestForm=" + ddlRequestForm.SelectedValue.ToString() + "&Status=" + ddlStatus.SelectedValue.ToString() + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd + "&Keyword=" + txtKeyword.Text);
        }

        else if (ddlRequestForm.SelectedValue.ToString() == "Leave")
        {
            Response.Redirect("LeaveList.aspx?&RequestForm=" + ddlRequestForm.SelectedValue.ToString() + "&Status=" + ddlStatus.SelectedValue.ToString() + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd + "&Keyword=" + txtKeyword.Text);
        }
    }
}