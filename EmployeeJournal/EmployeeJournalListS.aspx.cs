using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;
using System.Globalization;

public partial class EmployeeJournal_EmployeeJournalListS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
    //protected void LoadUpdates()
    //{
    //    string strWrite = "";
    //    string strStatus = "";
    //    string strLink = "";
    //    string strPreview = "";
    //    //foreach (DataRow drJournal in EmployeeJournal.GetDSG(Request.Cookies["Speedo"]["UserName"].ToString(), Convert.ToInt16(ddlJournalDates.SelectedValue.ToString()), ddlJournalYear.SelectedValue.ToString())).Rows)
    //    //foreach (DataRow drJournal in EmployeeJournal.GetDSG(Request.Cookies["Speedo"]["UserName"].ToString()).Rows)
    //    foreach (DataRow drJournal in EmployeeJournal.GetDSG(ddlEmployee.SelectedValue.ToString(), ddlJournalDates.SelectedValue.ToString(), ddlJournalYear.SelectedValue.ToString()).Rows)
    //    {

    //        if ((drJournal["WeekNumber"].ToString() == clsDateTime.GetIso8601WeekOfYear(DateTime.Today).ToString() && drJournal["WeekYear"].ToString() == DateTime.Now.Year.ToString()))
    //        {
    //            DateTime firstDayOfWeek = clsDateTime.FirstDateOfWeek(Convert.ToInt16(drJournal["WeekYear"]), Convert.ToInt16(drJournal["WeekNumber"]), CultureInfo.CurrentCulture);
    //            strLink = "EmployeeJournalViewer";
    //            strWrite = strWrite + "<tr>" +
    //                                  "<td class='GridRows'><img src='../Support/MRCF_View.png' alt='' /></td>" +
    //                                  "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>Journal: " + firstDayOfWeek.ToString("MM/dd/yyyy") + " - " + firstDayOfWeek.AddDays(6).ToString("MM/dd/yyyy") + " (Active)</a><br></td>" +
    //                                  "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
    //                                  "</tr>";
    //        }
    //        else
    //        {
    //            DateTime firstDayOfWeek = clsDateTime.FirstDateOfWeek(Convert.ToInt16(drJournal["WeekYear"]), Convert.ToInt16(drJournal["WeekNumber"]), CultureInfo.CurrentCulture);
    //            strLink = "EmployeeJournalViewer";
    //            strWrite = strWrite + "<tr>" +
    //                                  "<td class='GridRows'><img src='../Support/MRCF_View.png' alt='' /></td>" +
    //                                  "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>Journal: " + firstDayOfWeek.ToString("MM/dd/yyyy") + " - " + firstDayOfWeek.AddDays(6).ToString("MM/dd/yyyy") + "</a><br></td>" +
    //                                  "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
    //                                  "</tr>";
    //        }
    //    }
    //    //Response.Write(strWrite);
    //    lblWrite.Text = strWrite;
    //}


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
        //this.LoadUpdates();
    }
    protected void ddlJournalDates_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}