using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Synergy_EmployeeAchievement : System.Web.UI.Page
{

   private void InitializeFields()
   {
      using (PortalDataContext pdc = new PortalDataContext())
      {
         var qActivity = (from a in pdc.Activities
                          orderby a.ActivityID descending
                          select new
                          {
                             ActivityID = a.ActivityID,
                             Name = a.Name
                          }).ToList();
         ddlActivity.DataSource = qActivity;
         ddlActivity.DataValueField = "ActivityID";
         ddlActivity.DataTextField = "Name";
         ddlActivity.DataBind();
      }

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         var q = (from em in tdc.Employees
                  where em.pstatus == '1'
                  orderby em.lastname
                  select new
                  {
                     Usename = em.username,
                     Name = em.lastname + ", " + em.nickname
                  }).ToList();
         ddlTeamMember.DataSource = q;
         ddlTeamMember.DataValueField = "Usename";
         ddlTeamMember.DataTextField = "Name";
         ddlTeamMember.DataBind();
      }

      this.LoadAchievements();
   }

   private void LoadAchievements()
   {
      using (PortalDataContext pdc = new PortalDataContext())
      {
         var q = (from ea in pdc.Achievements
                  where ea.Username == ddlTeamMember.SelectedValue
                  join a in pdc.Activities on ea.ActivityID equals a.ActivityID
                  orderby a.ActivityID descending, ea.Award
                  select new
                  {
                     AchievementID = ea.AchievementID,
                     ActivityName = a.Name,
                     Awards = ea.Award
                  }).ToList();
         dgAchievements.DataSource = q;
         dgAchievements.DataBind();
      }
   }

   /////////////////////////////
   ///////// Page Load /////////
   /////////////////////////////

   protected void Page_Load(object sender, EventArgs e)
   {
      if (!Page.IsPostBack)
      {
         this.InitializeFields();
      }
   }

   protected void btnSave_Click(object sender, ImageClickEventArgs e)
   {
      using (PortalDataContext pdc = new PortalDataContext())
      {
         Achievement achievement = new Achievement()
         {
            ActivityID = ddlActivity.SelectedValue.ToInt(),
            Username = ddlTeamMember.SelectedValue,
            Award = txtAchievement.Text,
            CreatedBy = Request.Cookies["Speedo"]["Username"],
            DateCreated = DateTime.Now
         };

         pdc.Achievements.InsertOnSubmit(achievement);

         pdc.SubmitChanges();
      }
      txtAchievement.Text = "";
      this.LoadAchievements();
   }

   protected void ddlTeamMember_SelectedIndexChanged(object sender, EventArgs e)
   {
      this.LoadAchievements();
   }

   protected void dgAchievements_DeleteCommand(object source, DataGridCommandEventArgs e)
   {
      using (PortalDataContext pdc = new PortalDataContext())
      {
         Achievement achievement = (from a in pdc.Achievements
                                    where a.AchievementID == e.Item.Cells[0].Text.ToInt()
                                    select a).SingleOrDefault();

         pdc.Achievements.DeleteOnSubmit(achievement);
         pdc.SubmitChanges();
      }
      this.LoadAchievements();
   }
}