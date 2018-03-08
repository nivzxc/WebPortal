using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using STIeForms;

public partial class Finance_RFP_RFPMenu : System.Web.UI.Page
{

 protected string writeApprover(string pUsername, string pApproverType)
 {
  string strReturn = "";
  if (pApproverType == "Endorser")
  { strReturn = "For Endorsement of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

  if (pApproverType == "Authority")
  { strReturn = "For Authorization of: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

  if (pApproverType == "Approved")
  { strReturn = "Approved by: <a href='../../Userpage/UserPage.aspx?username=" + pUsername + "'>" + pUsername + "</a>"; }

  return strReturn;
 }

 protected string writeLink(string pKey, string pDescription, string pLinkType)
 {
  string strReturn="";
  if (pLinkType == "Edit")
  {
   strReturn = "<a href='RFPEditRequest.aspx?ControlNumber=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription),40) + "</a>";
  }

  if (pLinkType == "Print")
  { strReturn = "<a href='RFPPrint.aspx?ControlNumber=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }

  if (pLinkType == "None")
  { strReturn = "<a href='RFPDetails.aspx?ControlNumber=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>"; }
  
  return strReturn;

 }

 protected string writeImageLink(string pKey, string pLinkType, string pImage)
 {
  string strReturn = "";
  if (pLinkType == "Edit")
  {
   strReturn = "<a href='RFPEditRequest.aspx?ControlNumber=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>";
  }

  if (pLinkType == "Print")
  { strReturn = "<a href='RFPPrint.aspx?ControlNumber=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

  if (pLinkType == "None")
  { strReturn = "<a href='RFPDetails.aspx?ControlNumber=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>"; }

  return strReturn;
 }

 protected void LoadMenuRFP()
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
   int intEnd = 10;

   DataTable tblRFP = clsRFPRequest.GetDSGMainFormPerUser(" TOP 10 (ctrlnmbr), rqstcode, rqstfor, projttle, dateneed, payename,endrsby1,endrstt1,endrstt2,endrsby2,authrzby,authstat,statcode, createby, createon", Request.Cookies["Speedo"]["UserName"], strWhere, intStart, intEnd);
   foreach (DataRow drw in tblRFP.Rows)
   {
    ///////////////////////////////////////////No Endorser////////////////////////////////////////////////////
    if (clsFinanceApprover.IsHaveNoEndorser("ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
    {
     if (drw["AuthorizeStatus"].ToString().Trim() == "2")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Edit");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Edit", clsRFPRequest.GetRequestStatusIcon("2"));
      strApprovers = writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
     }
     else if (drw["AuthorizeStatus"].ToString().Trim() == "1")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
      strApprovers = "<br>Authorized";
     }
     else if (drw["AuthorizeStatus"].ToString().Trim() == "M")
     {
         strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
         strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
         strApprovers = "<br>Manual Approval";
     }
     else
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("0"));
      strApprovers = "<br>Disapproved";     
     }
    }
    ///////////////////////////////////////////With One Endorser////////////////////////////////////////////////////
    else if (!clsFinanceApprover.IsHave2ndEndorser("ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
    {
     if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["AuthorizeStatus"].ToString().Trim() == "2")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Edit");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Edit", clsRFPRequest.GetRequestStatusIcon("2"));
      strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Endorser") +
                     "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "2")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
      strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Approved") +
                     "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "1")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
      strApprovers = "<br>Authorized";
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "M")
     {
         strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
         strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
         strApprovers = "<br>Manual Approval";
     }
    }
    ///////////////////////////////////////////Two Endorser////////////////////////////////////////////////////
    else
    {
     if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "2" && drw["AuthorizeStatus"].ToString().Trim() == "2")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Edit");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Edit", clsRFPRequest.GetRequestStatusIcon("2"));
      strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Endorser") +
                     "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Endorser") +
                     "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "2" && drw["AuthorizeStatus"].ToString().Trim() == "2")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
      strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Approved") +
                     "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Endorser") +
                     "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "2")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
      strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Endorser") +
                     "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Approved") +
                     "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "2")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
      strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Approved") +
                     "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Approved") +
                     "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "1")
     {
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
      strApprovers = "<br>Authorized";
     }
     else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "M")
     {
         strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
         strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
         strApprovers = "<br>Manual Approval";
     }
    }

    if (drw["Status"].ToString().Trim() == "0")
    {
     strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("0"));
     strApprovers = "<br>Disapproved";
     strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
    }

    if (drw["Status"].ToString().Trim() == "4")
    {
     strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
     strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
    }

    if (drw["Status"].ToString().Trim() == "M")
    {
        strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
        strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
    }

    if (drw["Status"].ToString().Trim() == "3")
    {
     strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("0"));
     strApprovers = "<br>The user cancelled the application";
     strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
    }


    strWrite = strWrite + "<tr>" +
                            "<td class='GridRows'>" + strImage + "</td>" +
                            "<td class='GridRows'>" +
                              strLink +
                             "<br>Control Number: " + drw["ControlNumber"].ToString().Trim() +
                             "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                             "<br>Request For: " + clsRFPRequestType.GetRequestTypeName(drw["RequestCode"].ToString().Trim()) +
                             "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                             "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
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

 protected void LoadMenuRFPFinance()
 {
  if (!IsPostBack)
  {
   if (clsSystemModule.HasAccess("023", Request.Cookies["Speedo"]["UserName"].ToString().Trim()))
   {
    string strWhere = "";
    string strWrite = "";
    string strApprovers = "";
    string strImage = "";
    string strLink = "";
    int intCtr = 0;
    int intPage = Convert.ToInt32(Request.QueryString["page"]);
    int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"]);
    int intStart = 1;
    int intEnd = 10;

    DataTable tblRFP = clsRFPRequest.GetDSGMainForm("TOP 10 (ctrlnmbr), rqstcode, rqstfor, projttle, dateneed, payename,endrsby1,endrstt1,endrstt2,endrsby2,authrzby,authstat,statcode, createby, createon", strWhere, intStart, intEnd);
    foreach (DataRow drw in tblRFP.Rows)
    {
     ///////////////////////////////////////////No Endorser////////////////////////////////////////////////////
     if (clsFinanceApprover.IsHaveNoEndorser("ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
     {
      if (drw["AuthorizeStatus"].ToString().Trim() == "2")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Edit");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Edit", clsRFPRequest.GetRequestStatusIcon("2"));
       strApprovers = writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
      }
      else if (drw["AuthorizeStatus"].ToString().Trim() == "1")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
       strApprovers = "<br>Authorized";
      }
      else if (drw["AuthorizeStatus"].ToString().Trim() == "M")
      {
          strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
          strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
          strApprovers = "<br>Manual Approval";
      }
      else
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("0"));
       strApprovers = "<br>Disapproved";
      }
     }
     ///////////////////////////////////////////With One Endorser////////////////////////////////////////////////////
     else if (!clsFinanceApprover.IsHave2ndEndorser("ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
     {
      if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["AuthorizeStatus"].ToString().Trim() == "2")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Edit");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Edit", clsRFPRequest.GetRequestStatusIcon("2"));
       strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Endorser") +
                      "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "2")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
       strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Approved") +
                      "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "1")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
       strApprovers = "<br>Authorized";
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "M")
      {
          strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
          strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
          strApprovers = "<br>Manual Approval";
      }
     }
     ///////////////////////////////////////////Two Endorser////////////////////////////////////////////////////
     else
     {
      if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "2" && drw["AuthorizeStatus"].ToString().Trim() == "2")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Edit");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Edit", clsRFPRequest.GetRequestStatusIcon("2"));
       strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Endorser") +
                      "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Endorser") +
                      "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "2" && drw["AuthorizeStatus"].ToString().Trim() == "2")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
       strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Approved") +
                      "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Endorser") +
                      "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "2")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
       strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Endorser") +
                      "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Approved") +
                      "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "2")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("2"));
       strApprovers = writeApprover(drw["EndorsedBy1"].ToString(), "Approved") +
                      "<br>" + writeApprover(drw["EndorsedBy2"].ToString(), "Approved") +
                      "<br>" + writeApprover(drw["AuthorizeBy"].ToString(), "Authority");
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "1")
      {
       strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
       strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
       strApprovers = "<br>Authorized";
      }
      else if (drw["Endorsed1Status"].ToString().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim() == "1" && drw["AuthorizeStatus"].ToString().Trim() == "M")
      {
          strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
          strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
          strApprovers = "<br>Manual Approval";
      }
     }

     if (drw["Status"].ToString().Trim() == "0")
     {
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("0"));
      strApprovers = "<br>Disapproved";
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
     }

     if (drw["Status"].ToString().Trim() == "4")
     {
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
     }

     if (drw["Status"].ToString().Trim() == "M")
     {
         strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "Print", clsRFPRequest.GetRequestStatusIcon("1"));
         strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "Print");
     }

     if (drw["Status"].ToString().Trim() == "3")
     {
      strImage = writeImageLink(drw["ControlNumber"].ToString().Trim(), "None", clsRFPRequest.GetRequestStatusIcon("0"));
      strApprovers = "<br>The user cancelled the application";
      strLink = writeLink(drw["ControlNumber"].ToString().Trim(), drw["ProjectTitle"].ToString().Trim(), "None");
     }


     strWrite = strWrite + "<tr>" +
                             "<td class='GridRows'>" + strImage + "</td>" +
                             "<td class='GridRows'>" +
                               strLink +
                              "<br>Control Number: " + drw["ControlNumber"].ToString().Trim() +
                              "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                              "<br>Request For: " + clsRFPRequestType.GetRequestTypeName(drw["RequestCode"].ToString().Trim()) +
                              "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                              "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
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
 }

 protected void LoadMenuRFPApprover()
 {
  if (!IsPostBack)
  {
   string strWrite = "";
   int intCtr = 0;
   DataTable tblRFPApprover = clsRFPRequest.GetDSGMainFormApprover(Request.Cookies["Speedo"]["UserName"]);
   foreach (DataRow drw in tblRFPApprover.Rows)
    {

     if (clsFinanceApprover.IsAuthoritary((Request.Cookies["Speedo"]["UserName"]), "ctrlnmbr", drw["ControlNumber"].ToString().Trim(), "RFPRequest"))
     
     {
      if (drw["Endorsed1Status"].ToString().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim() == "")
      { continue; }

      else if (drw["Endorsed1Status"].ToString().Trim().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim().Trim() == "2")
      { continue; }

      else if (drw["Endorsed1Status"].ToString().Trim().Trim() == "2" && drw["Endorsed2Status"].ToString().Trim().Trim() == "1")
      { continue; }

      else if (drw["Endorsed1Status"].ToString().Trim().Trim() == "1" && drw["Endorsed2Status"].ToString().Trim().Trim() == "2")
      { continue; }
      else
      {
       strWrite = strWrite + "<tr>" +
                              "<td class='GridRows'></td>" +
                              "<td class='GridRows' colspan=2'>" +
                               "<a href='RFPDetailsApprover.aspx?ControlNumber=" + drw["ControlNumber"].ToString().Trim() + " ' style='font-size:small;'>" + CheckLength(drw["ProjectTitle"].ToString().Trim()) + "</a>" +
                               "<br>Control Number: " + drw["ControlNumber"].ToString().Trim() +
                               "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                               "<br>Request For: " + clsRFPRequestType.GetRequestTypeName(drw["RequestCode"].ToString().Trim()) +
                               "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                               "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
                              "</td>" +
                             "</tr>";
       intCtr++;
      }
     }
     else if (clsFinanceApprover.IsEndorder1(Request.Cookies["Speedo"]["UserName"], "ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
     {
      if (drw["Endorsed1Status"].ToString().Trim() == "2")
      {
       strWrite = strWrite + "<tr>" +
                                    "<td class='GridRows'></td>" +
                                    "<td class='GridRows' colspan=2'>" +
                                     "<a href='RFPDetailsApprover.aspx?ControlNumber=" + drw["ControlNumber"].ToString().Trim() + " ' style='font-size:small;'>" + CheckLength(drw["ProjectTitle"].ToString().Trim()) + "</a>" +
                                     "<br>Control Number: " + drw["ControlNumber"].ToString().Trim() +
                                     "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                                     "<br>Request For: " + clsRFPRequestType.GetRequestTypeName(drw["RequestCode"].ToString().Trim()) +
                                     "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                                     "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
                                    "</td>" +
                                   "</tr>";
       intCtr++;
      }

     }
     else if (clsFinanceApprover.IsEndorder2(Request.Cookies["Speedo"]["UserName"], "ctrlnmbr", drw["ControlNumber"].ToString(), "RFPRequest"))
     {
      if (drw["Endorsed2Status"].ToString().Trim() == "2")
      {
       strWrite = strWrite + "<tr>" +
                                    "<td class='GridRows'></td>" +
                                    "<td class='GridRows' colspan=2'>" +
                                     "<a href='RFPDetailsApprover.aspx?ControlNumber=" + drw["ControlNumber"].ToString().Trim() + " ' style='font-size:small;'>" + CheckLength(drw["ProjectTitle"].ToString().Trim()) + "</a>" +
                                     "<br>Control Number: " + drw["ControlNumber"].ToString().Trim() +
                                     "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                                     "<br>Request For: " + clsRFPRequestType.GetRequestTypeName(drw["RequestCode"].ToString().Trim()) +
                                     "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                                     "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
                                    "</td>" +
                                   "</tr>";
       intCtr++;
      }
     }
     else
     {
      strWrite = strWrite + "<tr>" +
                             "<td class='GridRows'></td>" +
                             "<td class='GridRows' colspan=2'>" +
                              "<a href='RFPDetailsApprover.aspx?ControlNumber=" + drw["ControlNumber"].ToString().Trim() + " ' style='font-size:small;'>" + CheckLength(drw["ProjectTitle"].ToString().Trim()) + "</a>" +
                              "<br>Control Number: " + drw["ControlNumber"].ToString().Trim() +
                              "<br>Date Requested: " + Convert.ToDateTime(drw["CreatedOn"]).ToString("MMMM dd, yyyy") +
                              "<br>Request For: " + clsRFPRequestType.GetRequestTypeName(drw["RequestCode"].ToString().Trim()) +
                              "<br>Requested by: <a href='../../Userpage/UserPage.aspx?username=" + drw["CreatedBy"] + "'>" + drw["CreatedBy"] + "</a>" +
                              "<br>Date check needed: " + Convert.ToDateTime(drw["DateNeeded"]).ToString("MMMM dd, yyyy") +
                             "</td>" +
                            "</tr>";
      intCtr++;
     }

    
   }
   Response.Write(strWrite);

   if (intCtr == 0)
    Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
   else
    Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
  }
 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
 }

 protected void btnNewRequest_Click(object sender, EventArgs e)
 {
  Response.Redirect("RFPNewRequest.aspx");
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
