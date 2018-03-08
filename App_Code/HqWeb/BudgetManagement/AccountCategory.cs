using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AccountCategory
/// </summary>
public class AccountCategory: IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }
    private int _intAccountCategoryCode;
    private string _strAccountCategoryName;
    private string _strAccountCategoryTypeCode;
    private int _intRecordOrder;
    private string _strCreatedBy;
    private DateTime _dteCreatedOn;
    private string _strModifiedBy;
    private DateTime _dteModifiedOn;
    private string _strRecordStatus;

    public int AccountCateogryCode { get { return _intAccountCategoryCode; } set { _intAccountCategoryCode = value; } }
    public string AccountCategoryName { get { return _strAccountCategoryName; } set { _strAccountCategoryName = value; } }
    public string AccountCategoryTypeCode { get { return _strAccountCategoryTypeCode; } set { _strAccountCategoryTypeCode = value; } }
    public int RecordOrder { get { return _intRecordOrder; } set { _intRecordOrder = value; } }
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
            cmd.CommandText = "SELECT * FROM Budget.AccountCategory WHERE accnt_cat_code=@accnt_cat_code";
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_code", _intAccountCategoryCode));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                _intAccountCategoryCode = Convert.ToInt16(dr["accnt_cat_code"]);
                _strAccountCategoryName = dr["accnt_cat_name"].ToString();
                _strAccountCategoryTypeCode = dr["accnt_cat_type_code"].ToString();
                _intRecordOrder = Convert.ToInt16(dr["record_order"]);
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
            cmd.CommandText = "INSERT INTO Budget.AccountCategory (accnt_cat_name, accnt_cat_type_code,record_order,created_by,created_on,updated_by, updated_on, record_status) VALUES (@accnt_cat_name, @accnt_cat_type_code,@record_order,@created_by,@created_on,@updated_by, @updated_on, @record_status)";
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_name", _strAccountCategoryName));
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_type_code", _strAccountCategoryTypeCode));
            cmd.Parameters.Add(new SqlParameter("@record_order", _intRecordOrder));
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
            cmd.CommandText = "UPDATE Budget.AccountCategory SET accnt_cat_name=@accnt_cat_name,accnt_cat_type_code=@accnt_cat_type_code,record_order=@record_order,record_status=@record_status, updated_by=@updated_by,updated_on=GETDATE() WHERE accnt_cat_code=@accnt_cat_code";
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_code", _intAccountCategoryCode));
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_name", _strAccountCategoryName));
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_type_code", _strAccountCategoryTypeCode));
            cmd.Parameters.Add(new SqlParameter("@record_order", _intRecordOrder));
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

    public static DataTable GetDSLAccountCategory()
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
                cmd.CommandText = "SELECT accnt_cat_code AS pValue, accnt_cat_name AS pText FROM Budget.AccountCategory WHERE record_status='1' ORDER BY record_order ASC";
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

    public static DataTable GetDSG()
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT accnt_cat_code,accnt_cat_name,accnt_cat_type_code,(SELECT accnt_cat_type_name FROM Budget.AccountCategoryType WHERE accnt_cat_type_code=Budget.AccountCategory.accnt_cat_type_code) AS accnt_cat_type_name, (CASE WHEN record_status='1' THEN 'True' ELSE 'False' END) AS record_status_YN FROM Budget.AccountCategory ORDER BY record_order ASC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static DataTable GetDSG(string pAccountCategoryTypeCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT accnt_cat_code,accnt_cat_name,accnt_cat_type_code,(SELECT accnt_cat_type_name FROM Budget.AccountCategoryType WHERE accnt_cat_type_code=Budget.AccountCategory.accnt_cat_type_code) AS accnt_cat_type_name, (CASE WHEN record_status='1' THEN 'True' ELSE 'False' END) AS record_status_YN FROM Budget.AccountCategory WHERE accnt_cat_type_code=@accnt_cat_type_code ORDER BY record_order ASC";
                cmd.Parameters.Add(new SqlParameter("@accnt_cat_type_code", pAccountCategoryTypeCode));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static string GetCategoryName (int pAccountCategoryCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT accnt_cat_name FROM Budget.AccountCategory WHERE accnt_cat_code=@accnt_cat_code";
            cmd.Parameters.Add(new SqlParameter("@accnt_cat_code", pAccountCategoryCode));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }

}