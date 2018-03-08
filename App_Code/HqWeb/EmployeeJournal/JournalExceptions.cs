using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using HRMS;

/// <summary>
/// Summary description for JournalExceptions
/// </summary>
public class JournalExceptions
{
	public JournalExceptions()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int CountIfExist(int pWeekCode, string pUsername)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Portal.JournalExceptions WHERE WeekCode=@WeekCode AND username=@username";
            cmd.Parameters.Add(new SqlParameter("@WeekCode", pWeekCode));
            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
        }
        return intReturn;
    }

}