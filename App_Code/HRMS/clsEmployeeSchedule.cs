using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeSchedule : IDisposable
 {
  private string _strEmployeeScheduleCode;
  private string _strUsername;
  private string _strScheduleCode;
  private DateTime _dteDateFrom;
  private DateTime _dteDateTo;
  private string _strReason;
  private string _strRemarks;
  private DateTime _dtePostDate;
  private string _strPostBy;

  public clsEmployeeSchedule() { }

  public string EmployeeScheduleCode { set { _strEmployeeScheduleCode = value; } get { return _strEmployeeScheduleCode; } }
  public string Username { set { _strUsername = value; } get { return _strUsername; } }
  public string ScheduleCode { set { _strScheduleCode = value; } get { return _strScheduleCode; } }
  public DateTime DateFrom { set { _dteDateFrom = value; } get { return _dteDateFrom; } }
  public DateTime DateTo { set { _dteDateTo = value; } get { return _dteDateTo; } }
  public string Reason { set { _strReason = value; } get { return _strReason; } }
  public string Remarks { set { _strRemarks = value; } get { return _strRemarks; } }
  public DateTime PostDate { set { _dtePostDate = value; } get { return _dtePostDate; } }
  public string PostBy { set { _strPostBy = value; } get { return _strPostBy; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeSchedule WHERE empschd='" + _strEmployeeScheduleCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strScheduleCode = dr["schdcode"].ToString();
     _dteDateFrom = clsValidator.CheckDate(dr["effcfrom"].ToString());
     _dteDateTo = clsValidator.CheckDate(dr["effcto"].ToString());
     _strReason = dr["preason"].ToString();
     _strRemarks = dr["premarks"].ToString();
     _strPostBy = dr["postby"].ToString();
     _dtePostDate = clsValidator.CheckDate(dr["postdate"].ToString());
    }
    dr.Close();
   }
  }

  public int Insert()
  {
   int intReturn = 0;

   int intCurrentKey = 0;
   string strCode = "";

   SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString);
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   cmd.Transaction = tran;

   cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='empschd'";
   intCurrentKey = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString());

   strCode = "000000000" + intCurrentKey.ToString();
   strCode = strCode.Substring(intCurrentKey.ToString().Length);

   cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='empschd'";
   cmd.ExecuteNonQuery();

   cmd.CommandText = "INSERT INTO HR.EmployeeSchedule VALUES(@empschd,@username,@schdcode,@effcfrom,@effcto,@preason,@premarks,@postby,@postdate)";
   cmd.Parameters.AddWithValue("@empschd", strCode);
   cmd.Parameters.AddWithValue("@username", _strUsername);
   cmd.Parameters.AddWithValue("@schdcode", _strScheduleCode);
   cmd.Parameters.AddWithValue("@effcfrom", _dteDateFrom);
   cmd.Parameters.AddWithValue("@effcto", _dteDateTo);
   cmd.Parameters.AddWithValue("@preason", _strReason);
   cmd.Parameters.AddWithValue("@premarks", _strRemarks);
   cmd.Parameters.AddWithValue("@postby", _strPostBy);
   cmd.Parameters.AddWithValue("@postdate", _dtePostDate);
   intReturn = cmd.ExecuteNonQuery();

   try { tran.Commit(); }
   catch { tran.Rollback(); }
   finally { cn.Close(); }

   return intReturn;
  }

  public int Update()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    cn.Open();
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.EmployeeSchedule SET schdcode=@schdcode, effcfrom=@effcfrom, effcto=@effcto, preason=@preason, premarks=@premarks WHERE empschd=@empschd";
    cmd.Parameters.AddWithValue("@empschd", _strEmployeeScheduleCode);
    cmd.Parameters.AddWithValue("@schdcode", _strScheduleCode);
    cmd.Parameters.AddWithValue("@effcfrom", _dteDateFrom);
    cmd.Parameters.AddWithValue("@effcto", _dteDateTo);
    cmd.Parameters.AddWithValue("@preason", _strReason);
    cmd.Parameters.AddWithValue("@premarks", _strRemarks);
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
    cmd.CommandText = "DELETE FROM HR.EmployeeSchedule WHERE empschd='" + _strEmployeeScheduleCode + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static bool HasExistingApplication(string pUsername, DateTime pDateFrom, DateTime pDateTo)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT empschd FROM HR.EmployeeSchedule WHERE username='" + pUsername + "' AND (('" + pDateFrom + "' BETWEEN effcfrom AND effcto) OR ('" + pDateTo + "' BETWEEN effcfrom AND effcto))";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static bool HasExistingApplication(string pUsername, DateTime pFocusDate)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT empschd FROM HR.EmployeeSchedule WHERE username='" + pUsername + "' AND '" + pFocusDate + "' BETWEEN effcfrom AND effcto";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static string GetScheduleCode(string pUserName, DateTime pFocusDate)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT schdcode FROM HR.EmployeeSchedule WHERE username='" + pUserName + "' AND '" + clsDateTime.GetDateOnly(pFocusDate) + "' BETWEEN effcfrom AND effcto";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strReturn = dr["schdcode"].ToString();
    dr.Close();
   }
   return strReturn;
  }

  //public static string GetEmployeeScheduleCode(string pUsername, DateTime pFocusDate)
  //{
  // string strReturn = "";
  // using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
  // {
  //  SqlCommand cmd = cn.CreateCommand();
  //  cmd.CommandText = "SELECT empschd FROM HR.EmployeeSchedule WHERE username='" + pUsername + "' AND '" + clsDateTime.GetDateOnly(pFocusDate) + "' BETWEEN effcfrom AND effcto";
  //  cn.Open();
  //  SqlDataReader dr = cmd.ExecuteReader();
  //  if (dr.Read())
  //   strReturn = dr["empschd"].ToString();
  //  dr.Close();
  // }
  // return strReturn;
  //}

  public static DataTable GetDataTable(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeSchedule WHERE username='" + pUsername + "' ORDER BY postdate";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }


 }
}