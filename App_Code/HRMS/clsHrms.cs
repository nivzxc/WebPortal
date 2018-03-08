using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace HRMS
{
 public static class clsHrms
 {
  private static string _strUsername;
  private static string _strPassword;
  private static string _strAccessLevel;

  public static string HrmsConnectionString
  { get { return System.Configuration.ConfigurationManager.ConnectionStrings["Speedo"].ToString(); } }

  public static string ACMConnectionString
  { get { return System.Configuration.ConfigurationManager.ConnectionStrings["ACM"].ToString(); } }

  public static string MessageBoxText
  { get { return "HRMS System"; } }

  public static string MessageBoxValidationError
  { get { return "The system cannot save your entry.\nPlease correct the following validation error(s):\n\n"; } }

  public static string MessageBoxErrorAdd
  { get { return "An error occured while adding your data.\n\nPlease contact your system administrator."; } }

  public static string MessageBoxErrorEdit
  { get { return "An error occured while editing your data.\n\nPlease contact your system administrator."; } }

  public static string MessageBoxSuccessAddAskNew
  { get { return "Your data was successfully added.\n\nWould you like to add new record?"; } }

  public static string RequiredTotalWorkHoursKey
  { get { return "000000001"; } }

  public static string Username
  {
   get { return _strUsername; }
   set { _strUsername = value; }
  }

  public static string Password
  {
   get { return _strPassword; }
   set { _strPassword = value; }
  }

  public static string AccessLevel
  {
   get { return _strAccessLevel; }
   set { _strAccessLevel = value; }
  }

  public static int WorkHoursPerDay
  {
   get { return 8; }
  }

  public static int ExecuteSQL(string pCommandText)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = pCommandText;
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

 }
}