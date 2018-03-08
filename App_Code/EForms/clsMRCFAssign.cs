using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HRMS;
/// <summary>
/// Summary description for clsMRCFAssign
/// </summary>
namespace STIeForms
{
    public class clsMRCFAssign : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        private string _strMrcfCode;
        private string _strHdlrCode;
        private DateTime _dteCreateOn;
        private string _strCreateBy;
        private string _strIsActive;
        private string _strAssignTo;
        private DateTime _dteAssignTo;
        private string _strAssignBy;
        private string _strRemarks;
        private string _strStatCode;
        private string _strstatDesc;
        private string _strstatLevel;

        public string MRCFCode { get { return _strMrcfCode; } set { _strMrcfCode = value; } }
        public string HandlerCode { get { return _strHdlrCode; } set { _strHdlrCode = value; } }
        public DateTime CreateOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
        public string CreateBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
        public string IsActive { get { return _strIsActive; } set { _strIsActive = value; } }
        public DateTime DateAssignTo { get { return _dteAssignTo; } set { _dteAssignTo = value; } }
        public string AssignTo { get { return _strAssignTo; } set { _strAssignTo = value; } }
        public string AssignBy { get { return _strAssignBy; } set { _strAssignBy = value; } }
        public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }
        public string StatusCode { get { return _strStatCode; } set { _strStatCode = value; } }
        public string StatusDescription { get { return _strstatDesc; } set { _strstatDesc = value; } }
        public string StatusLevel { get { return _strstatLevel; } set { _strstatLevel = value; } }

        public void AssignEmployee()
        {
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                int intSeed = 0;
                cn.Open();
                cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='hdlrcode'";
                intSeed = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString());
                _strHdlrCode = ("000000000" + intSeed.ToString()).Substring(intSeed.ToString().Length);

                cmd.CommandText = "INSERT INTO CIS.MrcfAssign(hdlrcode,mrcfcode,createon,createby,isactive) VALUES(@HdlrCode,@MrcfCode,@CreateOn,@CreateBy,@IsActive)";
                cmd.Parameters.Add(new SqlParameter("@MrcfCode", _strMrcfCode));
                cmd.Parameters.Add(new SqlParameter("@HdlrCode", _strHdlrCode));
                cmd.Parameters.Add(new SqlParameter("@CreateOn", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@CreateBy", _strCreateBy));
                cmd.Parameters.Add(new SqlParameter("@IsActive", _strIsActive));
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();

                cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='hdlrcode'";
                cmd.ExecuteNonQuery();
            }
        }



        public void AssignEmployeeDetails(string pAction)
        {
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();

                cmd.CommandText = "UPDATE CIS.MrcfAssignDetails SET isactive='0' WHERE hdlrcode=@hdlrcode";
                cmd.Parameters.Add(new SqlParameter("@hdlrcode", _strHdlrCode));
                cn.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                
                if (pAction == "Update Status")
                {
                    cmd.CommandText = "UPDATE CIS.MRCFAssign SET isactive='1' WHERE hdlrcode=@hdlrcode";
                    cmd.Parameters.Add(new SqlParameter("@hdlrcode", _strHdlrCode));}
                else
                {
                    cmd.CommandText = "UPDATE CIS.MRCFAssign SET isactive='1' WHERE hdlrcode=@hdlrcode";
                    cmd.Parameters.Add(new SqlParameter("@hdlrcode", _strHdlrCode));
                
                }
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                cmd.CommandText = "INSERT INTO CIS.MrcfAssignDetails(hdlrcode,assignto,assignby,createby,remarks,statcode,createon,isactive) VALUES(@HdlrCode,@assignto,@assignby,@CreateBy,@remarks,@statcode,@createon,@isactive)";
                cmd.Parameters.Add(new SqlParameter("@hdlrcode", _strHdlrCode));
                cmd.Parameters.Add(new SqlParameter("@assignto", _strAssignTo));
                cmd.Parameters.Add(new SqlParameter("@assignby", _strAssignBy));
                cmd.Parameters.Add(new SqlParameter("@CreateBy", _strCreateBy));
                cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
                cmd.Parameters.Add(new SqlParameter("@statcode", _strStatCode));
                cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@isactive", 1));

                cmd.ExecuteNonQuery();
            }
        }

        public string GetInitialStatusCode()
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Statcode FROM CIS.MrcfAssignStatus WHERE statlevl = '1'";
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();

            }
            return strReturn;
        }

        public string GetProcurementManager(string pModlCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT (SELECT username FROM Users.UsersModules WHERE modlcode = Speedo.Modules.modlcode) as ProcMngr FROM Speedo.Modules WHERE modlcode = @mdlcode";
                cmd.Parameters.Add(new SqlParameter("@mdlcode", pModlCode));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();

            }
            return strReturn;
        }

        public double GetProjectPercentage(string pStatusCode)
        {
            double dblReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select statlevl,(SELECT COUNT(*) from cis.mrcfassignstatus) -1 as MaxCount from cis.mrcfassignstatus where statcode = @statcode";
                cmd.Parameters.Add(new SqlParameter("@statcode", pStatusCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                  if (pStatusCode != "000" )
                  {
                    if (Convert.ToDouble(dr["statlevl"].ToString()) == 0)
                    { dblReturn = Convert.ToDouble(dr["MaxCount"].ToString()) / Convert.ToDouble(dr["MaxCount"].ToString()) * 100; }
                    else { dblReturn = Convert.ToDouble(dr["statlevl"].ToString()) / Convert.ToDouble(dr["MaxCount"].ToString()) * 100; }
                  }
                  else
                  {
                      dblReturn = 0;
                  }
                }
            }
            return dblReturn;
        }

        public string GetHandlerCode(string pMRCFCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode = @mrcfcode";
                cmd.Parameters.Add(new SqlParameter("@mrcfcode", pMRCFCode));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();

            }
            return strReturn;
        }


        public string LoadCurrentStatus(string pMRCFcode)
        {
            string StrCurrentStatus = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode ='" + pMRCFcode + "') AND isactive = '1' ORDER BY hdlrcode ASC";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StrCurrentStatus = dr["statcode"].ToString();


                }
                dr.Close();
            }

            return StrCurrentStatus;
        }

        public string GetMRCFAssignedBy(string pMRCFcode)
        {
            string StrReturn = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT assignby FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT TOP 1 hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode ='" + pMRCFcode + "' AND isactive = '1' ORDER BY hdlrcode DESC) AND isactive = '1' ORDER BY hdlrcode ASC";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StrReturn = dr["assignby"].ToString();
                }
                dr.Close();
            }

            return StrReturn;
        }

        public string GetMRCFAssignedto(string pMRCFcode)
        {
            string StrReturn = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT assignto FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT TOP 1 hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode ='" + pMRCFcode + "' AND isactive = '1' ORDER BY hdlrcode DESC) AND isactive = '1'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StrReturn = dr["assignto"].ToString();
                }
                dr.Close();
            }

            return StrReturn;
        }
        
        //////////////////////////////////
        ///////// Static Members /////////
        //////////////////////////////////

        public static bool IsPurchasing(string pUsername)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM USers.UsersModules WHERE modlcode = 'PROC' and pstatus = '1' and username = '" + pUsername + "'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                blnReturn = dr.Read();
                dr.Close();
            }
            return blnReturn;
        }


        public static int GetTotalAssigned(string pUsername)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT count(hdlrcode) AS Notif ,assignto FROM CIS.MRCFAssignDetails WHERE isactive='1' AND assignto = '" + pUsername + "' AND statcode='002' GROUP BY assignto";

                cn.Open();
                try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
                catch { intReturn = 0; }
            }
            return intReturn;
        }


    }
}




