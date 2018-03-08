using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using STIeForms;

public partial class Finance_CATA_FinanceCataMenuApprover : System.Web.UI.Page
{
    protected string writeApprover(string pUsername, string pApproverType, string pApproveStatus)
    {
        //A = Authorize
        //E = Endorser
        string strReturn = "";
        if (pApproverType == "E")
        {
            if (pApproveStatus == "1")
            { strReturn = "Approved by: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }
            if (pApproveStatus == "2")
            { strReturn = "For Endorsement of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }
        }

        if (pApproverType == "A")
        {
            if (pApproveStatus == "1")
            { strReturn = "Approved by: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }
            if (pApproveStatus == "2")
            { strReturn = "For Authorization of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }
        }

        return strReturn;
    }

    protected string writeLink(string pKey, string pDescription, string pLinkType)
    {
        string strReturn = "";
        if (pLinkType == "Edit")
        {
            strReturn = "<a href='FinanceCataEditRequest.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>";
        }

        if (pLinkType == "Print")
        { strReturn = "<a href='CATAReport.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>"; }


        if (pLinkType == "View")
        { strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>"; }


        return strReturn;
    }

    protected void LoadMenuCATAApprovedRequest()
    {
        if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
        {
            string strWrite = "";
            int intCtr = 0;
            DataTable tblApprovedReques = clsCATAApproval.GetDSGProcessedRequest(Request.Cookies["Speedo"]["UserName"], "1");

            if (tblApprovedReques.Rows.Count > 0)
            {
                foreach (DataRow drApprovedReques in tblApprovedReques.Rows)
                {
                    strWrite = strWrite + "<tr>" +
                               "<td class='GridRows'></td>" +
                               "<td class='GridRows' colspan=2'>" +
                                "<a href='CATADetailsApprover.aspx?catacode=" + drApprovedReques["CataCode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drApprovedReques["TripPurpose"].ToString()) + "</a>" +
                                 "<br>CATA Number: " + drApprovedReques["CataCode"].ToString() +
                                "<br>Date Requested: " + Convert.ToDateTime(drApprovedReques["DateRequested"]).ToString("MMMM dd, yyyy") +
                                 "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drApprovedReques["RequestedBy"] + "'>" + drApprovedReques["RequestedBy"] + "</a>" +
                                "<br>Date check needed: " + Convert.ToDateTime(drApprovedReques["DateNeeded"]).ToString("MMMM dd, yyyy") +
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

    protected void LoadMenuCATADisapprovedRequest()
    {
        if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
        {
            string strWrite = "";
            int intCtr = 0;
            DataTable tblDisapprovedRequest = clsCATAApproval.GetDSGProcessedRequest(Request.Cookies["Speedo"]["UserName"], "2");

            if (tblDisapprovedRequest.Rows.Count > 0)
            {
                foreach (DataRow drDisapprovedRequest in tblDisapprovedRequest.Rows)
                {
                    strWrite = strWrite + "<tr>" +
                               "<td class='GridRows'></td>" +
                               "<td class='GridRows' colspan=2'>" +
                                "<a href='CATADetailsApprover.aspx?catacode=" + drDisapprovedRequest["CataCode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drDisapprovedRequest["TripPurpose"].ToString()) + "</a>" +
                                 "<br>CATA Number: " + drDisapprovedRequest["CataCode"].ToString() +
                                "<br>Date Requested: " + Convert.ToDateTime(drDisapprovedRequest["DateRequested"]).ToString("MMMM dd, yyyy") +
                                 "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drDisapprovedRequest["RequestedBy"] + "'>" + drDisapprovedRequest["RequestedBy"] + "</a>" +
                                "<br>Date check needed: " + Convert.ToDateTime(drDisapprovedRequest["DateNeeded"]).ToString("MMMM dd, yyyy") +
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

    protected void LoadMenuCATAApprover()
    {

        if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
        {
            string strWrite = "";
            int intCtr = 0;
            DataTable tblEndorserApproval = clsCATAApproval.GetDSGForApprovalEndorser(Request.Cookies["Speedo"]["UserName"]);
            DataTable tblAuthorizeApproval = clsCATAApproval.GetDSGForApprovalAuthorize(Request.Cookies["Speedo"]["UserName"]);

            if (tblEndorserApproval.Rows.Count > 0)
            {
                foreach (DataRow drEndorser in tblEndorserApproval.Rows)
                {
                    strWrite = strWrite + "<tr>" +
                               "<td class='GridRows'></td>" +
                               "<td class='GridRows' colspan=2'>" +
                                "<a href='CATADetailsApprover.aspx?catacode=" + drEndorser["CataCode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drEndorser["TripPurpose"].ToString()) + "</a>" +
                                 "<br>CATA Number: " + drEndorser["CataCode"].ToString() +
                                "<br>Date Requested: " + Convert.ToDateTime(drEndorser["DateRequested"]).ToString("MMMM dd, yyyy") +
                                 "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drEndorser["RequestedBy"] + "'>" + drEndorser["RequestedBy"] + "</a>" +
                                "<br>Date check needed: " + Convert.ToDateTime(drEndorser["DateNeeded"]).ToString("MMMM dd, yyyy") +
                               "</td>" +
                              "</tr>";
                    intCtr++;

                }

            }

            if (tblAuthorizeApproval.Rows.Count > 0)
            {
                foreach (DataRow drAuthorize in tblAuthorizeApproval.Rows)
                {
                    strWrite = strWrite + "<tr>" +
                                   "<td class='GridRows'></td>" +
                                   "<td class='GridRows' colspan=2'>" +
                                    "<a href='CATADetailsApprover.aspx?catacode=" + drAuthorize["CataCode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drAuthorize["TripPurpose"].ToString()) + "</a>" +
                                     "<br>CATA Number: " + drAuthorize["CataCode"].ToString() +
                                    "<br>Date Requested: " + Convert.ToDateTime(drAuthorize["DateRequested"]).ToString("MMMM dd, yyyy") +
                                     "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drAuthorize["RequestedBy"] + "'>" + drAuthorize["RequestedBy"] + "</a>" +
                                    "<br>Date check needed: " + Convert.ToDateTime(drAuthorize["DateNeeded"]).ToString("MMMM dd, yyyy") +
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

}