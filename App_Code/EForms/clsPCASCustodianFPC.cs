using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace STIeForms
{
    public class clsPCASCustodianFPC
    {
        public static DataTable GetDSL()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username as pValue, (SELECT (lastname + ', ' + Firname) FROM HR.Employees WHERE username=Finance.PCASCustodianFPC.username) as pText FROM Finance.PCASCustodianFPC WHERE enabled='1' ORDER BY pValue";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }
    }
}