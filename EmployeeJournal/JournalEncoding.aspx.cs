using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class EmployeeJournal_JournalEncoding : System.Web.UI.Page
{
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("JournalDCode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("JournalCode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("ItemNumber", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Contents", System.Type.GetType("System.String"));
        //tblCart = EmployeeJournalDetails.GetDSGCart(EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()));
        tblCart = EmployeeJournalDetails.GetDSGCart(Convert.ToInt16(Request.QueryString["JournalCode"]));
        ViewState["Cart"] = tblCart;
        //objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
        LoadGrid();
    }

    protected void LoadDSLs()
    {
        //ddlDeptHead.DataSource = clsModuleApprover.DSLApproverDepartment(HRMS.clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"].ToString()), "EJS", "1");
        //ddlDeptHead.DataValueField = "pvalue";
        //ddlDeptHead.DataTextField = "ptext";
        //ddlDeptHead.DataBind();

        //ddlDivHead.DataSource = clsModuleApprover.DSLApproverDepartment(HRMS.clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"].ToString()), "EJS", "2");
        //ddlDivHead.DataValueField = "pvalue";
        //ddlDivHead.DataTextField = "ptext";
        //ddlDivHead.DataBind();
    }

    protected void LoadGrid()
    {
        //DataTable tblCart = ViewState["Cart"] as DataTable;
        //dgSchedule.DataSource = tblCart;
        //dgSchedule.DataBind();
        //divScheduleList.Visible = dgSchedule.Rows.Count > 0;
        //lblNoOBSchedule.Visible = !divScheduleList.Visible;
    }

    public void LoadDueValidation()
    {
        //Response.Write(WeekYear.GetDateDue(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"]))).AddHours(9).ToString());
        //Response.Write("<br> " + DateTime.Now.ToString());
        //Response.Write(Convert.ToDateTime("11/12/2015 4:50:17 PM").ToString());
        if (WeekYear.GetDateDue(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"]))).AddHours(9) < DateTime.Now)
        //if (Convert.ToDateTime("11/12/2015 4:57:00 PM") < DateTime.Now)
        {
            if (JournalExceptions.CountIfExist(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])),Request.Cookies["Speedo"]["UserName"].ToString())>0)
            {
                btnDraft.Enabled = true;
                btnFinalize.Enabled = true;
                btnPreview.Enabled = true;
            }
            else
            {
                btnDraft.Enabled = false;
                btnFinalize.Enabled = false;
                btnPreview.Enabled = false;
            }

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        this.LoadDueValidation();
        if (!Page.IsPostBack)
        {
            //lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + ")";
            lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + ")";
            this.MakeCart();
            this.LoadDSLs();
            this.LoadContents();

            if (EmployeeReviewer.GetReviewer(Request.Cookies["Speedo"]["UserName"].ToString()).Length > 0)
            {
                lblReviewer.ForeColor = Color.Black;
                lblReviewer.Text = EmployeeReviewer.GetReviewer(Request.Cookies["Speedo"]["UserName"].ToString());
            }
            else
            {
                lblReviewer.ForeColor = Color.Red;
                lblReviewer.Text = "No assigned reviewer.";
            }
        }
    }

    public void LoadContents()
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            //objEmployeeJournal.EmployeeJournalCode = EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString());
            objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
            objEmployeeJournal.Fill2();
            ckeContents.Text = objEmployeeJournal.Contents;
        }
    }
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        ViewState["JournalCode"] = Request.QueryString["JournalCode"];
        if (lblReviewer.ForeColor == Color.Black)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
                {
                    objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
                    objEmployeeJournal.JournalStatus = "F";
                    objEmployeeJournal.LockStatus = "1";
                    objEmployeeJournal.Enabled = "1";
                    objEmployeeJournal.Contents = ckeContents.Text.Replace("style=\"display: none\"", "");
                    objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                    if (objEmployeeJournal.UpdateStatus() > 0)
                    {
                        using (EmployeeJournalApproval objEmployeeJournalApproval = new EmployeeJournalApproval())
                        {
                            objEmployeeJournalApproval.EmployeeJournalCode = objEmployeeJournal.EmployeeJournalCode;
                            objEmployeeJournalApproval.JournalApprover = EmployeeReviewer.GetReviewer(Request.Cookies["Speedo"]["UserName"].ToString());
                            objEmployeeJournalApproval.JournalAStatus = "F";
                            objEmployeeJournalApproval.JournalAOrder = 1;
                            objEmployeeJournalApproval.JournalADate = DateTime.Now;
                            objEmployeeJournalApproval.Insert();

                            objEmployeeJournal.SendNotification(EmployeeJournal.EJSMailType.FiledAcknowledgementRequestor, Request.Cookies["Speedo"]["UserName"].ToString(), EmployeeReviewer.GetReviewer(Request.Cookies["Speedo"]["UserName"].ToString()));
                            objEmployeeJournal.SendNotification(EmployeeJournal.EJSMailType.FiledNotificationApprover, Request.Cookies["Speedo"]["UserName"].ToString(), EmployeeReviewer.GetReviewer(Request.Cookies["Speedo"]["UserName"].ToString()));
                        }
                        Response.Redirect("EmployeeJournalList.aspx");
                    }
                }
            }
            else
            {
                using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
                {
                    objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
                    objEmployeeJournal.Contents = ckeContents.Text;
                    objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                    objEmployeeJournal.ModifiedOn = DateTime.Now;

                    if (objEmployeeJournal.Update() > 0)
                    {
                        Response.Redirect("JournalEncoding.aspx?JournalCode=" + Request.QueryString["JournalCode"]);
                    }
                }
            }
        }

    }


    protected void btnDraft_Click(object sender, EventArgs e)
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            //objEmployeeJournal.EmployeeJournalCode = EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString());
            objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
            objEmployeeJournal.Contents = ckeContents.Text.Replace("style=\"display: none\"","");
            objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objEmployeeJournal.ModifiedOn = DateTime.Now;

            if (objEmployeeJournal.Update() > 0)
            {
                Response.Redirect("EmployeeJournalList.aspx");
            }
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            //objEmployeeJournal.EmployeeJournalCode = EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString());
            objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
            objEmployeeJournal.Contents = ckeContents.Text.Replace("style=\"display: none\"", "");
            objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objEmployeeJournal.ModifiedOn = DateTime.Now;

            if (objEmployeeJournal.Update() > 0)
            {
                Response.Redirect("JournalViewer.aspx?JournalCode=" + Request.QueryString["JournalCode"]);
            }
        }
    }
}