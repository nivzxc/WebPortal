using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsResignationReason : IDisposable
 {
  public clsResignationReason() { }

  private string _strResignationReasonCode;
  private string _strResignationReasonName;
  private string _strEnabled;

  public string ResignationReasonCode { get { return _strResignationReasonCode; } set { _strResignationReasonCode = value; } }
  public string ResignationReasonName { get { return _strResignationReasonName; } set { _strResignationReasonName = value; } }
  public string Enabled { get { return _strEnabled; } set { _strEnabled = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.ResignationReason WHERE rsgncode='" + _strResignationReasonCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strResignationReasonName = dr["rsgnname"].ToString();
     _strEnabled = dr["enabled"].ToString();
    }
    dr.Close();
   }
  }

  public int Insert()
  {
   int intReturn = 0;
   _strResignationReasonCode = GenerateCode();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.ResignationReason VALUES(@rsgncode,@rsgnname,@enabled)";
    cmd.Parameters.Add("@rsgncode", SqlDbType.Char, 3);
    cmd.Parameters.Add("@rsgnname", SqlDbType.VarChar, 50);
    cmd.Parameters.Add("@enabled", SqlDbType.Char, 1);
    cmd.Parameters["@rsgncode"].Value = _strResignationReasonCode;
    cmd.Parameters["@rsgnname"].Value = _strResignationReasonName;
    cmd.Parameters["@enabled"].Value = _strEnabled;
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
    cmd.CommandText = "UPDATE HR.ResignationReason SET rsgnname=@rsgnname, enabled=@enabled WHERE rsgncode=@rsgncode";
    cmd.Parameters.Add("@rsgncode", SqlDbType.Char, 3);
    cmd.Parameters.Add("@rsgnname", SqlDbType.VarChar, 50);
    cmd.Parameters.Add("@enabled", SqlDbType.Char, 1);
    cmd.Parameters["@rsgncode"].Value = _strResignationReasonCode;
    cmd.Parameters["@rsgnname"].Value = _strResignationReasonName;
    cmd.Parameters["@enabled"].Value = _strEnabled;
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
    cmd.CommandText = "DELETE FROM HR.ResignationReason WHERE rsgncode='" + _strResignationReasonCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DataTable DSGResignationReasonList()
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("rsgncode");
   tblReturn.Columns.Add("rsgnname");
   tblReturn.Columns.Add("enabled");
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.ResignationReason ORDER BY rsgncode";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     DataRow drwN = tblReturn.NewRow();
     drwN["rsgncode"] = dr["rsgncode"].ToString();
     drwN["rsgnname"] = dr["rsgnname"].ToString();
     drwN["enabled"] = (dr["enabled"].ToString() == "1" ? "Yes" : "No");
     tblReturn.Rows.Add(drwN);
    }
    dr.Close();
   }
   return tblReturn;
  }

  public static DataTable DSLResignationReason()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rsgncode AS pvalue, rsgnname AS ptext FROM HR.ResignationReason WHERE enabled='1' ORDER BY rsgnname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GenerateCode()
  {
   string strReturn = "";
   int intSeed = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 rsgncode FROM HR.ResignationReason ORDER BY rsgncode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["rsgncode"].ToString();
    dr.Close();
   }
   intSeed = clsValidator.CheckInteger(strReturn) + 1;
   strReturn = ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length - 1);
   return strReturn;
  }

  public static string GetResignationReasonLabel(string pResignationReasonCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rsgnname FROM HR.ResignationReason WHERE rsgncode='" + pResignationReasonCode + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

 }
}
