using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Threads_ThreadEdit : System.Web.UI.Page
{

	private void InitializeFields()
	{
		int threadCategoryID = Request.QueryString["categoryid"].ToInt();
		string username = Request.Cookies["Speedo"]["UserName"];

		ckeContents.config.toolbar = new object[]
		{
			new object[] { "Cut", "Copy", "Paste", "PasteText", "-", "SpellChecker" },
			new object[] { "Undo", "Redo", "-", "-" },
			new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
			new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
			new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
			new object[] { "Link", "Unlink", "Anchor" },
			new object[] { "Image", "Table", "HorizontalRule", "Smiley", "SpecialChar" },
			new object[] { "Styles", "Format", "Font", "FontSize", "TextColor" }
		};

		trPostHome.Visible = DALThread.IsAllowedPostHome(username);

		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			var qCategory = (from tc in tdc.ThreadCategories
							 orderby tc.Name
							 select new { ThreadCategoryID = tc.ThreadCategoryID, Name = tc.Name }).ToList();
			ddlCategory.DataSource = qCategory;
			ddlCategory.DataValueField = "ThreadCategoryID";
			ddlCategory.DataTextField = "Name";
			ddlCategory.DataBind();

			ddlCategory.SelectedValue = threadCategoryID.ToString();

			var qType = (from tt in tdc.ThreadTypes
						 where tt.IsPrivate == false
						 || (from ttu in tdc.ThreadTypesUsers
							 where ttu.Username == username
							 select ttu.ThreadTypeUserID).Count() > 0
						 orderby tt.Title
						 select new { ThreadTypeID = tt.ThreadTypeID, Name = tt.Title }).ToList();

			ddlType.DataSource = qType;
			ddlType.DataValueField = "ThreadTypeID";
			ddlType.DataTextField = "Name";
			ddlType.DataBind();
		}
	}

	public void LoadThreadDetails(int threadID)
	{		
		Thread thread = new Thread();
		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			thread = (from t in tdc.Threads
					  where t.ThreadID == threadID
					  select t).SingleOrDefault();
		}

		if (thread == null)
		{
			Response.Redirect("Thread.aspx?threadid=" + threadID.ToString() + "&page=1");
		}
		else
		{
			lnkThreadList.NavigateUrl = "~/Threads/ThreadList.aspx?categoryid=" + thread.ThreadCategoryID.ToString();
			lnkThreadList.Text = DALThread.GetCategoryName(thread.ThreadCategoryID.Value);
			lnkThread.NavigateUrl = "~/Threads/Thread.aspx?threadid=" + threadID.ToString() + "&page=1";
			lnkThread.Text = thread.Title;
			lnkThreadEdit.NavigateUrl = "~/Threads/ThreadEdit.aspx?threadid=" + threadID.ToString();			

			txtTitle.Text = thread.Title;
			txtDescription.Text = thread.Description;
			ddlCategory.SelectedValue = thread.ThreadCategoryID.Value.ToString();
			ddlType.SelectedValue = thread.ThreadTypeID.Value.ToString();
			chkPostAnnouncement.Checked = thread.IsPosted.Value;
			chkIsAllowReply.Checked = thread.IsAllowedReply.Value;
			chkHideThread.Checked = !thread.IsActive.Value;
			ckeContents.Text = thread.Contents;
			chkIsPrivate.Checked = thread.IsPrivate.Value;
			divPrivateList.Visible = chkIsPrivate.Checked;
		}
	}

	private bool IsAllowedToEdit(int threadID, string username)
	{
		string postedBy= string.Empty;

		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			postedBy = (from t in tdc.Threads
						where t.ThreadID == threadID
						select t.PostedBy).SingleOrDefault();
		}

		return username == postedBy;
	}

	private void LoadPrivateUsers(int threadID)
	{
		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			var qIncluded = (from em in tdc.Employees
							 where (from tpu in tdc.ThreadPrivateUsers
									where tpu.ThreadID == threadID
									select tpu.Username).Contains(em.username)
										&& em.pstatus.Value == '1'
							 orderby em.lastname
							 select new
							 {
								 Username = em.username,
								 Name = em.lastname + ", " + em.firname
							 }).ToList();

			cblThreadMembers.DataSource = qIncluded;
			cblThreadMembers.DataValueField = "Username";
			cblThreadMembers.DataTextField = "Name";
			cblThreadMembers.DataBind();

			var qExcluded = (from em in tdc.Employees
							 where em.pstatus.Value == '1'
							 && !(from tpu in tdc.ThreadPrivateUsers
								  where tpu.ThreadID == threadID
								  select tpu.Username).Contains(em.username)
							 orderby em.lastname
							 select new
							 {
								 Username = em.username,
								 Name = em.lastname + ", " + em.nickname
							 }).ToList();

			cblEmployeeList.DataSource = qExcluded;
			cblEmployeeList.DataValueField = "Username";
			cblEmployeeList.DataTextField = "Name";
			cblEmployeeList.DataBind();
		}
	}

	///////////////////////////////
	///////// Page Events /////////
	///////////////////////////////

	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.QueryString["threadid"] == null)
		{
			Response.Redirect("~/Threads/Forums.aspx");
		}

		if (!Page.IsPostBack)
		{
			hdnThreadID.Value = Request.QueryString["threadid"];
			int threadID = hdnThreadID.Value.ToInt();
			string username = Request.Cookies["Speedo"]["UserName"];

			if (this.IsAllowedToEdit(threadID, username))
			{
				this.InitializeFields();
				this.LoadThreadDetails(threadID);
				this.LoadPrivateUsers(threadID);
			}
			else
			{
				Response.Redirect("Thread.aspx?threadid=" + threadID.ToString() + "&page=1");
			}


		}
	}

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		Thread thread = new Thread()
		{
			ThreadID = hdnThreadID.Value.ToInt(),
			ThreadCategoryID = ddlCategory.SelectedValue.ToInt(),
			ThreadTypeID = ddlType.SelectedValue.ToInt(),
			Title = txtTitle.Text,
			Description = txtDescription.Text,
			Contents = ckeContents.Text,
			IsAllowedReply = chkIsAllowReply.Checked,
			IsPosted = chkPostAnnouncement.Checked,
			IsActive = !chkHideThread.Checked,
			IsSticky = false
		};

		if (DALThread.UpdateThread(thread) > 0)
		{
			Response.Redirect("Thread.aspx?threadid=" + hdnThreadID.Value + "&page=1");
		}		
	}

	protected void btnBack_Click(object sender, ImageClickEventArgs e)
	{
		Response.Redirect("Thread.aspx?threadid=" + hdnThreadID.Value + "&page=1");
	}

	protected void btnRemove_Click(object sender, ImageClickEventArgs e)
	{
		foreach (ListItem itm in cblThreadMembers.Items)
		{
			if (itm.Selected)
			{
				DALThread.DeleteThreadPrivateUser(hdnThreadID.Value.ToInt(), itm.Value);
			}
		}
		this.LoadPrivateUsers(hdnThreadID.Value.ToInt());
	}

	protected void btnInclude_Click(object sender, ImageClickEventArgs e)
	{
		List<ThreadPrivateUser> tpuList = new List<ThreadPrivateUser>();
		foreach (ListItem itm in cblEmployeeList.Items)
		{
			if (itm.Selected)
			{
				ThreadPrivateUser tpu = new ThreadPrivateUser()
				{
					ThreadID = hdnThreadID.Value.ToInt(),
					Username = itm.Value
				};
				tpuList.Add(tpu);
			}
		}

		if (tpuList.Count > 0)
		{
			using (ThreadDataContext tdc = new ThreadDataContext())
			{
				tdc.ThreadPrivateUsers.InsertAllOnSubmit(tpuList);
				tdc.SubmitChanges();
			}
		}

		this.LoadPrivateUsers(hdnThreadID.Value.ToInt());
	}

	protected void chkIsPrivate_CheckedChanged(object sender, EventArgs e)
	{
		divPrivateList.Visible = chkIsPrivate.Checked;
	}
}