using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for FiscalYear
/// </summary>
public class FiscalYear:IDisposable
{
	public FiscalYear()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Dispose() { GC.SuppressFinalize(this); }

    public static string GetActiveYearCode()
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1(FiscalYearCode) FROM Portal.FiscalYear WHERE IsActive='1' ORDER by FiscalYearName DESC";
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }

    public static DataTable GetDSLAFiscalYears()
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
                cmd.CommandText = "SELECT FiscalYearCode AS pValue, FiscalYearName AS pText FROM Portal.FiscalYear WHERE IsActive='1' ORDER BY FiscalYearName ASC";
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
}