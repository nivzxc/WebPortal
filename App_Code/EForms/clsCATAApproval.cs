using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;

namespace STIeForms
{
    public class clsCATAApproval : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        private string _strCataCode;
        private int _intApproverOrder;
        private string _strUsername;
        private string _strApproverType;
        private string _strStatusCode;
        private DateTime _dtApprovedate;

        //private static string _strFinanceApprover = "kate.gabriana";
        //private static string _strFinanceApprover = "cy.cabunag";
	private static string _strFinanceApprover = "alex.frias";

        public string CataCode { get { return _strCataCode; } set { _strCataCode = value; } }
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
                    cmd.CommandText = "INSERT INTO Finance.CATAApproval (catacode, apvorder, username, apvtype,statcode, apvdate) VALUES(@catacode, @apvorder, @username,@apvtype, @statcode, @apvdate)";
                    cmd.Parameters.Add(new SqlParameter("@catacode", drApprovers["CataCode"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@apvorder", drApprovers["ApproverOrder"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@username", drApprovers["Username"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@apvtype", drApprovers["ApproverType"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@statcode", drApprovers["StatusCode"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@apvdate", DateTime.Now));
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
                    cmd.CommandText = "DELETE FROM Finance.CATAApproval WHERE catacode=@catacode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", _strCataCode));
                    cn.Open();
                    intReturn = cmd.ExecuteNonQuery();
                }
            }
            return intReturn;
        }

        public static int TagApproved(string pCataCode, string pUsername, string pRequestor)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Finance.CATAApproval SET statcode='1', apvdate=@apvdate WHERE catacode=@catacode AND username=@username";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@apvdate", DateTime.Now));
                    cn.Open();
                    intReturn = cmd.ExecuteNonQuery();


                    string strApproverType = GetApproverType(pUsername, pCataCode);

                    if (strApproverType == "E")
                    {
                        SendMailApproveDisApprove(pCataCode, pRequestor, pUsername, "1", false);

                        DataTable tblApproverEndorser = GetDSGForApprovalMail(pCataCode, "E");
                        if (tblApproverEndorser.Rows.Count == 0)
                        {
                            DataTable tblAuthorize = GetDSGForApprovalMail(pCataCode, "A");
                            foreach (DataRow drAuthorize in tblAuthorize.Rows)
                            {
                                clsCATARequest.SendEmailNotification("Approver", pCataCode, pRequestor, drAuthorize["username"].ToString());
                            }
                            clsCATARequest.SendEmailNotification("Requestor", pCataCode, pRequestor, pRequestor);
                        }
                    }

                    else if (strApproverType == "A")
                    {
                        SendMailApproveDisApprove(pCataCode, pRequestor, pUsername, "1", false);

                        DataTable tbApproverAuthorize = GetDSGForApprovalMail(pCataCode, "A");
                        if (tbApproverAuthorize.Rows.Count == 0)
                        {
                            //Send mail to finance
                            clsCATARequest.SendEmailNotification("FinanceApprover", pCataCode, pRequestor, _strFinanceApprover);
                            clsCATARequest.SendEmailNotification("FinanceRequestor", pCataCode, pRequestor, pRequestor);
                        }
                    }

                    else if (strApproverType == "F")
                    {
                        if (pUsername == "Treasury")
                        {
                            if (clsCATARequest.Approve(pCataCode) > 0)
                            {
                                //mail from finance
                                SendMailApproveDisApprove(pCataCode, pRequestor, _strFinanceApprover, "1", true);
                            }
                        }
                    }

                }
            }
            return intReturn;
        }

        public static int SendMailApproveDisApprove(string pCataCode, string pRequestor, string pApprover, string pStatus, bool pIsFinance)
        {
            string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
            int intReturn = 0;
            string strSubject = "";
            string strBody = "";
            string strStatus = (pStatus == "1" ? "Approved" : "Disapproved");
            if (pIsFinance == false)
            {
                //send email to requestor
                strSubject = strStatus + " CATA: " + pCataCode;
                strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
                        "Your CATA has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(pApprover) + ".<br><br>" +
                        "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
                        "If you can't click on the above link,<br>" +
                        "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                       "All the best,<br>E-Forms Administrator";
                clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

                //send email to Approver
                strSubject = strStatus + " CATA: " + pCataCode;
                strBody = "You " + strStatus.ToLower() + " a CATA.<br><br>" +
                          "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "</i><br><br>" +
                          "All the best,<br>E-Forms Administrator";
                clsSpeedo.SendMail(clsUsers.GetEmail(pApprover), strSubject, strBody);
            }

            else
            {
                //send email to requestor
                string strCheck = strStatus == "Approved" ? "Your cheque is ready for pick-up" : "disapproved";
                strSubject = strStatus + " CATA: " + pCataCode;
                strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
                        "Your CATA has been " + strStatus.ToLower() + " by Finance." + strCheck + "<br><br>" +
                        "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
                        "If you can't click on the above link,<br>" +
                        "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                       "All the best,<br>E-Forms Administrator";
                clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

                //send email to Finance Approver
                strSubject = strStatus + " CATA: " + pCataCode;
                strBody = "The Finance has " + strStatus.ToLower() + " a CATA.<br><br>" +
                          "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                          "All the best,<br>E-Forms Administrator";
                clsSpeedo.SendMail(clsUsers.GetEmail(pApprover), strSubject, strBody);

            }
            return intReturn;

        }

        public static int TagDisapproved(string pCataCode, string pUsername, string pRequestor)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {

                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Finance.CATAApproval SET statcode='2', apvdate=@apvdate WHERE catacode=@catacode AND username=@username";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@apvdate", DateTime.Now));
                    cn.Open();
                    intReturn = cmd.ExecuteNonQuery();

                    if (intReturn > 0)
                    {
                        clsCATARequest.Disapprove(pCataCode);
                        if (pUsername == "Financial Planning and Control" || pUsername == "Treasury" || pUsername == "HQ Accounting")
                        {
                            SendMailApproveDisApprove(pCataCode, pRequestor, _strFinanceApprover, "0", true);
                        }
                        else
                        {
                            SendMailApproveDisApprove(pCataCode, pRequestor, pUsername, "0", false);
                        }
                    }
                }
            }
            return intReturn;
        }

        public static DataTable GetDSG(string pCataCode)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode AS CataCode, apvorder AS ApproverOrder, apvtype AS ApproverType, username AS Username, statcode AS StatusCode, apvdate AS ApproveDate FROM Finance.CATAApproval WHERE catacode=@catacode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static int CountForApproval(string pCataCode, string pApproverType)
        {
            int intReturn = 0;
            DataTable tblCount = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode FROM Finance.CATAApproval WHERE catacode=@catacode AND statcode='0' AND apvtype=@apvtype";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@apvtype", pApproverType));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblCount);
                    intReturn = tblCount.Rows.Count;
                }
            }
            return intReturn;
        }

        public static int CountDisapprove(string pCataCode)
        {
            int intReturn = 0;
            DataTable tblCount = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode FROM Finance.CATAApproval WHERE catacode=@catacode AND statcode='2'";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblCount);
                    intReturn = tblCount.Rows.Count;
                }
            }
            return intReturn;
        }

        public static DataTable GetDSGForApprovalMail(string pCataCode, string pApproverType)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT username FROM Finance.CATAApproval WHERE catacode=@catacode AND statcode='0' AND apvtype=@apvtype";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@apvtype", pApproverType));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static DataTable GetDSGForApprovalEndorser(string pUsername)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("CataCode");
            tblReturn.Columns.Add("TripPurpose");
            tblReturn.Columns.Add("DateNeeded");
            tblReturn.Columns.Add("DateRequested");
            tblReturn.Columns.Add("RequestedBy");

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode AS CataCode, (SELECT createby FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS RequestedBy, (SELECT trpprpse FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS TripPurpose, (SELECT dateneed FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS DateNeeded,(SELECT createon FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS DateRequested  FROM Finance.CATAApproval WHERE (SELECT statcode FROM Finance.CATARequest WHERE catacode = Finance.CATAApproval.catacode)='1' AND statcode='0' AND apvtype='E' AND username=@username";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (CountDisapprove(dr["CataCode"].ToString()) == 0)
                        {
                            DataRow drNew = tblReturn.NewRow();
                            drNew["CataCode"] = dr["CataCode"].ToString();
                            drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                            drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                            drNew["DateRequested"] = dr["DateRequested"].ToString();
                            drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                            tblReturn.Rows.Add(drNew);
                        }
                    }
                }
            }
            return tblReturn;
        }

        public static DataTable GetDSGForApprovalAuthorize(string pUsername)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("CataCode");
            tblReturn.Columns.Add("TripPurpose");
            tblReturn.Columns.Add("DateNeeded");
            tblReturn.Columns.Add("DateRequested");
            tblReturn.Columns.Add("RequestedBy");

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode AS CataCode,(SELECT createby FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS RequestedBy, (SELECT trpprpse FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS TripPurpose, (SELECT dateneed FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS DateNeeded,(SELECT createon FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS DateRequested  FROM Finance.CATAApproval WHERE (SELECT statcode FROM Finance.CATARequest WHERE catacode = Finance.CATAApproval.catacode)='1' AND statcode='0' AND apvtype='A'  AND username=@username";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (CountDisapprove(dr["CataCode"].ToString()) == 0)
                        {
                            if (CountForApproval(dr["CataCode"].ToString(), "E") == 0)
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                tblReturn.Rows.Add(drNew);
                            }
                        }
                    }
                }
            }
            return tblReturn;
        }

        public static DataTable GetDSGForApprovalFinance()
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("CataCode");
            tblReturn.Columns.Add("TripPurpose");
            tblReturn.Columns.Add("DateNeeded");
            tblReturn.Columns.Add("DateRequested");
            tblReturn.Columns.Add("RequestedBy");
            tblReturn.Columns.Add("StatusCode");
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode AS CataCode,statcode AS StatusCode, createby AS RequestedBy, trpprpse AS TripPurpose, dateneed AS DateNeeded, createon AS DateRequested FROM Finance.CATARequest WHERE statcode='1' ORDER BY createon DESC";
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (CountDisapprove(dr["CataCode"].ToString()) == 0)
                        {
                            if (CountForApproval(dr["CataCode"].ToString(), "E") == 0)
                            {
                                if (CountForApproval(dr["CataCode"].ToString(), "A") == 0)
                                {
                                    DataRow drNew = tblReturn.NewRow();
                                    drNew["CataCode"] = dr["CataCode"].ToString();
                                    drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                    drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                    drNew["DateRequested"] = dr["DateRequested"].ToString();
                                    drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                    drNew["StatusCode"] = dr["StatusCode"].ToString();
                                    tblReturn.Rows.Add(drNew);
                                }
                            }
                        }
                    }
                }
            }
            return tblReturn;
        }

        public static DataTable GetDSGForApprovalFinance(DateTime pDateStart, DateTime pDateEnd, string pUsername)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("CataCode");
            tblReturn.Columns.Add("TripPurpose");
            tblReturn.Columns.Add("DateNeeded");
            tblReturn.Columns.Add("DateRequested");
            tblReturn.Columns.Add("RequestedBy");
            tblReturn.Columns.Add("StatusCode");
            tblReturn.Columns.Add("Status");
            string strStatus = "";


            strStatus = " AND statcode IN ('1','2','3','4')";


            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode AS CataCode,statcode AS StatusCode, createby AS RequestedBy, trpprpse AS TripPurpose, dateneed AS DateNeeded, createon AS DateRequested FROM Finance.CATARequest WHERE (createon BETWEEN @startdate AND @enddate) " + strStatus + " ORDER BY createon DESC";
                    cmd.Parameters.Add(new SqlParameter("@startdate", DateTime.Parse(pDateStart.ToShortDateString() + " 12:00 AM")));
                    cmd.Parameters.Add(new SqlParameter("@enddate", DateTime.Parse(pDateEnd.ToShortDateString() + " 11:59 PM")));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (pUsername == "ALL")
                        {
                            if (CountDisapprove(dr["CataCode"].ToString()) > 0)
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                drNew["StatusCode"] = dr["StatusCode"].ToString();
                                drNew["Status"] = "Disapproved";
                                tblReturn.Rows.Add(drNew);
                            }

                            else if (CountForApproval(dr["CataCode"].ToString(), "E") > 0)
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                drNew["StatusCode"] = dr["StatusCode"].ToString();
                                drNew["Status"] = "DepartmentApproval";
                                tblReturn.Rows.Add(drNew);
                            }

                            else if (CountForApproval(dr["CataCode"].ToString(), "A") > 0)
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                drNew["StatusCode"] = dr["StatusCode"].ToString();
                                drNew["Status"] = "DivisionApproval";
                                tblReturn.Rows.Add(drNew);
                            }
                            else
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                drNew["StatusCode"] = dr["StatusCode"].ToString();
                                drNew["Status"] = "ForApproval";
                                tblReturn.Rows.Add(drNew);
                            }
                        }

                       else if (pUsername == "APPROVED")
                        {
                            if (dr["StatusCode"].ToString() == "2")
                            {

                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                drNew["StatusCode"] = dr["StatusCode"].ToString();
                                drNew["Status"] = "Approved";
                                tblReturn.Rows.Add(drNew);

                            }

                        }

                        else if (pUsername == "DISAPPROVED")
                        {
                            if (dr["StatusCode"].ToString() == "4")
                            {

                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                drNew["StatusCode"] = dr["StatusCode"].ToString();
                                drNew["Status"] = "Disapproved";
                                tblReturn.Rows.Add(drNew);

                            }
                        }

                        else if (pUsername == "CANCELLED")
                        {
                            if (dr["StatusCode"].ToString() == "3")
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["CataCode"] = dr["CataCode"].ToString();
                                drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                drNew["DateRequested"] = dr["DateRequested"].ToString();
                                drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                drNew["Status"] = "Cancelled";
                                tblReturn.Rows.Add(drNew);

                            }
                        }

                        else
                        {
                            if (CountDisapprove(dr["CataCode"].ToString()) == 0)
                            {
                                if (CountForApproval(dr["CataCode"].ToString(), "E") == 0)
                                {
                                    if (CountForApproval(dr["CataCode"].ToString(), "A") == 0)
                                    {
                                        if (FinanceApprover(dr["CataCode"].ToString()) == pUsername)
                                        {
                                            DataRow drNew = tblReturn.NewRow();
                                            drNew["CataCode"] = dr["CataCode"].ToString();
                                            drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                            drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                            drNew["DateRequested"] = dr["DateRequested"].ToString();
                                            drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                            drNew["StatusCode"] = dr["StatusCode"].ToString();
                                            drNew["Status"] = "ForApproval";
                                            tblReturn.Rows.Add(drNew);
                                        }
                                        else if (FinanceApprover(dr["CataCode"].ToString()) == pUsername)
                                        {
                                            DataRow drNew = tblReturn.NewRow();
                                            drNew["CataCode"] = dr["CataCode"].ToString();
                                            drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                            drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                            drNew["DateRequested"] = dr["DateRequested"].ToString();
                                            drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                            drNew["StatusCode"] = dr["StatusCode"].ToString();
                                            drNew["Status"] = "ForApproval";
                                            tblReturn.Rows.Add(drNew);

                                        }
                                        else if (FinanceApprover(dr["CataCode"].ToString()) == pUsername)
                                        {
                                            DataRow drNew = tblReturn.NewRow();
                                            drNew["CataCode"] = dr["CataCode"].ToString();
                                            drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                                            drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                                            drNew["DateRequested"] = dr["DateRequested"].ToString();
                                            drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                                            drNew["StatusCode"] = dr["StatusCode"].ToString();
                                            drNew["Status"] = "ForApproval";
                                            tblReturn.Rows.Add(drNew);
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            return tblReturn;
        }

        public static DataTable GetDSGApprovers(string pCataCode)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT username, apvtype AS ApproverType ,statcode AS ApproverStatus, apvdate AS ApproveDate  FROM Finance.CATAApproval WHERE catacode=@catacode ORDER BY catacode DESC";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static DataTable GetDSGApproversFinance(string pCataCode)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT username, apvtype AS ApproverType ,statcode AS ApproverStatus, apvdate AS ApproveDate  FROM Finance.CATAApproval WHERE catacode=@catacode AND apvtype='F' ORDER BY apvorder";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static bool IsForApproval(string pUsername, string pCataCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT statcode FROM Finance.CATAApproval WHERE catacode=@catacode AND username=@username";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cn.Open();
                    string strStatus = cmd.ExecuteScalar().ToString();
                    if (strStatus == "0")
                    {
                        blnReturn = true;
                    }
                }
            }
            return blnReturn;
        }

        public static string FinanceApprover(string pCataCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP(1)username FROM Finance.CATAApproval WHERE catacode=@catacode AND apvtype = 'F' AND statcode='0' ORDER BY apvorder";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cn.Open();
                    try
                    {
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                    catch
                    { }
                }
            }
            return strReturn;
        }

        public static string GetApproverType(string pUsername, string pCataCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT apvtype FROM Finance.CATAApproval WHERE catacode=@catacode AND username=@username";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
            return strReturn;
        }

        public static DataTable GetDSGProcessedRequest(string pUsername, string pStatus)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("CataCode");
            tblReturn.Columns.Add("TripPurpose");
            tblReturn.Columns.Add("DateNeeded");
            tblReturn.Columns.Add("DateRequested");
            tblReturn.Columns.Add("RequestedBy");

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT catacode AS CataCode, (SELECT createby FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS RequestedBy, (SELECT trpprpse FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS TripPurpose, (SELECT dateneed FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS DateNeeded,(SELECT createon FROM Finance.CATARequest WHERE catacode=Finance.CATAApproval.catacode) AS DateRequested  FROM Finance.CATAApproval WHERE statcode=@statcode AND username=@username";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        DataRow drNew = tblReturn.NewRow();
                        drNew["CataCode"] = dr["CataCode"].ToString();
                        drNew["TripPurpose"] = dr["TripPurpose"].ToString();
                        drNew["DateNeeded"] = dr["DateNeeded"].ToString();
                        drNew["DateRequested"] = dr["DateRequested"].ToString();
                        drNew["RequestedBy"] = dr["RequestedBy"].ToString();
                        tblReturn.Rows.Add(drNew);
                    }
                }
            }
            return tblReturn;
        }

    }
}