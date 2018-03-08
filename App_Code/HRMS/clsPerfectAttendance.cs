using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
   public class clsPerfectAttendance : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }


        public static bool IsPerfectAttendance(string pUsername, string pFiscalYear, string pFiscalMonth, string pLevelCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username  FROM  HR.PerfectAttendance WHERE username=@username  AND fsclyear=@fsclyear  AND fsmncode=@fsmncode  AND levlcode=@levlcode";
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cmd.Parameters.Add(new SqlParameter("@fsclyear", pFiscalYear));
                cmd.Parameters.Add(new SqlParameter("@fsmncode", pFiscalMonth));
                cmd.Parameters.Add(new SqlParameter("@levlcode", pLevelCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                blnReturn = dr.Read();
                dr.Close();
            }
            return blnReturn;
        }

        public static DataTable GetMonths()
        {
            DataTable months = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                string fiscalYear = "";
                DateTime lastProcess = DateTime.Now;
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT fsclyear FROM Speedo.FiscalYear WHERE penabled='1'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    fiscalYear = dr["fsclyear"].ToString();
                }
                dr.Close();

                cmd.CommandText = "SELECT TOP 1 tspto FROM HR.TimeSheetPeriod WHERE tspmode='M' ORDER BY tspcode DESC";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lastProcess = clsValidator.CheckDate(dr["tspto"].ToString());
                }
                dr.Close();

                cmd.CommandText = "SELECT tspfrom = datefrom, tspto = dateto,fsmncode,fsmnname,fsclyear FROM Speedo.FiscalYearMonth WHERE dateto <= @TargetYear AND fsclyear=@fsclyear ORDER BY fsmncode DESC";
                cmd.Parameters.Add(new SqlParameter("@TargetYear", lastProcess));
                cmd.Parameters.Add(new SqlParameter("@fsclyear", fiscalYear));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(months);
            }

            return months;
        }

        public static DataTable GetDivision()
        {
            DataTable division = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT divicode, division FROM HR.Division ORDER BY division";
                //cmd.CommandText = "SELECT divicode, division FROM HR.Division WHERE divicode='ACDMCS' ORDER BY division";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(division);
            }

            return division;
        }

        public static DataTable GetPerfectAttendanceLevel()
        {
            DataTable division = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT levlcode  AS pValue, descrptn AS pText FROM  HR.PerfectAttendanceLevel WHERE enabled='1' ORDER BY levlcode";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(division);
            }

            return division;
        }

        public static DataTable GetEmployees(string pDivisionCode, string pFiscalYear, string pLevelCode)
        {
            DataTable employees = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username, lastname + ', ' + firname AS EmployeeName, datestrt FROM HR.Employees WHERE divicode=@divicode AND username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') AND pstatus='1' AND username IN (SELECT username FROM HR.PerfectAttendance WHERE fsclyear=@fsclyear AND levlcode=@levlcode) ORDER BY lastname,firname";
                cmd.Parameters.Add(new SqlParameter("@divicode", pDivisionCode));
                cmd.Parameters.Add(new SqlParameter("@fsclyear", pFiscalYear));
                cmd.Parameters.Add(new SqlParameter("@levlcode", pLevelCode));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(employees);
            }
            return employees;
        }

        public static string GetActiveFiscalYear()
        {
            string strReturn = "";

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT fsclyear FROM Speedo.FiscalYear WHERE penabled='1'";
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }

            return strReturn;
        }

        public static DataTable DSGEmployeeList()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [rn],username, nickname FROM HR.Employees WHERE pstatus='1' ORDER BY rn";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetPerfectAttendanceAnnual(string pFiscalYear, string pLevelCode)
        { 
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("username");
            tblReturn.Columns.Add("nickname");
            DataTable tblEmployees = DSGEmployeeList();
            foreach (DataRow drEmployees in tblEmployees.Rows)
            {
                if (IsPerfectAttendanceAnnual(drEmployees["username"].ToString(), pFiscalYear, pLevelCode))
                {
                    DataRow drNew = tblReturn.NewRow();
                    drNew["username"] = drEmployees["username"].ToString();
                    drNew["nickname"] = drEmployees["nickname"].ToString();
                    tblReturn.Rows.Add(drNew);
                }
            }
            
            return tblReturn;
        }

        public static bool IsPerfectAttendanceAnnual(string pUsername, string pFiscalYear, string pLevelCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(username) FROM HR.PerfectAttendance WHERE username=@username AND fsclyear=@fsclyear AND levlcode=@levlcode";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@fsclyear", pFiscalYear));
                    cmd.Parameters.Add(new SqlParameter("@levlcode", pLevelCode));
                    cn.Open();
                    if (cmd.ExecuteScalar().ToString().ToInt() == 12)
                    {
                        blnReturn = true;
                    }
                }
            }
            return blnReturn;
        }


    }
}