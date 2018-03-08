using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class EmployeeJournal_EmployeeJournalViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadEmployeeJOurnal();
    }

    protected void LoadEmployeeJOurnal()
    {
        int intEmployeeJournalCode = 0;

        intEmployeeJournalCode = Convert.ToInt16(Request.QueryString["JournalCode"]);

        //using (EmployeeJournal objJournal = new EmployeeJournal())
        //{
        //    objJournal.EmployeeJournalCode = intEmployeeJournalCode;
        //    objJournal.Fill2();
        //    DateTime firstDayOfWeek = clsDateTime.FirstDateOfWeek(Convert.ToInt16(objJournal.WeekYear), objJournal.WeekNumber, CultureInfo.CurrentCulture);
        //    litContent.Text += "<div class='post-title'><h2><font style='font-size:25px; font-weight:bold;'>Journal: " + firstDayOfWeek.ToString("MM/dd/yyyy") + " - " + firstDayOfWeek.AddDays(6).ToString("MM/dd/yyyy") + "</font></h2></div>";
        //    litContent.Text += "<div class='post-date'><font style='font-size:11px;'>Created On: " + objJournal.CreatedOn.ToString("dddd, MMM dd, yyyy") + "</font></div>";
        //    litContent.Text += "<div class='post-date'><font style='font-size:11px;'>Last Modified On: " + objJournal.ModifiedOn.ToString("dddd, MMM dd, yyyy") + "</font></div>";
        //    litContent.Text += "<font style='font-size:14px;'>" + objJournal.Contents + "</font>";

        //}
    }
}