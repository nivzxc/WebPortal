using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeJournal_JournalEncodingM : System.Web.UI.Page
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
            lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + ")";
            this.MakeCart();

            lblDeptApprover.Text = "(" + EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "1") + ")";
            lblDeptRemarks.Text = EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "1");
            lblDivApprover.Text = "(" + EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "2") + ")";
            lblDivRemarks.Text = EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "2");
            ViewState["JournalCode"] = Request.QueryString["JournalCode"].ToString();
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
                Response.Redirect("JournalEncodingM.aspx?JournalCode=" + ViewState["JournalCode"].ToString());
            }
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
                    objEmployeeJournalApproval.ResetStatus();
                }
                
                Response.Redirect("EmployeeJournalList.aspx");
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
            Response.Redirect("JournalEncodingM.aspx?JournalCode=" + ViewState["JournalCode"].ToString());
        }
    }
    protected void dgSchedule_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string test = "";
        test = e.CommandName.ToString();
    }
}