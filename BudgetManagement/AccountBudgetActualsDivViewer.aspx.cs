using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;

public partial class BudgetManagement_AccountBudgetActualsDivViewer : System.Web.UI.Page
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

        Response.Redirect("AccountBudgetDivisionViewer.aspx?rccode=" + Request.QueryString["rccode"].ToString() + "");
    }
}