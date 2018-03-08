using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace STIeForms
{
 public class clsCATARepresentation : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }
  private string _strCatacode;
  private string _strRepresentation;

  public string Catacode { get { return _strCatacode; } set { _strCatacode = value; } }
  public string Representation { get { return _strRepresentation; } set { _strRepresentation = value; } }

  public clsCATARepresentation()
  {
   _strCatacode = "";
   _strRepresentation = "";
  }

  public int Insert()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {

    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO Finance.CATARepresentation value(@catacode,@rprsnttn)";
    cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
    cmd.Parameters.Add(new SqlParameter("@rprsnttn", _strRepresentation));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public static DataTable GetDSGMainForm(string pCataCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    using (SqlCommand cmd = cn.CreateCommand())
    {
     cmd.CommandText = "SELECT rprsnttn FROM Finance.CataRepresentation WHERE catacode=@catacode";
     cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
     cn.Open();
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblReturn);
    }
   }
   return tblReturn;
  }
 }
}