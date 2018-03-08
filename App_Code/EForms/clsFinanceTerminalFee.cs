using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;

namespace STIeForms
{
    public class clsFinanceTerminalFee : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        public clsFinanceTerminalFee() { }

        public static DataTable GetDSG()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT     TerminalFeeCode, TerminalFeeName, TerminalRate FROM Finance.TerminalFee WHERE enabled='1'";
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static double GetAmount(int pTerminalFeeCode)
        {

            double dblReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TerminalRate FROM Finance.TerminalFee WHERE TerminalFeeCode=@TerminalFeeCode";
                    cmd.Parameters.Add(new SqlParameter("@TerminalFeeCode", pTerminalFeeCode));
                    cn.Open();
                    dblReturn = Convert.ToDouble(cmd.ExecuteScalar().ToString());

                }
            }
            return dblReturn;
        }
    }
}