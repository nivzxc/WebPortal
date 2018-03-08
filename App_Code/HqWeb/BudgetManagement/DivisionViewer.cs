using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DivisionViewer
/// </summary>
public class DivisionViewer
{
    public static bool HasAccess(string username)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Budget.DivisionViewer WHERE username=@username";
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            blnReturn = dr.Read();
            dr.Close();
        }
        return blnReturn;
    }

	public DivisionViewer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}