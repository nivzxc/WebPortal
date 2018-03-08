using System;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsTransmittal
 {
  public clsTransmittal() { }

  public enum TransmittalUserType
  {
   Requestor = 0,
   GroupHead = 1,
   SpecialDispatchApprover = 2,
   SpecialDispatchApprover2 = 3
  }

  public enum DispatchTypes
  {
   Regular = 0,
   SpecialHQ = 1,
   SpecialSchool = 2
  }

  public enum TransmittalMailType
  {
   SentToRequestor = 1,
   SentToApproverGH = 2,
   SentToApprover = 3,
   ApproveToRequestor = 5,
   ApproveToApproverGH = 6,
   ApproveToApprover = 7,
   DisapproveToRequestor = 9,
   DisapproveToApproverGH = 10,
   DisapproveToApprover = 11,
  }

  private string _strTransmittalCode;
  private string _strUserName;
  private DateTime _dteDateRequested;
  private DateTime _dteDateNeeded;
  private string _strItemDescription;
  private string _strUnit;
  private string _strRemarks;
  private string _strDispatchType;
  private string _strChargeTo;
  private string _strGroupHead;
  private string _strGroupHeadRemarks;
  private string _strGroupHeadStatus;
  private string _strApprover;
  private string _strApproverRemarks;
  private string _strApproverStatus;
  private string _strStatus;

  private static string strFlagSubmitted = "ForProcessing.png";
  private static string strFlagApproved = "Approved.png";
  private static string strFlagDisapproved = "Disapproved.png";
  private static string strFlagForApproval = "Approval.png";

  public string TransmittalCode { get { return _strTransmittalCode; } set { _strTransmittalCode = value; } }
  public string UserName { get { return _strUserName; } set { _strUserName = value; } }
  public DateTime DateRequested { get { return _dteDateRequested; } set { _dteDateRequested = value; } }
  public DateTime DateNeeded { get { return _dteDateNeeded; } set { _dteDateNeeded = value; } }
  public string ItemDescription { get { return _strItemDescription; } set { _strItemDescription = value; } }
  public string Unit { get { return _strUnit; } set { _strUnit = value; } }
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }
  public string DispatchType { get { return _strDispatchType; } set { _strDispatchType = value; } }
  public string ChargeTo { get { return _strChargeTo; } set { _strChargeTo = value; } }
  public string GroupHead { get { return _strGroupHead; } set { _strGroupHead = value; } }
  public string GroupHeadRemarks { get { return _strGroupHeadRemarks; } set { _strGroupHeadRemarks = value; } }
  public string GroupHeadStatus { get { return _strGroupHeadStatus; } set { _strGroupHeadStatus = value; } }
  public string Approver { get { return _strApprover; } set { _strApprover = value; } }
  public string ApproverRemarks { get { return _strApproverRemarks; } set { _strApproverRemarks = value; } }
  public string ApproverStatus { get { return _strApproverStatus; } set { _strApproverStatus = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public string StatusDescription
  {
   get
   {
    switch (_strStatus)
    {
     case "F":
      return "For Approval";
     case "D":
      return "Disapproved";
     case "A":
      return "For Processing";
     case "C":
      return "Processed";
     case "V":
      return "Voided";
     default:
      return "Unknown";
    }
   }
  }

  public string DispatchTypeDescription
  {
   get
   {
    switch (_strDispatchType)
    {
     case "R":
      return "Regular Dispatch";
     case "S":
      return "Special Dispatch (Charge to School)";
     case "H":
      return "Special Dispatch (Charge to HQ)";
     default:
      return "Unknown";
    }
   }
  }

  //Updated by Ian - March 16, 2011
  // Changed status='F' = status='A' AND appstat='F' to appstat='A' : eliminate approval of PFK on special dispatch
  public void ApproveGH(string pGhRemarks)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    cmd.CommandText = "UPDATE CIS.Transmittal SET grphdate='" + DateTime.Now + "',grphrem=@grphrem,status='A',grphstat='A',appstat='A' WHERE trancode='" + _strTransmittalCode + "'";
    cmd.Parameters.Add("@grphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@grphrem"].Value = pGhRemarks;
    cmd.ExecuteNonQuery();
   }
  }

  public void ApproveSA(string pSARemarks, string pSAUserName)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Transmittal SET status='A',appstat='A',apprem=@apprem,appdate='" + DateTime.Now + "',appname='" + pSAUserName + "' WHERE trancode='" + _strTransmittalCode + "'";
    cmd.Parameters.Add("@apprem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apprem"].Value = pSARemarks;
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void DisapproveGH(string pGHRemarks)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    cmd.CommandText = "UPDATE CIS.Transmittal SET grphdate='" + DateTime.Now + "',grphrem=@grphrem,status='D',grphstat='D' WHERE trancode='" + _strTransmittalCode + "'";
    cmd.Parameters.Add("@grphrem", SqlDbType.VarChar, 255);
    cmd.Parameters["@grphrem"].Value = pGHRemarks;
    cmd.ExecuteNonQuery();
   }
  }

  public void DisapproverSA(string pSARemarks, string pSAUserName)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Transmittal SET status='D',appstat='D',apprem=@apprem,appdate='" + DateTime.Now + "',appname='" + pSAUserName + "' WHERE trancode='" + _strTransmittalCode + "'";
    cmd.Parameters.Add("@apprem", SqlDbType.VarChar, 255);
    cmd.Parameters["@apprem"].Value = pSARemarks;
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public DataTable DSGItems()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT CIS.TransmittalDetails.schlcode,schlname,trannmbr,qty,dispby,datedisp,recby,recdate,status FROM CIS.TransmittalDetails INNER JOIN CM.Schools ON CIS.TransmittalDetails.schlcode = CM.Schools.schlcode WHERE trancode='" + _strTransmittalCode + "' ORDER BY schlname";
    SqlDataAdapter da = new SqlDataAdapter();
    da.SelectCommand = cmd;
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM CIS.Transmittal WHERE trancode='" + _strTransmittalCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUserName = dr["username"].ToString();
     _dteDateRequested = Convert.ToDateTime(dr["datereq"].ToString());
     _dteDateNeeded = Convert.ToDateTime(dr["dateneed"].ToString());
     _strItemDescription = dr["itemdesc"].ToString();
     _strUnit = dr["unit"].ToString();
     _strRemarks = dr["remarks"].ToString();
     _strDispatchType = dr["disptype"].ToString();
     _strChargeTo = dr["chargeto"].ToString();
     _strGroupHead = dr["grphname"].ToString();
     _strGroupHeadStatus = dr["grphstat"].ToString();
     _strGroupHeadRemarks = dr["grphrem"].ToString();
     _strApprover = dr["appname"].ToString();
     _strApproverRemarks = dr["apprem"].ToString();
     _strApproverStatus = dr["appstat"].ToString();
     _strStatus = dr["status"].ToString();
    }
    dr.Close();
   }
  }

  public bool Void()
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    bool blnHasRecord = false;
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT schlcode FROM CIS.TransmittalDetails WHERE trancode='" + _strTransmittalCode + "' AND status='1'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
    if (!blnHasRecord)
    {
     cmd.CommandText = "UPDATE CIS.Transmittal SET status='V' WHERE trancode='" + _strTransmittalCode + "'";
     if (cmd.ExecuteNonQuery() > 0)
      blnReturn = true;
    }
   }
   return blnReturn;
  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static int CountRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(*) FROM CIS.Transmittal";
    cn.Open();
    try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

  public static string GetApprover()
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM CIS.TransmittalApprover WHERE userlvl='approver'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["username"].ToString();
    dr.Close();
   }
   return strReturn;
  }

  public static string GetApprover2()
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM CIS.TransmittalApprover WHERE userlvl='approver2'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["username"].ToString();
    dr.Close();
   }
   return strReturn;
  }

  public static string GetItemStatus(string pTransmittalCode, string pSchoolCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT status FROM CIS.TransmittalDetails WHERE trancode='" + pTransmittalCode + "' AND schlcode='" + pSchoolCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["status"].ToString();
    dr.Close();
   }
   return strReturn;
  }

  public static string GetRequestStatusIcon(string pDispatchType, string pTransmittalStatus, string pGroupHeadName, string pGroupHeadStatus, string pApproverName, string pApproverStat)
  {
   string strReturn = "";

   if (pTransmittalStatus == "V")
    strReturn = strFlagDisapproved;
   else if (pTransmittalStatus == "F")
    strReturn = strFlagForApproval;
   else if (pTransmittalStatus == "D")
    strReturn = strFlagDisapproved;
   else if (pTransmittalStatus == "A")
    strReturn = strFlagSubmitted;
   else if (pTransmittalStatus == "P")
    strReturn = strFlagSubmitted;
   else if (pTransmittalStatus == "C")
    strReturn = strFlagApproved;

   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pDispatchType, string pTransmittalStatus, string pGroupHeadName, string pGroupHeadStatus, string pApproverName, string pApproverStat)
  {
   string strReturn = "";

   if (pTransmittalStatus == "V")
    strReturn = "Voided by the requestor";
   else if (pTransmittalStatus == "F")
    strReturn = "For Approval of " + clsSpeedo.AssignUsernameLink((pGroupHeadStatus == "F" ? pGroupHeadName : pApproverName));
   else if (pTransmittalStatus == "D")
    strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink((pGroupHeadStatus == "D" ? pGroupHeadName : pApproverName));
   else if (pTransmittalStatus == "A")
    strReturn = "Approved. Processing ongoing";
   else if (pTransmittalStatus == "P")
    strReturn = "Some item(s) has dispatched";
   else if (pTransmittalStatus == "C")
    strReturn = "All items has been dispatched";

   return strReturn;
  }

  public static TransmittalUserType GetUserLevel(string pUsername)
  {
   TransmittalUserType tutReturn = TransmittalUserType.Requestor;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT userlvl FROM CIS.TransmittalApprover WHERE username='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     if (dr["userlvl"].ToString() == "grouphead")
      tutReturn = TransmittalUserType.GroupHead;
     else if (dr["userlvl"].ToString() == "approver")
      tutReturn = TransmittalUserType.SpecialDispatchApprover;
     else if (dr["userlvl"].ToString() == "approver2")
      tutReturn = TransmittalUserType.SpecialDispatchApprover2;
    }
    dr.Close();
   }
   return tutReturn;
  }

  public static bool IsApprover(TransmittalUserType pUserType, string pUsername)
  {
   bool blnReturn = false;
   if (pUserType == TransmittalUserType.GroupHead || pUserType == TransmittalUserType.SpecialDispatchApprover || pUserType == TransmittalUserType.SpecialDispatchApprover2)
   {
    using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
    {
     SqlCommand cmd = cn.CreateCommand();
     if (pUserType == TransmittalUserType.GroupHead)
      cmd.CommandText = "SELECT userlvl FROM CIS.TransmittalApprover WHERE userlvl='grouphead' AND username='" + pUsername + "'";
     else if (pUserType == TransmittalUserType.SpecialDispatchApprover)
      cmd.CommandText = "SELECT userlvl FROM CIS.TransmittalApprover WHERE userlvl='approver' AND username='" + pUsername + "'";
     else if (pUserType == TransmittalUserType.SpecialDispatchApprover2)
      cmd.CommandText = "SELECT userlvl FROM CIS.TransmittalApprover WHERE userlvl='approver2' AND username='" + pUsername + "'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     blnReturn = dr.Read();
     dr.Close();
    }
   }
   return blnReturn;
  }

  public static void SendNotification(TransmittalMailType pMailType, string pRequestorName, string pApproverName, string pMailTo, string pTransmittalCode)
  {
   string strSpeedoUrl = clsSystemConfigurations.PortalRootURL;
   string strSubject = "";
   string strBody = "";

   switch (pMailType)
   {
    case TransmittalMailType.SentToRequestor:
     {
      strSubject = "Delivered: Special Dispatch Transmittal";
      strBody = "Hi " + pRequestorName + ",<br><br>" +
                "Your Special Dispatch Transmittal has been delivered to " + pApproverName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetails.aspx?trancode=" + pTransmittalCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetails.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.SentToApproverGH:
     {
      strSubject = "For Your Approval: Special Dispatch Transmittal";
      strBody = "Hi " + pApproverName + ",<br><br>" +
                "There is a Special Dispatch Transmittal submitted by " + pRequestorName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsGH.aspx?trancode=" + pTransmittalCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsGH.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.SentToApprover:
     {
      strSubject = "For Your Approval: Special Dispatch Transmittal";
      strBody = "Hi " + pApproverName + ",<br><br>" +
                "There is a Special Dispatch Transmittal submitted by " + pRequestorName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsSA.aspx?trancode=" + pTransmittalCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsSA.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.ApproveToRequestor:
     {
      strSubject = "Approved Special Dispatch Transmittal";
      strBody = "Hi " + pRequestorName + ",<br><br>" +
                "Your Special Dispatch Transmittal has been approved by " + pApproverName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetails.aspx?trancode=" + pTransmittalCode + "'>Click here to review the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetails.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.ApproveToApproverGH:
     {
      strSubject = "Approved Special Dispatch Transmittal";
      strBody = "You approved a Special Dispatch Transmittal.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsGH.aspx?trancode=" + pTransmittalCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsGH.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.ApproveToApprover:
     {
      strSubject = "Approved Special Dispatch Transmittal";
      strBody = "You approved a Special Dispatch Transmittal.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsSA.aspx?trancode=" + pTransmittalCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsSA.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.DisapproveToRequestor:
     {
      strSubject = "Disapproved Special Dispatch Transmittal";
      strBody = "Hi " + pRequestorName + ",<br><br>" +
                "Your Special Dispatch Transmittal has been disapproved by " + pApproverName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetails.aspx?trancode=" + pTransmittalCode + "'>Click here to review the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetails.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.DisapproveToApproverGH:
     {
      strSubject = "Disapproved Special Dispatch Transmittal";
      strBody = "You disapproved a Special Dispatch Transmittal.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsGH.aspx?trancode=" + pTransmittalCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsGH.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case TransmittalMailType.DisapproveToApprover:
     {
      strSubject = "Disapproved Special Dispatch";
      strBody = "You disapproved a Special Dispatch Transmittal.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsSA.aspx?trancode=" + pTransmittalCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Transmittal/TranDetailsSA.aspx?trancode=" + pTransmittalCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

   }
   clsSpeedo.SendMail(pMailTo, strSubject, strBody);
  }

  public static string ToDispatchTypeDesc(string pDispatchType)
  {
   switch (pDispatchType)
   {
    case "R":
     return "Regular Dispatch";
    case "S":
     return "Special Dispatch (Charge to School)";
    case "H":
     return "Special Dispatch (Charge to HQ)";
    default:
     return "Unknown";
   }
  }

  public static string ToRequestStatusDesc(string pStatus)
  {
   switch (pStatus)
   {
    case "F":
     return "For Approval";
    case "A":
     return "Approved";
    case "D":
     return "Disapproved";
    case "P":
     return "Partially Processed";
    case "C":
     return "Completed";
    default:
     return "Unknown";
   }
  }

  //added by charlie bachiller for default.aspx 1128-2011
  public static DataTable GetNotificationForApproval(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringTransmittal))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT * FROM CIS.Transmittal WHERE (grphname='" + pUsername + "' AND grphstat='F' AND status='F') OR (appname='" + pUsername + "' AND appstat='F' AND grphname='A' )";
          cn.Open();
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

  public static int CountForApproval(string pUsername)
  {
      int intReturn = 0;
      DataTable tblCount = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          using (SqlCommand cmd = cn.CreateCommand())
          {
              //cmd.CommandText = "SELECT COUNT(*) FROM CIS.Transmittal WHERE (grphname='" + pUsername + "' AND grphstat='F' AND status='F') OR (appname='" + pUsername + "' AND appstat='F' AND grphname='A' )";
              cmd.CommandText = "SELECT * FROM CIS.Transmittal WHERE grphname=@username AND (disptype='S' OR disptype='H') AND status='F' AND grphstat='F'";
              cmd.Parameters.Add(new SqlParameter("@username", pUsername));
              cn.Open();
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              da.Fill(tblCount);
              intReturn = tblCount.Rows.Count;
          }
      }
      return intReturn;
  }

 }
}