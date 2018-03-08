using System;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsRequisition : IDisposable
 {

  public clsRequisition() { }
  public clsRequisition(string pRequisitionCode) { _strRequisitionCode = pRequisitionCode; }

  public enum RequisitionUserType
  {
   Requestor = 1,
   GroupHead = 2,
   DivisionHead = 3,
   SuppliesCustodian = 4
  }

  public enum RequisitionMailType
  {
   SentToRequestor = 1,
   SentToApproverGH = 2,
   SentToApproverDH = 3,
   SentToApproverSC = 4,
   ApproveToRequestor = 5,
   ApproveToApproverGH = 6,
   ApproveToApproverDH = 7,
   DisapproveToRequestor = 8,
   DisapproveToApproverGH = 9,
   DisapproveToApproverDH = 10,
   ModificationToRequestor = 11,
   ModificationToApproverGH = 12,
   ModificationToApproverDH = 13
  }

  private string _strRequisitionCode;
  private string _strUsername;
  private DateTime _dteDateRequested;
  private string _strIntended;
  private string _strChargeTo;
  private string _strGroupHead;
  private string _strGroupHeadStatus;
  private string _strGroupHeadRemarks;
  private string _strDivisionHead;
  private string _strDivisionHeadStatus;
  private string _strDivisionHeadRemarks;
  private string _strSuppliesCustodian;
  private string _strSuppliesCustodianStatus;
  private string _strSuppliesCustodianRemarks;
  private double _dblTotalCost;
  private string _strStatus;

  public static string CurrentSuppliesCustodian { get { return System.Configuration.ConfigurationManager.AppSettings["SuppliesCustodian"].ToString(); } }

  public string RequisitionCode { get { return _strRequisitionCode; } set { _strRequisitionCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime DateRequested { get { return _dteDateRequested; } set { _dteDateRequested = value; } }
  public string Intended { get { return _strIntended; } set { _strIntended = value; } }
  public string ChargeTo { get { return _strChargeTo; } set { _strChargeTo = value; } }
  public string GroupHead { get { return _strGroupHead; } set { _strGroupHead = value; } }
  public string GroupHeadStatus { get { return _strGroupHeadStatus; } set { _strGroupHeadStatus = value; } }
  public string GroupHeadRemarks { get { return _strGroupHeadRemarks; } set { _strGroupHeadRemarks = value; } }
  public string DivisionHead { get { return _strDivisionHead; } set { _strDivisionHead = value; } }
  public string DivisionHeadStatus { get { return _strDivisionHeadStatus; } set { _strDivisionHeadStatus = value; } }
  public string DivisionHeadRemarks { get { return _strDivisionHeadRemarks; } set { _strDivisionHeadRemarks = value; } }
  public string SuppliesCustodian { get { return _strSuppliesCustodian; } set { _strSuppliesCustodian = value; } }
  public string SuppliesCustodianStatus { get { return _strSuppliesCustodianStatus; } set { _strSuppliesCustodianStatus = value; } }
  public string SuppliesCustodianRemarks { get { return _strSuppliesCustodianRemarks; } set { _strSuppliesCustodianRemarks = value; } }
  public double TotalCost { get { return _dblTotalCost; } set { _dblTotalCost = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }

  public string StatusDescription
  {
   get
   {
    switch (_strStatus)
    {
     case "A":
      return "Approved";
     case "M":
      return "Modification Required";
     case "F":
      return "For Approval";
     case "V":
      return "Void";
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
  }

  public int CountItem()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(*) FROM CIS.RequisitionDetails WHERE requcode='" + _strRequisitionCode + "'";
    cn.Open();
    try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { intReturn = 0; }
   }
   return intReturn;
  }

  public void DisapproveDH(string pDHRemarks, string pGHStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    cmd.CommandText = "UPDATE CIS.Requisition SET headdate='" + DateTime.Now + "',headrem=@headrem,status='D',headstat='D',sprvstat=@sprvstat WHERE requcode='" + _strRequisitionCode + "'";
    cmd.Parameters.Add("@headrem", SqlDbType.VarChar, 200);
    cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
    cmd.Parameters["@headrem"].Value = pDHRemarks;
    cmd.Parameters["@sprvstat"].Value = (pGHStatus == "F" ? "N" : pGHStatus); ;
    cmd.ExecuteNonQuery();
   }
  }

  public void DisapproveGH(string pGHRemarks)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    cmd.CommandText = "UPDATE CIS.Requisition SET sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem,status='D',sprvstat='D',headstat='X',suppstat='X' WHERE requcode='" + _strRequisitionCode + "'";
    cmd.Parameters.Add("@sprvrem", SqlDbType.VarChar, 200);
    cmd.Parameters["@sprvrem"].Value = pGHRemarks;
    cmd.ExecuteNonQuery();
   }
  }

  public DataTable DSGItems()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT itemcode,itemdesc,qty,soqty,unit,price,tprice,reason,supprem,status FROM CIS.RequisitionDetails WHERE requcode='" + _strRequisitionCode + "' ORDER BY itemdesc";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM CIS.Requisition WHERE requcode='" + _strRequisitionCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _dteDateRequested = clsValidator.CheckDate(dr["datereq"].ToString());
     _strIntended = dr["userrem"].ToString();
     _strChargeTo = dr["rccode"].ToString();
     _strGroupHead = dr["sprvcode"].ToString();
     _strGroupHeadStatus = dr["sprvstat"].ToString();
     _strGroupHeadRemarks = dr["sprvrem"].ToString();
     _strDivisionHead = dr["headcode"].ToString();
     _strDivisionHeadStatus = dr["headstat"].ToString();
     _strDivisionHeadRemarks = dr["headrem"].ToString();
     _strSuppliesCustodian = dr["suppcode"].ToString();
     _strSuppliesCustodianStatus = dr["suppstat"].ToString();
     _strSuppliesCustodianRemarks = dr["supprem"].ToString();
     _dblTotalCost = clsValidator.CheckDouble(dr["totcost"].ToString());
     _strStatus = dr["status"].ToString();
    }
    dr.Close();
   }
  }

  public bool IsIssuedAll()
  {
   bool blnResult = true;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT qty,soqty FROM CIS.RequisitionDetails WHERE requcode='" + _strRequisitionCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     if (Convert.ToDouble(dr["qty"]) > Convert.ToDouble(dr["soqty"]))
      blnResult = false;
    }
    dr.Close();
   }
   return blnResult;
  }

  public void ModificationDH(string pDHRemarks, string pGHStatus)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    cmd.CommandText = "UPDATE CIS.Requisition SET headdate='" + DateTime.Now + "',headrem=@headrem,status='M',headstat='M',sprvstat=@sprvstat WHERE requcode='" + _strRequisitionCode + "'";
    cmd.Parameters.Add("@headrem", SqlDbType.VarChar, 200);
    cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
    cmd.Parameters["@headrem"].Value = pDHRemarks;
    cmd.Parameters["@sprvstat"].Value = (pGHStatus == "F" ? "N" : pGHStatus); ;
    cmd.ExecuteNonQuery();
   }
  }

  public void ModificationGH(string pGHRemarks)
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    cmd.CommandText = "UPDATE CIS.Requisition SET sprvdate='" + DateTime.Now + "',sprvrem=@sprvrem,status='M',sprvstat='M',headstat='X',suppstat='X' WHERE requcode='" + _strRequisitionCode + "'";
    cmd.Parameters.Add("@sprvrem", SqlDbType.VarChar, 200);
    cmd.Parameters["@sprvrem"].Value = pGHRemarks;
    cmd.ExecuteNonQuery();
   }
  }

  public double UpdateTotalCost()
  {
   double dblReturn = 0.0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Requisition SET totcost=(SELECT SUM(tprice) FROM CIS.RequisitionDetails WHERE requcode='" + _strRequisitionCode + "') WHERE requcode='" + _strRequisitionCode + "'";
    cn.Open();
    cmd.ExecuteNonQuery();

    cmd.CommandText = "SELECT totcost FROM CIS.Requisition WHERE requcode='" + _strRequisitionCode + "'";
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     dblReturn = Convert.ToDouble(dr["totcost"].ToString());
    dr.Close();
   }
   return dblReturn;
  }

  public void Void()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE CIS.Requisition SET status='V',sprvstat='F',headstat='N',suppstat='F' WHERE requcode='" + _strRequisitionCode + "'";
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static void AuthenticateUser(RequisitionUserType pUserType, string pUserName, string pRequisitionCode)
  {
   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    using (SqlCommand cmd = cn.CreateCommand())
    {
     if (pUserType == RequisitionUserType.Requestor)
      cmd.CommandText = "SELECT username FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "' AND username='" + pUserName + "'";
     else if (pUserType == RequisitionUserType.GroupHead)
      cmd.CommandText = "SELECT sprvcode FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "' AND sprvcode='" + pUserName + "'";
     else if (pUserType == RequisitionUserType.DivisionHead)
      cmd.CommandText = "SELECT headcode FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "' AND headcode='" + pUserName + "'";
     else if (pUserType == RequisitionUserType.SuppliesCustodian)
      cmd.CommandText = "SELECT suppcode FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "' AND suppcode='" + pUserName + "'";
     cn.Open();
     using (SqlDataReader dr = cmd.ExecuteReader())
     {
      blnHasRecord = dr.Read();
      dr.Close();
     }
    }
   }
   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static int CountRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(*) FROM CIS.Requisition";
    cn.Open();
    try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

  public static void DeductBudget(string pRcCode, double pDeduction)
  {
   string strQuarter = clsSpeedo.GetCurrentFiscalQuarter();
   string strFiscalYear = clsSpeedo.GetCurrentFiscalYear();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (strQuarter == "1")
     cmd.CommandText = "UPDATE Finance.QuarterBudget SET rbudget1=rbudget1-'" + pDeduction + "' WHERE rccode='" + pRcCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    else if (strQuarter == "2")
     cmd.CommandText = "UPDATE Finance.QuarterBudget SET rbudget2=rbudget2-'" + pDeduction + "' WHERE rccode='" + pRcCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    else if (strQuarter == "3")
     cmd.CommandText = "UPDATE Finance.QuarterBudget SET rbudget3=rbudget3-'" + pDeduction + "' WHERE rccode='" + pRcCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    else if (strQuarter == "4")
     cmd.CommandText = "UPDATE Finance.QuarterBudget SET rbudget4=rbudget4-'" + pDeduction + "' WHERE rccode='" + pRcCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public static double GetCurrentBudget(string pRCCode)
  {
   double dblReturn = 0;
   string strQuarter = clsSpeedo.GetCurrentFiscalQuarter();
   string strFiscalYear = clsSpeedo.GetCurrentFiscalYear();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (strQuarter == "1")
     cmd.CommandText = "SELECT rbudget1 AS rbudget FROM Finance.QuarterBudget WHERE rccode='" + pRCCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    else if (strQuarter == "2")
     cmd.CommandText = "SELECT rbudget2 AS rbudget FROM Finance.QuarterBudget WHERE rccode='" + pRCCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    else if (strQuarter == "3")
     cmd.CommandText = "SELECT rbudget3 AS rbudget FROM Finance.QuarterBudget WHERE rccode='" + pRCCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    else if (strQuarter == "4")
     cmd.CommandText = "SELECT rbudget4 AS rbudget FROM Finance.QuarterBudget WHERE rccode='" + pRCCode + "' AND fiscyear='" + strFiscalYear + "' AND accbcode='OSB'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     dblReturn = Convert.ToDouble(dr["rbudget"].ToString());
    dr.Close();
   }
   return dblReturn;
  }

  public static string GetLastDateItemRequest(string pItemCode, string pUserName)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT datereq FROM CIS.Requisition WHERE username='" + pUserName + "' AND requcode IN (SELECT requcode FROM CIS.RequisitionDetails WHERE itemcode='" + pItemCode + "') ORDER BY datereq DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["datereq"].ToString();
    else
     strReturn = "No request history found";
    dr.Close();
   }
   return strReturn;
  }

  public static string GetLastDateItemRequest(string pItemCode, string pUserName, DateTime pDateReq)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 datereq FROM CIS.Requisition WHERE username='" + pUserName + "' AND requcode IN (SELECT requcode FROM CIS.RequisitionDetails WHERE itemcode='" + pItemCode + "') AND datereq < '" + pDateReq + "' ORDER BY datereq DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["datereq"].ToString();
    else
     strReturn = "No request history found";
    dr.Close();
   }
   return strReturn;
  }

  public string GetRequestStatus(RequisitionUserType pUserType, string pRequisitionCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pUserType == RequisitionUserType.Requestor)
     cmd.CommandText = "SELECT status FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "'";
    else if (pUserType == RequisitionUserType.GroupHead)
     cmd.CommandText = "SELECT sprvstat FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "'";
    else if (pUserType == RequisitionUserType.DivisionHead)
     cmd.CommandText = "SELECT headstat FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "'";
    else if (pUserType == RequisitionUserType.SuppliesCustodian)
     cmd.CommandText = "SELECT suppstat FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { strReturn = ""; }
   }
   return strReturn;
  }

  public static string GetRequestStatusIcon(string UserStat, string SprvCode, string SprvStat, string HeadCode, string HeadStat, string SuppCode, string SuppStat)
  {
   string strReturn = "";

   if (UserStat == "V" || UserStat == "D")
    strReturn = "Disapproved.png";
   else if (UserStat == "M")
    strReturn = "Pending.png";
   else if (UserStat == "F")
    strReturn = "Approval.png";
   else if (UserStat == "A" || UserStat == "P")
    strReturn = "ForProcessing.png";
   else if (UserStat == "C")
    strReturn = "Approved.png";

   return strReturn;
  }

  public static string GetRequestStatusRemarks(string UserStat, string SprvCode, string SprvStat, string HeadCode, string HeadStat, string SuppCode, string SuppStat)
  {
   string strReturn = "";

   if (UserStat == "V")
    strReturn = "Voided request.";
   else if (UserStat == "D")
    strReturn = "Disapproved by " + clsSpeedo.AssignUsernameLink((SprvStat == "D" ? SprvCode : HeadCode), 2);
   else if (UserStat == "M")
    strReturn = "Modification required by " + clsSpeedo.AssignUsernameLink((SprvStat == "M" ? SprvCode : HeadCode), 2);
   else if (UserStat == "F")
    strReturn = "For approval of " + clsSpeedo.AssignUsernameLink((SprvStat == "F" ? SprvCode : HeadCode), 2);
   else if (UserStat == "A")
    strReturn = "For processing of " + clsSpeedo.AssignUsernameLink(SuppCode, 2);
   else if (UserStat == "P")
    strReturn = "Some items has been issued by " + clsSpeedo.AssignUsernameLink(SuppCode, 2);
   else if (UserStat == "C")
    strReturn = "All requested item has been issued by " + clsSpeedo.AssignUsernameLink(SuppCode, 2);

   return strReturn;
  }

  public static double GetTotalCost(string pRequisitionCode)
  {
   double dblReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT totcost FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "'";
    cn.Open();
    try { dblReturn = (double)cmd.ExecuteScalar(); }
    catch { dblReturn = 0; }
   }
   return dblReturn;
  }

  public static RequisitionUserType GetUserType(string pUsername)
  {
   RequisitionUserType rutReturn = RequisitionUserType.Requestor;
   if (pUsername.ToLower() == clsRequisition.CurrentSuppliesCustodian.ToLower())
   {
    rutReturn = RequisitionUserType.SuppliesCustodian;
   }
   else
   {
    using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT userlvl FROM CIS.MrcfApprover WHERE username='" + pUsername + "' AND pstatus='1' ORDER BY userlvl";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["userlvl"].ToString() == "head")
       rutReturn = RequisitionUserType.DivisionHead;
      else if (dr["userlvl"].ToString() == "sprv")
       rutReturn = RequisitionUserType.GroupHead;
     }
     else
     {
      rutReturn = RequisitionUserType.Requestor;
     }
     dr.Close();
    }
   }
   return rutReturn;
  }

  public static bool HasBudget(string pRequisitionCode, double pNewItemPrice, string pChargeTo)
  {
   double dblBudget = GetCurrentBudget(pChargeTo);
   double dblTPrice = 0.0;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT totcost FROM CIS.Requisition WHERE requcode='" + pRequisitionCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     try { dblTPrice = Convert.ToDouble(dr["totcost"].ToString()); }
     catch { }
    }
    dr.Close();
   }
   return (dblBudget >= (dblTPrice + pNewItemPrice));
  }

  public static bool HasBudget(string pRequisitionCode, double pNewItemPrice, string pChargeTo, string pItemCode)
  {
   double dblBudget = GetCurrentBudget(pChargeTo);
   double dblTotalPrice = pNewItemPrice;
   string strQuarter = clsSpeedo.GetCurrentFiscalQuarter();
   string strFiscalYear = clsSpeedo.GetCurrentFiscalYear();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT SUM(tprice) AS totalprice FROM CIS.RequisitionDetails WHERE requcode='" + pRequisitionCode + "' AND itemcode <> '" + pItemCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     try { dblTotalPrice += Convert.ToDouble(dr["totalprice"].ToString()); }
     catch { }
    }
    dr.Close();
   }
   return dblBudget >= dblTotalPrice;
  }

  public static bool IsApprover(RequisitionUserType pUserType, string pUsername)
  {
   bool blnReturn = false;
   if (pUserType == RequisitionUserType.GroupHead || pUserType == RequisitionUserType.DivisionHead)
   {
    using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
    {
     SqlCommand cmd = cn.CreateCommand();
     if (pUserType == RequisitionUserType.GroupHead)
      cmd.CommandText = "SELECT userlvl FROM CIS.RequisitionApprover WHERE userlvl='sprv' AND username='" + pUsername + "'";
     else if (pUserType == RequisitionUserType.DivisionHead)
      cmd.CommandText = "SELECT userlvl FROM CIS.RequisitionApprover WHERE userlvl='head' AND username='" + pUsername + "'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     blnReturn = dr.Read();
     dr.Close();
    }
   }
   else if (pUserType == RequisitionUserType.SuppliesCustodian)
   {
    blnReturn = (clsRequisition.CurrentSuppliesCustodian.ToLower() == pUsername ? true : false);
   }
   return blnReturn;
  }

  public static bool IsHeadApprovalRequired(string pRCCode, string pDivisionHead)
  {
   string strApprove = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT approve FROM CIS.RequisitionApprover WHERE username='" + pDivisionHead + "' AND rccode='" + pRCCode + "' AND userlvl='head'";
    cn.Open();
    try { strApprove = cmd.ExecuteScalar().ToString(); }
    catch { strApprove = ""; }
   }
   return (strApprove == "1" ? true : false);
  }

  public static bool IsIssuedAll(string pRequisitionCode)
  {
   bool blnResult = true;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT qty,soqty FROM CIS.RequisitionDetails WHERE requcode='" + pRequisitionCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     if (Convert.ToDouble(dr["qty"]) > Convert.ToDouble(dr["soqty"]))
      blnResult = false;
    }
    dr.Close();
   }
   return blnResult;
  }

  public static void SendNotification(RequisitionMailType pMailType, string pRequestorName, string pApproverName, string pMailTo, string pRequisitionCode)
  {
   string strSpeedoUrl = clsSystemConfigurations.PortalRootURL;
   string strSubject = "";
   string strBody = "";

   switch (pMailType)
   {
    case RequisitionMailType.SentToRequestor:
     {
      strSubject = "Delivered: Requisition";
      strBody = "Hi " + pRequestorName + ",<br><br>" +
                "Your Requisition has been delivered to " + pApproverName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.SentToApproverGH:
     {
      strSubject = "For Your Approval: Requisition";
      strBody = "Hi " + pApproverName + ",<br><br>" +
                "There is a Requisition submitted by " + pRequestorName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.SentToApproverDH:
     {
      strSubject = "For Your Approval: Requisition";
      strBody = "Hi " + pApproverName + ",<br><br>" +
                "There is a Requisition submitted by " + pRequestorName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.SentToApproverSC:
     {
      strSubject = "For Your Approval: Requisition";
      strBody = "Hi " + pApproverName + ",<br><br>" +
                "There is a Requisition submitted by " + pRequestorName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsSC.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsSC.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.ApproveToRequestor:
     {
      strSubject = "Approved Requisition";
      strBody = "Hi " + pRequestorName + ",<br><br>" +
                "Your Requisition has been approved by " + pApproverName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "'>Click here to review the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.ApproveToApproverGH:
     {
      strSubject = "Approved Requisition";
      strBody = "You approved a Requisition.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.ApproveToApproverDH:
     {
      strSubject = "Approved Requisition";
      strBody = "You approved a Requisition.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.DisapproveToRequestor:
     {
      strSubject = "Disapproved Requisition";
      strBody = "Hi " + pRequestorName + ",<br><br>" +
                "Your Requisition has been disapproved by " + pApproverName + ".<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "'>Click here to review the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.DisapproveToApproverGH:
     {
      strSubject = "Disapproved Requisition";
      strBody = "You disapproved a Requisition.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.DisapproveToApproverDH:
     {
      strSubject = "Disapproved Requisition";
      strBody = "You disapproved a Requisition.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.ModificationToRequestor:
     {
      strSubject = "Modification Required: Requisition";
      strBody = "Hi " + pRequestorName + ",<br><br>" +
                "Your Requisition requires modification according to " + pApproverName + ".<br>" +
                "You need to provide necessary corrections as per " + pApproverName + "'s instruction.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "'>Click here to view and edit the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetails.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.ModificationToApproverGH:
     {
      strSubject = "Modification Required: Requisition";
      strBody = "You have required modification for a Requisition.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsGH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

    case RequisitionMailType.ModificationToApproverDH:
     {
      strSubject = "Modification Required: Requisition";
      strBody = "You have required modification for a Requisition.<br><br>" +
                "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/CIS/Requisition/RequDetailsDH.aspx?requcode=" + pRequisitionCode + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      break;
     }

   }
   clsSpeedo.SendMail(pMailTo, strSubject, strBody);
  }

  public static string ToRequestStatusDesc(string RequestStatusCode)
  {
   switch (RequestStatusCode)
   {
    case "P":
     return "Pending";
    case "A":
     return "Approved";
    case "M":
     return "Modification Required";
    case "F":
     return "For Approval";
    case "V":
     return "Void";
    case "D":
     return "Disapproved";
    case "C":
     return "Completed";
    default:
     return "Unknown";
   }
  }

 }
}