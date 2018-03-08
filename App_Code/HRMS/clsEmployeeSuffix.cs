using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeSuffix
 {
  private string strSuffix;

  public clsEmployeeSuffix()
  {
  }

  public string Suffix
  {
   set { strSuffix = value; }
   get { return strSuffix; }
  }

  public int Add()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.UsersSuffix VALUES(@suffix)";
    cmd.Parameters.Add("@suffix", SqlDbType.VarChar, 20);
    cmd.Parameters["@suffix"].Value = strSuffix;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  ///////// Static Members /////////

  public static DataTable GetDdlSource()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT suffix AS pvalue, suffix AS ptext FROM HR.UsersSuffix ORDER BY suffix";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}
