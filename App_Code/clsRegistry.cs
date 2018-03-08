using System;
using System.Data;
using System.Data.SqlClient;

public static class clsRegistry
{
 public static string OvertimeCodeField { get { return "otcode"; } }

 public static string GetValue(string pRegistryKey)
 {
  string strReturn = "";
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='" + pRegistryKey + "'";
   cn.Open();
   try { strReturn = cmd.ExecuteScalar().ToString(); }
   catch { }
  }
  return strReturn;
 }
}