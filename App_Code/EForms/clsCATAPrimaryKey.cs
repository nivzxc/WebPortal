using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsCATAPrimaryKey : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }
  private string _strXkey;
  private string _strXvalue;

  public string Xkey { get { return _strXkey; } set { _strXkey = value; } }
  public string xValue { get { return _strXvalue; } set { _strXvalue = value; } }

  public clsCATAPrimaryKey()
  {
   _strXkey = "";
   _strXvalue = "";
  }

  public void Fill()
  {
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT xkey, xvalue FROM Finance.CATAPrimaryKey WHERE xkey=@xkey";
   cmd.Parameters.Add(new SqlParameter("@xkey", _strXkey));
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   while (dr.Read())
   {
   _strXkey= dr["xkey"].ToString();
    _strXvalue=dr["xvalue"].ToString();
   }
  }
 }

  public static string GetValue(string pXkey)
  {
   string strReturn="";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText  = "SELECT xvalue FROM Finance.CATAPrimaryKey WHERE xkey=@xkey";
    cmd.Parameters.Add(new SqlParameter("@xkey", pXkey));
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }
 }
}