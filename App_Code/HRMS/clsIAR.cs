using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRMS
{
 public enum IARUsers { Requestor, ApproverHead, ApproverDivision, EliteUser }
 public enum IARStatus { Cancelled, ForApproval, Approved, Disapproved }
 public enum IARMailType
 {
  FiledAcknowledgementHRequestor,
  FiledAcknowledgementDRequestor,
  FiledNotificationHApprover,
  FiledNotificationDApprover,
  ApprovedAcknowledgementHApprover,
  ApprovedAcknowledgementDApprover,
  ApprovedNotificationHRequestor,
  ApprovedNotificationDRequestor,
  DisapprovedAcknowledgementHApprover,
  DisapprovedAcknowledgementDApprover,
  DisapprovedNotificationHRequestor,
  DisapprovedNotificationDRequestor,
  ForwardtoIT
 }

 public class clsIAR : IDisposable
 {
  private string _strIARCode;
  private string _strUsername;
  private DateTime _dteDateFile;
  private DateTime _dteDateStart;
  private DateTime _dteDateEnd;
  private string _strReason;
  private string _strApproverHeadName;
  private DateTime _dteApproverHeadDate;
  private string _strApproverHeadRemarks;
  private string _strApproverHeadStatus;
  private string _strApproverDivisionName;
  private DateTime _dteApproverDivisionDate;
  private string _strApproverDivisionRemarks;
  private string _strApproverDivisionStatus;
  private string _strStatus;
  private string _strCreateBy;
  private DateTime _dteCreateOn;
  private string _strModifyBy;
  private DateTime _dteModifyOn;

  public clsIAR() { }

  public string IARCode { get { return _strIARCode; } set { _strIARCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime DateFile { get { return _dteDateFile; } set { _dteDateFile = value; } }
  public DateTime DateStart { get { return _dteDateStart; } set { _dteDateStart = value; } }
  public DateTime DateEnd { get { return _dteDateEnd; } set { _dteDateEnd = value; } }
  public string Reason { get { return _strReason; } set { _strReason = value; } }
  public string ApproverHeadName { get { return _strApproverHeadName; } set { _strApproverHeadName = value; } }
  public DateTime ApproverHeadDate { get { return _dteApproverHeadDate; } set { _dteApproverHeadDate = value; } }
  public string ApproverHeadRemarks { get { return _strApproverHeadRemarks; } set { _strApproverHeadRemarks = value; } }
  public string ApproverHeadStatus { get { return _strApproverHeadStatus; } set { _strApproverHeadStatus = value; } }
  public string ApproverDivisionName { get { return _strApproverDivisionName; } set { _strApproverDivisionName = value; } }
  public DateTime ApproverDivisionDate { get { return _dteApproverDivisionDate; } set { _dteApproverDivisionDate = value; } }
  public string ApproverDivisionRemarks { get { return _strApproverDivisionRemarks; } set { _strApproverDivisionRemarks = value; } }
  public string ApproverDivisionStatus { get { return _strApproverDivisionStatus; } set { _strApproverDivisionStatus = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }
  public string CreateBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
  public DateTime CreateOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
  public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
  public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.IAR WHERE iarcode='" + _strIARCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _dteDateFile = clsValidator.CheckDate(dr["datefile"].ToString());
     _dteDateStart = clsValidator.CheckDate(dr["datestrt"].ToString());
     _dteDateEnd = clsValidator.CheckDate(dr["dateend"].ToString());
     _strReason = dr["reason"].ToString();
     _strApproverHeadName = dr["apphname"].ToString();
     _dteApproverHeadDate = clsValidator.CheckDate(dr["apphdate"].ToString());
     _strApproverHeadRemarks = dr["apphrem"].ToString();
     _strApproverHeadStatus = dr["apphstat"].ToString();
     _strApproverDivisionName = dr["appdname"].ToString();
     _dteApproverDivisionDate = clsValidator.CheckDate(dr["appddate"].ToString());
     _strApproverDivisionRemarks = dr["appdrem"].ToString();
     _strApproverDivisionStatus = dr["appdstat"].ToString();
     _strStatus = dr["status"].ToString();
     _strCreateBy = dr["createby"].ToString();
     _dteCreateOn = clsValidator.CheckDate(dr["createon"].ToString());
     _strModifyBy = dr["modifyby"].ToString();
     _dteModifyOn = clsValidator.CheckDate(dr["modifyon"].ToString());
    }
    dr.Close();
   }
  }

  public int Insert()
  {
   int intReturn = 0;
   int intSeed = 0;
   SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString);
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   cmd.Transaction = tran;
   try
   {
    cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='iarcode'";
    intSeed = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString());
    _strIARCode = ("000000000" + intSeed.ToString()).Substring(intSeed.ToString().Length);

    cmd.CommandText = "INSERT INTO HR.IAR(iarcode,username,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,createby,createon,modifyby,modifyon) VALUES(@iarcode,@username,@datefile,@datestrt,@dateend,@reason,@apphname,@apphstat,@appdname,@appdstat,@status,@createby,@createon,@modifyby,@modifyon)";
    cmd.Parameters.Add("@iarcode", SqlDbType.Char, 9);
    cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@datefile", SqlDbType.DateTime);
    cmd.Parameters.Add("@datestrt", SqlDbType.DateTime);
    cmd.Parameters.Add("@dateend", SqlDbType.DateTime);
    cmd.Parameters.Add("@reason", SqlDbType.VarChar, 500);
    cmd.Parameters.Add("@apphname", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@apphstat", SqlDbType.Char, 1);
    cmd.Parameters.Add("@appdname", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@appdstat", SqlDbType.Char, 1);
    cmd.Parameters.Add("@status", SqlDbType.Char, 1);
    cmd.Parameters.Add("@createby", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@createon", SqlDbType.DateTime);
    cmd.Parameters.Add("@modifyby", SqlDbType.VarChar, 30);
    cmd.Parameters.Add("@modifyon", SqlDbType.DateTime);
    cmd.Parameters["@iarcode"].Value = _strIARCode;
    cmd.Parameters["@username"].Value = _strUsername;
    cmd.Parameters["@datefile"].Value = _dteDateFile;
    cmd.Parameters["@datestrt"].Value = _dteDateStart;
    cmd.Parameters["@dateend"].Value = _dteDateEnd;
    cmd.Parameters["@reason"].Value = _strReason;
    cmd.Parameters["@apphname"].Value = _strApproverHeadName;
    cmd.Parameters["@apphstat"].Value = _strApproverHeadStatus;
    cmd.Parameters["@appdname"].Value = _strApproverDivisionName;
    cmd.Parameters["@appdstat"].Value = _strApproverDivisionStatus;
    cmd.Parameters["@status"].Value = _strStatus;
    cmd.Parameters["@createby"].Value = _strCreateBy;
    cmd.Parameters["@createon"].Value = _dteCreateOn;
    cmd.Parameters["@modifyby"].Value = _strCreateBy;
    cmd.Parameters["@modifyon"].Value = _dteCreateOn;
    intReturn = cmd.ExecuteNonQuery();
    cmd.Parameters.Clear();

    cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='iarcode'";
    cmd.ExecuteNonQuery();

    tran.Commit();
   }
   catch { tran.Rollback(); }
   finally { cn.Close(); }

   return intReturn;
  }

  public int Cancel()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.IAR SET status='C' WHERE iarcode='" + _strIARCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int ApproveHead()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.IAR SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A' WHERE iarcode='" + _strIARCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(IARMailType.ApprovedAcknowledgementHApprover);
    SendNotification(IARMailType.ApprovedNotificationHRequestor);
    SendNotification(IARMailType.FiledNotificationDApprover);
   }
   return intReturn;
  }

  public int ApproveDivision()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.IAR SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "', appdstat='A', status='A' WHERE iarcode='" + _strIARCode + "'";
    cmd.Parameters.Add("@appdrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@appdrem"].Value = _strApproverDivisionRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(IARMailType.ApprovedAcknowledgementDApprover);
    SendNotification(IARMailType.ApprovedNotificationDRequestor);
    SendNotification(IARMailType.ForwardtoIT);
   }
   return intReturn;
  }

  public int DisapproveHead()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.IAR SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='D', status='D' WHERE iarcode='" + _strIARCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(IARMailType.DisapprovedAcknowledgementHApprover);
    SendNotification(IARMailType.DisapprovedNotificationHRequestor);
   }
   return intReturn;
  }

  public int DisapproveDivision()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.IAR SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "', appdstat='D', status='D' WHERE iarcode='" + _strIARCode + "'";
    cmd.Parameters.Add("@appdrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@appdrem"].Value = _strApproverDivisionRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(IARMailType.DisapprovedAcknowledgementDApprover);
    SendNotification(IARMailType.DisapprovedNotificationDRequestor);
   }
   return intReturn;
  }

  public void SendNotification(IARMailType pMailType)
  {
   string strSpeedoUrl = ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   string strSubject = "";
   string strBody = "";
   string strRequestorName = clsEmployee.GetName(_strUsername);
   string strRequestorEmail = clsUsers.GetEmail(_strUsername);
   string strHApproverName = clsEmployee.GetName(_strApproverHeadName);
   string strHApproverEmail = clsUsers.GetEmail(_strApproverHeadName);
   string strDApproverName = clsEmployee.GetName(_strApproverDivisionName);
   string strDApproverEmail = clsUsers.GetEmail(_strApproverDivisionName);
   string strURLIARDetails = strSpeedoUrl + "/HR/HRMS/IAR/IARDetails.aspx?iarcode=" + _strIARCode;
   string strURLIARDetailsH = strSpeedoUrl + "/HR/HRMS/IAR/IARDetailsH.aspx?iarcode=" + _strIARCode;
   string strURLIARDetailsD = strSpeedoUrl + "/HR/HRMS/IAR/IARDetailsD.aspx?iarcode=" + _strIARCode;
   string strURLIARDetailsIT = strSpeedoUrl + "/HR/HRMS/IAR/IARDetailsIT.aspx?iarcode=" + _strIARCode;
   switch (pMailType)
   {
    case IARMailType.FiledAcknowledgementHRequestor:
     strSubject = "Delivered: Internet Access Request";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Internet Access Request has been successfully sent to " + strHApproverName + ".<br>" +
               "** Internet Access Request Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br><br>" +
               "<a href='" + strURLIARDetails + "'>Click here to view your online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case IARMailType.FiledAcknowledgementDRequestor:
     strSubject = "Delivered: Internet Access Request";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Internet Access Request has been successfully sent to " + strDApproverName + ".<br>" +
               "** Internet Access Request Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br><br>" +
               "<a href='" + strURLIARDetails + "'>Click here to view your online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case IARMailType.FiledNotificationHApprover:
     strSubject = "For Your Approval: Internet Access Request - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               strRequestorName + " has just sent an Internet Access Request with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br><br>" +
               "<a href='" + strURLIARDetailsH + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetailsH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case IARMailType.FiledNotificationDApprover:
     strSubject = "For Your Approval: Internet Access Request - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               strRequestorName + " has just sent an Internet Access Request with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "<a href='" + strURLIARDetailsD + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetailsD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;

    case IARMailType.ApprovedAcknowledgementHApprover:
     strSubject = "Delivered: Approved Internet Access Request - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You approved an Internet Access Request.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLIARDetailsH + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetailsH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case IARMailType.ApprovedAcknowledgementDApprover:
     strSubject = "Delivered: Approved Internet Access Request - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               "You approved an Internet Access Request.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLIARDetailsD + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetailsD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;

    case IARMailType.ApprovedNotificationHRequestor:
     strSubject = "Approved: Internet Access Request";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has approved your Internet Access Request.<br>" +
               "Your Internet Access Request has been forwarded to " + strDApproverName + " for final approval.<br><br>" +
               "<a href='" + strURLIARDetails + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case IARMailType.ApprovedNotificationDRequestor:
     strSubject = "Approved: Internet Access Request";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strDApproverName + " has approved your Internet Access Request.<br><br>" +
               "<a href='" + strURLIARDetails + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case IARMailType.ForwardtoIT:
     strSubject = "Approved: Internet Access Request";
     strBody = "Hi IT,<br><br>" +
               strRequestorName + " has an approved Internet Access Request.<br><br>" +
               "<a href='" + strURLIARDetailsIT + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetailsIT + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail("ithelpdesk@stihq.net", strSubject, strBody);
     break;

    case IARMailType.DisapprovedAcknowledgementHApprover:
     strSubject = "Delivered: Disapproved Internet Access Request - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You disapproved an Internet Access Request.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLIARDetailsH + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetailsH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case IARMailType.DisapprovedAcknowledgementDApprover:
     strSubject = "Delivered: Disapproved Internet Access Request - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               "You disapproved an Internet Access Request.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLIARDetailsD + "'>Click here to view the online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetailsD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;


    case IARMailType.DisapprovedNotificationHRequestor:
     strSubject = "Disapproved: Internet Access Request";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has disapproved your Internet Access Request.<br><br>" +
               "<a href='" + strURLIARDetails + "'>Click here to view your online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case IARMailType.DisapprovedNotificationDRequestor:
     strSubject = "Disapproved: Internet Access Request";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strDApproverName + " has disapproved your Internet Access Request.<br><br>" +
               "<a href='" + strURLIARDetails + "'>Click here to view your online Internet Access Request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLIARDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;



   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static void AuthenticateAccessForm(IARUsers pIARUsers, string pUsername, string pIARCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pIARUsers == IARUsers.Requestor)
     cmd.CommandText = "SELECT username FROM HR.IAR WHERE iarcode='" + pIARCode + "' AND username='" + pUsername + "'";
    else if (pIARUsers == IARUsers.ApproverHead)
     cmd.CommandText = "SELECT apphname FROM HR.IAR WHERE iarcode='" + pIARCode + "' AND apphname='" + pUsername + "'";
    else if (pIARUsers == IARUsers.ApproverDivision)
     cmd.CommandText = "SELECT appdname FROM HR.IAR WHERE iarcode='" + pIARCode + "' AND appdname='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }

   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static bool AuthenticateAccess(string pUsername, string pIARCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM HR.IAR WHERE iarcode='" + pIARCode + "' AND (username='" + pUsername + "' OR apphname='" + pUsername + "' OR appdname='" + pUsername + "')";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable GetDSGApproved(DateTime pDateStart, DateTime pDateEnd)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT iarcode,username,datefile,datestrt,dateend,reason FROM HR.IAR WHERE status='A' AND ((datestrt BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') OR (dateend BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "')) ORDER BY datestrt DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDSGMenu(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 10 iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username FROM HR.IAR WHERE username='" + pUsername + "' ORDER BY datefile DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDSGMenuHead(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 10 iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username FROM HR.IAR WHERE apphname='" + pUsername + "' AND apphstat='F' AND status='F' ORDER BY datefile DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDSGMenuDivision(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 10 iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username FROM HR.IAR WHERE appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A'  AND status='F' ORDER BY datefile DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetRequestStatusIcon(string pIARStatus)
  {
   string strReturn = "";
   if (pIARStatus == "V")
    strReturn = "Disapproved.png";
   else if (pIARStatus == "D")
    strReturn = "Disapproved.png";
   else if (pIARStatus == "F")
    strReturn = "Approval.png";
   else if (pIARStatus == "A")
    strReturn = "Approved.png";
   else if (pIARStatus == "C")
    strReturn = "Disapproved.png";
   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pIARStatus, string pAHName, string pAHStatus, string pADName, string pADStatus)
  {
   string strReturn = "";
   if (pIARStatus == "D")
    strReturn = "Disapproved by " + (pAHStatus != "D" && pADStatus != "D" ? " System Administrator" : clsSpeedo.AssignUsernameLink((pAHStatus == "D" ? pAHName : pADName), 3));
   else if (pIARStatus == "C")
    strReturn = "The application has been cancelled";
   else if (pIARStatus == "F")
    strReturn = "For approval of " + clsSpeedo.AssignUsernameLink((pAHStatus == "F" ? pAHName : pADName), 3);
   else if (pIARStatus == "A")
    strReturn = "Approved";
   return strReturn;
  }

  public static int GetTotalForAttention(string pUsername)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(iarcode) FROM HR.IAR WHERE (apphname='" + pUsername + "' AND apphstat='F' AND status='F') OR (appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A' AND status='F')";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intReturn = 0; }
   }
   return intReturn;
  }

  public static IARStatus ToIARStatusDesc(string pIARStatusCode)
  {
   switch (pIARStatusCode)
   {
    case "C":
     return IARStatus.Cancelled;
    case "F":
     return IARStatus.ForApproval;
    case "A":
     return IARStatus.Approved;
    case "D":
     return IARStatus.Disapproved;
    default:
     return IARStatus.ForApproval;
   }
  }

  public static string ToIARStatus(string pIARStatusCode)
  {
   switch (pIARStatusCode)
   {
    case "C":
     return "Cancelled";
    case "F":
     return "For Approval";
    case "A":
     return "Approved";
    case "D":
     return "Disapproved";
    default:
     return "Cancelled";
   }
  }

  ///////// Web Methods /////////

  public static DataTable GetPageRecords(IARUsers pIARUsers, int pPage, string pUsername, string pStatus)
  {
   DataTable tblReturn = new DataTable();
   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intStart = ((pPage - 1) * intPageSize) + 1;
   int intEnd = pPage * intPageSize;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pStatus == "ALL")
    {
     if (pIARUsers == IARUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.IAR WHERE username='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pIARUsers == IARUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.IAR WHERE apphname='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pIARUsers == IARUsers.ApproverDivision)
      cmd.CommandText = "SELECT * FROM (SELECT iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.IAR WHERE appdname='" + pUsername + "' AND apphstat='A') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    else
    {
     if (pIARUsers == IARUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.IAR WHERE username='" + pUsername + "' AND status='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pIARUsers == IARUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.IAR WHERE apphname='" + pUsername + "' AND apphstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pIARUsers == IARUsers.ApproverDivision)
      cmd.CommandText = "SELECT * FROM (SELECT iarcode,datefile,datestrt,dateend,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.IAR WHERE appdname='" + pUsername + "' AND appdstat='" + pStatus + "' AND apphstat='A') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetPaging(IARUsers pIARUsers, int pPage, string pUsername, string pStatus, string pPageName)
  {
   string strReturn = "";

   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intTRows = 0;
   int intTRowsTemp = 0;
   int intPage = 1;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pIARUsers == IARUsers.Requestor)
     cmd.CommandText = "SELECT COUNT(iarcode) FROM HR.IAR WHERE username='" + pUsername + "'" + (pStatus == "ALL" ? "" : " AND status='" + pStatus + "'");
    else if (pIARUsers == IARUsers.ApproverHead)
     cmd.CommandText = "SELECT COUNT(iarcode) FROM HR.IAR WHERE apphname='" + pUsername + "'" + (pStatus == "ALL" ? "" : " AND apphstat='" + pStatus + "'");
    else if (pIARUsers == IARUsers.ApproverDivision)
     cmd.CommandText = "SELECT COUNT(iarcode) FROM HR.IAR WHERE appdname='" + pUsername + "'" + (pStatus == "ALL" ? "" : " AND apphstat='A' AND appdstat='" + pStatus + "'");
    cn.Open();
    try { intTRows = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intTRows = 0; }
   }

   intTRowsTemp = intTRows;
   while (intTRowsTemp > 0)
   {
    if (pPage == intPage)
     strReturn += (intPage == 1 ? "" : ",") + "&nbsp;" + intPage;
    else
     strReturn += "&nbsp;&nbsp;<a href='" + pPageName + ".aspx?page=" + intPage + "'>" + intPage + "</a>";
    intPage++;
    intTRowsTemp -= intPageSize;
   }

   return strReturn;
  }

  public static int GetTotalRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(iarcode) FROM HR.IAR";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

     //Added By Charlie Bachiller
  public static DataTable GetNotificationForApproval(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT * FROM HR.IAR WHERE (apphname='" + pUsername + "' AND apphstat='F' AND status='F') OR (appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A' AND status='F')";
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

 }
}