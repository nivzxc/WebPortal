using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using HqWeb.Forums;
using HqWeb.GroupUpdate;
using STIeForms;
using HRMS;

public partial class _Default : System.Web.UI.Page
{
	protected void PerfectAttendance()
	{
		string strWrite = "";
		int intCtr = 0;
		DateTime dteCurrentDateFrom = DateTime.Now;
		DateTime dteCurrentDateTo = DateTime.Now;
		using (clsTimeSheetPeriod tsp = new clsTimeSheetPeriod())
		{
			tsp.TimeSheetPeriodCode = clsTimeSheetPeriod.GetCurrentPosted();
			tsp.Fill();
			dteCurrentDateFrom = clsDateTime.GetDateOnly(tsp.PeriodFrom);
			dteCurrentDateTo = clsDateTime.GetDateOnly(tsp.PeriodTo);
		}
		Response.Write("<table><tr><td><img src='Support/star32.png' /></td><td><b><span class='HeaderText'>&nbsp;Perfect Attendance! (month of " + dteCurrentDateFrom.ToString("MMMM") + ")</span></b></td></tr></table><br />");

		DataTable tblPerfectAttendance = clsTimesheet.DSGARPerfectAttendancePortal(clsDateTime.GetMonthFirstWorkingDay(dteCurrentDateFrom), dteCurrentDateTo);
		foreach (DataRow drw in tblPerfectAttendance.Rows)
		{
			if ((float)intCtr % 5 == 0)
				strWrite += "<tr>";
			strWrite += "<td style='width:20%; text-align:center;'>" +
						 "<div class='border' style='height:100%;'>" +
						  "<table width='100%' cellpadding='3' cellspacing='1'>" +
						   "<tr>" +
							"<td class='GridRows'>" +
							 "<table>" +
							  "<tr><td><img src='http://hq.sti.edu/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + drw["username"].ToString() + ".jpg") ? drw["username"].ToString() + ".jpg" : "default.jpg") + "' width='100' height='100'></td></tr>" +
							  "<tr><td><a href='Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["NickName"] + "</a></td></tr>" +
							 "</table>" +
							"</td>" +
						   "</tr>" +
						  "</table>" +
						 "</div>" +
						"</td>";
			intCtr += 1;
			if ((float)intCtr % 5 == 0)
				strWrite += "</tr>";

		}

		if (intCtr == 0)
			Response.Write("<table style='width:100%'><tr><td style='text-align:center;font-size:small;'>No perfect attendance for this month</td></tr></table>");
		else
			Response.Write("<table style='width:100%'>" + strWrite + "</table>");
	}

	protected void PerfectAttendanceCDL()
	{
		string strWrite = "";
		int intCtr = 0;
		DateTime dteCurrentDateFrom = DateTime.Now;
		DateTime dteCurrentDateTo = DateTime.Now;
		using (clsTimeSheetPeriod tsp = new clsTimeSheetPeriod())
		{
			tsp.TimeSheetPeriodCode = clsTimeSheetPeriod.GetCurrentPosted();
			tsp.Fill();
			dteCurrentDateFrom = clsDateTime.GetDateOnly(tsp.PeriodFrom);
			dteCurrentDateTo = clsDateTime.GetDateOnly(tsp.PeriodTo);

			//DateTime dteFrom = Convert.ToDateTime("4/1/2011");
			//DateTime dteTo = Convert.ToDateTime("4/30/2011");

			//dteCurrentDateFrom = clsDateTime.GetDateOnly(dteFrom);
			//dteCurrentDateTo = Convert.ToDateTime(dteTo);
		}
		Response.Write("<table><tr><td><img src='Support/star32.png' /></td><td><b><span class='HeaderText'>&nbsp;Perfect Attendance! (month of " + dteCurrentDateFrom.ToString("MMMM") + ")</span></b></td></tr></table><br />");

		//DataTable tblPerfectAttendance = clsTimesheet.DSGARPerfectAttendancePortalCDL(clsDateTime.GetMonthFirstWorkingDay(dteCurrentDateFrom), dteCurrentDateTo);
		DataTable tblPerfectAttendance = clsTimesheet.DSGARPerfectAttendancePortalCDL(dteCurrentDateFrom, dteCurrentDateTo);
		foreach (DataRow drw in tblPerfectAttendance.Rows)
		{
			if ((float)intCtr % 5 == 0)
				strWrite += "<tr>";
			strWrite += "<td style='width:20%; text-align:center;'>" +
						 "<div class='border' style='height:100%;'>" +
						  "<table width='100%' cellpadding='3' cellspacing='1'>" +
						   "<tr>" +
							"<td class='GridRows'>" +
							 "<table>" +
							  "<tr><td><img src='http://hq.sti.edu/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + drw["username"].ToString() + ".jpg") ? drw["username"].ToString() + ".jpg" : "default.jpg") + "' width='100' height='100'></td></tr>" +
							  "<tr><td><a href='Userpage/UserPage.aspx?username=" + drw["username"] + "'>" + drw["NickName"] + "</a></td></tr>" +
							 "</table>" +
							"</td>" +
						   "</tr>" +
						  "</table>" +
						 "</div>" +
						"</td>";
			intCtr += 1;
			if ((float)intCtr % 5 == 0)
				strWrite += "</tr>";

		}

		if (intCtr == 0)
			Response.Write("<table style='width:100%'><tr><td style='text-align:center;font-size:small;'>No perfect attendance for this month</td></tr></table>");
		else
			Response.Write("<table style='width:100%'>" + strWrite + "</table>");
	}

    //protected void LoadNewEmployee()
    //{
    //    string strWrite = "";
    //    int intCtr = 0;
    //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
    //    {
    //        SqlCommand cmd = cn.CreateCommand();
    //        cmd.CommandText = "SELECT username,firname + ' ' + lastname AS pname,nickname,username,position,deptcode,divicode,datestrt FROM HR.Employees WHERE datestrt BETWEEN '" + DateTime.Now.AddDays(-30) + "' AND '" + DateTime.Now + "' AND pstatus='1' ORDER BY datestrt DESC";
    //        cn.Open();
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            strWrite = strWrite + "<tr><td width='99%'><div class='border' style='height:100%;'>" +
    //                                   "<table width='100%' cellpadding='3' cellspacing='1'>" +
    //                                    "<tr>" +
    //                                     "<td class='GridRows'><img src='http://hq.sti.edu/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='100' height='100'></td>" +
    //                                     "<td class='GridRows' style='width:100%;'>" +
    //                                      "<table>" +
    //                                       "<tr><td>Name:</td><td><a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["pname"] + "</a></td></tr>" +
    //                                       "<tr><td>Nick Name:</td><td>" + dr["nickname"].ToString() + "</td></tr>" +
    //                                       "<tr><td>Position:</td><td>" + dr["position"].ToString() + "</td></tr>" +
    //                                       "<tr><td>Division:</td><td>" + clsDivision.GetDivisionName(dr["divicode"].ToString()) + "</td></tr>" +
    //                                       "<tr><td style='vertical-align:top;'>Department:</td><td>" + clsDepartment.GetName(dr["deptcode"].ToString()) + "</td></tr>" +
    //                                       "<tr><td>Start Date:</td><td>" + clsValidator.CheckDate(dr["datestrt"].ToString()).ToString("MMMM dd, yyyy") + "</td></tr>" +
    //                                      "</table>" +
    //                                     "</td>" +
    //                                    "</tr>" +
    //                                   "</table>" +
    //                                  "</div></td></tr>";
    //            intCtr++;
    //        }
    //        dr.Close();
    //    }
    //    if (intCtr == 0)
    //        divNewEmployee.Visible = false;
    //    else
    //        divNewEmployee.Visible = true;
    //    ltNewEmployee.Text = strWrite;
    //        //Response.Write("<table style='width:100%'>" +  + "</table>");
    //}

    //protected void LoadPostHome()
    //{
    //    string strWrite = "";

    //    List<Thread> threadList = new List<Thread>();

    //    using (ThreadDataContext tdc = new ThreadDataContext())
    //    {
    //        threadList = (from t in tdc.Threads
    //                      where t.IsPosted == true && t.IsActive == true
    //                      orderby t.PostedDate descending
    //                      select t).Take(3).ToList();
    //    }

    //    foreach (Thread t in threadList)
    //    {
    //        strWrite += "<div class='ChildPagePanel'>" +
    //                        "<table>" +
    //                            "<tr>" +
    //                                "<td valign='top'>" +
    //                                    "<h2><a href='Threads/Thread.aspx?threadid=" + t.ThreadID.ToString() + "&page=1'>" + t.Title + "</a></h2>" +
    //                                    "&nbsp;by <a href='Userpage/Userpage.aspx?username=" + t.PostedBy + "'>" + t.PostedBy + "</a>  " + t.PostedDate.Value.ToString("ddd MMM, dd, yyyy") +
    //                                    (t.Description.Length == 0 ? "" : "<br /><br />" + "<span style='font-size:small;'>" + t.Description  + "<br><br><a href='Threads/Thread.aspx?threadid=" + t.ThreadID.ToString() + "&page=1'>Read More</a></span>") +
    //                                "</td>" +
    //                            "</tr>" +
    //                        "</table>" +
    //                      "</div>";
    //    }

    //    litPostHome.Text = strWrite;
    //}

	protected void LoadNotificationMRCF()
	{
		string strWrite = "";
		string strUsername = Request.Cookies["Speedo"]["UserName"];
		int intCtr = 0;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT mrcfcode,intended,username FROM CIS.Mrcf WHERE username='" + strUsername + "' AND status='M' ORDER BY datereq DESC";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(MRCF)&nbsp;<a href='CIS/MRCF/MRCFDetails.aspx?mrcfcode=" + dr["mrcfcode"] + "'>" + dr["intended"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();

			// For approval group head
			cmd.CommandText = "SELECT mrcfcode,username,intended FROM CIS.Mrcf WHERE sprvcode='" + strUsername + "' AND status='F' AND sprvstat='F' ORDER BY datereq DESC";
			dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(MRCF)&nbsp;<a href='CIS/MRCF/MRCFDetailsGH.aspx?mrcfcode=" + dr["mrcfcode"] + "'>" + dr["intended"] + "</a> by <a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();


			// For approval of division head
			cmd.CommandText = "SELECT mrcfcode,username,intended FROM CIS.Mrcf WHERE headcode='" + strUsername + "' AND status='F' AND headstat='F' AND sprvstat IN ('A','X','N') ORDER BY datereq DESC";
			dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(MRCF)&nbsp;<a href='CIS/MRCF/MRCFDetailsDH.aspx?mrcfcode=" + dr["mrcfcode"] + "'>" + dr["intended"] + "</a> by <a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();

			// for approval of procurement
			cmd.CommandText = "SELECT mrcfcode,username,intended FROM CIS.Mrcf WHERE proccode='" + strUsername + "' AND status='F' AND ((sprvstat='A' AND headstat='N') OR (headstat='A')) AND procstat='F' ORDER BY datereq DESC";
			dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(MRCF)&nbsp;<a href='CIS/MRCF/MRCFDetailsPM.aspx?mrcfcode=" + dr["mrcfcode"] + "'>" + dr["intended"] + "</a> by <a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();
		}
		Response.Write(strWrite);
	}

	protected void LoadNotificationRequisition()
	{
		string strWrite = "";
		string strUsername = Request.Cookies["Speedo"]["UserName"];
		int intCtr = 0;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT requcode,userrem,username FROM CIS.Requisition WHERE username='" + strUsername + "' AND status='M' ORDER BY datereq DESC";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(Requisition)&nbsp;<a href='CIS/Requisition/RequDetails.aspx?requcode=" + dr["requcode"] + "'>" + dr["userrem"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();

			// For approval group head
			cmd.CommandText = "SELECT requcode,username,userrem FROM CIS.Requisition WHERE sprvcode='" + strUsername + "' AND status='F' AND sprvstat='F' ORDER BY datereq DESC";
			dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(Requisition)&nbsp;<a href='CIS/Requisition/RequDetailsGH.aspx?requcode=" + dr["requcode"] + "'>" + dr["userrem"] + "</a> by <a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();


			// For approval of division head
			cmd.CommandText = "SELECT requcode,username,userrem FROM CIS.Requisition WHERE headcode='" + strUsername + "' AND status='F' AND headstat='F' AND sprvstat IN ('A','X','N') ORDER BY datereq DESC";
			dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(Requisition)&nbsp;<a href='CIS/Requisition/RequDetailsDH.aspx?requcode=" + dr["requcode"] + "'>" + dr["userrem"] + "</a> by <a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();

			// for approval of procurement
			cmd.CommandText = "SELECT requcode,username,userrem FROM CIS.Requisition WHERE suppcode='" + strUsername + "' AND status='F' AND ((sprvstat='A' AND headstat='N') OR (headstat='A')) AND suppstat='F' ORDER BY datereq DESC";
			dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				strWrite = strWrite + "<tr>" +
									   "<td class='GridRows'>" +
										"<table cellpadding='0' cellspacing='0'>" +
										 "<tr>" +
										  "<td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='Support/attach16.png' />&nbsp;</td>" +
										  "<td>&nbsp;(Requisition)&nbsp;<a href='CIS/Requisition/RequDetailsPM.aspx?requcode=" + dr["requcode"] + "'>" + dr["userrem"] + "</a> by <a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["username"] + "</a></td>" +
										 "</tr>" +
										"</table>" +
									   "</td>" +
									  "</tr>";
				intCtr++;
			}
			dr.Close();
		}
		Response.Write(strWrite);
	}

    //protected void LoadBirthday()
    //{
    //    string strReturn = "";
    //    int intCtr = 0;
    //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
    //    {
    //        SqlCommand cmd = cn.CreateCommand();
    //        cmd.CommandText = "SELECT username,firname + ' ' + lastname AS pname,username FROM Users.Users WHERE DATEPART(mm,brthdate)='" + DateTime.Now.Month + "' AND DATEPART(dd,brthdate)='" + DateTime.Now.Day + "' AND pstatus='1' ORDER BY firname";
    //        cn.Open();
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            intCtr++;
    //            strReturn = strReturn + "<td style='text-align:center;'>" +
    //                                     "<img src='http://hq.sti.edu/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='150' height='150'><br>" +
    //                                     "<a href='Userpage/UserPage.aspx?username=" + dr["username"] + "' style='font-size:small;'>" + dr["pname"] + "</a>" +
    //                                    "</td>";
    //        }
    //        dr.Close();
    //    }
    //    if (intCtr > 0)
    //    {
    //        divBirthday.Visible = true;
    //        strReturn = "<tr>" + strReturn + "</tr>";
    //        ltBirthday.Text = strReturn;
            
    //    }
    //    else
    //    {
    //        divBirthday.Visible = false;
    //    }

    //    //return strReturn;
    //}

    protected void LoadNews()
    {
        string strWrite = "";
        string strContributor="";
        DataTable tblNews = clsGroupUpdate.GetDSGHome();

        foreach (DataRow drNews in tblNews.Rows)
        {
            strContributor = drNews["Contributor"].ToString() != string.Empty ? drNews["Contributor"].ToString() : drNews["CreateBy"].ToString();
            strWrite = strWrite + "<li>" +
                                      "<a href='GroupUpdate/GroupUpdateView.aspx?GroupUpdateCode=" + drNews["GroupUpdateCode"].ToString() + "&DivisionCode=" + drNews["DivisionCode"].ToString() + "&DepartmentCode=" + drNews["DepartmentCode"].ToString() + "'><img src='UploadedFiles/GroupUpdates/" + drNews["ImageFilename"].ToString() + "' width='130' height='60' alt='' /></a>" +
                                      "<a href='GroupUpdate/GroupUpdateView.aspx?GroupUpdateCode=" + drNews["GroupUpdateCode"].ToString() + "&DivisionCode=" + drNews["DivisionCode"].ToString() + "&DepartmentCode=" + drNews["DepartmentCode"].ToString() + "'><h3>" + drNews["Title"].ToString() + "</h3></a>" +
			                          "<p>" +
                                      "<font style='font-size: x-small; font-weight: normal; font-style: italic'>By: " + strContributor + " - " + DateTime.Parse(drNews["CreateOn"].ToString()).ToString("MM/dd/yy") + "</font><br/><br/>" +
                                       drNews["Description"].ToString() +
                                       "...<br/><a href='GroupUpdate/GroupUpdateView.aspx?GroupUpdateCode=" + drNews["GroupUpdateCode"].ToString() + "&DivisionCode=" + drNews["DivisionCode"].ToString() + "&DepartmentCode=" + drNews["DepartmentCode"].ToString() + "'> &raquo; Read more</a>" +
                                      "</p>" +
                                  "</li>";
        }

        Response.Write(strWrite);
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			//this.LoadPostHome();
            //LoadBirthday();
            //LoadNewEmployee();
		}
	}

    protected void LoadAnnouncement()
    {
        string strWrite = "";

        /* REMOVE BY Calvin Feb 12, 2018
        List<Thread> threadList = new List<Thread>();
        using (ThreadDataContext tdc = new ThreadDataContext())
        {
            threadList = (from t in tdc.Threads
                          where t.IsPosted == true && t.IsActive == true
                          orderby t.PostedDate descending
                          select t).Take(7).ToList();
        }

        foreach (Thread t in threadList)
        {
            strWrite = strWrite + "<li>" +
                                    "<div class='left'><a href='Threads/Thread.aspx?threadid=" + t.ThreadID.ToString() + "&page=1' title='" + t.Title + "'>" + clsString.CutString(t.Title, 30) + "</a>" + "</div>" +
                                    "<div class='right'>" + t.PostedDate.Value.ToString("ddd MMM, dd, yyyy") + "</div>" +
                                    "<div class='clearer'>&nbsp;</div>" +
                                  "</li>";
        }

        Response.Write(strWrite);
        */
    }

    protected void LoadTodayBirthday()
    {
        string strWrite = "";

       string strReturn = "";
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username,firname + ' ' + lastname AS pname,username FROM Users.Users WHERE DATEPART(mm,brthdate)='" + DateTime.Now.Month + "' AND DATEPART(dd,brthdate)='" + DateTime.Now.Day + "' AND pstatus='1' ORDER BY firname";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                intCtr++;
                //strReturn = strReturn + "<td style='text-align:center;'>" +
                //                         "<img src='http://hq.sti.edu/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='150' height='150'><br>" +
                //                         "<a href='Userpage/UserPage.aspx?username=" + dr["username"] + "' style='font-size:small;'>" + dr["pname"] + "</a>" +
                //                        "</td>";
                strReturn = strReturn + "<li>" +
                                        "<div class='left'><div style='vertical-align:middle;padding-top:5px'><a href='Userpage/UserPage.aspx?username=" + dr["username"] + "' style='font-size:small;padding-top:10px'>" + dr["pname"] + "</a>" + "</div></div>" +
                                        //"<div class='right'><img src='http://hq.sti.edu/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='70' height='70'></div>" + <-- REMOVE By: Calvin Feb 12, 2018
                                        "<div class='right'><img src='" + clsSystemConfigurations.PortalRootURL + "/pictures/realpicture/" + clsSpeedo.GetRealPicture(dr["username"].ToString()) + ".jpg' width='70' height='70'></div>" + // added by Calvin feb 12, 2018
                                        "<div class='clearer'>&nbsp;</div>" +
                                      "</li>";
            }
            dr.Close();   
        }
        if (strReturn.Length > 0)
        {
            strReturn = "<div class='section'>" +
                        "<div class='section-title'>Today's Birthday Celebrator/s</div>" +
                        "<div class='section-content'><ul class='nice-list'>" + strReturn + "</ul></div>" +
                        "</div>";
            
            Response.Write(strReturn);
        }
    }

    protected void LoadNextBirthday()
    {
        string strWrite = "";
        string strReturn = "";
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "select TOP(4)firname + ' ' + lastname AS pname,username, brthdate from Users.Users where DATEADD (year, DatePart(year, DateAdd(dd, 1, GetDate())) - DatePart(year, brthdate), brthdate) between convert(datetime, DateAdd(dd, 1, GetDate()), 101) and convert(datetime, DateAdd(day, 20, DateAdd(dd, 1, GetDate())), 101) AND pstatus='1' ORDER BY DatePart(mm,brthdate), DatePart(dd,brthdate)";
            cmd.CommandText = "SELECT TOP(5)firname + ' ' + lastname AS pname,username, brthdate FROM Users.Users WHERE Cast(DATEDIFF(dd, brthdate, getDate()) / 365.25 as int) - Cast(DATEDIFF(dd, brthdate, DateAdd(dd, 10, GetDate())) / 365.25 as int) <> 0 AND pstatus='1' AND username NOT IN(SELECT username FROM Users.Users WHERE DATEPART(mm,brthdate)='" + DateTime.Now.Month + "' AND DATEPART(dd,brthdate)='" + DateTime.Now.Day + "' AND pstatus='1') ORDER BY DatePart(mm,brthdate), DatePart(dd,brthdate)";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                intCtr++;
                strReturn = strReturn + "<li>" +
                                        "<div class='left'><div><a href='Userpage/UserPage.aspx?username=" + dr["username"] + "' style='font-size:small;'>" + dr["pname"] + "</a>" + "</div></div>" +
                                        "<div class='right'>" + DateTime.Parse(dr["brthdate"].ToString()).ToString("MMMM dd") + "</div>" +
                                        "<div class='clearer'>&nbsp;</div>" +
                                      "</li>";

            }
            dr.Close();


        }
        
        if (strReturn.Length > 0)
        {
            strReturn = "<div class='section'>" +
                        "<div class='section-title'>Upcoming Birthday Celebrators</div>" +
                        "<div class='section-content'><ul class='nice-list'>" + strReturn +
                        "<li><div class='left'><b><a href='" + clsSystemConfigurations.PortalRootURL + "/HR/HRBirthdayCelebrants.aspx' style='font-size:small;'>View Celebrators &#187;</a></b></div></li>" +
                        "</ul></div>" +
                        "</div>";

            Response.Write(strReturn);
        }

    }

    protected void LoadNewEmployees()
    {
        string strWrite = "";
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username,firname + ' ' + lastname AS pname,nickname,username,position,deptcode,divicode,datestrt FROM HR.Employees WHERE datestrt BETWEEN '" + DateTime.Now.AddDays(-30) + "' AND '" + DateTime.Now + "' AND pstatus='1' ORDER BY datestrt DESC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = strWrite + "<li>" +
                                        "<div class='left'><a href='Userpage/UserPage.aspx?username=" + dr["username"] + "'>" + dr["pname"] + "</a></div>" +
                                        "<div class='right'>" + clsValidator.CheckDate(dr["datestrt"].ToString()).ToString("ddd MMM dd, yyyy") + "</div>" +
                                        "<div class='clearer'>&nbsp;</div>" +
                                      "</li>";

                intCtr++;
            }
            dr.Close();
        }
    
        if (strWrite.Length > 0)
        {
            strWrite = "<div class='section'>" +
                        "<div class='section-title'>New Employees</div>" +
                        "<div class='section-content'><ul class='nice-list'>" + strWrite + "</ul></div>" +
                        "</div>";

            Response.Write(strWrite);
        }

    }

}