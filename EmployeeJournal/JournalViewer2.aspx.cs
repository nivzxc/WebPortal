using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeJournal_JournalViewer : System.Web.UI.Page
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
        lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + ")";
        this.MakeCart();
        lblDeptApprover.Text = "(" + EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "1") + ")";
        lblDepartmentHeadsRemarks.Text = EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "1");
        lblDivApprover.Text = "(" + EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "2") + ")";
        lblDivisionHeadsRemarks.Text = EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "2");
    }
}