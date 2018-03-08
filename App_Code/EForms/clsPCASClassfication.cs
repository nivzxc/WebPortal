using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace STIeForms
{
    public class clsPCASClassfication:IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        private string _strPCASClassCode;
        private string _strPCASClassName;
        private string _strEnabled;

        public string PCASClassCode { get { return _strPCASClassCode; } set { _strPCASClassCode = value; } }
        public string PCASClassName { get { return _strPCASClassName; } set { _strPCASClassName = value; } }
        public string Enabled { get { return _strEnabled; } set { _strEnabled = value; } }


        public static DataTable GetDSL()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT pclscode as pValue, pclsname as pText FROM Finance.PCASClassification WHERE enabled='1' ORDER BY pValue";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static string GetName(string pClassification)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT pclsname FROM Finance.PCASClassification WHERE pclscode =@pclscode";
                cmd.Parameters.Add(new SqlParameter("@pclscode", pClassification));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["pclsname"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }
    }
}