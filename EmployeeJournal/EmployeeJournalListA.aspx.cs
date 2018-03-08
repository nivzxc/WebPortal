using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;
using System.Globalization;

public partial class EmployeeJournal_EmployeeJournalListA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        if (!Page.IsPostBack)
        {
            this.LoadDetails();
        }
    }

    protected void LoadUpdates()
    {
        string strWrite = "";
        string strStatus = "";
        string strLink = "";
        string strPreview = "";
        foreach (DataRow drJournal in EmployeeJournal.GetDSG(ddlEmployee.SelectedValue.ToString(),Convert.ToInt16(ddlJournalYear.SelectedValue),ddlJournalDates.SelectedValue.ToString()).Rows)
        {
            if (EmployeeJournal.GetJournalStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "S" && EmployeeJournal.GetLockStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "0")
            {

                strLink = "JournalViewer";
                strWrite = strWrite + "<tr>" +
                                      "<td class='GridRows'><img src='../Support/MRCF_New.png' alt='' /></td>" +
                                      "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
                                      "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
                                      "</tr>";
            }
            else if (EmployeeJournal.GetJournalStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "F" && EmployeeJournal.GetLockStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "0")
            {
                strLink = "JournalViewer";
                strWrite = strWrite + "<tr>" +
                                      "<td class='GridRows'><img src='../Support/MRCF_New.png' alt='' /></td>" +
                                      "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
                                      "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
                                      "</tr>";
            }
            else if (EmployeeJournal.GetJournalStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "A")
            {
                strLink = "JournalViewer";
                strWrite = strWrite + "<tr>" +
                                      "<td class='GridRows'><img src='../Support/Approved.png' alt='' width='16' height='16' /></td>" +
                                      "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
                                      "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
                                      "</tr>";
            }
            else
            {
                strLink = "JournalViewer";
                strWrite = strWrite + "<tr>" +
                                      "<td class='GridRows'><img src='../Support/hammer128.png' alt='' width='16' height='16' /></td>" +
                                      "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
                                      "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
                                      "</tr>";
            }

        }
        //Response.Write(strWrite);
        lblWrite.Text = strWrite;
    }

    public void LoadDetails()
    {
        ddlJournalYear.DataSource = EmployeeJournal.GetDSLJournalYearsALL();
        ddlJournalYear.DataValueField = "pvalue";
        ddlJournalYear.DataTextField = "ptext";
        ddlJournalYear.DataBind();

        ddlJournalDates.DataSource = EmployeeJournal.GetDSLJournalDatesALL(ddlJournalYear.SelectedValue.ToString());
        ddlJournalDates.DataValueField = "pvalue";
        ddlJournalDates.DataTextField = "ptext";
        ddlJournalDates.DataBind();

        ddlDivision.DataSource = clsDivision.GetDdlDsBasedOnApproverModule(Request.Cookies["Speedo"]["UserName"].ToString());
        ddlDivision.DataValueField = "pvalue";
        ddlDivision.DataTextField = "ptext";
        ddlDivision.DataBind();
        // ddlDivision.SelectedValue = clsEmployee.GetDivisionCode(Request.Cookies["Speedo"]["UserName"].ToString());

        ddlDepartment.DataSource = clsDepartment.GetDdlDsBasedOnModuleApprover(ddlDivision.SelectedValue.ToString(), Request.Cookies["Speedo"]["UserName"].ToString());
        ddlDepartment.DataValueField = "pvalue";
        ddlDepartment.DataTextField = "ptext";
        ddlDepartment.DataBind();
        //ddlDepartment.SelectedValue = clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"].ToString());

        ddlEmployee.DataSource = clsEmployee.DSLEmployeeList(ddlDepartment.SelectedValue.ToString());
        ddlEmployee.DataValueField = "pvalue";
        ddlEmployee.DataTextField = "ptext";
        ddlEmployee.DataBind();
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDepartment.DataSource = clsDepartment.GetDdlDs(ddlDivision.SelectedValue.ToString());
        ddlDepartment.DataValueField = "pvalue";
        ddlDepartment.DataTextField = "ptext";
        ddlDepartment.DataBind();

        ddlEmployee.DataSource = clsEmployee.DSLEmployeeList(ddlDepartment.SelectedValue.ToString());
        ddlEmployee.DataValueField = "pvalue";
        ddlEmployee.DataTextField = "ptext";
        ddlEmployee.DataBind();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee.DataSource = clsEmployee.DSLEmployeeList(ddlDepartment.SelectedValue.ToString());
        ddlEmployee.DataValueField = "pvalue";
        ddlEmployee.DataTextField = "ptext";
        ddlEmployee.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.LoadUpdates();
    }
}