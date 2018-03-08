using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;

namespace STIeForms
{
    public class clsCATATerminalFee : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        public clsCATATerminalFee(){}

        public static DataTable GetDSG(string pCataCode)
        {
            DataTable tblnReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "";
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblnReturn);
                }
            }
            return tblnReturn;
        }

        public static bool IsExist(string pCataCode, int pTerminalFeeCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TerminalFeeCode FROM Finance.CATATerminalFee WHERE catacode=@catacode AND TerminalFeeCode=@TerminalFeeCode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@TerminalFeeCode", pTerminalFeeCode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                }
            }
            return blnReturn;
        }
    }
}