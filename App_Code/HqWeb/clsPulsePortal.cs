using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
 public class clsPulsePortal : IDisposable 
 {
  public clsPulsePortal() { }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Methods /////////
  //////////////////////////////////

  public static string GetPulse(DateTime pDateTime)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT pcontent FROM Speedo.Pulse WHERE appdate='" + pDateTime + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static int PostResponse(string pPulseCode, string pUsername, string pResponse)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "INSERT INTO Speedo.PulseResponse VALUES('" + pPulseCode + "','" + pUsername + "','" + pResponse + "','" + DateTime.Now + "')";
    cn.Open();
    try { intReturn = cmd.ExecuteNonQuery(); }
    catch { }
   }
   return intReturn;
  }

  public static bool IsAnswered(string pUsername, string pPulseCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT pulscode FROM Speedo.PulseResponse WHERE pulscode='" + pPulseCode + "' AND username='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static string GetPulseCode(DateTime pDateTime)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT pulscode FROM Speedo.Pulse WHERE appdate='" + pDateTime + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string GetAskerAlias(string pPulseCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT paskby FROM Speedo.Pulse WHERE pulscode='" + pPulseCode + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string GetUserResponse(string pPulseCode, string pUsername)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT response FROM Speedo.PulseResponse WHERE pulscode='" + pPulseCode + "' AND username='" + pUsername + "'";
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static DataTable GetPulseResults(string pPulseCode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT response AS xaxis, COUNT(response) AS yaxis FROM Speedo.PulseResponse GROUP BY response ORDER BY xaxis DESC";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}
