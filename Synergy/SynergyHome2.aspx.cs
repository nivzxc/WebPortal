using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using HqWeb.Forums;
using HqWeb;
using STIeForms;

public partial class Synergy_SynergyHome2 : System.Web.UI.Page
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        if (!Page.IsPostBack)
        {
            divAdmin.Visible = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);
            LoadActiveTeams();
            LoadActiveTeams();
            LoadActiveTeamScores();
            LoadLatestSchedule();
            LoadLatesActiveEvent();

         
        }


    }

    private string GetActiveEventName()
    {
        string strReturn = "";
        try
        {
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT (SELECT Name FROM Portal.Events WHERE EventID=PEA.EventID) AS EventName FROM Portal.EventsActive as PEA WHERE IsActive='1'";
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                    litActiveEventTeamRoster.Text = strReturn != string.Empty ? strReturn.ToUpper() + " OFFICIAL TEAM ROSTER" : "OFFICIAL TEAM ROSTER";
                }
            }

        }
        catch
        {
            litActiveEventTeamRoster.Text = "OFFICIAL TEAM ROSTER";
        }
        return strReturn;
    }

    public string GetTeamLeader(int pTeamID)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT Captain FROM Portal.Team WHERE TeamID=@TeamID";
                cmd.Parameters.Add(new SqlParameter("@TeamID", pTeamID));
                cn.Open();
                strReturn = "<font style='font-size: x-small'><b>Captain:</b>&nbsp;" + clsEmployee.GetName(cmd.ExecuteScalar().ToString()) + "</font>";
            }
        }

        return strReturn;
    }

    public string GetTeamMember(int pTeamID)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT Username FROM Portal.TeamMember WHERE TeamID=@TeamID";
                cmd.Parameters.Add(new SqlParameter("@TeamID", pTeamID));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                strReturn = "<b>Members:</b>&nbsp;";
                while (dr.Read())
                {
                    strReturn = strReturn + clsEmployee.GetName(dr["Username"].ToString()) + ", ";
                }

                // strReturn.Remove(strReturn.Length - 2, 1);
                strReturn = strReturn.Remove(strReturn.Length - 2, 2);
            }
        }
        return strReturn;
    }

    protected void LoadActiveTeams()
    {
        string strWrite = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT TeamID,(SELECT Name FROM Portal.Team WHERE ActivityID=@ActivityID AND TeamID=pet.TeamID) AS Name, Rank, Score FROM Portal.EventTeamScore AS pet WHERE EventID=@EventID ORDER BY TeamID";
                cmd.Parameters.Add(new SqlParameter("@EventID", GetActiveEvent()));
                cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strWrite = strWrite + "<table width='100%'>" +

                            "<tr>" +
                               " <td style='font-size:medium; font-family:Arial'><b>" + dr["Name"].ToString() + "<br />" + GetTeamLeader(dr["TeamID"].ToString().ToInt()) + "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='font-size:x-small'>" + GetTeamMember(dr["TeamID"].ToString().ToInt()) + "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='font-size:x-small'><a href='TeamDetails.aspx?teamid=" + dr["TeamID"].ToString() + "'><b>View Team Details</b></a></td>" +
                            "</tr>" +
                            "<tr>" +
                               "<td style='height: 5px;'>&nbsp;</td>" +
                            "</tr>" +
                        "</table>";
                }
            }
        }
        litActiveTeamRoster.Text = strWrite;
    }

    private string GetActiveEvent()
    {
        string strReturn = "";
        try
        {

            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT EventID FROM Portal.EventsActive WHERE IsActive='1'";
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
        }
        catch { }
        return strReturn;
    }

    private string LoadLatestScheduleTeams(int gameID, int eventID)
    {
        string strReturn = "";

        List<EventGameTeam> eventGameTeamList = new List<EventGameTeam>();
        using (PortalDataContext pdc = new PortalDataContext())
        {
            eventGameTeamList = (from egt in pdc.EventGameTeams
                                 where egt.GameID == gameID
                                 orderby egt.Rank.Value
                                 select egt).ToList();
        }

        foreach (EventGameTeam egt in eventGameTeamList)
        {
            strReturn += (strReturn == "" ? "" : "&nbsp;vs.&nbsp;") + DALPortal.GetTeamName(egt.TeamID);
        }
        return strReturn;
    }

    private string LoadLatestScheduleTeams1(int gameID, int eventID)
    {
        string strReturn = "";

        List<EventGameTeam> eventGameTeamList = new List<EventGameTeam>();
        using (PortalDataContext pdc = new PortalDataContext())
        {
            eventGameTeamList = (from egt in pdc.EventGameTeams
                                 where egt.GameID == gameID
                                 orderby egt.Rank.Value
                                 select egt).ToList();
        }

        foreach (EventGameTeam egt in eventGameTeamList)
        {
            strReturn += (strReturn == "" ? "" : "<br/>Vs.<br/>") + DALPortal.GetTeamName(egt.TeamID);
        }
        return strReturn;
    }

    protected void LoadLatestSchedule()
    {
        string strWrite = "";
        int intCountEvents = 0;
        DateTime latestDate = DALPortal.GetLatestGameDate();

        if (latestDate != DateTime.MinValue)
        {
            DateTime latestDateStart = clsDateTime.ChangeTimeToStart(latestDate);
            DateTime latestDateEnd = clsDateTime.ChangeTimeToEnd(latestDate);

            List<EventGame> eventGamesList = new List<EventGame>();
            using (PortalDataContext pdc = new PortalDataContext())
            {
                eventGamesList = (from eg in pdc.EventGames
                                  where eg.IsActive == true && eg.StartDate >= latestDateStart && eg.IsFinished == false
                                  orderby eg.StartDate
                                  select eg).ToList();
            }

            foreach (EventGame eg in eventGamesList)
            {
                if (intCountEvents > 4)
                { break; }
                strWrite += "<tr>" +
                                  "<td colspan='3'>" +
                                  "<b>Event:</b> " + DALPortal.GetEventName(eg.EventID) + "<br />" +
                                  "<b>Teams:</b> " + this.LoadLatestScheduleTeams(eg.GameID, eg.EventID) + " <br />" +
                                  "<b>Date:</b> " + eg.StartDate.ToString("MMM dd, yyyy") + "<br />" +
                                  "<b>Time:</b> " + eg.StartDate.ToString("hh:mm tt") + "<br />" +
                                  "<b>Location:</b> " + eg.Location + "<br />" +
                                  "</td>" +
                                  "</tr>" +
                                  "<tr>" +
                                  "<td style='height: 5px;' colspan='3'>" +
                                  "</td>" +
                            "</tr>";

                intCountEvents++;
            }
        }
        litEcheduledEvents.Text = strWrite;
    }

    private void LoadActiveTeamScores()
    {
        string strWrite = "";
        int intCount = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT TeamID,(SELECT Name FROM Portal.Team WHERE ActivityID=@ActivityID AND TeamID=pet.TeamID) AS Name, Rank, Score FROM Portal.EventTeamScore AS pet WHERE EventID=@EventID ORDER BY TeamID";
                cmd.Parameters.Add(new SqlParameter("@EventID", GetActiveEvent()));
                cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strWrite = strWrite + "<tr> " +
                                              "<td><a href='../Synergy/TeamDetails.aspx?teamid=" + dr["TeamID"].ToString() + "'>" + dr["Name"].ToString() + "</a></td>" +
                                              "<td style='text-align:center'>" + DALPortal.CountTotalWon(dr["TeamID"].ToString().ToInt(), GetActiveEvent().ToInt()).ToString() + "</td>" +
                                              "<td style='text-align:center'>" + DALPortal.CountTotalLost(dr["TeamID"].ToString().ToInt(), GetActiveEvent().ToInt()).ToString() + "</td>" +
                                              "</tr>";
                }
            }
        }
        lblTeamLineUp.Text = GetActiveEventName() + " Team Standing";
        litLineup.Text = strWrite;
    }

    protected void LoadLatesActiveEvent()
    {
        string strWrite = "";
        int intCountEvents = 0;
        DateTime latestDate = DALPortal.GetLatestGameDate();

        if (latestDate != DateTime.MinValue)
        {
            DateTime latestDateStart = clsDateTime.ChangeTimeToStart(latestDate);
            DateTime latestDateEnd = clsDateTime.ChangeTimeToEnd(latestDate);

            List<EventGame> eventGamesList = new List<EventGame>();

            using (PortalDataContext pdc = new PortalDataContext())
            {
                eventGamesList = (from eg in pdc.EventGames
                                  where eg.IsActive == true && eg.StartDate >= latestDateStart && eg.IsFinished == false && eg.EventID == GetActiveEvent().ToInt()
                                  orderby eg.StartDate
                                  select eg).ToList();
            }

            foreach (EventGame eg in eventGamesList)
            {
                strWrite += "<table>" +
                            "<tr>" +
                                "<td>" +
                                    "<img src='../Support/play16red.png' alt='' />" +
                                "</td>" +
                                "<td style='font-size: 18pt; font-family: Arial; font-weight: bold'>" +
                                    eg.StartDate.ToString("MMM dd, yyyy") +
                                "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td>&nbsp;</td>" +
                                "<td>" +
                                   DALPortal.GetGamePhaseName(eg.GamePhase.ToString()) + "<br/>(" + eg.StartDate.ToString("hh:mm tt") + ", " + eg.Location + ")" +
                                   "<br/>" +
                                   this.LoadLatestScheduleTeams1(eg.GameID, eg.EventID) +
                               " </td>" +
                            "<tr>" +
                        "</table>";
                break;
            }
        }

        litLatestEvent.Text = strWrite;
    }
}