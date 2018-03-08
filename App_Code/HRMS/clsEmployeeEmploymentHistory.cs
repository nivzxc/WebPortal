using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HRMS
{
 public class clsEmployeeEmploymentHistory : IDisposable
 {
  private string _strEmploymentHistoryCode;
  private string _strUsername;
  private string _strInclusiveDates;
  private string _strPosition;
  private string _strResponsibility;
  private string _strCompanyName;
  private string _strCompanyAddress;
  private string _strCompanyContactNumber;
  private string _strEmploymentStatusCode;

  public clsEmployeeEmploymentHistory() { }
  public clsEmployeeEmploymentHistory(string pEmploymentHistoryCode) { _strEmploymentHistoryCode = pEmploymentHistoryCode; }

  public string EmploymentHistoryCode { get { return _strEmploymentHistoryCode; } set { _strEmploymentHistoryCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string InclusiveDates { get { return _strInclusiveDates; } set { _strInclusiveDates = value; } }
  public string Position { get { return _strPosition; } set { _strPosition = value; } }
  public string Responsibility { get { return _strResponsibility; } set { _strResponsibility = value; } }
  public string CompanyName { get { return _strCompanyName; } set { _strCompanyName = value; } }
  public string CompanyAddress { get { return _strCompanyAddress; } set { _strCompanyAddress = value; } }
  public string CompanyContactNumber { get { return _strCompanyContactNumber; } set { _strCompanyContactNumber = value; } }
  public string EmploymentStatusCode { get { return _strEmploymentStatusCode; } set { _strEmploymentStatusCode = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeEmploymentHistory WHERE emhscode='" + _strEmploymentHistoryCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strInclusiveDates = dr["incldate"].ToString();
     _strPosition = dr["position"].ToString();
     _strResponsibility = dr["rspnsblt"].ToString();
     _strCompanyName = dr["compname"].ToString();
     _strCompanyAddress = dr["compaddr"].ToString();
     _strCompanyContactNumber = dr["compcont"].ToString();
     _strEmploymentStatusCode = dr["esttcode"].ToString();
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
    cmd.CommandText = "INSERT INTO HR.EmployeeEmploymentHistory VALUES(@emhscode,@username,@incldate,@position,@rspnsblt,@compname,@compaddr,@compcont,@esttcode)";
    cmd.Parameters.Add(new SqlParameter("@emhscode", GenerateCode(_strUsername)));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
    cmd.Parameters.Add(new SqlParameter("@position", _strPosition));
    cmd.Parameters.Add(new SqlParameter("@rspnsblt", _strResponsibility));
    cmd.Parameters.Add(new SqlParameter("@compname", _strCompanyName));
    cmd.Parameters.Add(new SqlParameter("@compaddr", _strCompanyAddress));
    cmd.Parameters.Add(new SqlParameter("@compcont", _strCompanyContactNumber));
    cmd.Parameters.Add(new SqlParameter("@esttcode", _strEmploymentStatusCode));
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
    try
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "UPDATE HR.EmployeeEmploymentHistory SET incldate=@incldate, position=@position, rspnsblt=@rspnsblt, compname=@compname, compaddr=@compaddr, compcont=@compcont, esttcode=@esttcode WHERE emhscode=@emhscode";
     cmd.Parameters.Add(new SqlParameter("@emhscode", _strEmploymentHistoryCode));
     cmd.Parameters.Add(new SqlParameter("@incldate", _strInclusiveDates));
     cmd.Parameters.Add(new SqlParameter("@position", _strPosition));
     cmd.Parameters.Add(new SqlParameter("@rspnsblt", _strResponsibility));
     cmd.Parameters.Add(new SqlParameter("@compname", _strCompanyName));
     cmd.Parameters.Add(new SqlParameter("@compaddr", _strCompanyAddress));
     cmd.Parameters.Add(new SqlParameter("@compcont", _strCompanyContactNumber));
     cmd.Parameters.Add(new SqlParameter("@esttcode", _strEmploymentStatusCode));
     cn.Open();
     intReturn = cmd.ExecuteNonQuery();
    }
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
    cmd.CommandText = "DELETE FROM HR.EmployeeEmploymentHistory WHERE emhscode='" + _strEmploymentHistoryCode + "'";
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
    cmd.CommandText = "SELECT * FROM HR.EmployeeEmploymentHistory WHERE username='" + pUsername + "' ORDER BY incldate";
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
    cmd.CommandText = "SELECT TOP 1 emhscode FROM HR.EmployeeEmploymentHistory WHERE username='" + pUsername + "' ORDER BY emhscode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["emhscode"].ToString();
    dr.Close();
   }

   if (strLastCode == "")
    strReturn = "EH" + strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastCode.Substring(strLastCode.Length - 2)) + 1;
    strReturn = "EH" + strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }

   return strReturn;
  }

 }

}