using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for EmployeeJournalApproval
/// </summary>
public class EmployeeJournalApproval:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    private int _intEmployeeJournalACode;
    private int _intEmployeeJournalCode;
    private string _strJournalApprover;
    private string _strJournalAStatus;
    private int _intJournalAOrder;
    private DateTime _dteJournalADate;
    private string _strRemarks;


    public int EmployeeJournalACode { get { return _intEmployeeJournalACode; } set { _intEmployeeJournalACode = value; } }
    public int EmployeeJournalCode { get { return _intEmployeeJournalCode; } set { _intEmployeeJournalCode = value; } }
    public string JournalApprover { get { return _strJournalApprover; } set { _strJournalApprover = value; } }
    public string JournalAStatus { get { return _strJournalAStatus; } set { _strJournalAStatus = value; } }
    public int JournalAOrder { get { return _intJournalAOrder; } set { _intJournalAOrder = value; } }
    public DateTime JournalADate { get { return _dteJournalADate; } set { _dteJournalADate = value; } }
    public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }


	public EmployeeJournalApproval()
	{
		//
		// TODO: Add constructor logic here
		//
	}

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
            cmd.CommandText = "INSERT INTO portal.EmployeeJournalApproval (JournalCode, JournalApprover, JournalAStatus, JournalAOrder, JournalADate, Remarks) VALUES (@JournalCode, @JournalApprover, @JournalAStatus, @JournalAOrder, @JournalADate, @Remarks)";
            cmd.Parameters.Add(new SqlParameter("@JournalACode", _intEmployeeJournalACode));
            cmd.Parameters.Add(new SqlParameter("@JournalCode  ", _intEmployeeJournalCode));
            cmd.Parameters.Add(new SqlParameter("@JournalApprover", _strJournalApprover));
            cmd.Parameters.Add(new SqlParameter("@JournalAStatus", _strJournalAStatus));
            cmd.Parameters.Add(new SqlParameter("@JournalAOrder", _intJournalAOrder));
            cmd.Parameters.Add(new SqlParameter("@JournalADate", _dteJournalADate));
            cmd.Parameters.Add(new SqlParameter("@Remarks", ""));
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
            cmd.CommandText = "UPDATE Portal.EmployeeJournalApproval SET JournalAStatus=@JournalAStatus,Remarks=@Remarks,JournalADate=GETDATE() WHERE JournalACode=@JournalACode";
            cmd.Parameters.Add(new SqlParameter("@JournalACode", _intEmployeeJournalACode));
            cmd.Parameters.Add(new SqlParameter("@JournalAStatus", _strJournalAStatus));
            cmd.Parameters.Add(new SqlParameter("@Remarks", _strRemarks));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();
        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }

    public int ResetStatus()
    {
        int intReturn = 0;

        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "UPDATE Portal.EmployeeJournalApproval SET JournalAStatus='F' WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", _intEmployeeJournalCode));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();

        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }
    public static string GetRemarks(int pJournalCode, string pAppLevel)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Remarks FROM Portal.EmployeeJournalApproval WHERE JournalCode=@JournalCode AND JournalAOrder=@JournalAOrder";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cmd.Parameters.Add(new SqlParameter("@JournalAOrder", pAppLevel));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }

    public static string GetApprover(int pJournalCode, string pAppLevel)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {   
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT JournalApprover FROM Portal.EmployeeJournalApproval WHERE JournalCode=@JournalCode AND JournalAOrder=@JournalAOrder";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cmd.Parameters.Add(new SqlParameter("@JournalAOrder", pAppLevel));
            cn.Open();
            try
            {
                strReturn = cmd.ExecuteScalar().ToString();
            }
            catch
            {
                strReturn = "";
            }
        }
        return strReturn;
    }

    public static string GetApproverAStatus(int pJournalCode, string pAppLevel)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT JournalAStatus FROM Portal.EmployeeJournalApproval WHERE JournalCode=@JournalCode AND JournalAOrder=@JournalAOrder";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cmd.Parameters.Add(new SqlParameter("@JournalAOrder", pAppLevel));
            cn.Open();
            try
            {
                strReturn = cmd.ExecuteScalar().ToString();
            }
            catch
            {
                strReturn = "";
            }
        }
        return strReturn;
    }

    public static string GetApproverADate(int pJournalCode, string pAppLevel)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT JournalADate FROM Portal.EmployeeJournalApproval WHERE JournalCode=@JournalCode AND JournalAOrder=@JournalAOrder";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cmd.Parameters.Add(new SqlParameter("@JournalAOrder", pAppLevel));
            cn.Open();
            try
            {
                strReturn = cmd.ExecuteScalar().ToString();
            }
            catch
            {
                strReturn = "";
            }
        }
        return strReturn;
    }

    public static int GetForApprovalJournalACode(int pJournalCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 JournalACode FROM Portal.EmployeeJournalApproval WHERE JournalCode=@JournalCode AND JournalAStatus='F' ORDER BY JournalAOrder ASC";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode)); ;
            cn.Open();
            intReturn = Convert.ToInt16(cmd.ExecuteScalar());
        }
        return intReturn;
    }

    public static int GetForApprovalJournalAOrder(int pJournalCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 JournalAOrder FROM Portal.EmployeeJournalApproval WHERE JournalCode=@JournalCode AND JournalAStatus='F' ORDER BY JournalAOrder ASC";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cn.Open();
            intReturn = Convert.ToInt16(cmd.ExecuteScalar());
        }
        return intReturn;
    }


    public static int GetApprovalCount(string pUsername)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Portal.EmployeeJournal WHERE JournalStatus='F' AND LockStatus='1' AND (SELECT TOP 1 JournalApprover FROM Portal.EmployeeJournalApproval WHERE JournalCode=Portal.EmployeeJournal.JournalCode AND JournalAStatus='F' ORDER BY JournalAOrder ASC)=@username";
            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
            cn.Open();
            try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
            catch { }
        }
        return intReturn;
    }
}