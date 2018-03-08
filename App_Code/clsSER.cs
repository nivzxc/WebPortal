using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

public class clsSER : IDisposable
{
 public clsSER() { }

 public void Dispose() { GC.SuppressFinalize(this); }

 //////////////////////////////////
 ///////// Statis Members /////////
 //////////////////////////////////

 //public static DataTable GetProgramSemestral(string pSchoolCode, string pYearLevel)
 //{
 // DataTable tblReturn = new DataTable();
 // tblReturn.Columns.Add("pSem1");
 // tblReturn.Columns.Add("pSem2");
 // DataTable tblPrograms = clsProgramSER.DSProgram();
 // using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
 // {
 //  SqlCommand cmd = cn.CreateCommand();
 //  SqlDataReader dr;
 //  cn.Open();
 //  foreach (DataRow drwProgram in tblPrograms.Rows)
 //  {
 //   cmd.CommandText = "SELECT SUM(ns" + pYearLevel + "1) + SUM(os" + pYearLevel + "1) AS pSem1, SUM(ns" + pYearLevel + "2) + SUM(os" + pYearLevel + "2) AS pSem2 FROM Schools.SER WHERE schlcode='" + pSchoolCode + "' AND progcode='" + drwProgram["progcode"].ToString() + "'";
 //   dr = cmd.ExecuteReader();
 //   if (dr.Read())
 //   {
 //    DataRow drwN = tblReturn.NewRow();
 //    drwN["pSem1"] = clsValidator.CheckInteger(dr["pSem1"].ToString());
 //    drwN["pSem2"] = clsValidator.CheckInteger(dr["pSem2"].ToString());
 //    tblReturn.Rows.Add(drwN);
 //   }
 //   dr.Close();
 //  }
 // }
 // return tblReturn;
 //}

 public static DataTable GetProgramSemestral(string pSchoolCode)
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns11) + SUM(ns21) + SUM(ns31) + SUM(ns41) + SUM(ns51) + SUM(os11) + SUM(os21) + SUM(os31) + SUM(os41) + SUM(os51) AS pSem1, SUM(ns12) + SUM(ns22) + SUM(ns32) + SUM(ns42) + SUM(ns52) + SUM(os12) + SUM(os22) + SUM(os32) + SUM(os42) + SUM(os52) AS pSem2 FROM Schools.SER INNER JOIN Schools.SERProgram ON Schools.SER.progcode = Schools.SERProgram.progcode WHERE schlcode='" + pSchoolCode + "' GROUP BY Schools.SER.progcode,Schools.SERProgram.porder ORDER BY Schools.SERProgram.porder";
   cn.Open();
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

 public static DataTable GetProgramSemestral(string pSchoolCode, string pYearLevel)
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns" + pYearLevel + "1) + SUM(os" + pYearLevel + "1) AS pSem1, SUM(ns" + pYearLevel + "2) + SUM(os" + pYearLevel + "2) AS pSem2 FROM Schools.SER INNER JOIN Schools.SERProgram ON Schools.SER.progcode = Schools.SERProgram.progcode WHERE schlcode='" + pSchoolCode + "' GROUP BY Schools.SER.progcode,Schools.SERProgram.porder ORDER BY Schools.SERProgram.porder";
   cn.Open();
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

  public static DataTable GetProgramSemestralNational(string pNationalCode)
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns11) + SUM(ns21) + SUM(ns31) + SUM(ns41) + SUM(ns51) + SUM(os11) + SUM(os21) + SUM(os31) + SUM(os41) + SUM(os51) AS pSem1, SUM(ns12) + SUM(ns22) + SUM(ns32) + SUM(ns42) + SUM(ns52) + SUM(os12) + SUM(os22) + SUM(os32) + SUM(os42) + SUM(os52) AS pSem2 FROM Schools.SER INNER JOIN Schools.SERProgram ON Schools.SER.progcode = Schools.SERProgram.progcode WHERE schlcode IN (SELECT schlcode FROM CM.Schools WHERE natlcode='" + pNationalCode + "') GROUP BY Schools.SER.progcode,Schools.SERProgram.porder ORDER BY Schools.SERProgram.porder";
   cn.Open();
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 } 

 public static int GetNS()
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns11) + SUM(ns12) + SUM(ns21) + SUM(ns22) + SUM(ns31) + SUM(ns32) + SUM(ns41) + SUM(ns42) + SUM(ns51) + SUM(ns52) FROM Schools.SER";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetNS(string pSchoolCode)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns11) + SUM(ns12) + SUM(ns21) + SUM(ns22) + SUM(ns31) + SUM(ns32) + SUM(ns41) + SUM(ns42) + SUM(ns51) + SUM(ns52) FROM Schools.SER WHERE schlcode='" + pSchoolCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetNS(string pSchoolCode, string pYearLevel)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns" + pYearLevel.ToString() + "1) + SUM(ns" + pYearLevel.ToString() + "2) FROM Schools.SER WHERE schlcode='" + pSchoolCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetNS(string pSchoolCode, string pYearLevel, string pSemester)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns" + pYearLevel.ToString() + pSemester.ToString() + ") FROM Schools.SER WHERE schlcode='" + pSchoolCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetNS(string pSchoolCode, string pProgramCode, string pYearLevel, string pSemester)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT ns" + pYearLevel.ToString() + pSemester.ToString() + " FROM Schools.SER WHERE schlcode='" + pSchoolCode + "' AND progcode='" + pProgramCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetNSRegion(string pNationalcode)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns11) + SUM(ns12) + SUM(ns21) + SUM(ns22) + SUM(ns31) + SUM(ns32) + SUM(ns41) + SUM(ns42) + SUM(ns51) + SUM(ns52) FROM Schools.SER WHERE schlcode IN (SELECT schlcode FROM CM.Schools WHERE natlcode='" + pNationalcode + "')";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOGS()
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(os11) + SUM(os12) + SUM(os21) + SUM(os22) + SUM(os31) + SUM(os32) + SUM(os41) + SUM(os42) + SUM(os51) + SUM(os52) + SUM(ns11) + SUM(ns12) + SUM(ns21) + SUM(ns22) + SUM(ns31) + SUM(ns32) + SUM(ns41) + SUM(ns42) + SUM(ns51) + SUM(ns52) FROM Schools.SER";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOGS(string pSchoolCode)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(os11) + SUM(os12) + SUM(os21) + SUM(os22) + SUM(os31) + SUM(os32) + SUM(os41) + SUM(os42) + SUM(os51) + SUM(os52) + SUM(ns11) + SUM(ns12) + SUM(ns21) + SUM(ns22) + SUM(ns31) + SUM(ns32) + SUM(ns41) + SUM(ns42) + SUM(ns51) + SUM(ns52) FROM Schools.SER WHERE schlcode='" + pSchoolCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOGS(string pSchoolCode, string pProgramCode, string pSemester)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns1" + pSemester + ") + SUM(ns2" + pSemester + ") + SUM(ns3" + pSemester + ") + SUM(ns4" + pSemester + ") + SUM(ns5" + pSemester + ") + SUM(os1" + pSemester + ") + SUM(os2" + pSemester + ") + SUM(os3" + pSemester + ") + SUM(os4" + pSemester + ") + SUM(os5" + pSemester + ") FROM Schools.SER WHERE schlcode='" + pSchoolCode + "' AND progcode='" + pProgramCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOGS(string pProgramCode, string pSemester)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns1" + pSemester + ") + SUM(ns2" + pSemester + ") + SUM(ns3" + pSemester + ") + SUM(ns4" + pSemester + ") + SUM(ns5" + pSemester + ") + SUM(os1" + pSemester + ") + SUM(os2" + pSemester + ") + SUM(os3" + pSemester + ") + SUM(os4" + pSemester + ") + SUM(os5" + pSemester + ") FROM Schools.SER WHERE progcode='" + pProgramCode + "'";   
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOGS(string pSchoolCode, string pProgramCode, string pYearLevel, string pSemester)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns" + pYearLevel + pSemester + ") + SUM(os" + pYearLevel + pSemester + ") FROM Schools.SER WHERE schlcode='" + pSchoolCode + "' AND progcode='" + pProgramCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOGSRegion(string pNationalcode)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns11) + SUM(ns12) + SUM(ns21) + SUM(ns22) + SUM(ns31) + SUM(ns32) + SUM(ns41) + SUM(ns42) + SUM(ns51) + SUM(ns52) + SUM(os11) + SUM(os12) + SUM(os21) + SUM(os22) + SUM(os31) + SUM(os32) + SUM(os41) + SUM(os42) + SUM(os51) + SUM(os52) FROM Schools.SER WHERE schlcode IN (SELECT schlcode FROM CM.Schools WHERE natlcode='" + pNationalcode + "')";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOGSRegion(string pNationalCode, string pProgramCode, string pSemester)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(ns1" + pSemester + ") + SUM(ns2" + pSemester + ") + SUM(ns3" + pSemester + ") + SUM(ns4" + pSemester + ") + SUM(ns5" + pSemester + ") + SUM(os1" + pSemester + ") + SUM(os2" + pSemester + ") + SUM(os3" + pSemester + ") + SUM(os4" + pSemester + ") + SUM(os5" + pSemester + ") FROM Schools.SER WHERE progcode='" + pProgramCode + "' AND schlcode IN (SELECT schlcode FROM CM.Schools WHERE natlcode='" + pNationalCode + "')";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOS()
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(os11) + SUM(os12) + SUM(os21) + SUM(os22) + SUM(os31) + SUM(os32) + SUM(os41) + SUM(os42) + SUM(os51) + SUM(os52) FROM Schools.SER";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOS(string pSchoolCode)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(os11) + SUM(os12) + SUM(os21) + SUM(os22) + SUM(os31) + SUM(os32) + SUM(os41) + SUM(os42) + SUM(os51) + SUM(os52) FROM Schools.SER WHERE schlcode='" + pSchoolCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOS(string pSchoolCode, string pYearLevel)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(os" + pYearLevel.ToString() + "1) + SUM(os" + pYearLevel.ToString() + "2) FROM Schools.SER WHERE schlcode='" + pSchoolCode + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static int GetOSRegion(string pNationalcode)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT SUM(os11) + SUM(os12) + SUM(os21) + SUM(os22) + SUM(os31) + SUM(os32) + SUM(os41) + SUM(os42) + SUM(os51) + SUM(os52) FROM Schools.SER WHERE schlcode IN (SELECT schlcode FROM CM.Schools WHERE natlcode='" + pNationalcode + "')";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

}