using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{

 public class clsEmployeeEducation : IDisposable
 {
  private string _strEducationCode;
  private string _strUsername;
  private string _strEducationLevelCode;
  private string _strCourse;
  private string _strSchoolName;
  private string _strSchoolAddress;
  private string _strInclusiveDates;
  private string _strRecognition;
  private string _strComplete;

  public clsEmployeeEducation() { }
  public clsEmployeeEducation(string pEducationCode) { _strEducationCode = pEducationCode; }

  public string EducationCode { get { return _strEducationCode; } set { _strEducationCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string EducationLevelCode { get { return _strEducationLevelCode; } set { _strEducationLevelCode = value; } }
  public string Course { get { return _strCourse; } set { _strCourse = value; } }
  public string SchoolName { get { return _strSchoolName; } set { _strSchoolName = value; } }
  public string SchoolAddress { get { return _strSchoolAddress; } set { _strSchoolAddress = value; } }
  public string InclusiveDates { get { return _strInclusiveDates; } set { _strInclusiveDates = value; } }
  public string Recognition { get { return _strRecognition; } set { _strRecognition = value; } }
  public string Complete { get { return _strComplete; } set { _strComplete = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeEducation WHERE educcode='" + _strEducationCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strEducationCode = dr["educcode"].ToString();
     _strUsername = dr["username"].ToString();
     _strEducationLevelCode = dr["educlvl"].ToString();
     _strCourse = dr["course"].ToString();
     _strSchoolName = dr["schlname"].ToString();
     _strSchoolAddress = dr["schladdr"].ToString();
     _strInclusiveDates = dr["incldate"].ToString();
     _strRecognition = dr["recogntn"].ToString();
     _strComplete = dr["complete"].ToString();
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
    cmd.CommandText = "INSERT INTO HR.EmployeeEducation VALUES(@educcode,@username,@educlvl,@course,@schlname,@schladdr,@incldate,@recogntn,@complete)";
    cmd.Parameters.Add(new SqlParameter("@educcode", GenerateCode(_strUsername)));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@educlvl", _strEducationLevelCode));
    cmd.Parameters.Add(new SqlParameter("@course", _strCourse));
    cmd.Parameters.Add(new SqlParameter("@schlname", _strSchoolName));
    cmd.Parameters.Add(new SqlParameter("@schladdr", _strSchoolAddress));
    cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
    cmd.Parameters.Add(new SqlParameter("@recogntn", _strRecognition));
    cmd.Parameters.Add(new SqlParameter("@complete", _strComplete));
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
    cmd.CommandText = "UPDATE HR.EmployeeEducation SET educlvl=@educlvl, course=@course, schlname=@schlname, schladdr=@schladdr, incldate=@incldate, recogntn=@recogntn, complete=@complete WHERE educcode=@educcode";
    cmd.Parameters.Add(new SqlParameter("@educcode", _strEducationCode));
    cmd.Parameters.Add(new SqlParameter("@educlvl", _strEducationLevelCode));
    cmd.Parameters.Add(new SqlParameter("@course", _strCourse));
    cmd.Parameters.Add(new SqlParameter("@schlname", _strSchoolName));
    cmd.Parameters.Add(new SqlParameter("@schladdr", _strSchoolAddress));
    cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
    cmd.Parameters.Add(new SqlParameter("@recogntn", _strRecognition));
    cmd.Parameters.Add(new SqlParameter("@complete", _strComplete));
    cn.Open();
    try { intReturn = cmd.ExecuteNonQuery(); }
    catch (Exception) {}
   }
   return intReturn;
  }

  public int Delete()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "DELETE FROM HR.EmployeeEducation WHERE educcode='" + _strEducationCode + "'";
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
    cmd.CommandText = "SELECT * FROM HR.EmployeeEducation INNER JOIN HR.EducationLevel ON HR.EmployeeEducation.educlvl = HR.EducationLevel.educlvl WHERE username='" + pUsername + "' ORDER BY educordr";
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
    cmd.CommandText = "SELECT TOP 1 educcode FROM HR.EmployeeEducation WHERE username='" + pUsername + "' ORDER BY educcode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["educcode"].ToString();
    dr.Close();
   }

   if (strLastCode == "")
    strReturn = "ED" + strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastCode.Substring(strLastCode.Length - 2)) + 1;
    strReturn = "ED" + strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }

   return strReturn;
  }
 }

}
