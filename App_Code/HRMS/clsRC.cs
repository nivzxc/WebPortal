using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsRC:IDisposable
 {
  private string strRcCode;
  private string strRcName;
  private string strDivisionCode;
  private string strGroupCode;
  private string strCompanyCode;
  private string strStatus;

  public clsRC() { }
  public clsRC(string pRcCode) { strRcCode = pRcCode; }

  public string RcCode { get { return strRcCode; } set { strRcCode = value; } }
  public string RcName { get { return strRcName; } set { strRcName = value; } }
  public string DivisionCode { get { return strDivisionCode; } set { strDivisionCode = value; } }
  public string GroupCode { get { return strGroupCode; } set { strGroupCode = value; } }
  public string CompanyCode { get { return strCompanyCode; } set { strCompanyCode = value; } }
  public string Status { get { return strStatus; } set { strStatus = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Rc WHERE rccode='" + strRcCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     strRcName = dr["rcname"].ToString();
     strDivisionCode = dr["divicode"].ToString();
     strGroupCode = dr["grpcode"].ToString();
     strCompanyCode = dr["comcode"].ToString();
     strStatus = dr["status"].ToString();
    }
    dr.Close();
   }
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  ///////// Static Members /////////

  public static string GetRCName(string pRcCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rcname FROM HR.Rc WHERE rccode='" + pRcCode + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { strReturn = ""; }
   }
   return strReturn;
  }

  public static DataTable GetDdlDs()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rccode AS pValue,rcname AS pText FROM HR.Rc WHERE status='1' ORDER BY rcname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDdlDsByDivisionCode(string pDivicode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rccode AS pValue,rcname AS pText FROM HR.Rc WHERE divicode='" + pDivicode + "' ORDER BY rcname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTable(string pDivicode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rccode,rcname FROM HR.Rc WHERE divicode='" + pDivicode + "' ORDER BY rcname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
    cn.Close();
   }
   return tblReturn;
  }

  public static string GetSQLInClauseGP(string pDivision)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT gpcode FROM HR.Rc WHERE divicode='" + pDivision + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     if (strReturn == "")
      strReturn = dr["gpcode"].ToString();
     else
      strReturn = strReturn + "','" + dr["gpcode"].ToString();
    }
    dr.Close();
   }
   return strReturn;
  }

 }
}
