using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for DALPortal
/// </summary>
public static class DALPortal
{
	private static readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

	//public static string GetEventCategoryName(int eventCategoryID)
	//{
	//    string eventCategoryName = "";

	//    using (PortalDataContext pdc = new PortalDataContext())
	//    {
	//        eventCategoryName = (from ec in pdc.EventCategories
	//                             where ec.EventCategoryID == eventCategoryID
	//                             select ec.Name).SingleOrDefault();
	//    }

	//    return eventCategoryName;
	//}


	public static int DeleteEventGame(int gameID)
	{
		int intReturn = 0;
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cn.Open();
			cmd.Parameters.Add(new SqlParameter("@GameID", gameID));

			cmd.CommandText = "DELETE FROM Portal.EventGame WHERE GameID=@GameID";
			intReturn = cmd.ExecuteNonQuery();

			cmd.CommandText = "DELETE FROM Portal.EventGameOfficials WHERE GameID=@GameID";
			cmd.ExecuteNonQuery();

			cmd.CommandText = "DELETE FROM Portal.EventGameTeam WHERE GameID=@GameID";
			cmd.ExecuteNonQuery();

			cmd.CommandText = "DELETE FROM Portal.EventGameTeamPlayer WHERE GameID=@GameID";
			cmd.ExecuteNonQuery();
		}
		return intReturn;
	}

    public static int DeleteEventGameOfficial(int gameID, string username)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "DELETE FROM Portal.EventGameOfficials WHERE GameID=@GameID AND OfficialID=@OfficialID";
            cmd.Parameters.Add(new SqlParameter("@GameID", gameID));
            cmd.Parameters.Add(new SqlParameter("@OfficialID", username));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }

    public static int DeleteEventGameTeam(int gameID, int teamID)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "DELETE FROM Portal.EventGameTeam WHERE GameID=@GameID AND TeamID=@TeamID";
            cmd.Parameters.Add(new SqlParameter("@GameID", gameID));
            cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }

    public static int DeleteEventGameTeamPlayer(int gameID, int teamID, string username)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "DELETE FROM Portal.EventGameTeamPlayer WHERE GameID=@GameID AND TeamID=@TeamID AND Username=@Username";
            cmd.Parameters.Add(new SqlParameter("@GameID", gameID));
			cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }

	public static DataTable DSLEventsAll()
	{
		DataTable tblReturn = new DataTable();
		tblReturn.Columns.Add("EventID");
		tblReturn.Columns.Add("EventName");
		DataRow drwN = tblReturn.NewRow();
		drwN["EventID"] = "ALL";
		drwN["EventName"] = "All";
		tblReturn.Rows.Add(drwN);

		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT EventID, Name FROM Portal.Events WHERE ActivityID=@ActivityID AND IsActive='true' ORDER BY Name";
			cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				drwN = tblReturn.NewRow();
				drwN["EventID"] = dr["EventID"].ToString();
				drwN["EventName"] = dr["Name"].ToString();
				tblReturn.Rows.Add(drwN);
			}
			dr.Close();
		}
		return tblReturn;
	}

    public static DataTable DSLGamePhase()
    {
        DataTable tblReturn = new DataTable();
        tblReturn.Columns.Add("pvalue");
        tblReturn.Columns.Add("ptext");

        DataRow drwN = tblReturn.NewRow();
        drwN["pvalue"] = "1";
        drwN["ptext"] = "Elimination";
        tblReturn.Rows.Add(drwN);

        drwN = tblReturn.NewRow();
        drwN["pvalue"] = "2";
        drwN["ptext"] = "Semi-Finals";
        tblReturn.Rows.Add(drwN);

        drwN = tblReturn.NewRow();
        drwN["pvalue"] = "3";
        drwN["ptext"] = "Battle For Third";
        tblReturn.Rows.Add(drwN);

        drwN = tblReturn.NewRow();
        drwN["pvalue"] = "4";
        drwN["ptext"] = "Championship";
        tblReturn.Rows.Add(drwN);

        return tblReturn;
    }

	public static DataTable DSLTeamAll()
	{		
		DataTable tblReturn = new DataTable();
		tblReturn.Columns.Add("TeamID");
		tblReturn.Columns.Add("TeamName");

		DataRow drwN = tblReturn.NewRow();
		drwN["TeamID"] = "ALL";
		drwN["TeamName"] = "All";
		tblReturn.Rows.Add(drwN);

		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT TeamID, Name FROM Portal.Team WHERE ActivityID=@ActivityID AND IsActive='true' ORDER BY TeamID";
			cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				drwN = tblReturn.NewRow();
				drwN["TeamID"] = dr["TeamID"].ToString();
				drwN["TeamName"] = dr["Name"].ToString();
				tblReturn.Rows.Add(drwN);
			}
			dr.Close();
		}
		return tblReturn;
	}

    public static DataTable DSLTeamNA()
    {
        DataTable tblReturn = new DataTable();
        tblReturn.Columns.Add("TeamID");
        tblReturn.Columns.Add("TeamName");

        DataRow drwN = tblReturn.NewRow();
        drwN["TeamID"] = "0";
        drwN["TeamName"] = "Not Applicable";
        tblReturn.Rows.Add(drwN);

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TeamID, Name FROM Portal.Team WHERE ActivityID = @ActivityID ORDER BY ColorID";
			cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
			cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                drwN = tblReturn.NewRow();
                drwN["TeamID"] = dr["TeamID"].ToString();
                drwN["TeamName"] = dr["Name"].ToString();
                tblReturn.Rows.Add(drwN);
            }
            dr.Close();
        }
        return tblReturn;
    }

    public static DataTable DSLTeamNA(int pEventID)
    {
        DataTable tblReturn = new DataTable();
        tblReturn.Columns.Add("TeamID");
        tblReturn.Columns.Add("TeamName");

        DataRow drwN = tblReturn.NewRow();
        drwN["TeamID"] = "0";
        drwN["TeamName"] = "Not Applicable";
        tblReturn.Rows.Add(drwN);

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT TeamID,Name FROM Portal.Team AS pt WHERE ActivityID=@ActivityID AND IsActive='1' AND (SELECT EventID FROM Portal.EventTeamScore WHERE TeamID = pt.TeamID) = @EventID";
            cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
            cmd.Parameters.Add(new SqlParameter("@EventID", pEventID));
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                drwN = tblReturn.NewRow();
                drwN["TeamID"] = dr["TeamID"].ToString();
                drwN["TeamName"] = dr["Name"].ToString();
                tblReturn.Rows.Add(drwN);
            }
            dr.Close();
        }
        return tblReturn;
    }

	public static string GetEventDivisionName(int eventDivisionID)
	{
		string eventDivisionName = "";

		using (PortalDataContext pdc = new PortalDataContext())
		{
			eventDivisionName = (from ed in pdc.EventDivisions
								 where ed.EventDivisionID == eventDivisionID
								 select ed.Name).SingleOrDefault();
		}

		return eventDivisionName;
	}

	public static string GetEventName(int eventID)
	{
		string eventName = "";

		using (PortalDataContext pdc = new PortalDataContext())
		{
			eventName = (from ev in pdc.Events
						 where ev.EventID == eventID
						 select ev.Name).SingleOrDefault();
		}

		return eventName;
	}

    public static string GetJoinedEvents(string username, int activityID)
    {
        string joinedEvents = "";


        using (PortalDataContext pdc = new PortalDataContext())
        {
            var qEventList = (from ev in pdc.Events
                              where ev.ActivityID == activityID
                              && (from eg in pdc.EventGames
                                  where (from egtp in pdc.EventGameTeamPlayers
                                         where egtp.Username == username
                                         select egtp.GameID).Contains(eg.GameID)
                                  select eg.EventID).Distinct().Contains(ev.EventID)
                              orderby ev.Name
                              select ev.Name).Distinct().ToList();

            foreach (string s in qEventList)
            {
                joinedEvents += (joinedEvents.Length > 0 ? ", " : "") + s;
            }
        }

        return joinedEvents;
    }

	public static string GetAchievements(string username)
	{
		string achivementList = "";


		using (PortalDataContext pdc = new PortalDataContext())
		{
			List<string> qEventList = (from ea in pdc.Achievements
									   where ea.Username == username
									   join a in pdc.Activities on ea.ActivityID equals a.ActivityID
									   orderby ea.ActivityID descending, ea.Award
									   select ea.Award + " (" + a.Name + ")").ToList();

			foreach (string s in qEventList)
			{
				achivementList += (achivementList.Length > 0 ? "<br>" : "") + s;
			}
		}

		return achivementList;
	}

    public static string GetGamePhaseName(string gamePhase)
    {
        switch (gamePhase)
        {
            case "1":
                return "Elimination";
            case "2":
                return "Semi-Finals";
            case "3":
                return "Battle For Third";
            case "4":
                return "Championship";
            default:
                return "Invalid";
        }
    }

	public static string GetTeamLogo(int teamID)
	{
		string teamLogo = "";

		using (PortalDataContext pdc = new PortalDataContext())
		{
			teamLogo = (from t in pdc.Teams
						where t.TeamID == teamID
						select t.TeamLogo).SingleOrDefault();
		}

		return teamLogo;
	}


    public static string GetTeamName(int teamID)
    {
        string teamname = "";

        using (PortalDataContext pdc = new PortalDataContext())
        {
            teamname = (from t in pdc.Teams
                        where t.TeamID == teamID
                        select t.Name).SingleOrDefault();
        }

        return teamname;
    }

	public static int CountTotalGames(int teamID, int eventID)
	{
		int totalGames = 0;

		using (PortalDataContext pdc = new PortalDataContext())
		{
			totalGames = (from egt in pdc.EventGameTeams
						  where egt.TeamID == teamID
						  && (from eg in pdc.EventGames
							  where eg.EventID == eventID
							  select eg.GameID).Contains(egt.GameID)
						  select egt).Count();
		}

		return totalGames;
	}

	public static int CountTotalWon(int teamID, int eventID)
	{
		int totalWon = 0;

		using (PortalDataContext pdc = new PortalDataContext())
		{
			totalWon = (from eg in pdc.EventGames
						where eg.WinnerTeamID == teamID && eg.IsFinished == true && eg.EventID == eventID
						&& (from egt in pdc.EventGameTeams
							where egt.TeamID == teamID
							select egt.GameID).Contains(eg.GameID)
						select eg).Count();
		}

		return totalWon;
	}

	public static int CountTotalLost(int teamID, int eventID)
	{
		int totalLost = 0;

		using (PortalDataContext pdc = new PortalDataContext())
		{
			totalLost = (from eg in pdc.EventGames
						where eg.WinnerTeamID != teamID && eg.IsFinished == true && eg.EventID == eventID
						&& (from egt in pdc.EventGameTeams
							where egt.TeamID == teamID
							select egt.GameID).Contains(eg.GameID)
						select eg).Count();
		}

		return totalLost;
	}

	public static int CountTotalDraw(int teamID, int eventID)
	{
		int totalDraw = 0;

		using (PortalDataContext pdc = new PortalDataContext())
		{
			totalDraw = (from eg in pdc.EventGames
						 where eg.WinnerTeamID == 0 && eg.IsFinished == true && eg.EventID == eventID
						 && (from egt in pdc.EventGameTeams
							 where egt.TeamID == teamID
							 select egt.GameID).Contains(eg.GameID)
						 select eg).Count();
		}

		return totalDraw;
	}

	public static DateTime GetLatestGameDate()
	{
        //Commented by Charlie Bachiller
        //DateTime latestDate;
        ////using (PortalDataContext pdc = new PortalDataContext())
        ////{
        ////    latestDate = (from eg in pdc.EventGames
        ////                  //where eg.StartDate >= DateTime.Now.Date
        ////                  orderby eg.StartDate
        ////                  select eg.StartDate).LastOrDefault();
        ////}
        //return latestDate;

        //Added By Charlie Bachiller 12-19-2011
        //DateTime dtReturn = DateTime.Now;
        //using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        //{
        //    using (SqlCommand cmd = cn.CreateCommand())
        //    {
        //        cmd.CommandText = "SELECT TOP(1) StartDate FROM Portal.EventGame ORDER BY StartDate";
        //        cn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            dtReturn = DateTime.Parse(dr["StartDate"].ToString());
        //        }
        //    }
        //}
        
        //Editted by CHarlie bachiller may 21, 2012
        DateTime dtReturn = DateTime.Now;
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP(1) StartDate FROM Portal.EventGame WHERE IsFinished='0'  AND Convert(date, StartDate) >= Convert(date, GETDATE())  ORDER BY StartDate";
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dtReturn = DateTime.Parse(dr["StartDate"].ToString());
                    }
                }
            }
        }
        catch 
        {
            dtReturn = DateTime.Now;
        }
        
        return dtReturn;
	}

	public static int GetScore(int teamID, int eventID)
	{
		int score = 0;

		using (PortalDataContext pdc = new PortalDataContext())
		{
			score = (from egt in pdc.EventGameTeams
					 where egt.TeamID == teamID
					 && (from eg in pdc.EventGames
						 where eg.EventID == eventID
						 select eg.GameID).Contains(egt.GameID)
					 select egt.Score).Sum().ToString().ToInt();
		}

		return score;
	}

	public static string GetStanding(int teamID, int eventID)
	{
		string standing = "";
		int scoringType = 0;

		using (PortalDataContext pdc = new PortalDataContext())
		{
			scoringType = (from ev in pdc.Events
						   where ev.EventID == eventID
						   select ev.ScoringTypeID).SingleOrDefault();
		}
		if (scoringType == 1)
		{
			standing = "(" + DALPortal.CountTotalWon(teamID, eventID).ToString() + "-" + DALPortal.CountTotalDraw(teamID, eventID).ToString() + "-" + DALPortal.CountTotalLost(teamID, eventID).ToString() + ")";
		}
		else if (scoringType == 2)
		{
			standing = "(" + DALPortal.GetScore(teamID, eventID).ToString() + ")";
		}
		return standing;
	}

	public static void LogError(string username, string className, string method, string details)
	{
		using (PortalDataContext pdc = new PortalDataContext())
		{
			try
			{
				pdc.InsertErrorLog(username, className, method, details);
			}
			catch
			{
				// *gulp*
			}

		}
	}

	public static int UpdateEvent(int eventID, string eventName, int eventDivisionID, int eventCategoryID, int maxPoint, int winnerTeamID, int sortOrder,bool isActive, string modifiedBy)
	{
		int intReturn = 0;
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "UPDATE Portal.Events SET Name=@Name, EventDivisionID=@EventDivisionID, EventCategoryID=@EventCategoryID, MaxPoint=@MaxPoint, WinnerTeamID=@WinnerTeamID, SortOrder=@SortOrder, IsActive=@IsActive, ModifiedBy=@ModifiedBy, DateModified=@DateModified WHERE EventID=@EventID";
			cmd.Parameters.Add(new SqlParameter("@EventID", eventID));
			cmd.Parameters.Add(new SqlParameter("@Name", eventName));
			cmd.Parameters.Add(new SqlParameter("@EventDivisionID", eventDivisionID));
			cmd.Parameters.Add(new SqlParameter("@EventCategoryID", eventCategoryID));
			cmd.Parameters.Add(new SqlParameter("@MaxPoint", maxPoint));
			cmd.Parameters.Add(new SqlParameter("@WinnerTeamID", winnerTeamID));
			cmd.Parameters.Add(new SqlParameter("@SortOrder", sortOrder));
			cmd.Parameters.Add(new SqlParameter("@IsActive", isActive));
			cmd.Parameters.Add(new SqlParameter("@ModifiedBy", modifiedBy));
			cmd.Parameters.Add(new SqlParameter("@DateModified", DateTime.Now));
			cn.Open();
			intReturn = cmd.ExecuteNonQuery();
		}
		return intReturn;
	}

    public static int UpdateScoreRank(int eventID, int teamID, int rank, int score)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE Portal.EventTeamScore SET Rank = @Rank, Score = @Score WHERE EventID = @EventID AND TeamID = @TeamID";
            cmd.Parameters.Add(new SqlParameter("@Rank", rank));
            cmd.Parameters.Add(new SqlParameter("@Score", score));
            cmd.Parameters.Add(new SqlParameter("@EventID", eventID));
            cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }

    public static int UpdateEventGames(int gameID, string gamePhase, DateTime startDate, DateTime endDate, string location, int winnerTeamID, bool isFinished, string modifiedBy)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE Portal.EventGame SET GamePhase=@GamePhase, StartDate=@StartDate, EndDate=@EndDate, Location=@Location, WinnerTeamID=@WinnerTeamID, IsFinished=@IsFinished, ModifiedBy=@ModifiedBy, DateModified=@DateModified WHERE GameID=@GameID";
            cmd.Parameters.Add(new SqlParameter("@GameID", gameID));
            cmd.Parameters.Add(new SqlParameter("@GamePhase", gamePhase));
            cmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
            cmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
            cmd.Parameters.Add(new SqlParameter("@Location", location));
            cmd.Parameters.Add(new SqlParameter("@WinnerTeamID", winnerTeamID));
            cmd.Parameters.Add(new SqlParameter("@IsFinished", isFinished));
            cmd.Parameters.Add(new SqlParameter("@ModifiedBy", modifiedBy));
            cmd.Parameters.Add(new SqlParameter("@DateModified", DateTime.Now));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }

    public static int UpdateEventGameTeam(int gameID, int teamID, int rank, int score)
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE Portal.EventGameTeam SET Rank=@Rank, Score=@Score WHERE GameID=@GameID AND TeamID=@TeamID";
            cmd.Parameters.Add(new SqlParameter("@GameID", gameID));
            cmd.Parameters.Add(new SqlParameter("@TeamID", teamID));
            cmd.Parameters.Add(new SqlParameter("@Rank", rank));
            cmd.Parameters.Add(new SqlParameter("@Score", score));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
        }
        return intReturn;
    }




}