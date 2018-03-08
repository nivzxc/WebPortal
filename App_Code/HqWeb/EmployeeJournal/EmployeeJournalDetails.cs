using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for EmployeeJournalDetails
/// </summary>
public class EmployeeJournalDetails:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    private int _intEmployeeJournalDetailsCode;
    private int _intEmployeeJournalCode;
    private string _strContents;
    private int _intItemNumber;
    private DateTime _dteJournalDate;
    private DateTime _dteCreatedOn;
    private string _strEndorsedBy;
    private DateTime _dteEndorsedOn;
    private string _strEndoredRemarks;
    private string _strApprovedBy;
    private DateTime _dteApprovedOn;
    private string _strApprovedRemarks;
    private string _strJournalStatus;
    private string _strIsEnabled;


    public int EmployeeJournalDetailsCode { get { return _intEmployeeJournalDetailsCode; } set { _intEmployeeJournalDetailsCode = value; } }
    public int EmployeeJournalCode { get { return _intEmployeeJournalCode; } set { _intEmployeeJournalCode = value; } }
    public string Contents { get { return _strContents; } set { _strContents = value; } }
    public int ItemNumber { get { return _intItemNumber; } set { _intItemNumber = value; } }
    public DateTime JournalDate { get { return _dteJournalDate; } set { _dteJournalDate = value; } }
    public DateTime CreatedOn { get { return _dteCreatedOn; } set { _dteCreatedOn = value; } }
    public string EndorsedBy { get { return _strEndorsedBy; } set { _strEndorsedBy = value; } }
    public DateTime EndorsedOn { get { return _dteEndorsedOn; } set { _dteEndorsedOn = value; } }
    public string EndoredRemarks { get { return _strEndoredRemarks; } set { _strEndoredRemarks = value; } }
    public string ApprovedBy { get { return _strApprovedBy; } set { _strApprovedBy = value; } }
    public DateTime ApprovedOn { get { return _dteApprovedOn; } set { _dteApprovedOn = value; } }
    public string ApprovedRemarks { get { return _strApprovedRemarks; } set { _strApprovedRemarks = value; } }
    public string JournalStatus { get { return _strJournalStatus; } set { _strJournalStatus = value; } }
    public string IsEnabled { get { return _strIsEnabled; } set { _strIsEnabled = value; } }

	public EmployeeJournalDetails()
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
            cmd.CommandText = "INSERT INTO portal.EmployeeJournalDetails (JournalCode, Contents, ItemNumber, JournalDate, CreatedDate, EndorsedBy, EndorsedDate, EndorsedRemarks, ApprovedBy, Approveddate, ApprovedRemarks, JournalStatus, IsEnabled) VALUES (@JournalCode, @Contents, @ItemNumber, @JournalDate, @CreatedDate, @EndorsedBy, @EndorsedDate, @EndorsedRemarks, @ApprovedBy, @Approveddate, @ApprovedRemarks, @JournalStatus, @IsEnabled)";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", _intEmployeeJournalCode));
            cmd.Parameters.Add(new SqlParameter("@Contents  ", _strContents));
            cmd.Parameters.Add(new SqlParameter("@ItemNumber", _intItemNumber));
            cmd.Parameters.Add(new SqlParameter("@JournalDate", _dteJournalDate));
            cmd.Parameters.Add(new SqlParameter("@CreatedDate", _dteCreatedOn));
            cmd.Parameters.Add(new SqlParameter("@EndorsedBy", _strEndorsedBy));
            cmd.Parameters.Add(new SqlParameter("@EndorsedDate", _dteEndorsedOn));
            cmd.Parameters.Add(new SqlParameter("@EndorsedRemarks", _strEndoredRemarks));
            cmd.Parameters.Add(new SqlParameter("@ApprovedBy", _strApprovedBy));
            cmd.Parameters.Add(new SqlParameter("@Approveddate", _dteApprovedOn));
            cmd.Parameters.Add(new SqlParameter("@ApprovedRemarks", _strApprovedRemarks));
            cmd.Parameters.Add(new SqlParameter("@JournalStatus", _strJournalStatus));
            cmd.Parameters.Add(new SqlParameter("@IsEnabled", _strIsEnabled));
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

    public int UpdateEnabled()
    {
        int intReturn = 0;

        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "UPDATE Portal.EmployeeJournalDetails SET IsEnabled=@IsEnabled WHERE JournalDCode=@JournalDCode";
            cmd.Parameters.Add(new SqlParameter("@JournalDCode", _intEmployeeJournalDetailsCode));
            cmd.Parameters.Add(new SqlParameter("@IsEnabled", _strIsEnabled));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();
        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }

    public int UpdateContent()
    {
        int intReturn = 0;

        SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
        cn.Open();
        SqlTransaction tran = cn.BeginTransaction();
        SqlCommand cmd = cn.CreateCommand();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "UPDATE Portal.EmployeeJournalDetails SET Contents=@Contents WHERE JournalDCode=@JournalDCode";
            cmd.Parameters.Add(new SqlParameter("@JournalDCode", _intEmployeeJournalDetailsCode));
            cmd.Parameters.Add(new SqlParameter("@Contents", _strContents));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();
        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }

    public static int GetTotalRecords(int pJournalCode)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(JournalDCode) FROM Portal.EmployeeJournalDetails WHERE JournalCode=@JournalCode";
            cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
            cn.Open();
            try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
            catch { }
        }
        return intReturn;
    }

    public static DataTable GetDSGCart(int pJournalCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT JournalDCode, JournalCode, ItemNumber, Contents FROM Portal.EmployeeJournalDetails WHERE JournalCode=@JournalCode AND IsEnabled='1' ORDER BY ItemNumber ASC";
                cmd.Parameters.Add(new SqlParameter("@JournalCode", pJournalCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }
}