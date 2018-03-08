using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsDepartmentApprover
 {
  public clsDepartmentApprover() { }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static bool IsApprover(string pUsername, EFormType pEFormType)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pEFormType)
    {
     case EFormType.Leave:
      cmd.CommandText = "SELECT username FROM HR.DepartmentApprover WHERE username='" + pUsername + "' AND leave='1'";
      break;
     case EFormType.Overtime:
      cmd.CommandText = "SELECT username FROM HR.DepartmentApprover WHERE username='" + pUsername + "' AND ot='1'";
      break;
     case EFormType.OfficialBussiness:
      cmd.CommandText = "SELECT username FROM HR.DepartmentApprover WHERE username='" + pUsername + "' AND ob='1'";
      break;
     case EFormType.Undertime:
      cmd.CommandText = "SELECT username FROM HR.DepartmentApprover WHERE username='" + pUsername + "' AND ut='1'";
      break;
     case EFormType.RFI:
      cmd.CommandText = "SELECT username FROM HR.DepartmentApprover WHERE username='" + pUsername + "' AND friflag='1'";
      break;
    }
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable DSLApproverDepartment(string pDepartmentCode, EFormType pEFormType)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pEFormType)
    {
     case EFormType.Leave:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode='" + pDepartmentCode + "' AND leave='1') ORDER BY firname";
      break;
     case EFormType.OfficialBussiness:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode='" + pDepartmentCode + "' AND ob='1') ORDER BY firname";
      break;
     case EFormType.Overtime:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode='" + pDepartmentCode + "' AND ot='1') ORDER BY firname";
      break;
     case EFormType.Undertime:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode='" + pDepartmentCode + "' AND ut='1') ORDER BY firname";
      break;
     case EFormType.RFI:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode='" + pDepartmentCode + "' AND rfiflag='1') ORDER BY firname";
      break;
    }
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable DSLApproverEmployee(string pUsername, EFormType pEFormType)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pEFormType)
    {
     case EFormType.Leave:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND leave='1') ORDER BY firname";
      break;
     case EFormType.OfficialBussiness:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND ob='1') ORDER BY firname";
      break;
     case EFormType.Overtime:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND ot='1') ORDER BY firname";
      break;
     case EFormType.Undertime:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND ut='1') ORDER BY firname";
      break;
     case EFormType.RFI:
      cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM HR.DepartmentApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND rfiflag='1') ORDER BY firname";
      break;
    }
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    cn.Open();
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  // Added by Charlie
  // March 21, 2011
  public static void AuthenticateAccessFormWOP(string pLeaveUsers, string pCurrentUser)
  {
   string strChiefOperatingOfficerCode;
   string strDivisionCode = clsEmployee.GetDepartmentCode(pLeaveUsers);


   bool blnHasRecord;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT DISTINCT username FROM HR.DepartmentApprover WHERE    deptcode = @deptcode AND username=@username";
    cmd.Parameters.AddWithValue("@deptcode", strDivisionCode);
    cmd.Parameters.AddWithValue("@username", pCurrentUser);
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnHasRecord = dr.Read();
    dr.Close();
   }
   if (clsDepartmentApprover.GetCOO() == pCurrentUser)
   {
    blnHasRecord = true;
   }
   if (!blnHasRecord)
    System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
  }

  public static string GetCOO()
  {
   string strResult = "";
   DataTable tblCOOApprover = clsModuleApprover.DSLApprover(clsModule.LeaveModule, "3");
   if (tblCOOApprover.Rows.Count > 0)
   {
    strResult = tblCOOApprover.Rows[0]["pvalue"].ToString().Trim();
   }
   return strResult;
  }
  //added by charlie end

 }
}