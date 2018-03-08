using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HRMS;

/// <summary>
/// Summary description for EmployeeJournal
/// </summary>
public class EmployeeJournal : IDisposable
{
    public EmployeeJournal()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum EJSMailType
    {
        FiledAcknowledgementRequestor,
        FiledNotificationApprover,
        ApprovedAcknowledgementApprover,
        ApprovedNotificationRequestor,
        DisapprovedAcknowledgementApprover,
        DisapprovedNotificationRequestor
    }

    public void Dispose() { GC.SuppressFinalize(this); }

    private int _intEmployeeJournalCode;
    private int _intWeekCode;
    private string _strWeekYear;
    private string _strContents;
    private string _strJournalStatus;
    private string _strLockStatus;
    private string _StrIsEnabled;
    private string _strCreatedBy;
    private DateTime _dteCreatedOn;
    private string _strModifiedBy;
    private DateTime _dteModifiedOn;

    public int EmployeeJournalCode { get { return _intEmployeeJournalCode; } set { _intEmployeeJournalCode = value; } }
    public int WeekCode { get { return _intWeekCode; } set { _intWeekCode = value; } }
    public string WeekYear { get { return _strWeekYear; } set { _strWeekYear = value; } }
    public string Contents { get { return _strContents; } set { _strContents = value; } }
    public string JournalStatus { get { return _strJournalStatus; } set { _strJournalStatus = value; } }
    public string LockStatus { get { return _strLockStatus; } set { _strLockStatus = value; } }
    public string Enabled { get { return _StrIsEnabled; } set { _StrIsEnabled = value; } }
    public string CreatedBy { get { return _strCreatedBy; } set { _strCreatedBy = value; } }
    public DateTime CreatedOn { get { return _dteCreatedOn; } set { _dteCreatedOn = value; } }
    public string ModifiedBy { get { return _strModifiedBy; } set { _strModifiedBy = value; } }
    public DateTime ModifiedOn { get { return _dteModifiedOn; } set { _dteModifiedOn = value; } }

    public int Insert()
    {
        int intReturn = 0;
        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;
        try
        {
            cmd.CommandText = "INSERT INTO portal.EmployeeJournal (WeekCode, JournalStatus, LockStatus, IsEnabled, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn) VALUES (@WeekCode, 'S', '0', @IsEnabled, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn)";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", _intWeekCode));
            cmd.Parameters.Add(new SqlParameter("@IsEnabled", _StrIsEnabled));
            cmd.Parameters.Add(new SqlParameter("@CreatedBy", _strCreatedBy));
            cmd.Parameters.Add(new SqlParameter("@CreatedOn", _dteCreatedOn));
            cmd.Parameters.Add(new SqlParameter("@ModifiedBy", _strModifiedBy));
            cmd.Parameters.Add(new SqlParameter("@ModifiedOn", _dteModifiedOn));
            intReturn = cmd.ExecuteNonQuery();
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            intReturn = 0;
        }
        finally
        {
            cn.Close();
            cmd.Dispose();
        }
        return intReturn;
    }

    public int Update()
    {
        int intReturn = 0;

        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "UPDATE Portal.EmployeeJournal SET Contents=@Contents, ModifiedBy=@ModifiedBy,ModifiedOn=GETDATE() WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", _intEmployeeJournalCode));
            cmd.Parameters.Add(new SqlParameter("@Contents", _strContents));
            cmd.Parameters.Add(new SqlParameter("@ModifiedBy", _strModifiedBy));
            cmd.Parameters.Add(new SqlParameter("@ModifiedOn", _dteModifiedOn));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();
        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }

    public int UpdateStatus()
    {
        int intReturn = 0;

        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "UPDATE Portal.EmployeeJournal SET JournalStatus=@JournalStatus, LockStatus=@LockStatus, Contents=@Contents, IsEnabled=@IsEnabled, ModifiedBy=@ModifiedBy,ModifiedOn=GETDATE() WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", _intEmployeeJournalCode));
            cmd.Parameters.Add(new SqlParameter("@JournalStatus", _strJournalStatus));
            cmd.Parameters.Add(new SqlParameter("@LockStatus", _strLockStatus));
            cmd.Parameters.Add(new SqlParameter("@Contents", _strContents));
            cmd.Parameters.Add(new SqlParameter("@IsEnabled", _StrIsEnabled));
            cmd.Parameters.Add(new SqlParameter("@ModifiedBy", _strModifiedBy));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();
        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }

    public int UpdateStatus2()
    {
        int intReturn = 0;

        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "UPDATE Portal.EmployeeJournal SET JournalStatus=@JournalStatus, LockStatus=@LockStatus, IsEnabled=@IsEnabled WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", _intEmployeeJournalCode));
            cmd.Parameters.Add(new SqlParameter("@JournalStatus", _strJournalStatus));
            cmd.Parameters.Add(new SqlParameter("@LockStatus", _strLockStatus));
            cmd.Parameters.Add(new SqlParameter("@IsEnabled", _StrIsEnabled));
            //cmd.Parameters.Add(new SqlParameter("@ModifiedBy", _strModifiedBy));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();
        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }

    public void Fill()
    {
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Portal.EmployeeJournal WHERE WeekNumber=@WeekNumber AND WeekYear=@WeekYear AND CreatedBy=@CreatedBy";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", _intWeekCode));
            cmd.Parameters.Add(new SqlParameter("@WeekYear", _strWeekYear));
            cmd.Parameters.Add(new SqlParameter("@CreatedBy", _strCreatedBy));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                _intEmployeeJournalCode = Convert.ToInt16(dr["JournalCode"]);
                _intWeekCode = Convert.ToInt16(dr["WeekCode"]);
                _StrIsEnabled = dr["IsEnabled"].ToString();
                _strCreatedBy = dr["CreatedBy"].ToString();
                _dteCreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString());
                _strModifiedBy = dr["ModifiedBy"].ToString();
                _dteModifiedOn = Convert.ToDateTime(dr["ModifiedOn"].ToString());
            }
            dr.Close();
        }
    }

    public void Fill2()
    {
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Portal.EmployeeJournal WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", _intEmployeeJournalCode));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                _intEmployeeJournalCode = Convert.ToInt16(dr["JournalCode"]);
                _intWeekCode = Convert.ToInt16(dr["WeekCode"]);
                _strContents = dr["Contents"].ToString();
                _StrIsEnabled = dr["IsEnabled"].ToString();
                _strCreatedBy = dr["CreatedBy"].ToString();
                _dteCreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString());
                _strModifiedBy = dr["ModifiedBy"].ToString();
                _dteModifiedOn = Convert.ToDateTime(dr["ModifiedOn"].ToString());
            }
            dr.Close();
        }
    }

    public static bool HasExistingJournal(string pUsername, int pWeekCode)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Portal.EmployeeJournal WHERE createdby=@createdby AND WeekCode=@WeekCode";
            cmd.Parameters.Add(new SqlParameter("@createdby", pUsername));
            cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
        }
        return blnReturn;
    }

    public static DataTable GetDSGForApproval(string pUsername)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT *,(SELECT WeekName FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS WeekName, (SELECT DateStart FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateStart, (SELECT DateEnd FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateEnd FROM Portal.EmployeeJournal WHERE JournalStatus='F' AND LockStatus='1' AND (SELECT TOP 1 JournalApprover FROM Portal.EmployeeJournalApproval WHERE JournalCode=Portal.EmployeeJournal.JournalCode AND JournalAStatus='F' ORDER BY JournalAOrder ASC)=@username ORDER BY WeekCode DESC";
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSG(string pUsername)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT *,(SELECT WeekName FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS WeekName, (SELECT DateStart FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateStart, (SELECT DateEnd FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateEnd FROM Portal.EmployeeJournal WHERE CreatedBy=@CreatedBy ORDER BY WeekCode DESC";
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", pUsername));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSG(string pUsername, int pFiscalYearCode, string pWeekCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            if (pWeekCode == "ALL")
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT *,(SELECT WeekName FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS WeekName, (SELECT DateStart FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateStart, (SELECT DateEnd FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateEnd FROM Portal.EmployeeJournal WHERE CreatedBy=@CreatedBy AND WeekCode IN (SELECT WeekCode FROM Portal.WeekYear WHERE FiscalYearCode=@FiscalYearCode) ORDER BY WeekCode DESC";
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@FiscalYearCode", pFiscalYearCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            else
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT *,(SELECT WeekName FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS WeekName, (SELECT DateStart FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateStart, (SELECT DateEnd FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateEnd FROM Portal.EmployeeJournal WHERE CreatedBy=@CreatedBy AND WeekCode=@WeekCode ORDER BY WeekCode DESC";
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSG(int pFiscalYearCode, string pWeekCode, string pDeptCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            if (pWeekCode == "ALL")
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT *,(SELECT WeekName FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS WeekName, (SELECT DateStart FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateStart, (SELECT DateEnd FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateEnd FROM Portal.EmployeeJournal WHERE WeekCode IN (SELECT WeekCode FROM Portal.WeekYear WHERE FiscalYearCode=@FiscalYearCode) ORDER BY WeekCode DESC";
                    cmd.Parameters.Add(new SqlParameter("@FiscalYearCode", pFiscalYearCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            else
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT *,(SELECT WeekName FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS WeekName, (SELECT DateStart FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateStart, (SELECT DateEnd FROM Portal.WeekYear WHERE WeekCode=Portal.EmployeeJournal.WeekCode) AS DateEnd FROM Portal.EmployeeJournal WHERE WeekCode=@WeekCode AND createdby IN (SELECT username FROM HR.Employees WHERE deptcode=@deptcode) ORDER BY WeekCode DESC";
                    cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
                    cmd.Parameters.Add(new SqlParameter("@deptcode", pDeptCode));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSGs(string pUsername, string pWeekNumber, string pWeekYear)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                if (pWeekNumber == "ALL")
                {
                    cmd.CommandText = "SELECT * FROM Portal.EmployeeJournal WHERE CreatedBy=@CreatedBy AND WeekYear=@WeekYear ORDER BY WeekYear DESC, WeekNumber DESC";
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@WeekYear", pWeekYear));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM Portal.EmployeeJournal WHERE CreatedBy=@CreatedBy AND WeekNumber=@WeekNumber AND WeekYear=@WeekYear ORDER BY WeekYear DESC, WeekNumber DESC";
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", pUsername));
                    cmd.Parameters.Add(new SqlParameter("@WeekNumber", pWeekNumber));
                    cmd.Parameters.Add(new SqlParameter("@WeekYear", pWeekYear));
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
        }
        return tblReturn;
    }
    public static DataTable GetDSLJournalDatesALL(string pFiscalYearCode)
    {
        DataTable tblReturn = new DataTable();
        DataTable tblTemporary = new DataTable();

        tblReturn.Columns.Add("pValue");
        tblReturn.Columns.Add("pText");

        DataRow drNew = tblReturn.NewRow();
        drNew["pValue"] = "ALL";
        drNew["pText"] = "ALL";
        tblReturn.Rows.Add(drNew);

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                //cmd.CommandText = "SELECT DISTINCT(WeekNumber) AS pValue,((CONVERT(VARCHAR(10),(SELECT DATEADD(wk, DATEDIFF(wk, 6, '1/1/' + Portal.EmployeeJournal.WeekYear) + (Portal.EmployeeJournal.WeekNumber-1), 6)),110)) + ' - ' + (CONVERT(VARCHAR(10),(SELECT DATEADD(wk, DATEDIFF(wk, 6, '1/1/' + Portal.EmployeeJournal.WeekYear) + (Portal.EmployeeJournal.WeekNumber-1), 6)),110))) AS pText FROM Portal.EmployeeJournal WHERE WeekYear=@WeekYear ORDER BY WeekNumber DESC";
                cmd.CommandText = "SELECT WeekCode AS pvalue, (WeekName + ' (' + CONVERT(VARCHAR(11),DateStart,6) + ' - ' + CONVERT(VARCHAR(11),DateEnd,6) + ')') AS ptext FROM Portal.WeekYear WHERE FiscalYearCode=@FiscalYearCode ORDER BY WeekNumber DESC";
                cmd.Parameters.Add(new SqlParameter("@FiscalYearCode", pFiscalYearCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblTemporary);

                foreach (DataRow dr in tblTemporary.Rows)
                {
                    drNew = tblReturn.NewRow();
                    drNew["pValue"] = dr["pValue"].ToString();
                    drNew["pText"] = dr["pText"].ToString();
                    tblReturn.Rows.Add(drNew);
                }
            }
        }

        return tblReturn;
    }


    public static DataTable GetDSLJournalDates(string pFiscalYearCode)
    {
        DataTable tblReturn = new DataTable();
        DataTable tblTemporary = new DataTable();

        tblReturn.Columns.Add("pValue");
        tblReturn.Columns.Add("pText");

        DataRow drNew = tblReturn.NewRow();
        //drNew["pValue"] = "ALL";
        //drNew["pText"] = "ALL";
        //tblReturn.Rows.Add(drNew);

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                //cmd.CommandText = "SELECT DISTINCT(WeekNumber) AS pValue,((CONVERT(VARCHAR(10),(SELECT DATEADD(wk, DATEDIFF(wk, 6, '1/1/' + Portal.EmployeeJournal.WeekYear) + (Portal.EmployeeJournal.WeekNumber-1), 6)),110)) + ' - ' + (CONVERT(VARCHAR(10),(SELECT DATEADD(wk, DATEDIFF(wk, 6, '1/1/' + Portal.EmployeeJournal.WeekYear) + (Portal.EmployeeJournal.WeekNumber-1), 6)),110))) AS pText FROM Portal.EmployeeJournal WHERE WeekYear=@WeekYear ORDER BY WeekNumber DESC";
                cmd.CommandText = "SELECT WeekCode AS pvalue, (WeekName + ' (' + CONVERT(VARCHAR(11),DateStart,6) + ' - ' + CONVERT(VARCHAR(11),DateEnd,6) + ')') AS ptext FROM Portal.WeekYear WHERE FiscalYearCode=@FiscalYearCode";
                cmd.Parameters.Add(new SqlParameter("@FiscalYearCode", pFiscalYearCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblTemporary);

                foreach (DataRow dr in tblTemporary.Rows)
                {
                    drNew = tblReturn.NewRow();
                    drNew["pValue"] = dr["pValue"].ToString();
                    drNew["pText"] = dr["pText"].ToString();
                    tblReturn.Rows.Add(drNew);
                }
            }
        }

        return tblReturn;
    }

    public static DataTable GetDSLJournalYearsALL()
    {
        DataTable tblReturn = new DataTable();
        DataTable tblTemporary = new DataTable();

        tblReturn.Columns.Add("pValue");
        tblReturn.Columns.Add("pText");

        DataRow drNew = tblReturn.NewRow();
        //drNew["pValue"] = "ALL";
        //drNew["pText"] = "ALL";
        //tblReturn.Rows.Add(drNew);

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                //cmd.CommandText = "SELECT DISTINCT WeekYear AS pValue, WeekYear AS pText FROM Portal.EmployeeJournal ORDER BY WeekYear DESC";
                cmd.CommandText = "SELECT FiscalYearCode AS pvalue, FiscalYearName AS ptext FROM Portal.FiscalYear WHERE IsEnabled='1' ORDER BY IsActive DESC, FiscalYearName ASC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblTemporary);

                foreach (DataRow dr in tblTemporary.Rows)
                {
                    drNew = tblReturn.NewRow();
                    drNew["pValue"] = dr["pValue"].ToString();
                    drNew["pText"] = dr["pText"].ToString();
                    tblReturn.Rows.Add(drNew);
                }
            }
        }

        return tblReturn;
    }

    public static int GetLastJournalCode(int pWeekCode, string pCreatedBy)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 JournalCode FROM Portal.EmployeeJournal WHERE WeekCode=@WeekCode AND CreatedBy=@CreatedBy ORDER BY JournalCode DESC";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
            cmd.Parameters.Add(new SqlParameter("@CreatedBy", pCreatedBy));
            cn.Open();
            try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
            catch { }
        }
        return intReturn;
    }

    public static int GetWeekCode(int pJournalCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT WeekCode FROM Portal.EmployeeJournal WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cn.Open();
            try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
            catch { }
        }
        return intReturn;
    }

    public static string GetJournalStatus(int pJournalCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT JournalStatus FROM Portal.EmployeeJournal WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }
    //SELECT Distinct WeekCode FROM Portal.EmployeeJournal WHERE WeekCode NOT IN (SELECT WeekCode FROM Portal.EmployeeJournal WHERE CreatedBy='fatima.pagkalinawan') ORDER BY WeekCode DESC 
    public static string GetRequestor(int pJournalCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT CreatedBy FROM Portal.EmployeeJournal WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }

    public static string GetLockStatus(int pJournalCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT LockStatus FROM Portal.EmployeeJournal WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }

    public static DataTable GetDSGUncreatedJournal(string pUsername)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT Distinct WeekCode FROM Portal.EmployeeJournal WHERE WeekCode NOT IN (SELECT WeekCode FROM Portal.EmployeeJournal WHERE CreatedBy=@Username) ORDER BY WeekCode DESC ";
                cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public void SendNotification(EJSMailType pMailType, string pUsername, string pApproverUsername)
    {
        string strSpeedoUrl = ConfigurationManager.AppSettings["SpeedoURL"].ToString();
        string strSubject = "";
        string strBody = "";
        string strRequestorName = clsEmployee.GetName(pUsername);
        string strRequestorEmail = clsUsers.GetEmail(pUsername);
        string strApproverName = clsEmployee.GetName(pApproverUsername);
        string strApproverEmail = clsUsers.GetEmail(pApproverUsername);
        string strURLJournalDetails = strSpeedoUrl + "/EmployeeJournal/JournalViewer.aspx?JournalCode=" + _intEmployeeJournalCode.ToString();
        string strURLJournalDetailsA = strSpeedoUrl + "/EmployeeJournal/JournalViewerA.aspx?JournalCode=" + _intEmployeeJournalCode.ToString();

        switch (pMailType)
        {
            case EJSMailType.FiledAcknowledgementRequestor:
                strSubject = "Delivered: Employee Journal";
                strBody = "Hi " + strRequestorName + ",<br><br>" +
                          "Your journal has been successfully sent to " + strApproverName + ".<br>" +
                          "<a href='" + strURLJournalDetails + "'>Click here to view your online Journal</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br>" +
                          "<i>" + strURLJournalDetails + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
                break;

            case EJSMailType.FiledNotificationApprover:
                strSubject = "For Your Review: Employee Journal - " + strRequestorName;
                strBody = "Hi " + strApproverName + ",<br><br>" +
                          strRequestorName + " has just sent a journal.<br>" +
                          "<a href='" + strURLJournalDetailsA + "'>Click here to view the online Journal</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br>" +
                          "<i>" + strURLJournalDetailsA + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
                break;

            case EJSMailType.ApprovedAcknowledgementApprover:
                strSubject = "Delivered: Reviewed Employee Journal - " + strRequestorName;
                strBody = "Hi " + strApproverName + ",<br><br>" +
                          "You reviewed a journal.<br><br>" +
                          "An email notification has been sent to " + strRequestorName + " to inform him/her regarding this action.<br><br>" +
                          "<a href='" + strURLJournalDetails + "'>Click here to view the online Journal</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br>" +
                          "<i>" + strURLJournalDetails + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(strApproverEmail, strSubject, strBody);
                break;

            case EJSMailType.ApprovedNotificationRequestor:
                strSubject = "Reviewed: Employee Journal";
                strBody = "Hi " + strRequestorName + ",<br><br>" +
                          strApproverName + " has reviewed your journal.<br><br>" +
                          "<a href='" + strURLJournalDetails + "'>Click here to view the online Journal</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br>" +
                          "<i>" + strURLJournalDetails + "</i><br><br>" +
                          "All the best,<br>Head Office Portal";
                clsSpeedo.SendMail(strRequestorEmail, strSubject, strBody);
                break;
        }
    }
}