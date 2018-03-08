using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{

 class clsOvertimeType : IDisposable
{
  private string _strOvertimeTypeCode;
  private string _strOvertimeTypeName;
  private float _fltRate;

  public clsOvertimeType() { }
  public clsOvertimeType(string pOvertimeTypeCode) { _strOvertimeTypeCode = pOvertimeTypeCode; }

  public string OvertimeTypeCode { get { return _strOvertimeTypeCode; } set { _strOvertimeTypeCode = value; } }
  public string OvertimeTypeName { get { return _strOvertimeTypeName; } set { _strOvertimeTypeName = value; } }
  public float Rate { get { return _fltRate; } set { _fltRate = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.OvertimeType WHERE ottycode='" + _strOvertimeTypeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strOvertimeTypeCode = dr["ottycode"].ToString();
     _strOvertimeTypeName = dr["ottyname"].ToString();     
     _fltRate = clsValidator.CheckFloat(dr["prate"].ToString());
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
    cmd.CommandText = "INSERT INTO HR.OvertimeType VALUES(@ottycode,@ottyname,@prate)";
    cmd.Parameters.Add(new SqlParameter("@ottycode", _strOvertimeTypeCode));
    cmd.Parameters.Add(new SqlParameter("@ottyname", _strOvertimeTypeName));
    cmd.Parameters.Add(new SqlParameter("@prate", _fltRate));
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
    cmd.CommandText = "UPDATE HR.OvertimeType SET ottyname=@ottyname, prate=@prate WHERE ottycode=@ottycode";
    cmd.Parameters.Add(new SqlParameter("@ottycode", _strOvertimeTypeCode));
    cmd.Parameters.Add(new SqlParameter("@ottyname", _strOvertimeTypeName));
    cmd.Parameters.Add(new SqlParameter("@prate", _fltRate));
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
    cmd.CommandText = "DELETE FROM HR.OvertimeType WHERE ottycode=@ottycode";
    cmd.Parameters.Add(new SqlParameter("@ottycode", _strOvertimeTypeCode));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  ///////// Static Members /////////

  public static bool IsCodeExist(string pOvertimeTypeCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ottycode FROM HR.OvertimeType WHERE ottycode='" + pOvertimeTypeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable GetDataTable()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.OvertimeType ORDER BY ottycode";
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
    cmd.CommandText = "SELECT * FROM HR.OvertimeType ORDER BY " + pOrderBy;
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDdlDS()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ottyname AS ptext, ottycode AS pvalue FROM HR.OvertimeType ORDER BY ottycode";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}