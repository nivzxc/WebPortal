using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;
using STIeForms;
using HqWeb.GroupUpdate;

public partial class Report_LeaveList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPage();
            LoadLeave();
        }
    }
    protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLeave();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
         Response.Redirect("RequestFormMain.aspx");
    }
    public void LoadLeave()
    {
        string strWrite = "";
        string strKeyword = "";
        string strStatus = "";
        lblQueryResult.Visible = false;
        lblQueryResult.Text = "";
        DateTime dtStartDate = new DateTime();
        DateTime dtEndDate = new DateTime();
        string username = Request.Cookies["Speedo"]["UserName"];

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

        if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
        {
            strStatus = Request.QueryString["Status"].ToString();
        }
        else
        {
            strStatus = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Keyword"]))
        {
            strKeyword = Request.QueryString["Keyword"].ToString();
        }
        else
        {
            strKeyword = "";
        }
        using (clsPagination objPagination = new clsPagination())
        {
            objPagination.CurrentPage = ddlPages.SelectedValue.ToString().ToInt();
            objPagination.PagePerForm = 10;
            objPagination.TotalCount = clsEFormsReport.GetLeave(dtStartDate, dtEndDate, strKeyword, strStatus,username).Rows.Count;

            DataTable tblResult = clsEFormsReport.GetLeave(dtStartDate, dtEndDate, strKeyword, strStatus,username);
            int intFirstItem = 0;
            int intLastItem = 0;


            int intStart = 0;
            int intEnd = 0;

            if (tblResult.Rows.Count > 0)
            {
                intFirstItem = 0;
                intLastItem = 0;
                lblQueryResult.Visible = true;

                if (ddlPages.SelectedItem.Text.ToInt() == 1)
                {
                    intStart = 0;
                    intFirstItem = intStart + 1;
                    intLastItem = intStart + tblResult.Rows.Count;
                    intEnd = objPagination.PagePerForm * ddlPages.SelectedItem.Text.ToInt();

                }
                else
                {

                    intStart = objPagination.PagePerForm * ddlPages.SelectedItem.Text.ToInt() - objPagination.PagePerForm;
                    intFirstItem = intStart + 1;
                    if (ddlPages.SelectedItem.Text == ddlPages.Items[ddlPages.Items.Count - 1].Text)
                    {
                        intLastItem = tblResult.Rows.Count;
                    }

                    else
                    {
                        intLastItem = intStart + 10;
                    }

                    intEnd = objPagination.PagePerForm * ddlPages.SelectedItem.Text.ToInt();
                }

            }
            if (tblResult.Rows.Count == 0)
                strWrite = strWrite + "<tr><td class='GridRows'>No record found</td></tr>";
            else
                strWrite = strWrite + "<tr><td class='GridRows'>[ Showing results " + intFirstItem + " to " + intLastItem + " of " + objPagination.TotalCount + " ]</td></tr>";
            lblPage.Text = strWrite;
            foreach (DataRow dr in clsEFormsReport.GetLeave(dtStartDate, dtEndDate, strKeyword, strStatus,username).Rows)
            {
                if (intStart == intEnd) { break; }
                if (intStart == tblResult.Rows.Count) { break; }
                lblQueryResult.Visible = true;
                strWrite = "<tr><td class='GridRows'><font style='font-size:normal; font-weight: normal;'><a style='font-size:medium; font-weight:bold;' href='LeaveDetailsReport.aspx?leavcode=" + dr.Table.Rows[intStart]["leavcode"].ToString() + "'>" + dr.Table.Rows[intStart]["leavcode"].ToString() + "</a>" +
                          "<br><b>Submitted by: </b><a href='../Userpage/UserPage.aspx?username=" + dr.Table.Rows[intStart]["username"].ToString() + "'>" + clsEmployee.GetName(dr["username"].ToString()) + "</a></br>" +
                           "<b>Reason: </b>" + clsString.CutString(dr.Table.Rows[intStart]["reason"].ToString(), 80).ToString() +
                           "<br><b>Approver: </b> <a href='../Userpage/UserPage.aspx?username=" + dr.Table.Rows[intStart]["username"].ToString() + "'>" + clsEmployee.GetName(dr["apphname"].ToString()) + "</a></br>" +
                           "<b>Date Filed: </b>" + Convert.ToDateTime(dr.Table.Rows[intStart]["DateFiled"].ToString()).ToString("MMM dd, yyyy hh:mm tt")  + "</font></td></tr>";
                intStart++;
                lblQueryResult.Text += strWrite;

            }

        }
    }
    public void LoadPage()
    {
        string strWrite = "";
        string strKeyword = "";
        lblQueryResult.Text = "";
        lblQueryResult.Visible = false;
        DateTime dtStartDate = new DateTime();
        DateTime dtEndDate = new DateTime();
        string strStatus = "";
        string username = Request.Cookies["Speedo"]["UserName"];

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

        if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
        {
            strStatus = Request.QueryString["Status"].ToString();
        }
        else
        {
            strStatus = "ALL";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Keyword"]))
        {
            strKeyword = Request.QueryString["Keyword"].ToString();
        }
        else
        {
            strKeyword = "";
        }


        using (clsPagination objPagination = new clsPagination())
        {
            objPagination.CurrentPage = string.IsNullOrEmpty(Request.QueryString["Page"]) == true ? 1 : Request.QueryString["Page"].ToInt();
            objPagination.PagePerForm = 10;
            objPagination.TotalCount = clsEFormsReport.GetLeave(dtStartDate, dtEndDate, strKeyword, strStatus,username).Rows.Count;
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
}