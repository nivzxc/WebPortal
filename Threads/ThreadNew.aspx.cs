using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class Threads_ThreadNew : System.Web.UI.Page
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
            //var qCategory = (from tc in tdc.ThreadCategories
            //                 where tc.IsActive==true
            //                 orderby tc.Name
            //                 select new { ThreadCategoryID = tc.ThreadCategoryID, Name = tc.Name }).ToList();
            //ddlCategory.DataSource = qCategory;
            //ddlCategory.DataValueField = "ThreadCategoryID";
            //ddlCategory.DataTextField = "Name";
            //ddlCategory.DataBind();

            //ddlCategory.SelectedValue = threadCategoryID.ToString();

            ddlCategory.DataSource = clsThreadCategoryUser.GetDSLThreadCategoryPerUser(Request.Cookies["Speedo"]["UserName"]);
            ddlCategory.DataValueField = "ThreadCategoryID";
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataBind();

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

	private void InsertRecord()
	{
		string username = Request.Cookies["Speedo"]["UserName"];

		ThreadDataContext sdc = new ThreadDataContext();

		try
		{
			Thread thread = new Thread
			{
				ThreadCategoryID = ddlCategory.SelectedValue.ToInt(),
				ThreadTypeID = ddlType.SelectedValue.ToInt(),
				Title = txtTitle.Text,
				Description = txtDescription.Text,
				Contents = ckeContents.Text,
				IsAllowedReply = chkIsAllowReply.Checked,
				IsPosted = chkPostAnnouncement.Checked,
				IsPrivate = chkIsPrivate.Checked,
				IsActive = true,
				IsSticky = false,
				PostedBy = username,
				PostedDate = DateTime.Now,
				LastPostBy = username,
				LastPostDate = DateTime.Now,
				TotalReply = 0
			};

			if (fuAttachment.HasFile)
			{
				string fileName = System.Guid.NewGuid().ToString() + "." + Path.GetExtension(fuAttachment.FileName);
				fuAttachment.SaveAs(Server.MapPath(@"~\App_Attachments\" + fileName));
				thread.AttachedFileName = fileName;
				thread.AttachedFileDescription = (txtAttachment.Text.Length == 0 ? "Attached File" : txtAttachment.Text);
			}

			sdc.Threads.InsertOnSubmit(thread);
			sdc.SubmitChanges();

			if (chkIsPrivate.Checked)
			{
				foreach (ListItem itm in cblThreadMembers.Items)
				{
					ThreadPrivateUser tpu = new ThreadPrivateUser()
					{
						ThreadID = thread.ThreadID,
						Username = itm.Value
					};

					sdc.ThreadPrivateUsers.InsertOnSubmit(tpu);
				}
			}

			sdc.SubmitChanges();
		}
		catch (Exception ex)
		{
			DALPortal.LogError(username, MethodBase.GetCurrentMethod().ReflectedType.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
		}
		finally
		{
			sdc.Dispose();
		}

		Response.Redirect("ThreadList.aspx?categoryid=" + Request.QueryString["categoryid"] + "&page=1");
	}

	private void LoadPrivateUsers()
	{
		List<string> selectedEmployees = new List<string>();
		foreach (ListItem itm in cblThreadMembers.Items)
		{
			selectedEmployees.Add(itm.Value);
		}

		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			var q = (from em in tdc.Employees
					 where em.pstatus.Value == '1'
					 && !(from se in selectedEmployees
						  select se).Contains(em.username)
					 orderby em.lastname
					 select new
					 {
						 Username = em.username,
						 Name = em.lastname + ", " + em.nickname
					 }).ToList();

			cblEmployeeList.DataSource = q;
			cblEmployeeList.DataValueField = "Username";
			cblEmployeeList.DataTextField = "Name";
			cblEmployeeList.DataBind();
		}
	}

    ////////////////////////////////
    ///////// Page Events  /////////
    ////////////////////////////////

    protected void Page_Load(object sender, EventArgs e)
    {
       clsSpeedo.Authenticate();
		if (Request.QueryString["categoryid"] == null)
		{
			Response.Redirect("~/Threads/Forums.aspx");
		}

		int threadCategoryID = Request.QueryString["categoryid"].ToInt();


		if (!Page.IsPostBack)
		{
			this.InitializeFields();
			this.LoadPrivateUsers();
		}
    }

    protected void btnSave_Click(object sender, EventArgs e)
	{
		if (Page.IsValid)
		{
			this.InsertRecord();
		}
	}

    protected void btnBack_Click(object sender, EventArgs e)
	{
		Response.Redirect("ThreadList.aspx?categoryid" + Request.QueryString["categoryid"].ToString() + "&page=1");
	}

	protected void chkIsPrivate_CheckedChanged(object sender, EventArgs e)
	{
		divPrivateList.Visible = chkIsPrivate.Checked;
	}

    protected void btnRemove_Click(object sender, EventArgs e)
	{
		List<ListItem> deleteList = new List<ListItem>();
		foreach (ListItem itm in cblThreadMembers.Items)
		{
			if (itm.Selected)
			{
				deleteList.Add(itm);
			}
		}

		foreach (ListItem itm in deleteList)
		{
			cblThreadMembers.Items.Remove(itm);
		}

		this.LoadPrivateUsers();
	}

    protected void btnInclude_Click(object sender, EventArgs e)
	{
		foreach (ListItem itm in cblEmployeeList.Items)
		{
			if (itm.Selected)
			{
				cblThreadMembers.Items.Add(new ListItem(itm.Text, itm.Value));
			}
		}
		this.LoadPrivateUsers();
	}
}