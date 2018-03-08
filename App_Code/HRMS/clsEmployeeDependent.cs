using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeDependent : IDisposable
 {
  private string _strDependentCode;
  private string _strUsername;
  private string _strName;
  private DateTime _dteBirthdate;
  private string _strRelation;

  public clsEmployeeDependent() { }
  public clsEmployeeDependent(string pDependentCode) { _strDependentCode = pDependentCode; }

  public string DependentCode { get { return _strDependentCode; } set { _strDependentCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string Name { get { return _strName; } set { _strName = value; } }
  public DateTime Birthdate { get { return _dteBirthdate; } set { _dteBirthdate = value; } }
  public string Relation { get { return _strRelation; } set { _strRelation = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeDependent WHERE dpndcode='" + _strDependentCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strUsername = dr["username"].ToString();
     _strName = dr["pname"].ToString();
     _dteBirthdate = clsValidator.CheckDate(dr["brthdate"].ToString());
     _strRelation = dr["relation"].ToString();
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
    cmd.CommandText = "INSERT INTO HR.EmployeeDependent VALUES('" + GenerateCode(_strUsername) + "',@username,@pname,'" + _dteBirthdate + "',@relation)";
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@pname", _strName));
    cmd.Parameters.Add(new SqlParameter("@relation", _strRelation));
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
    cmd.CommandText = "UPDATE HR.EmployeeDependent SET pname=@pname, brthdate=@brthdate, relation=@relation WHERE dpndcode='" + _strDependentCode + "'";
    cmd.Parameters.Add(new SqlParameter("@pname", _strName));
    cmd.Parameters.Add(new SqlParameter("@brthdate", _dteBirthdate));
    cmd.Parameters.Add(new SqlParameter("@relation", _strRelation));    
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
    cmd.CommandText = "DELETE FROM HR.EmployeeDependent WHERE dpndcode='" + _strDependentCode + "'";
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
    cmd.CommandText = "SELECT * FROM HR.EmployeeDependent WHERE username='" + pUsername + "' ORDER BY pname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  ///////// Helper Class //////////

  private static string GenerateCode(string pUsername)
  {
   string strReturn = "";
   string strEmployeeCode = clsEmployee.GetEmployeeNumber(pUsername);
   string strLastDependentCode = "";
   int intSeed = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 dpndcode FROM HR.EmployeeDependent WHERE username='" + pUsername + "' ORDER BY dpndcode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastDependentCode = dr["dpndcode"].ToString();
    dr.Close();
   }
   if (strLastDependentCode == "")
    strReturn = strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastDependentCode.Substring(strLastDependentCode.Length - 2)) + 1;
    strReturn = strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }
   return strReturn;
  }

 }

}
