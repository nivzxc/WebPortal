using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HRMS;

namespace STIeForms
{
    public class clsCATARequest : IDisposable
    {
        private string _strCatacode;
        private string _strSchoolCode;
        private string _strRcCode;
        private string _strOther;
        private string _strLocationFrom;
        private string _strLocationTo;
        private double _dblNumberOfDays;
        private DateTime _dteDeparture;
        private DateTime _dteArrival;
        private string _strHotelName;
        private string _strTripPurpose;
        private DateTime _dteDateNeeded;
        private double _fltCataAmount;
        private string _strRequestedBy;
        private string _strAquiremode;
        private string _strObCode;
        private string _strStatus;
        private string _strCreateBy;
        private DateTime _dteCreateOn;
        private string _strModifyBy;
        private DateTime _dteModifyOn;
        private DataTable _dtbIncidentals;
        private DataTable _dtbRepresentation;

        public string CataCode { get { return _strCatacode; } set { _strCatacode = value; } }
        public string AcquireMode { get { return _strAquiremode; } set { _strAquiremode = value; } }
        public string SchoolCode { get { return _strSchoolCode; } set { _strSchoolCode = value; } }
        public string RcCode { get { return _strRcCode; } set { _strRcCode = value; } }
        public string Other { get { return _strOther; } set { _strOther = value; } }
        public string LocationFrom { get { return _strLocationFrom; } set { _strLocationFrom = value; } }
        public string LocationTo { get { return _strLocationTo; } set { _strLocationTo = value; } }
        public double NumberOfDays { get { return _dblNumberOfDays; } set { _dblNumberOfDays = value; } }
        public DateTime Departure { get { return _dteDeparture; } set { _dteDeparture = value; } }
        public DateTime Arrival { get { return _dteArrival; } set { _dteArrival = value; } }
        public string HotelName { get { return _strHotelName; } set { _strHotelName = value; } }
        public string TripPurpose { get { return _strTripPurpose; } set { _strTripPurpose = value; } }
        public DateTime DateNeeded { get { return _dteDateNeeded; } set { _dteDateNeeded = value; } }
        public double CataAmount { get { return _fltCataAmount; } set { _fltCataAmount = value; } }
        public string RequestedBy { get { return _strRequestedBy; } set { _strRequestedBy = value; } }
        public string ObCode { get { return _strObCode; } set { _strObCode = value; } }
        public string Status { get { return _strStatus; } set { _strStatus = value; } }
        public string CreateBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
        public DateTime CreateOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
        public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
        public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }
        public DataTable Incidentals { get { return _dtbIncidentals; } set { _dtbIncidentals = value; } }
        public DataTable Representation { get { return _dtbRepresentation; } set { _dtbRepresentation = value; } }

        public void Dispose() { GC.SuppressFinalize(this); }

        public clsCATARequest()
        {
            _strCatacode = "";
            _strSchoolCode = "";
            _strRcCode = "";
            _strOther = "";
            _strLocationFrom = "";
            _strLocationTo = "";
            _dblNumberOfDays = 0.0;
            _dteDeparture = DateTime.Now;
            _dteArrival = DateTime.Now;
            _strHotelName = "";
            _strTripPurpose = "";
            _dteDateNeeded = DateTime.Now;
            _fltCataAmount = 0.00;
            _strAquiremode = "";
            _strRequestedBy = "";
            _strStatus = "";
            _strCreateBy = "";
            _dteCreateOn = DateTime.Now;
            _strModifyBy = "";
            _dteModifyOn = DateTime.Now;
        }

        public void Fill()
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode,obcode, schlcode, rccode,others, lctnfrom,lctnto,acqrmode, nmbrdays,deprture,arrival,hotelnme,trpprpse,dateneed,cataamnt,rqstby, " +
                 "statcode,createby,createon FROM Finance.CATARequest WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _strCatacode = dr["catacode"].ToString();
                    _strObCode = dr["obcode"].ToString();
                    _strSchoolCode = dr["schlcode"].ToString();
                    _strRcCode = dr["rccode"].ToString(); ;
                    _strOther = dr["others"].ToString();
                    _strLocationFrom = dr["lctnfrom"].ToString();
                    _strLocationTo = dr["lctnto"].ToString();
                    _dblNumberOfDays = double.Parse(dr["nmbrdays"].ToString());
                    _dteDeparture = Convert.ToDateTime(dr["deprture"].ToString());
                    _dteArrival = Convert.ToDateTime(dr["arrival"].ToString());
                    _strHotelName = dr["hotelnme"].ToString();
                    _strTripPurpose = dr["trpprpse"].ToString();
                    _dteDateNeeded = Convert.ToDateTime(dr["dateneed"].ToString());
                    _fltCataAmount = double.Parse(dr["cataamnt"].ToString());
                    _strAquiremode = dr["acqrmode"].ToString();
                    _strRequestedBy = dr["rqstby"].ToString();

                    _strStatus = dr["statcode"].ToString();
                    _strCreateBy = dr["createby"].ToString();
                    _dteCreateOn = Convert.ToDateTime(dr["createon"].ToString());
                }
            }
        }

        public int Insert(DataTable pCataRequest, DataTable pApprovers, DataTable pIncidentals, DataTable pRepresentation, DataTable pTerminalFee)
        {
            int intIncidental = 0;
            int intReturn = 0;
            int intSeed = 0;
            int intMonth = 0;
            int intYear = 0;
            string strMonth = "";
            DateTime dtDateToday = DateTime.Now;
            //DateTime dtDateToday = DateTime.Parse("2/1/2013");

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
                    cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=0 Where xkey='CataNumber'";
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

                        cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=0 Where xkey='CataNumber'";
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }


                //if (Convert.ToInt32(DateTime.Now.Month.ToString()) == intMonth)
                //{
                //    strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
                //}
                //else
                //{
                //    intMonth = intMonth + 1;
                //    cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
                //    cmd.Parameters.Add(new SqlParameter("@xvalue", Convert.ToInt32(DateTime.Now.Month.ToString())));
                //    intReturn = cmd.ExecuteNonQuery();
                //    cmd.Parameters.Clear();

                //    strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
                //    cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=0 Where xkey='CataNumber'";
                //    intReturn = cmd.ExecuteNonQuery();
                //    cmd.Parameters.Clear();
                //}

                cmd.CommandText = "SELECT xvalue FROM Finance.CataPrimaryKey Where xkey='CataNumber'";
                intSeed = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

                cmd.CommandText = "UPDATE Finance.CataPrimaryKey SET xvalue=@xvalue Where xkey='CataNumber'";
                cmd.Parameters.Add(new SqlParameter("@xvalue", intSeed));
                intReturn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                _strCatacode = (intYear + "-" + strMonth + "-" + ("000" + intSeed.ToString()).Substring(intSeed.ToString().Length));

                cmd.CommandText = "INSERT INTO Finance.CataRequest VALUES(@catacode, @obcode,@schlcode, @rccode, @others, @lctnfrom, @lctnto,@nmbrdays, @deprture,@arrival,@hotelnme,@trpprpse,@dateneed,@acqrmode,@cataamnt,@rqstby,@statcode,@createby,@createon,@modifyby,@modifyon)";
                cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                cmd.Parameters.Add(new SqlParameter("@obcode", _strObCode));
                cmd.Parameters.Add(new SqlParameter("@schlcode", _strSchoolCode));
                cmd.Parameters.Add(new SqlParameter("@rccode", _strRcCode));
                cmd.Parameters.Add(new SqlParameter("@others", _strOther));
                cmd.Parameters.Add(new SqlParameter("@lctnfrom", _strLocationFrom));
                cmd.Parameters.Add(new SqlParameter("@lctnto", _strLocationTo));
                cmd.Parameters.Add(new SqlParameter("@nmbrdays", _dblNumberOfDays));
                cmd.Parameters.Add(new SqlParameter("@deprture", _dteDeparture));
                cmd.Parameters.Add(new SqlParameter("@arrival", _dteArrival));
                cmd.Parameters.Add(new SqlParameter("@hotelnme", _strHotelName));
                cmd.Parameters.Add(new SqlParameter("@trpprpse", _strTripPurpose));
                cmd.Parameters.Add(new SqlParameter("@dateneed", _dteDateNeeded));
                cmd.Parameters.Add(new SqlParameter("@cataamnt", _fltCataAmount));
                cmd.Parameters.Add(new SqlParameter("@rqstby", _strRequestedBy));
                cmd.Parameters.Add(new SqlParameter("@acqrmode", _strAquiremode));
                cmd.Parameters.Add(new SqlParameter("@statcode", _strStatus));
                cmd.Parameters.Add(new SqlParameter("@modifyby", _strModifyBy));
                cmd.Parameters.Add(new SqlParameter("@modifyon", DateTime.Parse("1/1/1990")));
                cmd.Parameters.Add(new SqlParameter("@createby", _strCreateBy));
                cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now));

                intReturn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();



                if (pCataRequest != null)
                {
                    //Add CATARequestDetails
                    foreach (DataRow drwRequestDetails in pCataRequest.Rows)
                    {
                        cmd.CommandText = "INSERT INTO Finance.CATADetails VALUES (@catacode, @stypcode, @amount)";
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@stypcode", drwRequestDetails["setcode"].ToString().Trim()));
                        cmd.Parameters.Add(new SqlParameter("@amount", Convert.ToDouble(drwRequestDetails["amount"].ToString())));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                if (pIncidentals != null)
                {
                    //add CATAIncidentals
                    foreach (DataRow drwIncidentals in pIncidentals.Rows)
                    {
                        cmd.CommandText = "SELECT xvalue FROM Finance.CATAPrimaryKey WHERE xkey='IncidentalNumber'";
                        intIncidental = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

                        cmd.CommandText = "UPDATE Finance.CATAPrimaryKey SET xvalue=@xvalue WHERE xkey='IncidentalNumber'";
                        cmd.Parameters.Add(new SqlParameter("@xvalue", intIncidental));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "INSERT INTO Finance.CATAIncedental VALUES (@ncdtcode,@catacode, @incdental, @amount)";
                        cmd.Parameters.Add(new SqlParameter("@ncdtcode", intIncidental));
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@incdental", drwIncidentals["incdental"].ToString().Trim()));
                        cmd.Parameters.Add(new SqlParameter("@amount", Convert.ToDouble(drwIncidentals["amount"].ToString())));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        intIncidental = 0;
                    }
                }

                if (pRepresentation != null)
                {
                    //Add CATARepresentation
                    foreach (DataRow drwRepresentation in pRepresentation.Rows)
                    {
                        cmd.CommandText = "INSERT INTO Finance.CATARepresentation VALUES(@catacode, @rprsnttn)";
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@rprsnttn", drwRepresentation["rprsnttn"].ToString().Trim()));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }


                if (pApprovers.Rows.Count > 0)
                {

                    foreach (DataRow drApprovers in pApprovers.Rows)
                    {
                        cmd.CommandText = "INSERT INTO Finance.CATAApproval (catacode, apvorder, username, apvtype,statcode, apvdate) VALUES(@catacode, @apvorder, @username,@apvtype, @statcode, @apvdate)";
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@apvorder", drApprovers["ApproverOrder"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@username", drApprovers["Username"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@apvtype", drApprovers["ApproverType"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@statcode", drApprovers["StatusCode"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@apvdate", DateTime.Now));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                if (pTerminalFee.Rows.Count > 0)
                {
                    foreach (DataRow drTerminalFee in pTerminalFee.Rows)
                    {
                        cmd.CommandText = "INSERT INTO Finance.CATATerminalFee ( catacode, TerminalFeeCode, TerminalRate, CreateBy, CreateOn, ModifyBy, ModifyOn) VALUES(@catacode, @TerminalFeeCode, @TerminalRate, @CreateBy, @CreateOn, @ModifyBy, @ModifyOn)";
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@TerminalFeeCode", drTerminalFee["TerminalFeeCode"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@TerminalRate", drTerminalFee["TerminalRate"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", _strModifyBy));
                        cmd.Parameters.Add(new SqlParameter("@ModifyOn", DateTime.Parse("1/1/1990")));
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", _strCreateBy));
                        cmd.Parameters.Add(new SqlParameter("@CreateOn", DateTime.Now));

                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                tran.Commit();
            }
            catch { tran.Rollback(); }
            finally { cn.Close(); }

            if (intReturn > 0)
            {
                if (_strStatus == "1")
                {
                    DataTable tblApproversEndorser = clsCATAApproval.GetDSGForApprovalMail(_strCatacode, "E");
                    DataTable tblApproversAuthorize = clsCATAApproval.GetDSGForApprovalMail(_strCatacode, "A");
                    if (tblApproversEndorser.Rows.Count > 0)
                    {
                        foreach (DataRow drEndorser in tblApproversEndorser.Rows)
                        {
                            SendEmailNotification("Approver", _strCatacode, _strCreateBy, drEndorser["username"].ToString());
                        }
                        SendEmailNotification("Requestor", _strCatacode, _strCreateBy, _strCreateBy);
                    }
                    else
                    {
                        foreach (DataRow drAuthorize in tblApproversAuthorize.Rows)
                        {
                            SendEmailNotification("Approver", _strCatacode, _strCreateBy, drAuthorize["username"].ToString());
                        }
                        SendEmailNotification("Requestor", _strCatacode, _strCreateBy, _strCreateBy);
                    }

                }
            }

            return intReturn;
        }

        public int Update(DataTable pCataRequest, DataTable pApprovers, DataTable pIncidentals, DataTable pRepresentation, DataTable pTerminalFee)
        {
            int intIncidental = 0;
            int intReturn = 0;
            SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmd = cn.CreateCommand();
            cmd.Transaction = tran;

            try
            {
                cmd.CommandText = "UPDATE Finance.CataRequest SET obcode=@obcode,schlcode=@schlcode, rccode=@rccode, others=@others,acqrmode=@acqrmode, lctnfrom=@lctnfrom,lctnto=@lctnto,nmbrdays=@nmbrdays,deprture=@deprture,arrival=@arrival,hotelnme=@hotelnme,trpprpse=@trpprpse,dateneed=@dateneed,cataamnt=@cataamnt,statcode=@statcode, modifyby=@modifyby, modifyon=@modifyon WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                cmd.Parameters.Add(new SqlParameter("@obcode", _strObCode));
                cmd.Parameters.Add(new SqlParameter("@schlcode", _strSchoolCode));
                cmd.Parameters.Add(new SqlParameter("@rccode", _strRcCode));
                cmd.Parameters.Add(new SqlParameter("@others", _strOther));
                cmd.Parameters.Add(new SqlParameter("@lctnfrom", _strLocationFrom));
                cmd.Parameters.Add(new SqlParameter("@lctnto", _strLocationTo));
                cmd.Parameters.Add(new SqlParameter("@nmbrdays", _dblNumberOfDays));
                cmd.Parameters.Add(new SqlParameter("@deprture", _dteDeparture));
                cmd.Parameters.Add(new SqlParameter("@arrival", _dteArrival));
                cmd.Parameters.Add(new SqlParameter("@hotelnme", _strHotelName));
                cmd.Parameters.Add(new SqlParameter("@acqrmode", _strAquiremode));
                cmd.Parameters.Add(new SqlParameter("@trpprpse", _strTripPurpose));
                cmd.Parameters.Add(new SqlParameter("@dateneed", _dteDateNeeded));
                cmd.Parameters.Add(new SqlParameter("@cataamnt", _fltCataAmount));
                cmd.Parameters.Add(new SqlParameter("@modifyby", _strModifyBy));
                cmd.Parameters.Add(new SqlParameter("@modifyon", _dteModifyOn));
                cmd.Parameters.Add(new SqlParameter("@statcode", _strStatus));
                intReturn = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                //delete previous value of Catacode
                cmd.CommandText = "DELETE FROM Finance.CATADetails WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                //delete previous value of Catacode
                cmd.CommandText = "DELETE FROM Finance.CATAIncedental WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                //delete previous catacode value
                cmd.CommandText = "DELETE FROM Finance.CATARepresentation WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                if (pCataRequest != null)
                {
                    //Add CATARequestDetails
                    foreach (DataRow drwRequestDetails in pCataRequest.Rows)
                    {
                        cmd.CommandText = "INSERT INTO Finance.CATADetails VALUES (@catacode, @stypcode, @amount)";
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@stypcode", drwRequestDetails["setcode"].ToString().Trim()));
                        cmd.Parameters.Add(new SqlParameter("@amount", Convert.ToDouble(drwRequestDetails["amount"].ToString())));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                if (pIncidentals != null)
                {
                    //add CATAIncidentals
                    foreach (DataRow drwIncidentals in pIncidentals.Rows)
                    {

                        cmd.CommandText = "SELECT xvalue FROM Finance.CATAPrimaryKey WHERE xkey='IncidentalNumber'";
                        intIncidental = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

                        cmd.CommandText = "UPDATE Finance.CATAPrimaryKey SET xvalue=@xvalue WHERE xkey='IncidentalNumber'";
                        cmd.Parameters.Add(new SqlParameter("@xvalue", intIncidental));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "SELECT xvalue FROM Finance.CATAPrimaryKey WHERE xkey='IncidentalNumber'";
                        intIncidental = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

                        cmd.CommandText = "UPDATE Finance.CATAPrimaryKey SET xvalue=@xvalue WHERE xkey='IncidentalNumber'";
                        cmd.Parameters.Add(new SqlParameter("@xvalue", intIncidental));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "INSERT INTO Finance.CATAIncedental VALUES (@ncdtcode,@catacode, @incdental, @amount)";
                        cmd.Parameters.Add(new SqlParameter("@ncdtcode", intIncidental));
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@incdental", drwIncidentals["incdental"].ToString().Trim()));
                        cmd.Parameters.Add(new SqlParameter("@amount", Convert.ToDouble(drwIncidentals["amount"].ToString())));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        intIncidental = 0;
                    }
                }

                if (pRepresentation != null)
                {
                    //Add CATARepresentation
                    foreach (DataRow drwRepresentation in pRepresentation.Rows)
                    {
                        cmd.CommandText = "INSERT INTO Finance.CATARepresentation VALUES(@catacode, @rprsnttn)";
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@rprsnttn", drwRepresentation["rprsnttn"].ToString().Trim()));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }


            

                if (pApprovers.Rows.Count > 0)
                {
                    cmd.CommandText = "DELETE FROM Finance.CATAApproval WHERE catacode=@catacode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    foreach (DataRow drApprovers in pApprovers.Rows)
                    {
                        cmd.CommandText = "INSERT INTO Finance.CATAApproval (catacode, apvorder, username, apvtype,statcode, apvdate) VALUES(@catacode, @apvorder, @username,@apvtype, @statcode, @apvdate)";
                        cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                        cmd.Parameters.Add(new SqlParameter("@apvorder", drApprovers["ApproverOrder"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@username", drApprovers["Username"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@apvtype", drApprovers["ApproverType"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@statcode", drApprovers["StatusCode"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@apvdate", DateTime.Now));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }


                cmd.CommandText = "DELETE FROM Finance.CATATerminalFee WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                foreach (DataRow drTerminalFee in pTerminalFee.Rows)
                {
                    cmd.CommandText = "INSERT INTO Finance.CATATerminalFee (catacode, TerminalFeeCode, TerminalRate, CreateBy, CreateOn, ModifyBy, ModifyOn) VALUES(@catacode, @TerminalFeeCode, @TerminalRate, @CreateBy, @CreateOn, @ModifyBy, @ModifyOn)";
                    cmd.Parameters.Add(new SqlParameter("@catacode", _strCatacode));
                    cmd.Parameters.Add(new SqlParameter("@TerminalFeeCode", drTerminalFee["TerminalFeeCode"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@TerminalRate", drTerminalFee["TerminalRate"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ModifyBy", _strModifyBy));
                    cmd.Parameters.Add(new SqlParameter("@ModifyOn", DateTime.Parse("1/1/1990")));
                    cmd.Parameters.Add(new SqlParameter("@CreateBy", _strCreateBy));
                    cmd.Parameters.Add(new SqlParameter("@CreateOn", DateTime.Now));

                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                tran.Commit();
            }

            catch
            { tran.Rollback(); }
            finally { cn.Close(); }

            if (intReturn > 0)
            {
                if (_strStatus == "1")
                {
                    DataTable tblApproversEndorser = clsCATAApproval.GetDSGForApprovalMail(_strCatacode, "E");
                    DataTable tblApproversAuthorize = clsCATAApproval.GetDSGForApprovalMail(_strCatacode, "A");
                    if (tblApproversEndorser.Rows.Count > 0)
                    {
                        foreach (DataRow drEndorser in tblApproversEndorser.Rows)
                        {
                            SendEmailNotification("Approver", _strCatacode, GetRequestor(_strCatacode), drEndorser["username"].ToString());
                        }
                        SendEmailNotification("Requestor", _strCatacode, _strCreateBy, _strCreateBy);
                    }
                    else
                    {
                        foreach (DataRow drAuthorize in tblApproversAuthorize.Rows)
                        {
                            SendEmailNotification("Approver", _strCatacode, GetRequestor(_strCatacode), drAuthorize["username"].ToString());
                        }
                        SendEmailNotification("Requestor", _strCatacode, GetRequestor(_strCatacode), _strCreateBy);
                    }

                }
            }

            return intReturn;
        }

        public static DataTable GetDSGMainFormPerUserTOP10(string pUsername)
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TOP 10 (catacode), trpprpse, dateneed, statcode, createon, createby FROM Finance.CATARequest WHERE createby=@createby ORDER BY createon DESC";
                cmd.Parameters.Add(new SqlParameter("@createby", pUsername));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormPerUser(string pUsername)
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode, trpprpse, dateneed, statcode, createon, createby FROM Finance.CATARequest WHERE createby=@createby ORDER BY createon DESC";
                cmd.Parameters.Add(new SqlParameter("@createby", pUsername));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormFinanceTOP10()
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT top 10 (catacode), trpprpse, dateneed, statcode, createon, createby FROM Finance.CATARequest ORDER BY createon DESC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetDSGMainFormFinance()
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode, trpprpse, dateneed,statcode, createon, createby FROM Finance.CATARequest ORDER BY createon DESC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
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

        public static bool IfApproved(string pCatacode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode FROM Finance.CATARequest WHERE endrstt1='1' OR endrstt2='1' OR authstt1='1' OR authstt2='1' AND catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCatacode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                blnReturn = dr.Read();
                dr.Close();
            }
            return blnReturn;
        }

        public static bool IfDisapproved(string pCatacode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode FROM Finance.CATARequest WHERE endrstt1='0' OR endrstt2='0' OR authstt1='1' OR authstt2='1' AND catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCatacode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                blnReturn = dr.Read();
                dr.Close();
            }
            return blnReturn;
        }

        public static DataTable GetDSGMainFormApproverTOP10(string pUsername)
        {
            DataTable tblReturn = new DataTable();

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TOP 10 catacode, trpprpse, dateneed, createon, createby, authstt1, authstt2, endrstt1, endrstt2  FROM Finance.CATARequest WHERE statcode='2' AND (endrsby1=@username OR endrsby2=@username OR authrzby=@username) ORDER BY createon, dateneed desc";
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static void SendEmailNotification(string MailType, string pCatacode, string pMailFrom, string pMailTo)
        {

            string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
            string strSubject = "";
            string strBody = "";

            if (MailType == "Approver")
            {

                strSubject = "For Your Approval: CATA";
                strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
                                                 "There is a CATA submitted by " + clsEmployee.GetName(pMailFrom) + ".<br><br>" +
                                                 "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCatacode + "'>Click here to view the request</a><br><br>" +
                                                 "If you can't click on the above link,<br>" +
                                                 "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCatacode + "</i><br><br>" +
                                                 "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
            }
            else if (MailType == "Requestor")
            {
                strSubject = "Delivered: CATA Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "Your CATA Request has been successfully sent to your respective approvers.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCatacode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCatacode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "FinanceRequestor")
            {
                //To requestor
                strSubject = "Delivered: CATA Request";
                strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
                          "Your CATA Request has been successfully sent to Finance for processing.<br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCatacode + "'>Click here to view the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCatacode + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
            }
            else if (MailType == "FinanceApprover")
            {
                //To Finance Department
                strSubject = "For Your Approval: CATA";
                strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
                                                 "There is a CATA submitted by " + clsEmployee.GetName(pMailFrom) + " for processing.<br><br>" +
                                                 "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApproverFinance.aspx?catacode=" + pCatacode + "'>Click here to view the request</a><br><br>" +
                                                 "If you can't click on the above link,<br>" +
                                                 "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCatacode + "</i><br><br>" +
                                                 "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
            }
        }

        public static int Approve(string pCataCode, string pApproverType, string pStatus, string pMailTo)
        {
            string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
            int intReturn = 0;
            string strSubject = "";
            string strBody = "";
            string strStatus = (pStatus == "1" ? "Approved" : "Disapproved");

            SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
            cn.Open();
            //SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmd = cn.CreateCommand();
            //cmd.Transaction = tran;

            try
            {
                //if disApproved, update request status
                if (pStatus == "0")
                {
                    cmd.CommandText = "UPDATE Finance.CATARequest SET statcode=@statcode WHERE catacode=@CATACode";
                    cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                if (pApproverType == "endrsby1")
                {
                    cmd.CommandText = "UPDATE Finance.CATARequest SET endrstt1=@endrstt1,e1aprvd=@e1aprvd WHERE catacode=@CATACode";
                    cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@e1aprvd", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@endrstt1", pStatus));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (!clsFinanceApprover.IsHave2ndEndorser("catacode", pCataCode, "CATARequest"))
                    {
                        if (intReturn > 0)
                        {
                            //send email to requestor
                            strSubject = strStatus + " CATA";
                            strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
                                    "Your CATA has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsCATARequest.GetApprover(pCataCode, pApproverType)) + ".<br><br>" +
                                    "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
                                    "If you can't click on the above link,<br>" +
                                    "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                   "All the best,<br>Head Office Portal";
                            clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

                            //send email to endorser
                            strSubject = strStatus + " CATA";
                            strBody = "You " + strStatus.ToLower() + " a CATA.<br><br>" +
                                      "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
                                      "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                      "If you can't click on the above link,<br>" +
                                      "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                      "All the best,<br>Head Office Portal";
                            clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

                            if (pStatus == "1")
                            {
                                //send email to authorizer1
                                strSubject = "For Your Approval: CATA";
                                strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetAuthorize1(pCataCode)) + ",<br><br>" +
                                          "There is a CATA submitted by " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ".<br><br>" +
                                          "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                          "If you can't click on the above link,<br>" +
                                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                          "All the best,<br>Head Office Portal";
                                clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize1(pCataCode)), strSubject, strBody);

                                if (clsFinanceApprover.IsHave2ndAuthority("catacode", pCataCode, "CATARequest"))
                                {
                                    //send email to authorizer2
                                    strSubject = "For Your Approval: CATA";
                                    strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetAuthorize2(pCataCode)) + ",<br><br>" +
                                              "There is a CATA submitted by " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ".<br><br>" +
                                              "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                              "If you can't click on the above link,<br>" +
                                              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                              "All the best,<br>Head Office Portal";

                                    clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize2(pCataCode)), strSubject, strBody);
                                }
                            }
                        }
                    }
                    else
                    {
                        //send email to requestor
                        strSubject = strStatus + " CATA";
                        strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
                                "Your CATA has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsCATARequest.GetApprover(pCataCode, pApproverType)) + ".<br><br>" +
                                "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
                                "If you can't click on the above link,<br>" +
                                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                               "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

                        //send email to endorser
                        strSubject = strStatus + " CATA";
                        strBody = "You " + strStatus.ToLower() + " a CATA.<br><br>" +
                                  "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                  "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

                        if (pStatus == "1")
                        {
                            if (clsCATARequest.IsApprovedbyEndorser2(pCataCode))
                            {
                                //send email to authorizer1
                                strSubject = "For Your Approval: CATA";
                                strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetAuthorize1(pCataCode)) + ",<br><br>" +
                                          "There is a CATA submitted by " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ".<br><br>" +
                                          "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                          "If you can't click on the above link,<br>" +
                                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                          "All the best,<br>Head Office Portal";
                                clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize1(pCataCode)), strSubject, strBody);

                                if (clsFinanceApprover.IsHave2ndAuthority("catacode", pCataCode, "CATARequest"))
                                {
                                    //send email to authorizer2
                                    strSubject = "For Your Approval: CATA";
                                    strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetAuthorize2(pCataCode)) + ",<br><br>" +
                                              "There is a CATA submitted by " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ".<br><br>" +
                                              "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                              "If you can't click on the above link,<br>" +
                                              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                              "All the best,<br>Head Office Portal";

                                    clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize2(pCataCode)), strSubject, strBody);
                                }
                            }
                        }
                    }
                }

                else if (pApproverType == "endrsby2")
                {
                    cmd.CommandText = "UPDATE Finance.CATARequest SET e2aprvd=@e2aprvd,endrstt2=@endrstt2 WHERE catacode=@CATACode";
                    cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@e2aprvd", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@endrstt2", pStatus));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (intReturn > 0)
                    {
                        //send email to requestor
                        strSubject = strStatus + " CATA";
                        strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
                                "Your CATA has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsCATARequest.GetApprover(pCataCode, pApproverType)) + ".<br><br>" +
                                "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
                                "If you can't click on the above link,<br>" +
                                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                               "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

                        //send email to endorser
                        strSubject = strStatus + " CATA";
                        strBody = "You " + strStatus.ToLower() + " a CATA.<br><br>" +
                                  "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                  "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

                        if (pStatus == "1")
                        {


                            if (clsCATARequest.IsApprovedbyEndorser1(pCataCode))
                            {
                                //send email to authorizer1
                                strSubject = "For Your Approval: CATA";
                                strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetAuthorize1(pCataCode)) + ",<br><br>" +
                                          "There is a CATA submitted by " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ".<br><br>" +
                                          "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                          "If you can't click on the above link,<br>" +
                                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                          "All the best,<br>Head Office Portal";
                                clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize1(pCataCode)), strSubject, strBody);

                                if (clsFinanceApprover.IsHave2ndAuthority("catacode", pCataCode, "CATARequest"))
                                {
                                    //send email to authorizer2
                                    strSubject = "For Your Approval: CATA";
                                    strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetAuthorize2(pCataCode)) + ",<br><br>" +
                                              "There is a CATA submitted by " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ".<br><br>" +
                                              "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                              "If you can't click on the above link,<br>" +
                                              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetailsApprover.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                              "All the best,<br>Head Office Portal";

                                    clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize2(pCataCode)), strSubject, strBody);
                                }
                            }


                        }
                    }
                }

                else if (pApproverType == "autrzby1")
                {
                    cmd.CommandText = "UPDATE Finance.CATARequest SET authstt1=@authstt1, a1aprvd=@a1aprvd WHERE catacode=@CATACode";
                    cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@a1aprvd", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@authstt1", pStatus));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (intReturn > 0)
                    {

                        //send email to requestor
                        strSubject = strStatus + " CATA";
                        strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
                                "Your CATA has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsCATARequest.GetApprover(pCataCode, pApproverType)) + ".<br><br>" +
                                "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
                                "If you can't click on the above link,<br>" +
                                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                               "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

                        //send email to authorizer
                        strSubject = strStatus + " CATA";
                        strBody = "You " + strStatus.ToLower() + " a CATA.<br><br>" +
                                  "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                  "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize1(pCataCode)), strSubject, strBody);

                        if (pStatus == "1")
                        {
                            if (clsFinanceApprover.IsHave2ndAuthority("catacode", pCataCode, "CATARequest"))
                            {
                                if (clsCATARequest.IsApprovedbyAuthorizeby2(pCataCode))
                                {
                                    cmd.CommandText = "UPDATE Finance.CATARequest SET statcode=@statcode WHERE catacode=@CATACode";
                                    cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                                    cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
                                    intReturn = cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                            }
                            else
                            {
                                cmd.CommandText = "UPDATE Finance.CATARequest SET statcode=@statcode WHERE catacode=@CATACode";
                                cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                                cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
                                intReturn = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                        }
                    }
                }

                else if (pApproverType == "autrzby2")
                {
                    cmd.CommandText = "UPDATE Finance.CATARequest SET authstt2=@authstt2, a2aprvd=@a2aprvd WHERE catacode=@CATACode";
                    cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                    cmd.Parameters.Add(new SqlParameter("@a2aprvd", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@authstt2", pStatus));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (intReturn > 0)
                    {


                        //send email to requestor
                        strSubject = strStatus + " CATA";
                        strBody = "Hi " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + ",<br><br>" +
                                "Your CATA has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsCATARequest.GetApprover(pCataCode, pApproverType)) + ".<br><br>" +
                                "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to review the request</a><br><br>" +
                                "If you can't click on the above link,<br>" +
                                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                               "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetRequestor(pCataCode)), strSubject, strBody);

                        //send email to authorizer
                        strSubject = strStatus + " CATA";
                        strBody = "You " + strStatus.ToLower() + " a CATA.<br><br>" +
                                  "An email notification has been sent to " + clsEmployee.GetName(clsCATARequest.GetRequestor(pCataCode)) + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/CATA/CATADetails.aspx?catacode=" + pCataCode + "</i><br><br>" +
                                  "All the best,<br>Head Office Portal";
                        clsSpeedo.SendMail(clsUsers.GetEmail(clsCATARequest.GetAuthorize2(pCataCode)), strSubject, strBody);

                        if (pStatus == "1")
                        {
                            if (clsCATARequest.IsApprovedbyAuthorizeby1(pCataCode))
                            {
                                cmd.CommandText = "UPDATE Finance.CATARequest SET statcode=@statcode WHERE catacode=@CATACode";
                                cmd.Parameters.Add(new SqlParameter("@CATACode", pCataCode));
                                cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
                                intReturn = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                        }

                    }
                }
                //tran.Commit();
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("Warning<br>Message: " + ex.Message + "<br>Source: " + ex.TargetSite);
                //tran.Rollback();
                cn.Close();
            }
            finally
            {
                cn.Close();
            }
            return intReturn;
        }

        public static bool IsCanChangeRequestStatus(string pCATACode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT statcode FROM Finance.CATARequest WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["statcode"].ToString() == "2")
                    {
                        blnReturn = true;
                    }
                    else
                    {
                        blnReturn = false;
                    }
                }
                dr.Close();
            }
            return blnReturn;
        }

        public static string GetRequestor(string pCATACode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT createby FROM Finance.CATARequest WHERE catacode =@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["createby"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }

        public static string GetApprover(string pCATACode, string pApproverType)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                if (pApproverType == "endrsby1")
                {
                    cmd.CommandText = "SELECT endrsby1 FROM Finance.CATARequest WHERE catacode =@catacode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["endrsby1"].ToString();
                    }
                    cn.Close();
                }
                else if (pApproverType == "endrsby2")
                {
                    cmd.CommandText = "SELECT endrsby2 FROM Finance.CATARequest WHERE catacode =@catacode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["endrsby2"].ToString();
                    }
                    cn.Close();
                }
                else if (pApproverType == "autrzby1")
                {
                    cmd.CommandText = "SELECT autrzby1 FROM Finance.CATARequest WHERE catacode =@catacode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["autrzby1"].ToString();
                    }
                    cn.Close();
                }

                else if (pApproverType == "autrzby2")
                {
                    cmd.CommandText = "SELECT autrzby2 FROM Finance.CATARequest WHERE catacode =@catacode";
                    cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["autrzby2"].ToString();
                    }
                    cn.Close();
                }
            }
            return strReturn;
        }

        public static string GetAuthorize1(string pCATACode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT autrzby1 FROM Finance.CATARequest WHERE catacode =@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["autrzby1"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }

        public static string GetAuthorize2(string pCATACode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT autrzby2 FROM Finance.CATARequest WHERE catacode =@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    strReturn = dr["autrzby2"].ToString();
                }
                cn.Close();
            }
            return strReturn;
        }

        public static Boolean IsApprovedbyEndorser1(string pCATACode)
        {
            Boolean blnReturn = false;
            string strResult = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode FROM Finance.CATARequest WHERE catacode=@catacode AND endrstt1 <> '2'";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { blnReturn = true; }
            }
            return blnReturn;
        }

        public static Boolean IsApprovedbyEndorser2(string pCATACode)
        {
            Boolean blnReturn = false;
            string strResult = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode FROM Finance.CATARequest WHERE catacode=@catacode AND endrstt2 <> '2'";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { blnReturn = true; }
            }
            return blnReturn;
        }

        public static Boolean IsApprovedbyAuthorizeby1(string pCATACode)
        {
            Boolean blnReturn = false;
            string strResult = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode FROM Finance.CATARequest WHERE catacode=@catacode AND authstt1 <> '2'";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { blnReturn = true; }
            }
            return blnReturn;
        }

        public static Boolean IsApprovedbyAuthorizeby2(string pCATACode)
        {
            Boolean blnReturn = false;
            string strResult = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT catacode FROM Finance.CATARequest WHERE catacode=@catacode AND authstt2 <> '2'";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCATACode));
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { blnReturn = true; }
            }
            return blnReturn;
        }

        public static bool AuthenticateAccess(string pUsername, string pCataCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cn.Open();

                cmd.CommandText = "SELECT catacode FROM Finance.CATARequest WHERE catacode=@catacode AND createby=@user";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                cmd.Parameters.Add(new SqlParameter("@user", pUsername));
                SqlDataReader dr = cmd.ExecuteReader();
                blnReturn = dr.Read();

                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT catacode FROM Finance.CATAApproval WHERE catacode=@catacode AND username=@user";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                cmd.Parameters.Add(new SqlParameter("@user", pUsername));


                dr.Close();
            }
            return blnReturn;
        }

        public static int GetTotalRecords()
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(catacode) FROM Finance.CATARequest";
                cn.Open();
                try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
                catch { }
            }
            return intReturn;
        }

        public static int Cancel(string pCataNumber)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.CATARequest SET statcode=@statcode WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@statcode", "3"));
                cmd.Parameters.Add(new SqlParameter("@catacode", pCataNumber));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int Disapprove(string pCataNumber)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.CATARequest SET statcode=@statcode WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@statcode", "4"));
                cmd.Parameters.Add(new SqlParameter("@catacode", pCataNumber));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }

        public static int Approve(string pCataNumber)
        {
            int intReturn = 0;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "UPDATE Finance.CATARequest SET statcode=@statcode WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@statcode", "2"));
                cmd.Parameters.Add(new SqlParameter("@catacode", pCataNumber));
                cn.Open();
                intReturn = cmd.ExecuteNonQuery();
                return intReturn;
            }
        }


        public static bool IsForApproval(string pCataCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT statcode FROM Finance.CATARequest  WHERE catacode=@catacode";
                cmd.Parameters.Add(new SqlParameter("@catacode", pCataCode));
                cn.Open();
                string strStatCode = cmd.ExecuteScalar().ToString();
                if (strStatCode == "1")
                {
                    blnReturn = true;
                }
                return blnReturn;
            }
        }

    }
}