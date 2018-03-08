//Programmer: Charlie Bachiller 
//Date finished: March 4, 2011

using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Finance_FinanceCataMenu : System.Web.UI.Page
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
     clsSpeedo.Authenticate();

     if (!Page.IsPostBack)
     {

         string strWrite = "";
         string strApprovers = "";
         string strImage = "";
         string strLink = "";
         int intCtr = 0;
         DataTable tblCata = clsCATARequest.GetDSGMainFormPerUserTOP10(Request.Cookies["Speedo"]["UserName"]);
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






         // foreach (DataRow drw in tblCata.Rows)
         // {
         //  ////////////////////////No Endorser//////////////////////
         //  if (clsFinanceApprover.IsHaveNoEndorser("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //  {
         //   //Two Authority
         //   if (clsFinanceApprover.IsHave2ndAuthority("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //   {
         //    strApprovers = writeApprover(drw["autrzby1"].ToString(), "A", drw["authstt1"].ToString().Trim()) +
         //                    "<br>" + writeApprover(drw["autrzby2"].ToString(), "A", drw["authstt2"].ToString().Trim());
         //   }
         //   //One Authority
         //   else
         //   {
         //    strApprovers = writeApprover(drw["autrzby1"].ToString(), "A", drw["authstt1"].ToString().Trim());
         //   }
         //  }

         //  //////////////////////// One Endorser///////////////////////
         //  else if (!clsFinanceApprover.IsHave2ndEndorser("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //  {
         //   //one endorser, Two Authority
         //   if (clsFinanceApprover.IsHave2ndAuthority("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //   {
         //    strApprovers = writeApprover(drw["endrsby1"].ToString(), "E", drw["endrstt1"].ToString().Trim()) +
         //                    "<br>" + writeApprover(drw["autrzby1"].ToString(), "A", drw["authstt1"].ToString().Trim()) +
         //                    "<br>" + writeApprover(drw["autrzby2"].ToString(), "A", drw["authstt2"].ToString().Trim());
         //   }
         //   //one endorser, One Authority
         //   else
         //   {
         //    strApprovers = writeApprover(drw["endrsby1"].ToString(), "E", drw["endrstt1"].ToString().Trim()) +
         //                   "<br>" + writeApprover(drw["autrzby1"].ToString(), "A", drw["authstt1"].ToString().Trim());

         //   }
         //  }
         //  /////////////////////////// Two Endorser//////////////////////////////
         //  else if (clsFinanceApprover.IsHave2ndEndorser("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //  {
         //   //two endorser, Two Authority
         //   if (clsFinanceApprover.IsHave2ndAuthority("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //   {
         //    strApprovers = writeApprover(drw["endrsby1"].ToString(), "E", drw["endrstt1"].ToString().Trim()) +
         //                    "<br>" + writeApprover(drw["endrsby2"].ToString(), "E", drw["endrstt2"].ToString().Trim()) +
         //                    "<br>" + writeApprover(drw["autrzby1"].ToString(), "A", drw["authstt1"].ToString().Trim()) +
         //                    "<br>" + writeApprover(drw["autrzby2"].ToString(), "A", drw["authstt2"].ToString().Trim());
         //   }
         //   //two endorser, One Authority
         //   else
         //   {
         //    strApprovers = writeApprover(drw["endrsby1"].ToString(), "E", drw["endrstt1"].ToString().Trim()) +
         //                   "<br>" + writeApprover(drw["endrsby2"].ToString(), "E", drw["endrstt2"].ToString().Trim()) +
         //                   "<br>" + writeApprover(drw["autrzby1"].ToString(), "A", drw["authstt1"].ToString().Trim());

         //   }
         //  }

         //  ///////////////////////////////////////////for the link/////////////////////////////////////////////////
         //  //No endorser
         //  if (clsFinanceApprover.IsHaveNoEndorser("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //  {
         //   //Two Authority
         //   if (clsFinanceApprover.IsHave2ndAuthority("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //   {
         //    if (drw["authstt1"].ToString() == "2" && drw["authstt2"].ToString() == "2")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "Edit");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //    else if (drw["authstt1"].ToString() == "1" && drw["authstt2"].ToString() == "2")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "View");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //    else if (drw["authstt1"].ToString() == "2" && drw["authstt2"].ToString() == "1")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "View");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //   }
         //   //One Authority
         //   else
         //   {
         //    if (drw["authstt1"].ToString() == "2")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "Edit");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //   }
         //  }

         //  //////////////////////// One Endorser///////////////////////
         //  else if (!clsFinanceApprover.IsHave2ndEndorser("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //  {
         //   //One Endorser, Two Authority
         //   if (clsFinanceApprover.IsHave2ndAuthority("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //   {
         //    if (drw["endrstt1"].ToString().Trim() == "1" || drw["authstt1"].ToString().Trim() == "1" || drw["authstt2"].ToString().Trim() == "1")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "View");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //    else if (drw["endrstt1"].ToString().Trim() == "2" && drw["authstt1"].ToString().Trim() == "2" && drw["authstt2"].ToString().Trim() == "2")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "Edit");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //   }
         //   // One Endorser, One Authority
         //   else
         //   {
         //    if (drw["endrstt1"].ToString().Trim() == "1" || drw["authstt1"].ToString().Trim() == "1")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "View");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //    else if (drw["endrstt1"].ToString().Trim() == "2" && drw["authstt1"].ToString().Trim() == "2")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "Edit");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //   }
         //  }
         //  /////////////////////////// Two Endorser//////////////////////////////
         //  else if (clsFinanceApprover.IsHave2ndEndorser("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //  {
         //   //Two Endorser, Two Authority
         //   if (clsFinanceApprover.IsHave2ndAuthority("catacode", drw["catacode"].ToString().Trim(), "CATARequest"))
         //   {
         //    if (drw["endrstt1"].ToString().Trim() == "1" || drw["endrstt2"].ToString().Trim() == "1" || drw["authstt1"].ToString().Trim() == "1" || drw["authstt2"].ToString().Trim() == "1")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "View");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //    else if (drw["endrstt1"].ToString().Trim() == "2" && drw["endrstt2"].ToString().Trim() == "2" && drw["authstt1"].ToString().Trim() == "2" && drw["authstt2"].ToString().Trim() == "2")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "Edit");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //   }
         //   //Two Endorser, One Authority
         //   else
         //   {
         //    if (drw["endrstt1"].ToString().Trim() == "1" || drw["endrstt2"].ToString().Trim() == "1" || drw["authstt1"].ToString().Trim() == "1")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "View");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //    else if (drw["endrstt1"].ToString().Trim() == "2" && drw["endrstt2"].ToString().Trim() == "2" && drw["authstt1"].ToString().Trim() == "2")
         //    {
         //     strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "Edit");
         //     strImage = clsFinanceApprover.GetRequestStatusIcon("2");
         //    }
         //   }
         //  }

         //  strApprovers = "<td class='GridRows'>" + strApprovers + "</td>";

         //  //////////////////////////////////////////////////////////////////
         //  if (drw["statcode"].ToString().Trim() == "0")
         //  {
         //   strImage = clsFinanceApprover.GetRequestStatusIcon("0");
         //   strApprovers = "<td class='GridRows'><br>Disapproved</td>";
         //   strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "None");
         //  }

         //  if (drw["statcode"].ToString().Trim() == "1")
         //  {
         //   strImage = clsFinanceApprover.GetRequestStatusIcon("1");
         //   strApprovers = "<td class='GridRows'>Authorized</td>";
         //   strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "Print");
         //  }

         //  if (drw["statcode"].ToString().Trim() == "3")
         //  {
         //   strImage = clsFinanceApprover.GetRequestStatusIcon("0");
         //   strApprovers = "<td class='GridRows'>Cancelled</td>";
         //   strLink = writeLink(drw["catacode"].ToString().Trim(), drw["trpprpse"].ToString().Trim(), "None");
         //  }

         

         //  intCtr++;
         //  strLink = "";
         //  strApprovers = "";
         //  if (intCtr == 10)
         //  { break; }
         // }

         // Response.Write(strWrite);
         // if (intCtr == 0)
         //  Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
         // else
         //  Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
         //}
     }
 }

 protected void LoadMenuCATAApprover()
 {
  if (!IsPostBack)
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
                  if (intCtr == 10)
                  {
                      break;
                  }
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
                  if (intCtr == 10)
                  {
                      break;
                  }
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
   //{

   // string strWrite = "";
   // int intCtr = 0;
   // DataTable tblCata = clsCATARequest.GetDSGMainFormApprover(Request.Cookies["Speedo"]["UserName"]);
   // foreach (DataRow drw in tblCata.Rows)
   // {

   //  if (clsFinanceApprover.IsAuthoritaryCATA(Request.Cookies["Speedo"]["UserName"], "catacode", drw["catacode"].ToString(), "CATARequest"))
   //  {
   //   if (drw["endrstt1"].ToString().Trim() == "2" && drw["endrstt2"].ToString().Trim() == "")
   //   { continue; }

   //   else if (drw["endrstt1"].ToString().Trim() == "2" && drw["endrstt2"].ToString().Trim() == "2")
   //   { continue; }

   //   else if (drw["endrstt1"].ToString().Trim() == "2" && drw["endrstt2"].ToString().Trim() == "1")
   //   { continue; }

   //   else if (drw["endrstt1"].ToString().Trim() == "1" && drw["endrstt2"].ToString().Trim() == "2")
   //   { continue; }
   //   else if (drw["endrstt1"].ToString().Trim() == "" && drw["endrstt2"].ToString().Trim() == "")
   //   {
   //    if (clsFinanceApprover.IsAuthoritary1CATA(Request.Cookies["Speedo"]["UserName"], "catacode", drw["catacode"].ToString(), "CATARequest"))
   //    {
   //     if (drw["authstt1"].ToString().Trim() == "2")
   //     {
   //      strWrite = strWrite + "<tr>" +
   //                  "<td class='GridRows'></td>" +
   //                  "<td class='GridRows' colspan=2'>" +
   //                   "<a href='CATADetailsApprover.aspx?catacode=" + drw["catacode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drw["trpprpse"].ToString()) + "</a>" +
   //                    "<br>CATA Number: " + drw["catacode"].ToString() +
   //                   "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
   //                    "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
   //                   "<br>Date check needed: " + Convert.ToDateTime(drw["dateneed"]).ToString("MMMM dd, yyyy") +
   //                  "</td>" +
   //                 "</tr>";
   //      intCtr++;
   //     }
   //    }
   //    else if (clsFinanceApprover.IsAuthoritary2CATA(Request.Cookies["Speedo"]["UserName"], "catacode", drw["catacode"].ToString(), "CATARequest"))
   //    {
   //     if (drw["authstt2"].ToString().Trim() == "2")
   //     {
   //      strWrite = strWrite + "<tr>" +
   //                  "<td class='GridRows'></td>" +
   //                  "<td class='GridRows' colspan=2'>" +
   //                   "<a href='CATADetailsApprover.aspx?catacode=" + drw["catacode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drw["trpprpse"].ToString()) + "</a>" +
   //                    "<br>CATA Number: " + drw["catacode"].ToString() +
   //                   "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
   //                    "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
   //                   "<br>Date check needed: " + Convert.ToDateTime(drw["dateneed"]).ToString("MMMM dd, yyyy") +
   //                  "</td>" +
   //                 "</tr>";
   //      intCtr++;
   //     }
   //    }
   //   }
   //   else
   //   {
   //    strWrite = strWrite + "<tr>" +
   //                       "<td class='GridRows'></td>" +
   //                       "<td class='GridRows' colspan=2'>" +
   //                        "<a href='CATADetailsApprover.aspx?catacode=" + drw["catacode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drw["trpprpse"].ToString()) + "</a>" +
   //                         "<br>CATA Number: " + drw["catacode"].ToString() +
   //                        "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
   //                         "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
   //                        "<br>Date check needed: " + Convert.ToDateTime(drw["dateneed"]).ToString("MMMM dd, yyyy") +
   //                       "</td>" +
   //                      "</tr>";
   //    intCtr++;
   //   }
   //  }
   //  else if (clsFinanceApprover.IsEndorder1(Request.Cookies["Speedo"]["UserName"], "catacode", drw["catacode"].ToString(), "CATARequest"))
   //  {
   //   if (drw["endrstt1"].ToString().Trim() == "2")
   //   {
   //    strWrite = strWrite + "<tr>" +
   //                      "<td class='GridRows'></td>" +
   //                      "<td class='GridRows' colspan=2'>" +
   //                       "<a href='CATADetailsApprover.aspx?catacode=" + drw["catacode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drw["trpprpse"].ToString()) + "</a>" +
   //                        "<br>CATA Number: " + drw["catacode"].ToString() +
   //                       "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
   //                        "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
   //                       "<br>Date check needed: " + Convert.ToDateTime(drw["dateneed"]).ToString("MMMM dd, yyyy") +
   //                      "</td>" +
   //                     "</tr>";
   //    intCtr++;
   //   }

   //  }
   //  else if (clsFinanceApprover.IsEndorder2(Request.Cookies["Speedo"]["UserName"], "catacode", drw["catacode"].ToString(), "CATARequest"))
   //  {
   //   if (drw["endrstt2"].ToString().Trim() == "2")
   //   {
   //    strWrite = strWrite + "<tr>" +
   //               "<td class='GridRows'></td>" +
   //               "<td class='GridRows' colspan=2'>" +
   //                "<a href='CATADetailsApprover.aspx?catacode=" + drw["catacode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drw["trpprpse"].ToString()) + "</a>" +
   //                 "<br>CATA Number: " + drw["catacode"].ToString() +
   //                "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
   //                 "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
   //                "<br>Date check needed: " + Convert.ToDateTime(drw["dateneed"]).ToString("MMMM dd, yyyy") +
   //               "</td>" +
   //              "</tr>";
   //    intCtr++;
   //   }
   //  }
   //  else
   //  {
   //   strWrite = strWrite + "<tr>" +
   //                       "<td class='GridRows'></td>" +
   //                       "<td class='GridRows' colspan=2'>" +
   //                        "<a href='CATADetailsApprover.aspx?catacode=" + drw["catacode"].ToString() + " 'style='font-size:small;'>" + CheckLength(drw["trpprpse"].ToString()) + "</a>" +
   //                         "<br>CATA Number: " + drw["catacode"].ToString() +
   //                        "<br>Date Requested: " + Convert.ToDateTime(drw["createon"]).ToString("MMMM dd, yyyy") +
   //                         "<br>Requested by: <a href='../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + drw["createby"] + "</a>" +
   //                        "<br>Date check needed: " + Convert.ToDateTime(drw["dateneed"]).ToString("MMMM dd, yyyy") +
   //                       "</td>" +
   //                      "</tr>";
   //   intCtr++;
   //  }


   //  if (intCtr == 10)
   //  { break; }
   // }

   // Response.Write(strWrite);
   // if (intCtr == 0)
   //  Response.Write("<tr><td colspan='3' class='GridRows'>No record found</td></tr>");
   // else
   //  Response.Write("<tr><td colspan='3' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
   //}
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

 protected void btnNewRequest_Click(object sender, EventArgs e)
 {
  Response.Redirect("FinanceNewCataRequest.aspx");
 }

 protected void btnBudgetDetails_Click(object sender, EventArgs e)
 {
     Response.Redirect("FinanceCataMenuFinance.aspx");
 }

 protected void btnNewRequestForExecutive_Click(object sender, EventArgs e)
 {
     Response.Redirect("FinanceNewCataRequestExec.aspx");
 }
}

