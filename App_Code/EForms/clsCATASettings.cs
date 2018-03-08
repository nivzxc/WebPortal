using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace STIeForms
{
 public class clsCATASettings : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }

  private string _strSettingCode;
  private string _strTypeCode;
  private string _strSTypeCode;
  private string _strJobGradeCode;
  private string _strAmount;

  public string SettingCode { get { return _strSettingCode; } set { _strSettingCode = value; } }
  public string TypeCode { get { return _strTypeCode; } set { _strTypeCode = value; } }
  public string SubTypeCode { get { return _strSTypeCode; } set { _strSTypeCode = value; } }
  public string JobGradeCode { get { return _strJobGradeCode; } set { _strJobGradeCode = value; } }
  public string Amount { get { return _strAmount; } set { _strAmount = value; } }

  public clsCATASettings()
  {
   _strSettingCode="";
   _strTypeCode = "";
   _strSTypeCode = "";
   _strJobGradeCode = "";
   _strAmount = "";
  }

  public static string GetAmount(string pTypeCode, string pJobGradeCode)
  {
   string strReturn="";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = new SqlCommand("SELECT amount FROM Finance.CATASettings WHERE  typecode = @typecode AND jgcode = @jgcode", cn);
    cmd.Parameters.Add(new SqlParameter("@typecode", pTypeCode));
    cmd.Parameters.Add(new SqlParameter("@jgcode", pJobGradeCode));
    cn.Open();
    try
    {
     strReturn = cmd.ExecuteScalar().ToString();
    }
    catch
    {
     strReturn = "";
    }
   }
   return strReturn;
  }

  public static string GetAmount(string pTypeCode,string pStypcode, string pJobGradeCode)
  {   
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = new SqlCommand("SELECT amount FROM Finance.CATASettings WHERE typecode=@typecode AND jgcode=@jgcode AND stypcode=@stypcode", cn);
    //cmd.CommandText = "SELECT amount FROM Finance.CATASettings WHERE typecode=@typecode AND jgcode=@jgcode AND stypcode=@stypcode";
    cmd.Parameters.Add(new SqlParameter("@typecode", pTypeCode));
    cmd.Parameters.Add(new SqlParameter("@jgcode", pJobGradeCode));
    cmd.Parameters.Add(new SqlParameter("@stypcode", pStypcode));

    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
   }
   return strReturn;
  }

  //public static string GetSetCode(string pTypeCode, string pStypcode, string pJobGradeCode)
  //{
  // string strReturn = "";
  // using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  // {
  //  SqlCommand cmd = new SqlCommand("SELECT setcode FROM Finance.CATASettings WHERE typecode=@typecode AND jgcode=@jgcode AND stypcode=@stypcode", cn);
  //  cmd.Parameters.Add(new SqlParameter("@typecode", pTypeCode));
  //  cmd.Parameters.Add(new SqlParameter("@jgcode", pJobGradeCode));
  //  cmd.Parameters.Add(new SqlParameter("@stypcode", pStypcode));
  //  cn.Open();
  //  strReturn = cmd.ExecuteScalar().ToString();
  // }
  // return strReturn;
  //}

  public static string GetSubTypeCode(string pTypeCode, string pJobGradeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = new SqlCommand("SELECT stypcode FROM Finance.CATASettings WHERE typecode=@typecode AND jgcode=@jgcode", cn);
    cmd.Parameters.Add(new SqlParameter("@typecode", pTypeCode));
    cmd.Parameters.Add(new SqlParameter("@jgcode", pJobGradeCode));
    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
   }
   return strReturn;
  }
 }
}