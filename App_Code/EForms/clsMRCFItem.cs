using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

namespace STIeForms
{
    public class clsMRCFItem
    {
        public clsMRCFItem()
        {  }

        public static string GetGLAccountItems(string pTransactionTypeCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT  GLAccount FROM Oracle.MrcfItems WHERE TransactionTypeCode = @TransactionTypeCode  AND enabled='1'";
                    cmd.Parameters.Add(new SqlParameter("@TransactionTypeCode", pTransactionTypeCode));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                    cn.Close();
               }
            }
            return strReturn;
        }


        public static string GetTransactionTypeName(string pTransactionTypeCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT SubCategoryName FROM oracle.MrcfItems WHERE TransactionTypeCode =@TransactionTypeCode";
                    cmd.Parameters.Add(new SqlParameter("@TransactionTypeCode", pTransactionTypeCode));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
            return strReturn;
        }

    }
}