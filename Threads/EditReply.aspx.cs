using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Threads_EditReply : System.Web.UI.Page
{

    private void InitializeFields()
    {
		int replyID = Request.QueryString["threadreplyid"].ToInt();
        string replyContents = "";

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

        using (ThreadDataContext tdc = new ThreadDataContext())
        {
            replyContents = (from tr in tdc.ThreadReplies
                             where tr.ThreadReplyID == replyID
                             select tr.ReplyContents).SingleOrDefault();
        }

        ckeContents.Text = replyContents;
    }

    private bool IsAuthorizedToEdit(string username, int replyID)
    {
        bool isAuthorized = false;

        using (ThreadDataContext tdc = new ThreadDataContext())
        {
            isAuthorized = (from tr in tdc.ThreadReplies
                            where tr.Username == username && tr.ThreadReplyID == replyID
                            select tr.ThreadReplyID).Count() > 0;
        }

        return isAuthorized;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        string username = Request.Cookies["Speedo"]["UserName"];
        int replyID = Request.QueryString["threadreplyid"].ToInt();

        if (!Page.IsPostBack)
        {
            if (this.IsAuthorizedToEdit(username, replyID))
            {
                this.InitializeFields();
            }
            else
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
		int replyID = Request.QueryString["threadreplyid"].ToInt();
		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			ThreadReply tr = tdc.ThreadReplies.Where(p => p.ThreadReplyID == replyID).SingleOrDefault();
			tr.ReplyContents = ckeContents.Text;
			tdc.SubmitChanges();
		}
		Response.Redirect("Thread.aspx?threadid=" + Request.QueryString["threadid"] + "&page=1");
    }
}