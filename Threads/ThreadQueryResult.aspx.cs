using System;
using System.Net.NetworkInformation;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using HRMS;
using STIeForms;
using HqWeb.GroupUpdate;

public partial class Threads_ThreadQueryResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //LoadFilter();
        if (!Page.IsPostBack)
        {
            LoadPage();
            LoadFilter();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("ThreadQuery.aspx");
    }
    protected void btnBack_Click1(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }

    protected void LoadFilter()
    {
        string strWrite = "";
        lblQueryResult.Visible = false;
        DateTime dtStartDate = new DateTime();
        DateTime dtEndDate = new DateTime();
        string strCategory = "";
        string strKeyword = "";

        if (!string.IsNullOrEmpty(Request.QueryString["DateStart"]))
        {
            dtStartDate = DateTime.ParseExact(Request.QueryString["DateStart"].ToString(), "MMMddyyyy", null);
        }
        else
        {
            dtStartDate = DateTime.Parse("1/1/1990");
        }

        if (!string.IsNullOrEmpty(Request.QueryString["DateEnd"]))
        {
            dtEndDate = DateTime.ParseExact(Request.QueryString["DateEnd"].ToString(), "MMMddyyyy", null);
        }
        else
        {
            dtEndDate = DateTime.Now;
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Category"]))
        {
            strCategory = Request.QueryString["Category"].ToString();
        }
        else
        {
            strCategory = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Keyword"]))
        {
            strKeyword = Request.QueryString["Keyword"].ToString();
        }



        using (clsPagination objPagination = new clsPagination())
        {
            //objPagination.CurrentPage = string.IsNullOrEmpty(Request.QueryString["Page"]) == true ? 1 : Request.QueryString["Page"].ToInt();
            objPagination.CurrentPage = ddlPages.SelectedValue.ToInt();
            objPagination.PagePerForm = 10;
            objPagination.TotalCount = DALThread.GetThreadArchiveCount(strCategory, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), strKeyword);

            DataTable tblResult = DALThread.GetThreadArchiveArchives(strCategory, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), objPagination.PagePerForm, objPagination.Offset(), strKeyword);

            if (tblResult.Rows.Count > 0)
            {
                lblQueryResult.Visible = true;
            }

            if (tblResult.Rows.Count == 0)
                strWrite = strWrite + "<tr><td class='GridRows'>No record found</td></tr>";
            else
                strWrite = strWrite + "<tr><td class='GridRows'>[ About " + tblResult.Rows.Count + " results found ]</td></tr>";

            foreach (DataRow drw in tblResult.Rows)
            {
                strWrite = strWrite + "<tr>" +
                                               "<td class='GridRows'>" +
                                                    "<a href='Thread.aspx?threadid=" + drw["ThreadID"].ToString() + "&page=1'  style='font-size:medium;'>" + DALThread.GetTitle(drw["ThreadID"].ToString().ToInt()) + "</a><br/>" +
                                                    "<font style='font-size: x-small; font-weight: normal; font-style: italic'>Posted By:&nbsp;<a href='../../../Userpage/UserPage.aspx?username=" + drw["PostedBy"] + "'>" + clsEmployee.GetName(drw["PostedBy"].ToString()) + "</a> (" + Convert.ToDateTime(drw["PostedDate"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")<br/>" +
                                                    DALThread.GetDescription(drw["ThreadID"].ToString().ToInt()) +
                                                "</td>" +
                                      "</tr>";
            }
            lblQueryResult.Text = strWrite;


        }
    }

    protected void LoadPage()
    {
        string strWrite = "";
        DateTime dtStartDate = new DateTime();
        DateTime dtEndDate = new DateTime();
        string strCategory = "";
        string strKeyword = "";

        if (!string.IsNullOrEmpty(Request.QueryString["DateStart"]))
        {
            dtStartDate = DateTime.ParseExact(Request.QueryString["DateStart"].ToString(), "MMMddyyyy", null);
        }
        else
        {
            dtStartDate = DateTime.Parse("1/1/1990");
        }

        if (!string.IsNullOrEmpty(Request.QueryString["DateEnd"]))
        {
            dtEndDate = DateTime.ParseExact(Request.QueryString["DateEnd"].ToString(), "MMMddyyyy", null);
        }
        else
        {
            dtEndDate = DateTime.Now;
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Category"]))
        {
            strCategory = Request.QueryString["Category"].ToString();
        }
        else
        {
            strCategory = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Keyword"]))
        {
            strKeyword = Request.QueryString["Keyword"].ToString();
        }

        using (clsPagination objPagination = new clsPagination())
        {
            objPagination.CurrentPage = string.IsNullOrEmpty(Request.QueryString["Page"]) == true ? 1 : Request.QueryString["Page"].ToInt();
            objPagination.PagePerForm = 10;
            objPagination.TotalCount = DALThread.GetThreadArchiveCount(strCategory, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), strKeyword);
            string strDateStart = dtStartDate.ToString("MMMddyyyy");
            string strDateEnd = dtEndDate.ToString("MMMddyyyy");

            //if (objPagination.TotalPage() > 1)
            //{
            //    if (objPagination.HasPreviousPage())
            //    {
            //        strWrite += "<a href='ThreadQueryResult.aspx?Page=" + objPagination.PreviousPage() + "&Category=" + strCategory + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd + "'> &laquo;Previous</a>";
            //    }

            //    int i = 0;

            //    for (i = 1; i <= objPagination.TotalPage(); i++)
            //    {
            //            if (i == objPagination.CurrentPage)
            //            {
            //                strWrite += "<span><b>" + i + "</b></span>";
            //            }
            //            else
            //            {
            //                strWrite += "<a href='ThreadQueryResult.aspx?Page=" + i + "&Category=" + strCategory + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd + "'>" + i + "</a> ";
            //            }

            //    }

            //    if (objPagination.HasNextPage())
            //    {
            //        strWrite += "<a href='ThreadQueryResult.aspx?Page=" + objPagination.NextPage() + "&Category=" + strCategory + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd + "'> Next &raquo;</a>";
            //    }
            //}

            ddlPages.Items.Clear();
            for (int i = 1; i <= objPagination.TotalPage(); i++)
            {
                ListItem l = new ListItem(i.ToString(), i.ToString(), true);
                l.Selected = l.Value == objPagination.CurrentPage.ToString() ? true : false;
                ddlPages.Items.Add(l);
            }

        }

        //lblPage.Text = strWrite;

    }

    protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime dtStartDate = new DateTime();
        //DateTime dtEndDate = new DateTime();
        //string strCategory = "";
        //string strKeyword = "";

        //if (!string.IsNullOrEmpty(Request.QueryString["DateStart"]))
        //{
        //    dtStartDate = DateTime.ParseExact(Request.QueryString["DateStart"].ToString(), "MMMddyyyy", null);
        //}
        //else
        //{
        //    dtStartDate = DateTime.Parse("1/1/1990");
        //}

        //if (!string.IsNullOrEmpty(Request.QueryString["DateEnd"]))
        //{
        //    dtEndDate = DateTime.ParseExact(Request.QueryString["DateEnd"].ToString(), "MMMddyyyy", null);
        //}
        //else
        //{
        //    dtEndDate = DateTime.Now;
        //}

        //if (!string.IsNullOrEmpty(Request.QueryString["Category"]))
        //{
        //    strCategory = Request.QueryString["Category"].ToString();
        //}
        //else
        //{
        //    strCategory = "ALL";
        //}

        //if (!string.IsNullOrEmpty(Request.QueryString["Keyword"]))
        //{
        //    strKeyword = Request.QueryString["Keyword"].ToString();
        //}
        //string strDateStart = dtStartDate.ToString("MMMddyyyy");
        //string strDateEnd = dtEndDate.ToString("MMMddyyyy");

        //Response.Redirect("ThreadQueryResult.aspx?Page=" + ddlPages.SelectedValue.ToString() + "&Category=" + strCategory + "&DateStart=" + strDateStart + "&DateEnd=" + strDateEnd);
        LoadFilter();
    }
}