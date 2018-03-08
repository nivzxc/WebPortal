using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AccountActual
/// </summary>
public class AccountActual:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    private int _intAccountAvailedCode;
    private int _intFiscalYearCode;
    private string _strResponsibilityCode;
    private string _strChargeTypeCode;
    private int _intAccountItemCode;
    private double _dblAvailedValue;
    private string _strRemarks;
    private string _strCreatedBy;
    private DateTime _dteCreatedOn;
    private string _strModifiedBy;
    private DateTime _dteModifiedOn;
    private string _strRecordStatus;

    public int AccountBudgetCode { get { return _intAccountAvailedCode; } set { _intAccountAvailedCode = value; } }
    public int FiscalYearCode { get { return _intFiscalYearCode; } set { _intFiscalYearCode = value; } }
    public string ResponsibilityCode { get { return _strResponsibilityCode; } set { _strResponsibilityCode = value; } }
    public string ChargeTypeCode { get { return _strChargeTypeCode; } set { _strChargeTypeCode = value; } }
    public int AccountItemCode { get { return _intAccountItemCode; } set { _intAccountItemCode = value; } }
    public double AvailedValue { get { return _dblAvailedValue; } set { _dblAvailedValue = value; } }
    public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }
    public string CreatedBy { get { return _strCreatedBy; } set { _strCreatedBy = value; } }
    public DateTime CreatedOn { get { return _dteCreatedOn; } set { _dteCreatedOn = value; } }
    public string ModifiedBy { get { return _strModifiedBy; } set { _strModifiedBy = value; } }
    public DateTime ModifiedOn { get { return _dteModifiedOn; } set { _dteModifiedOn = value; } }
    public string RecordStatus { get { return _strRecordStatus; } set { _strRecordStatus = value; } }


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
            cmd.CommandText = "INSERT INTO Budget.AccountAvailedRC (fiscal_year_code,rc_code,charge_type_code,accnt_items_code,availed_value,remarks,created_by,created_on,updated_by, updated_on, record_status) VALUES (@fiscal_year_code,@rc_code,@charge_type_code,@accnt_items_code,@availed_value,@remarks,@created_by,@created_on,@updated_by, @updated_on, @record_status)";
            cmd.Parameters.Add(new SqlParameter("@fiscal_year_code", _intFiscalYearCode));
            cmd.Parameters.Add(new SqlParameter("@rc_code", _strResponsibilityCode));
            cmd.Parameters.Add(new SqlParameter("@charge_type_code", _strChargeTypeCode));
            cmd.Parameters.Add(new SqlParameter("@accnt_items_code", _intAccountItemCode));
            cmd.Parameters.Add(new SqlParameter("@availed_value", _dblAvailedValue));
            cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
            cmd.Parameters.Add(new SqlParameter("@created_by", _strCreatedBy));
            cmd.Parameters.Add(new SqlParameter("@created_on", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@updated_by", _strModifiedBy));
            cmd.Parameters.Add(new SqlParameter("@updated_on", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@record_status", "1"));
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

    public static DataTable GetDSGforActualBudget(string pRcCode, int pFiscalYearCode, string pAccountItemCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT accnt_avail_rc_code, rc_code,(SELECT rcname FROM HR.Rc WHERE rccode=Budget.AccountAvailedRC.rc_code) AS rcname,charge_type_code,(SELECT charge_type_name FROM Budget.ChargeType WHERE charge_type_code=Budget.AccountAvailedRC.charge_type_code) AS charge_type_name,accnt_items_code,(SELECT accnt_items_name FROM Budget.AccountItems WHERE accnt_items_code=Budget.AccountAvailedRC.accnt_items_code) AS accnt_items_name, availed_value,remarks,created_by,created_on FROM Budget.AccountAvailedRC WHERE rc_code=@rc_code AND accnt_items_code=@accnt_items_code AND fiscal_year_code=@fiscal_year_code AND record_status='1' ORDER BY created_on DESC";
                cmd.Parameters.Add(new SqlParameter("@rc_code", pRcCode));
                cmd.Parameters.Add(new SqlParameter("@fiscal_year_code", pFiscalYearCode));
                cmd.Parameters.Add(new SqlParameter("@accnt_items_code", pAccountItemCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

}