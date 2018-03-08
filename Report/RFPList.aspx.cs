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

public partial class Report_RFPList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPage();
            LoadApprovedDisapproved();
        }
    }

    protected string writeImageLink(string pKey, string pLinkType, string pImage)
    {
        string strReturn = "";
        strReturn = "<a href='RFPDetails.aspx?ControlNumber=" + pKey + "' style='font-size:small;'><img src='../../Support/" + pImage + "' alt='' /></a>";

        return strReturn;
    }

    public string CheckLength(string pProjectTitle)
    {
        string strReturn = "";
        var intLength = 50;
        if ((pProjectTitle.Length > intLength))
        {
            strReturn = pProjectTitle.Substring(0, intLength) + "...";
        }
        else
        {
            strReturn = pProjectTitle;
        }
        return strReturn;
    }

    protected string writeLink(string pKey, string pDescription)
    {
        string strReturn = "";
        strReturn = "<a href='RFPDetails.aspx?ControlNumber=" + pKey + " ' style='font-size:small;'>" + clsString.CutString(CheckLength(pDescription), 40) + "</a>";

        return strReturn;

    }

    protected void LoadApprovedDisapproved()
    {
        string strWrite = "";
        string strKeyword = "";
        string strStatus = "";
        lblQueryResult.Visible = false;
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
            objPagination.TotalCount = clsEFormsReport.RFPCountArchives(username, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), strStatus, strKeyword);

            DataTable tblResult = clsEFormsReport.RFPDSGArchives(username, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), strStatus, objPagination.PagePerForm, objPagination.Offset(), strKeyword);
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
                strWrite = strWrite + "<tr><td class='GridRows'>No record found</td></tr>";
            else
                strWrite = strWrite + "<tr><td class='GridRows'>[ Showing results " + intFirstItem + " to " + intLastItem + " of " + objPagination.TotalCount + " ]</td></tr>";

            foreach (DataRow drw in tblResult.Rows)
            {
                strWrite = strWrite + "<tr>" +
                                               "<td class='GridRows'>" +
                                                    "<a href='../Finance/RFP/RFPDetails.aspx?ControlNumber=" + drw["ctrlnmbr"].ToString() + "&Username=" + username + "'  style='font-size:medium;' target='_blank'>" + drw["ctrlnmbr"].ToString() + "</a><br/><font style='normal: small; font-weight: normal;'><b>Request For:</b> " + drw["rqstfor"].ToString() + "</font><br/><font style='normal: small; font-weight: normal;'><b>Project Title:</b> " + drw["projttle"].ToString() + 
                                                    "<font style='font-size:normal; font-weight: normal;'><b><br/>Submitted By:</b>&nbsp;<a href='../../../Userpage/UserPage.aspx?username=" + drw["createby"] + "'>" + clsEmployee.GetName(drw["createby"].ToString()) + "</a> (" + Convert.ToDateTime(drw["createon"].ToString()).ToString("MMM dd, yyyy hh:mm tt") + ")" + 
                                                    "<br/><b>Status:</b> " + clsEFormsReport.RFPStatus(username,drw["ctrlnmbr"].ToString())  + "</font>" +
                                                "</td>" +
                                      "</tr>";
            }
            lblQueryResult.Text = strWrite;


        }
    }

    protected void LoadPage()
    {
        string strWrite = "";
        string strKeyword = "";
        lblQueryResult.Visible = false;
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
            objPagination.TotalCount = clsEFormsReport.RFPCountArchives(username, DateTime.Parse(dtStartDate.ToShortDateString() + " 1:00 AM"), DateTime.Parse(dtEndDate.ToShortDateString() + " 11:59 PM"), Request.QueryString["Status"].ToString(), strKeyword);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequestFormMain.aspx");
    }
    protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadApprovedDisapproved();
    }
}