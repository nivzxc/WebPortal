using System;
using System.Data;
using System.Data.SqlClient;

public class clsModuleApprover : IDisposable
{
    public clsModuleApprover() { }

    //private string _strModuleApproverCode;
    //private string _strModuleCode;
    //private string _strUserName;
    //private string _strApproverLevel;
    //private string _strDivisionCode;
    //private string _strRCCode;
    //private string _strDepartmentCode;
    //private string _strEmail;
    //private string _strRequireApproval;
    //private string _strEnabled;
    //private string _strCreateBy;
    //private DateTime _dteCreateOn;
    //private string _strModifyBy;
    //private DateTime _dteModifyOn;

    public void Dispose() { GC.SuppressFinalize(this); }

    //////////////////////////////////
    ///////// Static Members /////////
    //////////////////////////////////

    public static bool IsApprover(string pUsername, string pModuleCode)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username FROM Speedo.ModuleApprover WHERE username='" + pUsername + "' AND modlcode='" + pModuleCode + "' AND penabled='1'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
        }
        return blnReturn;
    }

    public static bool IsApprover(string pUsername, string pModuleCode, string pLevel)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username FROM Speedo.ModuleApprover WHERE username='" + pUsername + "' AND modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND penabled='1'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
        }
        return blnReturn;
    }

    public static DataTable DSLApprover(string pModuleCode, string pLevel)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND penabled='1') ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static DataTable DSLRCApprover(string pModuleCode, string pLevel, string pRCCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND rccode = '" + pRCCode + "' AND penabled='1') ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static bool IsDivisionHeadApprovalRequired(string pModuleCode, string pRCCode, string pDivisionhead)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT rapprove FROM Speedo.ModuleApprover WHERE modlcode='" + pModuleCode + "' AND username='" + pDivisionhead + "' AND rccode='" + pRCCode + "' AND userlvl='2'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { strReturn = ""; }
        }
        return (strReturn == "1" ? true : false);
    }

    public static DataTable DSGLevel2(string pModuleCode, string pDivisionHead)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT mappcode,divicode, deptcode,rapprove FROM Speedo.ModuleApprover WHERE penabled='1' AND applevel='2' AND username='" + pDivisionHead + "' AND modlcode='" + pModuleCode + "' ORDER BY deptcode";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static DataTable DSLApproverDepartment(string pDepartmentCode, string pModuleCode, string pLevel)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE deptcode='" + pDepartmentCode + "' AND modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND penabled='1') ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static DataTable DSLApproverRC(string pRCCode, string pModuleCode, string pLevel)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE rccode='" + pRCCode + "' AND modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND penabled='1') ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    

    public static DataTable DSLApproverEmployee(string pUsername, string pModuleCode, string pLevel)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND penabled='1') ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static string GetApproverDivisionHead(string pUsername, string pModuleCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TOP(1) username AS pvalue, lastname + ', ' + firname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND modlcode='" + pModuleCode + "' AND applevel='2' AND penabled='1') ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
        }
        return strReturn;
    }

    public static DataTable GetDSLDivisionHeadApprover(string pUsername, string pModuleCode)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext "+
                              "FROM Users.Users "+
                              "WHERE username IN "+
                                    "(SELECT username FROM Speedo.ModuleApprover "+
                                     "WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') "+
                                     "AND modlcode='" + pModuleCode + "' AND applevel='2' AND penabled='1') "+
                              "ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static bool IsApprovalRequiredDepartment(string pApprover, string pModuleCode, string pLevel, string pDepartmentCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT rapprove FROM Speedo.ModuleApprover WHERE modlcode='" + pModuleCode + "' AND username='" + pApprover + "' AND applevel='" + pLevel + "' AND deptcode='" + pDepartmentCode + "' AND penabled='1'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { strReturn = ""; }
        }
        return (strReturn == "1" ? true : false);
    }

    public static int UpdateRequiredApprovalFlag(string pMappCode, string pRequiredApprovalFlag, string pUsername)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE Speedo.ModuleApprover SET rapprove='" + pRequiredApprovalFlag + "', modifyby='" + pUsername + "', modifyon='" + DateTime.Now + "' WHERE mappcode='" + pMappCode + "'";
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }

    // Added by Charlie for CATA and RFP
    public static string ApproverEmployee(string pUsername, string pModuleCode, string pLevel)
    {
        string strReturn = "";
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT username FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE deptcode=(SELECT TOP 1 deptcode FROM HR.Employees WHERE username='" + pUsername + "') AND modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND penabled='1')";
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
        }
        catch
        { strReturn = ""; }
        return strReturn;
    }

    public static DataTable GetDSLFinanceApprover(string pModuleCode, string pLevel)
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ', ' + firname AS ptext FROM Users.Users WHERE username IN (SELECT username FROM Speedo.ModuleApprover WHERE modlcode='" + pModuleCode + "' AND applevel='" + pLevel + "' AND penabled='1') AND pstatus='1' ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            da.Fill(tblReturn);
        }
        return tblReturn;
    }

    public static string GetModule(string pMapCode)
    {
        string strReturn = "";
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT modlcode FROM Speedo.ModuleApprover WHERE mappcode=@mappcode";
                cmd.Parameters.Add(new SqlParameter("@mappcode", pMapCode));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
        }
        catch
        { strReturn = ""; }
        return strReturn;
    }

    


}