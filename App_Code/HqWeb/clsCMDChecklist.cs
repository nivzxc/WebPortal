using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsCMDChecklist
 {

  public static string ModuleCode { get { return "011"; } }

  public clsCMDChecklist()
  {

  }

  public static bool HasAccess(string pUsername)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM Users.UsersModules WHERE username='" + pUsername + "' AND modlcode='" + ModuleCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

 }
}