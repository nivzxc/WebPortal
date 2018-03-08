using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AccountItems
/// </summary>
public class AccountItems:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }

    private int _intAccountItemsCode;
    private string _strAccountItemsName;
    private int _intAccountCategoryCode;
    private string _strOracleCode;
    private string _strCreatedBy;
    private DateTime _dteCreatedOn;
    private string _strModifiedBy;
    private DateTime _dteModifiedOn;
    private string _strRecordStatus;

    public int AccountItemsCode { get { return _intAccountItemsCode; } set { _intAccountItemsCode = value; } }
    public string AccountItemsName { get { return _strAccountItemsName; } set { _strAccountItemsName = value; } }
    public int AccountCategoryCode { get { return _intAccountCategoryCode; } set { _intAccountCategoryCode = value; } }
    public string OracleCode { get { return _strOracleCode; } set { _strOracleCode = value; } }
    public string CreatedBy { get { return _strCreatedBy; } set { _strCreatedBy = value; } }
    public DateTime CreatedOn { get { return _dteCreatedOn; } set { _dteCreatedOn = value; } }
    public string ModifiedBy { get { return _strModifiedBy; } set { _strModifiedBy = value; } }
    public DateTime ModifiedOn { get { return _dteModifiedOn; } set { _dteModifiedOn = value; } }
    public string RecordStatus { get { return _strRecordStatus; } set { _strRecordStatus = value; } }

    public void Fill()
    {
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Budget.AccountItems WHERE accnt_items_code=@accnt_items_code";
            cmd.Parameters.Add(new SqlParameter("@accnt_items_code", _intAccountItemsCode));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                _intAccountItemsCode = Convert.ToInt16(dr["accnt_items_code"]);
                _strAccountItemsName = dr["accnt_items_name"].ToString();
                _intAccountCategoryCode = Convert.ToInt16(dr["accnt_cat_code"]);
                _strOracleCode = dr["oracle_code"].ToString();
                _strCreatedBy = dr["created_by"].ToString();
                _dteCreatedOn = Convert.ToDateTime(dr["created_on"].ToString());
                _strModifiedBy = dr["updated_by"].ToString();
                _dteModifiedOn = Convert.ToDateTime(dr["updated_on"].ToString());
                _strRecordStatus = dr["record_status"].ToString();
            }
            dr.Close();
        }
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
            cmd.CommandText = "INSERT INTO Budget.AccountItems (accnt_items_name, accnt_cat_code,oracle_code,created_by,created_on,updated_by, updated_on, record_status) VALUES (@accnt_items_name, @accnt_cat_code, @oracle_code, @created_by,@created_on,@updated_by, @updated_on, @record_status)";
            cmd.Parameters.Add(new SqlParameter("@accnt_items_name", _strAccountItemsName));
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_code", _intAccountCategoryCode));
            cmd.Parameters.Add(new SqlParameter("@oracle_code", _strOracleCode));
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
            cmd.CommandText = "UPDATE Budget.AccountItems SET accnt_items_name=@accnt_items_name,accnt_cat_code=@accnt_cat_code,oracle_code=@oracle_code, updated_by=@updated_by,updated_on=GETDATE() WHERE accnt_items_code=@accnt_items_code";
            cmd.Parameters.Add(new SqlParameter("@accnt_items_code", _intAccountItemsCode));
            cmd.Parameters.Add(new SqlParameter("@accnt_items_name", _strAccountItemsName));
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_code", _intAccountCategoryCode));
            cmd.Parameters.Add(new SqlParameter("@oracle_code", _strOracleCode));
            cmd.Parameters.Add(new SqlParameter("@updated_by", _strModifiedBy));
            cmd.Parameters.Add(new SqlParameter("@record_status", _strRecordStatus));
            intReturn = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            tran.Commit();
        }
        catch
        { tran.Rollback(); }
        finally { cn.Close(); }

        return intReturn;
    }

    public static DataTable GetDSG()
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT accnt_items_code,accnt_items_name,accnt_cat_code,(SELECT accnt_cat_name FROM Budget.AccountCategory WHERE accnt_cat_code=Budget.AccountItems.accnt_cat_code) AS accnt_cat_name, oracle_code, (CASE WHEN record_status='1' THEN 'True' ELSE 'False' END) AS record_status_YN FROM Budget.AccountItems ORDER BY accnt_cat_name ASC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }


    public static DataTable GetDSLAccountItem(string pAccountCategoryCode)
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
                cmd.CommandText = "SELECT accnt_items_code AS pValue, accnt_items_name AS pText FROM Budget.AccountItems WHERE record_status='1' AND accnt_cat_code=@accnt_cat_code ORDER BY accnt_items_name ASC";
                cmd.Parameters.Add(new SqlParameter("@accnt_cat_code", pAccountCategoryCode));
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


    public static string GetItemName(int pAccountItems)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT accnt_items_name FROM Budget.AccountItems WHERE accnt_items_code=@accnt_items_code";
            cmd.Parameters.Add(new SqlParameter("@accnt_items_code", pAccountItems));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }
	public AccountItems()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}