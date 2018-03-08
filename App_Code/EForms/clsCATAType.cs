using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;


namespace STIeForms
{
 public class clsCATAType : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }

  private string _strTypeCode;
  private string _strTypeName;
  private string _strEnabled;
  private string _strCreateBy;
  private DateTime _dteCreateOn;
  private string _strModifyBy;
  private DateTime _dteModifyOn;

  public string TypeCode { get { return _strTypeCode; } set { _strTypeCode = value; } }
  public string TypeName { get { return _strTypeName; } set { _strTypeName = value; } }
  public string Enabled { get { return _strEnabled; } set { _strEnabled = value; } }
  public string CreateBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
  public DateTime CreateOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
  public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
  public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }

  public clsCATAType()
  {
   _strTypeCode = "";
   _strTypeName = "";
   _strEnabled = "";
   _strCreateBy = "";
   _dteCreateOn = DateTime.Now;
   _strModifyBy = "";
   _dteModifyOn = DateTime.Now;
  }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT typecode,typename,enabled FROM Finance.CATAType WHERE typecode=@typecode";
    cmd.Parameters.Add(new SqlParameter("@typecode", _strTypeCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     _strTypeCode = dr["typecode"].ToString();
     _strTypeName = dr["typename"].ToString();
     _strEnabled = dr["enabled"].ToString();
    }
   }
  }

  public int Insert()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO Finance.CATAType values(@typecode,@typename,@enabled,@createby,@createon,@modifyby,@modifyon)";
    cmd.Parameters.Add(new SqlParameter("@typecode", _strTypeCode));
    cmd.Parameters.Add(new SqlParameter("@typename", _strTypeName));
    cmd.Parameters.Add(new SqlParameter("@enabled", _strEnabled));
    cmd.Parameters.Add(new SqlParameter("@createby", _strCreateBy));
    cmd.Parameters.Add(new SqlParameter("@createon", _dteCreateOn));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public static string GetName(string pTypeCode)
  {
   string strReturn="";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT typename FROM Finance.CATAType WHERE typecode=@typecode AND enabled='1'";
    cmd.Parameters.Add(new SqlParameter("@typecode", pTypeCode));
    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
    return strReturn;
   }
  }

  public static string GetCode(string pTypeName)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT typecode FROM Finance.CATAType WHERE typename=@typename AND enabled='1'";
    cmd.Parameters.Add(new SqlParameter("@typename", pTypeName));
    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
   }
   return strReturn;
  }

  public static DataTable GetDSL()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT typecode as pValue, typename as pText FROM Finance.CATAType ORDER BY pValue";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDSLUsed(string pCATACode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT typecode as pValue, typename as pText FROM Finance.CATAType WHERE typecode IN (SELECT typecode FROM Finance.CATASubType WHERE stypcode IN (SELECt stypcode FROM Finance.CATADetails WHERE catacode=@catacode)) ORDER BY pValue";
    cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }
 }
}