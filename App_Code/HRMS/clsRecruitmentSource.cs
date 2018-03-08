using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsRecruitmentSource : IDisposable
 {
  public clsRecruitmentSource() { }

  private string _strRecruitmentSourceCode;
  private string _strRecruitmentSourceName;
  private string _strEnabled;

  public string RecruitmentSourceCode { get { return _strRecruitmentSourceCode; } set { _strRecruitmentSourceCode = value; } }
  public string RecruitmentSourceName { get { return _strRecruitmentSourceName; } set { _strRecruitmentSourceName = value; } }
  public string Enabled { get { return _strEnabled; } set { _strEnabled = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.RecruitmentSource WHERE rsrccode='" + _strRecruitmentSourceCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strRecruitmentSourceName = dr["rsrcname"].ToString();
     _strEnabled = dr["enabled"].ToString();
    }
    dr.Close();
   }
  }

  public int Insert()
  {
   int intReturn = 0;
   _strRecruitmentSourceCode = GenerateCode();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.RecruitmentSource VALUES(@rsrccode,@rsrcname,@enabled)";
    cmd.Parameters.Add("@rsrccode", SqlDbType.Char, 3);
    cmd.Parameters.Add("@rsrcname", SqlDbType.VarChar, 50);
    cmd.Parameters.Add("@enabled", SqlDbType.Char, 1);
    cmd.Parameters["@rsrccode"].Value = _strRecruitmentSourceCode;
    cmd.Parameters["@rsrcname"].Value = _strRecruitmentSourceName;
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
    cmd.CommandText = "UPDATE HR.RecruitmentSource SET rsrcname=@rsrcname, enabled=@enabled WHERE rsrccode=@rsrccode";
    cmd.Parameters.Add("@rsrccode", SqlDbType.Char, 3);
    cmd.Parameters.Add("@rsrcname", SqlDbType.VarChar, 50);
    cmd.Parameters.Add("@enabled", SqlDbType.Char, 1);
    cmd.Parameters["@rsrccode"].Value = _strRecruitmentSourceCode;
    cmd.Parameters["@rsrcname"].Value = _strRecruitmentSourceName;
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
    cmd.CommandText = "DELETE FROM HR.RecruitmentSource WHERE rsrccode='" + _strRecruitmentSourceCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DataTable DSGRecruitmentSourceList()
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("rsrccode");
   tblReturn.Columns.Add("rsrcname");
   tblReturn.Columns.Add("enabled");
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.RecruitmentSource ORDER BY rsrccode";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DSLRecruitmentSource()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rsrccode AS pvalue, rsrcname AS ptext FROM HR.RecruitmentSource WHERE enabled='1' ORDER BY rsrcname";
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
    cmd.CommandText = "SELECT TOP 1 rsrccode FROM HR.RecruitmentSource ORDER BY rsrccode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["rsrccode"].ToString();
    dr.Close();
   }
   intSeed = clsValidator.CheckInteger(strReturn) + 1;
   strReturn = ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length - 1);
   return strReturn;
  }
 }
}