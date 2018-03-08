using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEducationLevel
 {

  ///////// Static Members /////////

  public static DataTable GetDdlDs()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT educlvl AS pvalue, details AS ptext FROM HR.EducationLevel ORDER BY educordr";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetDetails(string pEducationLevelCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT details FROM HR.EducationLevel WHERE educlvl='" + pEducationLevelCode + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

 }
}
