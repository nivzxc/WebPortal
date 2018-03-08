using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BudgetManagement_AccountItemsMaintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDLLs();
            this.LoadList();
        }
    }

    public void LoadDLLs()
    {
        ddlCategory.DataSource = AccountCategory.GetDSLAccountCategory();
        ddlCategory.DataValueField = "pvalue";
        ddlCategory.DataTextField = "ptext";
        ddlCategory.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (AccountItems objAccountItems = new AccountItems())
        {
            objAccountItems.AccountItemsName = txtAccountItemName.Text;
            objAccountItems.AccountCategoryCode = Convert.ToInt16(ddlCategory.SelectedValue);
            objAccountItems.OracleCode = txtOracleCode.Text;
            objAccountItems.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountItems.CreatedOn = DateTime.Now;
            objAccountItems.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountItems.ModifiedOn = DateTime.Now;
            objAccountItems.RecordStatus = "1";
            if (objAccountItems.Insert() > 0)
            {
                this.LoadList();
            }
        }
    }

    public void LoadList()
    {
        lblItems.Text = "";
        string strResult = "";
        foreach (DataRow drAccountItems in AccountItems.GetDSG().Rows)
        {
            strResult += "<tr>";
            strResult += "<td class='GridRows'><a href='AccountItemsMaintenanceModify.aspx?accnt_items_code=" + drAccountItems["accnt_items_code"].ToString() + "&rccode=" + Request.QueryString["rccode"] + "'>" + drAccountItems["oracle_code"] + "</a></td>";
            strResult += "<td class='GridRows'>" + drAccountItems["accnt_items_name"] + "</td>";
            strResult += "<td class='GridRows'>" + drAccountItems["accnt_cat_name"] + "</td>";
            strResult += "<td class='GridRows'>" + drAccountItems["record_status_YN"] + "</td>";
            strResult += "</tr>";

        }
        lblItems.Text = strResult;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountBudgetMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
    }
}