using System;
using System.Data;
using System.Data.SqlClient;

public class clsModule : IDisposable
{
    public clsModule() { }

    public void Dispose() { GC.SuppressFinalize(this); }

    //////////////////////////////////
    ///////// Static Members /////////
    //////////////////////////////////

    public static string OvertimeModule { get { return "018"; } } //overtime module
    public static string ATWModule { get { return "021"; } } // Authority to Work Module
    public static string IARModule { get { return "022"; } } // Internet Access Request module
    public static string RFPModule { get { return "023"; } } // Request for payment module
    public static string CATAModule { get { return "CATA"; } } // CATA module
    public static string LeaveModule { get { return "015"; } } // leave module
    public static string MRCFModule { get { return "MRCF"; } } // MRCF module
    public static string UTModule { get { return "016"; } } // Undertime module
    public static string OBModule { get { return "017"; } } // official business module
    public static string SportsFestModule { get { return "019"; } }
    public static string RequisitionModule { get { return "REQU"; } } //Requisition module
    public static string GroupUpdateModule { get { return "GROUPDATE"; } } // Group Update module
    public static string RewardModule { get { return "REWARD"; } }
    public static string TransmittalModule { get { return "TRAN"; } } // Transmittal module

    public static string ValidateModule(string pModuleCode)
    {
        string strReturn = "";
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT modlcat FROM Speedo.Modules WHERE modlcode=@modlcode";
                cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString().Trim();
            }
        }
        catch
        { strReturn = ""; }
        return strReturn;
    }

    public static DataTable GetDSGUser(string pModuleCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  modlcode, username,(SELECT lastname + ', ' + firname FROM HR.Employees WHERE username=Users.UsersModules.username) AS name, pstatus FROM Users.UsersModules WHERE modlcode=@modlcode AND pstatus='1' ORDER BY name";
            cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static int DeleteUser(string pModuleCode, string pUsername)
    {
        int intReturn = 0;
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "DELETE FROM Users.UsersModules WHERE modlcode=@modlcode AND username=@username";
                cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();

                intReturn = cmd.ExecuteNonQuery();
            }
        }
        catch { }
        return intReturn;
    }

    public static int InserUserModule(string pModuleCode, string pUsername)
    {
        int intReturn = 0;
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "INSERT INTO Users.UsersModules VALUES(@modlcode, @username,'1')";
                cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();

                intReturn = cmd.ExecuteNonQuery();
            }
        }
        catch { }
        return intReturn;
    }

    public static bool IsExistUserModule(string pModuleCode, string pUsername)
    {
        bool blnReturn = false;
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username FROM Users.UsersModules WHERE modlcode=@modlcode AND username=@username";
                cmd.Parameters.Add(new SqlParameter("@modlcode", pModuleCode));
                cmd.Parameters.Add(new SqlParameter("@username", pUsername));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    blnReturn = true;
                }
            }
        }
        catch { }
        return blnReturn;
    }
}