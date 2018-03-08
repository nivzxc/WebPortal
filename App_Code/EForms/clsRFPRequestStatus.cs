using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsRFPRequestStatus : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }
  private string _strStatusCode;
  private string _strStatusName;

  public string StatusCode { get { return _strStatusCode; } set { _strStatusCode = value; } }
  public string StatusName { get { return _strStatusName; } set { _strStatusName = value; } }

  public clsRFPRequestStatus()
  {
   _strStatusCode = "";
   _strStatusName = "";
  }

    public static string GetName(string pStatusCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT statname FROM Finance.RFPRequestStatus WHERE statcode=@statcode";
    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
   }
   return strReturn;
  }
 }
}