using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsRFPRequestType : IDisposable
 {

  private string _strRequestCode;
  private string _strRequestDescription;
  public string RequestCode { get { return _strRequestCode; } set { _strRequestCode = value; } }
  public string RequestDescription { get { return _strRequestDescription; } set { _strRequestDescription = value; } }

  public clsRFPRequestType()
  {
   _strRequestCode = "";
   _strRequestDescription = "";
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////
  public static DataTable GetDSL()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rqstcode as pValue, rqstname as pText FROM Finance.RFPRequestType ORDER BY pText";
    cn.Open();

    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
    cn.Close();
   }
   return tblReturn;
  }

  public static string GetRequestTypeName(string pRequestTypeCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT rqstname FROM Finance.RFPRequestType WHERE rqstcode =@rqstcode";
    cmd.Parameters.Add(new SqlParameter("@rqstcode", pRequestTypeCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     strReturn = dr["rqstname"].ToString();
    }
    cn.Close();
   }
   return strReturn;
  }

 }
}

