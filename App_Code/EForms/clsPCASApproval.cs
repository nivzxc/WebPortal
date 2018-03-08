using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;

namespace STIeForms
{
    public class clsPCASApproval:IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        private string _strPCASCode;
        private int _intApproverOrder;
        private string _strUsername;
        private string _strApproverType;
        private string _strStatusCode;
        private DateTime _dtApprovedate;

        public string PCASCode { get { return _strPCASCode; } set { _strPCASCode = value; } }
        public int ApproverOrder { get { return _intApproverOrder; } set { _intApproverOrder = value; } }
        public string Username { get { return _strUsername; } set { _strUsername = value; } }
        public string ApproverType { get { return _strApproverType; } set { _strApproverType = value; } }
        public string StatusCode { get { return _strStatusCode; } set { _strStatusCode = value; } }
        public DateTime Approvedate { get { return _dtApprovedate; } set { _dtApprovedate = value; } }

        public int Insert(DataTable pApprovers)
        {
            int intReturn = 0;
            SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmd = cn.CreateCommand();
            cmd.Transaction = tran;

            try
            {

                foreach (DataRow drApprovers in pApprovers.Rows)
                {
                    cmd.CommandText = "INSERT INTO Finance.PCASApproval (pcascode, aprvordr, username, apvrtype,pcasstat) VALUES(@pcascode, @aprvordr, @username,@apvrtype, @pcasstat)";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", drApprovers["PCASCode"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@aprvordr", drApprovers["ApproverOrder"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@username", drApprovers["Username"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@apvrtype", drApprovers["ApproverType"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@pcasstat", drApprovers["StatusCode"].ToString()));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
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

        public int Delete()
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Finance.PCASApproval WHERE pcascode=@pcascode";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", _strPCASCode));
                    cn.Open();
                    intReturn = cmd.ExecuteNonQuery();
                }
            }
            return intReturn;
        }

        public static int FPCFinalApprover(string pPCASCode, string pUsername)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASRequest SET adjstmnt=@adjstmnt WHERE pcascode=@pcascode";
                cmd.Parameters.Add(new SqlParameter("@adjstmnt", pUsername));
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int TagApprovedOrNot(string pPcasCode, string pUsername,string pPcasStatus)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    if (clsPCASApproval.GetApproverType(pUsername, pPcasCode) == "E" || clsPCASApproval.GetApproverType(pUsername, pPcasCode) == "H" || clsPCASApproval.GetApproverType(pUsername, pPcasCode) == "D")
                    {
                        cmd.CommandText = "UPDATE Finance.PCASApproval SET pcasstat=@pcasstat, apvrdate=@apvrdate WHERE pcascode=@pcascode AND username=@username AND apvrtype IN ('E','H','D')";
                        cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                        cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@pcasstat", pPcasStatus));
                        cmd.Parameters.Add(new SqlParameter("@apvrdate", DateTime.Now));
                        cn.Open();
                        intReturn = cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        cmd.CommandText = "UPDATE Finance.PCASApproval SET pcasstat=@pcasstat, apvrdate=@apvrdate WHERE pcascode=@pcascode AND username=@username AND apvrtype IN ('F1','F2','F3')";
                        cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                        cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@pcasstat", pPcasStatus));
                        cmd.Parameters.Add(new SqlParameter("@apvrdate", DateTime.Now));
                        cn.Open();
                        intReturn = cmd.ExecuteNonQuery();

                        //if (clsPCASApproval.GetApproverType(pUsername, pPcasCode) == "F1")
                        //{
                        //        if (clsPCASRequest.Approve(pPcasCode) > 0)
                        //        {
                        //            //mail from finance
                        //            SendMailApproveDisApprove(pPcasCode, pRequestor, _strFinanceApprover, "1", true);
                        //        }
                        //}
                    }

                    //string strApproverType = GetApproverType(pUsername, pPcasCode);

                    //if (strApproverType == "E")
                    //{
                    //    SendMailApproveDisApprove(pPcasCode, pRequestor, pUsername, "1", false);

                    //    DataTable tblApproverEndorser = GetDSGForApprovalMail(pPcasCode, "E");
                    //    if (tblApproverEndorser.Rows.Count == 0)
                    //    {
                    //        DataTable tblAuthorize = GetDSGForApprovalMail(pPcasCode, "A");
                    //        foreach (DataRow drAuthorize in tblAuthorize.Rows)
                    //        {
                    //            clsCATARequest.SendEmailNotification("Approver", pPcasCode, pRequestor, drAuthorize["username"].ToString());
                    //        }
                    //        clsCATARequest.SendEmailNotification("Requestor", pPcasCode, pRequestor, pRequestor);
                    //    }
                    //}

                    //if (strApproverType == "H")
                    //{
                    //    SendMailApproveDisApprove(pPcasCode, pRequestor, pUsername, "1", false);

                    //    DataTable tblApproverEndorser = GetDSGForApprovalMail(pPcasCode, "H");
                    //    if (tblApproverEndorser.Rows.Count == 0)
                    //    {
                    //        DataTable tblAuthorize = GetDSGForApprovalMail(pPcasCode, "A");
                    //        foreach (DataRow drAuthorize in tblAuthorize.Rows)
                    //        {
                    //            clsCATARequest.SendEmailNotification("Approver", pPcasCode, pRequestor, drAuthorize["username"].ToString());
                    //        }
                    //        clsCATARequest.SendEmailNotification("Requestor", pPcasCode, pRequestor, pRequestor);
                    //    }
                    //}

                    //else if (strApproverType == "D")
                    //{
                    //    SendMailApproveDisApprove(pPcasCode, pRequestor, pUsername, "1", false);

                    //    DataTable tbApproverAuthorize = GetDSGForApprovalMail(pPcasCode, "A");
                    //    if (tbApproverAuthorize.Rows.Count == 0)
                    //    {
                    //        //Send mail to finance
                    //        clsCATARequest.SendEmailNotification("FinanceApprover", pPcasCode, pRequestor, _strFinanceApprover);
                    //        clsCATARequest.SendEmailNotification("FinanceRequestor", pPcasCode, pRequestor, pRequestor);
                    //    }
                    //}

                    //if (strApproverType == "F3")
                    //{

                    //        if (clsPCASRequest.Approve(pPcasCode) > 0)
                    //        {
                    //            //mail from finance
                    //            //SendMailApproveDisApprove(pPcasCode, pRequestor, _strFinanceApprover, "1", true);
                    //        }

                    //}

                }
            }
            return intReturn;
        }

        //public static int SendMailApproveDisApprove(string pCataCode, string pRequestor, string pApprover, string pStatus, bool pIsFinance)
        //{
        //    string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
        //    int intReturn = 0;
        //    string strSubject = "";
        //    string strBody = "";
        //    string strStatus = (pStatus == "1" ? "Approved" : "Disapproved");
        //    if (pIsFinance == false)
        //    {
        //        //send email to requestor
        //        strSubject = strStatus + " CATA: " + pCataCode;
        //        strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
        //                "Your CATA has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(pApprover) + ".<br><br>" +
        //                "<a href='" + strSpeedoUrl + "/FINANCE/PCAS/PCASDetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
        //                "If you can't click on the above link,<br>" +
        //                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
        //               "All the best,<br>E-Forms Administrator";
        //        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

        //        //send email to Approver
        //        strSubject = strStatus + " CATA: " + pCataCode;
        //        strBody = "You " + strStatus.ToLower() + " a CATA.<br><br>" +
        //                  "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
        //                  "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
        //                  "If you can't click on the above link,<br>" +
        //                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
        //                  "All the best,<br>E-Forms Administrator";
        //        clsSpeedo.SendMail(clsUsers.GetEmail(pApprover), strSubject, strBody);
        //    }

        //    else
        //    {
        //        //send email to requestor
        //        string strCheck = strStatus == "Approved" ? "Your cheque is ready for pick-up" : "disapproved";
        //        strSubject = strStatus + " CATA: " + pCataCode;
        //        strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
        //                "Your CATA has been " + strStatus.ToLower() + " by Finance." + strCheck + "<br><br>" +
        //                "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
        //                "If you can't click on the above link,<br>" +
        //                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
        //               "All the best,<br>E-Forms Administrator";
        //        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

        //        //send email to Finance Approver
        //        strSubject = strStatus + " CATA: " + pCataCode;
        //        strBody = "The Finance has " + strStatus.ToLower() + " a CATA.<br><br>" +
        //                  "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
        //                  "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
        //                  "If you can't click on the above link,<br>" +
        //                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
        //                  "All the best,<br>E-Forms Administrator";
        //        clsSpeedo.SendMail(clsUsers.GetEmail(pApprover), strSubject, strBody);

        //    }
        //    return intReturn;

        //}


        public static string GetApproverType(string pUsername, string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 apvrtype FROM Finance.PCASApproval WHERE pcascode=@pcascode AND username=@username AND pcasstat='0'";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
            return strReturn;
        }

        public static DataTable GetDSG(string pPcasCode)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT pcascode AS PcasCode, aprvordr AS ApproverOrder, apvrtype AS ApproverType, username AS Username, pcasstat AS StatusCode, apvrdate AS ApproveDate FROM Finance.PCASApproval WHERE pcascode=@pcascode";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static int CountForApproval(string pPcasCode, string pApproverType)
        {
            int intReturn = 0;
            DataTable tblCount = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT pcascode FROM Finance.PCASApproval WHERE pcascode=@pcascode AND pcasstat='0' AND apvrtype=@apvrtype";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                    cmd.Parameters.Add(new SqlParameter("@apvrtype", pApproverType));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblCount);
                    intReturn = tblCount.Rows.Count;
                }
            }
            return intReturn;
        }

        public static int CountForPCASApproval(string pUsername)
        {
            int intReturn = 0;
            DataTable tblCount = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    //cmd.CommandText = "SELECT COUNT(*) FROM ( SELECT  *, ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.PCASRequest WHERE pcascode IN (SELECT DISTINCT(pcascode) FROM Finance.PCASRequest WHERE (SELECT TOP 1 username FROM Finance.PCASApproval WHERE pcasstat='0' AND pcascode=Finance.PCASRequest.pcascode ORDER BY aprvordr ASC)=@username)) as FinanceRequest";
                    cmd.CommandText = "SELECT pcascode AS PCASCount FROM Finance.PCASRequest WHERE pcasstat='P' AND (SELECT TOP 1 username FROM Finance.PCASApproval WHERE pcasstat='0' AND pcascode=Finance.PCASRequest.pcascode  ORDER BY aprvordr ASC)=@username";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblCount);
                    intReturn = tblCount.Rows.Count;
                }
            }
            return intReturn;
        }

        public static int CountDisapprove(string pPcasCode)
        {
            int intReturn = 0;
            DataTable tblCount = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Finance.PCASApproval WHERE pcascode=@pcascode AND pcasstat='2'";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblCount);
                    intReturn = tblCount.Rows.Count;
                }
            }
            return intReturn;
        }


        public static DataTable GetDSGMainForm(string pPcasCode)
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Finance.PCASApproval WHERE pcascode=@pcascode ORDER BY aprvordr ASC";
                    cmd.Parameters.Add(new SqlParameter("@pcascode",pPcasCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        //public static string GetApproverType(string pPCASCode, string pUsername)
        //{
        //    string strReturn = "";
        //    using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
        //    {
        //        SqlCommand cmd = cn.CreateCommand();
        //        cmd.CommandText = "SELECT TOP 1 apvrtype FROM Finance.PCASApproval WHERE pcascode=@pcascode AND username=@username";
        //        cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
        //        cmd.Parameters.Add(new SqlParameter("@username", pPCASCode));
        //        cn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            strReturn = dr["apvrtype"].ToString();
        //        }
        //        cn.Close();
        //    }
        //    return strReturn;
        //}

        public static DataTable GetDSGForApprovalMail(string pPCASCode, string pApproverType)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT username FROM Finance.PCASApproval WHERE pcascode=@pcascode AND pcasstat='0' AND apvrtype=@apvrtype";
                    cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                    cmd.Parameters.Add(new SqlParameter("@apvrtype", pApproverType));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static string GetNextApproverUserName(string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TOP(1) username FROM finance.PCASApproval WHERE pcascode=@pcascode AND pcasstat='0' ORDER BY aprvordr ASC";
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

        public static string GetFinalFPCApproverUserName(string pPCASCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TOP(1) username FROM finance.PCASApproval WHERE pcascode=@pcascode AND apvrtype='F3' ORDER BY aprvordr ASC";
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

        public static int UpdateFPC3Approver(string pPCASCode, string pUsername)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.PCASApproval SET username=@username WHERE pcascode=@pcascode AND apvrtype='F3'";
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cmd.Parameters.Add(new SqlParameter("@pcascode", pPCASCode));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

    }


}