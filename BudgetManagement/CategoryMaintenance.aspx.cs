using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BudgetManagement_CategoryMaintenance : System.Web.UI.Page
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
        ddlCategoryType.DataSource = AccountCategoryType.GetDSLAccountCategoryType();
        ddlCategoryType.DataValueField = "pvalue";
        ddlCategoryType.DataTextField = "ptext";
        ddlCategoryType.DataBind();
    }

    public void LoadList()
    {
        lblItems.Text = "";
        string strResult = "";
        foreach (DataRow drAccountCategory in AccountCategory.GetDSG().Rows)
        {
            strResult += "<tr>";
            strResult += "<td class='GridRows'><a href='CategoryMaintenanceModify.aspx?accnt_cat_code=" + drAccountCategory["accnt_cat_code"].ToString() + "&rccode=" + Request.QueryString["rccode"] + "'>" + drAccountCategory["accnt_cat_code"] + "</a></td>";
            strResult += "<td class='GridRows'>" + drAccountCategory["accnt_cat_name"] + "</td>";
            strResult += "<td class='GridRows'>" + drAccountCategory["accnt_cat_type_name"] + "</td>";
            strResult += "<td class='GridRows'>" + drAccountCategory["record_status_YN"] + "</td>";
            strResult += "</tr>";

        }
        lblItems.Text = strResult;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (AccountCategory objAccountCategory = new AccountCategory())
        {
            objAccountCategory.AccountCategoryName = txtCategoryName.Text;
            objAccountCategory.AccountCategoryTypeCode = ddlCategoryType.SelectedValue.ToString();
            objAccountCategory.CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountCategory.RecordOrder = Convert.ToInt16(txtRecordOrder.Text);
            objAccountCategory.CreatedOn = DateTime.Now;
            objAccountCategory.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountCategory.ModifiedOn = DateTime.Now;
            objAccountCategory.RecordStatus = "1";
            if (objAccountCategory.Insert() > 0)
            {
                this.LoadList();
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountBudgetMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
    }
}