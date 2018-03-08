//Programmer: Charlie Bachiller 
//Date finished: March 4, 2011

using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using STIeForms;

public partial class Finance_FinanceCataMenuAll : System.Web.UI.Page
{

    protected string writeApprover(DataTable pUsername, string pStatCode)
    {
        string strReturn = "";
        if (pStatCode == "0")
        {
            strReturn += "Saved as Draft";
        }
        else if (pStatCode == "1")
        {
            int intCounter = 0;
            foreach (DataRow drApprover in pUsername.Rows)
            {
                if (intCounter != 0)
                {
                    strReturn += "<br>";
                }
                if (drApprover["ApproverType"].ToString() == "E")
                {

                    if (drApprover["ApproverStatus"].ToString() == "1")
                    {
                        strReturn += "Approved by: <a href='../../Userpage/UserPage.aspx?username=" + drApprover["username"].ToString() + "'>" + drApprover["username"].ToString() + "</a>" + " (" + drApprover["ApproveDate"].ToString() + ")";
                    }
                    if (drApprover["ApproverStatus"].ToString() == "0")
                    {
                        strReturn += "For Endorsement of: <a href='../../Userpage/UserPage.aspx?username=" + drApprover["username"].ToString() + "'>" + drApprover["username"].ToString() + "</a>";
                    }

                }
                else if (drApprover["ApproverType"].ToString() == "A")
                {
                    if (drApprover["ApproverStatus"].ToString() == "1")
                    {
                        strReturn += "Approved by: <a href='../../Userpage/UserPage.aspx?username=" + drApprover["username"].ToString() + "'>" + drApprover["username"].ToString() + "</a>" + " (" + drApprover["ApproveDate"].ToString() + ")";
                    }
                    if (drApprover["ApproverStatus"].ToString() == "0")
                    {
                        strReturn += "For Authorization of: <a href='../../Userpage/UserPage.aspx?username=" + drApprover["username"].ToString() + "'>" + drApprover["username"].ToString() + "</a>";
                    }
                }
                else
                {
                    if (drApprover["ApproverStatus"].ToString() == "1")
                    {
                        strReturn += "Processed by: " + drApprover["username"].ToString() + " (" + drApprover["ApproveDate"].ToString() + ")";
                    }
                    if (drApprover["ApproverStatus"].ToString() == "0")
                    {
                        strReturn += "For Processing of: " + drApprover["username"].ToString();
                    }
                }

                intCounter++;
            }
        }
        else if (pStatCode == "2")
        {
            strReturn += "Cheque Released";
        }
        else if (pStatCode == "3")
        {
            strReturn += "Cancelled";
        }
        else if (pStatCode == "4")
        {
            strReturn += "Disapproved";
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

        if (pLinkType == "View")
        { strReturn = "<a href='CATADetails.aspx?catacode=" + pKey + " ' style='font-size:small;'>" + CheckLength(pDescription) + "</a>"; }


        return strReturn;
    }


 protected void LoadMenuCATA()
 {

     if (!IsPostBack)
     {

         string strWrite = "";
         string strApprovers = "";
         string strImage = "";
         string strLink = "";
         int intCtr = 0;
         DataTable tblCata = clsCATARequest.GetDSGMainFormPerUser(Request.Cookies["Speedo"]["UserName"]);
         foreach (DataRow drCata in tblCata.Rows)
         {

             if (drCata["statcode"].ToString() == "0")
             {
                 strLink = writeLink(drCata["catacode"].ToString().Trim(), drCata["trpprpse"].ToString().Trim(), "Edit");
             }
             else
             {
                 strLink = writeLink(drCata["catacode"].ToString().Trim(), drCata["trpprpse"].ToString().Trim(), "View");
             }
             strImage = clsFinanceApprover.GetRequestStatusIcon(drCata["statcode"].ToString());
             strApprovers = writeApprover(clsCATAApproval.GetDSGApprovers(drCata["catacode"].ToString()), drCata["statcode"].ToString());

             strWrite = strWrite + "<tr>" +
                               "<td class='GridRows'><img src='../../Support/" + strImage + "' alt='' /></td>" +
                               "<td class='GridRows'>" +
                                  strLink +
                                   "<br>CATA Number: " + drCata["catacode"].ToString() +
                                  "<br>Date Requested: " + Convert.ToDateTime(drCata["createon"]).ToString("MMMM dd, yyyy") +
                                   "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drCata["createby"] + "'>" + drCata["createby"] + "</a>" +
                                  "<br>Date check needed: " + Convert.ToDateTime(drCata["dateneed"]).ToString("MMMM dd, yyyy") +
                               "</td>" +
                               "<td class='GridRows'>" +
                                strApprovers +
                               "</td>" +
                              "</tr>";
             intCtr++;
         }


         Response.Write(strWrite);
         if (intCtr == 0)
         {
             Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
         }
         else
         {
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
