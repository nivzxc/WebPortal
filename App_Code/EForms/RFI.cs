using System;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class RFI
 {

  public enum RFIUserType { Requestor, Approver, ProcurementManager, ProcurementOfficer }

  public enum RFIMailType
  {
   SentToRequestor,
   SentToApprover,
   SentToApproverPM,
   SentToApproverPO,
   ApproveToRequestor,
   ApproveToApprover,
   ApproveToApproverPM,
   ApproveToApproverPO,
   DisapproveToRequestor,
   DisapproveToApprover,
   DisapproveToApproverPM,
  }

  private string _strRFICode;
  private string _strUsername;
  private DateTime _dteDateRequested;
  private string _strIntended;
  private string _strApprover;
  private string _strApproverStatus;
  private string _strApproverRemarks;
  private DateTime _dteApproverDate;
  private string _strProcurementManager;
  private string _strProcurementManagerStatus;
  private string _strProcurementManagerRemarks;
  private DateTime _dteProcurementManagerDate;
  private string _strStatus;

  public string RFICode { get { return _strRFICode; } set { _strRFICode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime DateRequested { get { return _dteDateRequested; } set { _dteDateRequested = value; } }
  public string Intended { get { return _strIntended; } set { _strIntended = value; } }
  public string Approver { get { return _strApprover; } set { _strApprover = value; } }
  public string ApproverStatus { get { return _strApproverStatus; } set { _strApproverStatus = value; } }
  public string ApproverRemarks { get { return _strApproverRemarks; } set { _strApproverRemarks = value; } }
  public DateTime ApproverDate { get { return _dteApproverDate; } set { _dteApproverDate = value; } }
  public string ProcurementManager { get { return _strProcurementManager; } set { _strProcurementManager = value; } }
  public string ProcurementManagerStatus { get { return _strProcurementManagerStatus; } set { _strProcurementManagerStatus = value; } }
  public string ProcurementManagerRemarks { get { return _strProcurementManagerRemarks; } set { _strProcurementManagerRemarks = value; } }
  public DateTime ProcurementManagerDate { get { return _dteProcurementManagerDate; } set { _dteProcurementManagerDate = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }
  
  public RFI()
  {
  
  }

  public string StatusDescription
  {
   get
   {
    switch (_strStatus)
    {
     case "F":
      return "For Approval";
     case "V":
      return "Void";
     case "A":
      return "Approved";
     case "D":
      return "Disapproved";
     case "M":
      return "For Modification";
     case "N":
      return "Not Applicable";
     default:
      return "Unknown";
    }
   }
  }

  public void DisapproveApprover()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.RFI SET status='D', apprstat='D', procstat='X', apprdate=@apprdate, apprrem=@apprrem WHERE rficode=@rficode";
    cmd.Parameters.Add(new SqlParameter("@apprdate", DateTime.Now));
    cmd.Parameters.Add(new SqlParameter("@apprrem", _strApproverRemarks));
    cmd.Parameters.Add(new SqlParameter("@rficode", _strRFICode));  
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void DisapprovePM(string pPMRemarks, string pGHStatus, string pDHStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.RFI SET status='D', procstat='D', apprstat=@apprstat, procdate=@procdate, procrem=@procrem WHERE rficode=@rficode";
    cmd.Parameters.Add(new SqlParameter("@apprstat", (_strApproverStatus == "F" ? "N" : _strApproverStatus)));
    cmd.Parameters.Add(new SqlParameter("@procdate", DateTime.Now));
    cmd.Parameters.Add(new SqlParameter("@procrem", _strProcurementManagerRemarks));
    cmd.Parameters.Add(new SqlParameter("@rficode", _strRFICode));  
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM CIS.RFI WHERE rficode=@rficode";
    cmd.Parameters.Add(new SqlParameter("@rficode", _strRFICode));  
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();     
     _dteDateRequested = clsValidator.CheckDate((dr["datereq"].ToString()));
     _strIntended = dr["intended"].ToString();
     _strApprover = dr["apprcode"].ToString();
     _strApproverStatus = dr["apprstat"].ToString();
     _strApproverRemarks = dr["apprrem"].ToString();
     _dteApproverDate = clsValidator.CheckDate(dr["apprdate"].ToString());
     _strProcurementManager = dr["proccode"].ToString();
     _strProcurementManagerStatus = dr["procstat"].ToString();
     _strProcurementManagerRemarks = dr["procrem"].ToString();
     _dteProcurementManagerDate = clsValidator.CheckDate(dr["procdate"].ToString());
     _strStatus = dr["status"].ToString();
    }
    dr.Close();
   }
  }

  public void Void()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.RFI SET status='V' WHERE rficode=@rficode";
    cmd.Parameters.Add(new SqlParameter("@rficode", _strRFICode));
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public int Insert(DataTable pRequestedItems)
  {
   int intReturn = 0;
   int intSeed = 0;

   SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF);
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   cmd.Transaction = tran;
   try
   {
    cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='rficode'";
    intSeed = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString());
    _strRFICode = ("000000000" + intSeed.ToString()).Substring(intSeed.ToString().Length);

    cmd.CommandText = "INSERT INTO CIS.RFI(rficode,username,datereq,intended,apprcode,apprstat,proccode,procstat,status) VALUES(@rficode,@username,@datereq,@intended,@apprcode,@apprstat,@proccode,@procstat,@status)";
    cmd.Parameters.Add(new SqlParameter("@rficode", _strRFICode));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@datereq", DateTime.Now));
    cmd.Parameters.Add(new SqlParameter("@intended", _strIntended));
    cmd.Parameters.Add(new SqlParameter("@apprcode", _strApprover));
    cmd.Parameters.Add(new SqlParameter("@apprstat", _strApproverStatus));
    cmd.Parameters.Add(new SqlParameter("@proccode", _strProcurementManager));
    cmd.Parameters.Add(new SqlParameter("@procstat", _strProcurementManagerStatus));
    cmd.Parameters.Add(new SqlParameter("@status", "F"));
    intReturn = cmd.ExecuteNonQuery();
    cmd.Parameters.Clear();

    cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='rficode'";
    cmd.ExecuteNonQuery();

    foreach (DataRow drw in pRequestedItems.Rows)
    {
     cmd.CommandText = "INSERT INTO CIS.RFIDetails(rficode,itemdesc,itemdtls,dateneed,status) VALUES(@rficode,@itemdesc,@itemdtls,@dateneed,@status)";
     cmd.Parameters.Add(new SqlParameter("@rficode", _strRFICode));
     cmd.Parameters.Add(new SqlParameter("@itemdesc", drw["ItemDesc"].ToString()));
     cmd.Parameters.Add(new SqlParameter("@itemdtls", drw["ItemDetails"].ToString()));
     cmd.Parameters.Add(new SqlParameter("@dateneed", clsValidator.CheckDate(drw["DateNeeded"].ToString())));
     cmd.Parameters.Add(new SqlParameter("@status", "F"));     
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();
    }

    tran.Commit();
   }
   catch
   {
    tran.Rollback();
   }
   finally
   {
    cn.Close();
   }

   return intReturn;
  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static readonly string ProcurementHead = "erwin.torres";

  public static void AuthenticateUser(RFIUserType pRFIUserType, string pUsername, string pRFICode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pRFIUserType == RFIUserType.Requestor)
     cmd.CommandText = "SELECT username FROM CIS.RFI WHERE rficode='" + pRFICode + "' AND username='" + pUsername + "'";
    else if (pRFIUserType == RFIUserType.Approver)
     cmd.CommandText = "SELECT apprcode FROM CIS.RFI WHERE rficode='" + pRFICode + "' AND apprcode='" + pUsername + "'";
    else if (pRFIUserType == RFIUserType.ProcurementManager)
     cmd.CommandText = "SELECT proccode FROM CIS.RFI WHERE rficode='" + pRFICode + "' AND proccode='" + pUsername + "'";
    else if (pRFIUserType == RFIUserType.ProcurementOfficer)
     cmd.CommandText = "SELECT poffname FROM CIS.RFIDetails WHERE rficode='" + pRFICode + "' AND poffname='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }

   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static string GetStatusDescription(string pStatus)
  {
   switch (pStatus)
   {
    case "F":
     return "For Approval";
    case "V":
     return "Void";
    case "A":
     return "Approved";
    case "D":
     return "Disapproved";
    case "C":
      return "Completed";
    case "N":
     return "Not Applicable";
    default:
     return "Unknown";
   }
  }

  public static void SendNotification(RFIMailType pMailType, string pRequestorName, string pApproverName, string pMailTo, string pRFIcode)
  {
   string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   string strSubject = "";
   string strBody = "";

   switch (pMailType)
   {
    case RFIMailType.SentToRequestor:
     strSubject = "Delivered: RFI";
     strBody = "Hi " + pRequestorName + ",<br><br>" +
               "Your RFI has been delivered to " + pApproverName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetails.aspx?rficode=" + pRFIcode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetails.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.SentToApprover:
     strSubject = "For Your Approval: RFI";
     strBody = "Hi " + pApproverName + ",<br><br>" +
               "There is an RFI submitted by " + pRequestorName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetailsA.aspx?rficode=" + pRFIcode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetailsA.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.SentToApproverPM:
     strSubject = "For Your Approval: RFI";
     strBody = "Hi " + pApproverName + ",<br><br>" +
               "There is an RFI submitted by " + pRequestorName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetailsPM.aspx?rficode=" + pRFIcode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetailsPM.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.ApproveToRequestor:
     strSubject = "Approved RFI";
     strBody = "Hi " + pRequestorName + ",<br><br>" +
               "Your RFI has been approved by " + pApproverName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetails.aspx?rficode=" + pRFIcode + "'>Click here to review the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetails.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.ApproveToApprover:
     strSubject = "Approved RFI";
     strBody = "You approved an RFI.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetailsGH.aspx?rficode=" + pRFIcode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetailsA.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.ApproveToApproverPM:
     strSubject = "Approved RFI";
     strBody = "You approved an RFI.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetailsPM.aspx?rficode=" + pRFIcode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetailsPM.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.DisapproveToRequestor:
     strSubject = "Disapproved RFI";
     strBody = "Hi " + pRequestorName + ",<br><br>" +
               "Your RFI has been disapproved by " + pApproverName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetails.aspx?rficode=" + pRFIcode + "'>Click here to review the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetails.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.DisapproveToApprover:
     strSubject = "Disapproved RFI";
     strBody = "You disapproved an RFI.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetailsGH.aspx?rficode=" + pRFIcode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetailsA.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case RFIMailType.DisapproveToApproverPM:
     strSubject = "Disapproved RFI";
     strBody = "You disapproved an RFI.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/RFI/RFIDetailsPM.aspx?rficode=" + pRFIcode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/RFI/RFIDetailsPM.aspx?rficode=" + pRFIcode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

   }
   clsSpeedo.SendMail(pMailTo, strSubject, strBody);
  }

  public static string GetRequestStatusIcon(string pUserStatus)
  {
   string strReturn = "";

   if (pUserStatus == "V")
    strReturn = "Disapproved.png";
   else if (pUserStatus == "D")
    strReturn = "Disapproved.png";
   else if (pUserStatus == "F")
    strReturn = "Approval.png";
   else if (pUserStatus == "A")
    strReturn = "Approved.png";
   else if (pUserStatus == "C")
    strReturn = "Approved.png";

   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pUserStatus, string pApprover, string pApproverStatus, string pPM, string pPMStatus)
  {
   string strReturn = "";

   if (pUserStatus == "V")
   {
    strReturn = "The requestor voided the request";
   }
   else if (pUserStatus == "D")
   {
    if (pApproverStatus == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pApprover, 2);
    else if (pPMStatus == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pPM, 2);
   }
   else if (pUserStatus == "F")
   {
    if (pApproverStatus == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pApprover, 2);
    else if (pPMStatus == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pPM, 2);
   }
   else if (pUserStatus == "A")
   {
    strReturn = "Approved by " + clsSpeedo.AssignUsernameLink(pPM, 2);
   }

   return strReturn;
  }

 }
}
