using System;
using System.Data;
using System.Data.SqlClient;
namespace STIeForms
{
    public class clsCATAFinanceApprovers : IDisposable
    {
        public clsCATAFinanceApprovers()
        {  }

        public void Dispose() { GC.SuppressFinalize(this); }

        public static DataTable GetDSG()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT aprvname FROM Finance.CATAFinanceApprover WHERE enabled='1' ORDER BY fapvcode";
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

       
    }
}