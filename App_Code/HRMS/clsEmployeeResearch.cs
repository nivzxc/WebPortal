using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeResearch : IDisposable
 {
  private string _strResearchCode;
  private string _strUsername;
  private string _strTitle;
  private string _strDateCompleted;
  private string _strRemarks;

  public clsEmployeeResearch() { }
  public clsEmployeeResearch(string pResearchCode) { _strResearchCode = pResearchCode; }

  public string ResearchCode { get { return _strResearchCode; } set { _strResearchCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string Title { get { return _strTitle; } set { _strTitle = value; } }
  public string DateCompleted { get { return _strDateCompleted; } set { _strDateCompleted = value; } }
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }  

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeResearch WHERE resecode='" + _strResearchCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strTitle = dr["title"].ToString();
     _strDateCompleted = dr["datecomp"].ToString();
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
    cmd.CommandText = "INSERT INTO HR.EmployeeResearch VALUES(@resecode,@username,@title,@datecomp,@remarks)";
    cmd.Parameters.Add(new SqlParameter("@resecode", GenerateCode(_strUsername)));
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@title", _strTitle));
    cmd.Parameters.Add(new SqlParameter("@datecomp", _strDateCompleted));
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
    cmd.CommandText = "UPDATE HR.EmployeeResearch SET title=@title, datecomp=@datecomp, remarks=@remarks WHERE resecode=@resecode";
    cmd.Parameters.Add(new SqlParameter("@resecode", _strResearchCode));
    cmd.Parameters.Add(new SqlParameter("@title", _strTitle));
    cmd.Parameters.Add(new SqlParameter("@datecomp", _strDateCompleted));
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
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
    cmd.CommandText = "DELETE FROM HR.EmployeeResearch WHERE resecode='" + _strResearchCode + "'";
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
    cmd.CommandText = "SELECT * FROM HR.EmployeeResearch WHERE username='" + pUsername + "' ORDER BY title";
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
    cmd.CommandText = "SELECT TOP 1 resecode FROM HR.EmployeeResearch WHERE username='" + pUsername + "' ORDER BY resecode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["resecode"].ToString();
    dr.Close();
   }

   if (strLastCode == "")
    strReturn = "RE" + strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastCode.Substring(strLastCode.Length - 2)) + 1;
    strReturn = "RE" + strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }

   return strReturn;
  }
 }
}
