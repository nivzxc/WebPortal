using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class BudgetManagement_AccountBudgetAdjustmentViewer : System.Web.UI.Page
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

    }

    public void LoadDetails()
    {
        lblResponsibilityCode.Text = clsRC.GetRCName(Request.QueryString["rccode"]);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountBudgetDepartmentViewer.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "");
    }
}