using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRMS
{
 public enum OBUsers { Requestor, ApproverRequestor, ApproverHead, HRAdmin }
 public enum OBStatus { Cancelled, ForApproval, Approved, Disapproved }
 public enum OBMailType
 {
  FiledAcknowledgementRRequestor,
  FiledAcknowledgementHRequestor,
  FiledNotificationRApprover,
  FiledNotificationHApprover,
  ApprovedAcknowledgementRApprover,
  ApprovedAcknowledgementHApprover,
  ApprovedNotificationRRequestor,
  ApprovedNotificationHRequestor,
  DisapprovedAcknowledgementRApprover,
  DisapprovedAcknowledgementHApprover,
  DisapprovedNotificationRRequestor,
  DisapprovedNotificationHRequestor
 }

 public class clsOB : IDisposable
 {
  private string _strOBCode;
  private string _strUsername;
  private DateTime _dteDateFile;
  private string _strReason;
  private string _strOBType;
  private string _strDepartmentCode;
  private string _strApproverRequestorName;
  private DateTime _dteApproverRequestorDate;
  private string _strApproverRequestorRemarks;
  private string _strApproverRequestorStatus;
  private string _strApproverHeadName;
  private DateTime _dteApproverHeadDate;
  private string _strApproverHeadRemarks;
  private string _strApproverHeadStatus;
  private string _strStatus;

  public clsOB() { }
  public clsOB(string pOBCode) { _strOBCode = pOBCode; }

  public string OBCode { get { return _strOBCode; } set { _strOBCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime DateFile { get { return _dteDateFile; } set { _dteDateFile = value; } }
  public string Reason { get { return _strReason; } set { _strReason = value; } }
  public string OBType { get { return _strOBType; } set { _strOBType = value; } }
  public string DepartmentCode { get { return _strDepartmentCode; } set { _strDepartmentCode = value; } }
  public string ApproverRequestorName { get { return _strApproverRequestorName; } set { _strApproverRequestorName = value; } }
  public DateTime ApproverRequestorDate { get { return _dteApproverRequestorDate; } set { _dteApproverRequestorDate = value; } }
  public string ApproverRequestorRemarks { get { return _strApproverRequestorRemarks; } set { _strApproverRequestorRemarks = value; } }
  public string ApproverRequestorStatus { get { return _strApproverRequestorStatus; } set { _strApproverRequestorStatus = value; } }
  public string ApproverHeadName { get { return _strApproverHeadName; } set { _strApproverHeadName = value; } }
  public DateTime ApproverHeadDate { get { return _dteApproverHeadDate; } set { _dteApproverHeadDate = value; } }
  public string ApproverHeadRemarks { get { return _strApproverHeadRemarks; } set { _strApproverHeadRemarks = value; } }
  public string ApproverHeadStatus { get { return _strApproverHeadStatus; } set { _strApproverHeadStatus = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.OB WHERE obcode='" + _strOBCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _dteDateFile = clsValidator.CheckDate(dr["datefile"].ToString());
     _strReason = dr["reason"].ToString();
     _strOBType = dr["obtype"].ToString();
     _strDepartmentCode = dr["deptcode"].ToString();
     _strApproverRequestorName = dr["apprname"].ToString();
     _dteApproverRequestorDate = clsValidator.CheckDate(dr["apprdate"].ToString());
     _strApproverRequestorRemarks = dr["apprrem"].ToString();
     _strApproverRequestorStatus = dr["apprstat"].ToString();
     _strApproverHeadName = dr["apphname"].ToString();
     _dteApproverHeadDate = clsValidator.CheckDate(dr["apphdate"].ToString());
     _strApproverHeadRemarks = dr["apphrem"].ToString();
     _strApproverHeadStatus = dr["apphstat"].ToString();
     _strStatus = dr["obstat"].ToString();
    }
    dr.Close();
   }
  }

  public int Insert()
  {

   getOBCode(); //added by calvin cavite FEB 14, 2018 P.S Happy Valentines <3! 
            
   int intReturn = 0;


            //ADDED BY CALVIN CAVITE FEB 15, 2018
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString)) {
                string ob_insert = "INSERT INTO HR.OB (obcode,username,datefile,reason,obtype,deptcode,apprname,apprdate,apprrem,apprstat,apphname,apphdate,apphrem,apphstat,obstat) " +
                                  "VALUES (@obcode,@username,@datefile,@reason,@obtype,@deptcode,@apprname,@apprdate,@apprrem,@apprstat,@apphname,@apphdate,@apphrem,@apphstat,@obstat)";

                SqlCommand cmd = new SqlCommand(ob_insert, cn);
                cmd.Parameters.Add(new SqlParameter("@obcode", _strOBCode));
                cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
                cmd.Parameters.Add(new SqlParameter("@datefile", _dteDateFile));
                cmd.Parameters.Add(new SqlParameter("@reason", _strReason));
                cmd.Parameters.Add(new SqlParameter("@obtype", _strOBType));
                cmd.Parameters.Add(new SqlParameter("@deptcode", _strDepartmentCode));

                if (_strOBType == "0")
                {
                    cmd.Parameters.Add(new SqlParameter("@apprname", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@apprdate", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@apprrem", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@apprstat", DBNull.Value));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@apprname", _strApproverRequestorName));
                    cmd.Parameters.Add(new SqlParameter("@apprdate", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@apprrem", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@apprstat", "F"));
                }
                cmd.Parameters.Add(new SqlParameter("@apphname", _strApproverHeadName));
                cmd.Parameters.Add(new SqlParameter("@apphdate", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@apphrem", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@apphstat", "F"));
                cmd.Parameters.Add(new SqlParameter("@obstat", "F"));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue + 1 WHERE pkey='obcode'";
                cmd.ExecuteNonQuery();
                _strOBCode = cmd.Parameters["@obcode"].Value.ToString();
            }
            return intReturn;

            
            /*REMOVE BY CALVIN CAVITE FEB 15, 2018
            SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString);
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmd = new SqlCommand("spOBInsert", cn);
            cmd.Transaction = tran;
            try
            {
                intReturn = cmd.ExecuteNonQuery();               

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@datefile", SqlDbType.DateTime);
                cmd.Parameters.Add("@reason", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@obtype", SqlDbType.Char, 1);
                cmd.Parameters.Add("@deptcode", SqlDbType.Char, 3);
                cmd.Parameters.Add("@apprname", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@apprstat", SqlDbType.Char, 1);
                cmd.Parameters.Add("@apphname", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@obcode", SqlDbType.Char, 9);
                cmd.Parameters["@username"].Value = _strUsername;
                cmd.Parameters["@datefile"].Value = _dteDateFile;
                cmd.Parameters["@reason"].Value = _strReason;
                cmd.Parameters["@obtype"].Value = _strOBType;
                cmd.Parameters["@deptcode"].Value = _strDepartmentCode;    
                if (_strOBType == "0")
                {
                 cmd.Parameters["@apprname"].Value = DBNull.Value;
                 cmd.Parameters["@apprstat"].Value = DBNull.Value;
                }
                else
                {
                 cmd.Parameters["@apprname"].Value = _strApproverRequestorName;
                 cmd.Parameters["@apprstat"].Value = "F";
                }
                cmd.Parameters["@apphname"].Value = _strApproverHeadName;
                cmd.Parameters["@obcode"].Direction = ParameterDirection.Output;
                _strOBCode = cmd.Parameters["@obcode"].Value.ToString();
           

                tran.Commit();
            }
            catch { tran.Rollback(); }
            finally { cn.Close(); }
            return intReturn;
            */
        }

  public bool Cancel()
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.OB SET obstat='C' WHERE obcode='" + _strOBCode + "'";
    cn.Open();
    blnReturn = cmd.ExecuteNonQuery() > 0;
   }
   return blnReturn;
  }

  public int ApproveRequestor()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {    
    SqlCommand cmd = cn.CreateCommand();    
    cmd.CommandText = "UPDATE HR.OB SET apprrem=@apprrem,apprdate='" + _dteApproverRequestorDate + "',apprstat='A' WHERE obcode='" + _strOBCode + "'";
    cmd.Parameters.Add("@apprrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apprrem"].Value = _strApproverRequestorRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OBMailType.ApprovedAcknowledgementRApprover);
    SendNotification(OBMailType.ApprovedNotificationRRequestor);
    SendNotification(OBMailType.FiledNotificationHApprover);
   }

   return intReturn;
  }

  public int ApproveHead()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {    
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.OB SET apphrem=@apphrem,apphdate='" + _dteApproverHeadDate + "',apphstat='A',obstat='A' WHERE obcode='" + _strOBCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OBMailType.ApprovedAcknowledgementHApprover);
    SendNotification(OBMailType.ApprovedNotificationHRequestor);    
   }

   return intReturn;
  }

  public int DisapproveRequestor()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {    
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.OB SET apprrem=@apprrem,apprdate='" + _dteApproverRequestorDate + "',apprstat='D',obstat='D' WHERE obcode='" + _strOBCode + "'";
    cmd.Parameters.Add("@apprrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apprrem"].Value = _strApproverRequestorRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();   
   }

   if (intReturn > 0)
   {
    SendNotification(OBMailType.DisapprovedAcknowledgementRApprover);
    SendNotification(OBMailType.DisapprovedNotificationRRequestor);
   }

   return intReturn;
  }

  public int DisapproveHead()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {    
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.OB SET apphrem=@apphrem, apphdate='" + _dteApproverHeadDate + "', apphstat='D', obstat='D' WHERE obcode='" + _strOBCode + "'";
    cmd.Parameters.Add("@apphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apphrem"].Value = _strApproverHeadRemarks;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }

   if (intReturn > 0)
   {
    SendNotification(OBMailType.DisapprovedAcknowledgementHApprover);
    SendNotification(OBMailType.DisapprovedNotificationHRequestor);
   }

   return intReturn;
  }

  public void SendNotification(OBMailType pMailType)
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
   string strURLOBDetails = strSpeedoUrl + "/HR/HRMS/OB/OBDetails.aspx?obcode=" + _strOBCode;
   string strURLOBDetailsAR = strSpeedoUrl + "/HR/HRMS/OB/OBDetailsAR.aspx?obcode=" + _strOBCode;
   string strURLOBDetailsAH = strSpeedoUrl + "/HR/HRMS/OB/OBDetailsA.aspx?obcode=" + _strOBCode;

   switch (pMailType)
   {
    case OBMailType.FiledAcknowledgementRRequestor:
     strSubject = "Delivered: OB Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your OB Application has been successfully sent to " + strRApproverName + ".<br>" +
               "** OB Details **<br>" +               
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "OB Type: " + clsOB.GetOBTypeDesc(_strOBType) + "<br>" +
               "OB Rendered To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "** Schedule Details **<br>" +
               clsOBDetails.GetHTMLTable(_strOBCode) + "<br><br>" +
               "<a href='" + strURLOBDetails + "'>Click here to view your online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OBMailType.FiledAcknowledgementHRequestor:
     strSubject = "Delivered: OB Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               "Your OB Application has been successfully sent to " + strHApproverName + ".<br>" +
               "** OB Details **<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "OB Type: " + clsOB.GetOBTypeDesc(_strOBType) + "<br>" +
               "OB Rendered To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "** Schedule Details **<br>" +
               clsOBDetails.GetHTMLTable(_strOBCode) + "<br><br>" +
               "<a href='" + strURLOBDetails + "'>Click here to view your online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OBMailType.FiledNotificationRApprover:
     strSubject = "For Your Approval: OB Application - " + strRequestorName;
     strBody = "Hi " + strRApproverName + ",<br><br>" +
               strRequestorName + " has just sent an OB Application with the following details:<br>" +               
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "OB Rendered To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "** Schedule Details **<br>" +
               clsOBDetails.GetHTMLTable(_strOBCode) + "<br><br>" +
               "<a href='" + strURLOBDetailsAR + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetailsAR + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRApproverEmail, strSubject, strBody);
     break;

    case OBMailType.FiledNotificationHApprover:
     strSubject = "For Your Approval: OB Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               strRequestorName + " has just sent an OB Application with the following details:<br>" +
               "Date Filed: " + _dteDateFile.ToString("ddd MMMM dd, yyyy hh:mm tt") + "<br>" +
               "Reason: " + _strReason + "<br>" +
               "OB Rendered To: " + clsDepartment.GetName(_strDepartmentCode) + "<br><br>" +
               "** Schedule Details **<br>" +
               clsOBDetails.GetHTMLTable(_strOBCode) + "<br><br>" +
               "<a href='" + strURLOBDetailsAH + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetailsAH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case OBMailType.ApprovedAcknowledgementRApprover:
     strSubject = "Delivered: Approved OB Application - " + strRequestorName;
     strBody = "Hi " + strRApproverName + ",<br><br>" +
               "You approved an OB Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOBDetailsAR + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetailsAR + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRApproverEmail, strSubject, strBody);
     break;

    case OBMailType.ApprovedAcknowledgementHApprover:
     strSubject = "Delivered: Approved OB Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You approved an OB Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOBDetailsAH + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetailsAH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case OBMailType.ApprovedNotificationRRequestor:
     strSubject = "Approved: OB Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strRApproverName + " has approved your OB Application.<br>" +
               "Your OB Application has been forwarded to " + strHApproverName + " for final approval.<br><br>" +
               "<a href='" + strURLOBDetails + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OBMailType.ApprovedNotificationHRequestor:
     strSubject = "Approved: OB Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has approved your OB Application.<br><br>" +
               "<a href='" + strURLOBDetails + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OBMailType.DisapprovedAcknowledgementRApprover:
     strSubject = "Delivered: Disapproved OB Application - " + strRequestorName;
     strBody = "Hi " + strRApproverName + ",<br><br>" +
               "You disapproved an OB Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOBDetailsAR + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetailsAR + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRApproverEmail, strSubject, strBody);
     break;

    case OBMailType.DisapprovedAcknowledgementHApprover:
     strSubject = "Delivered: Disapproved OB Application - " + strRequestorName;
     strBody = "Hi " + strHApproverName + ",<br><br>" +
               "You disapproved an OB Application.<br>" +
               "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strURLOBDetailsAH + "'>Click here to view the online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetailsAH + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strHApproverEmail, strSubject, strBody);
     break;

    case OBMailType.DisapprovedNotificationRRequestor:
     strSubject = "Disapproved: OB Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strRApproverName + " has disapproved your OB Application.<br><br>" +
               "<a href='" + strURLOBDetails + "'>Click here to view your online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

    case OBMailType.DisapprovedNotificationHRequestor:
     strSubject = "Disapproved: OB Application";
     strBody = "Hi " + strRequestorName + ",<br><br>" +
               strHApproverName + " has disapproved your OB Application.<br><br>" +
               "<a href='" + strURLOBDetails + "'>Click here to view your online OB application</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br>" +
               "<i>" + strURLOBDetails + "</i><br><br>" +
               "All the best,<br>Head Office Portal";
     clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
     break;

   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static void AuthenticateAccessForm(OBUsers pOBUsers, string pUsername, string pOBCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pOBUsers == OBUsers.Requestor)
     cmd.CommandText = "SELECT username FROM HR.OB WHERE obcode='" + pOBCode + "' AND username='" + pUsername + "'";
    else if (pOBUsers == OBUsers.ApproverRequestor)
     cmd.CommandText = "SELECT apprname FROM HR.OB WHERE obcode='" + pOBCode + "' AND apprname='" + pUsername + "'";
    else if (pOBUsers == OBUsers.ApproverHead)
     cmd.CommandText = "SELECT apphname FROM HR.OB WHERE obcode='" + pOBCode + "' AND apphname='" + pUsername + "'";
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
    cmd.CommandText = "SELECT username FROM HR.OB WHERE obcode='" + pOTCode + "' AND (username='" + pUsername + "' OR apprname='" + pUsername + "' OR apphname='" + pUsername + "')";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable DSGForApproval(DateTime pDateStart, DateTime pDateEnd)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT obcode,HR.OB.username,HR.OB.datefile,obtype,reason,apprname,apprstat,apphname,apphstat,lastname,firname FROM HR.Employees INNER JOIN HR.OB ON HR.Employees.username = HR.OB.username WHERE obstat='F' AND obcode IN (SELECT obcode FROM HR.OBDetails WHERE pstatus='1' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "')) AND HR.Employees.pstatus='1' AND HR.Employees.esttcode IN ('RE','PR') ORDER BY lastname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetTopRecords(OBUsers pOBUsers, int pTop, string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pOBUsers)
    {
     case OBUsers.Requestor:
      cmd.CommandText = "SELECT TOP " + pTop + " obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username FROM HR.OB WHERE username='" + pUsername + "' ORDER BY datefile DESC";
      break;
     case OBUsers.ApproverHead:
      cmd.CommandText = "SELECT TOP " + pTop + " obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username FROM HR.OB WHERE (apprname='" + pUsername + "' OR apphname='" + pUsername + "') AND obstat='F' ORDER BY datefile DESC";
      break;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTableHATop(int pTop, string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP " + pTop + " obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username FROM HR.OB WHERE apphname='" + pUsername + "' AND obstat='F' AND (obtype='0' OR (obtype='1' AND apprstat='A')) ORDER BY datefile DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTableRATop(int pTop, string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP " + pTop + " obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username FROM HR.OB WHERE apprname='" + pUsername + "' AND obstat='F' AND apprstat='F' ORDER BY datefile DESC";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  //public static DataTable GetDataTableHATop(int pTop, string pUsername)
  //{
  // DataTable tblReturn = new DataTable();
  // using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
  // {
  //  SqlCommand cmd = cn.CreateCommand();
  //  cmd.CommandText = "SELECT TOP " + pTop + " obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username FROM HR.OB WHERE apphname='" + pUsername + "' AND obstat='F' AND obtype='0' ORDER BY datefile DESC";
  //  cn.Open();
  //  SqlDataAdapter da = new SqlDataAdapter(cmd);
  //  da.Fill(tblReturn);
  // }
  // return tblReturn;
  //}

  public static string GetRequestStatusIcon(string pOBStatus)
  {
   string strReturn = "";
   if (pOBStatus == "V")
    strReturn = "Disapproved.png";
   else if (pOBStatus == "D")
    strReturn = "Disapproved.png";
   else if (pOBStatus == "F")
    strReturn = "Approval.png";
   else if (pOBStatus == "A")
    strReturn = "Approved.png";
   else if (pOBStatus == "C")
    strReturn = "Disapproved.png";
   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pOBStatus, string pOBType, string pARName, string pARStatus, string pAHName, string pAHStatus)
  {
   string strReturn = "";
   if (pOBStatus == "D")
   {
    if (pOBType == "1" && pARStatus == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pARName, 3);
    else
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pAHName, 3);
   }
   else if (pOBStatus == "C")
    strReturn = "The application has been cancelled";
   else if (pOBStatus == "F")
   {
    if (pOBType == "1" && pARStatus == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pARName, 3);
    else
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pAHName, 3);
   }
   else if (pOBStatus == "A")
    strReturn = "Approved";
   return strReturn;
  }

  public static string GetOBTypeDesc(string pOBType)
  {
   return (pOBType == "0" ? "Rendered within department" : "Rendered to other department");
  }

  public static OBStatus ToOBStatusDesc(string pOBStatusCode)
  {
   switch (pOBStatusCode)
   {
    case "C":
     return OBStatus.Cancelled;
    case "F":
     return OBStatus.ForApproval;
    case "A":
     return OBStatus.Approved;
    case "D":
     return OBStatus.Disapproved;
    default:
     return OBStatus.ForApproval;
   }
  }

  public static string ToOBStatus(string pOBStatusCode)
  {
   switch (pOBStatusCode)
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

  public static int GetTotalForAttention(string pUsername)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(obcode) FROM HR.OB WHERE obstat='F' AND ((apprname='" + pUsername + "' AND apprstat='F') OR (apphname='" + pUsername + "' AND apphstat='F' AND (obtype='0' OR (obtype='1' AND apprstat='A'))))";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { intReturn = 0; }
   }
   return intReturn;
  }

  ///////// Web Methods /////////

  public static DataTable GetPageRecords(OBUsers pOBUsers, int pPage, string pUsername, string pStatus)
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
     if (pOBUsers == OBUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.OB WHERE username='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOBUsers == OBUsers.ApproverRequestor)
      cmd.CommandText = "SELECT * FROM (SELECT obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.OB WHERE apprname='" + pUsername + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOBUsers == OBUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.OB WHERE apphname='" + pUsername + "' AND (obtype='0' OR (obtype='1' AND apprstat='A'))) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    else
    {
     if (pOBUsers == OBUsers.Requestor)
      cmd.CommandText = "SELECT * FROM (SELECT obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.OB WHERE username='" + pUsername + "' AND obstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOBUsers == OBUsers.ApproverRequestor)
      cmd.CommandText = "SELECT * FROM (SELECT obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.OB WHERE apprname='" + pUsername + "' AND apprstat='" + pStatus + "') AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
     else if (pOBUsers == OBUsers.ApproverHead)
      cmd.CommandText = "SELECT * FROM (SELECT obcode,datefile,reason,obtype,apprname,apprstat,apphname,apphstat,obstat,username,ROW_NUMBER() OVER(ORDER BY datefile DESC) AS RowNum FROM HR.OB WHERE apphname='" + pUsername + "' AND apphstat='" + pStatus + "' AND (obtype='0' OR (obtype='1' AND apprstat='A'))) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
    }
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetPaging(OBUsers pOBUsers, int pPage, string pUsername, string pStatus, string pPageName)
  {
   string strReturn = "";

   int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
   int intTRows = 0;
   int intTRowsTemp = 0;
   int intPage = 1;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pOBUsers == OBUsers.Requestor)
     cmd.CommandText = "SELECT COUNT(obcode) FROM HR.OB WHERE username='" + pUsername + "'" + (pStatus == "all" ? "" : " AND obstat='" + pStatus + "'");
    else if (pOBUsers == OBUsers.ApproverRequestor)
     cmd.CommandText = "SELECT COUNT(obcode) FROM HR.OB WHERE apprname='" + pUsername + "'" + (pStatus == "all" ? "" : " AND apprstat='" + pStatus + "'");
    else if (pOBUsers == OBUsers.ApproverHead)
     cmd.CommandText = "SELECT COUNT(obcode) FROM HR.OB WHERE apphname='" + pUsername + "'" + (pStatus == "all" ? "" : " AND apphstat='" + pStatus + "' AND (obtype='0' OR (obtype='1' AND apprstat='A'))");
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
    cmd.CommandText = "SELECT COUNT(obcode) FROM HR.OB";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

  public static DataTable GetNotificationForApproval(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT * FROM HR.OB WHERE obstat='F' AND ((apprname='" + pUsername + "' AND apprstat='F') OR (apphname='" + pUsername + "' AND apphstat='F' AND (obtype='0' OR (obtype='1' AND apphstat='A'))))";
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

     //add by charlie bachiller
     //Finance CATA
  public static DataTable GetDSL(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      tblReturn.Columns.Add("pValue");
      tblReturn.Columns.Add("pText");
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT obcode AS pValue, (obcode + '-' + reason) AS pText FROM HR.OB AS hrob WHERE username=@username AND obstat='A' AND obcode NOT IN (SELECT obcode FROM Finance.CATARequest WHERE statcode IN ('0','1','2')) AND convert(varchar(10), (SELECT TOP(1) focsdate FROM HR.OBDetails WHERE obcode=hrob.obcode),121) >= convert(varchar(10), GETDATE(),121) ORDER BY datefile DESC";
          cmd.Parameters.Add(new SqlParameter("@username", pUsername));
          cn.Open();
          SqlDataReader dr = cmd.ExecuteReader();
          while(dr.Read())
          {
              if (clsOBDetails.IsValid(dr["pValue"].ToString()))
              {
                  DataRow drNew = tblReturn.NewRow();
                  drNew["pValue"] = dr["pValue"].ToString();
                  drNew["pText"] = dr["pText"].ToString();
                  tblReturn.Rows.Add(drNew);
              }
          
          }
      }
      return tblReturn;
  }
     //added by Rollie
  public static DataTable GetDSLApproveOBPCAS(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      tblReturn.Columns.Add("pValue");
      tblReturn.Columns.Add("pText");
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          //cmd.CommandText = "SELECT obcode AS pValue, (obcode + '-' + reason) AS pText FROM HR.OB AS hrob WHERE username=@username AND obstat='A' AND convert(varchar(10), (SELECT TOP(1) focsdate FROM HR.OBDetails WHERE obcode=hrob.obcode),121) >= convert(varchar(10), GETDATE(),121) ORDER BY datefile DESC";
          cmd.CommandText = "SELECT obcode AS pValue, (obcode + '-' + reason) AS pText FROM HR.OB AS hrob WHERE username=@username AND obstat='A' AND convert(varchar(10), (SELECT TOP(1) focsdate FROM HR.OBDetails WHERE obcode=hrob.obcode),121) >= convert(varchar(10), GETDATE(),121) AND obcode NOT IN (SELECT obcode FROM Finance.PCASRequest WHERE pcasstat != '2') ORDER BY datefile DESC";
          cmd.Parameters.Add(new SqlParameter("@username", pUsername));
          cn.Open();
          SqlDataReader dr = cmd.ExecuteReader();
          while (dr.Read())
          {
              if (clsOBDetails.IsValid(dr["pValue"].ToString()))
              {
                  DataRow drNew = tblReturn.NewRow();
                  drNew["pValue"] = dr["pValue"].ToString();
                  drNew["pText"] = dr["pText"].ToString();
                  tblReturn.Rows.Add(drNew);
              }

          }
      }
      return tblReturn;
  }

  public static string GetOBReason(string pOBCode)
  {
      string strReturn = "";
      using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT reason FROM HR.OB WHERE obcode =@obcode";
          cmd.Parameters.Add(new SqlParameter("@obcode", pOBCode));
          cn.Open();
          SqlDataReader dr = cmd.ExecuteReader();
          if (dr.Read())
          {
              strReturn = dr["reason"].ToString();
          }
          cn.Close();
      }
      return strReturn;
  }

  //ADDED by Calvin Cavite FEB 13, 2018
  //use for auto generate Official Business code
  private void getOBCode()
  {
            string obcode  = "";
            int strOBcode = 0;
            using(SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select top 1 pvalue from Speedo.Keys WHERE pkey='obcode' order by pvalue desc";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) {

                    obcode = dr["pvalue"].ToString();
                }
                dr.Close();
                if (obcode == null || obcode == "") {
                    strOBcode = clsValidator.CheckInteger(obcode) + 1;
                    obcode = ("OB" + strOBcode.ToString());
                    OBCode = obcode;
                }
                else {
                    char[] removchar = { 'O', 'B' };
                    string nwOBcode = obcode.TrimStart(removchar);
                    obcode = nwOBcode;
                    strOBcode = clsValidator.CheckInteger(obcode) + 1;
                    obcode = ("OB" + strOBcode.ToString());
                    OBCode = obcode;
                }
            }
  }
 }
}