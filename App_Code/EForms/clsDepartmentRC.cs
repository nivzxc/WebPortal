using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;
using System.Configuration;

namespace STIeForms
{
    public class clsDepartmentRC:IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        public static DataTable GetDSL()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT pctpcode as pValue, pctpname as pText FROM Finance.PCASChargeType WHERE enabled='1' ORDER BY pValue";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetDSLRCApprovers(string pRcCode)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT(username) AS pvalue,(SELECT lastname + ', ' + firname FROM HR.Employees WHERE username=HR.DepartmentApprover.username) AS ptext FROM HR.DepartmentApprover WHERE deptcode IN (SELECT deptcode FROM Finance.DepartmentRC WHERE rccode=@rccode)";
                cmd.Parameters.Add(new SqlParameter("@rccode", pRcCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static string GetRCcode(string pDepartmentCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT rccode FROM Finance.DepartmentRC WHERE deptcode=@deptcode";
                cmd.Parameters.Add(new SqlParameter("@deptcode", pDepartmentCode));
                cn.Open();
                try
                {
                    strReturn = (string)cmd.ExecuteScalar();
                }
                catch
                {
                }
            }
            return strReturn;
        }
    }
}