using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsCity
 {

  ///////// Static Members /////////

  public static DataTable GetDdlDs()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT citycode AS pvalue, cityname AS ptext FROM Speedo.City ORDER BY cityname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}
