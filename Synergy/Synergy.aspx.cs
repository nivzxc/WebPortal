using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Synergy_Synergy : System.Web.UI.Page
{
   private readonly int SynergyAdminThreadTypeID = ConfigurationManager.AppSettings["CurrentSynergyThreadTypePost"].ToInt();
   private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

   //protected void LoadPosts()
   //{
   //   string strWrite = "";

   //   List<Thread> threadList = new List<Thread>();

   //   using (ThreadDataContext sdc = new ThreadDataContext())
   //   {
   //      threadList = (from t in sdc.Threads
   //                    where t.ThreadTypeID == SynergyAdminThreadTypeID
   //                    && t.IsActive == true
   //                    orderby t.PostedDate descending
   //                    select t).ToList();
   //   }
   //   int intLoopCount = 0;
   //   foreach (Thread t in threadList)
   //   {
   //    if (intLoopCount < 3)
   //    {
   //     strWrite += "<div class='border' style='padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px; background-image: none; background-color: aliceblue; margin-bottom: 9px;'>" +
   //                         "<table>" +
   //                              "<tr>" +
   //                                   "<td valign='top'>" +
   //                                        "<h2><a href='../Threads/Thread.aspx?threadid=" + t.ThreadID + "&page=1'>" + t.Title + "</a></h2>" +
   //                                        "&nbsp;Posted by <a href='Userpage/Userpage.aspx?username=" + t.PostedBy + "'>" + t.PostedBy + "</a>  " + t.PostedDate.Value.ToString("ddd MMM, dd, yyyy") +
   //                                        "<br /><br />" +
   //                                        "<span style='font-size:small;'>" + t.Description + "<br><br><a href='../Threads/Thread.aspx?threadid=" + t.ThreadID + "&page=1'>Read More</a></span>" +
   //                                   "</td>" +
   //                              "</tr>" +
   //                         "</table>" +
   //                    "</div>";
   //    }
   //    intLoopCount = intLoopCount + 1;
   //   }
   //   litAnnouncements.Text = strWrite;
   //}

   private void LoadTeams()
   {
      using (PortalDataContext pdc = new PortalDataContext())
      {
         var q = (from t in pdc.Teams
                  let xScore = (from ets in pdc.EventTeamScores where ets.TeamID == t.TeamID select ets.Score).Sum()
                  where t.ActivityID == SynergyCurrentID
                  orderby t.ColorID
                  select new
                  {
                     TeamID = t.TeamID,
                     Name = t.Name,
                     Captain = t.Captain,
                     ViceCaptain = t.ViceCaptain,
                     TeamLogo = t.TeamLogo,
                     Score = (xScore == null ? 0 : xScore)
                  }).ToList();

         dgTeams.DataSource = q.ToList();
         dgTeams.DataBind();
      }
   }

   private void LoadUpcomingSchedules()
   {
    //added by rollie 7/29/11
    DateTime latestDate = DALPortal.GetLatestGameDate();
    DateTime latestDateStart = clsDateTime.ChangeTimeToStart(latestDate);
    DateTime latestDateEnd = clsDateTime.ChangeTimeToEnd(latestDate);
    ////////


    //Add By Charlie Bachiller 2-1-2012
    DateTime dtCurrentDate = latestDate;
    DateTime dtFirstDayOfMonth = new DateTime();
    dtFirstDayOfMonth = dtCurrentDate.AddDays(-(dtCurrentDate.Day - 1));
    ////
      using (PortalDataContext pdc = new PortalDataContext())
      {
         var q = (from eg in pdc.EventGames
                  join e in pdc.Events on eg.EventID equals e.EventID into eeg
                  from eegx in eeg.DefaultIfEmpty()
                  //where eg.IsActive == true && eg.StartDate >= latestDateStart && eg.StartDate <= latestDateEnd
                  where eg.IsActive == true && eg.StartDate >= dtFirstDayOfMonth && eg.IsFinished == false
                  orderby eg.StartDate
                  select new
                  {
                     EventID = eg.EventID,
                     EventName = eegx.Name,
                     GameID = eg.GameID,
                     StartDate = eg.StartDate,
                     GamePhase = eg.GamePhase,
                     Location = eg.Location,
                     WinnerTeamID = eg.WinnerTeamID
                  });

         //var q = (from eg in pdc.EventGames
         //         join e in pdc.Events on eg.EventID equals e.EventID into eeg
         //         from eegx in eeg.DefaultIfEmpty()
         //         where eg.IsActive == true && eg.StartDate >= DateTime.Now.Date
         //         orderby eg.StartDate
         //         select new
         //         {
         //          EventID = eg.EventID,
         //          EventName = eegx.Name,
         //          GameID = eg.GameID,
         //          StartDate = eg.StartDate,
         //          GamePhase = eg.GamePhase,
         //          Location = eg.Location,
         //          WinnerTeamID = eg.WinnerTeamID
         //         });


         dgSchedule.DataSource = q.ToList();
         dgSchedule.DataBind();

         foreach (DataGridItem ditm in dgSchedule.Items)
         {
            HiddenField phdnGameID = (HiddenField)ditm.FindControl("hdnGameID");
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
                  plitTeams.Text += "<td ><img src='" + teamLogo + "'   Width='50px' Height='50px'></td>";
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
               pimgWinner.ImageUrl = (from t in pdc.Teams where t.TeamID == phdnWinner.Value.ToInt() select t.TeamLogo).SingleOrDefault();
            }

            plblGamePhase.Text = DALPortal.GetGamePhaseName(phdnGamePhase.Value);
         }
      }
   }

 /// <summary>
 /// //////Modified by Rollie 8/1/2011
 /// </summary>
   private void LoadFinishedEvents()
   {
      int synergyID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

      using (PortalDataContext pdc = new PortalDataContext())
      {
         var q = (from e in pdc.Events
                  where e.IsActive == true && e.WinnerTeamID != 0 && e.ActivityID == synergyID
                  let DivisionName = (from ed in pdc.EventDivisions
                                      where ed.EventDivisionID == e.EventDivisionID
                                      select ed.Name).SingleOrDefault()
                  let CategoryName = (from ec in pdc.EventCategories
                                      where ec.EventCategoryID == e.EventCategoryID
                                      select ec.Name).SingleOrDefault()
                  let TeamLogo = (from t in pdc.Teams
                                  where t.TeamID == e.WinnerTeamID
                                  select t.TeamLogo).SingleOrDefault()
                  orderby e.EventID descending
                  select new
                  {
                     EventID = e.EventID,
                     Name = e.Name,
                     EventDivisionName = DivisionName,
                     EventCategoryName = CategoryName,
                     MaxPoint = e.MaxPoint,
                     WinnerTeamID = e.WinnerTeamID,
                     WinnerTeamLogo = TeamLogo
                  }).Take(3).ToList();

         dgFinishedEvents.DataSource = q;
         dgFinishedEvents.DataBind();

         foreach (DataGridItem ditm in dgFinishedEvents.Items)
         {
            HiddenField phdnWinner = (HiddenField)ditm.FindControl("hdnWinner");
            Image pimgWinner = (Image)ditm.FindControl("imgWinner");
            pimgWinner.Visible = (phdnWinner.Value.Trim().Length != 0 && phdnWinner.Value != "0");
         }
      }
   }
 /////////////////

   // Page Events 

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();

      if (!Page.IsPostBack)
      {
         divControlPanel.Visible = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);
         divAddTeam.Visible = clsSystemModule.HasAccess(clsSystemModule.ModuleSynergy, Request.Cookies["Speedo"]["Username"]);
         //this.LoadPosts();
         this.LoadTeams();
         this.LoadFinishedEvents();
         this.LoadUpcomingSchedules();

          //if (SynergySurvey.IsSurveyEnd())
          //{
          //    dvVote.Visible = false;
          //}
          //else
          //{
          //    if (!SynergySurvey.AlreadyAnsweredSurvey(Request.Cookies["Speedo"]["Username"]))
          //    {
          //        dvVote.Visible = true;
          //    }
          //    else
          //    {
          //        dvVote.Visible = false;
          //    }
          //}
      }
   }
}