﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Threads_ThreadCategory : System.Web.UI.Page
{

   protected void LoadForums(int threadGroupID)
   {
      string writeBuffer = "";

      ThreadGroup threadGroup = new ThreadGroup();

      using (ThreadDataContext tdc = new ThreadDataContext())
      {
         threadGroup = (from tg in tdc.ThreadGroups
                        where tg.ThreadGroupID == threadGroupID
                        select tg).SingleOrDefault();

         writeBuffer += "<div class='GridBorder'>" +
                                  "<table width='100%' cellpadding='5' class='grid'>" +
                                       "<tr>" +
                                            "<td colspan='4' align='center' class='GridText' style='text-align:left;font-size:small;'>" +
                                                 "<table>" +
                                                      "<tr>" +
                                                           "<td><img src='../Support/AppHead.png' alt='' /></td>" +
                                                           "<td><b>" + threadGroup.Name + "</b></td>" +
                                                      "<tr>" +
                                                 "</table>" +
                                            "</td>" +
                                       "</tr>" +
                                       "<tr>" +
                                            "<td class='GridColumns' width='55%'><b>Thread</b></td>" +
                                            "<td class='GridColumns' width='30%'><b>Last Post</b></td>" +
                                            "<td class='GridColumns' width='15%'><b>Posts</b></td>" +
                                       "</tr>";

         List<ThreadCategory> threadCategoryList = new List<ThreadCategory>();

         threadCategoryList = (from tc in tdc.ThreadCategories
                               where tc.ThreadGroupID == threadGroup.ThreadGroupID
                               orderby tc.Name
                               select tc).ToList();

         foreach (ThreadCategory tc in threadCategoryList)
         {
            int postCount = (from t in tdc.Threads
                             where t.ThreadCategoryID == tc.ThreadCategoryID
                             select t).Count();
            Thread threadLastPost = (from t in tdc.Threads
                                     where t.ThreadCategoryID == tc.ThreadCategoryID
                                     orderby t.LastPostDate descending
                                     select t).Take(1).SingleOrDefault();

            string lastPostDetails = string.Empty;

            if (threadLastPost != null)
            {
               lastPostDetails = "<a href='Thread.aspx?threadid=" + threadLastPost.ThreadID + "&page=1'>" + (threadLastPost.Title.Length > 25 ? threadLastPost.Title.Substring(0, 25) + "..." : threadLastPost.Title) + "</a>" +
                                        "<br>by: <a href='../UserProfile.aspx?username=" + threadLastPost.LastPostBy + "'>" + threadLastPost.LastPostBy + "</a>";
            }

            writeBuffer += "<tr>" +
                                     "<td class='GridRows'>" +
                                          "<table cellpadding='0' cellspacing='0'>" +
                                               "<tr>" +
                                                    "<td><img src='../Support/folderyellow32.png' />&nbsp;</td>" +
                                                    "<td><a href='ThreadList.aspx?categoryid=" + tc.ThreadCategoryID.ToString() + "&page=1' style='font-size:small;'>" + tc.Name + "</a><br>" + tc.Description + "</td>" +
                                               "</tr>" +
                                          "</table>" +
                                     "</td>" +
                                     "<td class='GridRows'>" +
                                          lastPostDetails +
                                     "</td>" +
                                     "<td class='GridRows' style='text-align:center'>" + postCount.ToString() + "</td>" +
                                "</tr>";
         }

         writeBuffer += "</table></div><br>";

      }
      litCategoryHeader.Text = threadGroup.Name;
      litCategory.Text = writeBuffer;

   }

   protected void Page_Load(object sender, EventArgs e)
   {
      clsSpeedo.Authenticate();
      if (!Page.IsPostBack)
      {
         if (Request.QueryString["tgid"] == null)
            Response.Redirect("~/Threads/Forums.aspx");
         else
            this.LoadForums(Request.QueryString["tgid"].ToInt());
      }
   }
}