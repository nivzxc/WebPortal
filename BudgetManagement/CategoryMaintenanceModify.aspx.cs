using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BudgetManagement_CategoryMaintenanceModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.hdnURL.Value = this.Page.Request.UrlReferrer.AbsolutePath.ToString();
            lblAccountCategoryCode.Text = Request.QueryString["accnt_cat_code"].ToString();
            this.LoadDLLs();
            using (AccountCategory objAccountCategory = new AccountCategory())
            {
                objAccountCategory.AccountCateogryCode = Convert.ToInt16(Request.QueryString["accnt_cat_code"]);
                objAccountCategory.Fill();
                txtAccountCategoryName.Text = objAccountCategory.AccountCategoryName;
                ddlCategoryType.SelectedValue = objAccountCategory.AccountCategoryTypeCode.ToString();
                txtRecordOrder.Text = objAccountCategory.RecordOrder.ToString();
                lblCreatedBy.Text = objAccountCategory.CreatedBy;
                lblCreatedOn.Text = objAccountCategory.CreatedOn.ToString();
                lblModifiedBy.Text = objAccountCategory.ModifiedBy;
                lblModifiedOn.Text = objAccountCategory.ModifiedOn.ToString();
                chkbEnable.Checked = objAccountCategory.RecordStatus == "1" ? true : false;
            }
        }

    }

    public void LoadDLLs()
    {
        ddlCategoryType.DataSource = AccountCategoryType.GetDSLAccountCategoryType();
        ddlCategoryType.DataValueField = "pvalue";
        ddlCategoryType.DataTextField = "ptext";
        ddlCategoryType.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //string requestPage = this.hdnURL.Value.ToString();
        //Response.Redirect(requestPage, true);
        Response.Redirect("CategoryMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (AccountCategory objAccountCategory = new AccountCategory())
        {
            objAccountCategory.AccountCateogryCode = Convert.ToInt16(Request.QueryString["accnt_cat_code"]);
            objAccountCategory.AccountCategoryName = txtAccountCategoryName.Text;
            objAccountCategory.AccountCategoryTypeCode = ddlCategoryType.SelectedValue.ToString();
            objAccountCategory.RecordOrder = Convert.ToInt16(txtRecordOrder.Text);
            objAccountCategory.ModifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
            objAccountCategory.RecordStatus = chkbEnable.Checked == true ? "1" : "0";
            if (objAccountCategory.Update() > 0)
            {
                Response.Redirect("CategoryMaintenance.aspx?rccode=" + Request.QueryString["rccode"] + "");
                //string requestPage = this.hdnURL.Value.ToString();
                //Response.Redirect(requestPage, true);
            }
        }
    }
}