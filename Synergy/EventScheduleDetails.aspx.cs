using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Synergy_EventScheduleDetails : System.Web.UI.Page
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    private bool IsCorrectEntries()
    {
        string strErrorMessage = "";

        if (clsValidator.CheckDate(clsValidator.CheckInteger(txtStartYear.Text), clsValidator.CheckInteger(ddlStartMonth.SelectedValue), clsValidator.CheckInteger(ddlStartDay.SelectedValue), clsValidator.CheckInteger(ddlStartHour.SelectedValue), clsValidator.CheckInteger(ddlStartMinute.SelectedValue), ddlStartTimePeriod.SelectedValue) == clsDateTime.SystemMinDate)
            strErrorMessage += "<br>Invalid date/time start entry.";

        if (clsValidator.CheckDate(clsValidator.CheckInteger(txtEndYear.Text), clsValidator.CheckInteger(ddlEndMonth.SelectedValue), clsValidator.CheckInteger(ddlEndDay.SelectedValue), clsValidator.CheckInteger(ddlEndHour.SelectedValue), clsValidator.CheckInteger(ddlEndMinute.SelectedValue), ddlEndTimePeriod.SelectedValue) == clsDateTime.SystemMinDate)
            strErrorMessage += "<br>Invalid date/time end entry.";

        return strErrorMessage == "";
    }

    private void LoadOfficials()
    {
        bool blnHasAccess = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);

        List<EventGameOfficial> officialList = new List<EventGameOfficial>();

        using (PortalDataContext pdc = new PortalDataContext())
        {
            officialList = (from ego in pdc.EventGameOfficials
                            where ego.GameID == hdnGameID.Value.ToInt()
                            orderby ego.OfficialID
                            select ego).ToList();
        }

        dgOfficials.DataSource = officialList;
        dgOfficials.DataBind();
        dgOfficials.Columns[1].Visible = blnHasAccess;

        if (dgOfficials.Items.Count > 0)
        {
            divOfficials.Visible = true;
            lblNoOfficial.Visible = false;
        }
        else
        {
            divOfficials.Visible = false;
            lblNoOfficial.Visible = true;
        }

        if (blnHasAccess)
        {
            trOfficialsAdd.Visible = true;

			List<Committee> committeeList = new List<Committee>();
			using (PortalDataContext pdc = new PortalDataContext())
			{
				committeeList = (from c in pdc.Committees
								 where c.ActivityID == SynergyCurrentID
								 && !(from ego in pdc.EventGameOfficials
									  where ego.GameID == hdnGameID.Value.ToInt()
									  select ego.OfficialID).Contains(c.Username)
								 orderby c.Username
								 select c).ToList();
			}
            ddlCommittee.DataSource = committeeList;
            ddlCommittee.DataValueField = "Username";
            ddlCommittee.DataTextField = "Username";
            ddlCommittee.DataBind();
        }
        else
        {
            trOfficialsAdd.Visible = false;
        }

    }

    private void LoadCompetingTeams()
    {
        bool blnHasAccess = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);

        using (PortalDataContext pdc = new PortalDataContext())
        {
            var q = (from egt in pdc.EventGameTeams
                     where egt.GameID == hdnGameID.Value.ToInt()
                     let xTeamName = (from t in pdc.Teams
                                      where t.TeamID == egt.TeamID
                                      select t.Name).SingleOrDefault()
                     orderby egt.Rank, egt.TeamID
                     select new
                     {
                         TeamID = egt.TeamID,
                         TeamName = xTeamName,
                         Rank = egt.Rank,
                         Score = egt.Score
                     }).ToList();

            dgCompetingTeams.DataSource = q;
            dgCompetingTeams.DataBind();
            dgCompetingTeams.Columns[4].Visible = blnHasAccess;

            foreach (DataGridItem ditm in dgCompetingTeams.Items)
            {
                HiddenField phdnTeamID = (HiddenField)ditm.FindControl("hdnTeamID");
                DropDownList pddlPlayers = (DropDownList)ditm.FindControl("ddlPlayers");
                ImageButton pbtnAddPlayer = (ImageButton)ditm.FindControl("btnAddPlayer");
                ImageButton pbtnDelete = (ImageButton)ditm.FindControl("btnDelete");
                TextBox ptxtRank = (TextBox)ditm.FindControl("txtRank");
                TextBox ptxtScore = (TextBox)ditm.FindControl("txtScore");
                DataGrid pdgPlayers = (DataGrid)ditm.FindControl("dgPlayers");

                List<EventGameTeamPlayer> egtpList = new List<EventGameTeamPlayer>();
                egtpList = (from egtp in pdc.EventGameTeamPlayers
                            where egtp.GameID == hdnGameID.Value.ToInt() && egtp.TeamID == phdnTeamID.Value.ToInt()
                            orderby egtp.Username
                            select egtp).ToList();

                pdgPlayers.DataSource = egtpList;
                pdgPlayers.DataBind();
                pdgPlayers.Columns[1].Visible = blnHasAccess;

                if (blnHasAccess)
                {
                    var qPlayers = (from tm in pdc.TeamMembers
                                    where tm.TeamID == phdnTeamID.Value.ToInt()
                                    && !(from egtp in pdc.EventGameTeamPlayers
                                         where egtp.GameID == hdnGameID.Value.ToInt() && egtp.TeamID == phdnTeamID.Value.ToInt()
                                         select egtp.Username).Contains(tm.Username)
                                    orderby tm.Username
                                    select new { Username = tm.Username }).ToList();

                    pddlPlayers.DataSource = qPlayers;
                    pddlPlayers.DataValueField = "Username";
                    pddlPlayers.DataTextField = "Username";
                    pddlPlayers.DataBind();
                }
                else
                {
                    pddlPlayers.Visible = false;
                    pbtnAddPlayer.Visible = false;
                    pbtnDelete.Visible = false;
                    ptxtRank.ReadOnly = true;
                    ptxtRank.BackColor = System.Drawing.Color.AliceBlue;
                    ptxtScore.ReadOnly = true;
                    ptxtScore.BackColor = System.Drawing.Color.AliceBlue;
                }
            }

            if (blnHasAccess)
            {

                trSaveEventTeamScore.Visible = true;

                //Retrieve teams Added by charlie bachiller  May 21, 2012
                DataTable tblTeams = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TeamID,Name AS TeamName FROM Portal.Team AS pt WHERE ActivityID=@ActivityID AND IsActive='1' AND (SELECT EventID FROM Portal.EventTeamScore WHERE TeamID = pt.TeamID) = @EventID AND TeamID NOT IN (SELECT TeamID FROM Portal.EventGameTeam WHERE GameID=@GameID)";
                        cn.Open();
                        cmd.Parameters.Add(new SqlParameter("@ActivityID", SynergyCurrentID));
                        cmd.Parameters.Add(new SqlParameter("@EventID", hdnEventID.Value));
                        cmd.Parameters.Add(new SqlParameter("@GameID", hdnGameID.Value));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblTeams);
                    }
                }

				var qTeams = (from t in pdc.Teams
							  where t.ActivityID == SynergyCurrentID
							  && !(from egt in pdc.EventGameTeams
								   where egt.GameID == hdnGameID.Value.ToInt()
								   select egt.TeamID).Contains(t.TeamID)
							  orderby t.ColorID
							  select new { TeamID = t.TeamID, TeamName = t.Name }).ToList();
                //ddlTeams.DataSource = qTeams;
                ddlTeams.DataSource = tblTeams;
                ddlTeams.DataValueField = "TeamID";
                ddlTeams.DataTextField = "TeamName";
                ddlTeams.DataBind();

                btnAddTeam.Visible = ddlTeams.Items.Count > 0;
                ddlTeams.Visible = ddlTeams.Items.Count > 0;
            }
            else
            {
                trSaveEventTeamScore.Visible = false;
            }
        }
    }


    ///////////////////////////////
    ///////// Form Events /////////
    ///////////////////////////////

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool blnHasAccess = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);

            hdnGameID.Value = Request.QueryString["gameid"];

            ddlStartMonth.DataSource = clsDateTime.GetMonths();
            ddlStartMonth.DataValueField = "pvalue";
            ddlStartMonth.DataTextField = "ptext";
            ddlStartMonth.DataBind();
            ddlStartDay.DataSource = clsDateTime.GetDays();
            ddlStartDay.DataValueField = "pvalue";
            ddlStartDay.DataTextField = "ptext";
            ddlStartDay.DataBind();
            ddlStartHour.DataSource = clsDateTime.GetHours();
            ddlStartHour.DataValueField = "pvalue";
            ddlStartHour.DataTextField = "ptext";
            ddlStartHour.DataBind();
            ddlStartMinute.DataSource = clsDateTime.GetMinutes();
            ddlStartMinute.DataValueField = "pvalue";
            ddlStartMinute.DataTextField = "ptext";
            ddlStartMinute.DataBind();
            ddlStartTimePeriod.DataSource = clsDateTime.GetTimePeriod();
            ddlStartTimePeriod.DataValueField = "pvalue";
            ddlStartTimePeriod.DataTextField = "ptext";
            ddlStartTimePeriod.DataBind();

            ddlEndMonth.DataSource = clsDateTime.GetMonths();
            ddlEndMonth.DataValueField = "pvalue";
            ddlEndMonth.DataTextField = "ptext";
            ddlEndMonth.DataBind();
            ddlEndDay.DataSource = clsDateTime.GetDays();
            ddlEndDay.DataValueField = "pvalue";
            ddlEndDay.DataTextField = "ptext";
            ddlEndDay.DataBind();
            ddlEndHour.DataSource = clsDateTime.GetHours();
            ddlEndHour.DataValueField = "pvalue";
            ddlEndHour.DataTextField = "ptext";
            ddlEndHour.DataBind();
            ddlEndMinute.DataSource = clsDateTime.GetMinutes();
            ddlEndMinute.DataValueField = "pvalue";
            ddlEndMinute.DataTextField = "ptext";
            ddlEndMinute.DataBind();
            ddlEndTimePeriod.DataSource = clsDateTime.GetTimePeriod();
            ddlEndTimePeriod.DataValueField = "pvalue";
            ddlEndTimePeriod.DataTextField = "ptext";
            ddlEndTimePeriod.DataBind();

            ddlWinner.DataSource = DALPortal.DSLTeamNA();
            ddlWinner.DataValueField = "TeamID";
            ddlWinner.DataTextField = "TeamName";
            ddlWinner.DataBind();

            ddlGamePhase.DataSource = DALPortal.DSLGamePhase();
            ddlGamePhase.DataValueField = "pvalue";
            ddlGamePhase.DataTextField = "ptext";
            ddlGamePhase.DataBind();

            using (PortalDataContext pdc = new PortalDataContext())
            {
                EventGame eventGame = (from eg in pdc.EventGames
                                       where eg.GameID == hdnGameID.Value.ToInt()
                                       select eg).SingleOrDefault();

                hdnEventID.Value = eventGame.EventID.ToString();
                txtEvent.Text = DALPortal.GetEventName(eventGame.EventID);
                ddlGamePhase.SelectedValue = eventGame.GamePhase.ToString();
                txtStartYear.Text = eventGame.StartDate.Year.ToString();
                ddlStartMonth.SelectedValue = eventGame.StartDate.Month.ToString();
                ddlStartDay.SelectedValue = eventGame.StartDate.Day.ToString();
                ddlStartHour.SelectedValue = eventGame.StartDate.ToString("hh");
                ddlStartMinute.SelectedValue = eventGame.StartDate.Minute.ToString();
                ddlStartTimePeriod.SelectedValue = eventGame.StartDate.ToString("tt");
                txtEndYear.Text = eventGame.EndDate.Year.ToString();
                ddlEndMonth.SelectedValue = eventGame.EndDate.Month.ToString();
                ddlEndDay.SelectedValue = eventGame.EndDate.Day.ToString();
                ddlEndHour.SelectedValue = eventGame.EndDate.ToString("hh");
                ddlEndMinute.SelectedValue = eventGame.EndDate.Minute.ToString();
                ddlEndTimePeriod.SelectedValue = eventGame.EndDate.ToString("tt");
                txtLocation.Text = eventGame.Location;
                ddlWinner.SelectedValue = eventGame.WinnerTeamID.ToString();
                chkFinished.Checked = eventGame.IsFinished;
            }


 
            if (blnHasAccess)
            {
                trSaveSchedule.Visible = true;
            }
            else
            {
                trSaveSchedule.Visible = false;
                ddlGamePhase.Enabled = false;
                ddlStartMonth.Enabled = false;
                ddlStartDay.Enabled = false;
                txtStartYear.Enabled = false;
                ddlStartHour.Enabled = false;
                ddlStartMinute.Enabled = false;
                ddlStartTimePeriod.Enabled = false;

                ddlEndMonth.Enabled = false;
                ddlEndDay.Enabled = false;
                txtEndYear.Enabled = false;
                ddlEndHour.Enabled = false;
                ddlEndMinute.Enabled = false;
                ddlEndTimePeriod.Enabled = false;
                txtLocation.ReadOnly = true;
                txtLocation.BackColor = System.Drawing.Color.AliceBlue;
                ddlWinner.Enabled = false;
                chkFinished.Enabled = false;
            }

            this.LoadOfficials();
            this.LoadCompetingTeams();
        }
    }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		if (IsCorrectEntries())
		{
			int intRecordsAffected = 0;

			int gameID = hdnGameID.Value.ToInt();
			string gamePhase = ddlGamePhase.SelectedValue;
			DateTime startDate = clsValidator.CheckDate(txtStartYear.Text.ToInt(), ddlStartMonth.SelectedValue.ToInt(), ddlStartDay.SelectedValue.ToInt(), ddlStartHour.SelectedValue.ToInt(), ddlStartMinute.SelectedValue.ToInt(), ddlStartTimePeriod.SelectedValue);
			DateTime endDate = clsValidator.CheckDate(txtEndYear.Text.ToInt(), ddlEndMonth.SelectedValue.ToInt(), ddlEndDay.SelectedValue.ToInt(), ddlEndHour.SelectedValue.ToInt(), ddlEndMinute.SelectedValue.ToInt(), ddlEndTimePeriod.SelectedValue);
			string location = txtLocation.Text;
			int winnerTeamID = ddlWinner.SelectedValue.ToInt();
			bool isFinished = chkFinished.Checked;
			string modifiedBy = Request.Cookies["Speedo"]["UserName"];
			intRecordsAffected = DALPortal.UpdateEventGames(gameID, gamePhase, startDate, endDate, location, winnerTeamID, isFinished, modifiedBy);

			if (intRecordsAffected > 0)
			{
				Response.Redirect("EventDetails.aspx?eventid=" + Request.QueryString["eventid"]);
			}
		}
	}

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("EventDetails.aspx?eventid=" + Request.QueryString["eventid"]);
    }

    protected void btnAddCommittee_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlCommittee.Items.Count > 0)
        {
            using (PortalDataContext pdc = new PortalDataContext())
            {
                EventGameOfficial ego = new EventGameOfficial()
                {
                    GameID = hdnGameID.Value.ToInt(),
                    OfficialID = ddlCommittee.SelectedValue
                };

                pdc.EventGameOfficials.InsertOnSubmit(ego);

                pdc.SubmitChanges();
            }
            this.LoadOfficials();
        }
    }

    protected void dgOfficials_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Label plblUsername = (Label)e.Item.FindControl("lblUsername");
        DALPortal.DeleteEventGameOfficial(hdnGameID.Value.ToInt(), plblUsername.Text);
        this.LoadOfficials();
    }

    protected void btnAddTeam_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlTeams.Items.Count > 0)
        {
            using (PortalDataContext pdc = new PortalDataContext())
            {
                EventGameTeam egt = new EventGameTeam()
                {
                    GameID = hdnGameID.Value.ToInt(),
                    TeamID = ddlTeams.SelectedValue.ToInt(),
                    Rank = 0,
                    Score = 0
                };

                pdc.EventGameTeams.InsertOnSubmit(egt);

                pdc.SubmitChanges();
            }
            this.LoadCompetingTeams();
        }
    }

    protected void dgCompetingTeams_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        HiddenField phdnTeamID = (HiddenField)e.Item.FindControl("hdnTeamID");
        DALPortal.DeleteEventGameTeam(hdnGameID.Value.ToInt(), phdnTeamID.Value.ToInt());
        this.LoadCompetingTeams();
    }

    protected void btnSaveEventTeamScore_Click(object sender, ImageClickEventArgs e)
    {
        foreach (DataGridItem ditm in dgCompetingTeams.Items)
        {
            HiddenField phdnTeamID = (HiddenField)ditm.FindControl("hdnTeamID");
            TextBox ptxtRank = (TextBox)ditm.FindControl("txtRank");
            TextBox ptxtScore = (TextBox)ditm.FindControl("txtScore");
            DALPortal.UpdateEventGameTeam(hdnGameID.Value.ToInt(), phdnTeamID.Value.ToInt(), ptxtRank.Text.ToInt(), ptxtScore.Text.ToInt());
        }
        this.LoadCompetingTeams();
    }

    protected void dgCompetingTeams_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddPlayer")
        {
            DropDownList pddlPlayers = (DropDownList)e.Item.FindControl("ddlPlayers");
            HiddenField phdnTeamID = (HiddenField)e.Item.FindControl("hdnTeamID");
            if (pddlPlayers.Items.Count > 0)
            {
                using (PortalDataContext pdc = new PortalDataContext())
                {
                    EventGameTeamPlayer egtp = new EventGameTeamPlayer()
                    {
                        GameID = hdnGameID.Value.ToInt(),
                        TeamID = phdnTeamID.Value.ToInt(),
                        Username = pddlPlayers.SelectedValue
                    };

                    pdc.EventGameTeamPlayers.InsertOnSubmit(egtp);

                    pdc.SubmitChanges();
                }

                this.LoadCompetingTeams();
            }
        }
    }

    protected void dgPlayers_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Label plblUsername = (Label)e.Item.FindControl("lblUsername");
        HiddenField phdnTeamIDP = (HiddenField)e.Item.FindControl("hdnTeamIDP");
        DALPortal.DeleteEventGameTeamPlayer(hdnGameID.Value.ToInt(), phdnTeamIDP.Value.ToInt(), plblUsername.Text);
        this.LoadCompetingTeams();
    }
}