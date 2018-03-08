using System.Data.SqlClient;

public static class clsSystemModule
{
    public static string ModuleSynergy { get { return "019"; } }
	public static string ModuleForum { get { return "025"; } }

    public static bool HasAccess(string moduleCode, string username)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM Users.UsersModules WHERE modlcode=@modlcode AND username=@username AND pstatus='1'";
			cmd.Parameters.Add(new SqlParameter("@modlcode", moduleCode));
			cmd.Parameters.Add(new SqlParameter("@username", username));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
        }
        return blnReturn;
    }
}