using System;
using System.Data;
using System.Data.SqlClient;
using HRMS;
using System.Configuration;

/// <summary>
/// Summary description for clsFinanceAccountDueFrom
/// </summary>
public class clsFinanceAccountDueFrom:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }
	public clsFinanceAccountDueFrom()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable GetDSLAccountDueFrom()
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT schlcode AS pvalue, duefrom AS ptext FROM Finance.AccountDueFrom WHERE enabled='1'";
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static string GetDueFromName(string pSchoolCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT duefrom FROM Finance.AccountDueFrom WHERE schlcode=@schlcode";
            cmd.Parameters.Add(new SqlParameter("@schlcode", pSchoolCode));
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
}