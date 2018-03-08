using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{

 public class clsDivision : IDisposable
 {

  private string _strDivisionCode;
  private string _strDivisionName;
  private string _strDivisionNameShort;
  private string _strDescription;
  private string _strDivisionHead;

  public clsDivision() { }
  public clsDivision(string pDivisionCode) { _strDivisionCode = pDivisionCode; }

  public string Code { get { return _strDivisionCode; } set { _strDivisionCode = value; } }
  public string Name { get { return _strDivisionName; } set { _strDivisionName = value; } }
  public string NameShort { get { return _strDivisionNameShort; } set { _strDivisionNameShort = value; } }
  public string Description { get { return _strDescription; } set { _strDescription = value; } }
  public string Head { get { return _strDivisionHead; } set { _strDivisionHead = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Division WHERE divicode='" + _strDivisionCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strDivisionName = dr["division"].ToString();
     _strDivisionNameShort = dr["divisnam"].ToString();
     _strDescription = dr["pdesc"].ToString();
     _strDivisionHead = dr["divihead"].ToString();
    }
    dr.Close();
   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Methods /////////
  //////////////////////////////////

  public static int CountEmployeesTimesheet(string pDivisionCode)
  {
   int intReturn = 0;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(*) FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('PR','RE')";
    cn.Open();
    try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
    catch { }
   }

   return intReturn;
  }

  public static DataTable DSREmployeeCountManpowerCompliment()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT HR.Division.division, COUNT(HR.Employees.username) AS TotalEmployee FROM HR.Employees INNER JOIN HR.Division ON HR.Employees.divicode = HR.Division.divicode WHERE HR.Employees.pstatus='1' AND HR.Employees.esttcode IN (SELECT esttcode FROM HR.EmploymentStatus WHERE mnpwrcom='1') GROUP BY HR.Division.division ORDER BY HR.Division.division";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetDivisionHead(string pDivisionCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT divihead FROM HR.Division WHERE divicode='" + pDivisionCode + "' ORDER BY division";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string GetDivisionName(string pDivisionCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT division FROM HR.Division WHERE divicode='" + pDivisionCode + "' ORDER BY division";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string GetDivisionNameShort(string pDivisionCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT divisnam FROM HR.Division WHERE divicode='" + pDivisionCode + "' ORDER BY division";
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
    cmd.CommandText = "SELECT divicode AS pvalue, division AS ptext FROM HR.Division ORDER BY division";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }
  public static DataTable GetBudgetAllowedDdlDs(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT divicode AS pvalue, division AS ptext FROM HR.Division WHERE divicode IN (SELECT divicode FROM Budget.DivisionViewer WHERE username=@username) ORDER BY division";
          cmd.Parameters.Add(new SqlParameter("@username", pUsername));
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }
  public static DataTable GetDdlDsBasedOnApproverModule(string pUsername)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT divicode AS pvalue, division AS ptext FROM HR.Division WHERE divicode IN (SELECT divicode FROM Speedo.ModuleApprover WHERE username=@username AND modlcode='EJS') ORDER BY division";
          cmd.Parameters.Add(new SqlParameter("@username",pUsername));
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

  public static DataTable GetDdlDsDHead(string pDivisionCode)
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT divicode AS pvalue, (SELECT (firname + ' ' + lastname) FROM HR.employees WHERE username=hr.division.divihead) AS ptext FROM HR.Division WHERE divicode=@divicode ORDER BY division";
          cmd.Parameters.Add(new SqlParameter("@divicode", pDivisionCode));
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

  public static DataTable GetDSLDHead()
  {
      DataTable tblReturn = new DataTable();
      using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT divihead AS pvalue, (SELECT (firname + ' ' + lastname) FROM HR.employees WHERE username=hr.division.divihead) AS ptext FROM HR.Division ORDER BY (SELECT (firname + ' ' + lastname) FROM HR.employees WHERE username=hr.division.divihead) ASC";
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          da.Fill(tblReturn);
      }
      return tblReturn;
  }

  public static DataTable DSAll()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Division ORDER BY division";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTable(string pOrderBy)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Division ORDER BY " + pOrderBy;
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}