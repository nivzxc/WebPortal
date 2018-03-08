using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{

 public class clsEmployeeQualification : IDisposable
 {
  private string _strQualificationCode;
  private string _strUsername;
  private string _strInclusiveDates;
  private string _strQualification;
  private string _strRemarks;

  public clsEmployeeQualification() { }
  public clsEmployeeQualification(string pQualificationCode) { _strQualificationCode = pQualificationCode; }

  public string QualificationCode { get { return _strQualificationCode; } set { _strQualificationCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string InclusiveDates { get { return _strInclusiveDates; } set { _strInclusiveDates = value; } }
  public string Qualification { get { return _strQualification; } set { _strQualification = value; } }
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeQualification WHERE qualcode='" + _strQualificationCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strInclusiveDates = dr["incldate"].ToString();
     _strQualification = dr["qualfctn"].ToString();
     _strRemarks = dr["remarks"].ToString();
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
    cmd.CommandText = "INSERT INTO HR.EmployeeQualification VALUES(@qualcode,@username,@incldate,@qualfctn,@remarks)";
    cmd.Parameters.Add(new SqlParameter("@qualcode", GenerateCode(_strUsername)));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
    cmd.Parameters.Add(new SqlParameter("@qualfctn", _strQualification));
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
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
    cmd.CommandText = "UPDATE HR.EmployeeQualification SET incldate=@incldate, qualfctn=@qualfctn, remarks=@remarks WHERE qualcode=@qualcode";
    cmd.Parameters.Add(new SqlParameter("@qualcode", _strQualificationCode));
    cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
    cmd.Parameters.Add(new SqlParameter("@qualfctn", _strQualification));
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
    cn.Open();
    try { intReturn = cmd.ExecuteNonQuery(); }
    catch (Exception) { }
   }
   return intReturn;
  }

  public int Delete()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "DELETE FROM HR.EmployeeQualification WHERE qualcode='" + _strQualificationCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  ///////// Static Members /////////

  public static DataTable GetDataTable(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeQualification WHERE username='" + pUsername + "' ORDER BY qualfctn";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  /////////////////////////////////
  ///////// Helper Class //////////
  /////////////////////////////////

  private static string GenerateCode(string pUsername)
  {
   string strReturn = "";
   string strEmployeeCode = clsEmployee.GetEmployeeNumber(pUsername);
   string strLastCode = "";
   int intSeed = 0;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 qualcode FROM HR.EmployeeQualification WHERE username='" + pUsername + "' ORDER BY qualcode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["qualcode"].ToString();
    dr.Close();
   }

   if (strLastCode == "")
    strReturn = "QU" + strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastCode.Substring(strLastCode.Length - 2)) + 1;
    strReturn = "QU" + strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }

   return strReturn;
  }
 }

}
