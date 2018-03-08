using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AccountBudget
/// </summary>
public class AccountBudget:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    private int _intAccountBudgetCode;
    private string _strAccountBudgetCategoryCode;
    private int _intFiscalYearCode;
    private string _strResponsibilityCode;
    private string _strChargeTypeCode;
    private int _intAccountItemCode;
    private double _dblBudgetValue;
    private string _strRemarks;
    private string _strCreatedBy;
    private DateTime _dteCreatedOn;
    private string _strModifiedBy;
    private DateTime _dteModifiedOn;
    private string _strRecordStatus;

    public int AccountBudgetCode { get { return _intAccountBudgetCode; } set { _intAccountBudgetCode = value; } }
    public string AccountBudgetCategoryCode { get { return _strAccountBudgetCategoryCode; } set { _strAccountBudgetCategoryCode = value; } }
    public int FiscalYearCode { get { return _intFiscalYearCode; } set { _intFiscalYearCode = value; } }
    public string ResponsibilityCode { get { return _strResponsibilityCode; } set { _strResponsibilityCode = value; } }
    public string ChargeTypeCode { get { return _strChargeTypeCode; } set { _strChargeTypeCode = value; } }
    public int AccountItemCode { get { return _intAccountItemCode; } set { _intAccountItemCode = value; } }
    public double BudgetValue { get { return _dblBudgetValue; } set { _dblBudgetValue = value; } }
    public string Remarks { get { return _strRemarks; } set { _strRemarks= value; } }
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
            cmd.CommandText = "INSERT INTO Budget.AccountBudgetRC (accnt_bud_cat_code,fiscal_year_code,rc_code,charge_type_code,accnt_items_code,bud_value,remarks,created_by,created_on,updated_by, updated_on, record_status) VALUES (@accnt_bud_cat_code,@fiscal_year_code,@rc_code,@charge_type_code,@accnt_items_code,@bud_value,@remarks,@created_by,@created_on,@updated_by, @updated_on, @record_status)";
            cmd.Parameters.Add(new SqlParameter("@accnt_bud_cat_code", _strAccountBudgetCategoryCode));
            cmd.Parameters.Add(new SqlParameter("@fiscal_year_code", _intFiscalYearCode));
            cmd.Parameters.Add(new SqlParameter("@rc_code", _strResponsibilityCode));
            cmd.Parameters.Add(new SqlParameter("@charge_type_code", _strChargeTypeCode));
            cmd.Parameters.Add(new SqlParameter("@accnt_items_code", _intAccountItemCode));
            cmd.Parameters.Add(new SqlParameter("@bud_value", _dblBudgetValue));
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

    public static bool IsInitialBudgetExist(int pFiscalYearCode,string pRCCode, int pAccountItemCode)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {

                cmd.CommandText = "SELECT * FROM Budget.AccountBudgetRC WHERE fiscal_year_code=@fiscal_year_code AND rc_code=@rc_code AND accnt_items_code=@accnt_items_code AND accnt_bud_cat_code='01' AND record_status='1'";
                cmd.Parameters.Add(new SqlParameter("@fiscal_year_code", pFiscalYearCode));
                cmd.Parameters.Add(new SqlParameter("@rc_code", pRCCode));
                cmd.Parameters.Add(new SqlParameter("@accnt_items_code", pAccountItemCode));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    blnReturn = true;
                }
                cn.Close();
            }
        }
        return blnReturn;
    }

    public static DataTable GetDSG(string pDivisionCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT rccode,rcname FROM HR.Rc WHERE rccode IN (SELECT rccode FROM HR.Employees WHERE divicode=@divicode) AND status='1' ORDER BY rcname ASC";
                cmd.Parameters.Add(new SqlParameter("@divicode", pDivisionCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSG(string pDivisionCode, int pFiscalYearCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT rccode,rcname,((COALESCE((SELECT SUM(bud_value) FROM Budget.AccountBudgetRC WHERE rc_code=ABRC.rccode AND fiscal_year_code=@fiscal_year_code AND accnt_bud_cat_code IN ('01','02','03')),0))-(COALESCE((SELECT SUM(bud_value) FROM Budget.AccountBudgetRC WHERE rc_code=ABRC.rccode AND fiscal_year_code=@fiscal_year_code AND accnt_bud_cat_code IN ('04')),0))) AS bud_value,(COALESCE((SELECT SUM(availed_value) FROM Budget.AccountAvailedRC WHERE rc_code=ABRC.rccode AND fiscal_year_code=2),0)) AS availed_value FROM HR.Rc AS ABRC WHERE rccode IN (SELECT rccode FROM HR.Employees WHERE divicode=@divicode) AND status='1' ORDER BY rcname ASC";
                cmd.Parameters.Add(new SqlParameter("@divicode", pDivisionCode));
                cmd.Parameters.Add(new SqlParameter("@fiscal_year_code", pFiscalYearCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSGRCBudget(string pRcCode,int pFiscalYearCode, string pAccountItemCategoryCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT DISTINCT accnt_items_code,(SELECT accnt_items_name FROM Budget.AccountItems WHERE accnt_items_code=ABRC.accnt_items_code) AS accnt_items_name,(SELECT oracle_code FROM Budget.AccountItems WHERE accnt_items_code=ABRC.accnt_items_code) AS oracle_code,((COALESCE((SELECT SUM(bud_value) FROM Budget.AccountBudgetRC WHERE rc_code=ABRC.rc_code AND fiscal_year_code=@fiscal_year_code AND accnt_items_code=ABRC.accnt_items_code AND accnt_bud_cat_code IN ('01','02','03')),0))-(COALESCE((SELECT SUM(bud_value) FROM Budget.AccountBudgetRC WHERE rc_code=ABRC.rc_code AND fiscal_year_code=@fiscal_year_code AND accnt_items_code=ABRC.accnt_items_code AND accnt_bud_cat_code IN ('04')),0))) AS bud_value,(COALESCE((SELECT SUM(availed_value) FROM Budget.AccountAvailedRC WHERE rc_code=ABRC.rc_code AND fiscal_year_code=@fiscal_year_code AND accnt_items_code=ABRC.accnt_items_code),0)) AS availed_value FROM Budget.AccountBudgetRC AS ABRC WHERE rc_code=@rc_code AND fiscal_year_code=@fiscal_year_code AND accnt_items_code IN (SELECT accnt_items_code FROM budget.AccountItems WHERE accnt_cat_code=@accnt_cat_code)";
                cmd.Parameters.Add(new SqlParameter("@rc_code", pRcCode));
                cmd.Parameters.Add(new SqlParameter("@fiscal_year_code", pFiscalYearCode));
                cmd.Parameters.Add(new SqlParameter("@accnt_cat_code", pAccountItemCategoryCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSGforAdjustmentBudget(string pRcCode, int pFiscalYearCode, string pAccountItemCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT accnt_bud_rc_code,accnt_bud_cat_code,(SELECT accnt_bud_cat_name FROM Budget.AccountBudgetCategory WHERE accnt_bud_cat_code=Budget.AccountBudgetRC.accnt_bud_cat_code) AS accnt_bud_cat_name,rc_code,(SELECT rcname FROM HR.Rc WHERE rccode=Budget.AccountBudgetRC.rc_code) AS rcname,charge_type_code,(SELECT charge_type_name FROM Budget.ChargeType WHERE charge_type_code=Budget.AccountBudgetRC.charge_type_code) AS charge_type_name,accnt_items_code,(SELECT accnt_items_name FROM Budget.AccountItems WHERE accnt_items_code=Budget.AccountBudgetRC.accnt_items_code) AS accnt_items_name, bud_value,remarks,created_by,created_on FROM Budget.AccountBudgetRC WHERE rc_code=@rc_code AND accnt_items_code=@accnt_items_code AND fiscal_year_code=@fiscal_year_code AND record_status='1' ORDER BY created_on DESC";
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

    public AccountBudget()
	{
		//
		// TODO: Add constructor logic here
		//

        //SELECT DISTINCT accnt_items_code,(SELECT accnt_items_name FROM Budget.AccountItems WHERE accnt_items_code=ABRC.accnt_items_code) AS accnt_items_name,((SELECT bud_value FROM Budget.AccountBudgetRC WHERE rc_code=ABRC.rc_code AND accnt_items_code=ABRC.accnt_items_code AND accnt_bud_cat_code IN ('01','02','03'))) AS bud_value,((SELECT bud_value FROM Budget.AccountBudgetRC WHERE rc_code=ABRC.rc_code AND accnt_items_code=ABRC.accnt_items_code AND accnt_bud_cat_code IN ('04'))) AS bud_value2 FROM Budget.AccountBudgetRC AS ABRC WHERE rc_code='PQS'
	}
}