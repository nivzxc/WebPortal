using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace HRMS {
    public class clsSchool : IDisposable
    {
        private string _strSchoolCode;
        private string _strSchoolName;
        private string _strSchoolNameAlt;
        private string _strSCatCode;
        private string _strNatlCode;
        private string _strRegiCode;
        private string _strProvCode;
        private string _strAddress;
        private string _strCEO;
        private string _strCOO;
        private string _strCM;
        private string _strTelNumber;
        private string _strFaxNumber;
        private string _strHQOwned;
        private DateTime _dteLastUpdatedDate;
        private string _strLastUpdatedBy;

        //added by calvin
        //for philippine first portal
        private string _strBranchCode;
        private string _strBranchName;
        private string _strBranchManager;
        private string _strBranchAddress;
        private string _strBranchContact;
        private string _strBranchEmail;


        //added by calvin
        //for philippine first portal
        public string branchCode { get { return _strBranchCode; } set { _strBranchCode = value; } }
        public string branchName { get { return _strBranchName; } set { _strBranchName = value; } }
        public string branchManager { get { return _strBranchManager; } set { _strBranchManager = value; }  }
        public string branchAddress { get { return _strBranchAddress; } set { _strBranchAddress = value; } }
        public string branchContact { get { return _strBranchContact; } set { _strBranchContact = value; } }
        public string branchEmail { get { return _strBranchEmail; } set { _strBranchEmail = value; } }

        public clsSchool() { }

        public string SchoolCode { get { return _strSchoolCode; } set { _strSchoolCode = value; } }
        public string SchoolName { get { return _strSchoolName; } set { _strSchoolName = value; } }
        public string SchoolNameAlt { get { return _strSchoolNameAlt; } set { _strSchoolNameAlt = value; } }
        public string SCatCode { get { return _strSCatCode; } set { _strSCatCode = value; } }
        public string Address { get { return _strAddress; } set { _strAddress = value; } }
        public string CEO { get { return _strCEO; } set { _strCEO = value; } }
        public string COO { get { return _strCOO; } set { _strCOO = value; } }
        public string CM { get { return _strCM; } set { _strCM = value; } }
        public string TelNumber { get { return _strTelNumber; } set { _strTelNumber = value; } }
        public string FaxNumber { get { return _strFaxNumber; } set { _strFaxNumber = value; } }
        public string HQOwned { get { return _strHQOwned; } set { _strHQOwned = value; } }
        public DateTime LastUpdatedDate { get { return _dteLastUpdatedDate; } set { _dteLastUpdatedDate = value; } }
        public string LastUpdatedBy { get { return _strLastUpdatedBy; } set { _strLastUpdatedBy = value; } }

        public void Fill()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM CM.Schools WHERE schlcode='" + _strSchoolCode + "'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _strSchoolCode = dr["schlcode"].ToString();
                    _strSchoolName = dr["schlname"].ToString();
                    _strSchoolNameAlt = dr["schlnam2"].ToString();
                    _strSCatCode = dr["scatcode"].ToString();
                    _strNatlCode = dr["natlcode"].ToString();
                    _strRegiCode = dr["regicode"].ToString();
                    _strProvCode = dr["provcode"].ToString();
                    _strAddress = dr["schladdr"].ToString();
                    _strCEO = dr["ceoname"].ToString();
                    _strCOO = dr["cooname"].ToString();
                    _strCM = dr["cmname"].ToString();
                    _strTelNumber = dr["telnmbr"].ToString();
                    _strFaxNumber = dr["faxnmbr"].ToString();
                    _strHQOwned = dr["hqowned"].ToString();
                    _dteLastUpdatedDate = (Convert.IsDBNull(dr["lastupdt"]) ? DateTime.Now : Convert.ToDateTime(dr["lastupdt"].ToString()));
                    _strLastUpdatedBy = dr["lastupby"].ToString();
                }
                dr.Close();
            }
        }

        public int Update()
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE CM.Schools SET schlname=@schlname, schlnam2=@schlnam2, scatcode=@scatcode, schladdr=@schladdr, ceoname=@ceoname, cooname=@cooname, cmname=@cmname, telnmbr=@telnmbr, faxnmbr=@faxnmbr, hqowned=@hqowned, lastupdt=@lastupdt, lastupby=@lastupby WHERE schlcode='" + _strSchoolCode + "'";
                cmd.Parameters.Add("@schlname", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@schlnam2", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@scatcode", SqlDbType.Char, 1);
                cmd.Parameters.Add("@schladdr", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@ceoname", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@cooname", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@cmname", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@telnmbr", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@faxnmbr", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@hqowned", SqlDbType.Char, 1);
                cmd.Parameters.Add("@lastupdt", SqlDbType.DateTime);
                cmd.Parameters.Add("@lastupby", SqlDbType.VarChar, 30);

                cmd.Parameters["@schlname"].Value = _strSchoolName;
                cmd.Parameters["@schlnam2"].Value = _strSchoolNameAlt;
                cmd.Parameters["@scatcode"].Value = _strSCatCode;
                cmd.Parameters["@schladdr"].Value = _strAddress;
                cmd.Parameters["@ceoname"].Value = _strCEO;
                cmd.Parameters["@cooname"].Value = _strCOO;
                cmd.Parameters["@cmname"].Value = _strCM;
                cmd.Parameters["@telnmbr"].Value = _strTelNumber;
                cmd.Parameters["@faxnmbr"].Value = _strFaxNumber;
                cmd.Parameters["@hqowned"].Value = _strHQOwned;
                cmd.Parameters["@lastupdt"].Value = _dteLastUpdatedDate;
                cmd.Parameters["@lastupby"].Value = _strLastUpdatedBy;
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
            }
            return intReturn;
        }

        public void Dispose() { GC.SuppressFinalize(this); }

        //////////////////////////////////
        ///////// Statis Members /////////
        //////////////////////////////////

        public static DataTable DSSchoolsByNational(string pNationalCode)
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM CM.Schools WHERE natlcode='" + pNationalCode + "' AND scatcode IN ('C','E') AND pstatus='1' ORDER BY schlnam2";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }

            return tblReturn;
        }

        public static DataTable GetSchoolCategoryDDLDataSource()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT scatcode AS pvalue,scatname AS ptext FROM CM.SchoolCategory ORDER BY scatname";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetCMDDLDataSource()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username AS pvalue, firname + ' ' + lastname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Users.UsersCM) ORDER BY firname";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static string GetChannelManagerName(string pSchoolCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT firname + ' ' + lastname AS Name FROM Users.Users WHERE username=(SELECT cmname FROM CM.Schools WHERE schlcode='" + pSchoolCode + "')";
                cn.Open();
                try
                {
                    strReturn = (string)cmd.ExecuteScalar();
                }
                catch
                {
                }
            }
            return strReturn;
        }

        public static string GetChannelManager(string pSchoolCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT cmname FROM CM.Schools WHERE schlcode='" + pSchoolCode + "'";
                cn.Open();
                try
                {
                    strReturn = (string)cmd.ExecuteScalar();
                }
                catch
                {
                }
            }

            return strReturn;
        }

        public static string GetSchoolName(string pSchoolCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT schlname FROM CM.Schools WHERE schlcode='" + pSchoolCode + "'";
                cn.Open();
                try
                {
                    strReturn = (string)cmd.ExecuteScalar();
                }
                catch
                {
                }
            }
            return strReturn;
        }

        /// <summary>
        /// Get the drop down list data source of schools filtered by specified channel manager
        /// </summary>
        /// <param name="pUserName"></param>
        /// <returns></returns>
        public static DataTable GetSchoolCmDdlDataTable(string pUserName)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT schlcode AS pvalue,schlname AS ptext FROM CM.Schools WHERE cmname='" + pUserName + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        //added by charlie
        public static DataTable GetSchoolCMHQOwned()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT schlcode AS pvalue,schlname AS ptext FROM CM.Schools ORDER BY ptext";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        //added by rollie
        public static DataTable GetDSLSchool()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT schlcode AS pvalue,schlname AS ptext FROM CM.Schools ORDER BY ptext";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        //added by calvin
        // to add branches of philippine first
        public int insert()
        {
            branchCoder();
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                string insert_brnch = "INSERT INTO dbo.Branches (branchcode,branchname,branchmnger,branchaddress,branchcontact,branchEmail) "+
                                      "VALUES (@branchcode,@branchname,@branchmnger,@branchaddress,@branchcontact,@branchEmail)";
                cn.Open();
                SqlCommand cmd = new SqlCommand(insert_brnch, cn);
                cmd.Parameters.Add(new SqlParameter("@branchcode",_strBranchCode));
                cmd.Parameters.Add(new SqlParameter("@branchname", _strBranchName));
                cmd.Parameters.Add(new SqlParameter("@branchmnger", _strBranchManager));
                cmd.Parameters.Add(new SqlParameter("@branchaddress", _strBranchAddress));
                cmd.Parameters.Add(new SqlParameter("@branchcontact", _strBranchContact));
                cmd.Parameters.Add(new SqlParameter("@branchEmail", _strBranchEmail));
                intReturn = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return intReturn;

        }
        private void branchCoder()
        {
            string brnchcode = "";
            int Intcode = 0;
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 branchcode FROM dbo.Branches ORDER BY branchcode DESC";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) {
                    brnchcode = dr["branchcode"].ToString();
                }
                dr.Close();
                cn.Close();
                if (brnchcode == null || brnchcode == "")
                {
                    Intcode = clsValidator.CheckInteger(brnchcode) + 1;
                    brnchcode = ("BC0" + Intcode.ToString());
                    branchCode = brnchcode;
                }
                else {
                    char[] removechar = { 'B', 'C' };
                    string clearCharCode = brnchcode.TrimStart(removechar);
                    brnchcode = clearCharCode;
                    Intcode = clsValidator.CheckInteger(brnchcode) + 1;
                    brnchcode = ("BC0" + Intcode.ToString());
                    branchCode = brnchcode;
                }
            }
        }

    }
}

