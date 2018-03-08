using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class BudgetManagement_AccountBudgetMaintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDetails();
            this.LoadDdls();
            this.LoadGrid();
        }
    }

    public void LoadDdls()
    {
        ddlFiscalYear.DataSource = FiscalYear.GetDSLAFiscalYears();
        ddlFiscalYear.DataValueField = "pvalue";
        ddlFiscalYear.DataTextField = "ptext";
        ddlFiscalYear.DataBind();

        ddlAccountCategory.DataSource = AccountCategory.GetDSLAccountCategory();
        ddlAccountCategory.DataValueField = "pvalue";
        ddlAccountCategory.DataTextField = "ptext";
        ddlAccountCategory.DataBind();

        ddlAccountItem.DataSource = AccountItems.GetDSLAccountItem(ddlAccountCategory.SelectedValue.ToString());
        ddlAccountItem.DataValueField = "pvalue";
        ddlAccountItem.DataTextField = "ptext";
        ddlAccountItem.DataBind();
    }

    public void LoadDetails()
    {
        lblResponsibilityCode.Text = clsRC.GetRCName(Request.QueryString["rccode"].ToString());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        using (AccountBudget objAccountBudget = new AccountBudget())
        {
            objAccountBudget.AccountBudgetCategoryCode = "01";
            objAccountBudget.FiscalYearCode = Convert.ToInt16(ddlFiscalYear.SelectedValue);
            objAccountBudget.ResponsibilityCode = Request.QueryString["rccode"].ToString();
            objAccountBudget.ChargeTypeCode = "01";
            objAccountBudget.AccountItemCode = Convert.ToInt16(ddlAccountItem.SelectedValue);
            objAccountBudget.BudgetValue=Convert.ToDouble(txtInitialBudget.Text);
            objAccountBudget.Remarks = txtRemarks.Text;
            objAccountBudget.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountBudget.CreatedOn = DateTime.Now;
            objAccountBudget.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountBudget.ModifiedOn = DateTime.Now;
            objAccountBudget.RecordStatus = "1";
            if (AccountBudget.IsInitialBudgetExist(Convert.ToInt16(ddlFiscalYear.SelectedValue), Request.QueryString["rccode"].ToString(), Convert.ToInt16(ddlAccountItem.SelectedValue)) == false)
            {
                if (objAccountBudget.Insert() > 0)
                {
                    this.LoadGrid();
                }
            }
            else
            {
                divError.Visible = true;
                lblErrMsg.Text = "Budget for the selected account item already initialized.";
            }
        }
    }

    protected void ddlAccountCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        divError.Visible = false;
        ddlAccountItem.DataSource = AccountItems.GetDSLAccountItem(ddlAccountCategory.SelectedValue.ToString());
        ddlAccountItem.DataValueField = "pvalue";
        ddlAccountItem.DataTextField = "ptext";
        ddlAccountItem.DataBind();
    }
    protected void ddlAccountItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void LoadGrid()
    {
        string strWrite = "";
        int intCounter = 0;
        double dblCategoryBudget = 0;
        double dblCategoryAvailed = 0;
        double dblCategoryVariant = 0;
        double dblCategoryTotalBudget = 0;
        double dblCategoryTotalAvailed = 0;
        double dblTotalIncomeBudget = 0;
        double dblTotalIncomeAvailed = 0;
        double dblTotalExpensesBudget = 0;
        double dblTotalExpensesAvailed = 0;
        foreach (DataRow drCategoryType in AccountCategoryType.GetDSG().Rows)
        {
            foreach (DataRow drCategory in AccountCategory.GetDSG(drCategoryType["accnt_cat_type_code"].ToString()).Rows)
            {
                foreach (DataRow drAccountItems in AccountBudget.GetDSGRCBudget(Request.QueryString["rccode"].ToString(), Convert.ToInt16(ddlFiscalYear.SelectedValue), drCategory["accnt_cat_code"].ToString()).Rows)
                {
                    intCounter += 1;
                    strWrite += "<tr>";
                    strWrite += "<td class='GridRows'>" + drAccountItems["oracle_code"] + "</td>";
                    strWrite += "<td class='GridRows'>" + drAccountItems["accnt_items_name"] + "</td>";
                    if (Convert.ToDouble(drAccountItems["bud_value"]) < 0)//negative
                    {
                        strWrite += "<td class='GridRows' style='text-align:right;'><a href='AccountBudgetAdjustment.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "&accnt_cat_code=" + drCategory["accnt_cat_code"].ToString() + "&accnt_items_code=" + drAccountItems["accnt_items_code"] + "'>(" + (Convert.ToDouble(drAccountItems["bud_value"]) * -1).ToString("###,##0.00") + ")</a></td>";
                    }
                    else //positive
                    {
                        strWrite += "<td class='GridRows' style='text-align:right;'><a href='AccountBudgetAdjustment.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "&accnt_cat_code=" + drCategory["accnt_cat_code"].ToString() + "&accnt_items_code=" + drAccountItems["accnt_items_code"] + "'>" + Convert.ToDouble(drAccountItems["bud_value"]).ToString("###,##0.00") + "</a></td>";
                    }
                    if (Convert.ToDouble(drAccountItems["availed_value"]) < 0) //negative
                    {
                        strWrite += "<td class='GridRows' style='text-align:right;'><a href='AccountBudgetActuals.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "&accnt_cat_code=" + drCategory["accnt_cat_code"].ToString() + "&accnt_items_code=" + drAccountItems["accnt_items_code"] + "'>(" + (Convert.ToDouble(drAccountItems["availed_value"]) * -1).ToString("###,##0.00") + ")</a></td>";
                    }
                    else //positive
                    {
                        strWrite += "<td class='GridRows' style='text-align:right;'><a href='AccountBudgetActuals.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "&accnt_cat_code=" + drCategory["accnt_cat_code"].ToString() + "&accnt_items_code=" + drAccountItems["accnt_items_code"] + "'>" + Convert.ToDouble(drAccountItems["availed_value"]).ToString("###,##0.00") + "</a></td>";
                    }
                    if ((Convert.ToDouble(drAccountItems["bud_value"]) - Convert.ToDouble(drAccountItems["availed_value"])) < 0) //negative
                    {
                        strWrite += "<td class='GridRows' style='text-align:right;'>(" + ((Convert.ToDouble(drAccountItems["bud_value"]) - Convert.ToDouble(drAccountItems["availed_value"])) * -1).ToString("###,##0.00") + ")</td>";
                    }
                    else //positive
                    {
                        strWrite += "<td class='GridRows' style='text-align:right;'>" + (Convert.ToDouble(drAccountItems["bud_value"]) - Convert.ToDouble(drAccountItems["availed_value"])).ToString("###,##0.00") + "</td>";
                    }
                    strWrite += "</tr>";
                    strWrite += "<tr>";

                    dblCategoryBudget += Convert.ToDouble(drAccountItems["bud_value"]);
                    dblCategoryAvailed += Convert.ToDouble(drAccountItems["availed_value"]);
                }

                strWrite += "<td class='GridRows2' colspan='2'>&nbsp;&nbsp;" + drCategory["accnt_cat_name"].ToString() + "</td>";
                if (dblCategoryBudget < 0)
                {
                    strWrite += "<td class='GridRows2' style='text-align:right;'>(" + (dblCategoryBudget * -1).ToString("###,##0.00") + ")</td>";
                }
                else
                {
                    strWrite += "<td class='GridRows2' style='text-align:right;'>" + dblCategoryBudget.ToString("###,##0.00") + "</td>";
                }
                if (dblCategoryAvailed < 0)
                {
                    strWrite += "<td class='GridRows2' style='text-align:right;'>(" + (dblCategoryAvailed * -1).ToString("###,##0.00") + ")</td>";
                }
                else
                {
                    strWrite += "<td class='GridRows2' style='text-align:right;'>" + dblCategoryAvailed.ToString("###,##0.00") + "</td>";
                }
                if ((dblCategoryBudget - dblCategoryAvailed) < 0)
                {
                    strWrite += "<td class='GridRows2' style='text-align:right;'>(" + ((dblCategoryBudget - dblCategoryAvailed) * -1).ToString("###,##0.00") + ")</td>";
                }
                else
                {
                    strWrite += "<td class='GridRows2' style='text-align:right;'>" + (dblCategoryBudget - dblCategoryAvailed).ToString("###,##0.00") + "</td>";
                }
                strWrite += "</tr>";

                dblCategoryTotalBudget += dblCategoryBudget;
                dblCategoryTotalAvailed += dblCategoryAvailed;
                dblCategoryBudget = 0;
                dblCategoryAvailed = 0;

                if (drCategoryType["accnt_cat_type_code"].ToString() == "01")
                {
                    dblTotalIncomeBudget += dblCategoryBudget;
                    dblTotalIncomeAvailed += dblCategoryTotalAvailed;
                }
                if (drCategoryType["accnt_cat_type_code"].ToString() == "02")
                {
                    dblTotalExpensesBudget += dblCategoryBudget;
                    dblTotalExpensesAvailed += dblCategoryAvailed;
                }

                //strWrite += "<td class='GridRows2' colspan='2'>Category 1</td>";
                //strWrite += "<td class='GridRows2' style='text-align:right;'>10,000,000</td>";
                //strWrite += "<td class='GridRows2' style='text-align:right;'>5,000,000</td>";
                //strWrite += "<td class='GridRows2' style='text-align:right;'>5,000,000</td>";
                //strWrite += "</tr>";
            }

            strWrite += "<tr>";
            strWrite += "<td class='GridRows' style='text-align:right;' colspan='2'>TOTAL " + drCategoryType["accnt_cat_type_name"].ToString() + "</td>";
            if (dblCategoryTotalBudget < 0)
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>(" + (dblCategoryTotalBudget * -1).ToString("###,##0.00") + ")</td>";
            }
            else
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>" + dblCategoryTotalBudget.ToString("###,##0.00") + "</td>";
            }
            if (dblCategoryTotalAvailed < 0)
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>(" + (dblCategoryTotalAvailed * -1).ToString("###,##0.00") + ")</td>";
            }
            else
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>" + dblCategoryTotalAvailed.ToString("###,##0.00") + "</td>";
            }
            if ((dblCategoryTotalBudget - dblCategoryTotalAvailed) < 0)
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>(" + ((dblCategoryTotalBudget - dblCategoryTotalAvailed) * -1).ToString("###,##0.00") + ")</td>";
            }
            else
            {
                strWrite += "<td class='GridRows' style='text-align:right;'>" + (dblCategoryTotalBudget - dblCategoryTotalAvailed).ToString("###,##0.00") + "</td>";
            }
            strWrite += "</tr>";
            strWrite += "<tr>";

            dblCategoryTotalBudget = 0;
            dblCategoryTotalAvailed = 0;
        }
        lblItems.Text = strWrite;

        //strWrite += "<tr>";
        //strWrite += "<td class='GridRows' style='text-align:right;' colspan='2'>TOTAL</td>";
        //if ((dblTotalIncomeBudget -  dblTotalIncomeAvailed) < 0)
        //{
        //    strWrite += "<td class='GridRows' style='text-align:right;'>(" + ((dblTotalIncomeBudget - dblTotalIncomeAvailed) * -1).ToString("###,##0.00") + ")</td>";
        //}
        //else 
        //{
        //    strWrite += "<td class='GridRows' style='text-align:right;'>" + (dblTotalIncomeBudget - dblTotalIncomeAvailed).ToString("###,##0.00") + "</td>";
        //}
        //if ((dblTotalExpensesBudget - dblTotalExpensesAvailed) < 0)
        //{
        //    strWrite += "<td class='GridRows' style='text-align:right;'>(" + ((dblTotalExpensesBudget - dblTotalExpensesAvailed) * -1).ToString("###,##0.00") + ")</td>";
        //}
        //else
        //{
        //    strWrite += "<td class='GridRows' style='text-align:right;'>" + (dblTotalExpensesBudget - dblTotalExpensesAvailed).ToString("###,##0.00") + "</td>";
        //}
        //if (((dblTotalIncomeBudget - dblTotalIncomeAvailed) - (dblTotalExpensesBudget - dblTotalExpensesAvailed)) < 0)
        //{
        //    strWrite += "<td class='GridRows' style='text-align:right;'>(" + ((((dblTotalIncomeBudget - dblTotalIncomeAvailed) - (dblTotalExpensesBudget - dblTotalExpensesAvailed))) * -1).ToString("###,##0.00") + ")</td>";
        //}
        //else
        //{
        //    strWrite += "<td class='GridRows' style='text-align:right;'>" + (((dblTotalIncomeBudget - dblTotalIncomeAvailed) - (dblTotalExpensesBudget - dblTotalExpensesAvailed))).ToString("###,##0.00") + "</td>";
        //}
        //strWrite += "</tr>";
        //strWrite += "<tr>";

        //lblItems.Text = strWrite;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountBudgetList.aspx");
    }

    protected void btnNewAccountCategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("CategoryMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
    }
    protected void btnNewAccountItems_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountItemsMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountBudgetMaintenanceExcel.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "&fiscal_year_code=" + ddlFiscalYear.SelectedValue.ToString() + "");
    }
}