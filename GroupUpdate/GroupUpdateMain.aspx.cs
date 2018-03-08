using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using STIeForms;
using HRMS;
using HqWeb.GroupUpdate;

public partial class GroupUpdate_GroupUpdateMain : System.Web.UI.Page
{
    protected void ValidateUser()
    {
        string username = Request.Cookies["Speedo"]["UserName"];
        if (!(clsGroupUpdate.HasAccess(username) || clsGroupUpdate.IsGroupApprover(username) || clsGroupUpdate.IsDivisionApprover(username)))
        { Response.Redirect("../AccessDenied.aspx"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        ValidateUser();

        string strUsername = Request.Cookies["Speedo"]["UserName"];


        trApproverDivision.Visible = false;
        trApproverDivisionSpacer.Visible = false;
        trApproverHead.Visible = false;
        trApproverHeadSpacer.Visible = false;
        trEncoder.Visible = false;

        if (clsGroupUpdate.HasAccess(strUsername) || clsGroupUpdate.IsGroupApprover(strUsername) || clsGroupUpdate.IsDivisionApprover(strUsername))
        {

                //group head
                if (clsGroupUpdate.IsGroupApprover(strUsername) && !clsGroupUpdate.IsDivisionApprover(strUsername))
                {
                    //trApproverDivision.Visible = false;
                    //trApproverDivisionSpacer.Visible = false;
                    trApproverHead.Visible = true;
                    trApproverHeadSpacer.Visible = true;
                }

                //division head
                else if (clsGroupUpdate.IsDivisionApprover(strUsername) && !clsGroupUpdate.IsGroupApprover(strUsername))
                {
                    //trApproverDivision.Visible = true;
                    //trApproverDivisionSpacer.Visible = true;
                    trApproverHead.Visible = false;
                    trApproverHeadSpacer.Visible = false;
                }
                else if (clsGroupUpdate.IsDivisionApprover(strUsername) && clsGroupUpdate.IsGroupApprover(strUsername))
                {
                    //trApproverDivision.Visible = true;
                    //trApproverDivisionSpacer.Visible = true;
                    trApproverHead.Visible = true;
                    trApproverHeadSpacer.Visible = true;
                }
                
        }
        else
        {
            { Response.Redirect("../AccessDenied.aspx"); }
        }

        if (clsGroupUpdate.HasAccess(strUsername))
        {
            trEncoder.Visible = true;
        }


    }

    protected void LoadApproverDivision()
    {
        string strWrite = "";
        string strUsername = Request.Cookies["Speedo"]["UserName"];
        DataTable tblForApprovalDH = GroupUpdateApproval.GetDSGForApprovalLevel2(strUsername);
        foreach (DataRow drw in tblForApprovalDH.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                               "<td class='GridRows'>" +
                                                "<a href='GroupUpdateDH.aspx?GroupUpdateCode=" + drw["GroupUpdateCode"].ToString() + "'><img src='../Support/" + clsGroupUpdate.GetRequestStatusIcon(drw["Status"].ToString()) + "' alt='' /></a>" +
                                               "</td>" +
                                               "<td class='GridRows'>" +
                                                "<a href='GroupUpdateDH.aspx?GroupUpdateCode=" + drw["GroupUpdateCode"].ToString() + "' style='font-size:small;'>" + clsString.CutString(drw["Title"].ToString(), 50) + "</a><br>" +
                                                "Sent by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreateBy"].ToString() + "'>" + drw["CreateBy"].ToString() + "</a><br>" +
                                                "Date Created: " + Convert.ToDateTime(drw["CreateOn"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                               "</td>" +
                                              "</tr>";
        }
        Response.Write(strWrite);
        if (tblForApprovalDH.Rows.Count == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblForApprovalDH.Rows.Count + " records found ]</td></tr>");
    }

    protected void LoadApproverHead()
    {
        string strWrite = "";
        string strUsername = Request.Cookies["Speedo"]["UserName"];
        DataTable tblForApprovalGH = GroupUpdateApproval.GetDSGForApprovalLevel1(strUsername);
        foreach (DataRow drw in tblForApprovalGH.Rows)
        {
            strWrite = strWrite + "<tr>" +
                                               "<td class='GridRows'>" +
                                                "<a href='GroupUpdateGH.aspx?GroupUpdateCode=" + drw["GroupUpdateCode"].ToString() + "'><img src='../Support/" + clsGroupUpdate.GetRequestStatusIcon(drw["Status"].ToString()) + "' alt='' /></a>" +
                                               "</td>" +
                                               "<td class='GridRows'>" +
                                                "<a href='GroupUpdateGH.aspx?GroupUpdateCode=" + drw["GroupUpdateCode"].ToString() + "' style='font-size:small;'>" + clsString.CutString(drw["Title"].ToString(), 50) + "</a><br>" +
                                                "Sent by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreateBy"].ToString() + "'>" + drw["CreateBy"].ToString() + "</a><br>" +
                                                "Date Created: " + Convert.ToDateTime(drw["CreateOn"].ToString()).ToString("MMM dd, yyyy hh:mm tt") +
                                               "</td>" +
                                              "</tr>";
        }
        Response.Write(strWrite);
        if (tblForApprovalGH.Rows.Count == 0)
            Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='3' class='GridRows'>[ " + tblForApprovalGH.Rows.Count + " records found ]</td></tr>");
    }

    protected void LoadUpdates()
    {
        string strWrite = "";
        string strStatus = "";
        string strLink = "";
        string strPreview = "";
        foreach (DataRow drNews in clsGroupUpdate.GetDSG(Request.Cookies["Speedo"]["UserName"]).Rows)
        {
            if (drNews["Status"].ToString() == "0")
            {
                strStatus = "For Approval of <a href='../Userpage/UserPage.aspx?username=" + GroupUpdateApproval.ForApproval(drNews["GroupUpdateCode"].ToString()) + "'>" + GroupUpdateApproval.ForApproval(drNews["GroupUpdateCode"].ToString()) + "</a>";
                strLink = "GroupUpdateDetailsMain";
                strPreview = "";
            }
            else if (drNews["Status"].ToString() == "1")
            {
                strStatus = "Approved";
                strLink = "GroupUpdateDetailsMain";
                strPreview = "";
            }
            else if (drNews["Status"].ToString() == "2")
            {

                strStatus = "Disapproved";
                strLink = "GroupUpdateDetailsMain";
                strPreview = "";
            }
            else if (drNews["Status"].ToString() == "3")
            {
                strStatus = "Void";
                strLink = "GroupUpdateDetailsMain";
                strPreview = "";
            }
            else if (drNews["Status"].ToString() == "4")
            {
                strStatus = "For Modification";
                strLink = "GroupUpdateEdit";
                strPreview = "";
            }

            else if (drNews["Status"].ToString() == "5")
            {
                strStatus = "Saved as Draft";
                strLink = "GroupUpdateEdit";
                strPreview = "<a href='GroupUpdatePreview.aspx?GroupUpdateCode=" + drNews["GroupUpdateCode"].ToString() + "' style='font-size:x-small;' target='_blank'>Click for preview</a><br>";
            }

            strWrite = strWrite + "<tr>" +
                                  "<td><img src='../Support/" + clsGroupUpdate.GetRequestStatusIcon(drNews["Status"].ToString()) + "' alt='' /></td>" +
                                  "<td class='GridRows'>" +
                                  "<a href='" + strLink + ".aspx?GroupUpdateCode=" + drNews["GroupUpdateCode"].ToString() + "' style='font-size:small;'>" + clsString.CutString(drNews["Title"].ToString(), 40) + "</a><br>" +
                                  strPreview +
                                  "Submitted by: <a href='../../../Userpage/UserPage.aspx?username=" + drNews["CreateBy"] + "'>" + drNews["CreateBy"] + "</a><br>" +
                                  "Date Created: " + Convert.ToDateTime(drNews["CreateOn"].ToString()).ToString("MMM dd, yyyy") + "</td>" +
                                  "<td class='GridRows'>" + strStatus + "</td>" +
                                  "</tr>";
        }
        Response.Write(strWrite);
    }

    protected void btnNewRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupUpdateAdd.aspx");
    }
}