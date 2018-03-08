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

public partial class CMD_SIS_CurriculumOutline : System.Web.UI.Page
{

	protected void LoadRecords()
	{
		DataTable tblYearTerm = new DataTable();
		string strWrite = "";

		SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString());
		SqlDataReader drOmega;
		SqlCommand cmdOmega = cnOmega.CreateCommand();
		cmdOmega.CommandText = "SELECT DISTINCT dbo_mcurriculum_aux.year_level,dbo_mcurriculum_aux.term FROM dbo_mcurriculum_aux INNER JOIN dbo_pmatrix_cur ON dbo_mcurriculum_aux.cur_ref_no = dbo_pmatrix_cur.cur_ref_no WHERE course_code='" + ddlProgram.SelectedValue + "' ORDER BY dbo_mcurriculum_aux.year_level,dbo_mcurriculum_aux.term";
		SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
		cnOmega.Open();
		daOmega.Fill(tblYearTerm);
		foreach (DataRow drow in tblYearTerm.Rows)
		{
			strWrite = strWrite + "<br><div class='GridBorder'>" +
																										"<table width='100%' cellpadding='5' class='grid'>" +
																											"<tr>" +
                                                                                                                "<td class='' colspan='5'>" +
																													"<table>" +
																														"<tr>" +
																															"<td style='width:10px'><img src='../../Support/attach16.png'></td>" +
																															"<td>Year Level: " + drow["year_level"] + "  Term: " + drow["term"] + "</td>" +
																														"</tr>" +
																													"</table>" +
																												"</td>" +
																											"</tr>" +
																											"<tr>" +
																												"<td class='GridColumns' style='width:20%;'><b>Course Code</b></td>" +
																												"<td class='GridColumns' style='width:80%;'><b>Course Title</b></td>" +
																											"</tr>";
			cmdOmega.CommandText = "SELECT DISTINCT dbo_subject.subject_code,subject_name FROM dbo_mcurriculum_aux INNER JOIN dbo_subject ON dbo_mcurriculum_aux.subject_code = dbo_subject.subject_code WHERE cur_ref_no='" + ddlCurriculum.SelectedValue + "' AND dbo_mcurriculum_aux.year_level='" + drow["year_level"] + "' AND dbo_mcurriculum_aux.term='" + drow["term"] + "' ORDER BY dbo_subject.subject_code";
			drOmega = cmdOmega.ExecuteReader();
			while (drOmega.Read())
			{
				strWrite = strWrite + "<tr>" +
																											"<td class='GridRows'>" + drOmega["subject_code"] + "</td>" +
																											"<td class='GridRows'><a href=''>" + drOmega["subject_name"] + "</a></td>" +
																										"</tr>";
			}
			strWrite = strWrite + "</table></div>";
			drOmega.Close();
		}
		cnOmega.Close();

		tblYearTerm.Clear();
		Response.Write(strWrite);
	}

    protected DataTable Load_Programs()
    {
        DataTable tblReturn = new DataTable();

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["WireV2"].ToString()))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT ProgramCode, Name FROM Programs WHERE ProgramCode IN (SELECT ProgramCode FROM SchoolProgram WHERE PeriodId='" + GetWirePeriod() + "' AND SchoolCode='" + Request.QueryString["schlcode"] + "' AND IsOfficial='1') AND IsOfficial='1' ORDER BY Name";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
                cn.Close();
            }
        }

        return tblReturn;
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

    public string GetProgramName(string pProgramCode)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["WireV2"].ToString()))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT Name FROM Programs WHERE ProgramCode=@pProgramCode";
                cmd.Parameters.Add(new SqlParameter("@pProgramCode", pProgramCode));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
        }
        return strReturn;
    }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
  btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
		if (!Page.IsPostBack)
		{
			DataTable tblPrograms = new DataTable();
			DataTable tblCurriculum = new DataTable();
			using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
			{
				SqlCommand cmdOmega = cnOmega.CreateCommand();
                //cmdOmega.CommandText = "SELECT DISTINCT dbo_course.course_code, course_name FROM dbo_pmatrix_cur INNER JOIN dbo_course ON dbo_pmatrix_cur.course_code = dbo_course.course_code WHERE branch_code='" + Request.QueryString["SchlCode"] + "' AND ind_active = 'A' ORDER BY course_name";
                //SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
                //cnOmega.Open();
                //daOmega.Fill(tblPrograms);
                ddlProgram.DataSource = Load_Programs();
                ddlProgram.DataTextField = "ProgramCode";
                ddlProgram.DataValueField = "ProgramCode";
				ddlProgram.DataBind();

                SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
                cnOmega.Open();
				cmdOmega.CommandText = "SELECT DISTINCT cur_ref_no FROM dbo_pmatrix_cur WHERE branch_code='" + Request.QueryString["SchlCode"] + "' AND ind_active='A' AND course_code='" + ddlProgram.Items[0].Value + "'";
				daOmega.SelectCommand = cmdOmega;
				daOmega.Fill(tblCurriculum);
				ddlCurriculum.DataSource = tblCurriculum;
				ddlCurriculum.DataTextField = "cur_ref_no";
				ddlCurriculum.DataValueField = "cur_ref_no";
				ddlCurriculum.DataBind();
			}

		}
        lblCourseName.Text = GetProgramName(ddlProgram.SelectedValue.ToString());
 }

	protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
	{
		using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
		{
			DataTable tblCurriculum = new DataTable();
			SqlCommand cmdOmega = cnOmega.CreateCommand();
			cmdOmega.CommandText = "SELECT DISTINCT cur_ref_no FROM dbo_pmatrix_cur WHERE branch_code='" + Request.QueryString["SchlCode"] + "' AND ind_active='A' AND course_code='" + ddlProgram.SelectedValue + "'";
			SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega);
			daOmega.Fill(tblCurriculum);
			ddlCurriculum.DataSource = tblCurriculum;
			ddlCurriculum.DataTextField = "cur_ref_no";
			ddlCurriculum.DataValueField = "cur_ref_no";
			ddlCurriculum.DataBind();
		}

        lblCourseName.Text = GetProgramName(ddlProgram.SelectedValue.ToString());
	}

	protected void ddlCurriculum_SelectedIndexChanged(object sender, EventArgs e)
	{

	}

    protected void btnBack_Click(object sender, EventArgs e)
    {
       
    }
}
