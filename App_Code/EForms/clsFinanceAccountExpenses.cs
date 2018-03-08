using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;
using System.Configuration;

/// <summary>
/// Summary description for clsFinanceAccountExpenses
/// </summary>
public class clsFinanceAccountExpenses:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }
	public clsFinanceAccountExpenses()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable GetDSLAccountExpenses()
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT aexpcode AS pvalue, aexpname AS ptext FROM Finance.AccountExpenses WHERE enabled='1' ORDER BY aexpname";
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static DataTable GetDSLAccountExpenses(string pRCCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT aexpcode AS pvalue, aexpname AS ptext FROM Finance.AccountExpenses WHERE enabled='1' AND aexpcode IN (SELECT aexpcode FROM Finance.RCAccountExpenses WHERE rccode=@rccode)";
            cmd.Parameters.Add(new SqlParameter("@rccode", pRCCode));
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static string GetAccountExpenseName(string pAccountExpenseCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT aexpname FROM Finance.AccountExpenses WHERE aexpcode=@aexpcode";
            cmd.Parameters.Add(new SqlParameter("@aexpcode", pAccountExpenseCode));
            cn.Open();
            try
            {
                strReturn = (string)cmd.ExecuteScalar();
            }
            catch
            {
            }
        }
        return strReturn;
    }

    public static DataTable GetDSGMainFormExpensesAmount(string pPcasCode,string pRCCode)
    {
        DataTable tblReturn = new DataTable();
        tblReturn.Columns.Add("aexpname");
        tblReturn.Columns.Add("amount");

        using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT aexpcode,(SELECT aexpname FROM Finance.AccountExpenses WHERE aexpcode=Finance.PCASRequestAllocation.aexpcode) AS aexpname,amount FROM Finance.PCASRequestAllocation WHERE pcascode=@pcascode AND rccode =@rccode";
            cmd.Parameters.Add(new SqlParameter("@pcascode", pPcasCode));
            cmd.Parameters.Add(new SqlParameter("@rccode", pRCCode));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DataRow drwNew = tblReturn.NewRow();
                drwNew["aexpname"] = dr["aexpname"].ToString();
                drwNew["amount"] = dr["amount"].ToString();

                tblReturn.Rows.Add(drwNew);
            }
            dr.Close();
        }
        return tblReturn;
    }
}