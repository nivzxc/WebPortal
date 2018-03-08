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
using HRMS;


public partial class Report_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            ViewState["PreviousURL"] = Page.Request.UrlReferrer.ToString();
            //clsLeave.AuthenticateAccessForm(LeaveUsers.Requestor, Request.Cookies["Speedo"]["UserName"], Request.QueryString["leavcode"].ToString());
            clsLeave leave = new clsLeave(Request.QueryString["leavcode"].ToString());
            leave.Fill();
            txtLeaveCode.Text = leave.LeaveCode;
            txtLeaveType.Text = clsLeaveType.GetDescription(leave.LeaveType);
            txtDateFiled.Text = leave.DateFile.ToString("MMM dd, yyyy hh:mm tt");
            txtRequestorName.Text = clsUsers.GetName(leave.UserName);
            txtStatus.Text = clsLeave.ToLeaveStatusDesc(leave.Status);
            txtDateFrom.Text = leave.DateStart.ToString("MMM dd, yyyy hh:mm tt");
            txtDateTo.Text = leave.DateEnd.ToString("MMM dd, yyyy hh:mm tt");
            txtUnits.Text = leave.Units.ToString();
            txtReason.Text = leave.Reason;
            txtApproverName.Text = clsUsers.GetName(leave.ApproverName);
            txtApproverDate.Text = clsValidator.CheckMinDateTime(leave.ApproverDate);
            txtApproverRemarks.Text = leave.ApproverRemarks;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["PreviousURL"].ToString());
    }
}