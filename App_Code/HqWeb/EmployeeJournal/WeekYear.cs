using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for WeekYear
/// </summary>
public class WeekYear:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    private int _intWeekCode;
    private string _strWeekName;
    private int _intWeekNumber;
    private int _intFiscalYearCode;
    private DateTime _dteDateStart;
    private DateTime _dteDateTo;
    private string _strIsEnabled;
    private string _strCreatedBy;
    private DateTime _dteCreatedOn;
    private string _strModifiedBy;
    private DateTime _dteModifiedOn;

    public int WeekCode { get { return _intWeekCode; } set { _intWeekCode = value; } }
    public string WeekName { get { return _strWeekName; } set { _strWeekName = value; } }
    public int WeekNumber { get { return _intWeekNumber; } set { _intWeekNumber = value; } }
    public int FiscalYearCode { get { return _intFiscalYearCode; } set { _intFiscalYearCode = value; } }
    public DateTime DateStart { get { return _dteDateStart; } set { _dteDateStart = value; } }
    public DateTime DateTo { get { return _dteDateTo; } set { _dteDateTo = value; } }
    public string IsEnabled { get { return _strIsEnabled; } set { _strIsEnabled = value; } }
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
            cmd.CommandText = "INSERT INTO portal.WeekYear (WeekName, WeekNumber,FiscalYearCode,DateStart,DateEnd, IsEnabled, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn) VALUES (@WeekName, @WeekNumber,@FiscalYearCode,@DateStart,@DateEnd, @IsEnabled, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn)";
            cmd.Parameters.Add(new SqlParameter("@WeekName", _strWeekName));
            cmd.Parameters.Add(new SqlParameter("@WeekNumber", _intWeekNumber));
            cmd.Parameters.Add(new SqlParameter("@FiscalYearCode", _intFiscalYearCode));
            cmd.Parameters.Add(new SqlParameter("@DateStart", _dteDateStart));
            cmd.Parameters.Add(new SqlParameter("@DateEnd", _dteDateTo));
            cmd.Parameters.Add(new SqlParameter("@IsEnabled", "1"));
            cmd.Parameters.Add(new SqlParameter("@CreatedBy", _strCreatedBy));
            cmd.Parameters.Add(new SqlParameter("@CreatedOn", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@ModifiedBy", _strModifiedBy));
            cmd.Parameters.Add(new SqlParameter("@ModifiedOn", DateTime.Now));
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



    public static DataTable GetDSLFiscalYearsALL()
    {
        DataTable tblReturn = new DataTable();
        DataTable tblTemporary = new DataTable();

        tblReturn.Columns.Add("pValue");
        tblReturn.Columns.Add("pText");

        DataRow drNew = tblReturn.NewRow();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT FiscalYearCode AS pValue, FiscalYearName AS pText FROM Portal.FiscalYear WHERE IsEnabled='1' ORDER BY IsActive Desc, FiscalYearName ASC";
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

    public static DataTable GetDSG(string pFiscalYearCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Portal.FiscalYear WHERE FiscalYearCode=@FiscalYearCode ORDER BY FiscalYearName ASC, IsActive DESC";
                cmd.Parameters.Add(new SqlParameter("@FiscalYearCode", pFiscalYearCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSGCart(int WeekCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT WeekCode,WeekName,DateStart,DateEnd FROM Portal.WeekYear WHERE FiscalYearCode=@FiscalYearCode AND IsEnabled='1' ORDER BY WeekNumber ASC, WeekName ASC";
                cmd.Parameters.Add(new SqlParameter("@FiscalYearCode", WeekCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static string GetActiveWeekCode()
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1(WeekCode) FROM Portal.WeekYear WHERE  '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  BETWEEN DateStart AND DateEND ORDER by WeekNumber DESC";
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }


    public static string GetWeekName(int pWeekCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT WeekName FROM Portal.WeekYear WHERE WeekCode=@WeekCode";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }

    public static DateTime GetDateStart(int pWeekCode)
    {
        DateTime dteReturn = DateTime.Now;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT DateStart FROM Portal.WeekYear WHERE WeekCode=@WeekCode";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
            cn.Open();
            dteReturn = Convert.ToDateTime(cmd.ExecuteScalar());
        }
        return dteReturn;
    }

    public static DateTime GetDateDue(int pWeekCode)
    {
        DateTime dteReturn = DateTime.Now;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT DateDue FROM Portal.WeekYear WHERE WeekCode=@WeekCode";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
            cn.Open();
            dteReturn = Convert.ToDateTime(cmd.ExecuteScalar());
        }
        return dteReturn;
    }

    public static DateTime GetDateEnd(int pWeekCode)
    {
        DateTime dteReturn = DateTime.Now;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT DateEnd FROM Portal.WeekYear WHERE WeekCode=@WeekCode";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
            cn.Open();
            dteReturn = Convert.ToDateTime(cmd.ExecuteScalar());
        }
        return dteReturn;
    }
}