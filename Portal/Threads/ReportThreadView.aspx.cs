using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Threads_ReportThreadView : System.Web.UI.Page
{

	private void LoadCategory()
	{
		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			var q = (from tc in tdc.ThreadCategories
					 where tc.IsActive == true
					 orderby tc.Name
					 select new { ThreadCategoryID = tc.ThreadCategoryID, Name = tc.Name }).ToList();

			ddlThreadCategory.DataSource = q;
			ddlThreadCategory.DataValueField = "ThreadCategoryID";
			ddlThreadCategory.DataTextField = "Name";
			ddlThreadCategory.DataBind();
		}
	}

	private void LoadThread(int threadCategoryID)
	{
		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			var q = (from t in tdc.Threads
					 where t.ThreadCategoryID == threadCategoryID
					 orderby t.Title
					 select new { ThreadID = t.ThreadID, Title = t.Title }).ToList();

			ddlThread.DataSource = q;
			ddlThread.DataValueField = "ThreadID";
			ddlThread.DataTextField = "Title";
			ddlThread.DataBind();
		}
	}

	///////////////////////////////
	///////// Page Events /////////
	///////////////////////////////

    protected void Page_Load(object sender, EventArgs e)
    {
		clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			this.LoadCategory();
			if (ddlThreadCategory.Items.Count > 0)
			{
				this.LoadThread(ddlThreadCategory.Items[0].Value.ToInt());
			}
		}
    }

	protected void ddlThreadCategory_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.LoadThread(ddlThreadCategory.SelectedValue.ToInt());
	}

	protected void btnSearch_Click(object sender, ImageClickEventArgs e)
	{
		if (ddlThreadCategory.Items.Count > 0 && ddlThread.Items.Count > 0)
		{
			using (ThreadDataContext tdc = new ThreadDataContext())
			{
				var q = (from em in tdc.Employees
						 let xTotalView = (from tv in tdc.ThreadViews
										   where tv.ThreadID == ddlThread.SelectedValue.ToInt()
										   && tv.Username == em.username
										   select tv.ThreadViewID).Count()
						 where xTotalView > 0
						 orderby xTotalView descending
						 select new
						 {
							 Name = em.lastname + ", " + em.firname,
							 TotalView = xTotalView
						 }).ToList();
				dgItems.DataSource = q;
				dgItems.DataBind();
			}
		}
	}
}