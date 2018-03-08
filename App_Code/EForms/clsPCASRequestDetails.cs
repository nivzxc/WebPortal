using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace STIeForms
{
    public class clsPCASRequestDetails:IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }

        private string _strPcasCode;
        private string _strItemName;
        private string _strPcasChargeTypeCode;
        private string _strSchoolCode;
        private string _strRCCode;
        private string _strChargeToOthersName;
        private double _dblAmount;

        public string PCascode { get { return _strPcasCode; } set { _strPcasCode = value; } }
        public string ItemName { get { return _strItemName; } set { _strItemName = value; } }
        public string ChargeTypeCode { get { return _strPcasChargeTypeCode; } set { _strPcasChargeTypeCode = value; } }
        public string Schoolcode { get { return _strSchoolCode; } set { _strSchoolCode = value; } }
        public string RCCode { get { return _strRCCode; } set { _strRCCode = value; } }
        public string ChargeToOthersName { get { return _strChargeToOthersName; } set { _strChargeToOthersName = value; } }
        public double Amount { get { return _dblAmount; } set { _dblAmount = value; } }

        public int Insert()
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "INSERT INTO Finance.PCASDetails values(@pcascode,@itemname,@amount)";
                cmd.Parameters.Add(new SqlParameter("@pcascode", _strPcasCode));
                cmd.Parameters.Add(new SqlParameter("@itemname", _strItemName));
                cmd.Parameters.Add(new SqlParameter("@amount", _dblAmount));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
            }
            return intReturn;
        }


        //////////////////////////////////
        ///////// Static Members /////////
        //////////////////////////////////
        public static DataTable GetDSGMainForm(string pPcasCode)
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT pcascode,itemname,amount FROM Finance.PCASDetails WHERE pcascode=@pcascode";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static double GetAmount(string pPCASCode)
        {
            double dblReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT SUM(amount) FROM Finance.PCASDetails WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                try
                {
                    dblReturn = Convert.ToDouble(cmd.ExecuteScalar());
                }
                catch
                {

                }
            }
            return dblReturn;
        }
    }
}