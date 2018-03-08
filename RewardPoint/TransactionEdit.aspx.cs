using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using System.Text.RegularExpressions;
using HRMS;
using STIeForms;
using Oracles;
using HqWeb.Reward;

public partial class RewardPoint_TransactionEdit : System.Web.UI.Page
{
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("Username", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Points", System.Type.GetType("System.Double"));
        tblCart.Columns.Add("IsIncrease", System.Type.GetType("System.String"));
        tblCart.Columns.Add("DateAcquired", System.Type.GetType("System.String"));
        ViewState["Cart"] = tblCart;
    }

    protected void LoadDdl()
    {
        ddlUsername.DataSource = clsEmployee.DSLEmployeeList();
        ddlUsername.DataValueField = "pvalue";
        ddlUsername.DataTextField = "ptext";
        ddlUsername.DataBind();

        ddlEvent.DataSource = clsRewardCategory.GetDSL();
        ddlEvent.DataValueField = "pValue";
        ddlEvent.DataTextField = "pText";
        ddlEvent.DataBind();
    }


    protected void LoadTrasactionList()
    {
        DataTable tblTransactionList = clsRewardDetail.GetDSGEdit(hdnTransactionCode.Value.ToInt());
        ViewState["Cart"] = tblTransactionList;

        dgItems.DataSource = tblTransactionList;
        dgItems.DataBind();
    }

    protected void LoadDetails()
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            if (clsSystemModule.HasAccess("REWARD", Request.Cookies["Speedo"]["UserName"].ToString()))
            {
                MakeCart();
                LoadDdl();
                dtpDateAcquired.MinDate = DateTime.Now;
                using (clsReward objDetails = new clsReward())
                {
                    hdnTransactionCode.Value = Request.QueryString["TransactionCode"].ToString();
                    objDetails.TransactionCode = Request.QueryString["TransactionCode"].ToInt();
                    objDetails.Fill();
                    ddlEvent.SelectedValue = objDetails.RewardCategoryCode.ToString(); ;
                    txtDescription.Text = objDetails.Description;
                }


                LoadTrasactionList();
            }
            else
            {
                { Response.Redirect("../AccessDenied.aspx"); }
            }
        }
    }

    protected void btnAddNewItem_Click(object sender, EventArgs e)
    {
        DataTable tblCart = ViewState["Cart"] as DataTable;
        if (!IsUsernameExist(tblCart, ddlUsername.SelectedValue.ToString()))
        {
            DataRow drowCart = tblCart.NewRow();
            drowCart["Username"] = ddlUsername.SelectedValue.ToString();
            drowCart["Points"] = txtPoints.Text;
            drowCart["IsIncrease"] = ddlType.SelectedValue.ToString();
            drowCart["DateAcquired"] = dtpDateAcquired.Date;

            tblCart.Rows.Add(drowCart);

            ViewState["Cart"] = tblCart;
            dgItems.DataSource = tblCart;
            dgItems.DataBind();
        }
    }

    protected bool IsUsernameExist(DataTable pUsers, string pUsername)
    {
        bool blnReturn = false;
        DataRow[] drCheck = pUsers.Select("Username='" + pUsername + "'");
        if (drCheck.Length > 0)
        {
            blnReturn = true;
        }
        return blnReturn;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strUsername = Request.Cookies["Speedo"]["UserName"];
        divError.Visible = false;
        DataTable tblCart = ViewState["Cart"] as DataTable;
        if (tblCart.Rows.Count > 0)
        {
            using (clsReward objReward = new clsReward())
            {
                objReward.TransactionCode = hdnTransactionCode.Value.ToInt();
                objReward.Description = txtDescription.Text;
                objReward.RewardCategoryCode = ddlEvent.SelectedValue.ToString().ToInt();
                objReward.Status = "0";
                objReward.ModifyBy = strUsername;
                if (objReward.Update(tblCart) > 0)
                {
                    Response.Redirect("TransactionMain.aspx");
                }
            }
        }
        else
        {
            divError.Visible = true;
            lblErrMsg.Text = "You need to include at least one item to request. Make sure to click <b>Add New Item</b> button to include your requested item.";
        }
    }

    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            tblCart.Rows[e.Item.ItemIndex].Delete();
            tblCart.AcceptChanges();
            ViewState["Cart"] = tblCart;

            dgItems.DataSource = tblCart;
            dgItems.DataBind();

        }
        catch
        {
            Response.Redirect("TransactionMain.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionMain.aspx");
    }
}