using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HRMS;

/// <summary>
/// Summary description for EmployeeReviewer
/// </summary>
public class EmployeeReviewer
{
	public EmployeeReviewer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable DSLEmployeeList()
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ' ' + firname AS ptext FROM HR.Employees WHERE pstatus='1' AND username NOT IN (SELECT username FROM Portal.JournalReviewer) ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static string GetReviewer(string pUsername)
    {
        string strReturn;
        using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT happrover FROM Portal.JournalReviewer WHERE username=@username";
            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
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
}