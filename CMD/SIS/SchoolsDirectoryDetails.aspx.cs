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

public partial class CMD_SIS_SchoolsDirectoryDetails : System.Web.UI.Page
{

	protected void Load_Programs()
	{
        //string strWrite = "";
        //using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
        //{
        //    SqlCommand cmdOmega = cnOmega.CreateCommand();
        //    cmdOmega.CommandText = "SELECT DISTINCT dbo_course.course_code, course_name FROM dbo_pmatrix_cur INNER JOIN dbo_course ON dbo_pmatrix_cur.course_code = dbo_course.course_code WHERE branch_code='" + Request.QueryString["schlcode"] + "' AND ind_active = 'A' ORDER BY course_name";
        //    cnOmega.Open();
        //    SqlDataReader drOmega = cmdOmega.ExecuteReader();
        //    while (drOmega.Read())
        //    {
        //        strWrite += "<tr>" +
        //                                                        "<td class='GridRows' style='width:20px; text-align:center;'><img src='../../Support/attach16.png'></td>" +
        //                                                        "<td class='GridRows'><a href=''>" + drOmega["course_name"] + "</a></td>" +
        //                                                    "</tr>";				
        //    }
        //    drOmega.Close();
        //}
        //Response.Write(strWrite);
        

        string strWrite = "";

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["WireV2"].ToString()))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT Name FROM Programs WHERE ProgramCode IN (SELECT ProgramCode FROM SchoolProgram WHERE PeriodId='" + GetWirePeriod() + "' AND SchoolCode='" + Request.QueryString["schlcode"] + "' AND IsOfficial='1') AND IsOfficial='1' ORDER BY Name";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strWrite += "<tr>" +
                                                                    "<td class='GridRows' style='width:20px; text-align:center;'><img src='../../Support/attach16.png'></td>" +
                                                                    "<td class='GridRows'>" + dr["Name"] + "</td>" +
                                                                "</tr>";
                }
                cn.Close();
            }
        }

        Response.Write(strWrite);
	}

    public int GetWirePeriod()
    {
        int intReturn = 0;
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='PeriodID'";
                cn.Open();
                intReturn = cmd.ExecuteScalar().ToString().ToInt();
            }
        }
        return intReturn;
    }

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    clsSpeedo.Authenticate();

    //    if (!Page.IsPostBack)
    //    {
    //        string strCM = "";
    //        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
    //        {
    //            SqlCommand cmd = cn.CreateCommand();
    //            cmd.CommandText = "SELECT ceoname,cooname,schladdr,faxnmbr,telnmbr,cmname,acctname FROM CM.Schools WHERE schlcode='" + Request.QueryString["schlcode"] + "'";
    //            cn.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            dr.Read();
    //            lblCEO.Text = dr["ceoname"].ToString();
    //            lblCOO.Text = dr["cooname"].ToString();
    //            lblAddress.Text = dr["schladdr"].ToString();
    //            lblFaxNmbr.Text = dr["faxnmbr"].ToString();
    //            lblTelNmbr.Text = dr["telnmbr"].ToString();
    //            lblAccountant.Text = dr["acctname"].ToString();
    //            strCM = dr["cmname"].ToString();
    //            dr.Close();

    //            cmd.CommandText = "SELECT firname + ' ' + lastname AS pname FROM Users.Users WHERE username='" + strCM + "'";
    //            dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //                lblCM.Text = dr["pname"].ToString();
    //            dr.Close();
    //        }
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            string strCM = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ceoname,cooname,schladdr,faxnmbr,telnmbr,cmname,acctname, hqowned FROM CM.Schools WHERE schlcode='" + Request.QueryString["schlcode"] + "'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                //Added by Charlie Bachiller 10/04/2011
                if (dr["hqowned"].ToString() == "0")
                {
                    lblCEOLabel.Text = "President:";
                    lblCOOLabel.Text = "School Administrator:";
                    lblCMLabel.Text = "SOM:";
                    
                }
                else
                {
                    lblCEOLabel.Text = "School Administrator:";
                    lblCOOLabel.Text = "Deputy School Administrator:";
                    lblCMLabel.Text = "SSA:";
                }

                lblCEO.Text = dr["ceoname"].ToString();
                lblCOO.Text = dr["cooname"].ToString();
                lblAddress.Text = dr["schladdr"].ToString();
                lblFaxNmbr.Text = dr["faxnmbr"].ToString();
                lblTelNmbr.Text = dr["telnmbr"].ToString();
                lblAccountant.Text = dr["acctname"].ToString();
                strCM = dr["cmname"].ToString();
                dr.Close();

                cmd.CommandText = "SELECT firname + ' ' + lastname AS pname FROM Users.Users WHERE username='" + strCM + "'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    lblCM.Text = dr["pname"].ToString();
                dr.Close();
            }
        }
    }

    protected void btnCurriculumOutline_Click(object sender, EventArgs e)
	{
		Response.Redirect("CurriculumOutline.aspx?schlcode=" + Request.QueryString["schlcode"] + "&schlname=" + Request.QueryString["schlname"]);
	}

}
