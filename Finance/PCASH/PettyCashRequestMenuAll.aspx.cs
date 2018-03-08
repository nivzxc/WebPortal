using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using STIeForms;

public partial class Finance_PCASH_PettyCashRequestMenuAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnNewRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("PettyCashRequestNew.aspx");
    }

    public string CheckLength(string pProjectTitle)
    {
        string strReturn = "";
        var intLength = 50;
        if ((pProjectTitle.Length > intLength))
        {
            strReturn = pProjectTitle.Substring(0, intLength) + "...";
        }
        else
        {
            strReturn = pProjectTitle;
        }
        return strReturn;
    }
    protected string writeApprover(string pUsername, string pApproverType)
    {
        string strReturn = "";
        if (pApproverType == "Endorser")
        { strReturn = "For Endorsement of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

        if (pApproverType == "Authority")
        { strReturn = "For Authorization of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

        if (pApproverType == "Approved")
        { strReturn = "Approved by: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

        if (pApproverType == "Approval")
        { strReturn = "For Approval of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

        return strReturn;
    }

    protected string writeLink(string pKey, string pDescription, string pLinkType)
    {
        string strReturn = "";
        if (pLinkType == "Edit")
        {
            strReturn = "<a href='PettyCashRequestEditRequest.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>";
        }

        if (pLinkType == "Print")
        { strReturn = "<a href='PettyCashPrint.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }

        if (pLinkType == "None")
        { strReturn = "<a href='PettyCashRequestDetails.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }

        if (pLinkType == "Approver")
        { strReturn = "<a href='PettyCashRequestDetailsApprover.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }


        return strReturn;

    }

    protected string writeImageLink(string pKey, string pLinkType, string pImage)
    {
        string strReturn = "";
        if (pLinkType == "Edit")
        {
            strReturn = "<a href='PettyCashRequestEditRequest.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>";
        }

        if (pLinkType == "Print")
        { strReturn = "<a href='PettyCashPrint.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        if (pLinkType == "None")
        { strReturn = "<a href='PettyCashRequestDetails.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        if (pLinkType == "Approver")
        { strReturn = "<a href='PettyCashRequestDetailsApprover.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        return strReturn;
    }

    protected void LoadMenuPCASUser()
    {
        if (!IsPostBack)
        {
            string strWhere = "";

            string strWrite = "";
            string strApprovers = "";
            string strImage = "";
            string strLink = "";
            int intCtr = 0;

            int intStart = 1;
            int intEnd = 10000;

            DataTable tblPCAS = clsPCASRequest.GetDSGMainFormPerUser(" (pcascode), dateneed, reason, isexecut, reqstdby, pclscode,obcode,pctpcode,schlcode,rccode,others,pcasstat,createby, createon", Request.Cookies["Speedo"]["UserName"], strWhere, intStart, intEnd);
            foreach (DataRow drw in tblPCAS.Rows)
            {
                //strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Approved");

                foreach (DataRow drwApprover in clsPCASApproval.GetDSGMainForm(drw["pcascode"].ToString()).Rows)
                {
                    if (drwApprover["pcasstat"].ToString() == "0")
                    {
                        strApprovers = strApprovers + writeApprover(drwApprover["username"].ToString(), "Approval") + "<br>";
                    }
                    else if (drwApprover["pcasstat"].ToString() == "1")
                    {
                        strApprovers = strApprovers + writeApprover(drwApprover["username"].ToString(), "Approved") + "<br>";
                    }
                }

                strImage = writeImageLink(drw["pcascode"].ToString().Trim(), "None", clsPCASRequest.GetRequestStatusIcon("2"));
                strLink = writeLink(drw["pcascode"].ToString().Trim(), drw["reason"].ToString().Trim(), "None");
                strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + strImage + "</td>" +
                                        "<td class='GridRows'>" +
                                          strLink +
                                         "<br>PCAS Code: " + drw["pcascode"].ToString().Trim() +
                                         "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
                                         "<br>Request For: " + drw["reason"] +
                                         "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
                                         "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeed"]).ToString("MMMM dd, yyyy") +
                                        "</td>" +
                                       "<td class='GridRows'>" + strApprovers + "</td>" +
                                       "</tr>";
                intCtr++;
                strLink = "";
                strApprovers = "";
            }

            Response.Write(strWrite);
            if (intCtr == 0)
                Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
            else
                Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
        }
    }

    protected void LoadMenuPCASApprover()
    {
        if (!IsPostBack)
        {
            string strWhere = "";
            string strApprovers = "";
            string strWrite = "";
            string strImage = "";
            string strLink = "";
            int intCtr = 0;

            int intStart = 1;
            int intEnd = 10000;

            DataTable tblPCAS = clsPCASRequest.GetDSGMainFormPerApprover(" (pcascode), dateneed, reason, isexecut, reqstdby, pclscode,obcode,pctpcode,schlcode,rccode,others,pcasstat,createby, createon", Request.Cookies["Speedo"]["UserName"], strWhere, intStart, intEnd);
            foreach (DataRow drw in tblPCAS.Rows)
            {

                strImage = writeImageLink(drw["pcascode"].ToString().Trim(), "Approver", clsPCASRequest.GetRequestStatusIcon("2"));
                strLink = writeLink(drw["pcascode"].ToString().Trim(), drw["reason"].ToString().Trim(), "Approver");
                strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + strImage + "</td>" +
                                        "<td class='GridRows'>" +
                                          strLink +
                                         "<br>PCAS Code: " + drw["pcascode"].ToString().Trim() +
                                         "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
                                         "<br>Request For: " + drw["reason"] +
                                         "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
                                         "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeed"]).ToString("MMMM dd, yyyy") +
                                        "</td>" +
                                       "</tr>";
                intCtr++;
                strLink = "";
                strApprovers = "";
            }

            Response.Write(strWrite);
            if (intCtr == 0)
                Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
            else
                Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
        }
    }
}