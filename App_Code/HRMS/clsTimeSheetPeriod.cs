using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{

 public class clsTimeSheetPeriod : IDisposable
 {

  private string _strTimeSheetPeriodCode;
  private DateTime _dtePeriodFrom;
  private DateTime _dtePeriodTo;
  private string _strDescription;
  private string _strMode;
  private string _strPostData;
  private string _strStatus;
  private string _strCreateBy;
  private DateTime _dteCreateOn;
  private string _strModifyBy;
  private DateTime _dteModifyOn;

  public clsTimeSheetPeriod() { }
  public clsTimeSheetPeriod(string pTimeSheetPeriodCode) { _strTimeSheetPeriodCode = pTimeSheetPeriodCode; }

  public string TimeSheetPeriodCode { get { return _strTimeSheetPeriodCode; } set { _strTimeSheetPeriodCode = value; } }
  public DateTime PeriodFrom { get { return _dtePeriodFrom; } set { _dtePeriodFrom = value; } }
  public DateTime PeriodTo { get { return _dtePeriodTo; } set { _dtePeriodTo = value; } }
  public string Description { get { return _strDescription; } set { _strDescription = value; } }
  public string Mode { get { return _strMode; } set { _strMode = value; } }
  public string PostData { get { return _strPostData; } set { _strPostData = value; } }
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
    cmd.CommandText = "SELECT * FROM HR.TimeSheetPeriod WHERE tspcode='" + _strTimeSheetPeriodCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strTimeSheetPeriodCode = dr["tspcode"].ToString();
     _dtePeriodFrom = clsValidator.CheckDate(dr["tspfrom"].ToString());
     _dtePeriodTo = clsValidator.CheckDate(dr["tspto"].ToString());
     _strDescription = dr["tspdesc"].ToString();
     _strMode = dr["tspmode"].ToString();
     _strPostData = dr["postdata"].ToString();
     _strStatus = dr["pstatus"].ToString();
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
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.TimeSheetPeriod VALUES(@tspcode,@tspfrom,@tspto,@tspdesc,@tspmode,@postdata,@pstatus,@createby,@createon,@modifyby,@modifyon)";
    cmd.Parameters.Add(new SqlParameter("@tspcode", _strTimeSheetPeriodCode));
    cmd.Parameters.Add(new SqlParameter("@tspfrom", _dtePeriodFrom));
    cmd.Parameters.Add(new SqlParameter("@tspto", _dtePeriodTo));
    cmd.Parameters.Add(new SqlParameter("@tspdesc", _strDescription));
    cmd.Parameters.Add(new SqlParameter("@tspmode", _strMode));
    cmd.Parameters.Add(new SqlParameter("@postdata", _strPostData));
    cmd.Parameters.Add(new SqlParameter("@pstatus", _strStatus));
    cmd.Parameters.Add(new SqlParameter("@createby", _strCreateBy));
    cmd.Parameters.Add(new SqlParameter("@createon", _dteCreateOn));
    cmd.Parameters.Add(new SqlParameter("@modifyby", _strModifyBy));
    cmd.Parameters.Add(new SqlParameter("@modifyon", _dteModifyOn));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Update()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.TimeSheetPeriod SET tspfrom=@tspfrom, tspto=@tspto, tspdesc=@tspdesc, tspmode=@tspmode, postdata=@postdata, pstatus=@pstatus, modifyby=@modifyby, modifyon=@modifyon WHERE tspcode=@tspcode";
    cmd.Parameters.Add(new SqlParameter("@tspcode", _strTimeSheetPeriodCode));
    cmd.Parameters.Add(new SqlParameter("@tspfrom", _dtePeriodFrom));
    cmd.Parameters.Add(new SqlParameter("@tspto", _dtePeriodTo));
    cmd.Parameters.Add(new SqlParameter("@tspdesc", _strDescription));
    cmd.Parameters.Add(new SqlParameter("@tspmode", _strMode));
    cmd.Parameters.Add(new SqlParameter("@postdata", _strPostData));
    cmd.Parameters.Add(new SqlParameter("@pstatus", _strStatus));
    cmd.Parameters.Add(new SqlParameter("@modifyby", _strModifyBy));
    cmd.Parameters.Add(new SqlParameter("@modifyon", _dteModifyOn));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Delete()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "DELETE FROM HR.TimeSheetPeriod WHERE tspcode='" + _strTimeSheetPeriodCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static bool IsCodeExist(string pCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT tspcode FROM HR.TimeSheetPeriod WHERE tspcode='" + pCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static string GetCurrentTimeSheetPeriod()
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 tspcode FROM HR.TimeSheetPeriod WHERE pstatus='1' ORDER BY tspcode";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string GetCurrentPosted()
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 tspcode FROM HR.TimeSheetPeriod WHERE postdata='1' ORDER BY tspcode";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string GetStatusDescription(string pStatus)
  {
   string strReturn = "";
   if (pStatus == "0")
    strReturn = "Closed";
   else if (pStatus == "1")
    strReturn = "Open";
   return strReturn;
  }

  public static DataTable DdlDs()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT tspcode AS pvalue, tspcode AS ptext FROM HR.TimeSheetPeriod ORDER BY tspcode";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DSGTimeSheetPeriodList()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.TimeSheetPeriod ORDER BY tspcode";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static void SetOpen(string pTSPCode)
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.TimeSheetPeriod SET pstatus='1' WHERE tspcode='" + pTSPCode + "'";
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

  public static void SetClose(string pTSPCode)
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.TimeSheetPeriod SET pstatus='0' WHERE tspcode='" + pTSPCode + "'";
    cn.Open();
    cmd.ExecuteNonQuery();
   }
  }

 }
}