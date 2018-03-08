using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsTimesheetAccess
 {
  public clsTimesheetAccess() { }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DataTable DSLEmployee(string pApproverUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext FROM HR.Employees WHERE username IN (SELECT username FROM HR.TimesheetAccess WHERE approver='" + pApproverUsername + "') ORDER BY lastname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}