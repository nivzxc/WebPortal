using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class clsProgramSER
{
 public clsProgramSER() { }


 public static DataTable DSProgram()
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT * FROM Schools.SERProgram ORDER BY porder";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

}