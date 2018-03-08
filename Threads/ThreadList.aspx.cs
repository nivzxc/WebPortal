using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;

public partial class Threads_ThreadList : System.Web.UI.Page
{
   protected void LoadThreads()
   {
      string writeBuffer = "";
      int intPage = Request.QueryString["page"].ToInt();
      intPage = (intPage == 0 ? 1 : intPage);
      int intPageSize = ConfigurationManager.AppSettings["ForumPageSize"].ToInt();
      int intStart = ((intPage - 1) * intPageSize) + 1;
      int intEnd = intPage * intPageSize;

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = new SqlCommand("SELECT * FROM (SELECT IsSticky, ThreadID, Title, Description, PostedBy, LastPostDate, LastPostBy, TotalReply + 1 AS TotalReply, ROW_NUMBER() OVER(ORDER BY IsSticky, LastPostDate DESC) AS RowNum FROM Portal.Threads WHERE  ISActive =1 AND  ThreadCategoryID='" + Request.QueryString["categoryid"] + "') AS pao WHERE RowNum BETWEEN " + intStart + " AND " + intEnd, cn);
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         while (dr.Read())
         {
            writeBuffer += "<tr>" +
                                "<td class='GridRows'>" +
                                    "<table cellspacing='0' cellpadding='0'>" +
                                        "<tr>" +
                                            "<td style='width:50px' ><img src='../Support/" + (dr["IsSticky"].ToString() == "0" ? "sticky32.png" : "forum32.png") + "' />&nbsp;&nbsp;</td>" +
                                            "<td>" +
                                                (dr["IsSticky"].ToString() == "0" ? "<span style='font-size:small;color:cornflowerblue'><b>[Sticky]</b></span>&nbsp;" : "") +
                                                "<a href='Thread.aspx?threadid=" + dr["ThreadID"].ToString() + "&page=1' style='font-size:small;'>" + dr["Title"].ToString() + "</a>" +
                                                             //"&nbsp;by&nbsp;<a href='../Userpage/UserPage.aspx?username=" + dr["PostedBy"].ToString() + "'>" + dr["PostedBy"].ToString() + "</a>" +
                                                "<br>" + dr["Description"].ToSafeString() +
                                                             "<br>Posted on&nbsp;" + Convert.ToDateTime(dr["LastPostDate"]).ToString("MMMM dd, yyyy hh:mm tt") + " " +
                                                             //"by&nbsp;<a href='../Userpage/UserPage.aspx?username=" + dr["LastPostBy"].ToString() + "'>" + dr["LastPostBy"].ToString() + "</a>" +
                                                             "by&nbsp;<a href='../Userpage/UserPage.aspx?username=" + dr["LastPostBy"].ToString() + "'>" + dr["PostedBy"].ToString() + "</a>" +
                                                "<br>Department: " + clsDepartment.GetName(clsEmployee.GetDepartmentCode(dr["LastPostBy"].ToString())) + 
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                              
                            "</tr>";
         }
      }

      litThreads.Text = writeBuffer;
   }

   protected void LoadPaging()
   {
      string writeBuffer = "";
      int intPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
      int intTRows = 0;
      int intTRowsTemp = 0;
      int intPage = 1;
      int intPageIndex = 0;
      int intCurrentPage = 0;
      if (Request.QueryString["page"] == null)
      {
         intCurrentPage = 1;
      }
      else
      {
         intCurrentPage = Request.QueryString["page"].ToInt();
      }

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = new SqlCommand("SELECT COUNT(ThreadID) AS TCount FROM Portal.Threads WHERE  ISActive =1 AND  ThreadCategoryID='" + Request.QueryString["categoryid"] + "'", cn);
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         dr.Read();
         if (!Convert.IsDBNull(dr["TCount"]))
         {
            intTRows = dr["TCount"].ToString().ToInt();
         }
      }
      intTRowsTemp = intTRows;

      while (intTRowsTemp > 0)
      {
         if (intCurrentPage == intPage)
         {
            writeBuffer += (intPage == 1 ? "" : ",") + "&nbsp;" + intPage;
         }
         else
         {
            writeBuffer += (intPage == 1 ? "" : ",") + "&nbsp;<a href='ThreadList.aspx?categoryid=" + Request.QueryString["categoryid"] + "&page=" + intPage + "'>" + intPage + "</a>";
         }
         intPage += 1;
         intPageIndex += intPageSize;
         intTRowsTemp -= intPageSize;
      }

      litPaging.Text = writeBuffer;
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();

      if (clsThreadCategoryUser.IsAllowedToAccess(Request.Cookies["Speedo"]["UserName"]))
      {
          btnNew.Visible = true;
      }
      else
      {
          btnNew.Visible = false;
      }

      if (!Page.IsPostBack)
      {
         string threadCategoryName = "";
         int threadCategoryID = Request.QueryString["categoryid"].ToInt();

         using (ThreadDataContext tdc = new ThreadDataContext())
         {
            threadCategoryName = (from tc in tdc.ThreadCategories
                                  where tc.ThreadCategoryID == threadCategoryID
                                  select tc.Name).SingleOrDefault();
         }

         litCategoryName.Text = threadCategoryName;

      }

      this.LoadThreads();
      this.LoadPaging();
   }

   protected void btnNew_Click(object sender, EventArgs e)
   {
      Response.Redirect("ThreadNew.aspx?categoryid=" + Request.QueryString["categoryid"]);
   }
}