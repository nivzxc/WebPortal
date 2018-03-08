using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class BudgetManagement_AccountBudgetActuals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.hdnURL.Value = this.Page.Request.UrlReferrer.AbsolutePath.ToString();
            this.LoadDdls();
            this.LoadDetails();
            this.LoadGrid();
        }
    }

    public void LoadDdls()
    {
        ddlFiscalYear.DataSource = FiscalYear.GetDSLAFiscalYears();
        ddlFiscalYear.DataValueField = "pvalue";
        ddlFiscalYear.DataTextField = "ptext";
        ddlFiscalYear.DataBind();

        //ddlBudgetCategory.DataSource = AccountBudgetCategory.GetDSLBudgetCategoryWithoutInitialBudget();
        //ddlBudgetCategory.DataValueField = "pvalue";
        //ddlBudgetCategory.DataTextField = "ptext";
        //ddlBudgetCategory.DataBind();

    }

    public void LoadDetails()
    {
        lblResponsibilityCode.Text = clsRC.GetRCName(Request.QueryString["rccode"]);
        lblAccountCategory.Text = AccountCategory.GetCategoryName(Convert.ToInt16(Request.QueryString["accnt_cat_code"]));
        lblAccountItem.Text = AccountItems.GetItemName(Convert.ToInt16(Request.QueryString["accnt_items_code"]));
        lblAccountItemName.Text = AccountItems.GetItemName(Convert.ToInt16(Request.QueryString["accnt_items_code"]));
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (AccountActual objAccountActual = new AccountActual())
        {
            objAccountActual.FiscalYearCode = Convert.ToInt16(ddlFiscalYear.SelectedValue);
            objAccountActual.ResponsibilityCode = Request.QueryString["rccode"].ToString();
            objAccountActual.ChargeTypeCode = "01";
            objAccountActual.AccountItemCode = Convert.ToInt16(Request.QueryString["accnt_items_code"]);
            objAccountActual.AvailedValue = Convert.ToDouble(txtAmount.Text);
            objAccountActual.Remarks = txtRemarks.Text;
            objAccountActual.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountActual.CreatedOn = DateTime.Now;
            objAccountActual.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountActual.ModifiedOn = DateTime.Now;
            objAccountActual.RecordStatus = "1";
            if (objAccountActual.Insert() > 0)
            {
                this.LoadGrid();
            }
        }
    }

    public void LoadGrid()
    {
        string strWrite = "";
        int intCounter = 0;
        double dblAmountPossitive = 0;
        double dblAmountNegative = 0;
        foreach (DataRow drBudgetItem in AccountActual.GetDSGforActualBudget(Request.QueryString["rccode"], Convert.ToInt16(ddlFiscalYear.SelectedValue), Request.QueryString["accnt_items_code"].ToString()).Rows)
        {
            intCounter += 1;
            strWrite += "<tr>";
            strWrite += "<td class='GridRows'>" + intCounter.ToString() + "</td>";
            strWrite += "<td class='GridRows'>" + drBudgetItem["created_on"].ToString() + "</td>";
            strWrite += "<td class='GridRows'>" + drBudgetItem["remarks"].ToString() + "</td>";
            strWrite += "<td class='GridRows' style='text-align:right;'>" + Convert.ToDouble(drBudgetItem["availed_value"]).ToString("###,##0.00") + "</td>";
            dblAmountPossitive += Convert.ToDouble(drBudgetItem["availed_value"]);
            strWrite += "</tr>";
        }
        strWrite += "<tr><td colspan='3' class='GridRows' style='text-align:right;'>Total</td><td class='GridRows' style='text-align:right;'>" + (dblAmountPossitive).ToString("###,##0.00") + "</td></tr>";
        lblItems.Text = strWrite;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //string requestPage = this.hdnURL.Value.ToString();
        //Response.Redirect(requestPage, true);
        Response.Redirect("AccountBudgetMaintenance.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "");
    }
}