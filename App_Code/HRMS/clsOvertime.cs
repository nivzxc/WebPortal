using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRMS
{
 public enum OvertimeStatus { Cancelled, ForApproval, Approved, Disapproved }
 public enum OvertimeUsers { Requestor, ApproverRequestor, ApproverHead, ApproverDivision, ApproverCOO }
 public enum OvertimeMailType
 {
  FiledAcknowledgementRRequestor,
  FiledAcknowledgementHRequestor,
  FiledAcknowledgementDRequestor,
  FiledAcknowledgementCRequestor,
  FiledNotificationRApprover,
  FiledNotificationHApprover,
  FiledNotificationDApprover,
  FiledNotificationCApprover,
  ApprovedAcknowledgementRApprover,
  ApprovedAcknowledgementHApprover,
  ApprovedAcknowledgementDApprover,
  ApprovedAcknowledgementCApprover,
  ApprovedNotificationRRequestor,
  ApprovedNotificationHRequestor,
  ApprovedNotificationDRequestor,
  ApprovedNotificationCRequestor,
  DisapprovedAcknowledgementRApprover,
  DisapprovedAcknowledgementHApprover,
  DisapprovedAcknowledgementDApprover,
  DisapprovedAcknowledgementCApprover,
  DisapprovedNotificationRRequestor,
  DisapprovedNotificationHRequestor,
  DisapprovedNotificationDRequestor,
  DisapprovedNotificationCRequestor
 }

 public class clsOvertime : IDisposable
 {
  private string _strOvertimeCode;
  private string _strUsername;
  private DateTime _dteDateFile;
  private DateTime _dteDateStart;
  private DateTime _dteDateEnd;
  private float _fltUnits;
  private string _strReason;
  private string _strChargeType;
  private string _strDepartmentCode;
  private string _strApproverRequestorName;
  private DateTime _dteApproverRequestorDate;
  private string _strApproverRequestorRemarks;
  private string _strApproverRequestorStatus;
  private string _strApproverHeadName;
  private DateTime _dteApproverHeadDate;
  private string _strApproverHeadRemarks;
  private string _strApproverHeadStatus;
  private string _strApproverDivisionName;
  private DateTime _dteApproverDivisionDate;
  private string _strApproverDivisionRemarks;
  private string _strApproverDivisionStatus;
  private string _strApproverCOOName;
  private DateTime _dteApproverCOODate;
  private string _strApproverCOORemarks;
  private string _strApproverCOOStatus;
  private string _strStatus;

  public clsOvertime() { }
  public clsOvertime(string pOvertimeCode) { _strOvertimeCode = pOvertimeCode; }

  public string OvertimeCode { get { return _strOvertimeCode; } set { _strOvertimeCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime DateFile { get { return _dteDateFile; } set { _dteDateFile = value; } }
  public DateTime DateStart { get { return _dteDateStart; } set { _dteDateStart = value; } }
  public DateTime DateEnd { get { return _dteDateEnd; } set { _dteDateEnd = value; } }
  public float Units { get { return _fltUnits; } set { _fltUnits = value; } }
  public string Reason { get { return _strReason; } set { _strReason = value; } }
  public string ChargeType { get { return _strChargeType; } set { _strChargeType = value; } }
  public string DepartmentCode { get { return _strDepartmentCode; } set { _strDepartmentCode = value; } }
  public string ApproverRequestorName { get { return _strApproverRequestorName; } set { _strApproverRequestorName = value; } }
  public DateTime ApproverRequestorDate { get { return _dteApproverRequestorDate; } set { _dteApproverRequestorDate = value; } }
  public string ApproverRequestorRemarks { get { return _strApproverRequestorRemarks; } set { _strApproverRequestorRemarks = value; } }
  public string ApproverRequestorStatus { get { return _strApproverRequestorStatus; } set { _strApproverRequestorStatus = value; } }
  public string ApproverHeadName { get { return _strApproverHeadName; } set { _strApproverHeadName = value; } }
  public DateTime ApproverHeadDate { get { return _dteApproverHeadDate; } set { _dteApproverHeadDate = value; } }
  public string ApproverHeadRemarks { get { return _strApproverHeadRemarks; } set { _strApproverHeadRemarks = value; } }
  public string ApproverHeadStatus { get { return _strApproverHeadStatus; } set { _strApproverHeadStatus = value; } }
  public string ApproverDivisionName { get { return _strApproverDivisionName; } set { _strApproverDivisionName = value; } }
  public DateTime ApproverDivisionDate { get { return _dteApproverDivisionDate; } set { _dteApproverDivisionDate = value; } }
  public string ApproverDivisionRemarks { get { return _strApproverDivisionRemarks; } set { _strApproverDivisionRemarks = value; } }
  public string ApproverDivisionStatus { get { return _strApproverDivisionStatus; } set { _strApproverDivisionStatus = value; } }
  public string ApproverCOOName { get { return _strApproverCOOName; } set { _strApproverCOOName = value; } }
  public DateTime ApproverCOODate { get { return _dteApproverCOODate; } set { _dteApproverCOODate = value; } }
  public string ApproverCOORemarks { get { return _strApproverCOORemarks; } set { _strApproverCOORemarks = value; } }
  public string ApproverCOOStatus { get { return _strApproverCOOStatus; } set { _strApproverCOOStatus = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Overtime WHERE otcode='" + _strOvertimeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _dteDateFile = clsValidator.CheckDate(dr["datefile"].ToString());
     _dteDateStart = clsValidator.CheckDate(dr["datestrt"].ToString());
     _dteDateEnd = clsValidator.CheckDate(dr["dateend"].ToString());
     _fltUnits = clsValidator.CheckFloat(dr["units"].ToString());
     _strReason = dr["reason"].ToString();
     _strChargeType = dr["chartype"].ToString();
     _strDepartmentCode = dr["deptcode"].ToString();
     _strApproverRequestorName = dr["apprname"].ToString();
     _dteApproverRequestorDate = clsValidator.CheckDate(dr["apprdate"].ToString());
     _strApproverRequestorRemarks = dr["apprrem"].ToString();
     _strApproverRequestorStatus = dr["apprstat"].ToString();
     _strApproverHeadName = dr["apphname"].ToString();
     _dteApproverHeadDate = clsValidator.CheckDate(dr["apphdate"].ToString());
     _strApproverHeadRemarks = dr["apphrem"].ToString();
     _strApproverHeadStatus = dr["apphstat"].ToString();
     _strApproverDivisionName = dr["appdname"].ToString();
     _dteApproverDivisionDate = clsValidator.CheckDate(dr["appddate"].ToString());
     _strApproverDivisionRemarks = dr["appdrem"].ToString();
     _strApproverDivisionStatus = dr["appdstat"].ToString();
     _strApproverCOOName = dr["appcname"].ToString();
     _dteApproverCOODate = clsValidator.CheckDate(dr["appcdate"].ToString());
     _strApproverCOORemarks = dr["appcrem"].ToString();
     _strApproverCOOStatus = dr["appcstat"].ToString();
     _strStatus = dr["otstat"].ToString();
    }
    dr.Close();
   }
  }

  public int Insert()
  {
   int intReturn = 0;
   SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString);
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   cmd.Transaction = tran;
   //try
   //{
    OTcode(); // ADDED by calvin cavite feb 15, 2018

    //_strOvertimeCode = clsString.ZeroNumber(clsRegistry.GetValue(clsRegistry.OvertimeCodeField), 9); REMOVE BY CALVIN CAVITE feb 15, 2018 

    cmd.CommandText = "INSERT INTO HR.Overtime(otcode,username,datefile,datestrt,dateend,units,reason,chartype,deptcode,apprname,apprrem,apprstat,apphname,apphrem,apphstat,appdname,appdrem,appdstat,appcname,appcrem,appcstat,otstat) VALUES(@otcode,@username,@datefile,@datestrt,@dateend,@units,@reason,@chartype,@deptcode,@apprname,'',@apprstat,@apphname,'',@apphstat,@appdname,'',@appdstat,@appcname,'',@appcstat,@otstat);";
    cmd.Parameters.Add(new SqlParameter("@otcode", _strOvertimeCode));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@datefile", _dteDateFile));
    cmd.Parameters.Add(new SqlParameter("@datestrt", _dteDateStart));
    cmd.Parameters.Add(new SqlParameter("@dateend", _dteDateEnd));
    cmd.Parameters.Add(new SqlParameter("@units", _fltUnits));
    cmd.Parameters.Add(new SqlParameter("@reason", _strReason));
    cmd.Parameters.Add(new SqlParameter("@chartype", _strChargeType));
    cmd.Parameters.Add(new SqlParameter("@deptcode", _strDepartmentCode));
    cmd.Parameters.Add(new SqlParameter("@apprname", _strApproverRequestorName));
    cmd.Parameters.Add(new SqlParameter("@apprstat", _strApproverRequestorStatus));
    cmd.Parameters.Add(new SqlParameter("@apphname", _strApproverHeadName));
    cmd.Parameters.Add(new SqlParameter("@apphstat", _strApproverHeadStatus));
    cmd.Parameters.Add(new SqlParameter("@appdname", _strApproverDivisionName));
    cmd.Parameters.Add(new SqlParameter("@appdstat", _strApproverDivisionStatus));
    cmd.Parameters.Add(new SqlParameter("@appcname", _strApproverCOOName));
    cmd.Parameters.Add(new SqlParameter("@appcstat", _strApproverCOOStatus));
    cmd.Parameters.Add(new SqlParameter("@otstat", "F"));
    intReturn = cmd.ExecuteNonQuery();
   
    cmd.Parameters.Clear();
    cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='otcode'";
    cmd.ExecuteNonQuery();

    tran.Commit();
   //}
   //catch { tran.Rollback(); }
   //finally { cn.Close(); }

   if (_strChargeType == "0")
   {
    if (_strUsername == _strApproverHeadName)
    {
     SendNotification(OvertimeMailType.FiledAcknowledgementDRequestor);
     SendNotification(OvertimeMailType.FiledNotificationDApprover);
    }
    else
    {
     SendNotification(OvertimeMailType.FiledAcknowledgementHRequestor);
     SendNotification(OvertimeMailType.FiledNotificationHApprover);
    }
   }
   else
   {
    SendNotification(OvertimeMailType.FiledAcknowledgementRRequestor);
    SendNotification(OvertimeMailType.FiledNotificationRApprover);
   }

   return intReturn;
  }

  public int ApproveRequestor()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {    
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Overtime SET apprrem=@apprrem,apprdate='" + _dteApproverRequestorDate + "',apprstat='A' WHERE otcode='" + _strOvertimeCode + "'";
    cmd.Parameters.Add("@apprrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apprrem"].Value = _strApproverRequestorRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OvertimeMailType.ApprovedAcknowledgementRApprover);
    SendNotification(OvertimeMailType.ApprovedNotificationRRequestor);
    SendNotification(OvertimeMailType.FiledNotificationHApprover);
   }

   return intReturn;
  }

  public int ApproveHead()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (_strApproverCOOStatus == "F")
    {
     if (_strApproverHeadName == _strApproverCOOName)
      cmd.CommandText = "UPDATE HR.Overtime SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A', appcstat='A', otstat='A' WHERE otcode='" + _strOvertimeCode + "'";
     else if (_strApproverDivisionStatus == "F" && _strApproverHeadName == _strApproverDivisionName)
      cmd.CommandText = "UPDATE HR.Overtime SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A', appdstat='A' WHERE otcode='" + _strOvertimeCode + "'";
     else
      cmd.CommandText = "UPDATE HR.Overtime SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A' WHERE otcode='" + _strOvertimeCode + "'";
    }
    else if (_strApproverDivisionStatus == "F")
    {
     if (_strApproverHeadName == _strApproverDivisionName)
      cmd.CommandText = "UPDATE HR.Overtime SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A', appdstat='A', otstat='A' WHERE otcode='" + _strOvertimeCode + "'";
     else
      cmd.CommandText = "UPDATE HR.Overtime SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A' WHERE otcode='" + _strOvertimeCode + "'";
    }
    else
    {
     cmd.CommandText = "UPDATE HR.Overtime SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='A', otstat='A' WHERE otcode='" + _strOvertimeCode + "'";
    }
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    Fill();
    SendNotification(OvertimeMailType.ApprovedAcknowledgementHApprover);
    SendNotification(OvertimeMailType.ApprovedNotificationHRequestor);
    if (_strApproverDivisionStatus == "F" && _strStatus == "F")
     SendNotification(OvertimeMailType.FiledNotificationDApprover);
    else if (_strApproverCOOStatus == "F" && _strStatus == "F")
     SendNotification(OvertimeMailType.FiledNotificationCApprover);
   }

   return intReturn;
  }

  public int ApproveDivision()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (_strApproverCOOStatus == "F")
    {
     if (_strApproverDivisionName == _strApproverCOOName)
      cmd.CommandText = "UPDATE HR.Overtime SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "', appdstat='A', appcstat='A', otstat='A' WHERE otcode='" + _strOvertimeCode + "'";
     else
      cmd.CommandText = "UPDATE HR.Overtime SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "', appdstat='A' WHERE otcode='" + _strOvertimeCode + "'";
    }
    else
    {
     cmd.CommandText = "UPDATE HR.Overtime SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "', appdstat='A', otstat='A' WHERE otcode='" + _strOvertimeCode + "'";
    }
    cmd.Parameters.Add("@appdrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@appdrem"].Value = _strApproverDivisionRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    Fill();
    SendNotification(OvertimeMailType.ApprovedAcknowledgementDApprover);
    SendNotification(OvertimeMailType.ApprovedNotificationDRequestor);
    if (_strApproverCOOStatus == "F")
     SendNotification(OvertimeMailType.FiledNotificationCApprover);
   }

   return intReturn;
  }

  public int ApproveCOO()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Overtime SET appcrem=@appcrem,appcdate='" + _dteApproverCOODate + "',appcstat='A',otstat='A' WHERE otcode='" + _strOvertimeCode + "'";
    cmd.Parameters.Add("@appcrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@appcrem"].Value = _strApproverCOORemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OvertimeMailType.ApprovedAcknowledgementCApprover);
    SendNotification(OvertimeMailType.ApprovedNotificationCRequestor);
   }

   return intReturn;
  }
  
  public int DisapproveRequestor()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Overtime SET apprrem=@apprrem,apprdate='" + _dteApproverRequestorDate + "',apprstat='D',otstat='D' WHERE otcode='" + _strOvertimeCode + "'";
    cmd.Parameters.Add("@apprrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apprrem"].Value = _strApproverRequestorRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OvertimeMailType.DisapprovedAcknowledgementRApprover);
    SendNotification(OvertimeMailType.DisapprovedNotificationRRequestor);
   }

   return intReturn;
  }

  public int DisapproveHead()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {    
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Overtime SET apphrem=@apphrem,apphdate='" + _dteApproverHeadDate + "',apphstat='D',otstat='D' WHERE otcode='" + _strOvertimeCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OvertimeMailType.DisapprovedAcknowledgementHApprover);
    SendNotification(OvertimeMailType.DisapprovedNotificationHRequestor);
   }

   return intReturn;
  }

  public int DisapproveDivision()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Overtime SET appdrem=@appdrem, appddate='" + _dteApproverDivisionDate + "',appdstat='D',otstat='D' WHERE otcode='" + _strOvertimeCode + "'";
    cmd.Parameters.Add("@appdrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@appdrem"].Value = _strApproverDivisionRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OvertimeMailType.DisapprovedAcknowledgementDApprover);
    SendNotification(OvertimeMailType.DisapprovedNotificationDRequestor);
   }

   return intReturn;
  }

  public int DisapproveCOO()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Overtime SET appcrem=@appcrem,appcdate='" + _dteApproverCOODate + "',appcstat='D',otstat='D' WHERE otcode='" + _strOvertimeCode + "'";
    cmd.Parameters.Add("@appcrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@appcrem"].Value = _strApproverCOORemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OvertimeMailType.DisapprovedAcknowledgementCApprover);
    SendNotification(OvertimeMailType.DisapprovedNotificationCRequestor);
   }

   return intReturn;
  }

  public bool Cancel()
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Overtime SET otstat='C' WHERE otcode='" + _strOvertimeCode + "'";
    cn.Open();
    blnReturn = cmd.ExecuteNonQuery() > 0;
   }
   return blnReturn;
  }

  public void SendNotification(OvertimeMailType pMailType)
  {
   string strSpeedoUrl = ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   string strSubject = "";
   string strBody = "";
   string strRequestorName = clsEmployee.GetName(_strUsername);
   string strRequestorEmail = clsUsers.GetEmail(_strUsername);
   string strRApproverName = clsEmployee.GetName(_strApproverRequestorName);
   string strRApproverEmail = clsUsers.GetEmail(_strApproverRequestorName);
   string strHApproverName = clsEmployee.GetName(_strApproverHeadName);
   string strHApproverEmail = clsUsers.GetEmail(_strApproverHeadName);
   string strDApproverName = clsEmployee.GetName(_strApproverDivisionName);
   string strDApproverEmail = clsUsers.GetEmail(_strApproverDivisionName);
   string strCApproverName = clsEmployee.GetName(_strApproverCOOName);
   string strCApproverEmail = clsUsers.GetEmail(_strApproverCOOName);
   string strURLOvertimeDetails = strSpeedoUrl + "/HR/HRMS/Overtime/OvertimeDetails.aspx?otcode=" + _strOvertimeCode;
   string strURLOvertimeDetailsAR = strSpeedoUrl + "/HR/HRMS/Overtime/OvertimeDetailsAR.aspx?otcode=" + _strOvertimeCode;
   string strURLOvertimeDetailsAH = strSpeedoUrl + "/HR/HRMS/Overtime/OvertimeDetailsAH.aspx?otcode=" + _strOvertimeCode;
   string strURLOvertimeDetailsAD = strSpeedoUrl + "/HR/HRMS/Overtime/OvertimeDetailsAD.aspx?otcode=" + _strOvertimeCode;
   string strURLOvertimeDetailsAC = strSpeedoUrl + "/HR/HRMS/Overtime/OvertimeDetailsAC.aspx?otcode=" + _strOvertimeCode;

   switch (pMailType)
   {
    case OvertimeMailType.FiledAcknowledgementRRequestor:
     strSubject = "Delivered: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Overtime Application has been successfully sent to " + strRApproverName + ".<br>" +
               "** Overtime Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.FiledAcknowledgementHRequestor:
     strSubject = "Delivered: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Overtime Application has been successfully sent to " + strHApproverName + ".<br>" +
               "** Overtime Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.FiledAcknowledgementDRequestor:
     strSubject = "Delivered: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Overtime Application has been successfully sent to " + strDApproverName + ".<br>" +
               "** Overtime Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.FiledAcknowledgementCRequestor:
     strSubject = "Delivered: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Overtime Application has been successfully sent to " + strCApproverName + ".<br>" +
               "** Overtime Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.FiledNotificationRApprover:
     strSubject = "For Your Approval: Overtime Application - " + strRequestorName;
     strBody = "Hi " + strRApproverName + ",<br><br>" +
               strRequestorName + " has just sent an Overtime Application with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetailsAR + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAR + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.FiledNotificationHApprover:
     strSubject = "For Your Approval: Overtime Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               strRequestorName + " has just sent an Overtime Application with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetailsAH + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.FiledNotificationDApprover:
     strSubject = "For Your Approval: Overtime Application - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               strRequestorName + " has just sent an Overtime Application with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetailsAD + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.FiledNotificationCApprover:
     strSubject = "For Your Approval: Overtime Application - " + strRequestorName;
     strBody = "Hi " + strCApproverName + ",<br><br>" +
               strRequestorName + " has just sent an Overtime Application with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "Charge Type: " + clsOvertime.GetChargeTypeDesc(_strChargeType) + "<br>" +
               "Charge To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "<a href='" + strURLOvertimeDetailsAC + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAC + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strCApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedAcknowledgementRApprover:
     strSubject = "Delivered: Approved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strRApproverName + ",<br><br>" +
               "You approved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAR + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAR + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedAcknowledgementHApprover:
     strSubject = "Delivered: Approved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You approved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAH + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedAcknowledgementDApprover:
     strSubject = "Delivered: Approved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               "You approved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAD + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedAcknowledgementCApprover:
     strSubject = "Delivered: Approved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strCApproverName + ",<br><br>" +
               "You approved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAC + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAC + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strCApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedNotificationRRequestor:
     strSubject = "Approved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strRApproverName + " has approved your Overtime Application.<br>" +
               "Your Overtime Application has been forwarded to " + strHApproverName + " for final approval.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedNotificationHRequestor:
     strSubject = "Approved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has approved your Overtime Application.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedNotificationDRequestor:
     strSubject = "Approved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strDApproverName + " has approved your Overtime Application.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.ApprovedNotificationCRequestor:
     strSubject = "Approved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strCApproverName + " has approved your Overtime Application.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedAcknowledgementRApprover:
     strSubject = "Delivered: Disapproved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strRApproverName + ",<br><br>" +
               "You disapproved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAR + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAR + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedAcknowledgementHApprover:
     strSubject = "Delivered: Disapproved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You disapproved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAH + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedAcknowledgementDApprover:
     strSubject = "Delivered: Disapproved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strDApproverName + ",<br><br>" +
               "You disapproved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAD + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAD + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strDApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedAcknowledgementCApprover:
     strSubject = "Delivered: Disapproved Overtime Application - " + strRequestorName;
     strBody = "Hi " + strCApproverName + ",<br><br>" +
               "You disapproved an Overtime Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOvertimeDetailsAC + "'>Click here to view the online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetailsAC + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strCApproverEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedNotificationRRequestor:
     strSubject = "Disapproved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strRApproverName + " has disapproved your Overtime Application.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedNotificationHRequestor:
     strSubject = "Disapproved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has disapproved your Overtime Application.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedNotificationDRequestor:
     strSubject = "Disapproved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strDApproverName + " has disapproved your Overtime Application.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OvertimeMailType.DisapprovedNotificationCRequestor:
     strSubject = "Disapproved: Overtime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strCApproverName + " has disapproved your Overtime Application.<br><br>" +
               "<a href='" + strURLOvertimeDetails + "'>Click here to view your online Overtime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOvertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static void AuthenticateAccessForm(OvertimeUsers pOvertimeUsers, string pUsername, string pOvertimeCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pOvertimeUsers == OvertimeUsers.Requestor)
     cmd.CommandText = "SELECT username FROM HR.Overtime WHERE otcode='" + pOvertimeCode + "' AND username='" + pUsername + "'";
    else if (pOvertimeUsers == OvertimeUsers.ApproverRequestor)
     cmd.CommandText = "SELECT apprname FROM HR.Overtime WHERE otcode='" + pOvertimeCode + "' AND apprname='" + pUsername + "'";
    else if (pOvertimeUsers == OvertimeUsers.ApproverHead)
     cmd.CommandText = "SELECT apphname FROM HR.Overtime WHERE otcode='" + pOvertimeCode + "' AND apphname='" + pUsername + "'";
    else if (pOvertimeUsers == OvertimeUsers.ApproverDivision)
     cmd.CommandText = "SELECT apphname FROM HR.Overtime WHERE otcode='" + pOvertimeCode + "' AND appdname='" + pUsername + "'";
    else if (pOvertimeUsers == OvertimeUsers.ApproverCOO)
     cmd.CommandText = "SELECT apphname FROM HR.Overtime WHERE otcode='" + pOvertimeCode + "' AND appcname='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }

   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static bool AuthenticateAccess(string pUsername, string pOTCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM HR.Overtime WHERE otcode='" + pOTCode + "' AND (username='" + pUsername + "' OR apprname='" + pUsername + "' OR apphname='" + pUsername + "' OR appdname='" + pUsername + "' OR appcname='" + pUsername + "')";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable DSGOvertimeMenu(OvertimeUsers pOTUsers, int pTop, string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pOTUsers)
    {
     case OvertimeUsers.Requestor:
      cmd.CommandText = "SELECT TOP " + pTop + " otcode,datefile,datestrt,dateend,reason,chartype,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username FROM HR.Overtime WHERE username='" + pUsername + "' ORDER BY datefile DESC";
      break;
     case OvertimeUsers.ApproverRequestor:
      cmd.CommandText = "SELECT TOP " + pTop + " otcode,datefile,datestrt,dateend,reason,chartype,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username FROM HR.Overtime WHERE apprname='" + pUsername + "' AND apprstat='F' AND otstat='F' ORDER BY datefile DESC";
      break;
     case OvertimeUsers.ApproverHead:
      cmd.CommandText = "SELECT TOP " + pTop + " otcode,datefile,datestrt,dateend,reason,chartype,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username FROM HR.Overtime WHERE apphname='" + pUsername + "' AND apphstat='F' AND otstat='F' AND (chartype='0' OR (chartype='1' AND apprstat='A')) ORDER BY datefile DESC";
      break;
     case OvertimeUsers.ApproverDivision:
      //cmd.CommandText = "SELECT TOP " + pTop + " otcode,datefile,datestrt,dateend,reason,chartype,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username FROM HR.Overtime WHERE appdname='" + pUsername + "' AND ((appdstat='F' AND otstat='F' AND apphstat='A') OR (otstat IN ('A') AND appdstat NOT IN ('A','D') AND appcstat NOT IN ('A','D') AND datestrt > '" + DateTime.Now + "')) ORDER BY datefile DESC";
      cmd.CommandText = "SELECT TOP " + pTop + " otcode,datefile,datestrt,dateend,reason,chartype,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username FROM HR.Overtime WHERE appdname='" + pUsername + "' AND appdstat='F' AND otstat='F' AND apphstat='A' ORDER BY datefile DESC";
      break;
     case OvertimeUsers.ApproverCOO:
      cmd.CommandText = "SELECT TOP " + pTop + " otcode,datefile,datestrt,dateend,reason,chartype,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username FROM HR.Overtime WHERE appcname='" + pUsername + "' AND appcstat='F' AND otstat='F' AND ((appdstat='A') OR (apphstat='A' AND appdstat='X')) ORDER BY datefile DESC";
      break;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DSGOvertimeMenuAffirmation(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 9 otcode,datefile,datestrt,dateend,reason,chartype,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username FROM HR.Overtime WHERE appdname='" + pUsername + "' AND ((appdstat='F' AND otstat='F' AND apphstat='A') OR (otstat IN ('F','A') AND appdstat NOT IN ('A','D') AND appcstat NOT IN ('A','D') AND datestrt > '" + DateTime.Now + "')) ORDER BY datefile DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DSGOvertimeAll(OvertimeUsers pOvertimeUsers, int pPage, string pUsername, string pStatus)
  {
   DataTable tblReturn = new DataTable();
   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intStart = ((pPage - 1) * intPageSize) + 1;
   int intEnd = pPage * intPageSize;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pStatus == "all")
    {
     if (pOvertimeUsers == OvertimeUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE username='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverRequestor)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE apprname='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE apphname='" + pUsername + "' AND (chartype='0' OR (chartype='1' AND apprstat='A'))) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverDivision)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE appdname='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverCOO)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE appcname='" + pUsername + "' AND (appdstat='A' OR (apphstat='A' AND appdstat='X'))) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    else
    {
     if (pOvertimeUsers == OvertimeUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE username='" + pUsername + "' AND otstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverRequestor)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE apprname='" + pUsername + "' AND apprstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE apphname='" + pUsername + "' AND apphstat='" + pStatus + "' AND (chartype='0' OR (chartype='1' AND apprstat='A'))) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverDivision)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE appdname='" + pUsername + "' AND appdstat='" + pStatus + "' AND apphstat='A') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOvertimeUsers == OvertimeUsers.ApproverCOO)
      cmd.CommandText = "SELECT * FROM (SELECT otcode,datefile,reason,chartype,datestrt,dateend,apprname,apprstat,apphname,apphstat,appdname,appdstat,appcname,appcstat,otstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Overtime WHERE appcname='" + pUsername + "' AND appcstat='" + pStatus + "' AND (appdstat='A' OR (apphstat='A' AND appdstat='X'))) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetApprovedOvertime(DateTime pFocusDate, string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Overtime WHERE otstat='A' AND '" + pFocusDate + "' BETWEEN CONVERT(DATETIME, FLOOR(CONVERT(FLOAT,datestrt))) AND CONVERT(DATETIME, FLOOR(CONVERT(FLOAT,dateend)))";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetRequestStatusIcon(string pOTStatus)
  {
   string strReturn = "";
   if (pOTStatus == "V")
    strReturn = "Disapproved.png";
   else if (pOTStatus == "D")
    strReturn = "Disapproved.png";
   else if (pOTStatus == "F")
    strReturn = "Approval.png";
   else if (pOTStatus == "A")
    strReturn = "Approved.png";
   else if (pOTStatus == "C")
    strReturn = "Disapproved.png";
   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pOTStatus, string pChargeType, string pARName, string pARStatus, string pAHName, string pAHStatus, string pADName, string pADStatus, string pACName, string pACStatus)
  {
   string strReturn = "";
   if (pOTStatus == "V")
    strReturn = "The application has been voided by the application";
   else if (pOTStatus == "C")
    strReturn = "The user cancelled the application";
   else if (pOTStatus == "D")
   {
    if (pChargeType == "1" && pARStatus == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pARName, 3);
    else if (pADStatus == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pADName, 3);
    else if (pACStatus == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pACName, 3);
   }
   else if (pOTStatus == "F")
   {
    if (pChargeType == "1" && pARStatus == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pARName, 3);
    else if (pAHStatus == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pAHName, 3);
    else if (pADStatus == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pADName, 3);
    else if (pACStatus == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pACName, 3);
   }
   else if (pOTStatus == "A")
    strReturn = "Approved";
   return strReturn;
  }

  public static int GetTotalForAttention(string pUsername)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(otcode) FROM HR.Overtime WHERE (apprname='" + pUsername + "' AND apprstat='F' AND otstat='F') OR (apphname='" + pUsername + "' AND apphstat='F' AND (chartype='0' OR (chartype='1' AND apprstat='A')) AND otstat='F') OR (appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A' AND otstat='F') OR (appcname='" + pUsername + "' AND appcstat='F' AND (appdstat='A' OR (apphstat='A' AND appdstat='X')) AND otstat='F')";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intReturn = 0; }
   }
   return intReturn;
  }

  public static string GetChargeTypeDesc(string pOTType)
  {
   if (pOTType == "0")
    return "Charge within the department";
   else
    return "Charge to other department";
  }

  public static OvertimeStatus ToOvertimeStatus(string pOvertimeStatusCode)
  {
   switch (pOvertimeStatusCode)
   {
    case "C":
     return OvertimeStatus.Cancelled;
    case "F":
     return OvertimeStatus.ForApproval;
    case "A":
     return OvertimeStatus.Approved;
    case "D":
     return OvertimeStatus.Disapproved;
    default:
     return OvertimeStatus.Cancelled;
   }
  }

  public static string ToOvertimeStatusDesc(string pOTStatusCode)
  {
   switch (pOTStatusCode)
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
     return "";
   }
  }

  public static string GetPaging(OvertimeUsers pOvertimeUsers, int pPage, string pUsername, string pStatus, string pPageName)
  {
   string strReturn = "";

   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intTRows = 0;
   int intTRowsTemp = 0;
   int intPage = 1;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pOvertimeUsers == OvertimeUsers.Requestor)
     cmd.CommandText = "SELECT COUNT(otcode) FROM HR.Overtime WHERE username='" + pUsername + "'" + (pStatus == "all" ? "" : " AND otstat='" + pStatus + "'");
    else if (pOvertimeUsers == OvertimeUsers.ApproverRequestor)
     cmd.CommandText = "SELECT COUNT(otcode) FROM HR.Overtime WHERE apprname='" + pUsername + "'" + (pStatus == "all" ? "" : " AND apprstat='" + pStatus + "'");
    else if (pOvertimeUsers == OvertimeUsers.ApproverHead)
     cmd.CommandText = "SELECT COUNT(otcode) FROM HR.Overtime WHERE apphname='" + pUsername + "'" + (pStatus == "all" ? "" : " AND apphstat='" + pStatus + "' AND (chartype='0' OR (chartype='1' AND apprstat='A'))");
    else if (pOvertimeUsers == OvertimeUsers.ApproverDivision)
     cmd.CommandText = "SELECT COUNT(otcode) FROM HR.Overtime WHERE appdname='" + pUsername + "'" + (pStatus == "all" ? "" : " AND appdstat='" + pStatus + "' AND apphstat='A'");
    else if (pOvertimeUsers == OvertimeUsers.ApproverCOO)
     cmd.CommandText = "SELECT COUNT(otcode) FROM HR.Overtime WHERE apphname='" + pUsername + "'" + (pStatus == "all" ? "" : " AND appcstat='" + pStatus + "' AND (appdstat='A'' OR (apphstat='A' AND appdstat='X'))");
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

  public static string GetApplications(string pUsername, DateTime pDateApplied)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT otcode FROM HR.Overtime WHERE username='" + pUsername + "' AND ";
   }
   return strReturn;
  }

  public static int GetTotalRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(otcode) FROM HR.Overtime";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

  public static bool IsDivisionHeadApproverRequired(string pUsername, DateTime pDateFrom, DateTime pDateTo)
  {
   return clsHoliday.IsHoliday(clsDateTime.GetDateOnly(pDateFrom), clsDateTime.GetDateOnly(pDateTo)) || clsSchedule.IsRestDay(pUsername, pDateFrom, pDateTo);
  }

  // Added By Ian 
  // 20110427
  public static DataTable GetDSG(DateTime pDateStart, DateTime pDateEnd)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username, datefile,datestrt,dateend, reason, otstat FROM HR.Overtime WHERE ((datestrt BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') OR (dateend BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "')) ORDER BY datestrt DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }
     //added by charlie bachiller 11-28-2011
  public static DataTable GetNotificationForApproval(string pUsername)
  {
      DataTable tbltReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT * FROM HR.Overtime WHERE (apprname='" + pUsername + "' AND apprstat='F' AND otstat='F') OR (apphname='" + pUsername + "' AND apphstat='F' AND (chartype='0' OR (chartype='1' AND apprstat='A')) AND otstat='F') OR (appdname='" + pUsername + "' AND appdstat='F' AND apphstat='A' AND otstat='F') OR (appcname='" + pUsername + "' AND appcstat='F' AND (appdstat='A' OR (apphstat='A' AND appdstat='X')) AND otstat='F')";
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tbltReturn);

      }
      return tbltReturn;
  }
        //ADDED by calvin cavite Feb 15, 2018
        public void OTcode()
        {
            string ot_code = "";
            int strOTcode = 0;
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select top 1 pvalue from Speedo.Keys WHERE pkey='otcode' order by pvalue desc";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) {
                    ot_code = dr["pvalue"].ToString();
                }
                dr.Close();
                if (ot_code == null || ot_code == "")
                {
                    strOTcode = clsValidator.CheckInteger(ot_code) + 1;
                    ot_code = ("OT" + strOTcode.ToString());
                    OvertimeCode = ot_code;
                }
                else
                {
                    char[] removechar = { 'O', 'T' };
                    string nwOTcode = ot_code.TrimStart(removechar);
                    ot_code = nwOTcode;
                    strOTcode = clsValidator.CheckInteger(ot_code) + 1;
                    ot_code = ("OT" + strOTcode.ToString());
                    OvertimeCode = ot_code;
                }

            }
  }

 }
}