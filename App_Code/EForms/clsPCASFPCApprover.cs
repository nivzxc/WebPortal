using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
    public class clsPCASFPCApprover:IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }

        private string _strFPCApproverCode;
        private string _strFPCApproverName;
        private string _strFPCApproverTitle;
        private int _intFPCOrder;
        private string _strEnabled;

        public string FPCApproverCode { get { return _strFPCApproverCode; } set { _strFPCApproverCode = value; } }
        public string FPCApproverName { get { return _strFPCApproverName; } set { _strFPCApproverName = value; } }
        public string FPCApproverTitle { get { return _strFPCApproverTitle; } set { _strFPCApproverTitle = value; } }
        public int FPCOrder { get { return _intFPCOrder; } set { _intFPCOrder = value; } }
        public string Enabled { get { return _strEnabled; } set { _strEnabled = value; } }

        public static DataTable GetDSGMainForm(string pFPCApproverCode)
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT fpcacode,fpcaname,usrtitle,fpcorder,enabled FROM Finance.PCASFPCApprover WHERE fpcacode=@fpcacode";
                    cmd.Parameters.Add(new SqlParameter("@fpcacode", pFPCApproverCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainForm()
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT fpcacode,fpcaname,usrtitle,fpcorder,enabled FROM Finance.PCASFPCApprover ORDER BY fpcorder ASC";
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static bool IsExisting(string pUsername)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {

                    cmd.CommandText = "SELECT * FROM Finance.PCASFPCApprover WHERE fpcaname=@fpcaname AND enabled='1'";
                    cmd.Parameters.Add(new SqlParameter("@fpcaname", pUsername));


                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                    cn.Close();
                }
            }
            return blnReturn;
        }

        public static string GetUsername(int pFPCOrder)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT fpcaname FROM Finance.PCASFPCApprover WHERE fpcorder=@fpcorder";
                cmd.Parameters.Add(new SqlParameter("@fpcorder", pFPCOrder));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["fpcaname"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }


    }
}