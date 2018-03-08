using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace STIeForms
{
    public class clsPCASChargeType:IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        private string _strPCASChargeTypeCode;
        private string _strChargeTypeName;
        private string _strEnabled;

        public string PCASChargeTypeCode { get { return _strPCASChargeTypeCode;} set { _strPCASChargeTypeCode=value;} }
        public string ChargerTypeName { get { return _strChargeTypeName; } set { _strChargeTypeName = value; } }
        public string Enabled {get{return _strEnabled;}set{_strEnabled=value;}}


        public static DataTable GetDSL()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT pctpcode as pValue, pctpname as pText FROM Finance.PCASChargeType WHERE enabled='1' ORDER BY pValue";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static string GetName(string pChargeTypeCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT pctpname FROM Finance.PCASChargeType WHERE pctpcode =@pctpcode";
                cmd.Parameters.Add(new SqlParameter("@pctpcode", pChargeTypeCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["pctpname"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }

    }
}