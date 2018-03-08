using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsShift : IDisposable
 {
  private string _strShiftCode;
  private string _strShiftModeCode;
  private DateTime _dteTimeStart;
  private DateTime _dteTimeHalf;
  private DateTime _dteTimeEnd;
  private DateTime _dteBreakTimeStart;
  private DateTime _dteBreakTimeEnd;
  private DateTime _dteLateTime;
  private DateTime _dteUndertime;
  private float _fltTotalWorkHours;
  private string _strRemarks;

  public clsShift() { }
  public clsShift(string pShiftCode) { _strShiftCode = pShiftCode; }

  public string ShiftCode { get { return _strShiftCode; } set { _strShiftCode = value; } }
  public string ShiftModeCode { get { return _strShiftModeCode; } set { _strShiftModeCode = value; } }
  public DateTime TimeStart { get { return _dteTimeStart; } set { _dteTimeStart = value; } }
  public DateTime TimeHalf { get { return _dteTimeHalf; } set { _dteTimeHalf = value; } }
  public DateTime TimeEnd { get { return _dteTimeEnd; } set { _dteTimeEnd = value; } }
  public DateTime BreakTimeStart { get { return _dteBreakTimeStart; } set { _dteBreakTimeStart = value; } }
  public DateTime BreakTimeEnd { get { return _dteBreakTimeEnd; } set { _dteBreakTimeEnd = value; } }
  public DateTime LateTime { get { return _dteLateTime; } set { _dteLateTime = value; } }
  public DateTime UnderTime { get { return _dteUndertime; } set { _dteUndertime = value; } }
  public float TotalWorkHours { get { return _fltTotalWorkHours; } set { _fltTotalWorkHours = value; } }
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Shift WHERE shftcode='" + _strShiftCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _strShiftCode = dr["shftcode"].ToString();
     _strShiftModeCode = dr["shmdcode"].ToString();
     _dteTimeStart = clsValidator.CheckDate(dr["timestrt"].ToString());
     _dteTimeHalf = clsValidator.CheckDate(dr["timehalf"].ToString());
     _dteTimeEnd = clsValidator.CheckDate(dr["timeend"].ToString());
     _dteBreakTimeStart = clsValidator.CheckDate(dr["brkstrt"].ToString());
     _dteBreakTimeEnd = clsValidator.CheckDate(dr["brkend"].ToString());
     _dteLateTime = clsValidator.CheckDate(dr["timelate"].ToString());
     _dteUndertime = clsValidator.CheckDate(dr["timeundr"].ToString());
     _fltTotalWorkHours = clsValidator.CheckFloat(dr["tworkhrs"].ToString());
     _strRemarks = dr["remarks"].ToString();
    }
    dr.Close();
   }
  }

  public int Add()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (_strShiftModeCode == "W")
     cmd.CommandText = "INSERT INTO HR.Shift VALUES('" + _strShiftCode + "','W','" + _dteTimeStart + "','" + _dteTimeHalf + "','" + _dteTimeEnd + "','" + _dteBreakTimeStart + "','" + _dteBreakTimeEnd + "','" + _dteLateTime + "','" + _dteUndertime + "','" + _fltTotalWorkHours + "',@remarks)";
    else
     cmd.CommandText = "INSERT INTO HR.Shift VALUES('" + _strShiftCode + "','" + _strShiftModeCode + "',null,null,null,null,null,'" + _fltTotalWorkHours + "',@remarks)";
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Modify()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (_strShiftModeCode == "W")
     cmd.CommandText = "UPDATE HR.Shift SET shmdcode='W', timestrt='" + _dteTimeStart + "', timehalf='" + _dteTimeHalf + "', timeend='" + _dteTimeEnd + "', brkstrt='" + _dteBreakTimeStart + "', brkend='" + _dteBreakTimeEnd + "', timelate='" + _dteLateTime + "', timeundr='" + _dteUndertime + "', tworkhrs='" + _fltTotalWorkHours + "', remarks=@remarks WHERE shftcode='" + _strShiftCode + "'";
    else
     cmd.CommandText = "UPDATE HR.Shift SET shmdcode='" + _strShiftModeCode + "', timestrt=null, timehalf=null, timeend=null, brkstrt=null, brkend=null, timelate=null, timeundr=null, tworkhrs='" + _fltTotalWorkHours + "', remarks=@remarks WHERE shftcode='" + _strShiftCode + "'";
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public int Delete()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "DELETE FROM HR.Shift WHERE shftcode=@shftcode";
    cmd.Parameters.Add(new SqlParameter("@shftcode", _strShiftCode));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  //public double ComputeTotalWorkingHours()
  //{
  // double dblReturn = 0;
  // dblReturn += (_dteBreakTimeStart - _dteTimeStart).TotalHours + ((_dteBreakTimeStart - _dteTimeStart).Minutes / 60);
  // dblReturn += (_dteTimeEnd - _dteOverTime1End).TotalHours + ((_dteTimeEnd - _dteOverTime1End).Minutes / 60);
  // return dblReturn;
  //}

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static float GetTotalWorkingHours(string pShiftCode)
  {
   float fltReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT tworkhrs FROM HR.Shift WHERE shftcode='" + pShiftCode + "'";
    cn.Open();
    try { fltReturn = float.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return fltReturn;
  }

  public static bool IsCodeExist(string pShiftCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT shftcode FROM HR.Shift WHERE shftcode='" + pShiftCode + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static bool IsWorkingShift(string pShiftCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT shmdcode FROM HR.Shift WHERE shftcode='" + pShiftCode + "'";
    cn.Open();
    try { blnReturn = cmd.ExecuteScalar().ToString() == "W"; }
    catch { }
   }
   return blnReturn;
  }

  public static bool IsRestDay(string pShiftCode)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT shmdcode FROM HR.Shift WHERE shftcode='" + pShiftCode + "'";
    cn.Open();
    try { blnReturn = cmd.ExecuteScalar().ToString() == "R"; }
    catch { }
   }
   return blnReturn;
  }

  public static DataTable GetDataTable()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Shift ORDER BY shftcode";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTable(string pOrderBy)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Shift ORDER BY " + pOrderBy;
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTable(string pOrderBy, string pWhere)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.Shift" + pWhere + " ORDER BY " + pOrderBy;
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDdlDS()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT shftcode AS ptext, shftcode AS pvalue FROM HR.Shift ORDER BY shftcode";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static string GetShiftMode(string pShiftCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT shmdcode FROM HR.Shift WHERE shftcode='" + pShiftCode + "'";
    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
   }
   return strReturn;
  }

  public static string GetDayShiftCode(string pUsername, DateTime pDate)
  {
   string strReturn = "";
   //string strScheduleCode = clsEmployeeSchedule.GetEmployeeScheduleCode(pUsername, pDate);
   string strScheduleCode = clsEmployee.GetScheduleCurrent(pUsername, pDate);
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    switch (pDate.DayOfWeek)
    {
     case DayOfWeek.Sunday:
      cmd.CommandText = "SELECT sunshift AS shftcode FROM HR.Schedule WHERE schdcode='" + strScheduleCode + "'";
      break;
     case DayOfWeek.Monday:
      cmd.CommandText = "SELECT monshift AS shftcode FROM HR.Schedule WHERE schdcode='" + strScheduleCode + "'";
      break;
     case DayOfWeek.Tuesday:
      cmd.CommandText = "SELECT tueshift AS shftcode FROM HR.Schedule WHERE schdcode='" + strScheduleCode + "'";
      break;
     case DayOfWeek.Wednesday:
      cmd.CommandText = "SELECT wedshift AS shftcode FROM HR.Schedule WHERE schdcode='" + strScheduleCode + "'";
      break;
     case DayOfWeek.Thursday:
      cmd.CommandText = "SELECT thushift AS shftcode FROM HR.Schedule WHERE schdcode='" + strScheduleCode + "'";
      break;
     case DayOfWeek.Friday:
      cmd.CommandText = "SELECT frishift AS shftcode FROM HR.Schedule WHERE schdcode='" + strScheduleCode + "'";
      break;
     case DayOfWeek.Saturday:
      cmd.CommandText = "SELECT satshift AS shftcode FROM HR.Schedule WHERE schdcode='" + strScheduleCode + "'";
      break;
    }
    cn.Open();
    try { strReturn = cmd.ExecuteScalar().ToString(); }
    catch { }
   }
   return strReturn;
  }

  public static string RestDay { get { return "RESTDAYS"; } }

 }
}
