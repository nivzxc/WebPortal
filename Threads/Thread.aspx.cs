using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using HqWeb.Forums;

public partial class Threads_Thread : System.Web.UI.Page
{
    private string prevpage = "";
   private void InitializeReplyPanel()
   {
      //divReplies.Visible = true;
      //ckeReply.config.toolbar = new object[]
      //  {
      //      new object[] { "Cut", "Copy", "Paste", "PasteText", "-", "SpellChecker" },
      //      new object[] { "Undo", "Redo", "-", "-" },
      //      new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
      //      new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
      //      new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
      //      new object[] { "Link", "Unlink", "Anchor" },
      //      new object[] { "Image", "Table", "HorizontalRule", "Smiley", "SpecialChar" },
      //      new object[] { "Styles", "Format", "Font", "FontSize", "TextColor" }
      //  };
   }

   private int CountReplies(int threadID)
   {
      int replyCount = 0;

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         replyCount = (from tr in tdc.ThreadReplies
                       where tr.ThreadID == threadID
                       select tr.ThreadReplyID).Count();
      }

      return replyCount;
   }

   protected int GetLastPage(int threadID)
   {
      int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
      int intTRows = 0;
      int intTRowsTemp = 0;
      int intPage = 1;
      int intPageIndex = 0;

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         intTRows = (from tr in tdc.ThreadReplies
                     where tr.ThreadID == threadID
                     select tr.ThreadReplyID).Count();
      }

      intTRowsTemp = intTRows;

      while (intTRowsTemp > 0)
      {
         intPage += 1;
         intPageIndex += intPageSize;
         intTRowsTemp -= intPageSize;
      }

      return intPage - 1;
   }

   protected void LoadPaging()
   {
      string writeBuffer = "";

      int threadID = Request.QueryString["threadid"].ToInt();
      int intCurrentPage = clsValidator.CheckInteger(Request.QueryString["page"]);
      int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
      int intTRows = 0;
      int intTRowsTemp = 0;
      int intPage = 1;

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = new SqlCommand("SELECT COUNT(ThreadReplyID) AS TCount FROM Portal.ThreadReply WHERE ThreadID=@ThreadID", cn);
         cmd.Parameters.Add(new SqlParameter("@ThreadID", threadID));
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         dr.Read();
         if (!Convert.IsDBNull(dr["TCount"]))
            intTRows = Convert.ToInt32(dr["TCount"]);
         dr.Close();
      }
      intTRowsTemp = intTRows;


      writeBuffer = "<table width='100%' cellpadding='0' cellspacing='0'><tr><td style='text-align:left;'>&nbsp;Page&nbsp;&nbsp;&nbsp;&nbsp;";
      while (intTRowsTemp > 0)
      {

         intTRowsTemp -= intPageSize;

         if (intPage == intCurrentPage)
            writeBuffer += (intPage == 1 ? "" : " | ") + (intPage == 1 ? "First" : (intTRowsTemp <= 0 ? "Last" : intPage.ToString()));
         else
         {
            if ((intPage >= (intCurrentPage - 8) && intPage <= (intCurrentPage + 8)) || (intPage == 1) || (intTRowsTemp <= 0))
               writeBuffer += (intPage == 1 ? "" : " | ") + "<a href='Thread.aspx?threadid=" + Request.QueryString["threadid"] + "&page=" + intPage + "'>" + (intPage == 1 ? "First" : (intTRowsTemp <= 0 ? "Last" : intPage.ToString())) + "</a>";
         }

         intPage++;
      }

      writeBuffer += "</td><td style='text-align:right;'>[Total Items:" + intTRows + "]&nbsp;</td></tr></table>";
     // litPaging.Text = writeBuffer;
   }

   private string GetPosition(string username)
   {
      string title = "";

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         title = (from e in tdc.Employees
                  where e.username == username
                  select e.position).SingleOrDefault();
      }

      return title;
   }

   private string GetName(string username)
   {
      string name = "";

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         name = (from e in tdc.Employees
                 where e.username == username
                 select e.nickname + " " + e.lastname).SingleOrDefault();
      }

      return name;
   }

   private bool IsAllowedToView(int threadID, string username)
   {
      bool isAllowed = false;

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         isAllowed = (from tpu in tdc.ThreadPrivateUsers
                      where tpu.ThreadID == threadID && tpu.Username == username
                      select tpu.ThreadUserID).ToList().Count() > 0;
      }

      return isAllowed;
   }

   private void LoadThreadDetails(int threadID)
   {
      string username = Request.Cookies["Speedo"]["UserName"].ToLower();
      string threadCategoryName = "";
      Thread thread = new Thread();

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         thread = (from t in tdc.Threads
                   where t.ThreadID == threadID
                   select t).SingleOrDefault();

         threadCategoryName = (from tc in tdc.ThreadCategories
                               where tc.ThreadCategoryID == thread.ThreadCategoryID
                               select tc.Name).SingleOrDefault();
      }

      if (thread != null)
      {
         if (!thread.IsPrivate.Value || username == thread.PostedBy || this.IsAllowedToView(threadID, username))
         {


            litThreadName.Text = thread.Title;
            lnkCreatorName.Text = this.GetName(thread.PostedBy);
            lnkCreatorName.NavigateUrl = "~/UserPage/Userpage.aspx?username=" + thread.PostedBy;
            litPosition.Text = this.GetPosition(thread.PostedBy);
            litDatePosted.Text = "Date Posted: " + thread.PostedDate.Value.ToString("MMM dd, yyyy hh:mm tt");

            if (thread.AttachedFileName.ToSafeString().Length > 0)
            {
               divAttachment.Visible = true;
               lnkAttachment.Text = thread.AttachedFileDescription;
               lnkAttachment.NavigateUrl = "~/App_Attachments/" + thread.AttachedFileName;
            }

            if (thread.IsAllowedReply.Value)
            {
               this.InitializeReplyPanel();
               this.LoadThreadReplies(threadID);
            }
            else
            {
              // divReplies.Visible = false;
            }

            if (thread.Contents != null)
            {
               litContent.Text = clsBB.FormatContents(thread.Contents);
            }

            if (username == thread.PostedBy)
            {
               lnkEditThread.Visible = true;
               lnkEditThread.NavigateUrl = "~/Threads/ThreadEdit.aspx?threadid=" + thread.ThreadID.ToString();
            }
            else
            {
               lnkEditThread.Visible = false;
            }

            this.InsertView(threadID, username);
         }
         else
         {
            Response.Redirect("~/AccessDenied.aspx");
         }

      }
   }

   private void LoadThreadReplies(int threadID)
   {
      string writeBuffer = "";
      string username = Request.Cookies["Speedo"]["UserName"].ToLower();
      int intPage = Request.QueryString["page"].ToInt();
      int intPageSize = ConfigurationManager.AppSettings["PageSize"].ToInt();
      int intStart = ((intPage - 1) * intPageSize) + 1;
      int intEnd = intPage * intPageSize;

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT * FROM (SELECT ThreadID, ThreadReplyID, Portal.ThreadReply.Username, ReplyContents, DateReply, emptitle, ROW_NUMBER() OVER(ORDER BY DateReply DESC) AS RowNum FROM Portal.ThreadReply INNER JOIN Users.Users ON Portal.ThreadReply.Username = Users.Users.username WHERE ThreadID=@ThreadID) AS pao	WHERE RowNum BETWEEN " + intStart + " AND " + intEnd;
         cmd.Parameters.Add(new SqlParameter("@ThreadID", threadID));
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         while (dr.Read())
         {
            writeBuffer += "<div class='GridBorder' style='margin-top: 9px;'>" +
                                "<table width='100%' cellpadding='4' class='Grid' cellspacing='2'>" +
                                    "<tr>" +
                                        "<td style='text-align: left;' class='GridRows'>" +
                                            "<table cellpadding='0' cellspacing='0' width='100%'>" +
                                                "<tr>" +
                                                    "<td align='left'>" +
                                                        "<table cellpadding='0' cellspacing='0'>" +
                                                            "<tr>" +
                                                                "<td><img src='../Pictures/avatar/" + dr["Username"].ToString() + ".jpg' alt='' /></td>" +
                                                                "<td>&nbsp;</td>" +
                                                                "<td>" +
                                                                    "<a href='../UserPage/Userpage.aspx?username=" + dr["Username"].ToString() + "style='font-size: small;'>" + dr["Username"].ToString() + "</a>" +
                                                                    " - <i>" + dr["emptitle"].ToString() + "</i><br />" +
                                                                    "Date Posted: " + dr["DateReply"].ToString() +
                                                                "</td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td align='right' valign='top'>" +
                                                                       (dr["Username"].ToString() == username ? "<a href='EditReply.aspx?threadid=" + dr["ThreadID"].ToString() + "&threadreplyid=" + dr["ThreadReplyID"].ToString() + "' style='font-size:small;'>[Edit Reply]</a>" : "") +
                                                    "</td>" +
                                                "</tr>" +
                                            "</table>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td class='GridRows' style='font-size: small;'>" +
                                            "<br />" + clsBB.FormatContents(dr["ReplyContents"].ToString()) +
                                        "</td>" +
                                    "</tr>" +
                                "</table>" +
                            "</div>";
         }
         dr.Close();
      }

      //litReplies.Text = writeBuffer;
   }

   private void SaveReply()
   {
      bool isSuccess = false;
      string username = Request.Cookies["Speedo"]["UserName"];
      int threadID = Request.QueryString["threadid"].ToInt();
      ThreadDataContext sdc = new ThreadDataContext();

      try
      {
         ThreadReply tr = new ThreadReply
         {
            ThreadID = threadID,
            Username = username,
            //ReplyContents = ckeReply.Text,
            DateReply = DateTime.Now
         };

         sdc.ThreadReplies.InsertOnSubmit(tr);
         sdc.SubmitChanges();
         isSuccess = true;
      }
      catch (Exception ex)
      {
         DALPortal.LogError(username, MethodBase.GetCurrentMethod().ReflectedType.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
      }
      finally
      {
         sdc.Dispose();
      }

      using (ThreadDataContext pdc = new ThreadDataContext())
      {
         Thread thread = (from t in pdc.Threads where t.ThreadID == threadID select t).SingleOrDefault();
         thread.TotalReply = thread.TotalReply + 1;
         thread.LastPostBy = username;
         thread.LastPostDate = DateTime.Now;
         pdc.SubmitChanges();
      }

      if (isSuccess)
      {
         Response.Redirect("Thread.aspx?threadid=" + threadID.ToString() + "&page=1");
      }
   }

   private void InsertView(int threadID, string username)
   {
      using (ThreadDataContext pdc = new ThreadDataContext())
      {
         ThreadView tv = new ThreadView()
         {
            ThreadID = threadID,
            Username = username,
            DateViewed = DateTime.Now
         };

         pdc.ThreadViews.InsertOnSubmit(tv);
         pdc.SubmitChanges();
      }
   }

   ///////////////////////////////
   ///////// Page Events /////////
   ///////////////////////////////

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();

      if (Request.QueryString["threadid"] == null)
      {
         Response.Redirect("ThreadError.aspx");
      }
      else
      {
         int threadID = Request.QueryString["threadid"].ToInt();

         if (!Page.IsPostBack)
         {
            
            this.LoadThreadDetails(threadID);
            this.LoadPaging();
            btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");

         }
      }
   }

   protected void btnPostReply_Click(object sender, EventArgs e)
   {
      //if (ckeReply.Text.Length > 0)
      //{
      //   this.SaveReply();
      //}
   }

   protected void btnBack_Click(object sender, EventArgs e)
   {
       Response.Redirect(prevpage);
   }
}