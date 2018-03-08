using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HqWeb.GroupUpdate;
using System.Data;
using System.Globalization;

public partial class EmployeeJournal_EmployeeJournalList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        //this.LoadUpdates();

       // Response.Write(WeekYear.GetActiveWeekCode().ToString());
        if (!Page.IsPostBack)
        {
            btnEmployeesJournal.Visible = clsModuleApprover.IsApprover(Request.Cookies["Speedo"]["UserName"].ToString(), "EJS");
            btnMonitoring.Visible = clsModuleApprover.IsApprover(Request.Cookies["Speedo"]["UserName"].ToString(), "EJS");
            btnWeekMap.Visible = clsSystemModule.HasAccess("EJSAdmin", Request.Cookies["Speedo"]["UserName"].ToString());
            btnReviewers.Visible = clsSystemModule.HasAccess("EJSAdmin", Request.Cookies["Speedo"]["UserName"].ToString());
            this.LoadDetails();
        }
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
        try
        {
            ddlJournalDates.SelectedValue = WeekYear.GetActiveWeekCode();
        }
        catch { }

    }
    protected void LoadUpdates()
    {
        string strWrite = "";
        string strStatus = "";
        string strLink = "";
        string strPreview = "";
        foreach (DataRow drJournal in EmployeeJournal.GetDSG(Request.Cookies["Speedo"]["UserName"],Convert.ToInt16(ddlJournalYear.SelectedValue),ddlJournalDates.SelectedValue.ToString()).Rows)
        {
            //if (WeekYear.GetActiveWeekCode().ToString() == drJournal["WeekCode"].ToString())
            //{
            if (EmployeeJournal.GetJournalStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "S" && EmployeeJournal.GetLockStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "0")
                {
                    strLink = "JournalEncoding";
                    strWrite = strWrite + "<tr>" +
                                          "<td class='GridRows'><img src='../Support/Pending.png' alt='' width='16' height='16' /></td>" +
                                          "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
                                          "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
                                          "</tr>";
                }
                else if (EmployeeJournal.GetJournalStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "F" && EmployeeJournal.GetLockStatus(Convert.ToInt16(drJournal["JournalCode"])).ToString() == "0")
                {
                    strLink = "JournalEncodingM";
                    strWrite = strWrite + "<tr>" +
                                          "<td class='GridRows'><img src='../Support/MRCF_New.png' alt='' width='16' height='16' /></td>" +
                                          "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
                                          "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
                                          "</tr>";
                }
                else
                {
                    strLink = "JournalViewer";
                    strWrite = strWrite + "<tr>" +
                                          "<td class='GridRows'><img src='../Support/MRCF_View.png' alt='' width='16' height='16' /></td>" +
                                          "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
                                          "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
                                          "</tr>";
                }
            //}
            //else
            //{
            //    strLink = "JournalViewer";
            //    strWrite = strWrite + "<tr>" +
            //                          "<td class='GridRows'><img src='../Support/MRCF_View.png' alt='' /></td>" +
            //                          "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br></td>" +
            //                          "<td class='GridRows'>" + drJournal["ModifiedOn"].ToString() + "</td>" +
            //                          "</tr>";
            //}

        }
        //Response.Write(strWrite);
        lblWrite.Text = strWrite;
    }
    protected void btnGoToJournal_Click(object sender, EventArgs e)
    {
        string strLink = "";
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            try
            {
                foreach (DataRow drUncreatedJournal in EmployeeJournal.GetDSGUncreatedJournal(Request.Cookies["Speedo"]["UserName"]).Rows)
                {
                    objEmployeeJournal.WeekCode = Convert.ToInt16(drUncreatedJournal["WeekCode"]);
                    objEmployeeJournal.Enabled = "1";
                    objEmployeeJournal.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                    objEmployeeJournal.CreatedOn = DateTime.Now;
                    objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                    objEmployeeJournal.ModifiedOn = DateTime.Now;
                    objEmployeeJournal.Insert();
                }

                if (EmployeeJournal.HasExistingJournal(Request.Cookies["Speedo"]["UserName"].ToString(), Convert.ToInt16(WeekYear.GetActiveWeekCode())))
                {
                    if (EmployeeJournal.GetJournalStatus(Convert.ToInt16(EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()))).ToString() == "S" && EmployeeJournal.GetLockStatus(Convert.ToInt16(EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()))).ToString() == "0")
                    {
                        strLink = "JournalEncoding";
                        Response.Redirect("JournalEncoding.aspx?JournalCode=" + EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()).ToString());

                    }
                    else if (EmployeeJournal.GetJournalStatus(Convert.ToInt16(EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()))).ToString() == "F" && EmployeeJournal.GetLockStatus(Convert.ToInt16(EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()))).ToString() == "0")
                    {
                        strLink = "JournalEncodingM";
                        Response.Redirect("JournalEncodingM.aspx?JournalCode=" + EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()).ToString());
                    }
                    else
                    {
                        strLink = "JournalViewer";
                        Response.Redirect("JournalViewer.aspx?JournalCode=" + EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()).ToString());
                    }
                }
                else
                {
                    try
                    {
                        objEmployeeJournal.WeekCode = Convert.ToInt16(WeekYear.GetActiveWeekCode());
                        objEmployeeJournal.Enabled = "1";
                        objEmployeeJournal.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                        objEmployeeJournal.CreatedOn = DateTime.Now;
                        objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                        objEmployeeJournal.ModifiedOn = DateTime.Now;
                        if (objEmployeeJournal.Insert() > 0)
                        {
                            //Response.Redirect("JournalEncoding.aspx");
                            //Response.Redirect("JournalEncoding.aspx?JournalCode=" + EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()).ToString());

                            Response.Redirect("JournalEncoding.aspx?JournalCode=" + EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()).ToString());

                        }
                    }
                    catch
                    {
                        //Response.Redirect("JournalUnavailable.aspx");
                        Response.Redirect("EmployeeJournalList.aspx");
                    }
                }
            }
            catch 
            { 
                
            }

        }

    }
    protected void btnEmployeesJournal_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeJournalListA.aspx");
    }
    protected void btnMonitoring_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalMonitoringReport.aspx");
    }
    protected void btnWeekMap_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalWeeks.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.LoadUpdates();
    }
    protected void btnReviewers_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalApprovers.aspx");
    }
}