using System;
using System.Data;
using System.Data.SqlTypes;

public enum pDateFormat { Year, Month, Day, Hour, Minute, Second }

public static class clsDateTime
{
 public static DateTime SystemMinDate {  get { return SqlDateTime.MinValue.Value; } }

 public static DateTime ChangeTimeToStart(DateTime pDate)
 {
  return new DateTime(pDate.Year, pDate.Month, pDate.Day, 0, 0, 0);
 }

 public static DateTime ChangeTimeToEnd(DateTime pDate)
 {
  return new DateTime(pDate.Year, pDate.Month, pDate.Day, 23, 59, 59);
 }

 public static DateTime GetDateOnly(DateTime pDateTime)
 {
  return new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day, 0, 0, 0);
 }

 public static DateTime GetFirstDayOfMonth(DateTime pDate)
 {
  DateTime dteReturn = new DateTime(pDate.Year, pDate.Month, 1);
  return dteReturn;
 }

 public static DateTime GetFirstDayOfMonth(int pMonth)
 {
  DateTime dteReturn = new DateTime(DateTime.Now.Year, pMonth, 1);
  return dteReturn;
 }

 public static DateTime GetLastDayOfMonth(DateTime pDate)
 {
  DateTime dteReturn = new DateTime(pDate.Year, pDate.Month, 1);
  return dteReturn.AddMonths(1).AddDays(-1);
 }

 public static DateTime GetLastDayOfMonth(int pMonth)
 {
  DateTime dteReturn = new DateTime(DateTime.Now.Year, pMonth, 1);
  return dteReturn.AddMonths(1).AddDays(-1);
 }

 public static DateTime AddDaysWorking(int intDays)
 {
  DateTime dteReturn = DateTime.Today;
  int intCtr = 0;
  while (intCtr < intDays)
  {
   dteReturn = dteReturn.AddDays(1);
   if (dteReturn.DayOfWeek != DayOfWeek.Saturday && dteReturn.DayOfWeek != DayOfWeek.Sunday)
    intCtr++;
  }
  return dteReturn;
 }

 public static DateTime AddDaysWorking(int intDays, DateTime dteDateStart)
 {
  DateTime dteReturn = dteDateStart;
  int intCtr = 0;
  while (intCtr < intDays)
  {
   dteReturn = dteReturn.AddDays(1);
   if (dteReturn.DayOfWeek != DayOfWeek.Saturday && dteReturn.DayOfWeek != DayOfWeek.Sunday)
    intCtr++;
  }
  return dteReturn;
 }

 public static DateTime CombineDateTime(DateTime pDate, DateTime pTime)
 {
  return new DateTime(pDate.Year, pDate.Month, pDate.Day, pTime.Hour, pTime.Minute, pTime.Second);
 }

 public static DateTime CombineDateTime(DateTime pDate, string pTime)
 {
  DateTime dteDate = clsValidator.CheckDate(pDate.ToString("MM/dd/yyyy") + " " + pTime);
  return new DateTime(dteDate.Year, dteDate.Month, dteDate.Day, dteDate.Hour, dteDate.Minute, dteDate.Second);
 }

 public static float DateDiff(pDateFormat pDateFormat, DateTime pDateTime1, DateTime pDateTime2)
 {
  float fltReturn = 0;
  float fltDays = (float)(pDateTime2 - pDateTime1).Days;
  float fltHours = (float)(pDateTime2 - pDateTime1).Hours;
  float fltMinutes = (float)(pDateTime2 - pDateTime1).Minutes;
  if (pDateFormat == pDateFormat.Hour)
  {
   fltMinutes = fltMinutes / 60;
   fltReturn = (float)Math.Round((fltDays * 24) + fltHours + fltMinutes, 2);
  }
  else if (pDateFormat == pDateFormat.Minute)
  {
   fltReturn = fltMinutes;
  }
  return fltReturn;
 }

 public static DateTime RemoveSeconds(DateTime pDateTime)
 {
  return new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day, pDateTime.Hour, pDateTime.Minute, 0);
 }

 public static string CheckMinDate(DateTime pDateTime)
 {
  if (pDateTime == SystemMinDate)
   return "";
  else
   return pDateTime.ToString("MMM dd, yyyy hh:mm tt");
 }

 public static DataTable GetMonths()
 {
  DataTable tblReturn = new DataTable();
  tblReturn.Columns.Add("pvalue");
  tblReturn.Columns.Add("ptext");

  DataRow drw = tblReturn.NewRow();
  drw["pvalue"] = "1";
  drw["ptext"] = "Jan";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "2";
  drw["ptext"] = "Feb";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "3";
  drw["ptext"] = "Mar";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "4";
  drw["ptext"] = "Apr";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "5";
  drw["ptext"] = "May";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "6";
  drw["ptext"] = "Jun";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "7";
  drw["ptext"] = "Jul";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "8";
  drw["ptext"] = "Aug";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "9";
  drw["ptext"] = "Sep";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "10";
  drw["ptext"] = "Oct";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "11";
  drw["ptext"] = "Nov";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "12";
  drw["ptext"] = "Dec";
  tblReturn.Rows.Add(drw);

  return tblReturn;
 }

 public static DataTable GetDays()
 {
  DataTable tblReturn = new DataTable();
  tblReturn.Columns.Add("pvalue");
  tblReturn.Columns.Add("ptext");
  for (int i = 1; i <= 31; i++)
  {
   DataRow drw = tblReturn.NewRow();
   drw["ptext"] = ("0" + i.ToString()).Substring(("0" + i.ToString()).Length - 2);
   drw["pvalue"] = i;
   tblReturn.Rows.Add(drw);
  }
  return tblReturn;
 }

 public static DataTable GetHours()
 {
  DataTable tblReturn = new DataTable();
  tblReturn.Columns.Add("pvalue");
  tblReturn.Columns.Add("ptext");


  DataRow drw = tblReturn.NewRow();
  drw["pvalue"] = "01";
  drw["ptext"] = "01";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "02";
  drw["ptext"] = "02";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "03";
  drw["ptext"] = "03";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "04";
  drw["ptext"] = "04";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "05";
  drw["ptext"] = "05";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "06";
  drw["ptext"] = "06";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "07";
  drw["ptext"] = "07";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "08";
  drw["ptext"] = "08";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "09";
  drw["ptext"] = "09";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "10";
  drw["ptext"] = "10";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "11";
  drw["ptext"] = "11";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "00";
  drw["ptext"] = "12";
  tblReturn.Rows.Add(drw);

  return tblReturn;
 }

 public static DataTable GetMinutes()
 {
  DataTable tblReturn = new DataTable();
  tblReturn.Columns.Add("pvalue");
  tblReturn.Columns.Add("ptext");
  for (int i = 0; i <= 59; i++)
  {
   DataRow drw = tblReturn.NewRow();
   drw["ptext"] = ("0" + i.ToString()).Substring(("0" + i.ToString()).Length - 2);
   drw["pvalue"] = i;
   tblReturn.Rows.Add(drw);
  }
  return tblReturn;
 }

 public static DataTable GetTimePeriod()
 {
  DataTable tblReturn = new DataTable();
  tblReturn.Columns.Add("pvalue");
  tblReturn.Columns.Add("ptext");

  DataRow drw = tblReturn.NewRow();
  drw["pvalue"] = "AM";
  drw["ptext"] = "AM";
  tblReturn.Rows.Add(drw);

  drw = tblReturn.NewRow();
  drw["pvalue"] = "PM";
  drw["ptext"] = "PM";
  tblReturn.Rows.Add(drw);

  return tblReturn;
 }

 public static DateTime GetMonthFirstWorkingDay(DateTime pDateTime)
 {
  DateTime dteReturn = new DateTime(pDateTime.Year, pDateTime.Month, 1, 0, 0, 0);
  while (dteReturn.DayOfWeek != DayOfWeek.Monday)
  {
   dteReturn = dteReturn.AddDays(1);
  }
  return dteReturn;
 }

}