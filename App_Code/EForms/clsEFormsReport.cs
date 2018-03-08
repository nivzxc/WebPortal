using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;


namespace STIeForms
{
    public class clsEFormsReport : IDisposable
    {
        public clsEFormsReport()
        { }

        public void Dispose() { GC.SuppressFinalize(this); }

        //RFP
        public static DataTable RFPDSGArchives(string pUsername, DateTime dtStart, DateTime dtEnd, string pStatus, int pLimit, int pOffset, string pKeyword)
        {
            DataTable tblReturn = new DataTable();
            string strEndorseStatus1 = "";
            string strEndorseStatus2 = "";
            string strAuthorizeStatus1 = "";

            if (pStatus == "APPROVED")
            {
                pStatus = "1";
                strEndorseStatus1 = " AND endrstt1='1' ";
                strEndorseStatus2 = " AND endrstt2='1' ";
                strAuthorizeStatus1 = " AND authstat='1' ";
            }
            else if (pStatus == "DISAPPROVED")
            {
                pStatus = "0";
                strEndorseStatus1 = " AND endrstt1='0' ";
                strEndorseStatus2 = " AND endrstt2='0' ";
                strAuthorizeStatus1 = " AND authstat='0' ";
            }
            else
            {
                strEndorseStatus1 = " AND endrstt1 IN ('0','1') ";
                strEndorseStatus2 = " AND endrstt2 IN ('0','1') ";
                strAuthorizeStatus1 = " AND authstat IN ('0','1') ";
            }



            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT     RowNum, ctrlnmbr, rqstfor ,projttle, createby, createon FROM (SELECT DISTINCT  ctrlnmbr, rqstfor, projttle, createby, createon,(row_number() OVER (ORDER BY ctrlnmbr DESC)) AS RowNum  FROM Finance.RFPRequest " +
                                      "WHERE  (((endrsby1 =  @username " + strEndorseStatus1 + ") OR (endrsby2 = @username " + strEndorseStatus2 + ") OR (authrzby = @username " + strAuthorizeStatus1 + ")) AND (createon BETWEEN @dtStart AND @dtEnd) AND (rqstfor LIKE @pKeyWord OR projttle LIKE @pKeyWord OR createby LIKE @pKeyWord OR ctrlnmbr LIKE @pKeyWord))) AS DataRow " +
                                      "WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@status", pStatus));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }


            return tblReturn;

        }

        public static int RFPCountArchives(string pUsername, DateTime dtStart, DateTime dtEnd, string pStatus, string pKeyword)
        {
            int intReturn = 0;
            string strEndorseStatus1 = "";
            string strEndorseStatus2 = "";
            string strAuthorizeStatus1 = "";

            if (pStatus == "APPROVED")
            {
                pStatus = "1";
                strEndorseStatus1 = " AND endrstt1='1' ";
                strEndorseStatus2 = " AND endrstt2='1' ";
                strAuthorizeStatus1 = " AND authstat='1' ";
            }
            else if (pStatus == "DISAPPROVED")
            {
                pStatus = "0";
                strEndorseStatus1 = " AND endrstt1='0' ";
                strEndorseStatus2 = " AND endrstt2='0' ";
                strAuthorizeStatus1 = " AND authstat='0' ";
            }
            else
            {
                strEndorseStatus1 = " AND endrstt1 IN ('0','1') ";
                strEndorseStatus2 = " AND endrstt2 IN ('0','1') ";
                strAuthorizeStatus1 = " AND authstat IN ('0','1') ";
            }

           

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(ctrlnmbr) FROM Finance.RFPRequest " +
                                      "WHERE  ((endrsby1 =  @username " + strEndorseStatus1 + ") OR (endrsby2 = @username " + strEndorseStatus2 + ") OR (authrzby = @username " + strAuthorizeStatus1 + ")) AND (createon BETWEEN @dtStart AND @dtEnd) AND (rqstfor LIKE @pKeyWord OR projttle LIKE @pKeyWord OR createby LIKE @pKeyWord OR ctrlnmbr LIKE @pKeyWord)";

                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
                    cn.Open();
                    intReturn = cmd.ExecuteScalar().ToString().ToInt();
                }
            }


            return intReturn;

        }

        public static string RFPStatus(string pUsername, string pControlNumber)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.Open();
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //Endorse1 Status
                    cmd.CommandText = "SELECT endrstt1, e1aprvd FROM Finance.RFPRequest WHERE endrsby1=@username AND ctrlnmbr=@ctrlno";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@ctrlno", pControlNumber));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        strReturn = dr["endrstt1"].ToString() == "1" ? "Approved" : "Disapproved";
                        strReturn = strReturn + " (" + DateTime.Parse(dr["e1aprvd"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")";
                    }
                    cmd.Parameters.Clear();
                    dr.Close();

                    //Endorse2 Status
                    cmd.CommandText = "SELECT endrstt2, e2aprvd FROM Finance.RFPRequest WHERE endrsby2=@username AND ctrlnmbr=@ctrlno";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@ctrlno", pControlNumber));
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        strReturn = dr["endrstt2"].ToString() == "1" ? "Approved" : "Disapproved";
                        strReturn = strReturn + " (" + DateTime.Parse(dr["e2aprvd"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")";
                    }
                    cmd.Parameters.Clear();
                    dr.Close();

                    //Authorize Status
                    cmd.CommandText = "SELECT authstat, aaprdate FROM Finance.RFPRequest WHERE authrzby=@username AND ctrlnmbr=@ctrlno";
                    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@ctrlno", pControlNumber));
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        strReturn = dr["authstat"].ToString() == "1" ? "Approved" : "Disapproved";
                        strReturn = strReturn + " (" +DateTime.Parse(dr["aaprdate"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")";
                    }
                    cmd.Parameters.Clear();
                    dr.Close();

                }
            }

            return strReturn;
        }

        public static DataTable GetATW(DateTime pDateStart, DateTime pDateEnd, string pKeyword, string pStatus)
        {
            DataTable tblReturn = new DataTable();
            if (pStatus == "DISAPPROVED")
            {
                pStatus = "D";
            }
            else if (pStatus == "APPROVED")
            {
                pStatus = "A";
            }
            else
            {
                pStatus = "";
            }

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = " SELECT username,reason AS Reason,apphname,appdname,atwcode AS ATWCode, datefile AS DateFiled,status  FROM HR.ATW  WHERE createon BETWEEN @dtStart AND @dtEnd AND  status LIKE @status AND (atwcode LIKE @pKeyWord  OR username LIKE @pKeyword OR reason LIKE @pKeyword) ORDER BY atwcode DESC";
                    cmd.Parameters.Add(new SqlParameter("@status", "%" + pStatus + "%"));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", pDateStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", pDateEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }

        public static DataTable GetIAR(DateTime pDateStart, DateTime pDateEnd, string pKeyword, string pStatus)
        {
            DataTable tblReturn = new DataTable();
         
            if (pStatus == "DISAPPROVED")
            {
                pStatus = "D";
            }
            else if (pStatus == "APPROVED")
            {
                pStatus = "A";
            }
            else
            {
                pStatus = "";
            }

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT iarcode,username,createon AS DateFile,reason AS Reason,apphname,appdname  FROM HR.IAR  WHERE createon BETWEEN @dtStart AND @dtEnd AND (iarcode LIKE @pkeyword OR username LIKE @pKeyword OR reason LIKE @pKeyword) AND status LIKE @status ORDER BY iarcode DESC";
                    cmd.Parameters.Add(new SqlParameter("@status","%" + pStatus + "%"));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", pDateStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", pDateEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }


        public static DataTable GetMRCF(DateTime pDateStart, DateTime pDateEnd, string pKeyword, string pStatus)
        {
            DataTable tblReturn = new DataTable();

            if (pStatus == "DISAPPROVED")
            {
                pStatus = "D";
            }
            else if (pStatus == "APPROVED")
            {
                pStatus = "A";
            }
            else
            {
                pStatus = "";
            }
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT mrcfcode,username,intended,(SELECT rcname FROM HR.Rc WHERE rccode = CIS.Mrcf.chargeto) AS ChargeTo,(SELECT typename FROM CIS.MRCFRequestType WHERE reqtype=CIS.Mrcf.reqtype) AS RequestType,sprvcode,headcode,proccode,datereq FROM CIS.Mrcf WHERE datereq BETWEEN @dtStart AND @dtEnd AND status LIKE @status AND (mrcfcode LIKE @pKeyWord OR username LIKE @pKeyword OR intended LIKE @pKeyword) ORDER BY mrcfcode DESC";
                    cmd.Parameters.Add(new SqlParameter("@status","%" + pStatus + "%"));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", pDateStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", pDateEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }

        public static DataTable GetRequisition(DateTime pDateStart, DateTime pDateEnd, string pKeyword, string pStatus)
        {
            DataTable tblReturn = new DataTable();

            if (pStatus == "DISAPPROVED")
            {
                pStatus = "D";
            }
            else if (pStatus == "APPROVED")
            {
                pStatus = "A";
            }
            else
            {
                pStatus = "";
            }
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT requcode,datereq,username,userrem AS UserRemarks,(SELECT rcname FROM HR.Rc WHERE rccode=CIS.Requisition.rccode) AS ChargeTo,sprvcode,headcode FROM CIS.Requisition WHERE datereq BETWEEN @dtStart AND @dtEnd AND status LIKE @status AND (requcode LIKE @pKeyWord OR username LIKE @pKeyword OR userrem LIKE @pKeyword) ORDER BY requcode DESC";
                    cmd.Parameters.Add(new SqlParameter("@status", "%" + pStatus + "%"));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", pDateStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", pDateEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }

     

        public static DataTable GetLeave(DateTime pDateStart, DateTime pDateEnd, string pKeyword, string pStatus,string pApprover)
        {
            DataTable tblReturn = new DataTable();

            if (pStatus == "DISAPPROVED")
            {
                pStatus = "D";
            }
            else if (pStatus == "APPROVED")
            {
                pStatus = "A";
            }
            else
            {
                pStatus = "";
            }
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT leavcode,username,(SELECT ltdesc FROM HR.LeaveTypes WHERE leavtype=HR.Leave.leavtype) AS LeaveType,datestrt AS DateStart,dateend AS DateEnd,reason,datefile AS DateFiled,apphname FROM HR.Leave WHERE  apphname = @apphname AND datefile BETWEEN @dtStart AND @dtEnd  AND (leavcode LIKE @pKeyWord  OR reason LIKE @pKeyWord) AND leavstat LIKE @status  ORDER BY leavcode DESC";
                    cmd.Parameters.Add(new SqlParameter("@status", "%" + pStatus + "%"));
                    cmd.Parameters.Add(new SqlParameter("@apphname", pApprover ));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", pDateStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", pDateEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }

        public static DataTable GetOB(DateTime pDateStart, DateTime pDateEnd, string pKeyword, string pStatus)
        {
            DataTable tblReturn = new DataTable();
            if (pStatus == "DISAPPROVED")
            {
                pStatus = "D";
            }
            else if (pStatus == "APPROVED")
            {
                pStatus = "A";
            }
            else
            {
                pStatus = "";
            }

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT obcode,(SELECT firname + ' ' + lastname FROM Users.Users WHERE username=HR.OB.username) Requestor,reason,(SELECT CASE WHEN obtype='0' THEN 'OB within department' WHEN obtype='1' THEN 'OB for other department' END) AS OBType,(SELECT firname + ' ' + lastname FROM Users.Users WHERE username=HR.OB.apphname) AS ApproverHead,datefile FROM HR.OB  WHERE datefile BETWEEN @dtStart AND @dtEnd AND  obstat LIKE @status AND (obcode LIKE @pKeyWord  OR username LIKE @pKeyword OR reason LIKE @pKeyword) ORDER BY obcode DESC";
                    cmd.Parameters.Add(new SqlParameter("@status", "%" + pStatus + "%"));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", pDateStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", pDateEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }

        public static DataTable GetOvertime(DateTime pDateStart, DateTime pDateEnd, string pKeyword, string pStatus)
        {
            DataTable tblReturn = new DataTable();
            if (pStatus == "DISAPPROVED")
            {
                pStatus = "D";
            }
            else if (pStatus == "APPROVED")
            {
                pStatus = "A";
            }
            else
            {
                pStatus = "";
            }

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT     otcode,(SELECT firname + ' ' + lastname FROM Users.Users WHERE username=HR.Overtime.username) AS Requestor, datefile, reason,(SELECT firname + ' ' + lastname FROM Users.Users WHERE username=HR.Overtime.apphname) AS Approver, otstat FROM HR.Overtime WHERE datefile BETWEEN @dtStart AND @dtEnd AND  otstat LIKE @status AND (otcode LIKE @pKeyWord  OR username LIKE @pKeyword OR reason LIKE @pKeyword) ORDER BY otcode DESC";
                    cmd.Parameters.Add(new SqlParameter("@status", "%" + pStatus + "%"));
                    cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
                    cmd.Parameters.Add(new SqlParameter("@dtStart", pDateStart));
                    cmd.Parameters.Add(new SqlParameter("@dtEnd", pDateEnd));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }



        //ATW
        //public static DataTable ATWDSGArchives(string pUsername, DateTime dtStart, DateTime dtEnd, string pStatus, int pLimit, int pOffset, string pKeyword)
        //{
        //    DataTable tblReturn = new DataTable();
        //    string strDepartmentHead = "";
        //    string strDivisionHead = "";
        //    //string strAuthorizeStatus1 = "";

        //    if (pStatus == "APPROVED")
        //    {
        //        pStatus = "1";
        //        strDepartmentHead = " AND apphstat='1' ";
        //        strDivisionHead = " AND endrstt2='1' ";
        //    }
        //    else if (pStatus == "DISAPPROVED")
        //    {
        //        pStatus = "0";
        //        strDepartmentHead = " AND endrstt1='0' ";
        //        strDivisionHead = " AND endrstt2='0' ";
        //    }
        //    else
        //    {
        //        strDepartmentHead = " AND endrstt1 IN ('0','1') ";
        //        strDivisionHead = " AND endrstt2 IN ('0','1') ";
        //    }



        //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        //    {
        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            cmd.CommandText = "SELECT     RowNum, ctrlnmbr, rqstfor ,projttle, createby, createon FROM (SELECT DISTINCT  ctrlnmbr, rqstfor, projttle, createby, createon,(row_number() OVER (ORDER BY ctrlnmbr DESC)) AS RowNum  FROM Finance.RFPRequest " +
        //                              "WHERE  (((endrsby1 =  @username " + strEndorseStatus1 + ") OR (endrsby2 = @username " + strEndorseStatus2 + ") OR (authrzby = @username " + strAuthorizeStatus1 + ")) AND (createon BETWEEN @dtStart AND @dtEnd) AND (rqstfor LIKE @pKeyWord OR projttle LIKE @pKeyWord OR createby LIKE @pKeyWord OR ctrlnmbr LIKE @pKeyWord))) AS DataRow " +
        //                              "WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")";
        //            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
        //            cmd.Parameters.Add(new SqlParameter("@status", pStatus));
        //            cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
        //            cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
        //            cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
        //            cn.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(tblReturn);
        //        }
        //    }


        //    return tblReturn;

        //}

        //public static int ATWCountArchives(string pUsername, DateTime dtStart, DateTime dtEnd, string pStatus, string pKeyword)
        //{
        //    int intReturn = 0;
        //    string strEndorseStatus1 = "";
        //    string strEndorseStatus2 = "";
        //    string strAuthorizeStatus1 = "";

        //    if (pStatus == "APPROVED")
        //    {
        //        pStatus = "1";
        //        strEndorseStatus1 = " AND endrstt1='1' ";
        //        strEndorseStatus2 = " AND endrstt2='1' ";
        //        strAuthorizeStatus1 = " AND authstat='1' ";
        //    }
        //    else if (pStatus == "DISAPPROVED")
        //    {
        //        pStatus = "0";
        //        strEndorseStatus1 = " AND endrstt1='0' ";
        //        strEndorseStatus2 = " AND endrstt2='0' ";
        //        strAuthorizeStatus1 = " AND authstat='0' ";
        //    }
        //    else
        //    {
        //        strEndorseStatus1 = " AND endrstt1 IN ('0','1') ";
        //        strEndorseStatus2 = " AND endrstt2 IN ('0','1') ";
        //        strAuthorizeStatus1 = " AND authstat IN ('0','1') ";
        //    }



        //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        //    {
        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            cmd.CommandText = "SELECT COUNT(ctrlnmbr) FROM Finance.RFPRequest " +
        //                              "WHERE  ((endrsby1 =  @username " + strEndorseStatus1 + ") OR (endrsby2 = @username " + strEndorseStatus2 + ") OR (authrzby = @username " + strAuthorizeStatus1 + ")) AND (createon BETWEEN @dtStart AND @dtEnd) AND (rqstfor LIKE @pKeyWord OR projttle LIKE @pKeyWord OR createby LIKE @pKeyWord OR ctrlnmbr LIKE @pKeyWord)";

        //            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
        //            cmd.Parameters.Add(new SqlParameter("@pKeyWord", "%" + pKeyword + "%"));
        //            cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
        //            cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
        //            cn.Open();
        //            intReturn = cmd.ExecuteScalar().ToString().ToInt();
        //        }
        //    }


        //    return intReturn;

        //}

        //public static string ATWStatus(string pUsername, string pATWNumber)
        //{
        //    string strReturn = "";
        //    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        //    {
        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            cn.Open();
        //            //SqlDataReader dr = cmd.ExecuteReader();
        //            //Endorse1 Status
        //            cmd.CommandText = "SELECT endrstt1, e1aprvd FROM Finance.RFPRequest WHERE endrsby1=@username AND ctrlnmbr=@ctrlno";
        //            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
        //            cmd.Parameters.Add(new SqlParameter("@ctrlno", pControlNumber));
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                strReturn = dr["endrstt1"].ToString() == "1" ? "Approved" : "Disapproved";
        //                strReturn = strReturn + " (" + DateTime.Parse(dr["e1aprvd"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")";
        //            }
        //            cmd.Parameters.Clear();
        //            dr.Close();

        //            //Endorse2 Status
        //            cmd.CommandText = "SELECT endrstt2, e2aprvd FROM Finance.RFPRequest WHERE endrsby2=@username AND ctrlnmbr=@ctrlno";
        //            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
        //            cmd.Parameters.Add(new SqlParameter("@ctrlno", pControlNumber));
        //            dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                strReturn = dr["endrstt2"].ToString() == "1" ? "Approved" : "Disapproved";
        //                strReturn = strReturn + " (" + DateTime.Parse(dr["e2aprvd"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")";
        //            }
        //            cmd.Parameters.Clear();
        //            dr.Close();

        //            //Authorize Status
        //            cmd.CommandText = "SELECT authstat, aaprdate FROM Finance.RFPRequest WHERE authrzby=@username AND ctrlnmbr=@ctrlno";
        //            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
        //            cmd.Parameters.Add(new SqlParameter("@ctrlno", pControlNumber));
        //            dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                strReturn = dr["authstat"].ToString() == "1" ? "Approved" : "Disapproved";
        //                strReturn = strReturn + " (" + DateTime.Parse(dr["aaprdate"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")";
        //            }
        //            cmd.Parameters.Clear();
        //            dr.Close();

        //        }
        //    }

        //    return strReturn;
        //}
    }
}