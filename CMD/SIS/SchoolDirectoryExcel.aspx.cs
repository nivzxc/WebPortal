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
using System.Text.RegularExpressions;

public partial class CMD_SIS_SchoolDirectoryExcel : System.Web.UI.Page
{
    protected void LoadHQOwned()
    { 
    string strWhere = "";
    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
    {
        using (SqlCommand cmd = cn.CreateCommand())
        {
            cmd.CommandText = "SELECT schlcode, schlname, schlnam2, schladdr, ceoname, cooname,  (SELECT     lastname + ', ' + firname FROM HR.Employees WHERE  username = CM.Schools.cmname) AS cmname, telnmbr, faxnmbr FROM  CM.Schools WHERE     (hqowned = '1') AND (pstatus = '1') ORDER BY schlname";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWhere += "<tr>" +
                                "<td>" + dr["schlname"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                "<td>" + dr["schlnam2"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                "<td>" + dr["schladdr"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                "<td>" + dr["ceoname"].ToString().Replace("ñ", "&#x148;") + "</td>" +
                                "<td>" + dr["cooname"].ToString().Replace("ñ", "&#x148;") + "</td>" +
                                "<td>" + dr["cmname"].ToString().Replace("ñ", "&#x148;") + "</td>" +
                                "<td>" + dr["telnmbr"].ToString() + "</td>" +
                                "<td>" + dr["faxnmbr"].ToString() + "</td>" +
                            "</tr>";
            }
        }
    }
    Response.Write(strWhere);
    }

    protected void LoadFranchise()
    {
        string strWhere = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT schlcode, schlname, schlnam2, schladdr, ceoname, cooname,  (SELECT     lastname + ', ' + firname FROM HR.Employees WHERE  username = CM.Schools.cmname) AS cmname, telnmbr, faxnmbr FROM  CM.Schools WHERE     (hqowned = '0') AND (pstatus = '1') ORDER BY schlname";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strWhere += "<tr>" +
                                    "<td>" + dr["schlname"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                    "<td>" + dr["schlnam2"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                    "<td>" + dr["schladdr"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                    "<td>" + dr["ceoname"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                    "<td>" + dr["cooname"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                    "<td>" + dr["cmname"].ToString().Replace("ñ", "&#x148;").Replace("Ñ", "&#xD1;") + "</td>" +
                                    "<td>" + dr["telnmbr"].ToString() + "</td>" +
                                    "<td>" + dr["faxnmbr"].ToString() + "</td>" +
                                "</tr>";
                }
            }
        }
        Response.Write(strWhere);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblDate.Text = "List of Active School as of: " + DateTime.Now.ToString("MMMM dd, yyyy");
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=STISchoolDirectory-" + DateTime.Now.ToString("MM-dd-yy") + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
    }
}