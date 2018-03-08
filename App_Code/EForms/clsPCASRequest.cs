using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using HRMS;

namespace STIeForms
{
    public class clsPCASRequest : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }

        private string _strPCASCode;
        private DateTime _dteDateNeeded;
        private string _strReason;
        private string _strIsExecutive;
        private string _strRequestedBy;
        private string _strClassification;
        private string _strOBCode;
        private string _strChargeTypeCode;
        private string _strSchoolCode;
        private string _strRCCode;
        private string _strOthers;
        private string _strPCASStat;
        private string _strCreatedBy;
        private DateTime _dteCreatedOn;
        private string _strModifyBy;
        private DateTime _dteModifyOn;
        private string _strRemarks;
        private double _dblApprovedRFA;
        private double _dblAmountAllocated;
        private double _dblNetAmount;
        private double _dblRequestAmount;
        private double _dblRemainingBudget;

        public string PCASCode { get { return _strPCASCode; } set { _strPCASCode = value; } }
        public DateTime DateNeeded { get { return _dteDateNeeded; } set { _dteDateNeeded = value; } }
        public string Reason { get { return _strReason; } set { _strReason = value; } }
        public string IsExecutive { get { return _strIsExecutive; } set { _strIsExecutive = value; } }
        public string RequestedBy { get { return _strRequestedBy; } set { _strRequestedBy = value; } }
        public string Classification { get { return _strClassification; } set { _strClassification = value; } }
        public string OBCode { get { return _strOBCode; } set { _strOBCode = value; } }
        public string ChargeTypeCode { get { return _strChargeTypeCode; } set { _strChargeTypeCode = value; } }
        public string SchoolCode { get { return _strSchoolCode; } set { _strSchoolCode = value; } }
        public string RCCode { get { return _strRCCode; } set { _strRCCode = value; } }
        public string Others { get { return _strOthers; } set { _strOthers = value; } }
        public string PCASStat { get { return _strPCASStat; } set { _strPCASStat = value; } }
        public string CreatedBy { get { return _strCreatedBy; } set { _strCreatedBy = value; } }
        public DateTime CreatedOn { get { return _dteCreatedOn; } set { _dteCreatedOn = value; } }
        public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
        public DateTime ModifiedOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }
        public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }
        public double ApprovedRFA { get { return _dblApprovedRFA; } set { _dblApprovedRFA = value; } }
        public double AmountAllocated { get { return _dblAmountAllocated; } set { _dblAmountAllocated = value; } }
        public double NetAmount { get { return _dblNetAmount; } set { _dblNetAmount = value; } }
        public double RequestAmount { get { return _dblRequestAmount; } set { _dblRequestAmount = value; } }
        public double RemainingBudget { get { return _dblRemainingBudget; } set { _dblRemainingBudget = value; } }

        public int Insert()
        {
            int intReturn = 0;
            int intSeed = 0;
            int intMonth = 0;
            int intYear = 0;
            string strMonth = "";
            DateTime dtDateToday = DateTime.Now;

            SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmd = cn.CreateCommand();
            cmd.Transaction = tran;

            try
            {
                cmd.CommandText = "SELECT xvalue FROM Finance.CataPrimaryKey Where xkey='CurrentYear'";
                intYear = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "SELECT xvalue FROM Finance.CataPrimaryKey Where xkey='CurrentMonth'";
                intMonth = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (intYear != dtDateToday.Year.ToString().ToInt())
                {
                    intMonth = 1;
                    intYear = intYear + 1;
                    cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=@xvalue Where xkey='CurrentYear'";
                    cmd.Parameters.Add(new SqlParameter("@xvalue", intYear));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
                    cmd.Parameters.Add(new SqlParameter("@xvalue", intMonth));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
                    cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=0 Where xkey='PCASNumber'";
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                else
                {
                    if (Convert.ToInt32(dtDateToday.Month.ToString()) == intMonth)
                    {
                        strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
                    }
                    else
                    {
                        intMonth = dtDateToday.Month.ToString().ToInt();

                        cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
                        cmd.Parameters.Add(new SqlParameter("@xvalue", intMonth));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);

                        cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=0 Where xkey='PCASNumber'";
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                cmd.CommandText = "SELECT xvalue FROM Finance.CataPrimaryKey Where xkey='PCASNumber'";
                intSeed = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

                cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=@xvalue Where xkey='PCASNumber'";
                cmd.Parameters.Add(new SqlParameter("@xvalue", intSeed));
                intReturn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                _strPCASCode = (intYear + "-" + strMonth + "-" + ("000" + intSeed.ToString()).Substring(intSeed.ToString().Length));


                cmd.CommandText = ("INSERT INTO Finance.PCASRequest (pcascode,dateneed,reason,isexecut,reqstdby,pclscode,obcode,pctpcode,schlcode,rccode,others,pcasstat,isissued,adjstmnt,createby,createon,modifyby,modifyon,remarks,apprdrfa,amtalloc,netamnt,requamnt,rembudgt) VALUES (@pcascode,@dateneed,@reason,@isexecut,@reqstdby,@pclscode, @obcode, @pctpcode,@schlcode,@rccode,@others, @pcasstat,'F',0,@createby,GETDATE(),@modifyby,GETDATE(),@remarks,0,0,0,0,0)");
                cmd.Parameters.Add(new SqlParameter("@pcascode", _strPCASCode));
                cmd.Parameters.Add(new SqlParameter("@dateneed", _dteDateNeeded));
                cmd.Parameters.Add(new SqlParameter("@reason", _strReason));
                cmd.Parameters.Add(new SqlParameter("@isexecut", _strIsExecutive));
                cmd.Parameters.Add(new SqlParameter("@reqstdby", _strRequestedBy));
                cmd.Parameters.Add(new SqlParameter("@pclscode", _strClassification));
                cmd.Parameters.Add(new SqlParameter("@obcode", _strOBCode));
                cmd.Parameters.Add(new SqlParameter("@pctpcode", _strChargeTypeCode));
                cmd.Parameters.Add(new SqlParameter("@schlcode", _strSchoolCode));
                cmd.Parameters.Add(new SqlParameter("@rccode", _strRCCode));
                cmd.Parameters.Add(new SqlParameter("@others", _strOthers));
                cmd.Parameters.Add(new SqlParameter("@pcasstat", _strPCASStat));
                cmd.Parameters.Add(new SqlParameter("@createby", _strCreatedBy));
                cmd.Parameters.Add(new SqlParameter("@modifyby", _strModifyBy));
                cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
                tran.Commit();
                intReturn = cmd.ExecuteNonQuery();

            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                cn.Close();
            }
            return intReturn;
        }

        public void Fill()
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT   pcascode, dateneed, reason, isexecut, reqstdby, pclscode, obcode,rccode, pctpcode, schlcode, rccode, others, pcasstat, createby, createon, modifyby, modifyon,remarks,apprdrfa,amtalloc,netamnt,requamnt,rembudgt FROM  Finance.PCASRequest WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode",_strPCASCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _strPCASCode = dr["pcascode"].ToString();
                    _dteDateNeeded = Convert.ToDateTime(dr["dateneed"].ToString());
                    _strReason = dr["reason"].ToString();
                    _strIsExecutive = dr["isexecut"].ToString();
                    _strRequestedBy = dr["reqstdby"].ToString();
                    _strClassification = dr["pclscode"].ToString(); ;
                    _strOBCode = dr["obcode"].ToString();
                    _strChargeTypeCode = dr["pctpcode"].ToString();
                    _strSchoolCode = dr["schlcode"].ToString();
                    _strRCCode = dr["rccode"].ToString();
                    _strOthers = dr["others"].ToString();
                    _strPCASStat = dr["pcasstat"].ToString();
                    _strCreatedBy = dr["createby"].ToString();
                    _dteCreatedOn = Convert.ToDateTime(dr["createon"].ToString());
                    _strModifyBy = dr["modifyby"].ToString();
                    _dteModifyOn = Convert.ToDateTime(dr["modifyon"].ToString());
                    _strRemarks = dr["remarks"].ToString();
                    _dblApprovedRFA = Convert.ToDouble(dr["apprdrfa"].ToString());
                    _dblAmountAllocated = Convert.ToDouble(dr["amtalloc"].ToString());
                    _dblNetAmount = Convert.ToDouble(dr["netamnt"].ToString());
                    _dblRequestAmount = Convert.ToDouble(dr["requamnt"].ToString());
                    _dblRemainingBudget = Convert.ToDouble(dr["rembudgt"].ToString());
                }
            }
        }

        public static int Approve(string pPCASCode)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequest SET pcasstat=@pcasstat WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcasstat", "A"));
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int DisApprove(string pPCASCode)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequest SET pcasstat=@pcasstat WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcasstat", "D"));
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int UpdateFPCData(string pPCASCode,double pApprovedRFA, double pAmountAllocated, double pNet, double pRequestAmount, double pRemainingBudget)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequest SET apprdrfa=@apprdrfa,amtalloc=@amtalloc,netamnt=@netamnt,requamnt=@requamnt,rembudgt=@rembudgt WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cmd.Parameters.Add(new SqlParameter("@apprdrfa", pApprovedRFA));
                cmd.Parameters.Add(new SqlParameter("@amtalloc", pAmountAllocated));
                cmd.Parameters.Add(new SqlParameter("@netamnt", pNet));
                cmd.Parameters.Add(new SqlParameter("@requamnt", pRequestAmount));
                cmd.Parameters.Add(new SqlParameter("@rembudgt", pRemainingBudget));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int TagAsIssued(string pPCASCode)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequest SET isissued=@isissued WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@isissued", "I"));
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int TagAsReady(string pPCASCode)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequest SET isissued=@isissued WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@isissued", "R"));
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int UpdateAdjustment(string pPCASCode, double pAdjustment)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequest SET adjstmnt=@adjstmnt WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@adjstmnt", pAdjustment));
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }



        public static string GetLastCreatedRequest(string pUsername)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 pcascode FROM Finance.PCASRequest WHERE createby=@crateby ORDER BY createon DESC";
                cmd.Parameters.Add(new SqlParameter("@crateby", pUsername));
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

        public static DataTable GetDSGMainFormPerUser(string pSelect, string pUserName, string pWhere, int intStart, int intEnd)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("pcascode");
            tblReturn.Columns.Add("reason");
            tblReturn.Columns.Add("dateneed");
            tblReturn.Columns.Add("isexecut");
            tblReturn.Columns.Add("reqstdby");
            tblReturn.Columns.Add("pclscode");
            tblReturn.Columns.Add("obcode");
            tblReturn.Columns.Add("pctpcode");
            tblReturn.Columns.Add("schlcode");
            tblReturn.Columns.Add("others");
            tblReturn.Columns.Add("pcasstat");
            tblReturn.Columns.Add("createby");
            tblReturn.Columns.Add("createon");

            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                //cmd.CommandText = "SELECT * FROM ( SELECT " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.RFPRequest WHERE createby = @UserName) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
                cmd.CommandText = "SELECT * FROM ( SELECT  " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.PCASRequest WHERE createby = @username) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
                cmd.Parameters.Add(new SqlParameter("@UserName", pUserName));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drwNew = tblReturn.NewRow();
                    drwNew["pcascode"] = dr["pcascode"].ToString();
                    drwNew["reason"] = dr["reason"].ToString();
                    drwNew["reqstdby"] = dr["reqstdby"].ToString();
                    drwNew["isexecut"] = dr["isexecut"].ToString();
                    drwNew["dateneed"] = Convert.ToDateTime(dr["dateneed"].ToString());
                    drwNew["pclscode"] = dr["pclscode"].ToString();
                    drwNew["obcode"] = dr["obcode"].ToString();
                    drwNew["pctpcode"] = dr["pctpcode"].ToString();
                    drwNew["schlcode"] = dr["schlcode"].ToString();
                    drwNew["others"] = dr["others"].ToString();
                    drwNew["pcasstat"] = dr["pcasstat"].ToString();
                    drwNew["createby"] = dr["createby"].ToString();
                    drwNew["createon"] = Convert.ToDateTime(dr["createon"].ToString());
                    tblReturn.Rows.Add(drwNew);
                }
                dr.Close();
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormPerUser(string pSelect, string pWhere, int intStart, int intEnd)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("pcascode");
            tblReturn.Columns.Add("reason");
            tblReturn.Columns.Add("dateneed");
            tblReturn.Columns.Add("isexecut");
            tblReturn.Columns.Add("reqstdby");
            tblReturn.Columns.Add("pclscode");
            tblReturn.Columns.Add("obcode");
            tblReturn.Columns.Add("pctpcode");
            tblReturn.Columns.Add("schlcode");
            tblReturn.Columns.Add("others");
            tblReturn.Columns.Add("pcasstat");
            tblReturn.Columns.Add("createby");
            tblReturn.Columns.Add("createon");

            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                //cmd.CommandText = "SELECT * FROM ( SELECT " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.RFPRequest WHERE createby = @UserName) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
                cmd.CommandText = "SELECT * FROM ( SELECT  " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.PCASRequest) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " " + pWhere + " ORDER BY createon DESC";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drwNew = tblReturn.NewRow();
                    drwNew["pcascode"] = dr["pcascode"].ToString();
                    drwNew["reason"] = dr["reason"].ToString();
                    drwNew["reqstdby"] = dr["reqstdby"].ToString();
                    drwNew["isexecut"] = dr["isexecut"].ToString();
                    drwNew["dateneed"] = Convert.ToDateTime(dr["dateneed"].ToString());
                    drwNew["pclscode"] = dr["pclscode"].ToString();
                    drwNew["obcode"] = dr["obcode"].ToString();
                    drwNew["pctpcode"] = dr["pctpcode"].ToString();
                    drwNew["schlcode"] = dr["schlcode"].ToString();
                    drwNew["others"] = dr["others"].ToString();
                    drwNew["pcasstat"] = dr["pcasstat"].ToString();
                    drwNew["createby"] = dr["createby"].ToString();
                    drwNew["createon"] = Convert.ToDateTime(dr["createon"].ToString());
                    tblReturn.Rows.Add(drwNew);
                }
                dr.Close();
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormPerUser(string pSelect, string pWhere, int intStart, int intEnd, string pSearchText)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("pcascode");
            tblReturn.Columns.Add("reason");
            tblReturn.Columns.Add("dateneed");
            tblReturn.Columns.Add("isexecut");
            tblReturn.Columns.Add("reqstdby");
            tblReturn.Columns.Add("pclscode");
            tblReturn.Columns.Add("obcode");
            tblReturn.Columns.Add("pctpcode");
            tblReturn.Columns.Add("schlcode");
            tblReturn.Columns.Add("others");
            tblReturn.Columns.Add("pcasstat");
            tblReturn.Columns.Add("createby");
            tblReturn.Columns.Add("createon");

            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                //cmd.CommandText = "SELECT * FROM ( SELECT " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.RFPRequest WHERE createby = @UserName) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
                cmd.CommandText = "SELECT * FROM ( SELECT  " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.PCASRequest) as FinanceRequest WHERE ((reason + pcascode) LIKE @SearchText) AND RowNum BETWEEN " + intStart + " AND " + intEnd + " " + pWhere + " ORDER BY createon DESC";
                cmd.Parameters.Add(new SqlParameter("@SearchText", "%" + pSearchText + "%"));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drwNew = tblReturn.NewRow();
                    drwNew["pcascode"] = dr["pcascode"].ToString();
                    drwNew["reason"] = dr["reason"].ToString();
                    drwNew["reqstdby"] = dr["reqstdby"].ToString();
                    drwNew["isexecut"] = dr["isexecut"].ToString();
                    drwNew["dateneed"] = Convert.ToDateTime(dr["dateneed"].ToString());
                    drwNew["pclscode"] = dr["pclscode"].ToString();
                    drwNew["obcode"] = dr["obcode"].ToString();
                    drwNew["pctpcode"] = dr["pctpcode"].ToString();
                    drwNew["schlcode"] = dr["schlcode"].ToString();
                    drwNew["others"] = dr["others"].ToString();
                    drwNew["pcasstat"] = dr["pcasstat"].ToString();
                    drwNew["createby"] = dr["createby"].ToString();
                    drwNew["createon"] = Convert.ToDateTime(dr["createon"].ToString());
                    tblReturn.Rows.Add(drwNew);
                }
                dr.Close();
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormCashier(string pIsIssued)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("pcascode");
            tblReturn.Columns.Add("reason");
            tblReturn.Columns.Add("dateneed");
            tblReturn.Columns.Add("isexecut");
            tblReturn.Columns.Add("reqstdby");
            tblReturn.Columns.Add("pclscode");
            tblReturn.Columns.Add("obcode");
            tblReturn.Columns.Add("pctpcode");
            tblReturn.Columns.Add("schlcode");
            tblReturn.Columns.Add("others");
            tblReturn.Columns.Add("pcasstat");
            tblReturn.Columns.Add("createby");
            tblReturn.Columns.Add("createon");

            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Finance.PCASRequest WHERE pcasstat='A' AND isissued=@isissued";
                cmd.Parameters.Add(new SqlParameter("@isissued", pIsIssued));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drwNew = tblReturn.NewRow();
                    drwNew["pcascode"] = dr["pcascode"].ToString();
                    drwNew["reason"] = dr["reason"].ToString();
                    drwNew["reqstdby"] = dr["reqstdby"].ToString();
                    drwNew["isexecut"] = dr["isexecut"].ToString();
                    drwNew["dateneed"] = Convert.ToDateTime(dr["dateneed"].ToString());
                    drwNew["pclscode"] = dr["pclscode"].ToString();
                    drwNew["obcode"] = dr["obcode"].ToString();
                    drwNew["pctpcode"] = dr["pctpcode"].ToString();
                    drwNew["schlcode"] = dr["schlcode"].ToString();
                    drwNew["others"] = dr["others"].ToString();
                    drwNew["pcasstat"] = dr["pcasstat"].ToString();
                    drwNew["createby"] = dr["createby"].ToString();
                    drwNew["createon"] = Convert.ToDateTime(dr["createon"].ToString());
                    tblReturn.Rows.Add(drwNew);
                }
                dr.Close();
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormCashier(string pIsIssued, string pUsername)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("pcascode");
            tblReturn.Columns.Add("reason");
            tblReturn.Columns.Add("dateneed");
            tblReturn.Columns.Add("isexecut");
            tblReturn.Columns.Add("reqstdby");
            tblReturn.Columns.Add("pclscode");
            tblReturn.Columns.Add("obcode");
            tblReturn.Columns.Add("pctpcode");
            tblReturn.Columns.Add("schlcode");
            tblReturn.Columns.Add("others");
            tblReturn.Columns.Add("pcasstat");
            tblReturn.Columns.Add("createby");
            tblReturn.Columns.Add("createon");

            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                //cmd.CommandText = "SELECT * FROM Finance.PCASRequest WHERE pcasstat='A' AND isissued=@isissued";
                cmd.CommandText = "SELECT * FROM Finance.PCASRequest WHERE pcasstat='A' AND isissued=@isissued AND pcascode IN (SELECT pcascode FROM Finance.PCASRequestCustodian WHERE username=@username) ORDER BY pcascode DESC";
                cmd.Parameters.Add(new SqlParameter("@isissued", pIsIssued));
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drwNew = tblReturn.NewRow();
                    drwNew["pcascode"] = dr["pcascode"].ToString();
                    drwNew["reason"] = dr["reason"].ToString();
                    drwNew["reqstdby"] = dr["reqstdby"].ToString();
                    drwNew["isexecut"] = dr["isexecut"].ToString();
                    drwNew["dateneed"] = Convert.ToDateTime(dr["dateneed"].ToString());
                    drwNew["pclscode"] = dr["pclscode"].ToString();
                    drwNew["obcode"] = dr["obcode"].ToString();
                    drwNew["pctpcode"] = dr["pctpcode"].ToString();
                    drwNew["schlcode"] = dr["schlcode"].ToString();
                    drwNew["others"] = dr["others"].ToString();
                    drwNew["pcasstat"] = dr["pcasstat"].ToString();
                    drwNew["createby"] = dr["createby"].ToString();
                    drwNew["createon"] = Convert.ToDateTime(dr["createon"].ToString());
                    tblReturn.Rows.Add(drwNew);
                }
                dr.Close();
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormPerApprover(string pSelect, string pUserName, string pWhere, int intStart, int intEnd)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("pcascode");
            tblReturn.Columns.Add("reason");
            tblReturn.Columns.Add("dateneed");
            tblReturn.Columns.Add("isexecut");
            tblReturn.Columns.Add("reqstdby");
            tblReturn.Columns.Add("pclscode");
            tblReturn.Columns.Add("obcode");
            tblReturn.Columns.Add("pctpcode");
            tblReturn.Columns.Add("schlcode");
            tblReturn.Columns.Add("others");
            tblReturn.Columns.Add("pcasstat");
            tblReturn.Columns.Add("createby");
            tblReturn.Columns.Add("createon");

            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                //SELECT * FROM ( SELECT  TOP 10 (pcascode), dateneed, reason, isexecut, reqstdby, pclscode,obcode,pctpcode,schlcode,rccode,others,pcasstat,createby, createon, ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.PCASRequest WHERE pcascode IN (SELECT DISTINCT(pcascode) FROM Finance.PCASRequest WHERE (SELECT TOP 1 username FROM Finance.PCASApproval WHERE pcasstat='0' AND pcascode=Finance.PCASRequest.pcascode ORDER BY aprvordr ASC)='jbt')) as FinanceRequest WHERE RowNum BETWEEN 1 AND 10 ORDER BY createon DESC
                //cmd.CommandText = "SELECT * FROM ( SELECT  " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.PCASRequest WHERE createby = @username) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
                cmd.CommandText = "SELECT * FROM ( SELECT  " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.PCASRequest WHERE pcascode IN (SELECT DISTINCT(pcascode) FROM Finance.PCASRequest WHERE (SELECT TOP 1 username FROM Finance.PCASApproval WHERE pcasstat='0' AND pcascode=Finance.PCASRequest.pcascode ORDER BY aprvordr ASC)=@username)) as FinanceRequest WHERE pcasstat !='D' AND RowNum BETWEEN 1 AND 10 ORDER BY createon DESC";
                cmd.Parameters.Add(new SqlParameter("@UserName", pUserName));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drwNew = tblReturn.NewRow();
                    drwNew["pcascode"] = dr["pcascode"].ToString();
                    drwNew["reason"] = dr["reason"].ToString();
                    drwNew["reqstdby"] = dr["reqstdby"].ToString();
                    drwNew["isexecut"] = dr["isexecut"].ToString();
                    drwNew["dateneed"] = Convert.ToDateTime(dr["dateneed"].ToString());
                    drwNew["pclscode"] = dr["pclscode"].ToString();
                    drwNew["obcode"] = dr["obcode"].ToString();
                    drwNew["pctpcode"] = dr["pctpcode"].ToString();
                    drwNew["schlcode"] = dr["schlcode"].ToString();
                    drwNew["others"] = dr["others"].ToString();
                    drwNew["pcasstat"] = dr["pcasstat"].ToString();
                    drwNew["createby"] = dr["createby"].ToString();
                    drwNew["createon"] = Convert.ToDateTime(dr["createon"].ToString());
                    tblReturn.Rows.Add(drwNew);
                }
                dr.Close();
            }
            return tblReturn;
        }



        public static string GetRequestStatusIcon(string pCataStatus)
        {
            string strReturn = "";
            if (pCataStatus == "0")
                strReturn = "Disapproved.png";
            else if (pCataStatus == "2")
                strReturn = "Approval.png";
            else if (pCataStatus == "1")
                strReturn = "print32.png";
            return strReturn;
        }

        public static void SendEmailNotification(string MailType, string pPCASCode, string pMailFrom, string pMailTo)
        {

            string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
            string strSubject = "";
            string strBody = "";

            if (MailType == "Approver")
            {
                strSubject = "For Your Approval: PCAS";
                strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
                                                 "There is a Petty cash request submitted by " + clsEmployee.GetName(pMailFrom) + ".<br><br>" +
                                                 "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetailsApprover.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                                                 "If you can't click on the above link,<br>" +
                                                 "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetailsApprover.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                                                 "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
            }
            else if (MailType == "Adjustment4Heads")
            {
                strSubject = "Delivered: PCAS Charging Notification";
                strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
                                                 "There is a Petty Cash submitted by " + clsEmployee.GetName(pMailFrom) + " that was charged on your RC by FPC.<br><br>" +
                                                 "<TABLE>" +
                                                 "<TR><TH>Account Expenses</TH><TH>Amount</TH></TR>" +
                                                 "</TABLE>" +
                                                 "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetailsApprover.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                                                 "If you can't click on the above link,<br>" +
                                                 "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetailsApprover.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                                                 "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
            }
            else if (MailType == "Requestor")
            {
                strSubject = "Delivered: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "Your Petty Cash Request has been successfully sent to your respective approver/s.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "Requestor")
            {
                strSubject = "Disapproved: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "Your Petty Cash Request has been Disapproved by your respective approver.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "FinanceRequestor")
            {
                //To requestor
                strSubject = "Delivered: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "Your Petty Cash Request has been successfully sent to Finance for processing.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "FinanceApprover")
            {
                //To Finance Department
                strSubject = "For Your Approval: PCAS";
                strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
                                                 "There is a Petty Cash submitted by " + clsEmployee.GetName(pMailFrom) + " for processing.<br><br>" +
                                                 "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetailsApprover.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                                                 "If you can't click on the above link,<br>" +
                                                 "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetailsApprover.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                                                 "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
            }
            else if (MailType == "FPCApproved")
            {
                strSubject = "Delivered: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "You approved a Petty Cash Request.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "RequestorFinalFPC")
            {
                //To requestor
                strSubject = "Delivered: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "Your Petty Cash Request has been successfully approved by FPC.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "CashierApprover")
            {
                strSubject = "For Your Validation: PCAS";
                strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
                                                 "There is a Petty Cash submitted by " + clsEmployee.GetName(pMailFrom) + ".<br><br>" +
                                                 "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestCashierDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                                                 "If you can't click on the above link,<br>" +
                                                 "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestCashierDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                                                 "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
            }
            else if (MailType == "CashierTagAsIssued")
            {
                strSubject = "Delivered: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "You Tag Petty Cash Request as Issued.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "CashierTagAsReady")
            {
                strSubject = "Delivered: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "You tagged the Petty Cash Request as now ready for release.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "RequestorTagAsIssued")
            {
                strSubject = "Delivered: PCAS Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "Your Petty Cash Request has been issued.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "RequestorTagAsReady")
            {
                
                strSubject = "Delivered: PCAS Request";
                //strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                //          "You may now claim your Petty Cash Request from the designated PCF custodian.<br>" +
                //          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                //          "If you can't click on the above link,<br>" +
                //          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                //          "All the best,<br>HQ Portal";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "You may now claim your Petty Cash Request from " + clsEmployee.GetName(clsPCASRequestCustodian.GetUsername(pPCASCode)) + ".<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/PCASH/PettyCashRequestDetails.aspx?pcascode=" + pPCASCode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
        }

        public static void SendEmailNotificationAdjustment(string MailType, string pMailFrom, string pMailTo,string pTable)
        {

            string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
            string strSubject = "";
            string strBody = "";

            if (MailType == "Adjustment4Heads")
            {
                strSubject = "Delivered: PCAS Charging Notification";
                strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
                                                 "There is a Petty Cash submitted by " + clsEmployee.GetName(pMailFrom) + " that was charged on your RC by FPC.<br><br>" +
                                                  pTable +
                                                  "<br><br>" +
                                                 "For more Information,<br>" +
                                                 "You may also clarify this with FPC at local 612.<br><br>" +
                                                 "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
            }
         
        }
        public static string GetCreatedBy(string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT createby FROM Finance.PCASRequest WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
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

        public static string GetPCASStatus(string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT pcasstat FROM Finance.PCASRequest WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
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

        public static string GetPCASIsIssued(string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT isissued FROM Finance.PCASRequest WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
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

        public static bool AuthenticateAccess(string pUsername, string pPCASCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cn.Open();

                cmd.CommandText = "SELECT pcascode FROM Finance.PCASRequest WHERE pcascode=@pcascode AND createby=@user";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cmd.Parameters.Add(new SqlParameter("@user", pUsername));
                SqlDataReader dr = cmd.ExecuteReader();
                blnReturn = dr.Read();

                dr.Close();
            }
            return blnReturn;
        }

        public static DataTable GetDSGMainFormApproverPerRC(string pPcasCode)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("username");
            tblReturn.Columns.Add("rccode");

            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT(username),rccode FROM Speedo.ModuleApprover WHERE applevel=1 AND modlcode='PETTY' AND rccode NOT IN (SELECT rccode FROM Finance.PCASRequest WHERE pcascode=@pcascode) AND rccode IN (SELECT rccode FROM Finance.PCASRequestAllocation WHERE pcascode=@pcascode)";
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drwNew = tblReturn.NewRow();
                    drwNew["username"] = dr["username"].ToString();
                    drwNew["rccode"] = dr["rccode"].ToString();

                    tblReturn.Rows.Add(drwNew);
                }
                dr.Close();
            }
            return tblReturn;
        }
    }
}