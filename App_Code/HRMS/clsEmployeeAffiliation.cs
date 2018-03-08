using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeAffiliation : IDisposable
 {
  private string _strAffiliationCode;
  private string _strUsername;
  private string _strOrganization;
  private string _strPosition;
  private string _strInclusiveDates;
  private string _strRemarks;

  public clsEmployeeAffiliation() { }
  public clsEmployeeAffiliation(string pAffiliationCode) { _strAffiliationCode = pAffiliationCode; }

  public string AffiliationCode { get { return _strAffiliationCode; } set { _strAffiliationCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string Organization { get { return _strOrganization; } set { _strOrganization = value; } }
  public string Position { get { return _strPosition; } set { _strPosition = value; } }
  public string InclusiveDates { get { return _strInclusiveDates; } set { _strInclusiveDates = value; } }  
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeAffiliation WHERE afficode='" + _strAffiliationCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strOrganization = dr["organztn"].ToString();
     _strPosition = dr["position"].ToString();
     _strInclusiveDates = dr["incldate"].ToString();
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
    cmd.CommandText = "INSERT INTO HR.EmployeeAffiliation VALUES(@afficode,@username,@organztn,@position,@incldate,@remarks)";
    cmd.Parameters.Add(new SqlParameter("@afficode", GenerateCode(_strUsername)));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@organztn", _strOrganization));
    cmd.Parameters.Add(new SqlParameter("@position", _strPosition));
    cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
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
    cmd.CommandText = "UPDATE HR.EmployeeAffiliation SET organztn=@organztn, position=@position, incldate=@incldate, remarks=@remarks WHERE afficode=@afficode";
    cmd.Parameters.Add(new SqlParameter("@afficode", _strAffiliationCode));
    cmd.Parameters.Add(new SqlParameter("@organztn", _strOrganization));
    cmd.Parameters.Add(new SqlParameter("@position", _strPosition));
    cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
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
    cmd.CommandText = "DELETE FROM HR.EmployeeAffiliation WHERE afficode='" + _strAffiliationCode + "'";
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
    cmd.CommandText = "SELECT * FROM HR.EmployeeAffiliation WHERE username='" + pUsername + "' ORDER BY organztn";
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
    cmd.CommandText = "SELECT TOP 1 afficode FROM HR.EmployeeAffiliation WHERE username='" + pUsername + "' ORDER BY afficode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["afficode"].ToString();
    dr.Close();
   }

   if (strLastCode == "")
    strReturn = "AF" + strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastCode.Substring(strLastCode.Length - 2)) + 1;
    strReturn = "AF" + strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }

   return strReturn;
  }

 }
}
