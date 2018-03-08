using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public static class clsTeamMembers
{

	public static DataTable DSGTeamMember(int teamID)
	{
		DataTable tblReturn = new DataTable();
		tblReturn.Columns.Add("username");
		tblReturn.Columns.Add("fullname");
		tblReturn.Columns.Add("nickname");
		tblReturn.Columns.Add("position");
		tblReturn.Columns.Add("diviname");
		tblReturn.Columns.Add("deptname");

		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT HR.Employees.username,lastname,firname,nickname,position,deptname,division FROM HR.Division INNER JOIN (HR.Employees INNER JOIN HR.Department ON HR.Employees.deptcode = HR.Department.deptcode) ON HR.Division.divicode = HR.Employees.divicode WHERE HR.Employees.username IN (SELECT Username FROM Portal.TeamMember WHERE TeamID = @TeamID) ORDER BY firname";
			cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				DataRow drw = tblReturn.NewRow();
				drw["username"] = dr["username"].ToString();
				drw["fullname"] = dr["firname"].ToString() + " " + dr["lastname"].ToString();
				drw["nickname"] = dr["nickname"].ToString();
				drw["position"] = dr["position"].ToString();
				drw["diviname"] = dr["division"].ToString();
				drw["deptname"] = dr["deptname"].ToString();
				tblReturn.Rows.Add(drw);
			}
			dr.Close();
		}
		return tblReturn;
	}

	public static DataTable DSGTeamMember(int teamID, string gender)
	{
		DataTable tblReturn = new DataTable();
		tblReturn.Columns.Add("username");
		tblReturn.Columns.Add("fullname");
		tblReturn.Columns.Add("nickname");
		tblReturn.Columns.Add("position");
		tblReturn.Columns.Add("diviname");
		tblReturn.Columns.Add("deptname");

		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT HR.Employees.username,lastname,firname,nickname,position,deptname,division FROM HR.Division INNER JOIN (HR.Employees INNER JOIN HR.Department ON HR.Employees.deptcode = HR.Department.deptcode) ON HR.Division.divicode = HR.Employees.divicode WHERE HR.Employees.username IN (SELECT Username FROM Portal.TeamMember WHERE TeamID = @TeamID) AND gender='" + gender + "' ORDER BY firname";
			cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				DataRow drw = tblReturn.NewRow();
				drw["username"] = dr["username"].ToString();
				drw["fullname"] = dr["firname"].ToString() + " " + dr["lastname"].ToString();
				drw["nickname"] = dr["nickname"].ToString();
				drw["position"] = dr["position"].ToString();
				drw["diviname"] = dr["division"].ToString();
				drw["deptname"] = dr["deptname"].ToString();
				tblReturn.Rows.Add(drw);
			}
			dr.Close();
		}
		return tblReturn;
	}

	public static DataTable DSLIncluded(int teamID)
	{
		DataTable tblReturn = new DataTable();
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT Username AS pvalue, Username AS ptext FROM Portal.TeamMember WHERE TeamID = @TeamID ORDER BY Username";
			cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			cn.Open();
			da.Fill(tblReturn);
		}
		return tblReturn;
	}

	public static DataTable DSLExcluded()
	{
		int synergyID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

		DataTable tblReturn = new DataTable();
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT username AS pvalue, username AS ptext FROM HR.Employees WHERE pstatus='1' ORDER BY Username";
			cmd.Parameters.Add(new SqlParameter("@ActivityID", synergyID));
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			cn.Open();
			da.Fill(tblReturn);
		}
		return tblReturn;
	}

	public static int InsertMember(int teamID, string username)
	{
		int intReturn = 0;
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "INSERT INTO Portal.TeamMember(TeamID,Username) VALUES(@TeamID,@Username)";
			cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
			cmd.Parameters.Add(new SqlParameter("@Username", username));
			cn.Open();
			intReturn = cmd.ExecuteNonQuery();
		}
		return intReturn;
	}

    public static bool MemberExist(int teamID, string username)
    {
        bool blnReturn = false;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Username FROM Portal.TeamMember WHERE TeamID=@TeamID AND Username=@Username";
            cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
            cmd.Parameters.Add(new SqlParameter("@Username", username));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                blnReturn = true;
            }
            
        }
        return blnReturn;
    }

	public static int DeleteMember(int teamID, string username)
	{
		int intReturn = 0;
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "DELETE FROM Portal.TeamMember WHERE TeamID = @TeamID AND Username=@Username";
			cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
			cmd.Parameters.Add(new SqlParameter("@Username", username));
			cn.Open();
			intReturn = cmd.ExecuteNonQuery();
		}
		return intReturn;
	}

}