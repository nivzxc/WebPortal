using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class BudgetManagement_AccountBudgetAdjustment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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

        ddlBudgetCategory.DataSource = AccountBudgetCategory.GetDSLBudgetCategoryWithoutInitialBudget();
        ddlBudgetCategory.DataValueField = "pvalue";
        ddlBudgetCategory.DataTextField = "ptext";
        ddlBudgetCategory.DataBind();

    }

    public void LoadDetails()
    {
        lblResponsibilityCode.Text = clsRC.GetRCName(Request.QueryString["rccode"]);
        lblAccountCategory.Text = AccountCategory.GetCategoryName(Convert.ToInt16(Request.QueryString["accnt_cat_code"]));
        lblAccountItem.Text = AccountItems.GetItemName(Convert.ToInt16(Request.QueryString["accnt_items_code"]));
        lblAccountItemName.Text = AccountItems.GetItemName(Convert.ToInt16(Request.QueryString["accnt_items_code"]));
    }

    public void LoadGrid()
    {
        string strWrite = "";
        int intCounter = 0;
        double dblAmountPossitive = 0;
        double dblAmountNegative = 0;
        foreach (DataRow drBudgetItem in AccountBudget.GetDSGforAdjustmentBudget(Request.QueryString["rccode"], Convert.ToInt16(ddlFiscalYear.SelectedValue), Request.QueryString["accnt_items_code"].ToString()).Rows)
        {
            intCounter += 1;
            strWrite += "<tr>";
            strWrite += "<td class='GridRows'>" + intCounter.ToString() + "</td>";
            strWrite += "<td class='GridRows'>" + drBudgetItem["created_on"].ToString() + "</td>";
            strWrite += "<td class='GridRows'>" + drBudgetItem["accnt_bud_cat_name"].ToString() + "</td>";
            strWrite += "<td class='GridRows'>" + drBudgetItem["remarks"].ToString() + "</td>";
            if (drBudgetItem["accnt_bud_cat_code"].ToString() == "04")
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>(" + Convert.ToDouble(drBudgetItem["bud_value"]).ToString("###,##0.00") + ")</td>";
                dblAmountNegative += Convert.ToDouble(drBudgetItem["bud_value"]);
            }
            else
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>" + Convert.ToDouble(drBudgetItem["bud_value"]).ToString("###,##0.00") + "</td>";
                dblAmountPossitive += Convert.ToDouble(drBudgetItem["bud_value"]);
            }
            strWrite += "</tr>";
        }
        strWrite += "<tr><td colspan='4' class='GridRows' style='text-align:right;'>Total</td><td class='GridRows' style='text-align:right;'>" + (dblAmountPossitive - dblAmountNegative).ToString("###,##0.00") + "</td></tr>";
        lblItems.Text = strWrite;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        using (AccountBudget objAccountBudget = new AccountBudget())
        {
            objAccountBudget.AccountBudgetCategoryCode = ddlBudgetCategory.SelectedValue.ToString();
            objAccountBudget.FiscalYearCode = Convert.ToInt16(ddlFiscalYear.SelectedValue);
            objAccountBudget.ResponsibilityCode = Request.QueryString["rccode"].ToString();
            objAccountBudget.ChargeTypeCode = "01";
            objAccountBudget.AccountItemCode = Convert.ToInt16(Request.QueryString["accnt_items_code"]);
            objAccountBudget.BudgetValue = Convert.ToDouble(txtAmount.Text);
            objAccountBudget.Remarks = txtRemarks.Text;
            objAccountBudget.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountBudget.CreatedOn = DateTime.Now;
            objAccountBudget.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountBudget.ModifiedOn = DateTime.Now;
            objAccountBudget.RecordStatus = "1";

            if (objAccountBudget.Insert() > 0)
            {
                this.LoadGrid();
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountBudgetMaintenance.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "");
    }
}