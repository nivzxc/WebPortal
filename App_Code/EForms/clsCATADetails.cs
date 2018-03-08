using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsCATADetails : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }

  private string _strCataCode;
  private string _strSettingCode;
  private double _dblAmount;

  public string Catacode { get { return _strCataCode; } set { _strCataCode = value; } }
  public string SettingCode { get { return _strSettingCode; } set { _strSettingCode = value; } }
  public double Amount { get { return _dblAmount; } set { _dblAmount = value; } }

  public clsCATADetails()
  {
   _strCataCode = "";
   _strSettingCode = "";
   _dblAmount = 0.00;
  }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT catacode,setcode,amount FROM Finance.CataDetails WHERE catacode=@catacode";
    cmd.Parameters.Add(new SqlParameter("@catacode", _strCataCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     _strCataCode = dr["catacode"].ToString();
     _strSettingCode = dr["setcode"].ToString();
     _dblAmount = Convert.ToDouble(dr["amount"].ToString());
    }

   }
  }

  public int Insert()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO Finance.CataDetails values(@catacode,@setcode,@amount)";
    cmd.Parameters.Add(new SqlParameter("@catacode", _strCataCode));
    cmd.Parameters.Add(new SqlParameter("@setcode", _strSettingCode));
    cmd.Parameters.Add(new SqlParameter("@amount", _dblAmount));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////
  public static DataTable GetDSGMainForm(string pCataCode)
  {
   DataTable tblReturn = new DataTable();

   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
   using(SqlCommand cmd = cn.CreateCommand())
    {
     cmd.CommandText = "SELECT stypcode,amount FROM Finance.CataDetails WHERE catacode=@catacode";
     cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode)); 
     cn.Open();
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblReturn);
    }
   }
   return tblReturn;
  }

  public static DataTable GetCATADetails(string pTypeCode, string pCataCode)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("Catacode");
   tblReturn.Columns.Add("SubTypeCode");
   tblReturn.Columns.Add("Amount");

   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    using (SqlCommand cmd = cn.CreateCommand())
    {
     cmd.CommandText = "SELECT catacode,stypcode,amount FROM Finance.CataDetails WHERE stypcode IN (SELECT stypcode FROM Finance.CATASubtype WHERE typecode =@typecode) AND catacode=@catacode ORDER BY stypcode";
     cmd.Parameters.Add(new SqlParameter("@typecode", pTypeCode));
     cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
     cn.Open();
     using (SqlDataReader dr = cmd.ExecuteReader())
     {
      while (dr.Read())
      {
       DataRow drwNew = tblReturn.NewRow();
       drwNew["Catacode"] = dr["catacode"].ToString();
       drwNew["SubTypeCode"] = dr["stypcode"].ToString();
       drwNew["Amount"] = Convert.ToDouble(dr["amount"].ToString());
       tblReturn.Rows.Add(drwNew);
      }
      dr.Close();
     }
    }
   }
   return tblReturn;
  }

 }
}