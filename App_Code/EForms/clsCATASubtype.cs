using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsCATASubtype : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }

  private string _strTypecode;
  private string _strSTypecode;
  private string _strSTypename;
  private string _strEnabled;
  private string _strCreateBy;
  private DateTime _dteCreateOn;
  private string _strModifyBy;
  private DateTime _dteModifyOn;

  public string Typecode {get {return _strTypecode;} set {_strTypecode = value;}}
  public string STypecode {get {return _strSTypecode;} set {_strSTypecode=value;}}
  public string STypename {get {return _strSTypename;} set {_strSTypename = value;}}
  public string Enabled {get {return _strEnabled;} set {_strEnabled=value;}}
  public string CreateBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
  public DateTime CreateOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
  public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
  public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }
  
  public clsCATASubtype()
  {
   _strTypecode="";
   _strSTypecode="";
   _strSTypename="";
   _strEnabled="";
   _strCreateBy = "";
   _dteCreateOn = DateTime.Now;
   _strModifyBy = "";
   _dteModifyOn = DateTime.Now;
  }

  public static DataTable GetDSL(string pTypeCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT stypcode as pValue, stypname as pText FROM Finance.CATASubtype WHERE typecode=@typecode";
    cmd.Parameters.Add(new SqlParameter("@typecode", pTypeCode));
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetName(string pSubTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT stypname FROM Finance.CATASubtype WHERE stypcode =@stypcode";
    cmd.Parameters.Add(new SqlParameter("@stypcode", pSubTypeCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     strReturn = dr["stypname"].ToString();
    }
    cn.Close();
   }
   return strReturn;
  }

  public static string GetTypeCode(string pSubTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT typecode FROM Finance.CATASubType WHERE stypcode=@stypcode";
    cmd.Parameters.Add(new SqlParameter("@stypcode", pSubTypeCode));
    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
   }
   return strReturn;
  }
 }
}