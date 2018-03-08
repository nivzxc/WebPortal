using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EmployeeJournal_JournalEncoding : System.Web.UI.Page
{
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("JournalDCode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("JournalCode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("ItemNumber", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Contents", System.Type.GetType("System.String"));
        tblCart = EmployeeJournalDetails.GetDSGCart(EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString()));
        ViewState["Cart"] = tblCart;

        LoadGrid();
    }

    protected void LoadDSLs()
    {
        ddlDeptHead.DataSource = clsModuleApprover.DSLApproverDepartment(HRMS.clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"].ToString()), "EJS", "1");
        ddlDeptHead.DataValueField = "pvalue";
        ddlDeptHead.DataTextField = "ptext";
        ddlDeptHead.DataBind();

        ddlDivHead.DataSource = clsModuleApprover.DSLApproverDepartment(HRMS.clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"].ToString()), "EJS", "2");
        ddlDivHead.DataValueField = "pvalue";
        ddlDivHead.DataTextField = "ptext";
        ddlDivHead.DataBind();
    }

    protected void LoadGrid()
    {
        DataTable tblCart = ViewState["Cart"] as DataTable;
        dgSchedule.DataSource = tblCart;
        dgSchedule.DataBind();
        divScheduleList.Visible = dgSchedule.Rows.Count > 0;
        lblNoOBSchedule.Visible = !divScheduleList.Visible;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + ")";
            this.MakeCart();
            this.LoadDSLs();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (EmployeeJournalDetails objEmployeeJournalDetails = new EmployeeJournalDetails())
        {
            objEmployeeJournalDetails.EmployeeJournalCode = EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString());
            objEmployeeJournalDetails.Contents = txtContents.Text;
            objEmployeeJournalDetails.ItemNumber = EmployeeJournalDetails.GetTotalRecords(EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString())) + 1;
            objEmployeeJournalDetails.JournalDate = DateTime.Now;
            objEmployeeJournalDetails.CreatedOn = DateTime.Now;
            objEmployeeJournalDetails.EndorsedBy = "NA";
            objEmployeeJournalDetails.EndorsedOn = DateTime.Now;
            objEmployeeJournalDetails.EndoredRemarks = "NA";
            objEmployeeJournalDetails.ApprovedBy = "NA";
            objEmployeeJournalDetails.ApprovedOn = DateTime.Now;
            objEmployeeJournalDetails.ApprovedRemarks = "NA";
            objEmployeeJournalDetails.JournalStatus = "F";
            objEmployeeJournalDetails.IsEnabled = "1";
            if (objEmployeeJournalDetails.Insert() > 0)
            {
                Response.Redirect("JournalEncoding.aspx");
            }
        }
    }

    protected void dgSchedule_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            //test = tblCart.Rows[e.Item.ItemIndex].Field<Int64>("JournalDCode").ToString();

            using (EmployeeJournalDetails objEmployeeJournalDetails = new EmployeeJournalDetails())
            {
                objEmployeeJournalDetails.EmployeeJournalDetailsCode = Convert.ToInt16(tblCart.Rows[e.Item.ItemIndex].Field<Int64>("JournalDCode"));
                objEmployeeJournalDetails.IsEnabled = "0";
                if (objEmployeeJournalDetails.UpdateEnabled() > 0)
                {
                    tblCart.Rows[e.Item.ItemIndex].Delete();
                    ViewState["Cart"] = tblCart;
                    dgSchedule.DataSource = tblCart;
                    dgSchedule.DataBind();
                }
            }
        }
        catch
        {
            Response.Redirect("JournalEncoding.aspx");
        }
    }
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            objEmployeeJournal.EmployeeJournalCode = EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString());
            objEmployeeJournal.JournalStatus = "F";
            objEmployeeJournal.LockStatus = "1";
            objEmployeeJournal.Enabled = "1";
            objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            if (objEmployeeJournal.UpdateStatus() > 0)
            {
                using (EmployeeJournalApproval objEmployeeJournalApproval = new EmployeeJournalApproval())
                {
                    objEmployeeJournalApproval.EmployeeJournalCode = objEmployeeJournal.EmployeeJournalCode;
                    objEmployeeJournalApproval.JournalApprover = ddlDeptHead.SelectedValue.ToString();
                    objEmployeeJournalApproval.JournalAStatus = "F";
                    objEmployeeJournalApproval.JournalAOrder = 1;
                    objEmployeeJournalApproval.JournalADate = DateTime.Now;
                    objEmployeeJournalApproval.Insert();
                    
                    objEmployeeJournalApproval.EmployeeJournalCode = objEmployeeJournal.EmployeeJournalCode;
                    objEmployeeJournalApproval.JournalApprover = ddlDivHead.SelectedValue.ToString();
                    objEmployeeJournalApproval.JournalAStatus = "F";
                    objEmployeeJournalApproval.JournalAOrder = 2;
                    objEmployeeJournalApproval.JournalADate = DateTime.Now;
                    objEmployeeJournalApproval.Insert();
                }
                Response.Redirect("EmployeeJournalList.aspx");
            }
        }
    }


    protected void EditCustomer(object sender, GridViewEditEventArgs e)
    {
        dgSchedule.EditIndex = e.NewEditIndex;
        this.MakeCart();
        this.LoadDSLs();
    }
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        dgSchedule.EditIndex = -1;
        this.MakeCart();
        this.LoadDSLs();
    }
    protected void UpdateJournalDetails(object sender, GridViewUpdateEventArgs e)
    {
        string JournalDetailCode = ((HiddenField)dgSchedule.Rows[e.RowIndex].FindControl("hdnJournalDCode")).Value;
        string Contents = ((TextBox)dgSchedule.Rows[e.RowIndex].FindControl("txtContents")).Text;

        using (EmployeeJournalDetails objEmployeeJournalDetails = new EmployeeJournalDetails())
        {
            objEmployeeJournalDetails.EmployeeJournalDetailsCode = Convert.ToInt16(JournalDetailCode);
            objEmployeeJournalDetails.Contents = Contents;
            if (objEmployeeJournalDetails.UpdateContent() > 0)
            {
                dgSchedule.EditIndex = -1;
                this.MakeCart();
                this.LoadDSLs();
            }
        }

        dgSchedule.EditIndex = -1;
        this.MakeCart();
        this.LoadDSLs();
    }

    protected void DeleteJournalDetails(object sender, EventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;

            using (EmployeeJournalDetails objEmployeeJournalDetails = new EmployeeJournalDetails())
            {
                LinkButton lnkRemove = (LinkButton)sender;
                objEmployeeJournalDetails.EmployeeJournalDetailsCode = Convert.ToInt16(lnkRemove.CommandArgument);
                objEmployeeJournalDetails.IsEnabled = "0";
                if (objEmployeeJournalDetails.UpdateEnabled() > 0)
                {
                    dgSchedule.EditIndex = -1;
                    this.MakeCart();
                    this.LoadDSLs();
                }
            }
        }
        catch
        {
            Response.Redirect("JournalEncoding.aspx");
        }
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        this.MakeCart();
        this.LoadDSLs();
        dgSchedule.PageIndex = e.NewPageIndex;
        dgSchedule.DataBind();
    }

    protected void btnDraft_Click(object sender, EventArgs e)
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {

            if (objEmployeeJournal.Update() > 0)
            {
                Response.Redirect("EmployeeJournalList.aspx");
            }
        }
    }
}