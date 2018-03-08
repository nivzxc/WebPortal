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

public partial class GroupUpdate_GroupUpdateQueryResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnBack.Attributes.Add("onclick", "history.back(); return false");
        if (!Page.IsPostBack)
        {
            LoadPage();
            LoadFilter();
        }
    }

    protected void LoadFilter()
    {
        string strWrite = "";
        lblQueryResult.Visible = false;
        DateTime dtStartDate = new DateTime();
        DateTime dtEndDate = new DateTime();
        string strDivisionCode = "";
        string strDepartmentCode = "";
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

        if (!string.IsNullOrEmpty(Request.QueryString["DivisionCode"]))
        {
            strDivisionCode = Request.QueryString["DivisionCode"].ToString();
        }
        else
        {
            strDivisionCode = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["DepartmentCode"]))
        {
            strDepartmentCode = Request.QueryString["DepartmentCode"].ToString();
        }
        else
        {
            strDepartmentCode = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Keyword"]))
        {
            strKeyword = Request.QueryString["Keyword"].ToString();
        }



        using (clsPagination objPagination = new clsPagination())
        {
           // objPagination.CurrentPage = string.IsNullOrEmpty(Request.QueryString["Page"]) == true ? 1 : Request.QueryString["Page"].ToInt();
            objPagination.CurrentPage = ddlPages.SelectedValue.ToInt();
            objPagination.PagePerForm = 10;
            objPagination.TotalCount = clsGroupUpdate.GetDSGArchives(strDivisionCode, strDepartmentCode, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), strKeyword).Rows.Count;
            
                
            DataTable tblResult = clsGroupUpdate.GetDSGArchives(strDivisionCode, strDepartmentCode, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), objPagination.PagePerForm, objPagination.Offset(), strKeyword);
            int intFirstItem = 0;
            int intLastItem = 0;

            


            if (tblResult.Rows.Count > 0)
            {
                DataRow drFirstRow = tblResult.Rows[0];
                intFirstItem = drFirstRow["RowNum"].ToString().ToInt();
                DataRow drLastRow = tblResult.Rows[tblResult.Rows.Count - 1];
                intLastItem = drLastRow["RowNum"].ToString().ToInt();

                lblQueryResult.Visible = true;
            }

            if (tblResult.Rows.Count == 0)
            {
                strWrite = strWrite + "<tr><td class='GridRows'>No record found</td></tr>";
            }
            else
            {
                strWrite = strWrite + "<tr><td class='GridRows'>[ Showing results " + intFirstItem + " to " + intLastItem + " of " + objPagination.TotalCount + " ]</td></tr>";

                foreach (DataRow drw in tblResult.Rows)
                {
                    strWrite = strWrite + "<tr>" +
                                                   "<td class='GridRows'>" +
                                                        "<a href='GroupUpdateView.aspx?GroupUpdateCode=" + drw["GroupUpdateCode"].ToString() + "&DivisionCode=" + drw["DivisionCode"].ToString() + "&DepartmentCode=" + drw["DepartmentCode"].ToString() + "'  style='font-size:medium;' target='_blank'>" + clsGroupUpdate.GetTitle(drw["GroupUpdateCode"].ToString().ToInt()) + "</a><br/>" +
                                                        "<font style='font-size: x-small; font-weight: normal; font-style: italic'>Contributor:&nbsp;" + drw["Contributor"].ToString() + "<br/>Posted By:&nbsp;<a href='../../../Userpage/UserPage.aspx?username=" + drw["CreateBy"] + "'>" + clsEmployee.GetName(drw["CreateBy"].ToString()) + "</a> (" + Convert.ToDateTime(drw["CreateOn"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")<br/>" +
                                                        clsGroupUpdate.GetDescription(drw["GroupUpdateCode"].ToString().ToInt()) +
                                                    "</td>" +
                                          "</tr>";
                }
            }
            lblQueryResult.Text = strWrite;


        }
    }

    protected void LoadPage()
    {
        string strWrite = "";
        DateTime dtStartDate = new DateTime();
        DateTime dtEndDate = new DateTime();
        string strDivisionCode = "";
        string strDepartmentCode = "";
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

        if (!string.IsNullOrEmpty(Request.QueryString["DivisionCode"]))
        {
            strDivisionCode = Request.QueryString["DivisionCode"].ToString();
        }
        else
        {
            strDivisionCode = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["DepartmentCode"]))
        {
            strDepartmentCode = Request.QueryString["DepartmentCode"].ToString();
        }
        else
        {
            strDepartmentCode = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Keyword"]))
        {
            strKeyword = Request.QueryString["Keyword"].ToString();
        }

        using (clsPagination objPagination = new clsPagination())
        {
            objPagination.CurrentPage = string.IsNullOrEmpty(Request.QueryString["Page"]) == true ? 1 : Request.QueryString["Page"].ToInt();
            objPagination.PagePerForm = 10;
            objPagination.TotalCount = clsGroupUpdate.GetDSGArchives(strDivisionCode, strDepartmentCode, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), strKeyword).Rows.Count;
            string strDateStart = dtStartDate.ToString("MMMddyyyy");
            string strDateEnd = dtEndDate.ToString("MMMddyyyy");

            ddlPages.Items.Clear();
            for (int i = 1; i <= objPagination.TotalPage(); i++)
            {
                ListItem l = new ListItem(i.ToString(), i.ToString(), true);
                l.Selected = l.Value == objPagination.CurrentPage.ToString() ? true : false;
                ddlPages.Items.Add(l);
            }

        }

        lblPage.Text = strWrite;

    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("GroupUpdateView.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupUpdateQuery.aspx");
    }

    protected void btnBack_Click1(object sender, EventArgs e)
    {
        Response.Redirect("GroupUpdateView.aspx");
    }
    protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFilter();
    }
}