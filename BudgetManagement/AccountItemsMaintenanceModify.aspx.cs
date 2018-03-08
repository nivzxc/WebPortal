using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BudgetManagement_AccountItemsMaintenanceModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.hdnURL.Value = this.Page.Request.UrlReferrer.AbsolutePath.ToString();
            this.LoadDLLs();

            lblAccountItemCode.Text = Request.QueryString["accnt_items_code"].ToString();
            using (AccountItems objAccountItems = new AccountItems())
            {
                objAccountItems.AccountItemsCode = Convert.ToInt16(Request.QueryString["accnt_items_code"]);
                objAccountItems.Fill();
                txtAccountItemName.Text = objAccountItems.AccountItemsName;
                ddlCategory.SelectedValue = objAccountItems.AccountCategoryCode.ToString();
                txtOracleCode.Text = objAccountItems.OracleCode;
                lblCreatedBy.Text = objAccountItems.CreatedBy;
                lblCreatedOn.Text = objAccountItems.CreatedOn.ToString();
                lblModifiedBy.Text = objAccountItems.ModifiedBy;
                lblModifiedOn.Text = objAccountItems.ModifiedOn.ToString();
                chkbEnable.Checked = objAccountItems.RecordStatus == "1" ? true : false;
            }
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
            objAccountItems.AccountItemsCode = Convert.ToInt16(Request.QueryString["accnt_items_code"]);
            objAccountItems.AccountItemsName = txtAccountItemName.Text;
            objAccountItems.AccountCategoryCode=Convert.ToInt16(ddlCategory.SelectedValue);
            objAccountItems.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountItems.OracleCode = txtOracleCode.Text;
            objAccountItems.RecordStatus = chkbEnable.Checked == true ? "1" : "0";
            if (objAccountItems.Update() > 0)
            {
                Response.Redirect("AccountItemsMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
                //string requestPage = this.hdnURL.Value.ToString();
                //Response.Redirect(requestPage, true);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //string requestPage = this.hdnURL.Value.ToString();
        //Response.Redirect(requestPage, true);
        Response.Redirect("AccountItemsMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
    }
}