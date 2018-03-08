using System;
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
using System.IO;
using System.Text.RegularExpressions;

using HRMS;
using HqWeb.Forums;
using HqWeb.GroupUpdate;

public partial class Userpage_HQDirectory : System.Web.UI.Page
{
    protected void LoadDDl()
    {
        ddlDivision.DataSource = clsGroupUpdate.GetDSLDivision();
        ddlDivision.DataTextField = "pText";
        ddlDivision.DataValueField = "pValue";
        ddlDivision.DataBind();
        LoadDepartment();
    }

    protected void LoadDepartment()
    {

        if (ddlDivision.SelectedValue.ToString() == "ALL")
        {
            ddlDepartment.DataSource = clsGroupUpdate.GetDSLDepartment();
            ddlDepartment.DataTextField = "pText";
            ddlDepartment.DataValueField = "pValue";
            ddlDepartment.DataBind();
        }
        else
        {
            ddlDepartment.DataSource = clsGroupUpdate.GetDSLDepartment(ddlDivision.SelectedValue.ToString());
            ddlDepartment.DataTextField = "pText";
            ddlDepartment.DataValueField = "pValue";
            ddlDepartment.DataBind();
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDDl();
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDepartment();
    }

   

    protected void LoadUsers()
    {
        string strWrite = "";
        string strDivisionCode = "";
        string strDepartmentCode = "";
        string strKeyword = "";
        string strKeywordQ = "";
        int intCtr = 0;

        if (ddlDivision.SelectedValue.ToString() != "ALL")
        {
            strDivisionCode = ddlDivision.SelectedValue.ToString();
        }
        else
        {
            strDivisionCode = "ALL";
        }

        if (ddlDepartment.SelectedValue.ToString() != "ALL")
        {
            strDepartmentCode = ddlDepartment.SelectedValue.ToString();
        }
        else
        {
            strDepartmentCode = "ALL";
        }

        if (txtKeyWord.Text != string.Empty)
        {
            strKeyword = txtKeyWord.Text;
        }


        if (strDivisionCode != "ALL")
        {
            strDivisionCode = " AND divicode='" + strDivisionCode + "'";
        }
        else
        {
            strDivisionCode = "";
        }

        if (strDepartmentCode != "ALL")
        {
            strDepartmentCode = " AND deptcode='" + strDepartmentCode + "'";
        }
        else
        {
            strDepartmentCode = "";
        }

        if (strKeyword != string.Empty)
        {
            strKeywordQ = " AND (username LIKE '%" + strKeyword + "%' OR lastname LIKE '%" + strKeyword + "%' OR firname LIKE  '%" + strKeyword + "%') ";
        }
       

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "SELECT username,lastname + ', ' + firname AS name, (SELECT CASE  WHEN lcalnmbr = '' THEN '--' ELSE lcalnmbr END) AS lcalnmbr FROM HR.Employees WHERE (pstatus = '1') " + strDivisionCode + strDepartmentCode + strKeywordQ  + " ORDER BY lastname";
            cmd.CommandText = "SELECT username,lastname + ', ' + firname AS name,nickname,position,emailofc, (SELECT CASE  WHEN lcalnmbr = '' THEN '--' ELSE lcalnmbr END) AS lcalnmbr FROM HR.Employees WHERE (pstatus = '1') " + strDivisionCode + strDepartmentCode + strKeywordQ + " ORDER BY lastname";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            string strLocalNumber = "";

            while (dr.Read())
            {

                ///////////////////////////////////////////////////////
                ///// REMOVE DUE TO HEAVY LOAD ON WEBPAGE        ////// 
                ///// CAUSE FROM OVER FETCHING DATA FROM A CLASS //////
                ///// BY: calvin cavite Feb 20, 2018             //////
                ///////////////////////////////////////////////////////
                /************************************************************************/
                //strLocalNumber = clsUsers.GetLocalNumber(dr["username"].ToString());
                /***********************************************************************/

                strLocalNumber = dr["lcalnmbr"].ToString();
                if (strLocalNumber == "")
                {
                    strLocalNumber = "--";
                }

                ///////////////////////////////////////////////////////
                ///// REMOVE DUE TO HEAVY LOAD ON WEBPAGE        ////// 
                ///// CAUSE FROM OVER FETCHING DATA FROM A CLASS //////
                ///// BY: calvin cavite Feb 20, 2018             //////
                ///////////////////////////////////////////////////////
                /************************************************************************/
                //using (clsEmployee employee = new clsEmployee())
                //{
                //    employee.Username = dr["username"].ToString();
                //    employee.Fill();


                //    strWrite = strWrite + "<tr>" +
                //                               "<td class='GridRows' style='text-align:center;'>" + "<img src='http://192.20.4.120/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='100' height='100'>" + "</td>" +
                //                               "<td class='GridRows' style='font-size:small;'><a href='../Userpage/Userpage.aspx?username=" + dr["username"].ToString() + "'>" + dr["name"] + "</a>" +
                //                               "<font  style='font-size:x-small;'><br />Nickname: " + employee.NickName +
                //                               "<br />Position: " + employee.Position +
                //                               "<br />Email: " + "<a href='mailto://" + employee.EmailOfficial + "'>" + employee.EmailOfficial + "</a></font>" +
                //                               "</td>" +
                //                               "<td class='GridRows' style='font-size:small;text-align:left; vertical-align:middle'>" + strLocalNumber + "</td>" +
                //                           "</tr>";
                //    intCtr++;
                //}
                /************************************************************************/

                strWrite = strWrite + "<tr>" +
                                           "<td class='GridRows' style='text-align:center;'>" + "<img src='http://192.20.4.120/Pictures/realpicture/" + (File.Exists(Server.MapPath("~/pictures/realpicture/") + dr["username"].ToString() + ".jpg") ? dr["username"].ToString() + ".jpg" : "default.jpg") + "' width='100' height='100'>" + "</td>" +
                                           "<td class='GridRows' style='font-size:small;'><a href='../Userpage/Userpage.aspx?username=" + dr["username"].ToString() + "'>" + dr["name"] + "</a>" +
                                           "<font  style='font-size:x-small;'><br />Nickname: " + dr["nickname"].ToString() +
                                           "<br />Position: " + dr["position"].ToString() +
                                           "<br />Email: " + "<a href='mailto://" + dr["emailofc"].ToString() + "'>" + dr["emailofc"].ToString() + "</a></font>" +
                                           "</td>" +
                                           "<td class='GridRows' style='font-size:small;text-align:left; vertical-align:middle'>" + strLocalNumber + "</td>" +
                                       "</tr>";
                intCtr++;

            }
            dr.Close();
        }
        Response.Write(strWrite);
        if (intCtr == 0)
            Response.Write("<tr><td colspan='3' class='BrowseAll'>No record found</td></tr>");
        else
            Response.Write("<tr><td colspan='3' class='BrowseAll'>[ " + intCtr + " Employee/s found ]</td></tr>");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //LoadUsers();
    }


   
}