using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

public class clsSchoolNational : IDisposable
{
 public clsSchoolNational() { }

 public void Dispose() { GC.SuppressFinalize(this); }

 //////////////////////////////////
 ///////// Statis Members /////////
 //////////////////////////////////

 public static DataTable DSNational()
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT * FROM CM.SchoolNational ORDER BY natlcode";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

 public static DataTable DSLNational()
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT natlcode AS pvalue, natlname FROM CM.SchoolNational ORDER BY natlcode";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

}