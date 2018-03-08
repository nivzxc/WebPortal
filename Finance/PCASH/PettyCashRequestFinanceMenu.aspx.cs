using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using STIeForms;

public partial class Finance_PCASH_PettyCashRequestFinanceMenu : System.Web.UI.Page
{
    protected string writeApprover(DataTable pUsername, string pStatusCode, string pStatus)
    {
        string strReturn = "";
        int intCounter = 0;

        if (pStatusCode == "1")
        {

            if (pStatus == "Approved")
            {
                strReturn = "Approved";

            }
            else if (pStatus == "Disapproved")
            {
                strReturn = "Disapproved";
            }
            else if (pStatus == "DepartmentApproval")
            {
                strReturn = "For approval of group head";
            }
            else if (pStatus == "DivisionApproval")
            {
                strReturn = "For approval of division head";

            }
            else if (pStatus == "ForApproval")
            {
                foreach (DataRow drApprover in pUsername.Rows)
                {

                    if (intCounter != 0)
                    {
                        strReturn += "<br>";
                    }

                    if (drApprover["ApproverStatus"].ToString() == "1")
                    {

                        strReturn += "Processed by: " + drApprover["username"].ToString() + " (" + drApprover["ApproveDate"].ToString() + ")";
                        intCounter++;
                    }
                    else
                    {
                        strReturn += "For Processing of: " + drApprover["username"].ToString();
                        intCounter++;
                    }

                }
            }


        }
        else if (pStatusCode == "2")
        {
            strReturn += "Cheque Released";
        }
        else if (pStatusCode == "3")
        {
            strReturn += "Cancelled";
        }
        else if (pStatusCode == "4")
        {
            strReturn += "Disapproved";
        }

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
        { strReturn = "<a href='PettyCashRequestDetailsFPC.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        if (pLinkType == "Approver")
        { strReturn = "<a href='PettyCashRequestDetailsApprover.aspx?pcascode=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

        return strReturn;
    }

    protected string writeLink(string pKey, string pDescription, string pStatusCode, string pStatus)
    {
        string strReturn = "";

        if (pStatusCode == "1")
        {

            if (pStatus == "Approved")
            {
                strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";

            }
            else if (pStatus == "Disapprove")
            {
                strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";
            }
            else if (pStatus == "DepartmentApproval")
            {
                strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";
            }
            else if (pStatus == "DivisionApproval")
            {
                strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";

            }
            else if (pStatus == "ForApproval")
            {
                strReturn = "<a href='CATADetailsApproverFinance.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";
            }

        }
        else if (pStatusCode == "2")
        {
            strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";
        }
        else if (pStatusCode == "3")
        {
            strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";
        }

        else if (pStatusCode == "4")
        {
            strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";
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

        if (pApproverType == "Skipped")
        { strReturn = "Skipped: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

        if (pApproverType == "Disapproved")
        { strReturn = "Disapproved by: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

        if (pApproverType == "Approval")
        { strReturn = "For Approval of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

        if (pApproverType == "Issued")
        { strReturn = "Issued by: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }


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
        { strReturn = "<a href='PettyCashRequestDetailsFPC.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }

        if (pLinkType == "Approver")
        { strReturn = "<a href='PettyCashRequestDetailsApprover.aspx?pcascode=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }


        return strReturn;

    }

    protected void LoadMenuCATAFinance()
    {

        if (clsSystemModule.HasAccess("PETTYFPC", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            string strWrite = "";
            string strApprovers = "";
            string strImage = "";
            string strLink = "";
            int intCtr = 0;
            DataTable tblFinanceProcess = clsCATAApproval.GetDSGForApprovalFinance(dtpStart.Date, dtpEnd.Date, ddlFinance.SelectedValue.ToString());
            if (tblFinanceProcess.Rows.Count > 0)
            {
                foreach (DataRow drFinance in tblFinanceProcess.Rows)
                {

                    strLink = writeLink(drFinance["PCASCode"].ToString().Trim(), drFinance["TripPurpose"].ToString().Trim(), drFinance["StatusCode"].ToString(), drFinance["Status"].ToString());

                    if (drFinance["StatusCode"].ToString() == "1")
                    {
                        strImage = clsFinanceApprover.GetRequestStatusIcon("1");
                    }
                    else if (drFinance["StatusCode"].ToString() == "2")
                    {
                        strImage = clsFinanceApprover.GetRequestStatusIcon("2");
                    }
                    else if (drFinance["StatusCode"].ToString() == "3")
                    {
                        strImage = clsFinanceApprover.GetRequestStatusIcon("3");
                    }
                    else if (drFinance["StatusCode"].ToString() == "4")
                    {
                        strImage = clsFinanceApprover.GetRequestStatusIcon("4");
                    }

                    strApprovers = writeApprover(clsCATAApproval.GetDSGApproversFinance(drFinance["CataCode"].ToString()), drFinance["StatusCode"].ToString(), drFinance["Status"].ToString());

                    strWrite = strWrite + "<tr>" +
                               "<td class='GridRows'><img src='../../Support/" + strImage + "' alt='' /></td>" +
                               "<td class='GridRows'>" +
                                strLink +
                                 "<br>CATA Number: " + drFinance["CataCode"].ToString() +
                                "<br>Date Requested: " + Convert.ToDateTime(drFinance["DateRequested"]).ToString("MMMM dd, yyyy") +
                                 "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drFinance["RequestedBy"] + "'>" + drFinance["RequestedBy"] + "</a>" +
                                "<br>Date check needed: " + Convert.ToDateTime(drFinance["DateNeeded"]).ToString("MMMM dd, yyyy") +
                               "</td>" +
                               "<td class='GridRows'>" +
                               strApprovers +
                            "</td>" +
                              "</tr>";
                    intCtr++;

                }

            }

            if (intCtr > 0)
            {
                Response.Write(strWrite);
                if (intCtr == 0)
                    Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
                else
                    Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
            }
        }


    }

    protected void LoadMenuPCASUser()
    {
        //if (!IsPostBack)
        //{
            string strWhere = "";

            string strWrite = "";
            string strApprovers = "";
            string strImage = "";
            string strLink = "";
            int intCtr = 0;

            int intStart = 1;
            int intEnd = 10;

            if (ddlFinance.SelectedValue.ToString() == "ALL")
            {
                strWhere = "AND (createon BETWEEN '" + dtpStart.Date.ToString("yyyy-MM-dd") + " 00:00:01' AND '" + dtpEnd.Date.ToString("yyyy-MM-dd") + " 23:59:59')";
            }
            else if (ddlFinance.SelectedValue.ToString() == "APPROVEDFPC")
            {
                strWhere = "AND (createon BETWEEN '" + dtpStart.Date.ToString("yyyy-MM-dd") + " 00:00:01' AND '" + dtpEnd.Date.ToString("yyyy-MM-dd") + " 23:59:59') AND pcasstat='A' AND isissued='F'";
            }
            else if (ddlFinance.SelectedValue.ToString() == "APPROVEDREADY")
            {
                strWhere = "AND (createon BETWEEN '" + dtpStart.Date.ToString("yyyy-MM-dd") + " 00:00:01' AND '" + dtpEnd.Date.ToString("yyyy-MM-dd") + " 23:59:59') AND pcasstat='A' AND isissued='R'";
            }
            else if (ddlFinance.SelectedValue.ToString() == "ISSUED")
            {
                strWhere = "AND (createon BETWEEN '" + dtpStart.Date.ToString("yyyy-MM-dd") + " 00:00:01' AND '" + dtpEnd.Date.ToString("yyyy-MM-dd") + " 23:59:59') AND pcasstat='A' AND isissued='I'";
            }
            else if (ddlFinance.SelectedValue.ToString() == "DISAPPROVED")
            {
                strWhere = "AND (createon BETWEEN '" + dtpStart.Date.ToString("yyyy-MM-dd") + " 00:00:01' AND '" + dtpEnd.Date.ToString("yyyy-MM-dd") + " 23:59:59') AND pcasstat='D'";
            }
            else if (ddlFinance.SelectedValue.ToString() == "ONPROCESS")
            {
                strWhere = "AND (createon BETWEEN '" + dtpStart.Date.ToString("yyyy-MM-dd") + " 00:00:01' AND '" + dtpEnd.Date.ToString("yyyy-MM-dd") + " 23:59:59') AND pcasstat='P'";
            }
            else if (ddlFinance.SelectedValue.ToString() == "FApprovalFPC")
            {
                strWhere = "AND (createon BETWEEN '" + dtpStart.Date.ToString("yyyy-MM-dd") + " 00:00:01' AND '" + dtpEnd.Date.ToString("yyyy-MM-dd") + " 23:59:59')  AND pcascode IN (SELECT pcascode FROM Finance.PCASApproval WHERE apvrtype IN ('F1','F2','F3') AND pcasstat='0') AND pcasstat !='D'  AND pcascode IN (SELECT pcascode FROM Finance.PCASApproval WHERE apvrtype IN ('D') AND pcasstat='1')";
            }

            //DataTable tblPCAS = clsPCASRequest.GetDSGMainFormPerUser(" TOP 10 (pcascode), dateneed, reason, isexecut, reqstdby, pclscode,obcode,pctpcode,schlcode,rccode,others,pcasstat,createby, createon", strWhere, intStart, intEnd);
            DataTable tblPCAS = clsPCASRequest.GetDSGMainFormPerUser(" *", strWhere, intStart, intEnd, txtSearchData.Text);
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
                        strApprovers = strApprovers + writeApprover(drwApprover["username"].ToString(), "Approved") + " (" + drwApprover["apvrdate"].ToString() + ")<br>";
                    }
                    else if (drwApprover["pcasstat"].ToString() == "2")
                    {
                        strApprovers = strApprovers + writeApprover(drwApprover["username"].ToString(), "Disapproved") + "  (" + drwApprover["apvrdate"].ToString() + ")<br>";
                    }
                    else if (drwApprover["pcasstat"].ToString() == "3")
                    {
                        strApprovers = strApprovers + writeApprover(drwApprover["username"].ToString(), "Skipped") + " (" + drwApprover["apvrdate"].ToString() + ")<br>";
                    }
                }
                //rollie
                if (clsPCASRequest.GetPCASIsIssued(drw["pcascode"].ToString())=="I")
                {
                    strApprovers = strApprovers + writeApprover(clsPCASRequestCustodian.GetUsername(drw["pcascode"].ToString()), "Issued") + " (" + clsPCASRequestCustodian.GetDateIssued(drw["pcascode"].ToString()) + ")<br>";
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
        //}
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

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        if (!clsSystemModule.HasAccess("PETTYFPC", Request.Cookies["Speedo"]["UserName"].ToString()))
        {
            Response.Redirect("~/AccessDenied.aspx");
        }
        if (!Page.IsPostBack)
        {
            DataTable tblFinance = new DataTable();
            tblFinance.Columns.Add("pValue");
            tblFinance.Columns.Add("pText");

            DataRow drnew = tblFinance.NewRow();
            drnew["pText"] = "ALL";
            drnew["pValue"] = "ALL";
            tblFinance.Rows.Add(drnew);

            //foreach (DataRow drFinance in clsCATAFinanceApprovers.GetDSG().Rows)
            //{
            //    drnew = tblFinance.NewRow();
            //    drnew["pValue"] = drFinance["aprvname"].ToString();
            //    drnew["pText"] = drFinance["aprvname"].ToString();
            //    tblFinance.Rows.Add(drnew);
            //}

            //drnew = tblFinance.NewRow();
            //drnew["pText"] = "Financial Planning and Control";
            //drnew["pValue"] = "FPC";
            //tblFinance.Rows.Add(drnew);

            drnew = tblFinance.NewRow();
            drnew["pText"] = "On Process";
            drnew["pValue"] = "ONPROCESS";
            tblFinance.Rows.Add(drnew);

            drnew = tblFinance.NewRow();
            drnew["pText"] = "Disapproved";
            drnew["pValue"] = "DISAPPROVED";
            tblFinance.Rows.Add(drnew);

            drnew = tblFinance.NewRow();
            drnew["pText"] = "For Approval of FPC";
            drnew["pValue"] = "FApprovalFPC";
            tblFinance.Rows.Add(drnew);

            drnew = tblFinance.NewRow();
            drnew["pText"] = "Approved by FPC";
            drnew["pValue"] = "APPROVEDFPC";
            tblFinance.Rows.Add(drnew);

            drnew = tblFinance.NewRow();
            drnew["pText"] = "Approved-Ready for Pickup";
            drnew["pValue"] = "APPROVEDREADY";
            tblFinance.Rows.Add(drnew);

            drnew = tblFinance.NewRow();
            drnew["pText"] = "Issued";
            drnew["pValue"] = "ISSUED";
            tblFinance.Rows.Add(drnew);

            ddlFinance.DataSource = tblFinance;
            ddlFinance.DataValueField = "pValue";
            ddlFinance.DataTextField = "pText";
            ddlFinance.DataBind();

            DateTime dtpFirstDayNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1);
            dtpStart.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Date = new DateTime(DateTime.Now.Year, dtpFirstDayNextMonth.AddDays(-1).Month, dtpFirstDayNextMonth.AddDays(-1).Day);

            ddlFinance.SelectedValue = "FApprovalFPC";
        }
    }

    protected void btnPrintIssued_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCASIssuedSummaryReport.aspx");
    }
}