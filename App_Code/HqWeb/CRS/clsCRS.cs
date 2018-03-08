using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class clsCRS
{
	/* 
	 * Courseware Details Status 
	 * F - For endorsement
	 * E - Endorsed by CM Head
	 * D - Disapproved by CM Head
	 * P - Partial Dispatch
	 * C - Complete Dispatch
	 * 
	 * Courseware Dispatch Status
	 * P - Partial Dispatch
	 * C - Complete Dispatch
	 * 
 */

	public enum CRSUserType
	{
		Guest = 0,
		ChannelManager = 1,
		ChannelManagerHead = 2,
		CoursewareCoordinator = 3,
		EliteUsers = 4
	}

	public enum CRSStatus
	{
		ForEndorsement = 0,
		Endorsed = 1,
		Processed = 2,
	}

	// Courseware Material Request Status
	public enum CRSDetailsStatus
	{
		NotProcessed = 0,
		Partial = 1,
		Complete = 2,
	}

	private string strCrsCode;
	private string strSchlCode;
	private string strChannelManager;
	private string strRemarks;
	private string strChannelManagerHead;
	private string strChannelManagerHeadRemarks;
	private string strCoursewareCoordinator;
	private string strCoursewareCoordinatorRemarks;
	private DateTime dteDateRequested;
	private double dblTotalCost;
	private int intReorder;

	public clsCRS()
	{

	}

	public clsCRS(string pCRSCode)
	{
		strCrsCode = pCRSCode;
	}

	// =-=-=-=-=-=-=-=-=-=-
	// =-=- Properties =-=-
	// =-=-=-=-=-=-=-=-=-=-

	public string CRSCode { get { return strCrsCode; } set { strCrsCode = value; } }
	public string SchoolCode { get { return strSchlCode; } }
	public string ChannelManager { get { return strChannelManager; } }
	public string Remarks { get { return strRemarks; } }
	public string ChannelManagerHead { get { return strChannelManagerHead; } }
	public string ChannelManagerHeadRemarks { get { return strChannelManagerHeadRemarks; } }
	public string CoursewareCoordinator { get { return strCoursewareCoordinator; } }
	public string CoursewareCoordinatorRemarks { get { return strCoursewareCoordinatorRemarks; } }
	public DateTime DateRequested { get { return dteDateRequested; } }
	public double TotalCost { get { return dblTotalCost; } }
	public int Reorder { get { return intReorder; } }

	// =-=-=-=-=-=-=-=-=
	// =-=- Methods -=-=
	// =-=-=-=-=-=-=-=-=

	public void Load()
	{
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT * FROM CM.CRS WHERE crscode='" + strCrsCode + "'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.Read())
			{
				strSchlCode = dr["schlcode"].ToString();
	   strChannelManager = dr["cmname"].ToString();
				strRemarks = dr["remarks"].ToString();
				strChannelManagerHead = dr["cmhname"].ToString();
				strChannelManagerHeadRemarks = dr["cmhrem"].ToString();
				strCoursewareCoordinator = dr["ccname"].ToString();
				strCoursewareCoordinatorRemarks = dr["ccrem"].ToString();
				dteDateRequested = Convert.ToDateTime(dr["datereq"].ToString());
				dblTotalCost = Convert.ToDouble(dr["tcost"].ToString());
				intReorder = Convert.ToInt32(dr["treorder"].ToString());
			}
			dr.Close();
		}
	}

	// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=
	// =-=-= Static Members  =-=-=-=
	// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=

	public static string GetCAStatus(string pCourseCode)
	{
		string strReturn = "";
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT cwdstat FROM Academics.CoursewareInventory WHERE crsecode='" + pCourseCode + "'";
			cn.Open();
			try
			{ strReturn = cmd.ExecuteScalar().ToString(); }
			catch
			{ strReturn = ""; }
		}
		return strReturn;
	}

	public static string ToCAStatusDesc(string pCAStatus)
	{
		string strReturn = "";
		if (pCAStatus == "C")
			strReturn = "Completed";
		else if (pCAStatus == "O")
			strReturn = "On-going";
		else if (pCAStatus == "N")
			strReturn = "No Courseware";
		else if (pCAStatus == "G")
			strReturn = "Guidelines";
		else if (pCAStatus == "F")
			strReturn = "For Development";
		else if (pCAStatus == "T")
			strReturn = "TOP Courseware";
		else if (pCAStatus == "U")
			strReturn = "Not used anymore";
		else
			strReturn = "No Status";
		return strReturn;
	}

	public static int OrderedCoursewareCount(string pSchlCode, string pCourseCode)
	{
		int intReturn = 0;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(crsecode) FROM CM.CrsDetails WHERE crsecode='" + pCourseCode + "' AND pstatus IN ('C','P') AND crscode IN (SELECT crscode FROM CM.Crs WHERE schlcode='" + pSchlCode + "')";
			cn.Open();
			intReturn = (int)cmd.ExecuteScalar();

			cmd.CommandText = "SELECT COUNT(crsecode) FROM CM.CrsPrevOrders WHERE crsecode='" + pCourseCode + "' AND schlcode='" + pSchlCode + "'";
			intReturn += (int)cmd.ExecuteScalar();
		}
		return intReturn;
	}

	public static CRSUserType GetUserLevel(string pUsername)
	{
		string strUserLevel = "";
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT userlvl FROM CM.CrsUsers WHERE username='" + pUsername + "'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.Read())
				strUserLevel = dr["userlvl"].ToString();
			dr.Close();
		}
		switch(strUserLevel)
		{
			case "cm":
				return CRSUserType.ChannelManager;
			case "cmh":
				return CRSUserType.ChannelManagerHead;
			case "cc":
				return CRSUserType.CoursewareCoordinator;
			case "eu":
				return CRSUserType.EliteUsers;
			default:
				return CRSUserType.Guest;
		}
	}

	public static string GetCrsSchoolName(string pCrsCode)
	{
		string strReturn = "";
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT schlname FROM CM.Schools WHERE schlcode=(SELECT schlcode FROM CM.Crs WHERE crscode='" + pCrsCode + "')";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if(dr.Read())
				strReturn = dr["schlname"].ToString();
			dr.Close();
		}
		return strReturn;
	}

	public static string GetCrsSchoolCode(string pCrsCode)
	{
		string strReturn = "";
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT schlcode FROM CM.Crs WHERE crscode='" + pCrsCode + "'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if(dr.Read())
				strReturn = dr["schlcode"].ToString();
			dr.Close();
		}
		return strReturn;
	}

	public static string ToCrsDetailsStatusDesc(string pStatus)
	{
		if (pStatus == "F")
			return "For Endorsement";
		else if (pStatus == "E")
			return "Endorsed";
		else if (pStatus == "D")
			return "Disapproved by CM";
		else if (pStatus == "P")
			return "Partial Dispatch";
		else if (pStatus == "C")
			return "Complete Dispatch";
		else
			return "Status Unknown";
	}

	public static string ToDispatchTypeDesc(string pDispType)
	{
		if (pDispType == "P")
			return "Partial Dispatch";
		else if (pDispType == "C")
			return "Complete Dispatch";
		else
			return "Dispatch Type Unknown";
	}

	public static bool IsUser(CRSUserType pUserType, string pUsername)
	{
		bool blnReturn = false;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (pUserType == CRSUserType.ChannelManager)
				cmd.CommandText = "SELECT userlvl FROM CM.CrsUsers WHERE userlvl='cm' AND username='" + pUsername + "'";
			else if (pUserType == CRSUserType.ChannelManagerHead)
				cmd.CommandText = "SELECT userlvl FROM CM.CrsUsers WHERE userlvl='cmhead' AND username='" + pUsername + "'";
			else if (pUserType == CRSUserType.CoursewareCoordinator)
				cmd.CommandText = "SELECT userlvl FROM CM.CrsUsers WHERE userlvl='cc' AND username='" + pUsername + "'";
			else if (pUserType == CRSUserType.EliteUsers)
				cmd.CommandText = "SELECT userlvl FROM CM.CrsUsers WHERE userlvl='eu' AND username='" + pUsername + "'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnReturn = dr.Read();
			dr.Close();
		}
		return blnReturn;
	}

	/// <summary>
	/// Get the channel management head approver
	/// </summary>
	/// <returns></returns>
	public static string GetCmh()
	{
		string strReturn = "";
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT username FROM CM.CrsUsers WHERE userlvl='cmhead'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.Read())
				strReturn = dr["username"].ToString();
			dr.Close();
		}
		return strReturn;
	}

	/// <summary>
	/// Get the courseware coordinator
	/// </summary>
	/// <returns></returns>
	public static string GetCc()
	{
		string strReturn = "";
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT username FROM CM.CrsUsers WHERE userlvl='cc'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.Read())
				strReturn = dr["username"].ToString();
			dr.Close();
		}
		return strReturn;
	}

	public static string GetRequestStatusIcon(int pTCourseware, int pTForApproval, int pTDisapproved, int pTForProcessing, int pTPartial, int pTComplete)
	{
	 string strReturn = "";

		if (pTCourseware == pTDisapproved)
			strReturn = "Disapproved.png";
		else if (pTForApproval > 0)
			strReturn = "Approval.png";
		else if (pTForApproval == 0 && (pTForProcessing > 0 || pTPartial > 0))
			strReturn = "ForProcessing.png";
		else
			strReturn = "Approved.png";

	 return strReturn;
	}

	public static string GetRequestStatusRemarks(int pTCourseware, int pTForApproval, int pTDisapproved, int pTForProcessing, int pTPartial, int pTComplete)
	{
		string strReturn = "";

		if (pTCourseware == pTDisapproved)
			strReturn = "All request has been disapproved";
		else if (pTForApproval == 0 && pTForProcessing == 0 && pTPartial == 0)
			strReturn = "All items has been dispatch";
		else
			strReturn = (pTForApproval == 0 ? "" : pTForApproval + " items are for approval<br>") +
															(pTForProcessing == 0 ? "" : pTForProcessing + " items are for processing<br>") +
															(pTPartial == 0 ? "" : pTPartial + " items are patially dispatch<br>") +
															(pTComplete == 0 ? "" : pTComplete + " items are completely dispatch");

		return strReturn;
	}

	public static string GetCrsDetailsStatusIcon(string pStatus)
	{
		if (pStatus == "E")
			return "gear16.png";
		else if (pStatus == "D")
			return "close16.png";
		else if (pStatus == "F")
			return "document16.png";
		else if (pStatus == "P")
			return "mailsend16.png";
		else if (pStatus == "C")
			return "check16.png";
		else
			return "";
	}

	public static double GetCoursewarePrice()
	{
		return 5000.00;
	}

	public static void AuthenticateUser(CRSUserType pcutUserType, string pUserName, string pCrsCode)
	{
		bool blnHasRecord;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			if (pcutUserType == CRSUserType.ChannelManager)
				cmd.CommandText = "SELECT cmname FROM CM.Crs WHERE crscode='" + pCrsCode + "' AND cmname='" + pUserName + "'";
			else if (pcutUserType == CRSUserType.ChannelManagerHead)
				cmd.CommandText = "SELECT cmhname FROM CM.Crs WHERE crscode='" + pCrsCode + "' AND cmhname='" + pUserName + "'";
			else if (pcutUserType == CRSUserType.CoursewareCoordinator)
				cmd.CommandText = "SELECT ccname FROM CM.Crs WHERE crscode='" + pCrsCode + "' AND ccname='" + pUserName + "'";
			else if (pcutUserType == CRSUserType.EliteUsers)
				cmd.CommandText = "SELECT username FROM CM.CrsUsers WHERE username='" + pUserName + "' AND userlvl='eu'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnHasRecord = dr.Read();
			dr.Close();
		}

		if (!blnHasRecord)
		 System.Web.HttpContext.Current.Response.Redirect("~/AccessDenied.aspx");
	}

}
