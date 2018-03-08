using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace STIeForms
{
    public class clsPCASRequestCustodian : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }

        private string _strPcasCode;
        private string _strUsername;

        public string PCascode { get { return _strPcasCode; } set { _strPcasCode = value; } }
        public string Username { get { return _strUsername; } set { _strUsername = value; } }

        public int Insert()
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "INSERT INTO Finance.PCASRequestCustodian values(@pcascode,@username,getdate())";
                cmd.Parameters.Add(new SqlParameter("@pcascode", _strPcasCode));
                cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
            }
            return intReturn;
        }

        public int Delete()
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Finance.PCASRequestCustodian WHERE pcascode=@pcascode";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", _strPcasCode));
                    cn.Open();
                    intReturn = cmd.ExecuteNonQuery();
                }
            }
            return intReturn;
        }

        /////Static
        public static string GetUsername(string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username FROM Finance.PCASRequestCustodian WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["username"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }

        public static string GetDateIssued(string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT dateissd FROM Finance.PCASRequestCustodian WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["dateissd"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }

        public static int UpdateDateIssued(string pPCASCode, string pUsername)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequestCustodian SET dateissd=getdate() WHERE pcascode=@pcascode AND username=@username";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int ChangeCustodian(string pPCASCode, string pUsername)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequestCustodian SET username=@username WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int CountForApproval(string pUsername)
        {
            int intReturn = 0;
            DataTable tblCount = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Finance.PCASRequest WHERE pcasstat='A' AND isissued='F' AND pcascode IN (SELECT pcascode FROM Finance.PCASRequestCustodian WHERE username=@username)";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblCount);
                    intReturn = tblCount.Rows.Count;
                }
            }
            return intReturn;
        }
    }
}