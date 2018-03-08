using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeJournal_JournalViewandDraft : System.Web.UI.Page
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

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        if (!Page.IsPostBack)
        {
            string strDeptDetails = "", strDivDetails = "";
            string strDeptStatus = "", strDivStatus = "";
            lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(EmployeeJournal.GetWeekCode(Convert.ToInt16(Request.QueryString["JournalCode"])))).ToString("MMM dd, yyyy") + ")";
            this.MakeCart();
            this.LoadContent();
            //lblDeptApprover.Text = "(" + EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "1") + ")";
            try
            {
                lblDepartmentHeadsRemarks.Text = EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "1");

                if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(Request.QueryString["JournalCode"]), "1") == "F")
                {
                    strDeptStatus = "For Review";
                }
                else if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(Request.QueryString["JournalCode"]), "1") == "A")
                {
                    strDeptStatus = "Reviewed";
                }
                else if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(Request.QueryString["JournalCode"]), "1") == "F")
                {
                    strDeptStatus = "Disapproved";
                }
                strDeptDetails = "Reviewer: " + EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "1") + "<br />" +
                    "Status: " + strDeptStatus + "<br />" +
                    "Remarks: <br />" + EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "1");
                lblDepartmentHeadsRemarks.Text = strDeptDetails;
            }
            catch { }

            //if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(Request.QueryString["JournalCode"]), "2") == "F")
            //{
            //    strDivStatus = "For Approval";
            //}
            //else if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(Request.QueryString["JournalCode"]), "2") == "A")
            //{
            //    strDivStatus = "Approved";
            //}
            //else if (EmployeeJournalApproval.GetApproverAStatus(Convert.ToInt16(Request.QueryString["JournalCode"]), "2") == "F")
            //{
            //    strDivStatus = "Disapproved";
            //}
            //strDivDetails = "Approver: " + EmployeeJournalApproval.GetApprover(Convert.ToInt16(Request.QueryString["JournalCode"]), "2") + "<br />" +
            //    "Status: " + strDivStatus + "<br />" +
            //    "Remarks: <br />" + EmployeeJournalApproval.GetRemarks(Convert.ToInt16(Request.QueryString["JournalCode"]), "2");
            //lblDivisionHeadsRemarks.Text = strDivDetails;
        }
    }
    public void LoadContent()
    {
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
            //objEmployeeJournal.EmployeeJournalCode = EmployeeJournal.GetLastJournalCode(Convert.ToInt16(WeekYear.GetActiveWeekCode()), Request.Cookies["Speedo"]["UserName"].ToString());
            objEmployeeJournal.EmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);
            objEmployeeJournal.Fill2();
            litContent.Text = objEmployeeJournal.Contents;
            lblSubmittedDetails.Text = objEmployeeJournal.ModifiedBy + " " + "(" + objEmployeeJournal.ModifiedOn + ")";
            //litContent.Text += "<font style='font-size:14px;'>" + objEmployeeJournal.Contents + "</font>";
        }
    }
}