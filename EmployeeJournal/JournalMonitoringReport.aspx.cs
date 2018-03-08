using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class EmployeeJournal_JournalMonitoringReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        if (!Page.IsPostBack)
        {
            this.LoadDetails();
        }
    }

    public void LoadDetails()
    {
        ddlJournalYear.DataSource = EmployeeJournal.GetDSLJournalYearsALL();
        ddlJournalYear.DataValueField = "pvalue";
        ddlJournalYear.DataTextField = "ptext";
        ddlJournalYear.DataBind();

        ddlJournalDates.DataSource = EmployeeJournal.GetDSLJournalDates(ddlJournalYear.SelectedValue.ToString());
        ddlJournalDates.DataValueField = "pvalue";
        ddlJournalDates.DataTextField = "ptext";
        ddlJournalDates.DataBind();
        ddlJournalDates.SelectedValue = WeekYear.GetActiveWeekCode();

        ddlDivision.DataSource = clsDivision.GetDdlDsBasedOnApproverModule(Request.Cookies["Speedo"]["UserName"].ToString());
        ddlDivision.DataValueField = "pvalue";
        ddlDivision.DataTextField = "ptext";
        ddlDivision.DataBind();

        ddlDepartment.DataSource = clsDepartment.GetDdlDsBasedOnModuleApprover(ddlDivision.SelectedValue.ToString(), Request.Cookies["Speedo"]["UserName"].ToString());
        ddlDepartment.DataValueField = "pvalue";
        ddlDepartment.DataTextField = "ptext";
        ddlDepartment.DataBind();

    }

    protected void LoadUpdates()
    {
        string strWrite = "";
        string strStatus = "";
        string strLink = "";
        string strPreview = "";
        string strJournalStatus = "";
        string strEmployeeStatDate = "";
        string strDepHeadStatus = "";
        string strDivHeadStatus = "";
        foreach (DataRow drJournal in EmployeeJournal.GetDSG(Convert.ToInt16(ddlJournalYear.SelectedValue), ddlJournalDates.SelectedValue.ToString(),ddlDepartment.SelectedValue.ToString()).Rows)
        {
            if (drJournal["JournalStatus"].ToString() == "S" && drJournal["LockStatus"].ToString() == "0")
            {
                strJournalStatus = "Draft";
                strEmployeeStatDate = "(Last Log on " + drJournal["ModifiedOn"].ToString() + ")";
                strDepHeadStatus = "";
                strDivHeadStatus = "";
            }
            else if (drJournal["JournalStatus"].ToString() == "F")
            {
                strJournalStatus = "For Approval";
                strEmployeeStatDate = "(Submitted on " + drJournal["ModifiedOn"].ToString() + ")";
                if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(drJournal["JournalCode"]), "1") == "A")
                {
                    strDepHeadStatus = "(" + EmployeeJournalApproval.GetApproverADate(Convert.ToInt16(drJournal["JournalCode"]), "1") + ")";
                }
                else 
                {
                    strDepHeadStatus = "(Not Yet Approved)";
                }
                if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(drJournal["JournalCode"]), "2") == "A")
                {
                    strDivHeadStatus = "(" + EmployeeJournalApproval.GetApproverADate(Convert.ToInt16(drJournal["JournalCode"]), "2") + ")";
                }
                else
                {
                    strDivHeadStatus = "(Not Yet Approved)";
                }
            }
            else if (drJournal["JournalStatus"].ToString() == "A")
            {
                strJournalStatus = "Approved";
                strEmployeeStatDate = "(Submitted on " + drJournal["ModifiedOn"].ToString() + ")";
                strDepHeadStatus = "";
                strDivHeadStatus = "";
            }
            strLink = "JournalViewer";
            strWrite = strWrite + "<tr>" +
                                  "<td class='GridRows'>" + clsEmployee.GetName(drJournal["CreatedBy"].ToString()) + "<br />" + strEmployeeStatDate + "</td>" +
                                  "<td class='GridRows'>" + strJournalStatus + "</td>" +
                                  "<td class='GridRows'>" + EmployeeJournalApproval.GetApprover(Convert.ToInt16(drJournal["JournalCode"]), "1") + "<br />" + strDepHeadStatus + "</td>" +
                                  //"<td class='GridRows'>" + EmployeeJournalApproval.GetApprover(Convert.ToInt16(drJournal["JournalCode"]), "2") + "<br />" + strDivHeadStatus + "</td>" +
                                  "</tr>";

        }
        //Response.Write(strWrite);
        lblWrite.Text = strWrite;
    }

    protected void ddlJournalYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlJournalDates.DataSource = EmployeeJournal.GetDSLJournalDatesALL(ddlJournalYear.SelectedValue.ToString());
        ddlJournalDates.DataValueField = "pvalue";
        ddlJournalDates.DataTextField = "ptext";
        ddlJournalDates.DataBind();

        try
        {
            ddlJournalDates.SelectedValue = WeekYear.GetActiveWeekCode();
        }
        catch { }
    }
    protected void ddlJournalDates_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDepartment.DataSource = clsDepartment.GetDdlDsBasedOnModuleApprover(ddlDivision.SelectedValue.ToString(), Request.Cookies["Speedo"]["UserName"].ToString());
        ddlDepartment.DataValueField = "pvalue";
        ddlDepartment.DataTextField = "ptext";
        ddlDepartment.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.LoadUpdates();
    }
}