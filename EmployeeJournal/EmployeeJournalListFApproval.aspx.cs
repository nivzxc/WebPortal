using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeJournal_EmployeeJournalListFApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
    }
    protected void LoadUpdates()
    {
        string strWrite = "";
        string strStatus = "";
        string strLink = "";
        string strPreview = "";
        foreach (DataRow drJournal in EmployeeJournal.GetDSGForApproval(Request.Cookies["Speedo"]["UserName"]).Rows)
        {

                strLink = "JournalViewerA";
                strWrite = strWrite + "<tr>" +
                                      "<td class='GridRows'><img src='../Support/MRCF_View.png' alt='' /></td>" +
                                      "<td class='GridRows'><a href='" + strLink + ".aspx?JournalCode=" + drJournal["JournalCode"].ToString() + "' style='font-size:small;'>" + drJournal["WeekName"].ToString() + " (" + Convert.ToDateTime(drJournal["DateStart"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(drJournal["DateEnd"].ToString()).ToString("MMM dd, yyyy") + ") </a><br>" + 
                                      "Submitted by: " + drJournal["ModifiedBy"].ToString() + "<br />" +
                                      "Submitted On: " + drJournal["ModifiedOn"].ToString() + "<br />" +
                                      "</td>" +
                                      "</tr>";


        }
        Response.Write(strWrite);
    }
    protected void btnGoToJournal_Click(object sender, EventArgs e)
    {
        string strLink = "";
        using (EmployeeJournal objEmployeeJournal = new EmployeeJournal())
        {
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
                objEmployeeJournal.WeekCode = Convert.ToInt16(WeekYear.GetActiveWeekCode());
                objEmployeeJournal.Enabled = "1";
                objEmployeeJournal.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                objEmployeeJournal.CreatedOn = DateTime.Now;
                objEmployeeJournal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
                objEmployeeJournal.ModifiedOn = DateTime.Now;
                if (objEmployeeJournal.Insert() > 0)
                {
                    Response.Redirect("JournalEncoding.aspx");
                }
            }

        }

    }
}