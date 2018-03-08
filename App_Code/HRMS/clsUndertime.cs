using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRMS
{
 public enum UndertimeUsers { Requestor, Approver, HrAdmin }
 public enum UndertimeStatus { Cancelled, ForApproval, Approved, Disapproved, Void }
 public enum UndertimeMailType
 {
  FiledAcknowledgementRequestor,
  FiledNotificationApprover,
  ApprovedAcknowledgementApprover,
  ApprovedNotificationRequestor,
  DisapprovedAcknowledgementApprover,
  DisapprovedNotificationRequestor
 }

 public class clsUndertime : IDisposable
 {
  private string _strUndertimeCode;
  private string _strUsername;
  private DateTime _dteDateFiled;
  private DateTime _dteDateApplied;
  private string _strReason;
  private string _strApproverUsername;
  private DateTime _dteApproverDate;
  private string _strApproverRemarks;
  private string _strUTStatus;

  public clsUndertime() { }
  public clsUndertime(string pUTCode) { _strUndertimeCode = pUTCode; }

  public string UndertimeCode { get { return _strUndertimeCode; } set { _strUndertimeCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime DateFiled { get { return _dteDateFiled; } set { _dteDateFiled = value; } }
  public DateTime DateApplied { get { return _dteDateApplied; } set { _dteDateApplied = value; } }
  public string Reason { get { return _strReason; } set { _strReason = value; } }
  public string ApproverUsername { get { return _strApproverUsername; } set { _strApproverUsername = value; } }
  public DateTime ApproverDate { get { return _dteApproverDate; } set { _dteApproverDate = value; } }
  public string ApproverRemarks { get { return _strApproverRemarks; } set { _strApproverRemarks = value; } }
  public string Status { get { return _strUTStatus; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.UnderTime WHERE utcode='" + _strUndertimeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _dteDateFiled = Convert.ToDateTime(dr["datefile"].ToString());
     _dteDateApplied = Convert.ToDateTime(dr["dateapp"].ToString());
     _strReason = dr["reason"].ToString();
     _strApproverUsername = dr["apphname"].ToString();
     _dteApproverDate = clsValidator.CheckDate(dr["apphdate"].ToString());
     _strApproverRemarks = dr["apphrem"].ToString();
     _strUTStatus = dr["utstat"].ToString();
    }
    dr.Close();
   }
  }

  public int Insert()
  {
   int intReturn = 0;

            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                cn.Open();
                getUTcode(); // ADDED by calvin cavite FEB 15, 2018

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "INSERT INTO HR.UnderTime (utcode, username,datefile,dateapp,reason,apphname,apphdate,apphrem,utstat) "+
                                  "VALUES (@utcode,@username,@datefile,@dateapp,@reason,@apphname,@apphdate,@apphrem,@utstat)";

                cmd.Parameters.Add(new SqlParameter("@utcode", _strUndertimeCode));
                cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
                cmd.Parameters.Add(new SqlParameter("@datefile", _dteDateFiled));
                cmd.Parameters.Add(new SqlParameter("@dateapp", _dteDateApplied));
                cmd.Parameters.Add(new SqlParameter("@reason",_strReason));
                cmd.Parameters.Add(new SqlParameter("@apphname", _strApproverUsername));
                cmd.Parameters.Add(new SqlParameter("@apphdate",DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@apphrem","for approval"));
                cmd.Parameters.Add(new SqlParameter("utstat","F"));
                intReturn = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();

                cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue + 1 WHERE pkey='utcode'";
                cmd.ExecuteNonQuery();  

                cn.Close();
                /*
                SqlCommand cmd = new SqlCommand("spUndertimeInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@datefile", SqlDbType.DateTime);
                cmd.Parameters.Add("@dateapp", SqlDbType.DateTime);
                cmd.Parameters.Add("@reason", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@apphname", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@utstat", SqlDbType.Char, 1);
                cmd.Parameters.Add("@utcode", SqlDbType.Char, 9);
                cmd.Parameters["@username"].Value = _strUsername;
                cmd.Parameters["@datefile"].Value = _dteDateFiled;
                cmd.Parameters["@dateapp"].Value = _dteDateApplied;
                cmd.Parameters["@reason"].Value = _strReason;
                cmd.Parameters["@apphname"].Value = _strApproverUsername;
                cmd.Parameters["@utstat"].Value = "F";
                cmd.Parameters["@utcode"].Direction = ParameterDirection.Output;    
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                _strUndertimeCode = cmd.Parameters["@utcode"].Value.ToString();
                */
            }

   if (intReturn > 0)
   {
    SendNotification(UndertimeMailType.FiledAcknowledgementRequestor);
    SendNotification(UndertimeMailType.FiledNotificationApprover);
   }

   return intReturn;
  }

  public int Approve()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.UnderTime SET apphrem=@apphrem,apphdate='" + _dteApproverDate + "',utstat='A' WHERE utcode='" + _strUndertimeCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(UndertimeMailType.ApprovedAcknowledgementApprover);
    SendNotification(UndertimeMailType.ApprovedNotificationRequestor);
   }

   return intReturn;
  }

  public int Disapprove()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.UnderTime SET apphrem=@apphrem,apphdate='" + _dteApproverDate + "',utstat='D' WHERE utcode='" + _strUndertimeCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(UndertimeMailType.DisapprovedAcknowledgementApprover);
    SendNotification(UndertimeMailType.DisapprovedNotificationRequestor);
   }

   return intReturn;
  }

  public int Cancel()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.UnderTime SET utstat='C' WHERE utcode='" + _strUndertimeCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void SendNotification(UndertimeMailType pMailType)
  {
   string strSpeedoUrl = ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   string strSubject = "";
   string strBody = "";
   string strRequestorName = clsEmployee.GetName(_strUsername);
   string strRequestorEmail = clsUsers.GetEmail(_strUsername);
   string strApproverName = clsEmployee.GetName(_strApproverUsername);
   string strApproverEmail = clsUsers.GetEmail(_strApproverUsername);
   string strURLUndertimeDetails = strSpeedoUrl + "/HR/HRMS/Undertime/UndertimeDetails.aspx?utcode=" + _strUndertimeCode;
   string strURLUndertimeDetailsA = strSpeedoUrl + "/HR/HRMS/Undertime/UndertimeDetailsA.aspx?utcode=" + _strUndertimeCode;

   switch (pMailType)
   {
    case UndertimeMailType.FiledAcknowledgementRequestor:
     strSubject = "Delivered: Undertime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your Undertime Application has been successfully sent to " + strApproverName + ".<br>" +
               "** Undertime Details **<br>" +
               "Date Filed: " + _dteDateFiled.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Applied: " + _dteDateApplied.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br><br>" +
               "<a href='" + strURLUndertimeDetails + "'>Click here to view your online undertime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLUndertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case UndertimeMailType.FiledNotificationApprover:
     strSubject = "For Your Approval: Undertime Application - " + strRequestorName;
     strBody = "Hi " + strApproverName + ",<br><br>" +
               strRequestorName + " has just sent an Undertime Application with the following details:<br>" +
               "Date Filed: " + _dteDateFiled.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Date Applied: " + _dteDateApplied.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br><br>" +
               "<a href='" + strURLUndertimeDetailsA + "'>Click here to view the online undertime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLUndertimeDetailsA + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
     break;

    case UndertimeMailType.ApprovedAcknowledgementApprover:
     strSubject = "Delivered: Approved Undertime Application - " + strRequestorName;
     strBody = "Hi " + strApproverName + ",<br><br>" +
               "You approved an Undertime Application.<br><br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLUndertimeDetailsA + "'>Click here to view the online undertime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLUndertimeDetailsA + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
     break;

    case UndertimeMailType.ApprovedNotificationRequestor:
     strSubject = "Approved: Undertime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strApproverName + " has approved your Undertime Application.<br><br>" +
               "<a href='" + strURLUndertimeDetails + "'>Click here to view the online undertime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLUndertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case UndertimeMailType.DisapprovedAcknowledgementApprover:
     strSubject = "Delivered: Disapproved Undertime Application - " + strRequestorName;
     strBody = "Hi " + strApproverName + ",<br><br>" +
               "You disapproved an Undertime Application.<br><br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLUndertimeDetailsA + "'>Click here to view the online undertime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLUndertimeDetailsA + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
     break;

    case UndertimeMailType.DisapprovedNotificationRequestor:
     strSubject = "Disapproved: Undertime Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strApproverName + " has disapproved your Undertime Application.<br><br>" +
               "<a href='" + strURLUndertimeDetails + "'>Click here to view the online undertime application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLUndertimeDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;
   }

  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static void AuthenticateAccessForm(UndertimeUsers pUndertimeUsers, string pUsername, string pUndertimeCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pUndertimeUsers == UndertimeUsers.Requestor)
     cmd.CommandText = "SELECT username FROM HR.UnderTime WHERE utcode='" + pUndertimeCode + "' AND username='" + pUsername + "'";
    else if (pUndertimeUsers == UndertimeUsers.Approver)
     cmd.CommandText = "SELECT apphname FROM HR.UnderTime WHERE utcode='" + pUndertimeCode + "' AND apphname='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }

   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static bool HasExistingApplication(string pUsername, DateTime pDateApp)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT utcode FROM HR.Undertime WHERE username='" + pUsername + "' AND utstat IN ('F','A') AND DATEPART(mm,dateapp)='" + pDateApp.Month + "' AND DATEPART(dd,dateapp)='" + pDateApp.Day + "' AND DATEPART(yy,dateapp)='" + pDateApp.Year + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static bool AuthenticateAccess(string pUsername, string pUndertimeCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM HR.Undertime WHERE utcode='" + pUndertimeCode + "' AND (username='" + pUsername + "' OR apphname='" + pUsername + "')";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static int GetTotalForAttention(string pUsername)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(utcode) FROM HR.Undertime WHERE ((apphname='" + pUsername + "' AND utstat='F'))";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intReturn = 0; }
   }
   return intReturn;
  }

  public static DataTable GetTopRecords(UndertimeUsers pUTUsers, int pTop, string pUserName)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pUTUsers)
    {
     case UndertimeUsers.Requestor:
      cmd.CommandText = "SELECT TOP " + pTop + " utcode,datefile,dateapp,apphname,utstat,username FROM HR.Undertime WHERE username='" + pUserName + "' ORDER BY datefile DESC";
      break;
     case UndertimeUsers.Approver:
      cmd.CommandText = "SELECT TOP " + pTop + " utcode,datefile,dateapp,apphname,utstat,username FROM HR.Undertime WHERE apphname='" + pUserName + "' AND utstat='F' ORDER BY datefile DESC";
      break;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetPageRecords(UndertimeUsers pUTUsers, int pPage, string pUserName, string pStatus)
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
     if (pUTUsers == UndertimeUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT utcode,datefile,dateapp,apphname,utstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Undertime WHERE username='" + pUserName + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pUTUsers == UndertimeUsers.Approver)
      cmd.CommandText = "SELECT * FROM (SELECT utcode,datefile,dateapp,apphname,utstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Undertime WHERE apphname='" + pUserName + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    else
    {
     if (pUTUsers == UndertimeUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT utcode,datefile,dateapp,apphname,utstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Undertime WHERE username='" + pUserName + "' AND utstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pUTUsers == UndertimeUsers.Approver)
      cmd.CommandText = "SELECT * FROM (SELECT utcode,datefile,dateapp,apphname,utstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.Undertime WHERE apphname='" + pUserName + "' AND utstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetPaging(UndertimeUsers pUTUsers, int pPage, string pUserName, string pStatus, string pPageName)
  {
   string strReturn = "";

   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intTRows = 0;
   int intTRowsTemp = 0;
   int intPage = 1;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pUTUsers == UndertimeUsers.Requestor)
     cmd.CommandText = "SELECT COUNT(utcode) FROM HR.Undertime WHERE username='" + pUserName + "'" + (pStatus == "all" ? "" : " AND utstat='" + pStatus + "'");
    else if (pUTUsers == UndertimeUsers.Approver)
     cmd.CommandText = "SELECT COUNT(utcode) FROM HR.Undertime WHERE apphname='" + pUserName + "'" + (pStatus == "all" ? "" : " AND utstat='" + pStatus + "'");
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

  public static string GetRequestStatusIcon(string pUTStatus)
  {
   string strReturn = "";
   if (pUTStatus == "V")
    strReturn = "Disapproved.png";
   else if (pUTStatus == "D")
    strReturn = "Disapproved.png";
   else if (pUTStatus == "F")
    strReturn = "Approval.png";
   else if (pUTStatus == "A")
    strReturn = "Approved.png";
   else if (pUTStatus == "C")
    strReturn = "Disapproved.png";
   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pUTStatus, string pApproverName)
  {
   string strReturn = "";
   if (pUTStatus == "V")
    strReturn = "The application has been voided by the application";
   else if (pUTStatus == "D")
    strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pApproverName, 3);
   else if (pUTStatus == "F")
    strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pApproverName, 3);
   else if (pUTStatus == "A")
    strReturn = "Approved by " + clsSpeedo.AssignUsernameLink(pApproverName, 3);
   else if (pUTStatus == "C")
    strReturn = "The user cancelled the application";
   return strReturn;
  }

  public static string ToUndertimeStatusText(string pUTStatus)
  {
   string strReturn = "";
   if (pUTStatus == "V")
    strReturn = "Void";
   else if (pUTStatus == "D")
    strReturn = "Disapproved";
   else if (pUTStatus == "F")
    strReturn = "For Approval";
   else if (pUTStatus == "A")
    strReturn = "Approved";
   else if (pUTStatus == "C")
    strReturn = "Cancelled";
   return strReturn;
  }

  public static UndertimeStatus ToUndertimeStatus(string pUTStatusCode)
  {
   switch (pUTStatusCode)
   {
    case "X":
     return UndertimeStatus.Cancelled;
    case "F":
     return UndertimeStatus.ForApproval;
    case "A":
     return UndertimeStatus.Approved;
    case "D":
     return UndertimeStatus.Disapproved;
    case "V":
     return UndertimeStatus.Void;
    default:
     return UndertimeStatus.Cancelled;
   }
  }

  public static int GetTotalRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(utcode) FROM HR.UnderTime";
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
          cmd.CommandText = "SELECT * FROM HR.Undertime WHERE ((apphname='" + pUsername + "' AND utstat='F'))";
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);

      }
      return tblReturn;
  }

  //ADDED BY CALVIN CAVITE FEB 15, 2018
  public void getUTcode()
  {
    string strUTcode = "";
    int ut_code = 0;
    using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
    {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select top 1 pvalue from Speedo.Keys WHERE pkey='utcode' order by pvalue desc";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strUTcode = dr["pvalue"].ToString();
                }
                if (strUTcode == null || strUTcode == "")
                {
                    ut_code = clsValidator.CheckInteger(strUTcode) + 1;
                    strUTcode = ("UT" + ut_code.ToString());
                    UndertimeCode = strUTcode;
                }
                else
                {
                    char[] removechar = { 'U', 'T' };
                    string nwUTcode = strUTcode.TrimStart(removechar);
                    strUTcode = nwUTcode;
                    ut_code = clsValidator.CheckInteger(strUTcode) + 1;
                    strUTcode = ("UT" + ut_code.ToString());
                    UndertimeCode = strUTcode;
                }
    }
  }

 }
}