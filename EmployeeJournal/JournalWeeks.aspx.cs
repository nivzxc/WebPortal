using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeJournal_JournalWeeks : System.Web.UI.Page
{

    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("WeekCode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("WeekName", System.Type.GetType("System.String"));
        tblCart.Columns.Add("DateStart", System.Type.GetType("System.DateTime"));
        tblCart.Columns.Add("DateEnd", System.Type.GetType("System.DateTime"));
        //tblCart = WeekYear.GetDSGCart(Convert.ToInt16(FiscalYear.GetActiveYearCode()));
        tblCart = WeekYear.GetDSGCart(Convert.ToInt16(ddlFiscalYear.SelectedValue));
        ViewState["Cart"] = tblCart;

        //LoadGrid();
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
        clsSpeedo.Authenticate();
        //int weeks = 0;
        //DateTime firstdayback = Convert.ToDateTime("2015-04-01");
        ////weeks = DateTime.Now.Subtract(firstdayback).Days / 7;
        //weeks = (Convert.ToDateTime("2015-04-03").Subtract(firstdayback).Days / 7)+1;
        //Response.Write(weeks);

        if (!Page.IsPostBack)
        {
            ddlFiscalYear.DataSource = WeekYear.GetDSLFiscalYearsALL();
            ddlFiscalYear.DataValueField = "pvalue";
            ddlFiscalYear.DataTextField = "ptext";
            ddlFiscalYear.DataBind();

            try
            {
                MakeCart();
            }
            catch { }
        }
        LoadGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (WeekYear objWeekYear = new WeekYear())
        {
            objWeekYear.WeekName = txtWeekName.Text;
            objWeekYear.WeekNumber = Convert.ToInt16(ddlWeekNunmber.SelectedValue.ToString());
            objWeekYear.DateStart = Convert.ToDateTime(dtpFrom.SelectedDate.ToString());
            objWeekYear.DateTo = Convert.ToDateTime(dtpTo.SelectedDate.ToString());
            objWeekYear.FiscalYearCode = Convert.ToInt16(ddlFiscalYear.SelectedValue);
            objWeekYear.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objWeekYear.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            if (objWeekYear.Insert() > 0)
            {
                Response.Redirect("JournalWeeks.aspx");
            }
        }
        
        
    }

    protected void dgSchedule_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            tblCart.Rows[e.Item.ItemIndex].Delete();
            ViewState["Cart"] = tblCart;

            dgSchedule.DataSource = tblCart;
            dgSchedule.DataBind();
        }
        catch
        {
            Response.Redirect("JournalWeeks.aspx");
        }
    }
    protected void ddlFiscalYear_SelectedIndexChanged(object sender, EventArgs e)
    {


        try
        {
            MakeCart();
        }
        catch { }

        LoadGrid();

    }
}