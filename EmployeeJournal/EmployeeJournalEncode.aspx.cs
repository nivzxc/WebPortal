using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class EmployeeJournal_EmployeeJournalEncode : System.Web.UI.Page
{
    //private int _intThisWeekNumber = clsDateTime.GetIso8601WeekOfYear(DateTime.Today);
    private int _intThisWeekCode = Convert.ToInt16(WeekYear.GetActiveWeekCode());
    protected void Page_Load(object sender, EventArgs e)
    {
        //DateTime firstDayOfWeek = clsDateTime.FirstDateOfWeek(DateTime.Now.Year, _intThisWeekCode, CultureInfo.CurrentCulture);
        //lblWeekDates.Text = firstDayOfWeek.ToString("MM/dd/yyyy") + " - " + firstDayOfWeek.AddDays(6).ToString("MM/dd/yyyy");

        lblWeekDates.Text = WeekYear.GetWeekName(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString() + " (" + WeekYear.GetDateStart(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + " - " + WeekYear.GetDateEnd(Convert.ToInt16(WeekYear.GetActiveWeekCode())).ToString("MMM dd, yyyy") + ")";

        //if (EmployeeJournal.HasExistingJournal(Request.Cookies["Speedo"]["UserName"].ToString(), _intThisWeekCode))
        //{
        //    using (EmployeeJournal objEmployeeJouurnal = new EmployeeJournal())
        //    {
        //        objEmployeeJouurnal.WeekCode = _intThisWeekCode;
        //        objEmployeeJouurnal.WeekYear = DateTime.Now.Year.ToString();
        //        objEmployeeJouurnal.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
        //        objEmployeeJouurnal.Fill();
        //        ckeContents.Text = objEmployeeJouurnal.Contents;

        //    }
        //}
    }

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        //using (EmployeeJournal objEmployeeJouurnal = new EmployeeJournal())
        //{
        //    objEmployeeJouurnal.WeekCode = _intThisWeekCode;
        //    objEmployeeJouurnal.WeekYear = DateTime.Now.Year.ToString();
        //    objEmployeeJouurnal.Contents = ckeContents.Text;
        //    objEmployeeJouurnal.Enabled = "1";

        //    if (EmployeeJournal.HasExistingJournal(Request.Cookies["Speedo"]["UserName"].ToString(), _intThisWeekCode, DateTime.Now.Year.ToString()))
        //    {
        //        objEmployeeJouurnal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
        //        objEmployeeJouurnal.ModifiedOn = DateTime.Now;
        //        if (objEmployeeJouurnal.Update() > 0)
        //        {
        //            Response.Redirect("EmployeeJournalList.aspx");
        //        }
        //    }
        //    else
        //    {
        //        objEmployeeJouurnal.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
        //        objEmployeeJouurnal.CreatedOn = DateTime.Now;
        //        objEmployeeJouurnal.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
        //        objEmployeeJouurnal.ModifiedOn = DateTime.Now;
        //        if (objEmployeeJouurnal.Insert() > 0)
        //        {
        //            Response.Redirect("EmployeeJournalList.aspx");
        //        }
        //    }
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}