using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using HRMS;
/// <summary>
/// Summary description for clsMRCFAirlines
/// </summary>
namespace STIeForms
{
    public class clsMRCFAirlines : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        private string _strAirlcode;
        private string _strAirldesc;
        private string _strURL;
        private string _strStatus;
        private string _strCreateBy;
        private DateTime _dteCreateOn;

        public string AirlineCode { get { return _strAirlcode; } set { _strAirlcode = value; } }
        public string AirlineDescription { get { return _strAirldesc; } set { _strAirldesc = value; } }
        public string URL { get { return _strURL; } set { _strURL = value; } }
        public string Status { get { return _strStatus; } set { _strStatus = value; } }
        public string CreateBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
        public DateTime CreateOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }

        public void Fill(string pAirlcode)
        {
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM CIS.MRCFAirlines WHERE airlcode='" + pAirlcode + "'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _strAirlcode = dr["airlcode"].ToString();
                    _strAirldesc = dr["airldesc"].ToString();
                    _strURL = dr["url"].ToString();
                    _strStatus = dr["status"].ToString();
                    _strCreateBy = dr["createby"].ToString();
                    _dteCreateOn = clsValidator.CheckDate((dr["createon"].ToString()));
                }
                dr.Close();
            }
        }

        public void InsertAirlines()
        {
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                int intSeed = 0;
                cn.Open();
                cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='airlcode'";
                intSeed = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString());
                _strAirlcode = ("000" + intSeed.ToString()).Substring(intSeed.ToString().Length);

                cmd.CommandText = "INSERT INTO CIS.MRCFAirlines(airlcode,airldesc,URL,status,createby,createon,modifyby,modifyon) VALUES(@airlcode,@airdesc,@URL,'1',@CreateBy,@CreateOn,@CreateBy,@CreateOn)";
                cmd.Parameters.Add(new SqlParameter("@airlcode", _strAirlcode));
                cmd.Parameters.Add(new SqlParameter("@airdesc", _strAirldesc));
                cmd.Parameters.Add(new SqlParameter("@URL", _strURL));
                cmd.Parameters.Add(new SqlParameter("@CreateBy", _strCreateBy));
                cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='airlcode'";
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateAirlines()
        {
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();

                cmd.CommandText = "UPDATE CIS.MRCFAirlines SET airldesc = @airldesc, url = @url , status = @status , modifyby = @modifyby , modifyon = getdate() WHERE airlcode=@airlcode";
                cmd.Parameters.Add(new SqlParameter("@Airlcode", _strAirlcode));
                cmd.Parameters.Add(new SqlParameter("@Airldesc", _strAirldesc));
                cmd.Parameters.Add(new SqlParameter("@url", _strURL));
                cmd.Parameters.Add(new SqlParameter("@status", _strStatus));
                cmd.Parameters.Add(new SqlParameter("@modifyby", _strCreateBy));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStatus()
        {
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();

                cmd.CommandText = "UPDATE CIS.MRCFAirlines SET status = @status , modifyby = @modifyby , modifyon = getdate() WHERE airlcode=@airlcode";
                cmd.Parameters.Add(new SqlParameter("@Airlcode", _strAirlcode));
                cmd.Parameters.Add(new SqlParameter("@status", _strStatus));
                cmd.Parameters.Add(new SqlParameter("@modifyby", _strCreateBy));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //static

        public static bool CheckStatus(string pAirlcode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM CIS.MRCFAirlines WHERE airlcode = '" + pAirlcode + "' AND status = '1'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                blnReturn = dr.Read();
                dr.Close();
            }
            return blnReturn;

        }

        public static DataTable GetEmployees()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT convert(varchar, brthdate, 107) as pValue, lastname + ', ' + firname + ' ' + midintl as pText FROM hr.Employees WHERE pstatus = '1' ORDER BY lastname";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }
    }
}