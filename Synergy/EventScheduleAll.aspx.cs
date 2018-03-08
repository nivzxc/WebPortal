using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Synergy_EventScheduleAll : System.Web.UI.Page
{
   private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

   private void BindEventScheduleList()
   {
      using (PortalDataContext pdc = new PortalDataContext())
      {
         var predicate = PredicateBuilder.True<EventGame>();

         if (ddlEvent.SelectedValue != "ALL")
         {
            predicate = predicate.And(p => p.EventID == ddlEvent.SelectedValue.ToInt());
         }

         if (ddlTeam.SelectedValue != "ALL")
         {
            predicate = predicate.And(p => (from egt in pdc.EventGameTeams where egt.TeamID == ddlTeam.SelectedValue.ToInt() select egt.GameID).Contains(p.GameID));
         }

         var q = (from eg in pdc.EventGames.Where(predicate)
                  where eg.IsActive == true
                  && (from ev in pdc.Events
                      where ev.ActivityID == SynergyCurrentID
                      select ev.EventID).Contains(eg.EventID)
                  let xEventName = (from ev in pdc.Events
                                    where ev.EventID == eg.EventID
                                    select ev.Name).SingleOrDefault()
                  orderby eg.StartDate
                  select new
                  {
                     EventID = eg.EventID,
                     EventName = xEventName,
                     GameID = eg.GameID,
                     GamePhase = eg.GamePhase,
                     StartDate = eg.StartDate,
                     Location = eg.Location,
                     WinnerTeamID = eg.WinnerTeamID,
                     IsFinished = eg.IsFinished
                  }).ToList();
         dgSchedule.DataSource = q;
         dgSchedule.DataBind();

         foreach (DataGridItem ditm in dgSchedule.Items)
         {
            HiddenField phdnEventID = (HiddenField)ditm.FindControl("hdnEventID");
            HiddenField phdnGameID = (HiddenField)ditm.FindControl("hdnGameID");
            HiddenField phdnDateStart = (HiddenField)ditm.FindControl("hdnDateStart");
            Label plblDateStart = (Label)ditm.FindControl("lblDateStart");
            Literal plitTeams = (Literal)ditm.FindControl("litTeams");
            HiddenField phdnWinner = (HiddenField)ditm.FindControl("hdnWinner");
            Image pimgWinner = (Image)ditm.FindControl("imgWinner");
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
                  plitTeams.Text += "<td><img src='" + teamLogo + "'   Width='50px' Height='50px'></td>";
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

            plblDateStart.Text = clsValidator.CheckDate(phdnDateStart.Value).ToString("MMM dd, yyyy hh:mm tt");
            plblGamePhase.Text = DALPortal.GetGamePhaseName(phdnGamePhase.Value);

         }
      }
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      if (!Page.IsPostBack)
      {
         ddlEvent.DataSource = DALPortal.DSLEventsAll();
         ddlEvent.DataValueField = "EventID";
         ddlEvent.DataTextField = "EventName";
         ddlEvent.DataBind();

         ddlTeam.DataSource = DALPortal.DSLTeamAll();
         ddlTeam.DataValueField = "TeamID";
         ddlTeam.DataTextField = "TeamName";
         ddlTeam.DataBind();
         btnBack.Attributes.Add("onclick", "history.back(); return false");

      }
      this.BindEventScheduleList();
   }

   protected void btnBack_Click(object sender, ImageClickEventArgs e)
   {
       //Response.Redirect("SynergyHome.aspx");
   }
}