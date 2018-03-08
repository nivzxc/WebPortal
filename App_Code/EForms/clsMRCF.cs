using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HRMS;

namespace STIeForms
{
 public class clsMRCF
 {
  public clsMRCF() { }
  public clsMRCF(string pMRCFCode) { _strMrcfCode = pMRCFCode; }

  public enum MRCFUserType
  {
   Requestor = 1,
   GroupHead = 2,
   DivisionHead = 3,
   ProcurementManager = 4
  }

  public enum MRCFMailType
  {
   SentToRequestor = 1,
   SentToApproverGH = 2,
   SentToApproverDH = 3,
   SentToApproverPM = 4,
   ApproveToRequestor = 5,
   ApproveToApproverGH = 6,
   ApproveToApproverDH = 7,
   ApproveToApproverPM = 8,
   DisapproveToRequestor = 9,
   DisapproveToApproverGH = 10,
   DisapproveToApproverDH = 11,
   DisapproveToApproverPM = 12,
   ModificationToRequestor = 13,
   ModificationToApproverGH = 14,
   ModificationToApproverDH = 15,
   ModificationToApproverPM = 16
  }

  private string _strMrcfCode;
  private string _strUsername;
  private string _strRequestType;
  private DateTime _dteDateRequested;
  private string _strIntended;
  private string _strChargeTo;
  private string _strGroupHead;
  private string _strGroupHeadStatus;
  private string _strGroupHeadRemarks;
  private string _strDivisionHead;
  private string _strDivisionHeadStatus;
  private string _strDivisionHeadRemarks;
  private string _strProcurementManager;
  private string _strProcurementManagerStatus;
  private string _strProcurementManagerRemarks;
  private string _strStatus;  

  public string MRCFCode { get { return _strMrcfCode; } set { _strMrcfCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername=value; } }
  public string RequestType { get { return _strRequestType; } set { _strRequestType=value; } }
  public DateTime DateRequested { get { return _dteDateRequested; } set { _dteDateRequested=value; } }
  public string Intended { get { return _strIntended; } set { _strIntended=value; } }
  public string ChargeTo { get { return _strChargeTo; } set { _strChargeTo=value; } }
  public string GroupHead { get { return _strGroupHead; } set { _strGroupHead=value; } }
  public string GroupHeadStatus { get { return _strGroupHeadStatus; } set { _strGroupHeadStatus=value; } }
  public string GroupHeadRemarks { get { return _strGroupHeadRemarks; } set { _strGroupHeadRemarks=value; } }
  public string DivisionHead { get { return _strDivisionHead; } set { _strDivisionHead = value; } }
  public string DivisionHeadStatus { get { return _strDivisionHeadStatus; } set { _strDivisionHeadStatus = value; } }
  public string DivisionHeadRemarks { get { return _strDivisionHeadRemarks; } set { _strDivisionHeadRemarks = value; } }
  public string ProcurementManager { get { return _strProcurementManager; } set { _strProcurementManager = value; } }
  public string ProcurementManagerStatus { get { return _strProcurementManagerStatus; } set { _strProcurementManagerStatus = value; } }
  public string ProcurementManagerRemarks { get { return _strProcurementManagerRemarks; } set { _strProcurementManagerRemarks = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public string RequestTypeDesc
  {
   get
   {
    switch (_strRequestType)
    {
     case "B":
      return "Canvass, Advise Before PO";
     case "C":
      return "Canvass Only";
     case "P":
      return "Purchase Immediately";
     default:
      return "Unknown";
    }
   }
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

  public void AddItem(string pItemDesc, string pItemSpec, string pAsstCode, int pQty, string pUnit, DateTime pDateNeed, string pStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO CIS.MrcfDetails(mrcfcode,itemdesc,itemspec,asstcode,qty,unit,dateneed,status) VALUES('" + _strMrcfCode + "',@itemdesc,@itemspec,'" + pAsstCode + "','" + pQty + "','" + pUnit + "','" + pDateNeed + "','" + pStatus + "')";
    cmd.Parameters.Add("@itemdesc", SqlDbType.VarChar, 100);
    cmd.Parameters.Add("@itemspec", SqlDbType.VarChar, 5000);
    cmd.Parameters["@itemdesc"].Value = pItemDesc;
    cmd.Parameters["@itemspec"].Value = pItemSpec;
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public int CheckDateNeeded()
  {
   int intReturn = 0;
   DataTable tblMRCFItems = new DataTable();
   int intLeadDays = 0;
   DateTime dteNewDateNeeded = DateTime.Now;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leaddays FROM CIS.MrcfRequestType WHERE reqtype=(SELECT reqtype FROM CIS.Mrcf WHERE mrcfcode='" + _strMrcfCode + "')";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     intLeadDays = Convert.ToInt32(dr["leaddays"].ToString());
    dr.Close();

    if (intLeadDays != 0)
    {
     cmd.CommandText = "SELECT mitmcode,dateneed FROM CIS.MrcfDetails WHERE mrcfcode='" + _strMrcfCode + "'";
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblMRCFItems);

     foreach (DataRow drow in tblMRCFItems.Rows)
     {
      dteNewDateNeeded = clsDateTime.AddDaysWorking(intLeadDays, DateTime.Now);
      if (dteNewDateNeeded > Convert.ToDateTime(drow["dateneed"].ToString()))
      {
       cmd.CommandText = "UPDATE CIS.MrcfDetails SET dateneed='" + dteNewDateNeeded + "' WHERE mitmcode='" + drow["mitmcode"] + "'";
       cmd.ExecuteNonQuery();
       intReturn++;
      }
     }
    }
   }
   return intReturn;
  }

  public int CountItem()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(*) FROM CIS.MrcfDetails WHERE mrcfcode='" + _strMrcfCode + "'";
    cn.Open();
    try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }



  //Ross 03-13-14------------------------------------------------------------------------------------------------------------------
  public static DataTable DDLEmployee(string pModuleCode, string pStatus)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT Username as pValue, (SELECT lastname + ', ' + firname FROM hr.Employees WHERE username = Users.UsersModules.username) as pText FROM Users.UsersModules WHERE modlcode = '"+ pModuleCode +"' AND pstatus = '"+ pStatus +"' ORDER BY pText";
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          cn.Open();
          da.Fill(tblReturn);
          cn.Close();
      }
      return tblReturn;
  }

  public static DataTable DDLEmployeeSearch(string pModuleCode, string pStatus)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT '999' as pvalue ,'All' as ptext union all SELECT Username as pValue, (SELECT lastname + ', ' + firname FROM hr.Employees WHERE username = Users.UsersModules.username) as pText FROM Users.UsersModules WHERE modlcode = '" + pModuleCode + "' AND pstatus = '" + pStatus + "' ORDER BY pText";
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          cn.Open();
          da.Fill(tblReturn);
          cn.Close();
      }
      return tblReturn;
  }

  public static DataTable DDLEmployeeRemoveCurrent(string pModuleCode, string pStatus, string pCurrentHandler)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT Username as pValue, (SELECT lastname + ', ' + firname FROM hr.Employees WHERE username = Users.UsersModules.username) as pText FROM Users.UsersModules WHERE modlcode = '" + pModuleCode + "' AND pstatus = '" + pStatus + "' AND Username <> '"+ pCurrentHandler +"' ORDER BY pText";
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          cn.Open();
          da.Fill(tblReturn);
          cn.Close();
      }
      return tblReturn;
  }

  public static DataTable DDLAssignStatusSearch()
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT '999' AS pvalue, 'All' AS ptext union all SELECT  statcode as pvalue , statdesc as ptext FROM CIS.MrcfAssignStatus";
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          cn.Open();
          da.Fill(tblReturn);
          cn.Close();
      }
      return tblReturn;
  }

  public static DataTable DDLAssignStatus()
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT statcode as pvalue , statdesc as ptext FROM CIS.MrcfAssignStatus";
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          cn.Open();
          da.Fill(tblReturn);
          cn.Close();
      }
      return tblReturn;
  }

  public static DataTable DDLExportToExcel(string pStatCode, string pUser)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          if (pUser == "999")
          {
              if (pStatCode == "999")
              { cmd.CommandText = "SELECT (SELECT datereq FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Datereq,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,(SELECT username FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Requestor,(SELECT btchcode FROM cis.mrcfbatch WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as batchcode, (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE isactive = '1' ORDER BY hdlrcode ASC"; }
              else
              { cmd.CommandText = "SELECT (SELECT datereq FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Datereq,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,(SELECT username FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Requestor,(SELECT btchcode FROM cis.mrcfbatch WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as batchcode,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE isactive = '1' and statcode = '" + pStatCode + "' ORDER BY hdlrcode ASC"; }
          }
          else
          {
              if (pStatCode == "999")
              { cmd.CommandText = "SELECT (SELECT datereq FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Datereq,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,(SELECT username FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Requestor,(SELECT btchcode FROM cis.mrcfbatch WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as batchcode,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE assignto = '" + pUser + "' AND isactive = '1' ORDER BY hdlrcode ASC"; }
              else
              { cmd.CommandText = "SELECT (SELECT datereq FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Datereq,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,(SELECT username FROM cis.mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as Requestor,(SELECT btchcode FROM cis.mrcfbatch WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) ) as batchcode,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE assignto = '" + pUser + "' AND isactive = '1' and statcode = '" + pStatCode + "' ORDER BY hdlrcode ASC"; }
          }
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          cn.Open();
          da.Fill(tblReturn);
          cn.Close();
      }
      return tblReturn;
  }
  //----------------------------------------------------------------------------------------------------------------------------------

 public void DisapproveDH(string pDHRemarks, string pGroupHeadStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Mrcf SET status='D',headstat='D',sprvstat=@sprvstat,procstat='X',headdate='" + DateTime.Now + "',headrem=@headrem WHERE mrcfcode='" + _strMrcfCode + "'";
    cmd.Parameters.Add("@headrem", SqlDbType.VarChar, 200);
    cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
    cmd.Parameters["@headrem"].Value = pDHRemarks;
    cmd.Parameters["@sprvstat"].Value = (pGroupHeadStatus == "F" ? "N" : pGroupHeadStatus);
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void DisapproveGH(string pGHRemarks)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Mrcf SET status='D',sprvstat='D',headstat='X',procstat='X',sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem WHERE mrcfcode='" + _strMrcfCode + "'";
    cmd.Parameters.Add("@sprvrem", SqlDbType.VarChar, 200);
    cmd.Parameters["@sprvrem"].Value = pGHRemarks;
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void DisapprovePM(string pPMRemarks, string pGHStatus, string pDHStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Mrcf SET status='D',procstat='D',sprvstat=@sprvstat,headstat=@headstat,procdate='" + DateTime.Now + "',procrem=@procrem WHERE mrcfcode='" + _strMrcfCode + "'";
    cmd.Parameters.Add("@procrem", SqlDbType.VarChar, 200);
    cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
    cmd.Parameters.Add("@headstat", SqlDbType.Char, 1);
    cmd.Parameters["@procrem"].Value = pPMRemarks;
    cmd.Parameters["@sprvstat"].Value = (pGHStatus == "F" ? "N" : pGHStatus);
    cmd.Parameters["@headstat"].Value = (pDHStatus == "F" ? "N" : pDHStatus);
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM CIS.Mrcf WHERE mrcfcode='" + _strMrcfCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strRequestType = dr["reqtype"].ToString();
     _dteDateRequested = clsValidator.CheckDate((dr["datereq"].ToString()));
     _strIntended = dr["intended"].ToString();
     _strChargeTo = dr["chargeto"].ToString();
     _strGroupHead = dr["sprvcode"].ToString();
     _strGroupHeadStatus = dr["sprvstat"].ToString();
     _strGroupHeadRemarks = dr["sprvrem"].ToString();
     _strDivisionHead = dr["headcode"].ToString();
     _strDivisionHeadStatus = dr["headstat"].ToString();
     _strDivisionHeadRemarks = dr["headrem"].ToString();
     _strProcurementManager = dr["proccode"].ToString();
     _strProcurementManagerStatus = dr["procstat"].ToString();
     _strProcurementManagerRemarks = dr["procrem"].ToString();
     _strStatus = dr["status"].ToString();
    }
    dr.Close();
   }
  }

  public void ModificationDH(string pDHRemarks, string pGroupHeadStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Mrcf SET status='M',headstat='M',sprvstat=@sprvstat,procstat='X',headdate='" + DateTime.Now + "',headrem=@headrem WHERE mrcfcode='" + _strMrcfCode + "'";
    cmd.Parameters.Add("@headrem", SqlDbType.VarChar, 200);
    cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
    cmd.Parameters["@headrem"].Value = pDHRemarks;
    cmd.Parameters["@sprvstat"].Value = (pGroupHeadStatus == "F" ? "N" : pGroupHeadStatus);
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void ModificationGH(string pGHRemarks)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Mrcf SET status='M',sprvstat='M',headstat='X',procstat='X',sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem WHERE mrcfcode='" + _strMrcfCode + "'";
    cmd.Parameters.Add("@sprvrem", SqlDbType.VarChar, 200);
    cmd.Parameters["@sprvrem"].Value = pGHRemarks;
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void ModificationPM(string pPMRemarks, string pGHStatus, string pDHStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = new SqlCommand("UPDATE CIS.Mrcf SET status='M',procstat='M',sprvstat=@sprvstat,headstat=@headstat,procdate='" + DateTime.Now + "', procrem=@procrem WHERE mrcfcode='" + _strMrcfCode + "'", cn);
    cmd.Parameters.Add("@procrem", SqlDbType.VarChar, 200);
    cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
    cmd.Parameters.Add("@headstat", SqlDbType.Char, 1);
    cmd.Parameters["@procrem"].Value = pPMRemarks;
    cmd.Parameters["@sprvstat"].Value = (pGHStatus == "F" ? "N" : pGHStatus);
    cmd.Parameters["@headstat"].Value = (pDHStatus == "F" ? "N" : pDHStatus);
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void Void()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Mrcf SET status='V' WHERE mrcfcode='" + _strMrcfCode + "'";
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////  

  public static int CountRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(*) FROM CIS.Mrcf";
    cn.Open();
    try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

  public static bool IsHeadApprovalRequired(string pRCCode, string pHeadCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT approve FROM CIS.MrcfApprover WHERE username='" + pHeadCode + "' AND rccode='" + pRCCode + "' AND userlvl='head'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { strReturn = ""; }
   }
   return (strReturn == "1" ? true : false);
  }

  public static MRCFUserType GetUserType(string pUserName)
  {
   MRCFUserType mrcfusertype = MRCFUserType.Requestor;
   if (pUserName.ToLower() == clsMRCFApprover.GetProcurementManager())
   {
    mrcfusertype = MRCFUserType.ProcurementManager;
   }
   else
   {
    using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT userlvl FROM CIS.MrcfApprover WHERE username='" + pUserName + "' AND pstatus='1' ORDER BY userlvl";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["userlvl"].ToString() == "head")
       mrcfusertype = MRCFUserType.DivisionHead;
      else if (dr["userlvl"].ToString() == "sprv")
       mrcfusertype = MRCFUserType.GroupHead;
     }
     else
     {
      mrcfusertype = MRCFUserType.Requestor;
     }
     dr.Close();
    }
   }
   return mrcfusertype;
  }

  public static bool IsApprover(MRCFUserType pUserType, string pUsername)
  {
   bool blnReturn = false;
   if (pUserType == MRCFUserType.GroupHead || pUserType == MRCFUserType.DivisionHead)
   {
    using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
    {
     SqlCommand cmd = cn.CreateCommand();
     if (pUserType == MRCFUserType.GroupHead)
      cmd.CommandText = "SELECT userlvl FROM CIS.MrcfApprover WHERE userlvl='sprv' AND username='" + pUsername + "'";
     else if (pUserType == MRCFUserType.DivisionHead)
      cmd.CommandText = "SELECT userlvl FROM CIS.MrcfApprover WHERE userlvl='head' AND username='" + pUsername + "'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     blnReturn = dr.Read();
     dr.Close();
    }
   }
   else if (pUserType == MRCFUserType.ProcurementManager)
   {
    blnReturn = (clsMRCFApprover.GetProcurementManager() == pUsername ? true : false);
   }
   return blnReturn;
  }

  public static void AuthenticateUser(MRCFUserType pMrcfUserType, string pUserName, string pMrcfCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pMrcfUserType == MRCFUserType.Requestor)
     cmd.CommandText = "SELECT username FROM CIS.Mrcf WHERE mrcfcode='" + pMrcfCode + "' AND username='" + pUserName + "'";
    else if (pMrcfUserType == MRCFUserType.GroupHead)
     cmd.CommandText = "SELECT sprvcode FROM CIS.Mrcf WHERE mrcfcode='" + pMrcfCode + "' AND sprvcode='" + pUserName + "'";
    else if (pMrcfUserType == MRCFUserType.DivisionHead)
     cmd.CommandText = "SELECT headcode FROM CIS.Mrcf WHERE mrcfcode='" + pMrcfCode + "' AND headcode='" + pUserName + "'";
    else if (pMrcfUserType == MRCFUserType.ProcurementManager)
     cmd.CommandText = "SELECT proccode FROM CIS.Mrcf WHERE mrcfcode='" + pMrcfCode + "' AND proccode='" + pUserName + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }

   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static void AuthenticateUser(string pUserName, string pMrcfCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM CIS.Mrcf WHERE mrcfcode='" + pMrcfCode + "' AND (username='" + pUserName + "' OR sprvcode='" + pUserName + "' OR headcode='" + pUserName + "' OR proccode='" + pUserName + "')";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }

   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static DateTime GetMinimumDateNeeded(string pRequestType)
  {
   int intRequestLeadTime = 0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leaddays FROM CIS.MrcfRequestType WHERE reqtype='" + pRequestType + "'";
    cn.Open();
    try { intRequestLeadTime = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return clsDateTime.AddDaysWorking(intRequestLeadTime);
  }

  public static DateTime GetMinimumDateNeeded(string pRequestType, DateTime pDateStart)
  {
   int intRequestLeadTime = 0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leaddays FROM CIS.MrcfRequestType WHERE reqtype='" + pRequestType + "'";
    cn.Open();
    try { intRequestLeadTime = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return clsDateTime.AddDaysWorking(intRequestLeadTime, pDateStart);
  }
  public static string GetRequestType(string pMrcfCode)
  {
      string strReturn = "";
      using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT reqtype FROM CIS.MRCF WHERE mrcfcode=@mrcfcode";
          cmd.Parameters.Add(new SqlParameter("@mrcfcode", pMrcfCode));
          cn.Open();
          strReturn = cmd.ExecuteScalar().ToString();

      }
      return strReturn;
  }
  public static string GetRequestTypeDesc(string pRequestType)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT typename FROM CIS.MrcfRequestType WHERE reqtype='" + pRequestType + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { strReturn = "Error occured."; }
   }
   return strReturn;
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
    case "M":
     return "For Modification";
    case "N":
     return "Not Applicable";
    default:
     return "Unknown";
   }
  }

  public static int CountItem(string pMrcfCode)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(itemdesc) FROM CIS.MrcfDetails WHERE mrcfcode='" + pMrcfCode + "'";
    cn.Open();
    try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }


  public static DataTable GetMrcfUnit(string pAssetCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT mrcfunit FROM CIS.MrcfUnit WHERE asstcode='" + pAssetCode + "' ORDER BY mrcfunit";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }





//Ross Update 06-03-2014
  public static DataTable GetMrcfItems(string pMrcfCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
  //  cmd.CommandText = "SELECT  * FROM CIS.MrcfDetails WHERE mrcfcode='" + pMrcfCode + "' ORDER BY itemdesc";
  //  cmd.CommandText = "SELECT mitmcode,mrcfcode,itemdesc,itemspec,asstcode,ltypcode,itemcode,qty,unit,dateneed,status,glaccount,destination,empname,birthdate,(SELECT TransactionTypeName FROM Oracle.MrcfTransactionType WHERE transactiontypecode = CIS.MRCFDetails.asstcode) + ' - [' + asstcode + ']' AS TypeCode, (SELECT Linetypename FROM Oracle.MrcfLineType WHERE Linetypecode = CIS.MRCFDetails.ltypcode) + ' - [' + ltypcode + ']' AS LineTypeCode   FROM CIS.MrcfDetails WHERE mrcfcode = '" + pMrcfCode + "' ORDER BY itemdesc";
 cmd.CommandText = "SELECT mitmcode,mrcfcode,itemdesc,itemspec,asstcode,ltypcode,itemcode,qty,unit,dateneed,status,glaccount,destination,empname,birthdate,(SELECT CASE WHEN  (SELECT TransactionTypeName FROM Oracle.MrcfTransactionType WHERE transactiontypecode = CIS.MRCFDetails.asstcode) is null THEN (SELECT SubCategoryName FROM oracle.MrcfItems WHERE TransactionTypeCode =CIS.MRCFDetails.asstcode) + ' - [' + asstcode + ']'  ELSE  (SELECT TransactionTypeName FROM Oracle.MrcfTransactionType WHERE transactiontypecode = CIS.MRCFDetails.asstcode) + ' - [' + asstcode + ']' END) AS TypeCode, (SELECT Linetypename FROM Oracle.MrcfLineType WHERE Linetypecode = CIS.MRCFDetails.ltypcode) + ' - [' + ltypcode + ']' AS LineTypeCode FROM CIS.MrcfDetails WHERE mrcfcode =  '" + pMrcfCode + "' ORDER BY itemdesc  ";

  SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
     DataRow[] FormatRow = tblReturn.Select("empname <> '' AND Birthdate <> ''");
     foreach (DataRow drw in FormatRow)
     {
         drw["empname"]="<tr><td>Name : " +drw["empname"].ToString()+ "</td></tr>";
         drw["birthdate"] = "<tr><td>Birthdate : " + drw["birthdate"].ToString() + "</td></tr>";
     }

   return tblReturn;
  }
     //end update




  public static void RemoveItem(string pMitmCode)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "DELETE FROM CIS.MrcfDetails WHERE mitmcode='" + pMitmCode + "'";
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public static void SendNotification(MRCFMailType pMailType, string pRequestorName, string pApproverName, string pMailTo, string pMRCFCode)
  {
   string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   string strSubject = "";
   string strBody = "";

   switch (pMailType)
   {
    case MRCFMailType.SentToRequestor:
     strSubject = "Delivered: MRCF";
     strBody = "Hi " + pRequestorName + ",<br><br>" +
               "Your MRCF has been delivered to " + pApproverName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.SentToApproverGH:
     strSubject = "For Your Approval: MRCF";
     strBody = "Hi " + pApproverName + ",<br><br>" +
               "There is an MRCF submitted by " + pRequestorName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.SentToApproverDH:
     strSubject = "For Your Approval: MRCF";
     strBody = "Hi " + pApproverName + ",<br><br>" +
               "There is an MRCF submitted by " + pRequestorName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.SentToApproverPM:
     strSubject = "For Your Approval: MRCF";
     strBody = "Hi " + pApproverName + ",<br><br>" +
               "There is an MRCF submitted by " + pRequestorName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ApproveToRequestor:
     strSubject = "Approved MRCF";
     strBody = "Hi " + pRequestorName + ",<br><br>" +
               "Your MRCF has been approved by " + pApproverName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "'>Click here to review the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ApproveToApproverGH:
     strSubject = "Approved MRCF";
     strBody = "You approved an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ApproveToApproverDH:
     strSubject = "Approved MRCF";
     strBody = "You approved an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ApproveToApproverPM:
     strSubject = "Approved MRCF";
     strBody = "You approved an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.DisapproveToRequestor:
     strSubject = "Disapproved MRCF";
     strBody = "Hi " + pRequestorName + ",<br><br>" +
               "Your MRCF has been disapproved by " + pApproverName + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "'>Click here to review the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.DisapproveToApproverGH:
     strSubject = "Disapproved MRCF";
     strBody = "You disapproved an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.DisapproveToApproverDH:
     strSubject = "Disapproved MRCF";
     strBody = "You disapproved an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.DisapproveToApproverPM:
     strSubject = "Disapproved MRCF";
     strBody = "You disapproved an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ModificationToRequestor:
     strSubject = "Modification Required: MRCF";
     strBody = "Hi " + pRequestorName + ",<br><br>" +
               "Your MRCF requires modification according to " + pApproverName + ".<br>" +
               "You need to provide necessary corrections as per " + pApproverName + "'s instruction.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view and edit the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ModificationToApproverGH:
     strSubject = "Modification Required: MRCF";
     strBody = "You have required modification for an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ModificationToApproverDH:
     strSubject = "Modification Required: MRCF";
     strBody = "You have required modification for an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

    case MRCFMailType.ModificationToApproverPM:
     strSubject = "Modification Required: MRCF";
     strBody = "You have required modification for an MRCF.<br><br>" +
               "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
               "<a href='" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "'>Click here to view the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + pMRCFCode + "</i><br><br>" +
               "All the best,<br>E-Forms Administrator";
     break;

   }
   clsSpeedo.SendMail(pMailTo, strSubject, strBody);
  }

  /// <summary>
  /// Get the status icon of an mrcf request
  /// </summary>
  /// <param name="pUserStat"></param>
  /// <param name="pSprvCode"></param>
  /// <param name="pSprvStat"></param>
  /// <param name="pHeadCode"></param>
  /// <param name="pHeadStat"></param>
  /// <param name="pProcCode"></param>
  /// <param name="pProcStat"></param>
  /// <returns></returns>
  public static string GetRequestStatusIcon(string pUserStat, string pSprvCode, string pSprvStat, string pHeadCode, string pHeadStat, string pProcCode, string pProcStat)
  {
   string strReturn = "";

   if (pUserStat == "V")
    strReturn = "Disapproved.png";
   else if (pUserStat == "D")
    strReturn = "Disapproved.png";
   else if (pUserStat == "M")
    strReturn = "Pending.png";
   else if (pUserStat == "F")
    strReturn = "Approval.png";
   else if (pUserStat == "A")
    strReturn = "Approved.png";

   return strReturn;
  }

  public static string GetRequestStatusRemarks(string pUserStat, string pSprvCode, string pSprvStat, string pHeadCode, string pHeadStat, string pProcCode, string pProcStat)
  {
   string strReturn = "";

   if (pUserStat == "V")
   {
    strReturn = "The requestor voided the request";
   }
   else if (pUserStat == "D")
   {
    if (pSprvStat == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pSprvCode, 2);
    else if (pHeadStat == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pHeadCode, 2);
    else if (pProcStat == "D")
     strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink(pProcCode, 2);
   }
   else if (pUserStat == "M")
   {
    if (pSprvStat == "M")
     strReturn = "Modification required by " + clsSpeedo.AssignUsernameLink(pSprvCode, 2);
    else if (pHeadStat == "M")
     strReturn = "Modification required by " + clsSpeedo.AssignUsernameLink(pHeadCode, 2);
    else if (pProcStat == "M")
     strReturn = "Modification required by " + clsSpeedo.AssignUsernameLink(pProcCode, 2);
   }
   else if (pUserStat == "F")
   {
    if (pSprvStat == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pSprvCode, 2);
    else if (pHeadStat == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pHeadCode, 2);
    else if (pProcStat == "F")
     strReturn = "For approval of " + clsSpeedo.AssignUsernameLink(pProcCode, 2);
   }
   else if (pUserStat == "A")
   {
    strReturn = "Approved by " + clsSpeedo.AssignUsernameLink(pProcCode, 2);
   }

   return strReturn;
  }

  public static DataTable GetDDLSourceMrcfAssetType()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT asstcode AS pValue,asset AS pText FROM CIS.MrcfAsset ORDER BY asset";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDDLSourceMrcfRequestType()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT reqtype AS pValue,typename AS pText FROM CIS.MrcfRequestType ORDER BY typename";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }


  public int Insert(DataTable tblItems)
  {
      int intReturn = 0;
      string strMrcfCode = "";
      string strApproverName = "";
      string strApproverMail = "";
      clsMRCF.MRCFMailType mmtApprover = clsMRCF.MRCFMailType.ApproveToApproverGH;

      SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
      cn.Open();
      SqlTransaction tran = cn.BeginTransaction();
      SqlCommand cmd = cn.CreateCommand();
      cmd.CommandText = "spMRCFInsert";
      cmd.Transaction = tran;
      try
      {
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
          cmd.Parameters.Add("@reqtype", SqlDbType.Char, 1);
          cmd.Parameters.Add("@datereq", SqlDbType.DateTime);
          cmd.Parameters.Add("@intended", SqlDbType.VarChar, 200);
          cmd.Parameters.Add("@chargeto", SqlDbType.VarChar, 200);
          cmd.Parameters.Add("@sprvcode", SqlDbType.VarChar, 30);
          cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
          cmd.Parameters.Add("@headcode", SqlDbType.VarChar, 30);
          cmd.Parameters.Add("@headstat", SqlDbType.Char, 1);
          cmd.Parameters.Add("@proccode", SqlDbType.VarChar, 30);
          cmd.Parameters.Add("@procstat", SqlDbType.Char, 1);
          cmd.Parameters.Add("@status", SqlDbType.Char, 1);
          cmd.Parameters.Add("@mrcfcode", SqlDbType.VarChar, 9);

          cmd.Parameters["@username"].Value = _strUsername;
          cmd.Parameters["@reqtype"].Value = _strRequestType;
          cmd.Parameters["@datereq"].Value = DateTime.Now;
          cmd.Parameters["@intended"].Value = _strIntended;
          cmd.Parameters["@chargeto"].Value = _strChargeTo;
          cmd.Parameters["@headcode"].Value = _strDivisionHead;
          cmd.Parameters["@proccode"].Value = ConfigurationManager.AppSettings["ProcurementManager"];
          cmd.Parameters["@procstat"].Value = "F";
          cmd.Parameters["@status"].Value = "F";

          // Check if the requestor is the division head
          if (_strUsername == _strDivisionHead)
          {
              strApproverName = _strProcurementManager;
              strApproverMail = _strProcurementManager;
              mmtApprover = clsMRCF.MRCFMailType.SentToApproverPM;

              cmd.Parameters["@sprvcode"].Value = "";
              cmd.Parameters["@sprvstat"].Value = "X";
              cmd.Parameters["@headstat"].Value = "A";
          }
          // Check if the requestor is the group approver
          else if (_strUsername == _strGroupHead)
          {
              // If the approval of division head is required
              //if (clsMRCF.IsHeadApprovalRequired(ddlChargeTo.SelectedValue, ddlDivision.SelectedValue.ToString()))

              if (clsModuleApprover.IsDivisionHeadApprovalRequired(clsModule.MRCFModule, _strChargeTo, _strDivisionHead))
              {
                  strApproverName = _strDivisionHead;
                  strApproverMail = clsUsers.GetEmail(_strDivisionHead);
                  mmtApprover = clsMRCF.MRCFMailType.SentToApproverDH;

                  cmd.Parameters["@sprvcode"].Value = _strGroupHead;
                  cmd.Parameters["@sprvstat"].Value = "A";
                  cmd.Parameters["@headstat"].Value = "F";
              }
              // The approval of division head is not required
              else
              {
                  strApproverName = clsEmployee.GetName(_strProcurementManager);
                  strApproverMail = clsUsers.GetEmail(_strProcurementManager);
                  mmtApprover = clsMRCF.MRCFMailType.SentToApproverPM;

                  cmd.Parameters["@sprvcode"].Value = _strGroupHead;
                  cmd.Parameters["@sprvstat"].Value = "A";
                  cmd.Parameters["@headstat"].Value = "N";
              }
          }
          // The requestor is an ordinary user
          else
          {
              // There is no group head, the division head is the only approver
              if (_strGroupHead == "none")
              {
                  strApproverName = _strDivisionHead;
                  strApproverMail = clsUsers.GetEmail(_strDivisionHead); ;
                  mmtApprover = clsMRCF.MRCFMailType.SentToApproverDH;

                  cmd.Parameters["@sprvcode"].Value = "";
                  cmd.Parameters["@sprvstat"].Value = "X";
                  cmd.Parameters["@headstat"].Value = "F";
              }
              // If group head is = to divisionhead updated by Charlie Bachiller
              else if (_strGroupHead == _strDivisionHead)
              {
                  strApproverName = _strDivisionHead;
                  strApproverMail = clsUsers.GetEmail(_strDivisionHead); ;
                  mmtApprover = clsMRCF.MRCFMailType.SentToApproverDH;

                  cmd.Parameters["@sprvcode"].Value = "";
                  cmd.Parameters["@sprvstat"].Value = "X";
                  cmd.Parameters["@headstat"].Value = "F";
              }
              // Ordinary MRCF
              else
              {
                  strApproverName = _strGroupHead;
                  strApproverMail = clsUsers.GetEmail(_strGroupHead);
                  mmtApprover = clsMRCF.MRCFMailType.SentToApproverGH;

                  cmd.Parameters["@sprvcode"].Value = _strGroupHead;
                  cmd.Parameters["@sprvstat"].Value = "F";
                  cmd.Parameters["@headstat"].Value = "F";
              }
          }

          cmd.Parameters["@mrcfcode"].Direction = ParameterDirection.Output;
          cmd.ExecuteNonQuery();
          strMrcfCode = cmd.Parameters["@mrcfcode"].Value.ToString();
          cmd.Parameters.Clear();

          cmd.CommandType = CommandType.Text;
          DataTable tblCart = tblItems;
          foreach (DataRow dr in tblCart.Rows)
          {
              string strItemDescription = "";
              //Item Description
              if (clsMrcfLineType.IsHasItemCode(dr["LineType"].ToString()) == true)
              {
                  strItemDescription = dr["itemdesc"].ToString();
              }
              else
              {
                  strItemDescription = dr["itemdesc"].ToString();
              }

              cmd.CommandText = "INSERT INTO CIS.MrcfDetails(mrcfcode,itemdesc,itemspec,asstcode, ltypcode, itemcode, qty,unit,dateneed,status,GLAccount,Destination,empname,birthdate) VALUES(@mrcfcode,@itemdesc,@itemspec,@asstcode, @ltypcode, @itemcode, @qty,@unit,@dateneed,'F',@GLAccount,@Destination,@empname,@birthdate)";
              cmd.Parameters.Add("@itemdesc", SqlDbType.VarChar, 100);
              cmd.Parameters.Add("@itemspec", SqlDbType.VarChar, 5000);
              cmd.Parameters["@itemdesc"].Value = strItemDescription;
              cmd.Parameters["@itemspec"].Value = dr["itemspec"].ToString();
              cmd.Parameters.Add(new SqlParameter("@mrcfcode", strMrcfCode));
              cmd.Parameters.Add(new SqlParameter("@GLAccount", dr["GLAccount"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@Destination", dr["Destination"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@ltypcode", dr["LineType"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@asstcode", dr["TransactionType"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@itemcode", dr["Item"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@qty", dr["qty"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@unit", dr["unit"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@dateneed", dr["dateneed"].ToString()));

              cmd.Parameters.Add(new SqlParameter("@Empname", dr["empname"].ToString()));
              cmd.Parameters.Add(new SqlParameter("@Birthdate", dr["Birthdate"].ToString()));
            intReturn =   cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();
          }

          tran.Commit();
          clsMRCF.SendNotification(mmtApprover, _strUsername, strApproverName, strApproverMail, strMrcfCode);
          clsMRCF.SendNotification(clsMRCF.MRCFMailType.SentToRequestor, _strUsername, strApproverName, clsUsers.GetEmail(_strUsername), strMrcfCode);

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

 }
}