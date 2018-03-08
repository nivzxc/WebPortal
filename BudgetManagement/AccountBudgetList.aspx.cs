using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using System.Data;

public partial class BudgetManagement_AccountBudgetList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDdls();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //this.LoadList();
    }
    public void LoadDdls()
    {
        ddlFiscalYear.DataSource = FiscalYear.GetDSLAFiscalYears();
        ddlFiscalYear.DataValueField = "pvalue";
        ddlFiscalYear.DataTextField = "ptext";
        ddlFiscalYear.DataBind();

        ddlDivision.DataSource = clsDivision.GetDdlDs();
        ddlDivision.DataValueField = "pvalue";
        ddlDivision.DataTextField = "ptext";
        ddlDivision.DataBind();
    }
    public void LoadList()
    {
        foreach (DataRow drAccountBudget in AccountBudget.GetDSG(ddlDivision.SelectedValue.ToString(),Convert.ToInt16(ddlFiscalYear.SelectedValue)).Rows)
        {
            Response.Write("<tr>");
            Response.Write("<td class='GridRows'><a href='AccountBudgetMaintenance.aspx?rccode=" + drAccountBudget["rccode"] + "'>" + drAccountBudget["rccode"] + "</td>");
            Response.Write("<td class='GridRows'>" + drAccountBudget["rcname"] + "</td>");
            if (Convert.ToDouble(drAccountBudget["bud_value"]) < 0)
            {
                Response.Write("<td class='GridRows' style='text-align:right;'>(" + (Convert.ToDouble(drAccountBudget["bud_value"])*-1).ToString("###,##0.00") + ")</td>");
            }
            else
            {
                Response.Write("<td class='GridRows' style='text-align:right;'>" + Convert.ToDouble(drAccountBudget["bud_value"]).ToString("###,##0.00") + "</td>");
            }
            if (Convert.ToDouble(drAccountBudget["availed_value"]) < 0)
            {
                Response.Write("<td class='GridRows' style='text-align:right;'>(" + (Convert.ToDouble(drAccountBudget["availed_value"])*-1).ToString("###,##0.00") + ")</td>");
            }
            else
            {
                Response.Write("<td class='GridRows' style='text-align:right;'>" + Convert.ToDouble(drAccountBudget["availed_value"]).ToString("###,##0.00") + "</td>");
            }
            if ((Convert.ToDouble(drAccountBudget["bud_value"]) - Convert.ToDouble(drAccountBudget["availed_value"])) < 0)
            {
                Response.Write("<td class='GridRows' style='text-align:right;'>(" + ((Convert.ToDouble(drAccountBudget["bud_value"]) - Convert.ToDouble(drAccountBudget["availed_value"]))*-1).ToString("###,##0.00") + ")</td>");
            }
            else
            {
                Response.Write("<td class='GridRows' style='text-align:right;'>" + (Convert.ToDouble(drAccountBudget["bud_value"]) - Convert.ToDouble(drAccountBudget["availed_value"])).ToString("###,##0.00") + "</td>");            
            }
            Response.Write("</tr>");
            //Response.Write("<tr>");
        }
        //Response.Write("<tr>");
        //Response.Write("<td class='GridRows'>1</td>");
        //Response.Write("<td class='GridRows'>Management Information Systems</td>");
        //Response.Write("<td class='GridRows' style='text-align:right;'>10,000,000</td>");
        //Response.Write("<td class='GridRows' style='text-align:right;'>5,000,000</td>");
        //Response.Write("<td class='GridRows' style='text-align:right;'>5,000,000</td>");
        //Response.Write("</tr>");
        //Response.Write("<tr>");

        //Response.Write("<td class='GridRows2' colspan='2'>Category 1</td>");
        //Response.Write("<td class='GridRows2' style='text-align:right;'>10,000,000</td>");
        //Response.Write("<td class='GridRows2' style='text-align:right;'>5,000,000</td>");
        //Response.Write("<td class='GridRows2' style='text-align:right;'>5,000,000</td>");
        //Response.Write("</tr>");
    }
}