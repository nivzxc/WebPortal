using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

namespace STIeForms
{
    public class clsMRCFTransactionType
    {
        public clsMRCFTransactionType()
        { }

        public static DataTable GetDataSourceList(string pLineTypeCode)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TransactionTypeCode AS pValue, TransactionTypeName AS pText FROM Oracle.MrcfTransactionType WHERE  Enabled = '1' AND LineTypeCode = @LineTypeCode ORDER BY pText";
                    cmd.Parameters.Add(new SqlParameter("@LineTypeCode", pLineTypeCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }

        public static string GetDescription(string pTransactionTypeCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TransactionTypeName FROM Oracle.MrcfTransactionType WHERE  Enabled = '1' AND TransactionTypeCode = @TransactionTypeCode";
                    cmd.Parameters.Add(new SqlParameter("@TransactionTypeCode", pTransactionTypeCode));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
            return strReturn;
        }
    }
}