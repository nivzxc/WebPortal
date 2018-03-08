using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public static class clsSIS
{
	public enum SISUsers
	{
		Admin = 0,
		EliteUser = 1,
		ChannelManager = 2,
		Encoder = 3,
		User = 4
	}

	public static int TotalSchools { get { return 94; } }
	public static int TotalSchoolsCollege { get { return 59; } }
	public static int TotalSchoolsEC { get { return 35; } }
	
	public static bool IsUserValid(SISUsers pUserType, string pUsername)
	{
		bool blnReturn = false;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (pUserType == SISUsers.Encoder)
				cmd.CommandText = "SELECT userlvl FROM CM.SisUsers WHERE userlvl='encoder' AND username='" + pUsername + "' AND pstatus='1'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnReturn = dr.Read();
			dr.Close();
		}
		return blnReturn;
	}

	public static void AuthenticateUser(SISUsers pUserType, string pUserName)
	{
		bool blnHasRecord;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (pUserType == SISUsers.Encoder)
				cmd.CommandText = "SELECT userlvl FROM CM.SisUsers WHERE username='" + pUserName + "' AND userlvl='encoder'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnHasRecord = dr.Read();
			dr.Close();
		}

		if (!blnHasRecord)
			HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
	}

}
