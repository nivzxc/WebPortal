using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

namespace STIeForms
{
    public class clsMRCFGLAccount
    {
        public clsMRCFGLAccount(){}

        public static string GetGLAccountCode(string pTransactionTypeCode, string pDivisionCode, string pDepartmentCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT GLAccount FROM   Oracle.MrcfGLAccount WHERE    Enabled = '1' AND TransactionTypeCode =@TransactionTypeCode AND divicode = @divicode AND deptcode = @deptcode";
                    cmd.Parameters.Add(new SqlParameter("@TransactionTypeCode", pTransactionTypeCode));
                    cmd.Parameters.Add(new SqlParameter("@divicode", pDivisionCode));
                    cmd.Parameters.Add(new SqlParameter("@deptcode", pDepartmentCode));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
            return strReturn;
        }
    }
}