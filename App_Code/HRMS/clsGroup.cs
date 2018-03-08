using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsGroup : IDisposable
 {
  private string _strGroupCode;
  private string _strGroupName;
  private string _strDivisionCode;

  public clsGroup() { }
  public clsGroup(string pGroupCode) { _strGroupCode = pGroupCode; }

  public string GroupCode { get { return _strGroupCode; } set { _strGroupCode = value; } }
  public string GroupName { get { return _strGroupName; } set { _strGroupName = value; } }
  public string DivisionCode { get { return _strDivisionCode; } set { _strDivisionCode = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Groups WHERE grpcode='" + _strGroupCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strGroupName = dr["grpname"].ToString();
     _strDivisionCode = dr["divicode"].ToString();
    }
    dr.Close();
   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  ///////// Static Members /////////

  public static string GetGroupName(string pGroupCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT grpname FROM HR.Groups WHERE grpcode='" + pGroupCode + "' GROUP BY grpname";
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
    cmd.CommandText = "SELECT grpcode AS pvalue, grpname AS ptext FROM HR.Groups ORDER BY grpname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}