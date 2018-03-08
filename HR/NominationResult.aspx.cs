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

public partial class HR_NominationResult : System.Web.UI.Page
{

	protected bool IsAllowed(string strEmpNum, string strMidName)
	{
		if ((strEmpNum == "51492" && strMidName.ToLower() == "dizon") || (strEmpNum == "01328" && strMidName.ToLower() == "nogales") || (strEmpNum == "48988" && strMidName.ToLower() == "bautista") || (strEmpNum == "64704" && strMidName.ToLower() == "bustamante") || (strEmpNum == "51953" && strMidName.ToLower() == "kho"))
			return true;
		else
			return false;
	}

	protected void LoadRecords()
	{
		try
		{
			if (Session["allowed"].ToString() == "1")
			{
				string strWrite = "";
				DataTable tblAttributes = new DataTable();
				using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
				{
					string strDiviCode;
					try
					{
						strDiviCode = Request.QueryString["divicode"].ToString();
					}
					catch
					{
						strDiviCode = "CIS";
					}
					SqlCommand cmd = cn.CreateCommand();
					cmd.CommandText = "SELECT * FROM attributes ORDER BY attrname";
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					SqlDataReader dr;
					cn.Open();
					da.Fill(tblAttributes);
					foreach (DataRow drow in tblAttributes.Rows)
					{
						strWrite = "<br /><br />" +
																	"<div class='GridBorder' style='width:40%'>" +
																		"<table width='100%' class='grid'>" +
																			"<tr>" +
																				"<td class='GridColumns' style='width:200px;'><b>Employee</b></td>" +
																				"<td class='GridColumns'><b>" + drow["attrname"] + "</b></td>" +
																			"</tr>";
						Response.Write(strWrite);
						if (strDiviCode == "ALL")
						{
							if (Request.QueryString["filter"].ToString() == "3")
							{
								cmd.CommandText = "SELECT TOP 3 users.username,firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
																										"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																										"WHERE attrcode='" + drow["attrcode"] + "' " +
																										"GROUP BY users.username,firname,lastname,divicode ORDER BY tvote DESC";
							}
							else
							{
								cmd.CommandText = "SELECT users.username,firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
																										"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																										"WHERE attrcode='" + drow["attrcode"] + "' " +
																										"GROUP BY users.username,firname,lastname,divicode ORDER BY tvote DESC";
							}
						}
						else
						{
							if (Request.QueryString["filter"].ToString() == "3")
							{
								cmd.CommandText = "SELECT TOP 3 users.username,firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
																										"FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																										"WHERE divicode='" + strDiviCode + "' AND attrcode='" + drow["attrcode"] + "' " +
																										"GROUP BY users.username,firname,lastname,divicode ORDER BY tvote DESC";
							}
							else
							{
								cmd.CommandText = "SELECT users.username,firname + ' ' + lastname AS wname,divicode,COUNT(nomuname) AS tvote " +
									                 "FROM users_division INNER JOIN (users INNER JOIN awards_votes ON users.username = awards_votes.nomuname) ON users_division.username = users.username " +
																										"WHERE divicode='" + strDiviCode + "' AND attrcode='" + drow["attrcode"] + "' " +
																										"GROUP BY users.username,firname,lastname,divicode ORDER BY tvote DESC";
							}
						}
						dr = cmd.ExecuteReader();
						while (dr.Read())
						{
							Response.Write("<tr><td class='GridRows'><a href='NominationVotee.aspx?username=" + dr["username"] + "' target='_blank'>" + dr["wname"] + "</a> <i>" + dr["divicode"] + "</i></td><td class='GridRows' style='text-align:center'>" + dr["tvote"] + "</td></tr>");
						}
						dr.Close();
						Response.Write("</table></div>");
					}
				}
			}
			else
			{
				trTally.Visible = false;
			}
		}
		catch
		{
		}
	}

 protected void Page_Load(object sender, EventArgs e)
 {
		try
		{
			if (Session["allowed"].ToString() == "1")
			{
				trValidation.Visible = false;
				trTally.Visible = true;
			}
		}
		catch
		{
		}

			if (!Page.IsPostBack)
			{
				chkTop3.Checked = (Request.QueryString["filter"].ToString() == "3" ? true : false);

				DataTable tblDivision = new DataTable();
				using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
				{
					SqlCommand cmd = cn.CreateCommand();
					cmd.CommandText = "SELECT * FROM division ORDER BY divicode";
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					cn.Open();
					da.Fill(tblDivision);
				}
				ddlDivision.DataSource = tblDivision;
				ddlDivision.DataValueField = "divicode";
				ddlDivision.DataTextField = "division";
				ddlDivision.DataBind();
				ListItem itm = new ListItem();
				itm.Text = "All Division";
				itm.Value = "ALL";
				ddlDivision.Items.Add(itm);

				foreach (ListItem itmP in ddlDivision.Items)
				{
					if (itmP.Value == Request.QueryString["divicode"].ToString())
						itmP.Selected = true;
				}
			}


 }

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		Response.Redirect("NominationResult.aspx?divicode=" + ddlDivision.SelectedValue + "&filter=" + (chkTop3.Checked ? "3" : "0"));
	}

	protected void btnValidate_Click(object sender, ImageClickEventArgs e)
	{
		if (IsAllowed(txtEmpNum.Text, txtMidName.Text))
		{
			Session["allowed"] = "1";
			trValidation.Visible = false;
			trTally.Visible = true;
		}
		else
		{
			Session["allowed"] = "0";
			trValidation.Visible = true;
			trTally.Visible = false;
		}
	}

	protected void btnDownload_Click(object sender, EventArgs e)
	{
		Response.Redirect("NominatiionResultExcel.aspx?divicode=" + ddlDivision.SelectedValue + "&filter=" + (chkTop3.Checked ? "3" : "0"));
	}
}
