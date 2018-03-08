using System;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsMRCFApprover
 {
  public enum MRCFApprover { GroupHeadApprover = 1, DivisionHeadApprover = 2, ProcurementManagerApprover = 3 }

  public clsMRCFApprover() { }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DataTable DSLGroupHeadApprover(string pRcCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username AS pvalue,firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM CIS.MRCFApprover WHERE rccode='" + pRcCode + "' AND pstatus='1' AND userlvl='sprv') ORDER BY firname";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetDivisionHeadApprover(string pRcCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM CIS.MRCFApprover WHERE rccode='" + pRcCode + "' AND pstatus='1' AND userlvl='head'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string GetProcurementManager()
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT username FROM CIS.MrcfApprover WHERE userlvl='pm'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { strReturn = ""; }
   }
   return strReturn;
  }

 }
}