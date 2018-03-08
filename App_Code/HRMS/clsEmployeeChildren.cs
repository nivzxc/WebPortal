using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{

 public class clsEmployeeChildren : IDisposable
 {
  private string _strChildCode;
  private string _strUsername;
  private string _strName;
  private DateTime _dteBirthdate;

  public clsEmployeeChildren() { }
  public clsEmployeeChildren(string pChildCode) { _strChildCode = pChildCode; }

  public string ChildCode { get { return _strChildCode; } set { _strChildCode = value; } }
  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public string Name { get { return _strName; } set { _strName = value; } }
  public DateTime Birthdate { get { return _dteBirthdate; } set { _dteBirthdate = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.EmployeeChildren WHERE chldcode='" + _strChildCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strName = dr["pname"].ToString();
     _dteBirthdate = clsValidator.CheckDate(dr["brthdate"].ToString());
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
    cmd.CommandText = "INSERT INTO HR.EmployeeChildren VALUES('" + GenerateCode(_strUsername) + "','" + _strUsername + "',@pname,'" + _dteBirthdate + "')";
    cmd.Parameters.Add(new SqlParameter("@pname", _strName));
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
    cmd.CommandText = "UPDATE HR.EmployeeChildren SET pname=@pname, brthdate='" + _dteBirthdate + "' WHERE chldcode='" + _strChildCode + "'";
    cmd.Parameters.Add(new SqlParameter("@pname", _strName));
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
    cmd.CommandText = "DELETE FROM HR.EmployeeChildren WHERE chldcode='" + _strChildCode + "'";
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
    cmd.CommandText = "SELECT * FROM HR.EmployeeChildren WHERE username='" + pUsername + "' ORDER BY brthdate";
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
    cmd.CommandText = "SELECT TOP 1 chldcode FROM HR.EmployeeChildren WHERE username='" + pUsername + "' ORDER BY chldcode DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     strLastCode = dr["chldcode"].ToString();
    dr.Close();
   }

   if (strLastCode == "")
    strReturn = strEmployeeCode + "-01";
   else
   {
    intSeed = int.Parse(strLastCode.Substring(strLastCode.Length - 2)) + 1;
    strReturn = strEmployeeCode + "-" + ("00" + intSeed.ToString()).Substring(intSeed.ToString().Length);
   }

   return strReturn;
  }

 }

}
