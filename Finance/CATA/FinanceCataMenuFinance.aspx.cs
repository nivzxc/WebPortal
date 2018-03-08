using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Finance_CATA_FinanceCataMenuFinance : System.Web.UI.Page
{


    protected string writeApprover(DataTable pUsername,string pStatusCode, string pStatus)
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

    protected void LoadMenuCATAFinance()
    {
       
            if (clsSystemModule.HasAccess("023", Request.Cookies["Speedo"]["UserName"].ToString()))
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

                        strLink = writeLink(drFinance["CataCode"].ToString().Trim(), drFinance["TripPurpose"].ToString().Trim(), drFinance["StatusCode"].ToString(), drFinance["Status"].ToString());

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
        if (!clsSystemModule.HasAccess("CATA", Request.Cookies["Speedo"]["UserName"].ToString()))
        { 
            Response.Redirect("~/AccessDenied.aspx"); 
        }
        if (!Page.IsPostBack)
        {
            DataTable tblFinance = new DataTable();
            tblFinance.Columns.Add("pValue");
            tblFinance.Columns.Add("pText");

            DataRow drnew = tblFinance.NewRow();
            drnew["pValue"] = "ALL";
            drnew["pText"] = "ALL";
            tblFinance.Rows.Add(drnew);

            foreach (DataRow drFinance in clsCATAFinanceApprovers.GetDSG().Rows)
            {
                drnew = tblFinance.NewRow();
                drnew["pValue"] = drFinance["aprvname"].ToString();
                drnew["pText"] = drFinance["aprvname"].ToString();
                tblFinance.Rows.Add(drnew);
            }

            drnew = tblFinance.NewRow();
            drnew["pValue"] = "APPROVED";
            drnew["pText"] = "Processed";
            tblFinance.Rows.Add(drnew);

            drnew = tblFinance.NewRow();
            drnew["pText"] = "Disapproved";
            drnew["pValue"] = "DISAPPROVED";
            tblFinance.Rows.Add(drnew);

            ddlFinance.DataSource = tblFinance;
            ddlFinance.DataValueField = "pValue";
            ddlFinance.DataTextField = "pText";
            ddlFinance.DataBind();

            DateTime dtpFirstDayNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1);
            dtpStart.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Date = new DateTime(DateTime.Now.Year, dtpFirstDayNextMonth.AddDays(-1).Month, dtpFirstDayNextMonth.AddDays(-1).Day);


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}