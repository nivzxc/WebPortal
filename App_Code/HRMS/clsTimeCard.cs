using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{

 public class clsTimeCard : IDisposable
 {

  private string _strUsername;
  private DateTime _dteFocusDate;
  private DateTime _dteKeyIn;
  private DateTime _dteKeyOut;
  private string _strRemarks;
  private string _strUpdateBy;
  private DateTime _dteUpdateOn;

  public clsTimeCard() { }

  public string Username { get { return _strUsername; } set { _strUsername = value; } }
  public DateTime FocusDate { get { return _dteFocusDate; } set { _dteFocusDate = value; } }
  public DateTime KeyIn { get { return _dteKeyIn; } set { _dteKeyIn = value; } }
  public DateTime KeyOut { get { return _dteKeyOut; } set { _dteKeyOut = value; } }
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }
  public string UpdateBy { get { return _strUpdateBy; } set { _strUpdateBy = value; } }
  public DateTime UpdateOn { get { return _dteUpdateOn; } set { _dteUpdateOn = value; } }

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.TimeCard WHERE username='" + _strUsername + "' AND focsdate='" + _dteFocusDate + "' AND keyin='" + _dteKeyIn + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     _dteKeyOut = clsValidator.CheckDate(dr["keyout"].ToString());
     _strRemarks = dr["remarks"].ToString();
     _strUpdateBy = dr["updateby"].ToString();
     _dteUpdateOn = clsValidator.CheckDate(dr["updateon"].ToString());
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
    cmd.CommandText = "INSERT INTO HR.TimeCard VALUES(@username,@focsdate,@keyin,@keyout,@remarks,@updateby,@updateon)";
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@focsdate", clsDateTime.GetDateOnly(_dteFocusDate)));
    cmd.Parameters.Add(new SqlParameter("@keyin", _dteKeyIn));
    cmd.Parameters.Add(new SqlParameter("@keyout", _dteKeyOut));
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
    cmd.Parameters.Add(new SqlParameter("@updateby", _strUpdateBy));
    cmd.Parameters.Add(new SqlParameter("@updateon", _dteUpdateOn));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }


  /// <summary>
  /// Update the timecard
  /// </summary>
  /// <param name="pKeyIn">Old Key In</param>
  /// <param name="pKeyOut">Old Key Out</param>
  /// <returns></returns>
  public int Edit(DateTime pKeyIn)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE HR.TimeCard SET focsdate=@focsdate, keyin=@keyin, keyout=@keyout, remarks=@remarks, updateby=@updateby, updateon=@updateon WHERE username=@username AND focsdate=@focsdate AND keyin=@pkeyin";
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@focsdate", clsDateTime.GetDateOnly(_dteFocusDate)));
    cmd.Parameters.Add(new SqlParameter("@keyin", _dteKeyIn));
    cmd.Parameters.Add(new SqlParameter("@keyout", _dteKeyOut));
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
    cmd.Parameters.Add(new SqlParameter("@updateby", _strUpdateBy));
    cmd.Parameters.Add(new SqlParameter("@updateon", _dteUpdateOn));
    cmd.Parameters.Add(new SqlParameter("@pkeyin", pKeyIn));
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
    cmd.CommandText = "DELETE FROM HR.TimeCard WHERE username=@username AND focsdate=@focsdate AND keyin=@keyin";
    cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
    cmd.Parameters.Add(new SqlParameter("@focsdate", _dteFocusDate));
    cmd.Parameters.Add(new SqlParameter("@keyin", _dteKeyIn));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
   }
   return intReturn;
  }

  public void Dispose() { GC.SuppressFinalize(this); }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DateTime GetIn(string pUsername, DateTime pFocusDate)
  {
   DateTime dteReturn = clsDateTime.SystemMinDate;
   string strEmployeeNumber = clsEmployee.GetEmployeeNumber(pUsername);
   //using (SqlConnection cn = new SqlConnection(clsHrms.ACMConnectionString))
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 ttime FROM ACM.TimeCard WHERE tdate='" + pFocusDate + "' AND staffid='" + strEmployeeNumber + "' ORDER BY ttime";
  //  cmd.CommandText = " SELECT TOP 1 keyin FROM HR.TimeCard WHERE focsdate='" + pFocusDate + "' AND username='" + pUsername + "' ORDER BY keyin ";
       cn.Open();
    try { dteReturn = clsValidator.CheckDate(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return dteReturn;
  }

  public static DateTime GetOut(string pUsername, DateTime pFocusDate)
  {
   DateTime dteReturn = clsDateTime.SystemMinDate;
   string strEmployeeNumber = clsEmployee.GetEmployeeNumber(pUsername);
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
 //  using (SqlConnection cn = new SqlConnection(clsHrms.ACMConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1 ttime FROM ACM.TimeCard WHERE tdate='" + pFocusDate + "' AND staffid='" + strEmployeeNumber + "' ORDER BY ttime DESC";
   // cmd.CommandText = " SELECT TOP 1 keyout FROM HR.TimeCard WHERE focsdate='" + pFocusDate + "' AND username='" + pUsername + "' ORDER BY keyout DESC ";
    cn.Open();
    try { dteReturn = clsValidator.CheckDate(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return dteReturn;
  }

  //public static DateTime GetExactIn(string pUsername, DateTime pFocusDate)
  //{
  // DateTime dteReturn = clsDateTime.SystemMinDate;
  // using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
  // {
  //  SqlCommand cmd = cn.CreateCommand();
  //  cmd.CommandText = "SELECT TOP 1 keyin FROM HR.TimeCard WHERE username='" + pUsername + "' AND ((keyin BETWEEN '" + clsDateTime.ChangeTimeToStart(pFocusDate) + "' AND '" + clsDateTime.ChangeTimeToEnd(pFocusDate) + "') OR (keyout BETWEEN '" + clsDateTime.ChangeTimeToStart(pFocusDate) + "' AND '" + clsDateTime.ChangeTimeToEnd(pFocusDate) + "')) ORDER BY keyin";
  //  cn.Open();
  //  try { dteReturn = DateTime.Parse(cmd.ExecuteScalar().ToString()); }
  //  catch { }
  // }
  // return dteReturn;
  //}

  //public static DateTime GetExactOut(string pUsername, DateTime pFocusDate)
  //{
  // DateTime dteReturn = clsDateTime.SystemMinDate;
  // using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
  // {
  //  SqlCommand cmd = cn.CreateCommand();
  //  cmd.CommandText = "SELECT TOP 1 keyout FROM HR.TimeCard WHERE username='" + pUsername + "' AND ((keyin BETWEEN '" + clsDateTime.ChangeTimeToStart(pFocusDate) + "' AND '" + clsDateTime.ChangeTimeToEnd(pFocusDate) + "') OR (keyout BETWEEN '" + clsDateTime.ChangeTimeToStart(pFocusDate) + "' AND '" + clsDateTime.ChangeTimeToEnd(pFocusDate) + "')) ORDER BY keyout DESC";
  //  cn.Open();
  //  try { dteReturn = DateTime.Parse(cmd.ExecuteScalar().ToString()); }
  //  catch { }
  // }
  // return dteReturn;
  //}

  public static DataTable GetTimeCards(string pUsername, DateTime pFocusDate)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.TimeCard WHERE username='" + pUsername + "' AND focsdate='" + pFocusDate + "' ORDER BY username";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetTimeCardsList(string pWhereClause, string pOrderByClause)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT HR.TimeCard.username, HR.TimeCard.focsdate, HR.TimeCard.keyin, HR.TimeCard.keyout, HR.TimeCard.updateby, HR.Employees.firname, HR.Employees.lastname FROM HR.TimeCard INNER JOIN HR.Employees ON HR.TimeCard.username = HR.Employees.username " + pWhereClause + "ORDER BY " + pOrderByClause;
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetTimeCards(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.TimeCard WHERE username='" + pUsername + "' ORDER BY focsdate DESC";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static DataTable GetDataTable()
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM HR.TimeCard ORDER BY username,focsdate,keyin";
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

  public static bool HasTimeCardEntry(DateTime pFocusDate, string pUsername)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT focsdate FROM HR.TimeCard WHERE focsdate='" + pFocusDate + "' AND username='" + pUsername + "'";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static DataTable GetEmloyeeMonthlyTimeCard(string pUsername, DateTime pDateStart, DateTime pDateEnd)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("ImageID", System.Type.GetType("System.String"));
   tblReturn.Columns.Add("DateApp", System.Type.GetType("System.String"));
   tblReturn.Columns.Add("DateIn", System.Type.GetType("System.String"));
   tblReturn.Columns.Add("DateOut", System.Type.GetType("System.String"));

   DataTable tblHolidays = new DataTable();
   tblHolidays = clsHoliday.GetHoliday(pDateStart, pDateEnd);

   DataRow drow;
   DateTime dteTemp;
   DateTime dteTempIn;
   DateTime dteTempOut;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    for (dteTemp = clsDateTime.GetDateOnly(pDateStart); dteTemp <= pDateEnd; dteTemp = dteTemp.AddDays(1))
    {
     drow = tblReturn.NewRow();
     drow["ImageID"] = "";
     drow["DateApp"] = dteTemp.ToString("ddd MMM dd");

     dteTempIn = clsTimeCard.GetIn(pUsername, dteTemp);
     if (dteTempIn == clsDateTime.SystemMinDate)
      drow["DateIn"] = "-";
     else
      drow["DateIn"] = dteTempIn.ToString("hh:mm tt");

     dteTempOut = clsTimeCard.GetOut(pUsername, dteTemp);
     if (dteTempOut == clsDateTime.SystemMinDate)
      drow["DateOut"] = "-";
     else
      drow["DateOut"] = dteTempOut.ToString("hh:mm tt");

     if (dteTempIn == clsDateTime.SystemMinDate)
     {
      foreach (DataRow drHolidays in tblHolidays.Rows)
      {
       if (Convert.ToDateTime(drHolidays["dateapp"].ToString()).ToString("yyyy-MM-dd") == dteTemp.ToString("yyyy-MM-dd"))
       {
        drow["DateIn"] = "Holiday";
        drow["DateOut"] = "-";
        break;
       }
      }
     }

     tblReturn.Rows.Add(drow);
    }
   }
   return tblReturn;
  }

 }

}