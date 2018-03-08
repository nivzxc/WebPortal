using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRMS
{
 public enum LeaveUsers { Requestor = 0, Approver = 1, HrAdmin = 2 }
 public enum LeaveStatus { Cancelled = 0, ForApproval = 1, Approved = 2, Disapproved = 3, Void = 4 }
 public enum LeaveMailType
 {
  FiledAcknowledgementRequestor,
  FiledNotificationApprover,
  ApprovedAcknowledgementApprover,
  ApprovedNotificationRequestor,  
  DisapprovedAcknowledgementApprover,
  DisapprovedNotificationRequestor  
 }

 public class clsLeave : IDisposable
 {
  private string _strLeaveCode;
  private string _strLeaveType;
  private string _strUserName;
  private DateTime _dteDateFile;
  private DateTime _dteDateStart;
  private DateTime _dteDateEnd;
  private float _fltUnits;
  private string _strReason;
  private string _strApproverName;
  private DateTime _dteApproverDate;
  private string _strApproverRemarks;
  private string _strStatus;

  public clsLeave() { }
  public clsLeave(string pLeaveCode) { _strLeaveCode = pLeaveCode; }

  public string LeaveCode { get { return _strLeaveCode; } set { _strLeaveCode = value; } }
  public string LeaveType { get { return _strLeaveType; } set { _strLeaveType = value; } }
  public string UserName { get { return _strUserName; } set { _strUserName = value; } }
  public DateTime DateFile { get { return _dteDateFile; } set { _dteDateFile = value; } }
  public DateTime DateStart { get { return _dteDateStart; } set { _dteDateStart = value; } }
  public DateTime DateEnd { get { return _dteDateEnd; } set { _dteDateEnd = value; } }
  public float Units { get { return _fltUnits; } set { _fltUnits = value; } }
  public string Reason { get { return _strReason; } set { _strReason = value; } }
  public string ApproverName { get { return _strApproverName; } set { _strApproverName = value; } }
  public DateTime ApproverDate { get { return _dteApproverDate; } set { _dteApproverDate = value; } }
  public string ApproverRemarks { get { return _strApproverRemarks; } set { _strApproverRemarks = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Leave WHERE leavcode='" + _strLeaveCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strLeaveType = dr["leavtype"].ToString();
     _strUserName = dr["username"].ToString();
     _dteDateFile = Convert.ToDateTime(dr["datefile"].ToString());
     _dteDateStart = Convert.ToDateTime(dr["datestrt"].ToString());
     _dteDateEnd = Convert.ToDateTime(dr["dateend"].ToString());
     _fltUnits = float.Parse(dr["units"].ToString());
     _strReason = dr["reason"].ToString();
     _strApproverName = dr["apphname"].ToString();
     if (Convert.IsDBNull(dr["apphdate"].ToString()) || dr["apphdate"].ToString() == "")
      _dteApproverDate = DateTime.MinValue;
     else
      _dteApproverDate = Convert.ToDateTime(dr["apphdate"].ToString());
     _strApproverRemarks = dr["apphrem"].ToString();
     _strStatus = dr["leavstat"].ToString();
    }
    dr.Close();
   }
  }

  public bool Insert()
  {
   //added by Calvin V. Cavite FEB 13, 2018
   getLeaveCode();
   bool blnReturn = false;
   SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString);
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = new SqlCommand("spLeaveInsert", cn);
   cmd.Transaction = tran;
  // try
 //  {
                //REMOVE BY CALVIN CAVITE FEB 13, 2018

                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@leavtype", SqlDbType.VarChar, 5);
                //cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
                //cmd.Parameters.Add("@datefile", SqlDbType.DateTime);
                //cmd.Parameters.Add("@datestrt", SqlDbType.DateTime);
                //cmd.Parameters.Add("@dateend", SqlDbType.DateTime);
                //cmd.Parameters.Add("@units", SqlDbType.Float);
                //cmd.Parameters.Add("@reason", SqlDbType.VarChar, 255);
                //cmd.Parameters.Add("@apphname", SqlDbType.VarChar, 30);
                //cmd.Parameters.Add("@leavstat", SqlDbType.Char, 1);
                //cmd.Parameters.Add("@leavcode", SqlDbType.Char, 9);

                //cmd.Parameters["@leavtype"].Value = _strLeaveType;
                //cmd.Parameters["@username"].Value = _strUserName;
                //cmd.Parameters["@datefile"].Value = _dteDateFile;
                //cmd.Parameters["@datestrt"].Value = _dteDateStart;
                //cmd.Parameters["@dateend"].Value = _dteDateEnd;
                //cmd.Parameters["@units"].Value = _fltUnits;
                //cmd.Parameters["@reason"].Value = _strReason;
                //cmd.Parameters["@apphname"].Value = _strApproverName;
                //cmd.Parameters["@leavstat"].Value = "F";
                //cmd.Parameters["@leavcode"].Direction = ParameterDirection.Output;
                //cmd.ExecuteNonQuery();

                //ADDED BY CALVIN CAVITE Feb 13, 2018
                cmd.CommandText = "INSERT INTO HR.Leave VALUES(@leavcode,@leavtype,@username,@datefile,@datestrt,@dateend,@units,@reason,@apphname,@apphdate,@apphrem,@leavstat)";
                cmd.Parameters.Add(new SqlParameter("@leavcode", _strLeaveCode));
                cmd.Parameters.Add(new SqlParameter("@leavtype", _strLeaveType));
                cmd.Parameters.Add(new SqlParameter("@username", _strUserName));
                cmd.Parameters.Add(new SqlParameter("@datefile", _dteDateFile));
                cmd.Parameters.Add(new SqlParameter("@datestrt", _dteDateStart));
                cmd.Parameters.Add(new SqlParameter("@dateend", _dteDateEnd));
                cmd.Parameters.Add(new SqlParameter("@units", _fltUnits));
                cmd.Parameters.Add(new SqlParameter("@reason", _strReason));
                cmd.Parameters.Add(new SqlParameter("@apphname", _strApproverName));
                cmd.Parameters.Add(new SqlParameter("@apphdate",DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@apphrem",DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@leavstat", "F"));
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue + 1 WHERE pkey='leavcode'";
                cmd.ExecuteNonQuery();
                _strLeaveCode = cmd.Parameters["@leavcode"].Value.ToString();
    tran.Commit();
    blnReturn = true;
   //}
  /// catch { tran.Rollback(); blnReturn = false; }
   //finally { cn.Close(); }

   if (blnReturn)
   {
    SendNotification(LeaveMailType.FiledAcknowledgementRequestor);
    SendNotification(LeaveMailType.FiledNotificationApprover);
   }

   return blnReturn;
  }

  public bool Approve()
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {    
    SqlCommand cmd = cn.CreateCommand();    
    cmd.CommandText = "UPDATE HR.Leave SET apphrem=@apphrem,apphdate='" + _dteApproverDate + "',leavstat='A' WHERE leavcode='" + _strLeaveCode + "'";
    cn.Open();
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverRemarks;
    blnReturn = cmd.ExecuteNonQuery() > 0;
   }
   if (blnReturn)
   {
    SendNotification(LeaveMailType.ApprovedAcknowledgementApprover);
    SendNotification(LeaveMailType.ApprovedNotificationRequestor);
   }

   return blnReturn;
  }

  public bool Disapprove()
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Leave SET apphrem=@apphrem,apphdate='" + _dteApproverDate + "',leavstat='D' WHERE leavcode='" + _strLeaveCode + "'";
    cn.Open();
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverRemarks;
    blnReturn = cmd.ExecuteNonQuery() > 0;
   }

   if (blnReturn)
   {
    SendNotification(LeaveMailType.DisapprovedAcknowledgementApprover);
    SendNotification(LeaveMailType.DisapprovedNotificationRequestor);
   }

   return blnReturn;
  }

  public bool Cancel()
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.Leave SET leavstat='C' WHERE leavcode='" + _strLeaveCode + "'";
    cn.Open();
    blnReturn = cmd.ExecuteNonQuery() > 0;
   }
   return blnReturn;
  }

  public void SendNotification(LeaveMailType pMailType)
  {
   string strSpeedoUrl = ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   string strSubject = "";
   string strBody = "";
   string strRequestorName = clsEmployee.GetName(_strUserName);
   string strRequestorEmail = clsUsers.GetEmail(_strUserName);
   string strApproverName = clsEmployee.GetName(_strApproverName);
   string strApproverEmail = clsUsers.GetEmail(_strApproverName);
   string strURLLeaveDetails = strSpeedoUrl + "/HR/HRMS/Leave/LeaveDetails.aspx?leavcode=" + _strLeaveCode;
   string strURLLeaveDetailsA = strSpeedoUrl + "/HR/HRMS/Leave/LeaveDetailsA.aspx?leavcode=" + _strLeaveCode;

   switch (pMailType)
   {
    case LeaveMailType.FiledAcknowledgementRequestor:
     strSubject = "Delivered: Leave Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Leave Application has been successfully sent to " + strApproverName + ".<br>" +
               "** Leave Details **<br>" +
               "Type: " + clsLeaveType.GetDescription(_strLeaveType) + "<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Days: " + _fltUnits.ToString() + "<br>" +
               "Reason: " + _strReason + "<br><br>" +
               "<a href='" + strURLLeaveDetails + "'>Click here to view your online leave application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" + 
               "<i>" + strURLLeaveDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case LeaveMailType.FiledNotificationApprover:
     strSubject = "For Your Approval: Leave Application - " + strRequestorName;
     strBody = "Hi " + strApproverName + ",<br><br>" +
               strRequestorName + " has just sent a Leave Application with the following details:<br>" +
               "Type: " + clsLeaveType.GetDescription(_strLeaveType) + "<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Start: " + _dteDateStart.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date End: " + _dteDateEnd.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Days: " + _fltUnits.ToString() + "<br>" +
               "Reason: " + _strReason + "<br><br>" +
               "<a href='" + strURLLeaveDetailsA + "'>Click here to view the online leave application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLLeaveDetailsA + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
     break;

    case LeaveMailType.ApprovedAcknowledgementApprover:
     strSubject = "Delivered: Approved Leave Application - " + strRequestorName;
     strBody = "Hi " + strApproverName + ",<br><br>" +             
               "You approved a Leave Application.<br><br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLLeaveDetailsA + "'>Click here to view the online leave application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLLeaveDetailsA + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
     break;

    case LeaveMailType.ApprovedNotificationRequestor:
     strSubject = "Approved: Leave Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strApproverName + " has approved your Leave Application.<br><br>" +
               "<a href='" + strURLLeaveDetails + "'>Click here to view the online leave application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLLeaveDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case LeaveMailType.DisapprovedAcknowledgementApprover:
     strSubject = "Delivered: Disapproved Leave Application - " + strRequestorName;
     strBody = "Hi " + strApproverName + ",<br><br>" +
               "You disapproved a Leave Application.<br><br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLLeaveDetailsA + "'>Click here to view the online leave application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLLeaveDetailsA + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
     break;

    case LeaveMailType.DisapprovedNotificationRequestor:
     strSubject = "Disapproved: Leave Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strApproverName + " has disapproved your Leave Application.<br><br>" +
               "<a href='" + strURLLeaveDetails + "'>Click here to view the online leave application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLLeaveDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;
   }
   
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static void AuthenticateAccessForm(LeaveUsers pLeaveUsers, string pUsername, string pLeaveCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pLeaveUsers == LeaveUsers.Requestor)
     cmd.CommandText = "SELECT username FROM HR.Leave WHERE leavcode='" + pLeaveCode + "' AND username='" + pUsername + "'";
    else if (pLeaveUsers == LeaveUsers.Approver)
     cmd.CommandText = "SELECT apphname FROM HR.Leave WHERE leavcode='" + pLeaveCode + "' AND apphname='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }
   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static bool AuthenticateAccess(string pUsername, string pLeaveCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM HR.Leave WHERE leavcode='" + pLeaveCode + "' AND (username='" + pUsername + "' OR apphname='" + pUsername + "')";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable DSGApplications(string pUsername, DateTime pDateStart, DateTime pDateEnd)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT datestrt,dateend FROM HR.Leave WHERE username='" + pUsername + "' AND leavstat IN ('A','F') AND ((datestrt BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') OR (dateend BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "'))";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DSGForApproval(DateTime pDateStart, DateTime pDateEnd)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavcode,HR.Leave.username,leavtype,HR.Leave.datefile,HR.Leave.datestrt,HR.Leave.dateend,units,reason,apphname,lastname,firname FROM HR.Employees INNER JOIN HR.Leave ON HR.Employees.username = HR.Leave.username WHERE leavstat='F' AND ((HR.Leave.datestrt BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') OR (HR.Leave.dateend BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "')) AND HR.Employees.pstatus='1' AND HR.Employees.esttcode IN ('01','02') ORDER BY lastname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static int GetTotalForAttention(string pUsername)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    //cmd.CommandText = "SELECT COUNT(leavcode) FROM HR.Leave WHERE ((username='" + pUsername + "' AND leavstat='F') OR (apphname='" + pUsername + "' AND leavstat='F'))";
    cmd.CommandText = "SELECT COUNT(leavcode) FROM HR.Leave WHERE ((apphname='" + pUsername + "' AND leavstat='F'))";

    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intReturn = 0; }
   }
   return intReturn;
  }

  //public static float GetLeaveUnits(DateTime pFrom, DateTime pTo, string pUsername)
  //{
  // float fltReturn = 0;
  // float fltDifferenceDay = 0;
  // float fltDifferenceTime = 0;
  // DateTime dteFromDate = new DateTime(pFrom.Year, pFrom.Month, pFrom.Day);
  // DateTime dteFromTime = new DateTime(clsDateTime.SystemMinDate.Year, clsDateTime.SystemMinDate.Month, clsDateTime.SystemMinDate.Day, pFrom.Hour, pFrom.Minute, pFrom.Second);
  // DateTime dteToDate = new DateTime(pTo.Year, pTo.Month, pTo.Day);
  // DateTime dteToTime = new DateTime(clsDateTime.SystemMinDate.Year, clsDateTime.SystemMinDate.Month, clsDateTime.SystemMinDate.Day, pTo.Hour, pTo.Minute, pTo.Second);
  // DateTime dteTemp = dteFromDate;
  // while (dteTemp < dteToDate)
  // {
  //  if (clsShift.IsWorkingShift(clsSchedule.GetShiftCode(pUsername, dteTemp)))
  //   fltReturn++;
  //  dteTemp = dteTemp.AddDays(1);
  // }

  // //if (!clsShift.IsWorkingShift(clsSchedule.GetShiftCode(pUsername, pDateFrom)))
  // // dblTimeFrom = 0;

  // //if (!clsShift.IsWorkingShift(clsSchedule.GetShiftCode(pUsername, pDateTo)))
  // // dblTimeTo = 0;

  // //fltReturn += (dblTimeTo - dblTimeFrom);
  // return fltReturn;
  //}

  public static float GetLeaveUnits(DateTime pDateFrom, DateTime pDateTo, float pTimeFrom, float pTimeTo, string pUsername)
  {
   float fltReturn = 0;
   float fltTimeFrom = pTimeFrom;
   float fltTimeTo = pTimeTo;
   DateTime dteTemp = pDateFrom;
   while (dteTemp < pDateTo)
   {
    if (clsShift.IsWorkingShift(clsShift.GetDayShiftCode(pUsername, dteTemp)))
     fltReturn++;
    dteTemp = dteTemp.AddDays(1);
   }
   if (!clsShift.IsWorkingShift(clsShift.GetDayShiftCode(pUsername, pDateFrom)))
    fltTimeFrom = 0;

   if (!clsShift.IsWorkingShift(clsShift.GetDayShiftCode(pUsername, pDateTo)))
    fltTimeTo = 0;

   fltReturn = fltReturn + (fltTimeTo - fltTimeFrom);
   return fltReturn;
  }

  public static bool HasEnoughBalance(double pLeaveUnit, string pUsername, string pLeaveType)
  {
   bool blnReturn = false;
   float fltBalance = 0;
   bool blnHasBalance = clsLeaveType.IsHasBalance(pLeaveType);
   if (blnHasBalance)
   {
    fltBalance = clsLeaveBalance.GetRemainingBalance(pLeaveType, pUsername);
    blnReturn = (fltBalance >= pLeaveUnit ? true : false);
   }
   else
    blnReturn = true;
   return blnReturn;
  }

  public static bool HasExistingApplication(string pUsername, DateTime pDateFrom, DateTime pDateTo)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavcode FROM HR.Leave WHERE username='" + pUsername + "' AND leavstat IN ('F','A') AND (('" + pDateFrom + "' >=  datestrt AND '" + pDateFrom + "' < dateend) OR ('" + pDateFrom + "' < datestrt AND '" + pDateTo + "' >= dateend) OR ('" + pDateTo + "' >  datestrt AND '" + pDateTo + "' <= dateend))";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable GetTopRecords(LeaveUsers pLeaveUsers, int pTop, string pUserName)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pLeaveUsers)
    {
     case LeaveUsers.Requestor:
      cmd.CommandText = "SELECT TOP " + pTop + " leavcode,datefile,datestrt,dateend,reason,apphname,leavstat,leavtype,username FROM HR.Leave WHERE username='" + pUserName + "' ORDER BY datefile DESC";
      break;
     case LeaveUsers.Approver:
      cmd.CommandText = "SELECT TOP " + pTop + " leavcode,datefile,datestrt,dateend,reason,apphname,leavstat,leavtype,username FROM HR.Leave WHERE apphname='" + pUserName + "' AND leavstat='F' ORDER BY datefile DESC";
      break;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetPageRecords(LeaveUsers pLeaveUsers, int pPage, string pUsername, string pStatus, string pLeaveType)
  {
   DataTable tblReturn = new DataTable();
   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intStart = ((pPage - 1) * intPageSize) + 1;
   int intEnd = pPage * intPageSize;
   string strWhere = "";

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pStatus != "ALL")
     strWhere = " AND leavstat='" + pStatus + "'";

    if (pLeaveType != "ALL")
     strWhere = " AND leavtype='" + pLeaveType + "'";

    if (pLeaveUsers == LeaveUsers.Requestor)
     cmd.CommandText = "SELECT * FROM (SELECT leavcode,datefile,datestrt,dateend,reason,apphname,leavstat,leavtype,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Leave WHERE username='" + pUsername + "'" + strWhere + ") AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    else if (pLeaveUsers == LeaveUsers.Approver)
     cmd.CommandText = "SELECT * FROM (SELECT leavcode,datefile,datestrt,dateend,reason,apphname,leavstat,leavtype,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Leave WHERE apphname='" + pUsername + "'" + strWhere + ") AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetPaging(LeaveUsers pLeaveUsers, int pPage, string pUserName, string pStatus, string pPageName)
  {
   string strReturn = "";

   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intTRows = 0;
   int intTRowsTemp = 0;
   int intPage = 1;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pLeaveUsers == LeaveUsers.Requestor)
     cmd.CommandText = "SELECT COUNT(leavcode) FROM HR.Leave WHERE username='" + pUserName + "'" + (pStatus == "ALL" ? "" : " AND leavstat='" + pStatus + "'");
    else if (pLeaveUsers == LeaveUsers.Approver)
     cmd.CommandText = "SELECT COUNT(leavcode) FROM HR.Leave WHERE apphname='" + pUserName + "'" + (pStatus == "ALL" ? "" : " AND leavstat='" + pStatus + "'");
    cn.Open();
    try { intTRows = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intTRows = 0; }
   }

   intTRowsTemp = intTRows;
   while (intTRowsTemp > 0)
   {
    if (pPage == intPage)
     strReturn += (intPage == 1 ? "" : "") + "&nbsp;" + intPage;
    else
     strReturn += "&nbsp;&nbsp;<a href='" + pPageName + ".aspx?page=" + intPage + "'>" + intPage + "</a>";
    intPage++;
    intTRowsTemp -= intPageSize;
   }

   return strReturn;
  }

  public static string GetRequestStatusIcon(string pLeaveStatus)
  {
   string strReturn = "";
   if (pLeaveStatus == "V")
    strReturn = "Disapproved.png";
   else if (pLeaveStatus == "D")
    strReturn = "Disapproved.png";
   else if (pLeaveStatus == "F")
    strReturn = "Approval.png";
   else if (pLeaveStatus == "A")
    strReturn = "Approved.png";
   else if (pLeaveStatus == "C")
    strReturn = "Disapproved.png";
   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pLeaveStatus, string pApproverName)
  {
   string strReturn = "";
   if (pLeaveStatus == "V")
    strReturn = "The application has been voided by the application";
   else if (pLeaveStatus == "D")
    strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pApproverName, 3);
   else if (pLeaveStatus == "F")
    strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pApproverName, 3);
   else if (pLeaveStatus == "A")
    strReturn = "Approved by " + clsSpeedo.AssignUsernameLink(pApproverName, 3);
   else if (pLeaveStatus == "C")
    strReturn = "The user cancelled the application";
   return strReturn;
  }

  public static LeaveStatus ToLeaveStatus(string pLeaveStatusCode)
  {
   switch (pLeaveStatusCode)
   {
    case "C":
     return LeaveStatus.Cancelled;
    case "F":
     return LeaveStatus.ForApproval;
    case "A":
     return LeaveStatus.Approved;
    case "D":
     return LeaveStatus.Disapproved;
    case "V":
     return LeaveStatus.Void;
    default:
     return LeaveStatus.Cancelled;
   }
  }

  public static string ToLeaveStatusDesc(string pLeaveStatusCode)
  {
   switch (pLeaveStatusCode)
   {
    case "C":
     return "Cancelled";
    case "F":
     return "For Approval";
    case "A":
     return "Approved";
    case "D":
     return "Disapproved";
    case "V":
     return "Void";
    default:
     return "Cancelled";
   }
  }

  public static int GetTotalRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(leavcode) FROM HR.Leave";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

  ///////////////////////////////////
  ///////// Reports Members /////////
  ///////////////////////////////////

  public static DataTable DSRLeaveApplications(string pDivisionCode, DateTime pDateFrom, DateTime pDateTo)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("StatusCode");
   tblReturn.Columns.Add("Status");
   tblReturn.Columns.Add("DateFiled");
   tblReturn.Columns.Add("Employee");
   tblReturn.Columns.Add("LeaveType");
   tblReturn.Columns.Add("DateStart");
   tblReturn.Columns.Add("DateEnd");
   tblReturn.Columns.Add("Duration");
   tblReturn.Columns.Add("Approver");
   tblReturn.Columns.Add("ApproverDate");
   tblReturn.Columns.Add("Department");
   tblReturn.Columns.Add("Reason");
   tblReturn.Columns.Add("Remarks");

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Leave WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "') AND username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND ((datestrt BETWEEN '" + pDateFrom + "' AND '" + pDateTo + "') OR (dateend BETWEEN '" + pDateFrom + "' AND '" + pDateTo + "')) ORDER BY datestrt";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     DataRow drw = tblReturn.NewRow();
     drw["StatusCode"] = dr["leavstat"].ToString();
     drw["Status"] = clsLeave.ToLeaveStatusDesc(dr["leavstat"].ToString());
     drw["DateFiled"] = clsValidator.CheckDate(dr["datefile"].ToString()).ToString("MM/dd/yyyy<br>hh:mm tt");
     drw["Employee"] = clsEmployee.GetName(dr["username"].ToString(), EmployeeNameFormat.LastFirst);
     drw["LeaveType"] = clsLeaveType.GetDescription(dr["leavtype"].ToString());
     drw["DateStart"] = clsValidator.CheckDate(dr["datestrt"].ToString()).ToString("MM/dd/yyyy<br>hh:mm tt");
     drw["DateEnd"] = clsValidator.CheckDate(dr["dateend"].ToString()).ToString("MM/dd/yyyy<br>hh:mm tt");
     drw["Duration"] = dr["units"].ToString();
     drw["Approver"] = clsEmployee.GetName(dr["apphname"].ToString(), EmployeeNameFormat.LastFirst);
     if (clsValidator.CheckDate(dr["apphdate"].ToString()) == clsDateTime.SystemMinDate)
      drw["ApproverDate"] = "-";
     else
      drw["ApproverDate"] = clsValidator.CheckDate(dr["apphdate"].ToString()).ToString("MM/dd/yyyy<br>hh:mm tt");
     drw["Department"] = clsDepartment.GetName(clsEmployee.GetDepartmentCode(dr["username"].ToString()));
     drw["Reason"] = dr["reason"].ToString();
     drw["Remarks"] = dr["apphrem"].ToString();
     tblReturn.Rows.Add(drw);
    }
    dr.Close();
   }

   return tblReturn;
  }

  // Added by Charlie 
  // March 21, 2011
  // Get Leave record per emplyee, per leave type
  public static DataTable GetRecord(string pUsername, string pLeaveType)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavtype, datefile, units, reason, leavstat, apphname FROM HR.Leave WHERE username = @username AND leavtype = @leavtype ORDER BY datefile DESC";
    cmd.Parameters.AddWithValue("@username", pUsername);
    cmd.Parameters.AddWithValue("@leavtype", pLeaveType);
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }
     //Added by charlie bachiller 11-28-2011
  public static DataTable GetNotificationForApproval(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT * FROM HR.Leave WHERE ((apphname='" + pUsername + "' AND leavstat='F'))";
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

     //Added by Rollie Flores 2015-07-03
  public static string getLeaveType(string pLeaveCode)
  {
      string strReturn = "";
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT leavtype FROM HR.Leave WHERE leavcode=@leavcode";
          cmd.Parameters.Add(new SqlParameter("@leavcode", pLeaveCode));
          cn.Open();
          try
          { strReturn = cmd.ExecuteScalar().ToString(); }
          catch
          { }
      }
      return strReturn;
  }
        //added by Calvin Cavite FEB 13, 2018
  private void getLeaveCode()
  {
            string lvcode = "";
            int strLvcode = 0; 
            using(SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString)) {

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select top 1 pvalue from Speedo.Keys WHERE pkey='leavcode' order by pvalue desc";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read()){

                    lvcode = dr["pvalue"].ToString();
                }
                dr.Close();
                if (lvcode == null || lvcode == "")
                {
                    strLvcode = clsValidator.CheckInteger(lvcode) + 1;
                    lvcode = ("LV" + strLvcode.ToString()).Substring(strLvcode.ToString().Length - 1);
                    LeaveCode = lvcode;
                }
                else {
                    char[] removechar = { 'L', 'V' };
                    string nwLvcode = lvcode.TrimStart(removechar);
                    lvcode = nwLvcode;
                    strLvcode = clsValidator.CheckInteger(nwLvcode) + 1;
                    lvcode = ("LV" + strLvcode.ToString());
                    LeaveCode = lvcode;
                }
            }
  }
 }
}