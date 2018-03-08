using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using STIeForms;
using System.Drawing.Imaging;
using HRMS;
using HqWeb.Forums;
using HqWeb.GroupUpdate;

public partial class GroupUpdate_GroupUpdatePreview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadGroupUpdate();
    }

    protected void LoadActiveDivision()
    {

        string strActiveDivisionCode = "";

        if (string.IsNullOrEmpty(Request.QueryString["DivisionCode"]))
        {
            strActiveDivisionCode = "ACDMCS";
        }
        else
        {
            strActiveDivisionCode = Request.QueryString["DivisionCode"].ToString();
        }

        Response.Write(strActiveDivisionCode);
    }

    protected void LoadTab()
    {
        string strWrite = "";
        string strActiveDivisionCode = "";
        if (string.IsNullOrEmpty(Request.QueryString["DivisionCode"]))
        {
            strActiveDivisionCode = "ACDMCS";
        }
        else
        {
            strActiveDivisionCode = Request.QueryString["DivisionCode"].ToString();
        }

            strWrite = strWrite + "<li class=''><a href='#'>ACAD</a></li>";
            strWrite = strWrite + "<li class=''><a href='#'>CIS</a></li>";
            strWrite = strWrite + "<li class=''><a href='#'>CMD</a></li>";
            strWrite = strWrite + "<li class=''><a href='#'>COMM</a></li>";
            strWrite = strWrite + "<li class=''><a href='#'>COO/EVP</a></li>";
            strWrite = strWrite + "<li class=''><a href='#'>FINANCE</a></li>";
            strWrite = strWrite + "<li class=''><a href='#'>HR/LEGAL</a></li>";
            strWrite = strWrite + "<li class=''><a href='#'>OCOO/OP</a></li>";


        Response.Write(strWrite);

    }

    protected void LoadGroupUpdate()
    {
        string strGroupUpdateCode = "";
        //string strActiveDivisionCode = "";

        //if (string.IsNullOrEmpty(Request.QueryString["DivisionCode"]))
        //{
        //    strActiveDivisionCode = "ACDMCS";
        //}
        //else
        //{
        //    strActiveDivisionCode = Request.QueryString["DivisionCode"].ToString();
        //}

        //if (string.IsNullOrEmpty(Request.QueryString["GroupUpdateCode"]))
        //{
        //    strGroupUpdateCode = clsGroupUpdate.GetTopNews(strActiveDivisionCode).ToString();
        //}
        //else
        //{
        //    strGroupUpdateCode = Request.QueryString["GroupUpdateCode"].ToString();
        //}

        strGroupUpdateCode = Request.QueryString["GroupUpdateCode"].ToString();

        using (clsGroupUpdate objUpdate = new clsGroupUpdate())
        {
            //objUpdate.GroupUpdateCode = Request.QueryString["GroupUpdateCode"].ToString().ToInt();
            objUpdate.GroupUpdateCode = strGroupUpdateCode.ToInt();
            objUpdate.Fill();
            string strPhoto = objUpdate.PhotoSource != string.Empty ? "Photo By: " + objUpdate.PhotoSource : "";
            string strContributor = objUpdate.Contributor != string.Empty ? "<br/>Contributor:&nbsp" + objUpdate.Contributor + "<br/>Posted by: &nbsp;<a href='../UserPage/Userpage.aspx?username=" + objUpdate.CreateBy + "'>" + clsEmployee.GetName(objUpdate.CreateBy) + "</a>&nbsp;" : "<br/>Posted by: &nbsp;<a href='../UserPage/Userpage.aspx?username=" + objUpdate.CreateBy + "'>" + clsEmployee.GetName(objUpdate.CreateBy) + "</a>&nbsp";
            string strImage = "<a href='#'><img class='imgl' src='../UploadedFiles/GroupUpdates/" + objUpdate.ImageFilename + "' width='420' height='250' alt='' /></a></img>";
            litContent.Text += "<div class='post-title'><h2><font style='font-size:25px; font-weight:bold;'>" + objUpdate.Title + "</font></h2></div>";
            litContent.Text += "<div class='post-date'><font style='font-size:11px;'>" + objUpdate.CreateOn.ToString("dddd, MMM dd, yyyy") + strContributor + "</font></div>";
            litContent.Text += "<p><font style='font-size:12px;font-style:italic;'>" + objUpdate.Description + "</font></p>";
            litContent.Text += "<p><img src='../UploadedFiles/GroupUpdates/" + objUpdate.ImageFilename + "' alt='' class='bordered' height='200px' width='350px' /></p>";
            litContent.Text += strPhoto == string.Empty ? "" : "<font style='font-size:12px;font-style:italic;'>" + strPhoto + "</font>";
            litContent.Text += "<font style='font-size:14px;'>" + objUpdate.Content + "</font>";

            if (string.IsNullOrEmpty(objUpdate.Title))
            {
                litContent.Text = "<p>" + "No Updates for this division" + "</p>";
            }
        }
    }

    protected void LoadLatestNews()
    {
        string strWrite = "";
        //DataTable tblLatestNews = clsGroupUpdate.GetDSGLatestNews();

        //foreach (DataRow drNews in tblLatestNews.Rows)
        //{
        //    strWrite = strWrite + "<li>" +
        //                            "<div class='left'><a href='GroupUpdateView.aspx?GroupUpdateCode=" + drNews["GroupUpdateCode"].ToString() + "&DivisionCode=" + drNews["DivisionCode"].ToString() + "&DepartmentCode=" + drNews["DepartmentCode"].ToString() + "'>" + clsString.CutString(drNews["Title"].ToString(), 30) + "</a>" + "</div>" +
        //                            "<div class='right'>" + DateTime.Parse(drNews["CreateOn"].ToString()).ToString("MMM dd") + "</div>" +
        //                            "<div class='clearer'>&nbsp;</div>" +
        //                          "</li>";

        //}

        if (strWrite.Length <= 0)
        {
            strWrite = strWrite + "<li>" +
                                       "<div class='left'>No Updates</div>" +
                                       "<div class='right'></div>" +
                                       "<div class='clearer'>&nbsp;</div>" +
                                  "</li>";
        }
        else
        {
            strWrite = strWrite + "<li><a href='GroupUpdateQueryResult.aspx' class='more'>Browse all &#187;</a></li>";
        }
        Response.Write(strWrite);
    }

    protected void LoadDivisionNews()
    {
        string strWrite = "";
        string strDivisionCode = "";

        if (string.IsNullOrEmpty(Request.QueryString["DivisionCode"]))
        {
            strDivisionCode = "ACDMCS";
        }
        else
        {
            strDivisionCode = Request.QueryString["DivisionCode"].ToString();
        }

        //DataTable tblLatestNews = clsGroupUpdate.GetDSGLatestNews(strDivisionCode);

        //foreach (DataRow drNews in tblLatestNews.Rows)
        //{
        //    strWrite = strWrite + "<li>" +
        //                            "<div class='left'><a href='GroupUpdateView.aspx?GroupUpdateCode=" + drNews["GroupUpdateCode"].ToString() + "&DivisionCode=" + drNews["DivisionCode"].ToString() + "&DepartmentCode=" + drNews["DepartmentCode"].ToString() + "'>" + clsString.CutString(drNews["Title"].ToString(), 30) + "</a>" + "</div>" +
        //                            "<div class='right'>" + DateTime.Parse(drNews["CreateOn"].ToString()).ToString("MMM dd") + "</div>" +
        //                            "<div class='clearer'>&nbsp;</div>" +
        //                          "</li>";

        //}

        if (strWrite == string.Empty)
        {
            strWrite = strWrite + "<li>" +
                                        "<div class='left'>No Updates.</div>" +
                                        "<div class='right'></div>" +
                                        "<div class='clearer'>&nbsp;</div>" +
                                   "</li>";
        }
        else
        {
            strWrite = strWrite + "<li><a href='GroupUpdateQueryResult.aspx?DivisionCode=" + strDivisionCode + "' class='more'>Browse all &#187;</a></li>";
        }



        Response.Write(strWrite);
    }
}