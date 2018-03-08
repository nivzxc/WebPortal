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

public partial class Academics_CoursewareView : System.Web.UI.Page
{

	protected void Load_Curriculum()
	{
		string strWrite = "";
		using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
		{
			SqlCommand cmdOmega = cnOmega.CreateCommand();
			cmdOmega.CommandText = "SELECT dbo_mcurriculum.cur_ref_no, course_name FROM dbo_mcurriculum_aux INNER JOIN (dbo_mcurriculum INNER JOIN dbo_course ON dbo_mcurriculum.course_code = dbo_course.course_code) ON dbo_mcurriculum_aux.cur_ref_no = dbo_mcurriculum.cur_ref_no WHERE dbo_mcurriculum_aux.subject_code = '" + Request.QueryString["subjcode"] + "' ORDER BY course_name";
			cnOmega.Open();
			SqlDataReader drOmega = cmdOmega.ExecuteReader();
			while (drOmega.Read())
			{
				strWrite = "<tr>" +
																"<td class='GridRows'>" + drOmega["cur_ref_no"] + "</td>" +
																"<td class='GridRows'>" + drOmega["course_name"] + "</td>" +
															"</tr>";
				Response.Write(strWrite);
			}
			drOmega.Close();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
 {

		if (!Page.IsPostBack)
		{
			string strSubjCode = "";
			string strSubjDesc = "";

			using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
			{
				SqlCommand cmdOmega = cnOmega.CreateCommand();
				cmdOmega.CommandText = "SELECT subject_name,lec_units,subject_category,subject_description FROM dbo_subject WHERE subject_code='" + Request.QueryString["subjcode"] + "'";
				cnOmega.Open();
				SqlDataReader drOmega = cmdOmega.ExecuteReader();
				if (drOmega.Read())
				{
					txtCourseCode.Text = Request.QueryString["subjcode"].ToString();
					txtCourseName.Text = drOmega["subject_name"].ToString().Trim();
					txtUnits.Text = drOmega["lec_units"].ToString().Trim();
					strSubjCode = drOmega["subject_category"].ToString();
					strSubjDesc = drOmega["subject_description"].ToString();
				}
				drOmega.Close();

				cmdOmega.CommandText = "SELECT desc_type FROM dbo_types WHERE type='" + strSubjCode + "'";
				drOmega = cmdOmega.ExecuteReader();
				if (drOmega.Read())
					txtUnitClass.Text = drOmega["desc_type"].ToString();
				drOmega.Close();

				cmdOmega.CommandText = "SELECT TOP 1 datos FROM dbo_control WHERE clave='" + strSubjDesc + "' ORDER BY d_from DESC";
				drOmega = cmdOmega.ExecuteReader();
				if (drOmega.Read())
					txtCourseDesc.Text = drOmega["datos"].ToString();
				drOmega.Close();
			}

			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandText = "SELECT cwdstat,datecomp,modidate,moduname,remarks FROM Academics.CoursewareInventory WHERE crsecode='" + Request.QueryString["subjcode"] + "'";
				cn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if (dr.Read())
				{
					ddlCWA.SelectedValue = dr["cwdstat"].ToString();
					if (dr["cwdstat"].ToString() == "C")
					{
						lblDateComp.Visible = true;
						dteCompletion.Visible = true;
						dteCompletion.Date = Convert.ToDateTime(dr["datecomp"].ToString());
					}
					txtRemarks.Text = dr["remarks"].ToString();
					txtUpdateBy.Text = dr["modidate"].ToString();
					txtUpdateDate.Text = dr["moduname"].ToString();
				}
				dr.Close();
			}
		}
 }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		if (ddlCWA.SelectedValue != "X")
		{
			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandText = "SELECT crsecode FROM Academics.CoursewareInventory WHERE crsecode='" + Request.QueryString["subjcode"] + "'";
				cn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				bool blnHasRecord = dr.Read();
				dr.Close();
				if (blnHasRecord)
				{
					cmd.CommandText = "UPDATE Academics.CoursewareInventory SET cwdstat='" + ddlCWA.SelectedValue + "',datecomp='" + dteCompletion.Date.ToShortDateString() + "',modidate='" + DateTime.Now + "',moduname='" + Request.Cookies["Speedo"]["UserName"] + "',remarks=@remarks WHERE crsecode='" + Request.QueryString["subjcode"] + "'";
					cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 255);
					cmd.Parameters["@remarks"].Value = txtRemarks.Text;
				}
				else
				{
					cmd.CommandText = "INSERT INTO Academics.CoursewareInventory VALUES('" + Request.QueryString["subjcode"] + "','" + ddlCWA.SelectedValue + "','" + (ddlCWA.SelectedValue == "C" ? dteCompletion.Date.ToShortDateString() : "") + "',@remarks,'" + DateTime.Now + "','" + Request.Cookies["Speedo"]["UserName"] + "')";
					cmd.Parameters.Add("@remarks", SqlDbType.VarChar, 255);
					cmd.Parameters["@remarks"].Value = txtRemarks.Text;
				}
				cmd.ExecuteNonQuery();
			}
		}
	}

	protected void ddlCWA_SelectedIndexChanged(object sender, EventArgs e)
	{
		lblDateComp.Visible = (ddlCWA.SelectedValue == "C" ? true : false);
		dteCompletion.Visible = lblDateComp.Visible;
 }

}
