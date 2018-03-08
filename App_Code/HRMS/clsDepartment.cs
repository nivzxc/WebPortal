using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsDepartment : IDisposable
 {
  private string _strDepartmentCode;
  private string _strDepartmentName;
  private string _strDivisionCode;
  private string _strGroupCode;

  public clsDepartment() { }
  public clsDepartment(string pDepartmentCode) { _strDepartmentCode = pDepartmentCode; }

  public string DepartmentCode { get { return _strDepartmentCode; } set { _strDepartmentCode = value; } }
  public string DepartmentName { get { return _strDepartmentName; } set { _strDepartmentName = value; } }
  public string DivisionCode { get { return _strDivisionCode; } set { _strDivisionCode = value; } }
  public string GroupCode { get { return _strGroupCode; } set { _strGroupCode = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Department WHERE deptcode='" + _strDepartmentCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strDepartmentName = dr["deptname"].ToString();
     _strDivisionCode = dr["divicode"].ToString();
     _strGroupCode = dr["grpcode"].ToString();
    }
    dr.Close();
   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  ///////// Static Members /////////

  public static string GetName(string pDepartmentCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT deptname FROM HR.Department WHERE deptcode='" + pDepartmentCode + "' ORDER BY deptname";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static DataTable GetDdlDs()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT deptcode AS pvalue, deptname AS ptext FROM HR.Department ORDER BY deptname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDdlDs(string pDivisionCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT deptcode AS pvalue, deptname AS ptext FROM HR.Department WHERE divicode='" + pDivisionCode + "' ORDER BY deptname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }
  public static DataTable GetDdlDsBasedOnModuleApprover(string pDivisionCode, string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT deptcode AS pvalue, deptname AS ptext FROM HR.Department WHERE divicode=@divicode AND deptcode IN (SELECT deptcode FROM Speedo.ModuleApprover WHERE username=@username AND modlcode='EJS') ORDER BY deptname";
          cmd.Parameters.Add(new SqlParameter("@divicode", pDivisionCode));
          cmd.Parameters.Add(new SqlParameter("@username", pUsername));
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }


  public static DataTable GetDdlDs(string pDivisionCode, string pGroupCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT deptcode AS pvalue, deptname AS ptext FROM HR.Department WHERE divicode='" + pDivisionCode + "' AND grpcode='" + pGroupCode + "' ORDER BY deptname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}