using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Synergy_EventDetails : System.Web.UI.Page
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    private void LoadEventScheduleList()
    {
        bool blnHasAccess = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);
        List<EventGame> eventGameList = new List<EventGame>();

        using (PortalDataContext pdc = new PortalDataContext())
        {
            eventGameList = (from eg in pdc.EventGames
                             where eg.IsActive == true && eg.EventID == hdnEventID.Value.ToInt()
                             orderby eg.StartDate
                             select eg).ToList();


            dgSchedule.DataSource = eventGameList;
            dgSchedule.DataBind();
            dgSchedule.Columns[1].Visible = blnHasAccess;

            if (dgSchedule.Items.Count > 0)
            {
                divSchedule.Visible = true;
                lblNoSchedule.Visible = false;
                foreach (DataGridItem ditm in dgSchedule.Items)
                {
                    HiddenField phdnGameID = (HiddenField)ditm.FindControl("hdnGameID");
                    Literal plitTeams = (Literal)ditm.FindControl("litTeams");
                    HiddenField phdnWinner = (HiddenField)ditm.FindControl("hdnWinner");
                    Image pimgWinner = (Image)ditm.FindControl("imgWinner");
                    HiddenField phdnFinished = (HiddenField)ditm.FindControl("hdnFinished");
                    Image pimgFinished = (Image)ditm.FindControl("imgFinished");
                    HiddenField phdnGamePhase = (HiddenField)ditm.FindControl("hdnGamePhase");
                    Label plblGamePhase = (Label)ditm.FindControl("lblGamePhase");
                    List<EventGameTeam> egtList = new List<EventGameTeam>();

                    egtList = (from egt in pdc.EventGameTeams
                               where egt.GameID == phdnGameID.Value.ToInt()
                               orderby egt.TeamID
                               select egt).ToList();
                    foreach (EventGameTeam egt in egtList)
                    {
                        string teamLogo = (from t in pdc.Teams where t.TeamID == egt.TeamID select t.TeamLogo).SingleOrDefault();
                        if (teamLogo.Length > 0)
                        {
                            plitTeams.Text += "<td><img src='" + teamLogo + "'></td>";
                        }
                    }

                    if (plitTeams.Text.Trim().Length > 0)
                    {
                        plitTeams.Text = "<table cellpadding='5'><tr>" + plitTeams.Text + "</tr></table>";
                    }

                    if (phdnWinner.Value == "" || phdnWinner.Value == "0")
                    {
                        pimgWinner.Visible = false;
                    }
                    else
                    {
                        pimgWinner.ImageUrl = DALPortal.GetTeamLogo(phdnWinner.Value.ToInt());
                    }

                    pimgFinished.ImageUrl = "~/Support/" + (phdnFinished.Value == "True" ? "check16" : "history16") + ".png";
                    plblGamePhase.Text = DALPortal.GetGamePhaseName(phdnGamePhase.Value);
                }
            }
            else
            {
                divSchedule.Visible = false;
                lblNoSchedule.Visible = true;
            }
        }
    }

    private void LoadEventTeamScore()
    {
        bool blnHasAccess = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);

        using (PortalDataContext pdc = new PortalDataContext())
        {
            var q = (from ets in pdc.EventTeamScores
                     where ets.EventID == hdnEventID.Value.ToInt()
                     let pTeamName = (from t in pdc.Teams
                                      where t.TeamID == ets.TeamID
                                      select t.Name).SingleOrDefault()
                     orderby ets.Rank
                     select new
                     {
                         TeamID = ets.TeamID,
                         TeamName = pTeamName,
                         Rank = ets.Rank,
                         Score = ets.Score
                     }).ToList();

            dgTeamEventScore.DataSource = q;
            dgTeamEventScore.DataBind();
        }

        if (!blnHasAccess)
        {
            foreach (DataGridItem ditm in dgTeamEventScore.Items)
            {
                TextBox ptxtRank = (TextBox)ditm.FindControl("txtRank");
                TextBox ptxtScore = (TextBox)ditm.FindControl("txtScore");
                ptxtRank.ReadOnly = true;
                ptxtRank.BackColor = System.Drawing.Color.AliceBlue;
                ptxtScore.ReadOnly = true;
                ptxtScore.BackColor = System.Drawing.Color.AliceBlue;
            }
        }
    }

	private void CheckInsertEventTeamScore()
	{
		using (PortalDataContext pdc = new PortalDataContext())
		{
			List<int> teamIDList = new List<int>();
			teamIDList = (from t in pdc.Teams
						  where t.ActivityID == SynergyCurrentID
						  && !(from ets in pdc.EventTeamScores
							   where ets.EventID == hdnEventID.Value.ToInt()
							   select ets.TeamID).Contains(t.TeamID)
						  select t.TeamID).ToList();

			foreach (int teamID in teamIDList)
			{
				EventTeamScore ets = new EventTeamScore()
				{
					EventID = hdnEventID.Value.ToInt(),
					TeamID = teamID,
					Rank = 0,
					Score = 0
				};
				pdc.EventTeamScores.InsertOnSubmit(ets);
			}

			pdc.SubmitChanges();
		}
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            bool blnHasAccess = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);
            btnSave.Visible = blnHasAccess;
            btnNewSchedule.Visible = blnHasAccess;
            btnSaveEventTeamScore.Visible = blnHasAccess;

            hdnEventID.Value = Request.QueryString["eventid"];

            Event eventSynergy = new Event();
            using (PortalDataContext pdc = new PortalDataContext())
            {
                var qDivision = (from ed in pdc.EventDivisions
                                 orderby ed.Name
                                 select new { DivisionID = ed.EventDivisionID, Name = ed.Name }).ToList();

                ddlDivision.DataSource = qDivision;
                ddlDivision.DataValueField = "DivisionID";
                ddlDivision.DataTextField = "Name";
                ddlDivision.DataBind();

                var qCategory = (from ec in pdc.EventCategories
                                 orderby ec.Name
                                 select new { CategoryID = ec.EventCategoryID, Name = ec.Name }).ToList();

                ddlCategory.DataSource = qCategory;
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataBind();

                ddlWinner.DataSource = DALPortal.DSLTeamNA();
                ddlWinner.DataValueField = "TeamID";
                ddlWinner.DataTextField = "TeamName";
                ddlWinner.DataBind();

                eventSynergy = (from ev in pdc.Events
                                where ev.EventID == hdnEventID.Value.ToInt()
                                select ev).SingleOrDefault();
            }

			lnkEventDetails.Text = eventSynergy.Name;
			lnkEventDetails.NavigateUrl = "EventDetails.aspx?eventid=" + hdnEventID.Value.ToInt();
            txtEventName.Text = eventSynergy.Name;
            txtMaxPoints.Text = eventSynergy.MaxPoint.ToString();
            txtOrder.Text = eventSynergy.SortOrder.ToString();
            ddlDivision.SelectedValue = eventSynergy.EventDivisionID.ToString();
            ddlCategory.SelectedValue = eventSynergy.EventCategoryID.ToString();
            ddlWinner.SelectedValue = eventSynergy.WinnerTeamID.ToString();
            txtCreateBy.Text = eventSynergy.CreatedBy;
            txtCreateOn.Text = eventSynergy.DateCreated.Value.ToString("MMM dd, yyyy hh:mm tt");
            txtModifyBy.Text = eventSynergy.ModifiedBy.ToSafeString();
			txtModifyOn.Text = (eventSynergy.DateModified == null ? "" : eventSynergy.DateModified.Value.ToString("MMM dd, yyyy hh:mm tt"));

            if (!blnHasAccess)
            {
                txtEventName.ReadOnly = true;
                txtEventName.BackColor = System.Drawing.Color.AliceBlue;
                ddlCategory.Enabled = false;
                ddlDivision.Enabled = false;
                txtMaxPoints.ReadOnly = true;
                txtMaxPoints.BackColor = System.Drawing.Color.AliceBlue;
                ddlWinner.Enabled = false;
                txtOrder.ReadOnly = true;
                txtOrder.BackColor = System.Drawing.Color.AliceBlue;
            }

			this.CheckInsertEventTeamScore();
            this.LoadEventScheduleList();
            this.LoadEventTeamScore();
        }
    }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		int intRecordsAffected = 0;

		int eventID = Request.QueryString["eventid"].ToInt();
		string eventName = txtEventName.Text;
		int eventDivisionID = ddlDivision.SelectedValue.ToInt();
		int eventCategoryID = ddlCategory.SelectedValue.ToInt();
		int maxPoint = txtMaxPoints.Text.ToInt();
		int winnerTeamID = ddlWinner.SelectedValue.ToInt();
		int sortOrder = txtOrder.Text.ToInt();
		string modifiedBy = Request.Cookies["Speedo"]["UserName"].ToString();
		intRecordsAffected = DALPortal.UpdateEvent(eventID, eventName, eventDivisionID, eventCategoryID, maxPoint, winnerTeamID, sortOrder, true, modifiedBy);
	}

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("EventMenu.aspx");
    }

    protected void btnNewSchedule_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("EventScheduleNew.aspx?eventid=" + hdnEventID.Value);
    }

    protected void btnSaveEventTeamScore_Click(object sender, ImageClickEventArgs e)
    {
        foreach (DataGridItem ditm in dgTeamEventScore.Items)
        {
            HiddenField phdnTeamID = (HiddenField)ditm.FindControl("hdnTeamID");
            TextBox ptxtRank = (TextBox)ditm.FindControl("txtRank");
            TextBox ptxtScore = (TextBox)ditm.FindControl("txtScore");
            DALPortal.UpdateScoreRank(hdnEventID.Value.ToInt(), phdnTeamID.Value.ToInt(), ptxtRank.Text.ToInt(), ptxtScore.Text.ToInt());
        }
        this.LoadEventTeamScore();
    }

    protected void dgMatch_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        HiddenField phdnGameID = (HiddenField)e.Item.FindControl("hdnGameID");
		DALPortal.DeleteEventGame(phdnGameID.Value.ToInt());
        this.LoadEventScheduleList();
    }
}