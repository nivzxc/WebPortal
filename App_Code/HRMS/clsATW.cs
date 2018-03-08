using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRMS
{
 public enum ATWUsers { Requestor, ApproverHead, ApproverDivision }
 public enum ATWStatus { Cancelled, ForApproval, Approved, Disapproved }
 public enum ATWMailType
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
  DisapprovedNotificationDRequestor
 }

 public class clsATW : IDisposable
 {
  private string _strATWCode;
  private string _strUsername;
  private DateTime _dteDateFile;
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

  public clsATW() { }

  public string ATWCode { get { return _strATWCode; } set { _strATWCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime DateFile { get { return _dteDateFile; } set { _dteDateFile = value; } }
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
    cmd.CommandText = "SELECT * FROM HR.ATW WHERE atwcode='" + _strATWCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _dteDateFile = clsValidator.CheckDate(dr["datefile"].ToString());
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

  public int Insert(DataTable pATWDetails)
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
        cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='atwcode'";
        intSeed = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString());
        _strATWCode = ("000000000" + intSeed.ToString()).Substring(intSeed.ToString().Length);

        cmd.CommandText = "INSERT INTO HR.ATW(atwcode,username,datefile,reason,apphname,apphstat,appdname,appdstat,status,createby,createon,modifyby,modifyon) VALUES(@atwcode,@username,@datefile,@reason,@apphname,@apphstat,@appdname,@appdstat,@status,@createby,@createon,@modifyby,@modifyon)";
        cmd.Parameters.Add("@atwcode", SqlDbType.Char, 9);
        cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
        cmd.Parameters.Add("@datefile", SqlDbType.DateTime);
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
        cmd.Parameters["@atwcode"].Value = _strATWCode;
        cmd.Parameters["@username"].Value = _strUsername;
        cmd.Parameters["@datefile"].Value = _dteDateFile;
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

        cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='atwcode'";
        cmd.ExecuteNonQuery();

        int intATWDCodeSeed = 1;
        cmd.CommandText = "INSERT INTO HR.ATWDetails VALUES(@atwdcode,@atwcode,@datestrt,@dateend,@reason,@remarks,@status,@modifyby,@modifyon)";
        cmd.Parameters.Add("@atwdcode", SqlDbType.Char, 11);
        cmd.Parameters.Add("@atwcode", SqlDbType.Char, 9);
        cmd.Parameters.Add("@datestrt", SqlDbType.DateTime);
        cmd.Parameters.Add("@dateend", SqlDbType.DateTime);
        cmd.Parameters.Add("@reason", SqlDbType.VarChar, 500);
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 500);
        cmd.Parameters.Add("@status", SqlDbType.Char, 1);
        cmd.Parameters.Add("@modifyby", SqlDbType.VarChar, 30);
        cmd.Parameters.Add("@modifyon", SqlDbType.DateTime);

        foreach (DataRow drw in pATWDetails.Rows)
        {
         cmd.Parameters["@atwdcode"].Value = _strATWCode + ("00" + intATWDCodeSeed).Substring(intATWDCodeSeed.ToString().Length);
         cmd.Parameters["@atwcode"].Value = _strATWCode;
         cmd.Parameters["@datestrt"].Value = clsValidator.CheckDate(drw["datestrt"].ToString());
         cmd.Parameters["@dateend"].Value = clsValidator.CheckDate(drw["dateend"].ToString());
         cmd.Parameters["@reason"].Value = drw["reason"].ToString();
         cmd.Parameters["@remarks"].Value = "";
         cmd.Parameters["@status"].Value = "1";
         cmd.Parameters["@modifyby"].Value = _strUsername;
         cmd.Parameters["@modifyon"].Value = DateTime.Now;
         cmd.ExecuteNonQuery();
         intATWDCodeSeed++;
        }

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
    cmd.CommandText = "UPDATE HR.ATW SET status='C' WHERE atwcode='" + _strATWCode + "'";
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
    cmd.CommandText = "UPDATE HR.ATW SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A' WHERE atwcode='" + _strATWCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(ATWMailType.ApprovedAcknowledgementHApprover);
    SendNotification(ATWMailType.ApprovedNotificationHRequestor);
    SendNotification(ATWMailType.FiledNotificationDApprover);
   }
   return intReturn;
  }

  public int ApproveDivision()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.ATW SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "', appdstat='A', status='A' WHERE atwcode='" + _strATWCode + "'";
    cmd.Parameters.Add("@appdrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@appdrem"].Value = _strApproverDivisionRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(ATWMailType.ApprovedAcknowledgementDApprover);
    SendNotification(ATWMailType.ApprovedNotificationDRequestor);
   }
   return intReturn;
  }

  public int DisapproveHead()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.ATW SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='D', status='D' WHERE atwcode='" + _strATWCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(ATWMailType.DisapprovedAcknowledgementHApprover);
    SendNotification(ATWMailType.DisapprovedNotificationHRequestor);
   }
   return intReturn;
  }

  public int DisapproveDivision()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.ATW SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "', appdstat='D', status='D' WHERE atwcode='" + _strATWCode + "'";
    cmd.Parameters.Add("@appdrem", SqlDbType.VarChar, 500);
    cmd.Parameters["@appdrem"].Value = _strApproverDivisionRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   if (intReturn > 0)
   {
    SendNotification(ATWMailType.DisapprovedAcknowledgementDApprover);
    SendNotification(ATWMailType.DisapprovedNotificationDRequestor);
   }
   return intReturn;
  }

  public void SendNotification(ATWMailType pMailType)
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
   string strURLATWDetails = strSpeedoUrl + "/HR/HRMS/ATW/ATWDetails.aspx?atwcode=" + _strATWCode;
   string strURLATWDetailsH = strSpeedoUrl + "/HR/HRMS/ATW/ATWDetailsH.aspx?atwcode=" + _strATWCode;
   string strURLATWDetailsD = strSpeedoUrl + "/HR/HRMS/ATW/ATWDetailsD.aspx?atwcode=" + _strATWCode;

   switch (pMailType)
   {
    case ATWMailType.FiledAcknowledgementHRequestor:
     strSubject = "Delivered: Authority to Work Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your ATW Application has been successfully sent to " + strHApproverName + ".<br>" +
               "** ATW Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "** Schedule Details **<br>" +
               clsATWDetails.GetHTMLTable(_strATWCode) + "<br><br>" +
               "<a href='" + strURLATWDetails + "'>Click here to view your online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case ATWMailType.FiledAcknowledgementDRequestor:
     strSubject = "Delivered: Authority to Work Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your ATW Application has been successfully sent to " + strDApproverName + ".<br>" +
               "** ATW Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "** Schedule Details **<br>" +
               clsATWDetails.GetHTMLTable(_strATWCode) + "<br><br>" +
               "<a href='" + strURLATWDetails + "'>Click here to view your online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case ATWMailType.FiledNotificationHApprover:
     strSubject = "For Your Approval: ATW Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               strRequestorName + " has just sent an ATW Application with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "** Schedule Details **<br>" +
               clsATWDetails.GetHTMLTable(_strATWCode) + "<br><br>" +
               "<a href='" + strURLATWDetailsH + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetailsH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case ATWMailType.FiledNotificationDApprover:
     strSubject = "For Your Approval: ATW Application - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               strRequestorName + " has just sent an ATW Application with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "** Schedule Details **<br>" +
               clsATWDetails.GetHTMLTable(_strATWCode) + "<br><br>" +
               "<a href='" + strURLATWDetailsD + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetailsD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;

    case ATWMailType.ApprovedAcknowledgementHApprover:
     strSubject = "Delivered: Approved ATW Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You approved an ATW Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLATWDetailsH + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetailsH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case ATWMailType.ApprovedAcknowledgementDApprover:
     strSubject = "Delivered: Approved ATW Application - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               "You approved an ATW Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLATWDetailsD + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetailsD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;

    case ATWMailType.ApprovedNotificationHRequestor:
     strSubject = "Approved: ATW Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has approved your ATW Application.<br>" +
               "Your ATW Application has been forwarded to " + strDApproverName + " for final approval.<br><br>" +
               "<a href='" + strURLATWDetails + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case ATWMailType.ApprovedNotificationDRequestor:
     strSubject = "Approved: ATW Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strDApproverName + " has approved your ATW Application.<br><br>" +
               "<a href='" + strURLATWDetails + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case ATWMailType.DisapprovedAcknowledgementHApprover:
     strSubject = "Delivered: Disapproved ATW Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You disapproved an ATW Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLATWDetailsH + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetailsH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case ATWMailType.DisapprovedAcknowledgementDApprover:
     strSubject = "Delivered: Disapproved ATW Application - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               "You disapproved an ATW Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLATWDetailsD + "'>Click here to view the online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetailsD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;


    case ATWMailType.DisapprovedNotificationHRequestor:
     strSubject = "Disapproved: ATW Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has disapproved your ATW Application.<br><br>" +
               "<a href='" + strURLATWDetails + "'>Click here to view your online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case ATWMailType.DisapprovedNotificationDRequestor:
     strSubject = "Disapproved: ATW Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strDApproverName + " has disapproved your ATW Application.<br><br>" +
               "<a href='" + strURLATWDetails + "'>Click here to view your online ATW application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLATWDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static void AuthenticateAccessForm(ATWUsers pATWUsers, string pUsername, string pATWCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pATWUsers == ATWUsers.Requestor)
     cmd.CommandText = "SELECT username FROM HR.ATW WHERE atwcode='" + pATWCode + "' AND username='" + pUsername + "'";
    else if (pATWUsers == ATWUsers.ApproverHead)
     cmd.CommandText = "SELECT apphname FROM HR.ATW WHERE atwcode='" + pATWCode + "' AND apphname='" + pUsername + "'";
    else if (pATWUsers == ATWUsers.ApproverDivision)
     cmd.CommandText = "SELECT appdname FROM HR.ATW WHERE atwcode='" + pATWCode + "' AND appdname='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }

   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static bool AuthenticateAccess(string pUsername, string pATWCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM HR.ATW WHERE atwcode='" + pATWCode + "' AND (username='" + pUsername + "' OR apphname='" + pUsername + "' OR appdname='" + pUsername + "')";
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
    cmd.CommandText = "SELECT username,datefile,datestrt,dateend,HR.ATWDetails.reason FROM HR.ATW INNER JOIN HR.ATWDetails ON HR.ATW.atwcode = HR.ATWDetails.atwcode WHERE HR.ATW.status='A' AND HR.ATWDetails.status='1' AND ((datestrt BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') OR (dateend BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "')) ORDER BY datestrt DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDSG(DateTime pDateStart, DateTime pDateEnd)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username,datefile,datestrt,dateend, HR.ATWDetails.reason, HR.ATW.status FROM HR.ATW INNER JOIN HR.ATWDetails ON HR.ATW.atwcode = HR.ATWDetails.atwcode WHERE ((datestrt BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') OR (dateend BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "')) ORDER BY datestrt DESC";
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
    cmd.CommandText = "SELECT TOP 10 atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username FROM HR.ATW WHERE username='" + pUsername + "' ORDER BY datefile DESC";
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
    cmd.CommandText = "SELECT TOP 10 atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username FROM HR.ATW WHERE apphname='" + pUsername + "' AND apphstat='F' AND status='F' ORDER BY datefile DESC";
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
    cmd.CommandText = "SELECT TOP 10 atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username FROM HR.ATW WHERE appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A' AND status='F' ORDER BY datefile DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetRequestStatusIcon(string pATWStatus)
  {
   string strReturn = "";
   if (pATWStatus == "V")
    strReturn = "Disapproved.png";
   else if (pATWStatus == "D")
    strReturn = "Disapproved.png";
   else if (pATWStatus == "F")
    strReturn = "Approval.png";
   else if (pATWStatus == "A")
    strReturn = "Approved.png";
   else if (pATWStatus == "C")
    strReturn = "Disapproved.png";
   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pATWStatus, string pAHName, string pAHStatus, string pADName, string pADStatus)
  {
   string strReturn = "";
   if (pATWStatus == "D")
    strReturn = "Disapproved by " + (pAHStatus != "D" && pADStatus != "D" ? " System Administrator" : clsSpeedo.AssignUsernameLink((pAHStatus == "D" ? pAHName : pADName), 3));
   else if (pATWStatus == "C")
    strReturn = "The application has been cancelled";
   else if (pATWStatus == "F")
    strReturn = "For approval of " + clsSpeedo.AssignUsernameLink((pAHStatus == "F" ? pAHName : pADName), 3);
   else if (pATWStatus == "A")
    strReturn = "Approved";
   return strReturn;
  }

  public static int GetTotalForAttention(string pUsername)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(atwcode) FROM HR.ATW WHERE (apphname='" + pUsername + "' AND apphstat='F' AND status='F') OR (appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A' AND status='F')";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intReturn = 0; }
   }
   return intReturn;
  }

  public static ATWStatus ToATWStatusDesc(string pATWStatusCode)
  {
   switch (pATWStatusCode)
   {
    case "C":
     return ATWStatus.Cancelled;
    case "F":
     return ATWStatus.ForApproval;
    case "A":
     return ATWStatus.Approved;
    case "D":
     return ATWStatus.Disapproved;
    default:
     return ATWStatus.ForApproval;
   }
  }

  public static string ToATWStatus(string pATWStatusCode)
  {
   switch (pATWStatusCode)
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

  public static DataTable GetPageRecords(ATWUsers pATWUsers, int pPage, string pUsername, string pStatus)
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
     if (pATWUsers == ATWUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.ATW WHERE username='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pATWUsers == ATWUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.ATW WHERE apphname='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pATWUsers == ATWUsers.ApproverDivision)
      cmd.CommandText = "SELECT * FROM (SELECT atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.ATW WHERE appdname='" + pUsername + "' AND apphstat='A') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    else
    {
     if (pATWUsers == ATWUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.ATW WHERE username='" + pUsername + "' AND status='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pATWUsers == ATWUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.ATW WHERE apphname='" + pUsername + "' AND apphstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pATWUsers == ATWUsers.ApproverDivision)
      cmd.CommandText = "SELECT * FROM (SELECT atwcode,datefile,reason,apphname,apphstat,appdname,appdstat,status,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.ATW WHERE appdname='" + pUsername + "' AND appdstat='" + pStatus + "' AND apphstat='A') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetPaging(ATWUsers pATWUsers, int pPage, string pUsername, string pStatus, string pPageName)
  {
   string strReturn = "";

   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intTRows = 0;
   int intTRowsTemp = 0;
   int intPage = 1;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pATWUsers == ATWUsers.Requestor)
     cmd.CommandText = "SELECT COUNT(atwcode) FROM HR.ATW WHERE username='" + pUsername + "'" + (pStatus == "ALL" ? "" : " AND status='" + pStatus + "'");
    else if (pATWUsers == ATWUsers.ApproverHead)
     cmd.CommandText = "SELECT COUNT(atwcode) FROM HR.ATW WHERE apphname='" + pUsername + "'" + (pStatus == "ALL" ? "" : " AND apphstat='" + pStatus + "'");
    else if (pATWUsers == ATWUsers.ApproverDivision)
     cmd.CommandText = "SELECT COUNT(atwcode) FROM HR.ATW WHERE appdname='" + pUsername + "'" + (pStatus == "ALL" ? "" : " AND apphstat='A' AND appdstat='" + pStatus + "'");
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
    cmd.CommandText = "SELECT COUNT(atwcode) FROM HR.ATW";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

     //added by charlie bachiller 11-28-2011
  public static DataTable GetNotificationForApproval(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT * FROM HR.ATW WHERE (apphname='" + pUsername + "' AND apphstat='F' AND status='F') OR (appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A' AND status='F')";
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

 }
}