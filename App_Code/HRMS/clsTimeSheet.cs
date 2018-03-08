using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;

namespace HRMS
{

   public class clsTimesheet : IDisposable
   {
      private const int RoundDecimal = 2;
      private static DateTime NightDifferentialStart = new DateTime(2006, 2, 9, 22, 00, 00);
      private static DateTime NightDifferentialEnd = new DateTime(2006, 2, 9, 6, 00, 00);

      private string _strUsername;
      private DateTime _dteFocusDate;
      private DateTime _dteTimeIn;
      private DateTime _dteTimeOut;
      private string _strShiftCode;
      private DateTime _dteShiftIn;
      private DateTime _dteShiftOut;
      private DateTime _dteOverIn;
      private DateTime _dteOverOut;
      private float _fltTotalUnit;
      private float _fltWorkUnit;
      private float _fltAbsentUnit;
      private float _fltLeaveWithPay;
      private float _fltLeaveWithoutPay;
      private float _fltLateUnit;
      private float _fltUndertimeUnit;
      private float _fltOBUnit;
      private float _fltOTUnit;
      private float _fltExcessUnit;
      private float _fltRegularOT;
      private float _fltRegularND;
      private float _fltRestDayOT;
      private float _fltRestDayND;
      private float _fltRestDayEX;
      private float _fltSpecialHolidayOT;
      private float _fltSpecialHolidayND;
      private float _fltSpecialHolidayEX;
      private float _fltRegularHolidayOT;
      private float _fltRegularHolidayND;
      private float _fltRegularHolidayEX;
      private float _fltRestDaySpecialHolidayOT;
      private float _fltRestDaySpecialHolidayND;
      private float _fltRestDaySpecialHolidayEX;
      private float _fltRestDayRegularHolidayOT;
      private float _fltRestDayRegularHolidayND;
      private float _fltRestDayRegularHolidayEX;
      private DateTime _dteLastUpdateDate;
      private string _strLastUpdateBy;
      private string _strStatus;

      public clsTimesheet() { }

      public string Username { get { return _strUsername; } set { _strUsername = value; } }
      public DateTime FocusDate { get { return _dteFocusDate; } set { _dteFocusDate = value; } }
      public DateTime TimeIn { get { return _dteTimeIn; } set { _dteTimeIn = value; } }
      public DateTime TimeOut { get { return _dteTimeOut; } set { _dteTimeOut = value; } }
      public string ShiftCode { get { return _strShiftCode; } set { _strShiftCode = value; } }
      public DateTime ShiftIn { get { return _dteShiftIn; } set { _dteShiftIn = value; } }
      public DateTime ShiftOut { get { return _dteShiftOut; } set { _dteShiftOut = value; } }
      public DateTime OverIn { get { return _dteOverIn; } set { _dteOverIn = value; } }
      public DateTime OverOut { get { return _dteOverOut; } set { _dteOverOut = value; } }
      public float TotalUnit { get { return _fltTotalUnit; } set { _fltTotalUnit = value; } }
      public float WorkUnit { get { return _fltWorkUnit; } set { _fltWorkUnit = value; } }
      public float AbsentUnit { get { return _fltAbsentUnit; } set { _fltAbsentUnit = value; } }
      public float LeaveWithPay { get { return _fltLeaveWithPay; } set { _fltLeaveWithPay = value; } }
      public float LeaveWithoutPay { get { return _fltLeaveWithoutPay; } set { _fltLeaveWithoutPay = value; } }
      public float LateUnit { get { return _fltLateUnit; } set { _fltLateUnit = value; } }
      public float UndertimeUnit { get { return _fltUndertimeUnit; } set { _fltUndertimeUnit = value; } }
      public float OBUnit { get { return _fltOBUnit; } set { _fltOBUnit = value; } }
      public float OTUnit { get { return _fltOTUnit; } set { _fltOTUnit = value; } }
      public float ExcessUnit { get { return _fltExcessUnit; } set { _fltExcessUnit = value; } }
      public float RegularOT { get { return _fltRegularOT; } set { _fltRegularOT = value; } }
      public float RegularND { get { return _fltRegularND; } set { _fltRegularND = value; } }
      public float RestDayOT { get { return _fltRestDayOT; } set { _fltRestDayOT = value; } }
      public float RestDayND { get { return _fltRestDayND; } set { _fltRestDayND = value; } }
      public float RestDayEX { get { return _fltRestDayEX; } set { _fltRestDayEX = value; } }
      public float SpecialHolidayOT { get { return _fltSpecialHolidayOT; } set { _fltSpecialHolidayOT = value; } }
      public float SpecialHolidayND { get { return _fltSpecialHolidayND; } set { _fltSpecialHolidayND = value; } }
      public float SpecialHolidayEX { get { return _fltSpecialHolidayEX; } set { _fltSpecialHolidayEX = value; } }
      public float RegularHolidayOT { get { return _fltRegularHolidayOT; } set { _fltRegularHolidayOT = value; } }
      public float RegularHolidayND { get { return _fltRegularHolidayND; } set { _fltRegularHolidayND = value; } }
      public float RegularHolidayEX { get { return _fltRegularHolidayEX; } set { _fltRegularHolidayEX = value; } }
      public float RestDaySpecialHolidayOT { get { return _fltRestDaySpecialHolidayOT; } set { _fltRestDaySpecialHolidayOT = value; } }
      public float RestDaySpecialHolidayND { get { return _fltRestDaySpecialHolidayND; } set { _fltRestDaySpecialHolidayND = value; } }
      public float RestDaySpecialHolidayEX { get { return _fltRestDaySpecialHolidayEX; } set { _fltRestDaySpecialHolidayEX = value; } }
      public float RestDayRegularHolidayOT { get { return _fltRestDayRegularHolidayOT; } set { _fltRestDayRegularHolidayOT = value; } }
      public float RestDayRegularHolidayND { get { return _fltRestDayRegularHolidayND; } set { _fltRestDayRegularHolidayND = value; } }
      public float RestDayRegularHolidayEX { get { return _fltRestDayRegularHolidayEX; } set { _fltRestDayRegularHolidayEX = value; } }
      public string LastUpdateBy { get { return _strLastUpdateBy; } set { _strLastUpdateBy = value; } }
      public DateTime LastUpdateDate { get { return _dteLastUpdateDate; } set { _dteLastUpdateDate = value; } }
      public string Status { get { return _strStatus; } set { _strStatus = value; } }

      public void Dispose() { GC.SuppressFinalize(this); }

      ////////////////////////////////////
      ////////// Static Members //////////
      ////////////////////////////////////

      public static float CountAbsentSum(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(absunit) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND absunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static float CountAbsentSumDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(absunit) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND absunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static float CountAbsentSumNoApplication(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         DataTable tblAbsent = new DataTable();
         DataTable tblApplications = clsLeave.DSGApplications(pUsername, pDateStart, pDateEnd);
         bool blnHasApplication = false;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT focsdate,absunit FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND absunit > 0";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblAbsent);
         }

         foreach (DataRow drwAbs in tblAbsent.Rows)
         {
            blnHasApplication = false;
            DateTime dteFocusDate = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwAbs["focsdate"].ToString()));
            float fltAbsentUnit = clsValidator.CheckFloat(drwAbs["absunit"].ToString());
            foreach (DataRow drwApp in tblApplications.Rows)
            {
               DateTime dteDateStart = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwApp["datestrt"].ToString()));
               DateTime dteDateEnd = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwApp["dateend"].ToString()));
               blnHasApplication = (dteDateStart <= dteFocusDate && dteDateEnd >= dteFocusDate);
            }
            if (!blnHasApplication)
               fltReturn += fltAbsentUnit;
         }
         return fltReturn;
      }

      public static int CountAbsentTotal(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND absunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountAbsentTotalDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND absunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountAbsentTotalNoApplication(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         DataTable tblAbsent = new DataTable();
         DataTable tblApplications = clsLeave.DSGApplications(pUsername, pDateStart, pDateEnd);
         bool blnHasApplication = false;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT focsdate FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND absunit > 0";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblAbsent);
         }

         foreach (DataRow drwAbs in tblAbsent.Rows)
         {
            blnHasApplication = false;
            DateTime dteFocusDate = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwAbs["focsdate"].ToString()));
            foreach (DataRow drwApp in tblApplications.Rows)
            {
               DateTime dteDateStart = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwApp["datestrt"].ToString()));
               DateTime dteDateEnd = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwApp["dateend"].ToString()));

               if (dteDateStart <= dteFocusDate && dteDateEnd >= dteFocusDate)
               {
                  blnHasApplication = true;
                  break;
               }
            }
            if (!blnHasApplication)
               intReturn += 1;
         }
         return intReturn;
      }

      public static float CountLeaveWithPaySum(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lwithpay) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwithpay > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      // Added by Ian
      // 05022011
      // Work Around for CDL
      public static float CountLeaveWithPaySumCDL(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lwithpay) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND focsdate NOT IN (SELECT dateapp FROM HR.CDL) AND lwithpay > 0 ";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static float CountLeaveWithPaySumDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lwithpay) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwithpay > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static int CountLeaveWithPayTotal(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwithpay > 0 ";
            //cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND ttalunit > 1";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      // Added by Ian
      // 05022011
      // Work Around for CDL
      public static int CountLeaveWithPayTotalCDL(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND focsdate NOT IN (SELECT dateapp FROM HR.CDL) AND lwithpay > 0 ";

            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountLeaveWithPayTotalDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwithpay > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static float CountLeaveWithOutPaySum(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lwoutpay) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwoutpay > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      // Added by Ian
      // 05022011
      // Work Around for CDL
      public static float CountLeaveWithOutPaySumCDL(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lwoutpay) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND focsdate NOT IN (SELECT dateapp FROM HR.CDL) AND lwoutpay > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static float CountLeaveWithOutPaySumDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lwoutpay) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwoutpay > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static int CountLeaveWithOutPayTotal(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwoutpay > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      // Added by Ian
      // 05022011
      // Work Around for CDL
      public static int CountLeaveWithOutPayTotalCDL(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND focsdate NOT IN (SELECT dateapp FROM HR.CDL) AND lwithpay > 0 ";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountLeaveWithOutPayTotalDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lwoutpay > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static float CountTardinessSum(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lateunit) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lateunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()) * 60; }
            catch { }
         }
         return fltReturn;
      }

      public static float CountTardinessSumDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(lateunit) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lateunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()) * 60; }
            catch { }
         }
         return fltReturn;
      }

      public static int CountTardinessTotal(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lateunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountTardinessTotalDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND lateunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static float CountWorkHour(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(workunit) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND workunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static float CountWorkHourDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(workunit) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND workunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return fltReturn;
      }

      public static int CountWorkDay(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND workunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountWorkDayDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND workunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static float CountUndertimeSum(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(undrunit) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND undrunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()) * 60; }
            catch { }
         }
         return fltReturn;
      }

      public static float CountUndertimeSumDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         float fltReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT SUM(undrunit) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND undrunit > 0";
            cn.Open();
            try { fltReturn = clsValidator.CheckFloat(cmd.ExecuteScalar().ToString()) * 60; }
            catch { }
         }
         return fltReturn;
      }

      public static int CountUndertimeTotal(string pUsername, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND undrunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountUndertimeTotalDivision(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Timesheet WHERE username IN (SELECT username FROM HR.Employees WHERE divicode='" + pDivisionCode + "' AND pstatus='1' AND esttcode IN ('RE','PR')) AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "') AND undrunit > 0";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static DataTable DSCDTRSummary(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("xvalue");
         tblReturn.Columns.Add("yvalue");

         DataRow drw = tblReturn.NewRow();
         drw["xvalue"] = "Tardiness";
         drw["yvalue"] = clsTimesheet.CountTardinessTotalDivision(pDivisionCode, pDateStart, pDateEnd);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "Undertime";
         drw["yvalue"] = clsTimesheet.CountUndertimeTotalDivision(pDivisionCode, pDateStart, pDateEnd);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "Absent";
         drw["yvalue"] = clsTimesheet.CountAbsentTotalDivision(pDivisionCode, pDateStart, pDateEnd);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "LWP";
         drw["yvalue"] = clsTimesheet.CountLeaveWithPayTotalDivision(pDivisionCode, pDateStart, pDateEnd);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "LWOP";
         drw["yvalue"] = clsTimesheet.CountLeaveWithOutPayTotalDivision(pDivisionCode, pDateStart, pDateEnd);
         tblReturn.Rows.Add(drw);

         return tblReturn;
      }

      public static DataTable DSCDTRSummaryPercentage(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("xvalue");
         tblReturn.Columns.Add("yvalue");

         int intDivisionCount = clsDivision.CountEmployeesTimesheet(pDivisionCode);

         DataRow drw = tblReturn.NewRow();
         drw["xvalue"] = "Tardiness";
         drw["yvalue"] = Math.Round((clsValidator.CheckFloat(clsTimesheet.CountTardinessTotalDivision(pDivisionCode, pDateStart, pDateEnd).ToString()) / clsValidator.CheckFloat(intDivisionCount.ToString())) * 100, 2);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "Undertime";
         drw["yvalue"] = Math.Round((clsValidator.CheckFloat(clsTimesheet.CountUndertimeTotalDivision(pDivisionCode, pDateStart, pDateEnd).ToString()) / clsValidator.CheckFloat(intDivisionCount.ToString())) * 100, 2);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "Absent";
         drw["yvalue"] = Math.Round((clsValidator.CheckFloat(clsTimesheet.CountAbsentTotalDivision(pDivisionCode, pDateStart, pDateEnd).ToString()) / clsValidator.CheckFloat(intDivisionCount.ToString())) * 100, 2);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "LWP";
         drw["yvalue"] = Math.Round((clsValidator.CheckFloat(clsTimesheet.CountLeaveWithPayTotalDivision(pDivisionCode, pDateStart, pDateEnd).ToString()) / clsValidator.CheckFloat(intDivisionCount.ToString())) * 100, 2);
         tblReturn.Rows.Add(drw);

         drw = tblReturn.NewRow();
         drw["xvalue"] = "LWOP";
         drw["yvalue"] = Math.Round((clsValidator.CheckFloat(clsTimesheet.CountLeaveWithOutPayTotalDivision(pDivisionCode, pDateStart, pDateEnd).ToString()) / clsValidator.CheckFloat(intDivisionCount.ToString())) * 100, 2);
         tblReturn.Rows.Add(drw);

         return tblReturn;
      }

      public static DataTable DSGARPerfectAttendance(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("NickName");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("Division");
         tblReturn.Columns.Add("TardinessCount");
         tblReturn.Columns.Add("TardinessMinute");
         tblReturn.Columns.Add("UndertimeCount");
         tblReturn.Columns.Add("UndertimeMinute");
         tblReturn.Columns.Add("AbsentCount");
         tblReturn.Columns.Add("AbsentDay");
         tblReturn.Columns.Add("LWPCount");
         tblReturn.Columns.Add("LWPDay");
         tblReturn.Columns.Add("LWOPCount");
         tblReturn.Columns.Add("LWOPDay");
         tblReturn.Columns.Add("TotalWorkDay");
         tblReturn.Columns.Add("TotalWorkHour");

         DateTime dteDateStart = DateTime.Now;
         DateTime dteDateEnd = DateTime.Now;
         int intTardinessTotal = 0;
         float fltTardinessSum = 0;
         int intUndertimeTotal = 0;
         float fltUndertimeSum = 0;
         int intAbsentTotal = 0;
         float fltAbsentSum = 0;
         int intLWPTotal = 0;
         float fltLWPSum = 0;
         int intLWOPTotal = 0;
         float fltLWOPSum = 0;
         int intWorkDay = 0;
         float fltWorkHour = 0;

         DataTable tblEmployee = clsEmployee.DSDTREmployee();
         foreach (DataRow drwEmployee in tblEmployee.Rows)
         {
            dteDateStart = clsValidator.CheckDate(drwEmployee["datestrt"].ToString());
            dteDateEnd = clsValidator.CheckDate(drwEmployee["dateend"].ToString());
            intTardinessTotal = clsTimesheet.CountTardinessTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltTardinessSum = clsTimesheet.CountTardinessSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intUndertimeTotal = clsTimesheet.CountUndertimeTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltUndertimeSum = clsTimesheet.CountUndertimeSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intAbsentTotal = clsTimesheet.CountAbsentTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltAbsentSum = clsTimesheet.CountAbsentSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intLWPTotal = clsTimesheet.CountLeaveWithPayTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltLWPSum = clsTimesheet.CountLeaveWithPaySum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intLWOPTotal = clsTimesheet.CountLeaveWithOutPayTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltLWOPSum = clsTimesheet.CountLeaveWithOutPaySum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intWorkDay = clsTimesheet.CountWorkDay(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltWorkHour = clsTimesheet.CountWorkHour(drwEmployee["username"].ToString(), pDateStart, pDateEnd);

            if (drwEmployee["username"].ToString() == "noah.duran")
               intWorkDay = intWorkDay;
            if (dteDateStart <= pDateStart & intTardinessTotal == 0 && fltTardinessSum == 0 && intUndertimeTotal == 0 && fltUndertimeSum == 0 && intAbsentTotal == 0 && fltAbsentSum == 0 && intLWPTotal == 0 && fltLWPSum == 0 && intLWOPTotal == 0 && fltLWOPSum == 0 && intWorkDay != 0 && fltWorkHour != 0)
            {
               DataRow drwNew = tblReturn.NewRow();
               drwNew["Username"] = drwEmployee["username"].ToString();
               drwNew["NickName"] = drwEmployee["nickname"].ToString();
               drwNew["EmployeeName"] = drwEmployee["lastname"].ToString() + ", " + drwEmployee["firname"].ToString();
               drwNew["Division"] = drwEmployee["division"].ToString();
               drwNew["TardinessCount"] = clsValidator.ZeroToDash(intTardinessTotal);
               drwNew["TardinessMinute"] = clsValidator.ZeroToDash(fltTardinessSum);
               drwNew["UndertimeCount"] = clsValidator.ZeroToDash(intUndertimeTotal);
               drwNew["UndertimeMinute"] = clsValidator.ZeroToDash(fltUndertimeSum);
               drwNew["AbsentCount"] = clsValidator.ZeroToDash(intAbsentTotal);
               drwNew["AbsentDay"] = clsValidator.ZeroToDash(fltAbsentSum);
               drwNew["LWPCount"] = clsValidator.ZeroToDash(intLWPTotal);
               drwNew["LWPDay"] = clsValidator.ZeroToDash(fltLWPSum);
               drwNew["LWOPCount"] = clsValidator.ZeroToDash(intLWOPTotal);
               drwNew["LWOPDay"] = clsValidator.ZeroToDash(fltLWOPSum);
               drwNew["TotalWorkDay"] = clsValidator.ZeroToDash(intWorkDay);
               drwNew["TotalWorkHour"] = clsValidator.ZeroToDash(fltWorkHour);
               tblReturn.Rows.Add(drwNew);
            }
         }

         return tblReturn;
      }

      public static DataTable DSGARPerfectAttendancePortal(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("NickName");

         DateTime dteDateStart = DateTime.Now;
         DateTime dteDateEnd = DateTime.Now;
         int intTardinessTotal = 0;
         float fltTardinessSum = 0;
         int intUndertimeTotal = 0;
         float fltUndertimeSum = 0;
         int intAbsentTotal = 0;
         float fltAbsentSum = 0;
         int intLWPTotal = 0;
         float fltLWPSum = 0;
         int intLWOPTotal = 0;
         float fltLWOPSum = 0;
         int intWorkDay = 0;
         float fltWorkHour = 0;

         DataTable tblEmployee = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "SELECT username,nickname FROM HR.Employees WHERE username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') ORDER BY nickname";
            cmd.CommandText = "SELECT ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [rn],username,nickname,datestrt,dateend FROM HR.Employees WHERE username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND username NOT IN (SELECT username FROM HR.Offense WHERE enabled='1') AND username NOT IN (SELECT username FROM HR.Leave3Days WHERE enabled='1') ORDER BY rn";
            //cmd.CommandText = "SELECT ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [rn],username,nickname,datestrt,dateend FROM HR.Employees WHERE username='armand.lumibao' AND username NOT IN (SELECT username FROM HR.Offense WHERE enabled='1') AND username NOT IN (SELECT username FROM HR.Leave3Days WHERE enabled='1') ORDER BY rn";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblEmployee);
         }

         foreach (DataRow drwEmployee in tblEmployee.Rows)
         {
            dteDateStart = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwEmployee["datestrt"].ToString()));
            dteDateEnd = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwEmployee["dateend"].ToString()));
            intTardinessTotal = clsTimesheet.CountTardinessTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltTardinessSum = clsTimesheet.CountTardinessSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intUndertimeTotal = clsTimesheet.CountUndertimeTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltUndertimeSum = clsTimesheet.CountUndertimeSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intAbsentTotal = clsTimesheet.CountAbsentTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltAbsentSum = clsTimesheet.CountAbsentSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intLWPTotal = clsTimesheet.CountLeaveWithPayTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltLWPSum = clsTimesheet.CountLeaveWithPaySum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intLWOPTotal = clsTimesheet.CountLeaveWithOutPayTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltLWOPSum = clsTimesheet.CountLeaveWithOutPaySum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intWorkDay = clsTimesheet.CountWorkDay(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltWorkHour = clsTimesheet.CountWorkHour(drwEmployee["username"].ToString(), pDateStart, pDateEnd);

            if (dteDateStart <= pDateStart & intTardinessTotal == 0 && fltTardinessSum == 0 && intUndertimeTotal == 0 && fltUndertimeSum == 0 && intAbsentTotal == 0 && fltAbsentSum == 0 && intLWPTotal == 0 && fltLWPSum == 0 && intLWOPTotal == 0 && fltLWOPSum == 0 && intWorkDay != 0 && fltWorkHour != 0)
            {
               DataRow drwNew = tblReturn.NewRow();
               drwNew["Username"] = drwEmployee["username"].ToString();
               drwNew["NickName"] = drwEmployee["nickname"].ToString();
               tblReturn.Rows.Add(drwNew);
            }
         }

         return tblReturn;
      }

      // Added by IAN as Work Around for CDL
      // 05022011
      public static DataTable DSGARPerfectAttendancePortalCDL(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("NickName");

         DateTime dteDateStart = DateTime.Now;
         DateTime dteDateEnd = DateTime.Now;
         int intTardinessTotal = 0;
         float fltTardinessSum = 0;
         int intUndertimeTotal = 0;
         float fltUndertimeSum = 0;
         int intAbsentTotal = 0;
         float fltAbsentSum = 0;
         int intLWPTotal = 0;
         float fltLWPSum = 0;
         int intLWOPTotal = 0;
         float fltLWOPSum = 0;
         int intWorkDay = 0;
         float fltWorkHour = 0;

         DataTable tblEmployee = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [rn],username,lastname,nickname,datestrt,dateend FROM HR.Employees WHERE username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND username NOT IN (SELECT username FROM HR.Offense WHERE enabled='1') " +
                  " AND username NOT IN (SELECT username FROM HR.Leave3Days WHERE enabled='1') ORDER BY rn";
            //cmd.CommandText = "SELECT ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [rn],username,nickname,datestrt,dateend FROM HR.Employees WHERE username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND username NOT IN (SELECT username FROM HR.Offense WHERE enabled='1') " +
            //       " AND username NOT IN (SELECT username FROM HR.Leave3Days WHERE enabled='1') AND username='otchoi.genoso' ORDER BY rn";
            //remove HR.LeaveCDL to qualify for the month of august as advise by Meliza: Charlie Bachiller 09-06-2011
            //cmd.CommandText = "SELECT ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [rn],username,nickname,datestrt,dateend FROM HR.Employees WHERE username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND username NOT IN (SELECT username FROM HR.Offense WHERE enabled='1') " +
            //               " AND username IN (SELECT username FROM HR.LeaveCDL WHERE enabled='1') ORDER BY rn";
            //cmd.CommandText = "SELECT ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [rn],username,nickname,datestrt,dateend FROM HR.Employees WHERE username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND username NOT IN (SELECT username FROM HR.Offense WHERE enabled='1') " +
            //                 " AND username ='phia.miranda' ORDER BY rn";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblEmployee);
         }

         foreach (DataRow drwEmployee in tblEmployee.Rows)
         {
             
            dteDateStart = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwEmployee["datestrt"].ToString()));
            dteDateEnd = clsDateTime.GetDateOnly(clsValidator.CheckDate(drwEmployee["dateend"].ToString()));
            intTardinessTotal = clsTimesheet.CountTardinessTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltTardinessSum = clsTimesheet.CountTardinessSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intUndertimeTotal = clsTimesheet.CountUndertimeTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltUndertimeSum = clsTimesheet.CountUndertimeSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intAbsentTotal = clsTimesheet.CountAbsentTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltAbsentSum = clsTimesheet.CountAbsentSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intLWPTotal = clsTimesheet.CountLeaveWithPayTotalCDL(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltLWPSum = clsTimesheet.CountLeaveWithPaySumCDL(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intLWOPTotal = clsTimesheet.CountLeaveWithOutPayTotalCDL(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltLWOPSum = clsTimesheet.CountLeaveWithOutPaySumCDL(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            intWorkDay = clsTimesheet.CountWorkDay(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltWorkHour = clsTimesheet.CountWorkHour(drwEmployee["username"].ToString(), pDateStart, pDateEnd);

             //Remove by charlie bachiller 11-08-2011
             //to avoid error Date start of employee (10/3/2011) <= Date start of Month 10-1-2011
            //if (dteDateStart <= pDateStart & intTardinessTotal == 0 && fltTardinessSum == 0 && intUndertimeTotal == 0 && fltUndertimeSum == 0 && intAbsentTotal == 0 && fltAbsentSum == 0 && intLWPTotal == 0 && fltLWPSum == 0 && intLWOPTotal == 0 && fltLWOPSum == 0 && intWorkDay != 0 && fltWorkHour != 0)
            //{
            if (intTardinessTotal == 0 && fltTardinessSum == 0 && intUndertimeTotal == 0 && fltUndertimeSum == 0 && intAbsentTotal == 0 && fltAbsentSum == 0 && intLWPTotal == 0 && fltLWPSum == 0 && intLWOPTotal == 0 && fltLWOPSum == 0 && intWorkDay != 0 && fltWorkHour != 0)
            {
               DataRow drwNew = tblReturn.NewRow();
               drwNew["Username"] = drwEmployee["username"].ToString();
               drwNew["NickName"] = drwEmployee["nickname"].ToString();
               tblReturn.Rows.Add(drwNew);
            }


         }

         return tblReturn;
      }


      public static DataTable DSGARLeaveForApproval(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("LeaveType");
         tblReturn.Columns.Add("DateFiled");
         tblReturn.Columns.Add("DateStart");
         tblReturn.Columns.Add("DateEnd");
         tblReturn.Columns.Add("Unit");
         tblReturn.Columns.Add("Reason");
         tblReturn.Columns.Add("Approver");

         DataTable tblLeave = clsLeave.DSGForApproval(pDateStart, pDateEnd);
         foreach (DataRow drw in tblLeave.Rows)
         {
            DataRow drwNew = tblReturn.NewRow();
            drwNew["Username"] = drw["username"].ToString();
            drwNew["EmployeeName"] = drw["lastname"].ToString() + ", " + drw["firname"].ToString();
            drwNew["LeaveType"] = clsLeaveType.GetDescription(drw["leavtype"].ToString());
            drwNew["DateFiled"] = clsValidator.CheckDate(drw["datefile"].ToString()).ToString("MM/dd/yy hh:mm tt");
            drwNew["DateStart"] = clsValidator.CheckDate(drw["datestrt"].ToString()).ToString("MM/dd/yy hh:mm tt");
            drwNew["DateEnd"] = clsValidator.CheckDate(drw["dateend"].ToString()).ToString("MM/dd/yy hh:mm tt");
            drwNew["Unit"] = drw["units"].ToString();
            drwNew["Reason"] = drw["reason"].ToString();
            drwNew["Approver"] = clsEmployee.GetName(drw["apphname"].ToString(), EmployeeNameFormat.LastFirst);
            tblReturn.Rows.Add(drwNew);
         }
         return tblReturn;
      }

      public static DataTable DSGARNoApplication(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("Division");
         tblReturn.Columns.Add("AbsentCount");
         tblReturn.Columns.Add("AbsentDay");

         int intAbsentTotal = 0;
         float fltAbsentCount = 0;

         DataTable tblEmployee = clsEmployee.DSDTREmployee();
         foreach (DataRow drwEmployee in tblEmployee.Rows)
         {
            intAbsentTotal = clsTimesheet.CountAbsentTotalNoApplication(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltAbsentCount = clsTimesheet.CountAbsentSumNoApplication(drwEmployee["username"].ToString(), pDateStart, pDateEnd);

            if (intAbsentTotal > 0 && fltAbsentCount > 0)
            {
               DataRow drwNew = tblReturn.NewRow();
               drwNew["Username"] = drwEmployee["username"].ToString();
               drwNew["EmployeeName"] = drwEmployee["lastname"].ToString() + ", " + drwEmployee["firname"].ToString();
               drwNew["Division"] = drwEmployee["division"].ToString();
               drwNew["AbsentCount"] = clsValidator.ZeroToDash(intAbsentTotal);
               drwNew["AbsentDay"] = clsValidator.ZeroToDash(fltAbsentCount);
               tblReturn.Rows.Add(drwNew);
            }
         }
         return tblReturn;
      }

      public static DataTable DSGAROBForApproval(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("DateFiled");
         tblReturn.Columns.Add("DateStart");
         tblReturn.Columns.Add("DateEnd");
         tblReturn.Columns.Add("Reason");
         tblReturn.Columns.Add("Approver");

         DataTable tblOB = clsOB.DSGForApproval(pDateStart, pDateEnd);
         foreach (DataRow drw in tblOB.Rows)
         {
            DataTable tblOBDetails = clsOBDetails.DSGApproved(drw["obcode"].ToString());
            foreach (DataRow drwDetails in tblOBDetails.Rows)
            {
               DataRow drwNew = tblReturn.NewRow();
               drwNew["Username"] = drw["username"].ToString();
               drwNew["EmployeeName"] = drw["lastname"].ToString() + ", " + drw["firname"].ToString();
               drwNew["DateFiled"] = clsValidator.CheckDate(drw["datefile"].ToString()).ToString("MM/dd/yy hh:mm tt");
               drwNew["DateStart"] = clsValidator.CheckDate(drwDetails["keyin"].ToString()).ToString("MM/dd/yy hh:mm tt");
               drwNew["DateEnd"] = clsValidator.CheckDate(drwDetails["keyout"].ToString()).ToString("MM/dd/yy hh:mm tt");
               drwNew["Reason"] = drw["reason"].ToString();
               if (drw["obtype"].ToString() == "0")
               {
                  drwNew["Approver"] = clsEmployee.GetName(drw["apphname"].ToString(), EmployeeNameFormat.LastFirst);
               }
               else
               {
                  if (drw["apprstat"].ToString() == "F")
                     drwNew["Approver"] = clsEmployee.GetName(drw["apprname"].ToString(), EmployeeNameFormat.LastFirst);
                  else
                     drwNew["Approver"] = clsEmployee.GetName(drw["apphname"].ToString(), EmployeeNameFormat.LastFirst);
               }
               tblReturn.Rows.Add(drwNew);
            }
         }
         return tblReturn;
      }

      public static DataTable DSGARSummary(string pDivisionCode, DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("Department");
         tblReturn.Columns.Add("TardinessCount");
         tblReturn.Columns.Add("TardinessMinute");
         tblReturn.Columns.Add("UndertimeCount");
         tblReturn.Columns.Add("UndertimeMinute");
         tblReturn.Columns.Add("AbsentCount");
         tblReturn.Columns.Add("AbsentDay");
         tblReturn.Columns.Add("LWPCount");
         tblReturn.Columns.Add("LWPDay");
         tblReturn.Columns.Add("LWOPCount");
         tblReturn.Columns.Add("LWOPDay");
         tblReturn.Columns.Add("TotalWorkDay");
         tblReturn.Columns.Add("TotalWorkHour");

         DataTable tblEmployee = clsEmployee.DSDTREmployee(pDivisionCode);
         foreach (DataRow drwEmployee in tblEmployee.Rows)
         {
            DataRow drwNew = tblReturn.NewRow();
            drwNew["Username"] = drwEmployee["username"].ToString();
            drwNew["EmployeeName"] = drwEmployee["lastname"].ToString() + ", " + drwEmployee["firname"].ToString();
            drwNew["Department"] = drwEmployee["deptname"].ToString();
            drwNew["TardinessCount"] = clsValidator.ZeroToDash(clsTimesheet.CountTardinessTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["TardinessMinute"] = clsValidator.ZeroToDash(clsTimesheet.CountTardinessSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["UndertimeCount"] = clsValidator.ZeroToDash(clsTimesheet.CountUndertimeTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["UndertimeMinute"] = clsValidator.ZeroToDash(clsTimesheet.CountUndertimeSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["AbsentCount"] = clsValidator.ZeroToDash(clsTimesheet.CountAbsentTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["AbsentDay"] = clsValidator.ZeroToDash(clsTimesheet.CountAbsentSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["LWPCount"] = clsValidator.ZeroToDash(clsTimesheet.CountLeaveWithPayTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["LWPDay"] = clsValidator.ZeroToDash(clsTimesheet.CountLeaveWithPaySum(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["LWOPCount"] = clsValidator.ZeroToDash(clsTimesheet.CountLeaveWithOutPayTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["LWOPDay"] = clsValidator.ZeroToDash(clsTimesheet.CountLeaveWithOutPaySum(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["TotalWorkDay"] = clsValidator.ZeroToDash(clsTimesheet.CountWorkDay(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            drwNew["TotalWorkHour"] = clsValidator.ZeroToDash(clsTimesheet.CountWorkHour(drwEmployee["username"].ToString(), pDateStart, pDateEnd));
            tblReturn.Rows.Add(drwNew);
         }

         return tblReturn;
      }

      public static DataTable DSGARTardinessExcessiveSummary(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("Division");
         tblReturn.Columns.Add("TardinessCount");
         tblReturn.Columns.Add("TardinessMinute");

         int intTardinessCount = 0;
         float fltTardinessMinute = 0;

         DataTable tblEmployee = clsEmployee.DSDTREmployee();
         foreach (DataRow drwEmployee in tblEmployee.Rows)
         {
            intTardinessCount = clsTimesheet.CountTardinessTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltTardinessMinute = clsTimesheet.CountTardinessSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);

            if (intTardinessCount > 4 || fltTardinessMinute > 60)
            {
               DataRow drwNew = tblReturn.NewRow();
               drwNew["Username"] = drwEmployee["username"].ToString();
               drwNew["EmployeeName"] = drwEmployee["lastname"].ToString() + ", " + drwEmployee["firname"].ToString();
               drwNew["Division"] = drwEmployee["division"].ToString();
               drwNew["TardinessCount"] = clsValidator.ZeroToDash(intTardinessCount);
               drwNew["TardinessMinute"] = clsValidator.ZeroToDash(fltTardinessMinute);
               tblReturn.Rows.Add(drwNew);
            }
         }

         return tblReturn;
      }

      public static DataTable DSGARTardinessSummary(DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("Username");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("Division");
         tblReturn.Columns.Add("TardinessCount");
         tblReturn.Columns.Add("TardinessMinute");

         int intTardinessCount = 0;
         float fltTardinessMinute = 0;

         DataTable tblEmployee = clsEmployee.DSDTREmployee();
         foreach (DataRow drwEmployee in tblEmployee.Rows)
         {
            intTardinessCount = clsTimesheet.CountTardinessTotal(drwEmployee["username"].ToString(), pDateStart, pDateEnd);
            fltTardinessMinute = clsTimesheet.CountTardinessSum(drwEmployee["username"].ToString(), pDateStart, pDateEnd);

            if (intTardinessCount > 0 && fltTardinessMinute > 0)
            {
               DataRow drwNew = tblReturn.NewRow();
               drwNew["Username"] = drwEmployee["username"].ToString();
               drwNew["EmployeeName"] = drwEmployee["lastname"].ToString() + ", " + drwEmployee["firname"].ToString();
               drwNew["Division"] = drwEmployee["division"].ToString();
               drwNew["TardinessCount"] = clsValidator.ZeroToDash(intTardinessCount);
               drwNew["TardinessMinute"] = clsValidator.ZeroToDash(fltTardinessMinute);
               tblReturn.Rows.Add(drwNew);
            }
         }

         return tblReturn;
      }

      public static DataTable GetEmployeeTimeSheet(string pUsername, DateTime pDateFrom, DateTime pDateTo)
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM HR.Timesheet WHERE username='" + pUsername + "' AND focsdate BETWEEN '" + pDateFrom + "' AND '" + pDateTo + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
         }
         return tblReturn;
      }

      public static Color GetStatusForeColor(string pStatus)
      {
         switch (pStatus)
         {
            case "W":
               return Color.Black;
            case "N":
               return Color.Blue;
            case "A":
               return Color.Red;
            case "R":
               return Color.Green;
            case "L":
               return Color.DeepPink;
            default:
               return Color.Black;
         }
      }

      private static DateTime GetTimeIn(DateTime pTimeCardIn, DateTime pObIn)
      {
         if (pTimeCardIn != clsDateTime.SystemMinDate && pObIn != clsDateTime.SystemMinDate)
            return (pTimeCardIn <= pObIn ? pTimeCardIn : pObIn);
         else if (pTimeCardIn != clsDateTime.SystemMinDate && pObIn == clsDateTime.SystemMinDate)
            return pTimeCardIn;
         else if (pTimeCardIn == clsDateTime.SystemMinDate && pObIn != clsDateTime.SystemMinDate)
            return pObIn;
         else
            return clsDateTime.SystemMinDate;
      }

      private static DateTime GetTimeOut(DateTime pTimeCardOut, DateTime pObOut)
      {
         if (pTimeCardOut != clsDateTime.SystemMinDate && pObOut != clsDateTime.SystemMinDate)
            return (pTimeCardOut >= pObOut ? pTimeCardOut : pObOut);
         else if (pTimeCardOut != clsDateTime.SystemMinDate && pObOut == clsDateTime.SystemMinDate)
            return pTimeCardOut;
         else if (pTimeCardOut == clsDateTime.SystemMinDate && pObOut != clsDateTime.SystemMinDate)
            return pObOut;
         else
            return clsDateTime.SystemMinDate;
      }

      private static DateTime AdaptFocusDate(DateTime pFocusDate, DateTime pDateTime)
      {
         return new DateTime(pFocusDate.Year, pFocusDate.Month, pFocusDate.Day, pDateTime.Hour, pDateTime.Minute, pDateTime.Second);
      }

      public static float GetWorkUnit(DateTime pTimeIn, DateTime pTimeOut, DateTime pBreakStart, DateTime pBreakEnd)
      {
         float fltReturn = 0;
         DateTime dteTempIn = (pTimeIn >= pBreakStart && pTimeIn < pBreakEnd ? pBreakEnd : pTimeIn);
         DateTime dteTempOut = (pTimeOut > pBreakStart && pTimeOut <= pBreakEnd ? pBreakStart : pTimeOut);

         fltReturn = clsDateTime.DateDiff(pDateFormat.Hour, clsDateTime.RemoveSeconds(dteTempIn), clsDateTime.RemoveSeconds(dteTempOut));

         if (dteTempIn < pBreakStart && dteTempOut > pBreakEnd)
            fltReturn -= clsDateTime.DateDiff(pDateFormat.Hour, clsDateTime.RemoveSeconds(pBreakStart), clsDateTime.RemoveSeconds(pBreakEnd));

         return fltReturn;
      }

      public static DataTable GetEmloyeeMonthlyTimeSheet(string pUserName, DateTime pDateStart, DateTime pDateEnd)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("ImageID", System.Type.GetType("System.String"));
         tblReturn.Columns.Add("DateApp", System.Type.GetType("System.String"));
         tblReturn.Columns.Add("DateIn", System.Type.GetType("System.String"));
         tblReturn.Columns.Add("DateOut", System.Type.GetType("System.String"));

         DataTable tblHolidays = new DataTable();
         tblHolidays = clsHoliday.GetHoliday(pDateStart, pDateEnd);

         string strIn = "";
         string strOut = "";
         DataRow drow;
         DateTime dteTemp;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cn.Open();
            for (dteTemp = pDateStart; dteTemp <= pDateEnd; dteTemp = dteTemp.AddDays(1))
            {
               if (dteTemp <= pDateEnd)
               {
                  drow = tblReturn.NewRow();
                  drow["ImageID"] = "";
                  drow["DateApp"] = dteTemp.ToString("ddd MMM dd");

                  strIn = GetTimeCardIn(dteTemp, pUserName);
                  if (strIn == "")
                     drow["DateIn"] = "-";
                  else
                     drow["DateIn"] = Convert.ToDateTime(strIn).ToString("MMM dd hh:mm tt");

                  strOut = GetTimeCardOut(dteTemp, pUserName);
                  if (strOut == "")
                     drow["DateOut"] = "-";
                  else
                     drow["DateOut"] = Convert.ToDateTime(strOut).ToString("MMM dd hh:mm tt");

                  if (strIn == "")
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
               else
               {
                  break;
               }

            }
         }
         return tblReturn;
      }

      private static string GetTimeCardIn(DateTime pFocusDate, string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 keyin FROM HR.TimeCard WHERE username='" + pUsername + "' AND focsdate='" + pFocusDate + "' ORDER BY keyin";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      private static string GetTimeCardOut(DateTime pFocusDate, string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 keyout FROM HR.TimeCard WHERE username='" + pUsername + "' AND focsdate='" + pFocusDate + "' ORDER BY keyout DESC";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static DataTable GetDataTableProcessed(DateTime pDateStart, DateTime pDateEnd, string pUsername)
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM HR.TimeSheet WHERE username='" + pUsername + "' AND (focsdate BETWEEN '" + pDateStart + "' AND '" + pDateEnd + "')";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
         }
         return tblReturn;
      }

   }
}