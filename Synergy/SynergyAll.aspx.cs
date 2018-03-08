using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Synergy_SynergyAll : System.Web.UI.Page
{
 private readonly int SynergyAdminThreadTypeID = ConfigurationManager.AppSettings["CurrentSynergyThreadTypePost"].ToInt();
 private readonly int SynergyCurrentID = ConfigurationManager.AppSettings["CurrentSynergyID"].ToString().ToInt();

 protected void LoadPosts()
 {
  string strWrite = "";

  List<Thread> threadList = new List<Thread>();

  using (ThreadDataContext sdc = new ThreadDataContext())
  {
   threadList = (from t in sdc.Threads
                 where t.ThreadTypeID == SynergyAdminThreadTypeID
                 && t.IsActive == true
                 orderby t.PostedDate descending
                 select t).ToList();
  }

  foreach (Thread t in threadList)
  {
   strWrite += "<div class='border' style='padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px; background-image: none; background-color: aliceblue; margin-bottom: 9px;'>" +
                       "<table>" +
                            "<tr>" +
                                 "<td valign='top'>" +
                                      "<h2><a href='../Threads/Thread.aspx?threadid=" + t.ThreadID + "&page=1'>" + t.Title + "</a></h2>" +
                                      "&nbsp;Posted by <a href='Userpage/Userpage.aspx?username=" + t.PostedBy + "'>" + t.PostedBy + "</a>  " + t.PostedDate.Value.ToString("ddd MMM, dd, yyyy") +
                                      "<br /><br />" +
                                      "<span style='font-size:small;'>" + t.Description + "<br><br><a href='../Threads/Thread.aspx?threadid=" + t.ThreadID + "&page=1'>Read More</a></span>" +
                                 "</td>" +
                            "</tr>" +
                       "</table>" +
                  "</div>";
  }
  litAnnouncements.Text = strWrite;
 }
    protected void Page_Load(object sender, EventArgs e)
    {
     this.LoadPosts();
    }
}