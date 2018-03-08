using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for clsThreadCategoryUser
/// </summary>
public class clsThreadCategoryUser:IDisposable
{
    public void Dispose() { GC.SuppressFinalize(this); }




    //Static//
    public static bool IsAllowedToAccess(string pUsername)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT ThreadCategoryUserId, username, deptcode, ThreadCategoryID, IsActive FROM Portal.ThreadCategoryUser WHERE isActive='1' AND username=@username";
            cmd.Parameters.Add(new SqlParameter("@username",pUsername));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
        }
        return blnReturn;
    }

    public static DataTable GetDSLThreadCategoryPerUser(string pUsername)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT ThreadCategoryID,Name FROM Portal.ThreadCategories WHERE IsActive='1' AND ThreadCategoryID IN (SELECT ThreadCategoryID FROM Portal.ThreadCategoryUser WHERE username=@username)";
            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

}