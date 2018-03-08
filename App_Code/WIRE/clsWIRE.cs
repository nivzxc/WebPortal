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

public static class clsWIRE
{

	public enum WireUsers
	{
		Administrator = 0,
		EliteUsers = 1,
		ChannelManager = 2,
		HQUsers = 3,
		SchoolEncoder = 4
	}

	public static int DayAdjustment { get { return (DateTime.IsLeapYear(DateTime.Now.Year) ? 2 : 1); } }
 public static string TableNameEncodeDates { get { return "encodedates0809"; } }
 public static string TableNameSchlProg { get { return "schlprog0809"; } }
 public static string TableNameTy { get { return "data0809"; } }
 public static string TableNameLy { get { return "data0708"; } }
 public static string TableNameTyWeek { get { return "week0809"; } }
 public static string TableNameLyWeek { get { return "week0708"; } }
 public static string StartDate { get { return "2008-04-01"; } }
 public static string EndDate { get { return "2008-06-30"; } }
 public static string SchoolYear { get { return "2008-2009"; } }
	public static string Semester { get { return "1"; } }

	public static bool IsUser(WireUsers pWireUsers, string pUserName)
	{
		bool blnHasRecord;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (pWireUsers == WireUsers.Administrator)
				cmd.CommandText = "SELECT username FROM CM.WireUsers WHERE username='" + pUserName + "' AND userlvl='admin'";
			else if (pWireUsers == WireUsers.EliteUsers)
				cmd.CommandText = "SELECT username FROM CM.WireUsers WHERE username='" + pUserName + "' AND userlvl='eu'";
			else if (pWireUsers == WireUsers.ChannelManager)
				cmd.CommandText = "SELECT username FROM CM.WireUsers WHERE username='" + pUserName + "' AND userlvl='cm'";
			else if (pWireUsers == WireUsers.HQUsers)
				cmd.CommandText = "SELECT username FROM CM.WireUsers WHERE username='" + pUserName + "' AND userlvl='hu'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnHasRecord = dr.Read();
			dr.Close();
		}

		return blnHasRecord;
	}

 public static void AuthenticateUser(string pUserName, WireUsers pWireUser)
 {
  bool blnHasRecord;
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   if(pWireUser == WireUsers.EliteUsers)
    cmd.CommandText = "SELECT username FROM CM.WireUsers WHERE username='" + pUserName + "' AND userlvl='eu'";
   else if (pWireUser == WireUsers.Administrator)
    cmd.CommandText = "SELECT username FROM CM.WireUsers WHERE username='" + pUserName + "' AND userlvl='admin'";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   blnHasRecord = dr.Read();
   dr.Close();
  }

  if (!blnHasRecord)
   HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
 }

}
