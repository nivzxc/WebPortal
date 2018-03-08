using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsEmployeeTitle
 {
  private string strTitle;

  public clsEmployeeTitle()
  {
  }

  public string Title
  {
   set { strTitle = value; }
   get { return strTitle; }
  }

  public int Add()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO HR.UsersTitle VALUES(@title)";
    cmd.Parameters.Add("@title", SqlDbType.VarChar, 20);
    cmd.Parameters["@title"].Value = strTitle;
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
    cmd.CommandText = "SELECT title AS pvalue, title AS ptext FROM HR.UsersTitle ORDER BY title";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}
