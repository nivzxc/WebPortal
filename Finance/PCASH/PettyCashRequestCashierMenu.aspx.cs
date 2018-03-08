using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using STIeForms;

public partial class Finance_PCASH_PettyCashRequestCashierMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
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
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }

        if (pLinkType == "None")
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }

        if (pLinkType == "Approver")
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }

        if (pLinkType == "Approved")
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }


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
        //{ strReturn = "<a href='PCASReport.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        if (pLinkType == "None")
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        if (pLinkType == "Approver")
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        if (pLinkType == "Approved")
        { strReturn = "<a href='PettyCashRequestCashierDetails.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }


        return strReturn;
    }

    protected void LoadMenuPCASForApproval()
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
            int intEnd = 10;

            DataTable tblPCAS = clsPCASRequest.GetDSGMainFormCashier("F", Request.Cookies["Speedo"]["UserName"].ToString());
            foreach (DataRow drw in tblPCAS.Rows)
            {

                strImage = writeImageLink(drw["pcascode"].ToString().Trim(), "Approver", clsPCASRequest.GetRequestStatusIcon("2"));
                strLink = writeLink(drw["pcascode"].ToString().Trim(), drw["reason"].ToString().Trim(), "Approver");
                strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + strImage + "</td>" +
                                        "<td class='GridRows'>" +
                                          strLink +
                                         "<br>PCAS #: " + drw["pcascode"].ToString().Trim() +
                                         "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
                                         "<br>Request For: " + drw["reason"] +
                                         "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
                                         "<br>Date fund needed: " + Convert.ToDateTime(drw["DateNeed"]).ToString("MMMM dd, yyyy") +
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


    protected void LoadMenuPCASApprove()
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
            int intEnd = 10;

            DataTable tblPCAS = clsPCASRequest.GetDSGMainFormCashier("I", Request.Cookies["Speedo"]["UserName"].ToString());
            foreach (DataRow drw in tblPCAS.Rows)
            {

                strImage = writeImageLink(drw["pcascode"].ToString().Trim(), "Print", clsPCASRequest.GetRequestStatusIcon("1"));
                strLink = writeLink(drw["pcascode"].ToString().Trim(), drw["reason"].ToString().Trim(), "Approved");
                strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + strImage + "</td>" +
                                        "<td class='GridRows'>" +
                                          strLink +
                                         "<br>PCAS #: " + drw["pcascode"].ToString().Trim() +
                                         "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
                                         "<br>Request For: " + drw["reason"] +
                                         "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
                                         "<br>Date fund needed: " + Convert.ToDateTime(drw["DateNeed"]).ToString("MMMM dd, yyyy") +
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

    protected void LoadMenuPCASReady()
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
            int intEnd = 10;

            DataTable tblPCAS = clsPCASRequest.GetDSGMainFormCashier("R", Request.Cookies["Speedo"]["UserName"].ToString());
            foreach (DataRow drw in tblPCAS.Rows)
            {

                strImage = writeImageLink(drw["pcascode"].ToString().Trim(), "Approver", clsPCASRequest.GetRequestStatusIcon("2"));
                strLink = writeLink(drw["pcascode"].ToString().Trim(), drw["reason"].ToString().Trim(), "Approver");
                strWrite = strWrite + "<tr>" +
                                        "<td class='GridRows'>" + strImage + "</td>" +
                                        "<td class='GridRows'>" +
                                          strLink +
                                         "<br>PCAS #: " + drw["pcascode"].ToString().Trim() +
                                         "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
                                         "<br>Request For: " + drw["reason"] +
                                         "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
                                         "<br>Date fund needed: " + Convert.ToDateTime(drw["DateNeed"]).ToString("MMMM dd, yyyy") +
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