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

public partial class RewardPoint_TransactionMain : System.Web.UI.Page
{
    protected void LoadApproverDivision()
    {
        string strWrite = "";
        DataTable tblTransaction= clsRewardApproval.GetDSGForApprovalLevel2("jay.jamandre");
        foreach (DataRow drw in tblTransaction.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                   "<td class='GridRows'>" +
                                    "<a href='TransactionApprovalDH.aspx?TransactionCode=" + drw["TransactionCode"].ToString() + "'><img src='../Support/" + clsReward.GetRequestStatusIcon(drw["Status"].ToString()) + "' alt='' /></a>" +
                                   "</td>" +
                                   "<td class='GridRows'>" +
                                    "<a href='TransactionApprovalDH.aspx?TransactionCode=" + drw["TransactionCode"].ToString() + "' style='font-size:small;'>" + clsString.CutString(drw["Description"].ToString(), 50) + "</a><br>" +
                                    "Sent by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreateBy"].ToString() + "'>" + drw["CreateBy"].ToString() + "</a><br>" +
                                    "Date Created: " + Convert.ToDateTime(drw["CreateOn"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                   "</td>" +
                                  "</tr>";
        }
        Response.Write(strWrite);
        if (tblTransaction.Rows.Count == 0)
            Response.Write("<tr><td colspan='2' class='GridRows'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='2' class='GridRows'>[ " + tblTransaction.Rows.Count + " records found ]</td></tr>");
    }

    protected void LoadApproverHead()
    {
        string strWrite = "";
        DataTable tblTransaction = clsRewardApproval.GetDSGForApprovalLevel1("liezel.diego");
        foreach (DataRow drw in tblTransaction.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                               "<td class='GridRows'>" +
                                                "<a href='TransactionApprovalGH.aspx?TransactionCode=" + drw["TransactionCode"].ToString() + "'><img src='../Support/" + clsReward.GetRequestStatusIcon(drw["Status"].ToString()) + "' alt='' /></a>" +
                                               "</td>" +
                                               "<td class='GridRows'>" +
                                                "<a href='TransactionApprovalGH.aspx?TransactionCode=" + drw["TransactionCode"].ToString() + "' style='font-size:small;'>" + clsString.CutString(drw["Description"].ToString(), 50) + "</a><br>" +
                                                "Sent by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreateBy"].ToString() + "'>" + drw["CreateBy"].ToString() + "</a><br>" +
                                                "Date Created: " + Convert.ToDateTime(drw["CreateOn"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                               "</td>" +
                                              "</tr>";
        }
        Response.Write(strWrite);
        if (tblTransaction.Rows.Count == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblTransaction.Rows.Count + " records found ]</td></tr>");
    }

    protected void LoadEncoder()
    {
        string strWrite = "";
        string strStatus = "";
        string strLink = "";
        DataTable tblTransaction = clsRewardApproval.GetDSGTransaction(Request.Cookies["Speedo"]["UserName"]);
        foreach (DataRow drw in tblTransaction.Rows)
        {
            if (drw["Status"].ToString() == "0")
            {
                strStatus = "For Approval";
                strLink = "TransactionDetails";
            }
            else if (drw["Status"].ToString() == "1")
            {
                strStatus = "Approved";
                strLink = "TransactionDetails";
            }
            else if (drw["Status"].ToString() == "2")
            {

                strStatus = "Disapproved";
                strLink = "TransactionDetails";
            }
            else if (drw["Status"].ToString() == "3")
            {
                strStatus = "Void";
                strLink = "TransactionDetails";
            }
            else if (drw["Status"].ToString() == "4")
            {
                strStatus = "For Modification";
                strLink = "TransactionEdit";
            }


            strWrite = strWrite + "<tr>" +
                                              "<td class='GridRows'>" +
                                               "<a href='" + strLink + ".aspx?TransactionCode=" + drw["TransactionCode"].ToString() + "'><img src='../Support/" + clsReward.GetRequestStatusIcon(drw["Status"].ToString()) + "' alt='' /></a>" +
                                              "</td>" +
                                              "<td class='GridRows'>" +
                                               "<a href='" + strLink + ".aspx?TransactionCode=" + drw["TransactionCode"].ToString() + "' style='font-size:small;'>" + clsString.CutString(drw["Description"].ToString(), 50) + "</a><br>" +
                                               "Sent by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreateBy"].ToString() + "'>" + drw["CreateBy"].ToString() + "</a><br>" +
                                               "Date Created: " + Convert.ToDateTime(drw["CreateOn"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                              "</td>" +
                                   "<td class='GridRows'>" + strStatus + "</td>" +
                                  "</tr>";
        }
        Response.Write(strWrite);
        if (tblTransaction.Rows.Count == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblTransaction.Rows.Count + " records found ]</td></tr>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string strUsername = Request.Cookies["Speedo"]["UserName"];

        if (clsSystemModule.HasAccess("REWARD", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            trApproverDivision.Visible = false;
            trApproverDivisionSpacer.Visible = false;
            trApproverHead.Visible = false;
            trApproverHeadSpacer.Visible = false;

            if (strUsername == "liezel.diego")
            {
                trApproverDivision.Visible = false;
                trApproverDivisionSpacer.Visible = false;
                trApproverHead.Visible = true;
                trApproverHeadSpacer.Visible = true;
            }
            else if (strUsername == "jay.jamandre")
            {
                trApproverDivision.Visible = true;
                trApproverDivisionSpacer.Visible = true;
                trApproverHead.Visible = false;
                trApproverHeadSpacer.Visible = false;
            }
        }
        else
        {
            { Response.Redirect("../AccessDenied.aspx"); }
        }
    }
    protected void btnNewRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransactionAdd.aspx");
    }
}