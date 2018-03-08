using System;

public static class clsValidator
{
 public static short CheckShort(string pEntry)
 {
  short intReturn;
  try { intReturn = short.Parse(pEntry); }
  catch { intReturn = 0; }
  return intReturn;
 }

 public static int CheckInteger(string pEntry)
 {
  int intReturn;
  try { intReturn = int.Parse(pEntry); }
  catch { intReturn = 0; }
  return intReturn;
 }

 public static float CheckFloat(string pEntry)
 {
  float fltReturn;
  try { fltReturn = float.Parse(pEntry); }
  catch { fltReturn = 0; }
  return fltReturn;
 }

 public static double CheckDouble(string pEntry)
 {
  double dblReturn;
  if (Convert.IsDBNull(pEntry) || pEntry == "")
   dblReturn = 0;
  else
   dblReturn = Convert.ToDouble(pEntry);
  return dblReturn;
 }

 public static DateTime CheckDate(string pDateTime)
 {
  if (Convert.IsDBNull(pDateTime) || pDateTime == "")
   return clsDateTime.SystemMinDate;
  else
   return Convert.ToDateTime(pDateTime);
 }

 public static DateTime CheckDate(int pYear, int pMonth, int pDay)
 {
  try { return new DateTime(pYear, pMonth, pDay); }
  catch { return clsDateTime.SystemMinDate; }
 }

 public static DateTime CheckDate(int pYear, int pMonth, int pDay, int pHour, int pMinute, string pTimePeriod)
 {
  try { return new DateTime(pYear, pMonth, pDay, (pTimePeriod == "AM" ? pHour : pHour + 12), pMinute, 0); }
  catch { return clsDateTime.SystemMinDate; }
 }

 public static string ZeroToDash(short pEntry)
 {
  if (pEntry == 0)
   return "-";
  else
   return pEntry.ToString("#,###,###");
 }

 public static string ZeroToDash(int pEntry)
 {
  if (pEntry == 0)
   return "-";
  else
   return pEntry.ToString("#,###,###");
 }

 public static string ZeroToDash(double pEntry)
 {
  if (pEntry == 0)
   return "-";
  else
   return pEntry.ToString("#,###,##0.00");
 }

 public static string ZeroToDash(float pEntry)
 {
  if (pEntry == 0)
   return "-";
  else
   return pEntry.ToString("#,###,##0.00");
 }

 public static string CheckMinDate(DateTime pDate)
 {
  if (pDate == DateTime.MinValue)
   return "";
  else
   return pDate.ToString("MMMM dd, yyyy");
 }

 public static string CheckMinDateTime(DateTime pDateTime)
 {
  if (pDateTime == DateTime.MinValue)
   return "";
  else
   return pDateTime.ToString("MMMM dd, yyyy hh:mm tt");
 }

}
