using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeAchievement : IDisposable
 {
  private string _strAchievementCode;
  private string _strUsername;
  private string _strAchievement;
  private string _strAchievementDate;
  private string _strDetails;

  public clsEmployeeAchievement() { }
  public clsEmployeeAchievement(string pAchievementCode) { _strAchievementCode = pAchievementCode; }

  public string AchievementCode { get { return _strAchievementCode; } set { _strAchievementCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string Achievement { get { return _strAchievement; } set { _strAchievement = value; } }
  public string AchievementDate { get { return _strAchievementDate; } set { _strAchievementDate = value; } }
  public string Details { get { return _strDetails; } set { _strDetails = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeAchievement WHERE achvcode='" + _strAchievementCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strAchievement = dr["achivmnt"].ToString();
     _strAchievementDate = dr["achvdate"].ToString();
     _strDetails = dr["details"].ToString();
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
    cmd.CommandText = "INSERT INTO HR.EmployeeAchievement VALUES(@achvcode,@username,@achivmnt,@achvdate,@details)";
    cmd.Parameters.Add(new SqlParameter("@achvcode", GenerateCode(_strUsername)));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@achivmnt", _strAchievement));
    cmd.Parameters.Add(new SqlParameter("@achvdate", _strAchievementDate));
    cmd.Parameters.Add(new SqlParameter("@details", _strDetails));
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
    cmd.CommandText = "UPDATE HR.EmployeeAchievement SET achivmnt=@achivmnt, achvdate=@achvdate, details=@details WHERE achvcode=@achvcode";
    cmd.Parameters.Add(new SqlParameter("@achvcode", _strAchievementCode));
    cmd.Parameters.Add(new SqlParameter("@achivmnt", _strAchievement));
    cmd.Parameters.Add(new SqlParameter("@achvdate", _strAchievementDate));
    cmd.Parameters.Add(new SqlParameter("@details", _strDetails));
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
    cmd.CommandText = "DELETE FROM HR.EmployeeAchievement WHERE achvcode='" + _strAchievementCode + "'";
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
    cmd.CommandText = "SELECT * FROM HR.EmployeeAchievement WHERE username='" + pUsername + "' ORDER BY achivmnt";
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
    cmd.CommandText = "SELECT TOP 1 achvcode FROM HR.EmployeeAchievement WHERE username='" + pUsername + "' ORDER BY achvcode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["achvcode"].ToString();
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
