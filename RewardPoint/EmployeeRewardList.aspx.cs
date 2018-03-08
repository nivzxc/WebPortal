using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HRMS;
using HqWeb.Reward;

public partial class RewardPoint_EmployeeRewardList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
    }

    protected void LoadRewards()
    {
        string strWrite = "";
        DataTable tblRewardList = clsRewardDetail.GetDSG(Request.Cookies["Speedo"]["UserName"]);
        string strNegative = "";
        foreach (DataRow drow in tblRewardList.Rows)
        {
            strNegative = drow["IsIncrease"].ToString() == "0" ? "-" : "";
            strWrite = strWrite + "<tr>" +
                                   "<td class='GridRows'><a href='EmployeeRewardDetails.aspx?EmployeeRewardCode=" + drow["EmployeeRewardCode"].ToString() + "'><img src='../Support/find24.png' alt='' /></a></td>" +
                                   "<td class='GridRows' style='text-align:left;'>" + drow["RewardCategoryName"].ToString() + "</td>" +
                                   "<td class='GridRows' style='text-align:left;'>" + clsString.CutString(drow["Description"].ToString(),50) + "</td>" +
                                   "<td class='GridRows' style='text-align:left;'>" + Convert.ToDateTime(drow["DateAcquired"]).ToString("MMMM dd, yyyy") + "</td>" +
                                     "<td class='GridRows' style='text-align:right;'>" + strNegative + string.Format("{0:n2}", double.Parse(drow["Points"].ToString())) + "</td>" +                                
                                   "</tr>";
        }

        //display total
        strWrite = strWrite + "<tr>" +
                                   "<td class='GridRows' style='text-align:right;' colspan='4'><b>Total Accumulated Points:</b></td>" +
                                   "<td class='GridRows' style='text-align:right;'><b>" + string.Format("{0:n2}", clsRewardDetail.GetPoints(Request.Cookies["Speedo"]["UserName"])) + "</b></td>" +
                                  "</tr>";

        Response.Write(strWrite);
    }
}