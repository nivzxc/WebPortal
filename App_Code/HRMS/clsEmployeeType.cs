using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;

namespace HRMS
{
 public class clsEmployeeType : IDisposable
 {
  private string _strEmployeeTypeCode;
  private string _strName;
  private string _strDescription;

  public clsEmployeeType() { }
  public clsEmployeeType(string pEmployeeTypeCode) { _strEmployeeTypeCode = pEmployeeTypeCode; }

  public string EmployeeTypeCode { get { return _strEmployeeTypeCode; } set { _strEmployeeTypeCode = value; } }
  public string Name { get { return _strName; } set { _strName = value; } }
  public string Description { get { return _strDescription; } set { _strDescription = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeType WHERE etypcode='" + _strEmployeeTypeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strEmployeeTypeCode = dr["etypcode"].ToString();
     _strName = dr["etypname"].ToString();
     _strDescription = dr["etypdesc"].ToString();
    }
    dr.Close();
   }
  }

  public int Add()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.EmployeeType VALUES('" + GenerateCode() + "',@etypname,@etypdesc)";
    cmd.Parameters.Add(new SqlParameter("@etypname", _strName));
    cmd.Parameters.Add(new SqlParameter("@etypdesc", _strDescription));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Edit()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.EmployeeType SET etypname=@etypname, etypdesc=@etypdesc WHERE etypcode=@etypcode";
    cmd.Parameters.Add(new SqlParameter("@etypcode", _strEmployeeTypeCode));
    cmd.Parameters.Add(new SqlParameter("@etypname", _strName));
    cmd.Parameters.Add(new SqlParameter("@etypdesc", _strDescription));
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
    cmd.CommandText = "DELETE FROM HR.EmployeeType WHERE etypcode=@etypcode";
    cmd.Parameters.Add(new SqlParameter("@etypcode", _strEmployeeTypeCode));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  ///////// Static Members /////////

  public static string GetEmployeeTypeName(string pEmployeeTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT etypname FROM HR.EmployeeType WHERE etypcode='" + pEmployeeTypeCode + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static DataTable GetDdlSource()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT etypcode AS pvalue, etypname AS ptext FROM HR.EmployeeType ORDER BY etypname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  ///////// Helper Methods /////////

  private static string GenerateCode()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 CAST(etypcode AS INT) FROM HR.EmployeeType ORDER BY CAST(etypcode AS INT) DESC";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   intReturn += 1;
   return (intReturn++).ToString("00");
  }

 }
}
