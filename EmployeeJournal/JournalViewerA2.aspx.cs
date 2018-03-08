using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeJournal_JournalViewerA : System.Web.UI.Page
{
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("JournalDCode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("JournalCode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("ItemNumber", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Contents", System.Type.GetType("System.String"));
        tblCart = EmployeeJournalDetails.GetDSGCart(Convert.ToInt16(Request.QueryString["JournalCode"]));
        ViewState["Cart"] = tblCart;

        LoadGrid();
    }

    protected void LoadGrid()
    {
        DataTable tblCart = ViewState["Cart"] as DataTable;
        dgSchedule.DataSource = tblCart;
        dgSchedule.DataBind();
        divScheduleList.Visible = dgSchedule.Items.Count > 0;
        lblNoOBSchedule.Visible = !divScheduleList.Visible;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDepartmentHeadsRemarks.Visible = true;
            txtDivisionHeadsRemarks.Visible = true;
            txtDepartmentHeadsRemarks.Enabled = false;
            txtDivisionHeadsRemarks.Enabled = false;

            txtDepartmentHeadsRemarks.Text = EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "1");
            txtDivisionHeadsRemarks.Text = EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "2");

            lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + ")";
            this.MakeCart();
            if (EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "1").ToString() == Request.Cookies["Speedo"]["UserName"].ToString())
            {
                txtDepartmentHeadsRemarks.Enabled = true;
                btnApprove.Text = "Approved";
            }
            if (EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "2").ToString() == Request.Cookies["Speedo"]["UserName"].ToString())
            {
                txtDivisionHeadsRemarks.Enabled = true;
                btnApprove.Text = "Read and Commented";
            }
        }
    }
    protected void btnDisapprove_Click(object sender, EventArgs e)
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
            objEmployeeJournal.JournalStatus = "F";
            objEmployeeJournal.LockStatus = "0";
            objEmployeeJournal.Enabled = "1";
            objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            if (objEmployeeJournal.UpdateStatus() > 0)
            {
                using (EmployeeJournalApproval objEmployeeJournalApproval = new EmployeeJournalApproval())
                {
                    objEmployeeJournalApproval.EmployeeJournalACode = EmployeeJournalApproval.GetForApprovalJournalACode(objEmployeeJournal.EmployeeJournalCode);
                    objEmployeeJournalApproval.JournalAStatus = "D";
                    if (EmployeeJournalApproval.GetForApprovalJournalAOrder(objEmployeeJournal.EmployeeJournalCode) == 1)
                    {
                        objEmployeeJournalApproval.Remarks = txtDepartmentHeadsRemarks.Text;
                    }
                    else if (EmployeeJournalApproval.GetForApprovalJournalAOrder(objEmployeeJournal.EmployeeJournalCode) == 2)
                    {
                        objEmployeeJournalApproval.Remarks = txtDivisionHeadsRemarks.Text;
                    }
                    
                    if (objEmployeeJournalApproval.UpdateStatus() > 0)
                    {
                        Response.Redirect("EmployeeJournalListA.aspx");                  
                    }
                }

            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            //objEmployeeJournal.EmployeeJournalCode = EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString());
            objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
            objEmployeeJournal.LockStatus = "1";
            objEmployeeJournal.Enabled = "1";
            objEmployeeJournal.JournalStatus = "F";
            objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            if (objEmployeeJournal.UpdateStatus() > 0)
            {
                using (EmployeeJournalApproval objEmployeeJournalApproval = new EmployeeJournalApproval())
                {
                    objEmployeeJournalApproval.EmployeeJournalACode = EmployeeJournalApproval.GetForApprovalJournalACode(objEmployeeJournal.EmployeeJournalCode);
                    objEmployeeJournalApproval.JournalAStatus = "A";
                    if (EmployeeJournalApproval.GetForApprovalJournalAOrder(objEmployeeJournal.EmployeeJournalCode) == 1)
                    {
                        objEmployeeJournalApproval.Remarks = txtDepartmentHeadsRemarks.Text;
                    }
                    else if (EmployeeJournalApproval.GetForApprovalJournalAOrder(objEmployeeJournal.EmployeeJournalCode) == 2)
                    {
                        objEmployeeJournal.JournalStatus = "A";
                        objEmployeeJournal.UpdateStatus();
                        objEmployeeJournalApproval.Remarks = txtDivisionHeadsRemarks.Text;
                    }

                    if (objEmployeeJournalApproval.UpdateStatus() > 0)
                    {
                        Response.Redirect("EmployeeJournalListA.aspx");
                    }
                }

            }
        }
    }
}