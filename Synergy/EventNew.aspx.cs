using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Synergy_EventNew : System.Web.UI.Page
{
    private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
            }
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        int intEventID = 0;
        Event ev = new Event()
          {
              Name = txtEventName.Text,
              ActivityID = SynergyCurrentID,
              EventDivisionID = ddlDivision.SelectedValue.ToInt(),
              EventCategoryID = ddlCategory.SelectedValue.ToInt(),
              MaxPoint = txtMaxPoints.Text.ToInt(),
              WinnerTeamID = ddlWinner.SelectedValue.ToInt(),
              SortOrder = txtOrder.Text.ToInt(),
              IsActive = true,
              ScoringTypeID = 1,
              CreatedBy = Request.Cookies["Speedo"]["UserName"].ToString(),
              DateCreated = DateTime.Now
          };

        using (PortalDataContext pdc = new PortalDataContext())
        {
            pdc.Events.InsertOnSubmit(ev);
            pdc.SubmitChanges();

            List<int> teamIDList = new List<int>();
            teamIDList = (from t in pdc.Teams
                          where t.ActivityID == SynergyCurrentID
                          select t.TeamID).ToList();

            //foreach (int teamID in teamIDList)
            //{
            //    EventTeamScore ets = new EventTeamScore()
            //    {

            //        EventID = ev.EventID,
            //        TeamID = teamID,
            //        Rank = 0,
            //        Score = 0
            //    };
            //    pdc.EventTeamScores.InsertOnSubmit(ets);
            //}

            //added by charlie bachiller
            intEventID = ev.EventID;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                SqlCommand cmd = cn.CreateCommand();
                cn.Open();
                if (chkActive.Checked == true)
                {
                    cmd.CommandText = "UPDATE Portal.EventsActive SET IsActive='0'";
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                cmd.CommandText = "INSERT INTO Portal.EventsActive (EventID, IsActive) VALUES(@EventID, @IsActive)";
                cmd.Parameters.Add(new SqlParameter("@EventID", intEventID));
                cmd.Parameters.Add(new SqlParameter("@IsActive", chkActive.Checked == true ? "1" : "0"));
                cmd.ExecuteNonQuery();
            }

            pdc.SubmitChanges();

        }

        Response.Redirect("EventMenu.aspx");
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("EventMenu.aspx");
    }
}