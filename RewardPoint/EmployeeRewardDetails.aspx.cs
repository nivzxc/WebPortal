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

public partial class RewardPoint_EmployeeRewardDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        using (clsRewardDetail objReward = new clsRewardDetail())
        {
            objReward.EmployeeRewardCode = Request.QueryString["EmployeeRewardCode"].ToInt();
            objReward.Fill();
            lblEventName.Text = clsRewardCategory.GetName(clsReward.GetRewardCategoryCode(objReward.TransactionCode.ToString().ToInt()).ToInt());
            lblDescription.Text = clsReward.GetDescription(objReward.TransactionCode);
            lblPoints.Text = string.Format("{0:n2}", objReward.Points);
            lblCreateBy.Text = clsEmployee.GetName(objReward.CreatedBy);
            lblCreateOn.Text = Convert.ToDateTime(objReward.CreatedOn).ToString("MMMM dd, yyyy");
            lblDateAcquired.Text = Convert.ToDateTime(objReward.DateAcquired).ToString("MMMM dd, yyyy");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeRewardList.aspx");
    }
}