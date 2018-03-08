using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultAllPosts : System.Web.UI.Page
{

	protected void LoadPostHome()
	{
		string strWrite = "";

		List<Thread> threadList = new List<Thread>();

		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			threadList = (from t in tdc.Threads
						  where t.IsPosted == true && t.IsActive == true
						  orderby t.PostedDate descending
						  select t).ToList();
		}

		foreach (Thread t in threadList)
		{
			strWrite += "<div class='ChildPagePanel'>" +
							"<table>" +
								"<tr>" +
									"<td valign='top'>" +
										"<h2><a href='Threads/Thread.aspx?threadid=" + t.ThreadID.ToString() + "&page=1'>" + t.Title + "</a></h2>" +
										"&nbsp;by <a href='Userpage/Userpage.aspx?username=" + t.PostedBy + "'>" + t.PostedBy + "</a>  " + t.PostedDate.Value.ToString("ddd MMM, dd, yyyy") +
										(t.Description.Length == 0 ? "" : "<br /><br />" + "<span style='font-size:small;'>" + t.Description + "<br><br><a href='Threads/Thread.aspx?threadid=" + t.ThreadID.ToString() + "&page=1'>Read More</a></span>") +
									"</td>" +
								"</tr>" +
							"</table>" +
						  "</div>";
		}

		litPostHome.Text = strWrite;
	}

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!Page.IsPostBack)
		{
			this.LoadPostHome();
		}
    }
}