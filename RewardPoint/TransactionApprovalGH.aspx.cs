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

public partial class RewardPoint_TransactionApprovalGH : System.Web.UI.Page
{
    protected void LoadDetails()
    {
        string strWrite = "";
        DataTable tblTransaction = clsRewardDetail.GetDSG(Request.QueryString["TransactionCode"].ToInt());
        foreach (DataRow drw in tblTransaction.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                   "<td class='GridRows'>" + drw["Name"].ToString() + "</td>" +
                                   "<td class='GridRows' style='text-align:right'>" + drw["Points"].ToString() + "</td>" +
                                   "<td class='GridRows' style='text-align:center'>" + (drw["IsIncrease"].ToString() == "1" ? "Add" : "Deduct") + "</td>" +
                                   "<td class='GridRows' style='text-align:center'>" + Convert.ToDateTime(drw["DateAcquired"].ToString()).ToString("MMM dd, yyyy") + "</td>" +
                                  "</tr>";
        }
        lblEmployeePointsList.Text = strWrite;

        if (tblTransaction.Rows.Count == 0)
            lblEmployeePointsList.Text = lblEmployeePointsList.Text + "<tr><td colspan='4' class='GridRows'  align='center'>No record found</td></tr>";
        else
            lblEmployeePointsList.Text = lblEmployeePointsList.Text + "<tr><td colspan='4' class='GridRows' align='left'>[ " + tblTransaction.Rows.Count + " records found ]</td></tr>";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            if (clsSystemModule.HasAccess("REWARD", Request.Cookies["Speedo"]["UserName"].ToString()))
            {
                if (clsReward.ApproverLevel1 == strUsername)
                {
                    using (clsReward objDetails = new clsReward())
                    {
                        string strStatus = "";

                        objDetails.TransactionCode = Request.QueryString["TransactionCode"].ToInt();
                        objDetails.Fill();

                        if (objDetails.Status == "0")
                        {
                            strStatus = "For Approval";
                        }

                        else if (objDetails.Status == "1")
                        {
                            strStatus = "Approved";
                        }

                        else if (objDetails.Status == "2")
                        {
                            strStatus = "Disapproved";
                        }
                        else if (objDetails.Status == "3")
                        {
                            strStatus = "Voided";
                        }

                        lblEventName.Text = clsRewardCategory.GetName(clsReward.GetRewardCategoryCode(objDetails.TransactionCode.ToString().ToInt()).ToInt());
                        lblDescription.Text = clsReward.GetDescription(objDetails.TransactionCode);
                        lblCreateBy.Text = objDetails.CreatedBy;
                        lblCreateOn.Text = Convert.ToDateTime(objDetails.CreatedOn).ToString("MMM dd, yyyy");
                        lblStatus.Text = strStatus;
                    }

                    LoadDetails();
                }
                else
                {
                    { Response.Redirect("../AccessDenied.aspx"); }
                }

            }
            else
            {
                { Response.Redirect("../AccessDenied.aspx"); }
            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (clsRewardApproval.ApprovedLevel1(Request.QueryString["TransactionCode"].ToString(), Request.Cookies["Speedo"]["UserName"]) > 0)
        {
            Response.Redirect("TransactionMain.aspx");
        }
    }
    protected void btnDisApprove_Click(object sender, EventArgs e)
    {
        if (clsRewardApproval.DisApprovedLevel1(Request.QueryString["TransactionCode"].ToString(), Request.Cookies["Speedo"]["UserName"]) > 0)
        {
            Response.Redirect("TransactionMain.aspx");
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (clsRewardApproval.Modification(Request.QueryString["TransactionCode"].ToString(), Request.Cookies["Speedo"]["UserName"]) > 0)
        {
            Response.Redirect("TransactionMain.aspx");
        }
    }
}