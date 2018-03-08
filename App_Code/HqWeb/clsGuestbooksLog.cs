using System;
using System.Data;
using System.Data.SqlClient;

public class clsGuestbooksLog
{
 public clsGuestbooksLog() { }

 //////////////////////////////////
 ///////// Static Members /////////
 //////////////////////////////////

 public static int InsertRecord(string pUsername, string pViewedBy)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "INSERT INTO Speedo.GuestbooksLog VALUES(@username,@viewedby,@viewedon)";
   cmd.Parameters.Add(new SqlParameter("@username", pUsername));
   cmd.Parameters.Add(new SqlParameter("@viewedby", pViewedBy));
   cmd.Parameters.Add(new SqlParameter("@viewedon", DateTime.Now));
   cn.Open();
   intReturn = cmd.ExecuteNonQuery();
  }
  return intReturn;
 }

 public static DataTable DSGLogList(string pUsername)
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT * FROM Speedo.GuestbooksLog WHERE username='" + pUsername + "' ORDER BY viewedon";
   cn.Open();
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

}