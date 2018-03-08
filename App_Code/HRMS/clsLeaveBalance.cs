using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsLeaveBalance
 {

  public clsLeaveBalance()
  {

  }

  ///////// Static Members /////////

  public static DataTable GetDdlDSUsersLeaveTypes(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT leavtype AS pvalue,ltdesc AS ptext FROM HR.LeaveTypes WHERE pstatus='1' AND leavtype IN (SELECT leavtype FROM HR.LeaveBalance WHERE username='" + pUsername + "' AND pstatus='1') ORDER BY ltdesc";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static float GetRemainingBalance(string pLeavTypeCode, string pUsername)
  {
   float fltReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT pbalance FROM HR.LeaveBalance WHERE username='" + pUsername + "' AND leavtype='" + pLeavTypeCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
     fltReturn = clsValidator.CheckFloat(dr["pbalance"].ToString());
    dr.Close();
   }
   return fltReturn;
  }

  public static int DeductLeaveBalance(float pDeductUnit, string pUserName, string pLeaveType)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.LeaveBalance SET pbalance=pbalance-" + pDeductUnit + " WHERE username='" + pUserName + "' AND leavtype='" + pLeaveType + "'";
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public static DataTable GetActiveLeaveBalance(string pUserName)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ltdesc,maxbal,pbalance,maxbal-pbalance AS pused,HR.LeaveTypes.hasbal,HR.LeaveBalance.pstatus FROM HR.LeaveTypes INNER JOIN HR.LeaveBalance ON HR.LeaveTypes.leavtype = HR.LeaveBalance.leavtype WHERE HR.LeaveBalance.pstatus='1' AND HR.LeaveBalance.username='" + pUserName + "' ORDER BY ltdesc";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  // Added by Charlie 
  // March 21, 2011
  // Get Leave w/o Pay Balance per Employee per Leave Type
  public static DataTable GetActiveLeaveBalanceWOP(string pUserName)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ltdesc,maxbal,pbalance,maxbal-pbalance AS pused,HR.LeaveTypes.hasbal,HR.LeaveBalance.pstatus FROM HR.LeaveTypes INNER JOIN HR.LeaveBalance ON HR.LeaveTypes.leavtype = HR.LeaveBalance.leavtype WHERE HR.LeaveBalance.pstatus='1' AND  HR.LeaveBalance.username='" + pUserName + "' ORDER BY ltdesc asc";
    cn.Open();
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }


 }
}