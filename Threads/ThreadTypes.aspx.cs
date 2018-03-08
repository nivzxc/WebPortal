using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Threads_ThreadTypes : System.Web.UI.Page
{
   private void InitializeFields()
   {
      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         var q = (from tt in tdc.ThreadTypes
                  where tt.IsPrivate == true
                  orderby tt.Title
                  select new
                  {
                     ThreadTypeID = tt.ThreadTypeID,
                     Title = tt.Title
                  }).ToList();
         ddlThreadType.DataSource = q;
         ddlThreadType.DataValueField = "ThreadTypeID";
         ddlThreadType.DataTextField = "Title";
         ddlThreadType.DataBind();
      }
   }

   private void LoadThreadTypeUsers()
   {
      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         var qIncluded = (from ttu in tdc.ThreadTypesUsers
                          where ttu.ThreadTypeID == ddlThreadType.SelectedValue.ToInt()
                          orderby ttu.Username
                          select new
                          {
                             ThreadTypeUserID = ttu.ThreadTypeUserID,
                             Username = ttu.Username
                          }).ToList();

         cblThreadTypeUsers.DataSource = qIncluded;
         cblThreadTypeUsers.DataValueField = "ThreadTypeUserID";
         cblThreadTypeUsers.DataTextField = "Username";
         cblThreadTypeUsers.DataBind();

         var qExcluded = (from u in tdc.Users
                          where u.pstatus.Value == '1'
                          && !(from ttu in tdc.ThreadTypesUsers
                               where ttu.ThreadTypeID == ddlThreadType.SelectedValue.ToInt()
                               select ttu.Username).Contains(u.username)
                          orderby u.username
                          select new { Username = u.username }).ToList();

         cblEmployeeList.DataSource = qExcluded;
         cblEmployeeList.DataValueField = "username";
         cblEmployeeList.DataTextField = "username";
         cblEmployeeList.DataBind();
      }



   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         this.InitializeFields();
         this.LoadThreadTypeUsers();
      }
   }

   protected void btnSearch_Click(object sender, EventArgs e)
   {
      this.LoadThreadTypeUsers();
   }

   protected void btnRemove_Click(object sender, EventArgs e)
   {
      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         foreach (ListItem itm in cblThreadTypeUsers.Items)
         {
            if (itm.Selected)
            {
               ThreadTypesUser threadTypeUser = (from ttu in tdc.ThreadTypesUsers
                                                 where ttu.ThreadTypeUserID == itm.Value.ToInt()
                                                 select ttu).SingleOrDefault();
               tdc.ThreadTypesUsers.DeleteOnSubmit(threadTypeUser);
            }
         }

         tdc.SubmitChanges();
      }
      this.LoadThreadTypeUsers();
   }

   protected void btnInclude_Click(object sender, EventArgs e)
   {
      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         foreach (ListItem itm in cblEmployeeList.Items)
         {
            if (itm.Selected)
            {
               ThreadTypesUser ttu = new ThreadTypesUser()
               {
                  ThreadTypeID = ddlThreadType.SelectedValue.ToInt(),
                  Username = itm.Value
               };
               tdc.ThreadTypesUsers.InsertOnSubmit(ttu);
            }
         }

         tdc.SubmitChanges();
      }
      this.LoadThreadTypeUsers();
   }

}