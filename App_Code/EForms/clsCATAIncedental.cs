using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsCATAIncedental : IDisposable
 {
  public void Dispose() { GC.SuppressFinalize(this); }

  private int _intIncidentalCode;
  private string _strCatacode;
  private string _strIncidental;
  private double _dblAmount;

  public int IncidentalCode { get { return _intIncidentalCode; } set { _intIncidentalCode = value; } }
  public string Catacode { get { return _strCatacode; } set { _strCatacode = value; } }
  public string Incidental { get { return _strIncidental; } set { _strIncidental = value; } }
  public double Amount { get { return _dblAmount; } set { _dblAmount = value; } }

  public clsCATAIncedental()
  {
   _intIncidentalCode = 0;
   _strCatacode = "";
   _strIncidental = "";
   _dblAmount = 0.00;
  }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ncdtcode,catacode,incdental,amount FROM Finance.CATAIncedental WHERE catacode=@catacode";
    cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     _strCatacode = dr["catacode"].ToString();
     _strIncidental = dr["incdental"].ToString();
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
    cmd.CommandText = "INSERT INTO Finance.CATAIncedental values(@ncdtcode,@catacode,@incdental,@amount)";
    cmd.Parameters.Add(new SqlParameter("@ncdtcode", _intIncidentalCode));
    cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
    cmd.Parameters.Add(new SqlParameter("@incdental", _strIncidental));
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
    using (SqlCommand cmd = cn.CreateCommand())
    {
     cmd.CommandText = "SELECT incdental,amount FROM Finance.CataIncedental WHERE catacode=@catacode";
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