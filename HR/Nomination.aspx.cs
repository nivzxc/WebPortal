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
using HRMS;

public partial class HR_Nomination : System.Web.UI.Page
{

	protected bool IsMiddleNameValid(string strUserName,string strMidName)
	{
		bool blnResult;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT empnum FROM users WHERE username='" + strUserName + "' AND midname='" + strMidName + "'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnResult = dr.Read();
			dr.Close();
		}
		if (blnResult)
			lblMessage.Text = "Invalid Middle Name.";
		return blnResult;
	}

	protected bool CheckItems()
	{
		if (((ddlRes1.SelectedValue == ddlRes2.SelectedValue) && (ddlRes1.SelectedValue != "na" && ddlRes2.SelectedValue != "na")) || ((ddlRes1.SelectedValue == ddlRes3.SelectedValue) && (ddlRes1.SelectedValue != "na" && ddlRes3.SelectedValue != "na")) || ((ddlRes2.SelectedValue == ddlRes3.SelectedValue) && (ddlRes2.SelectedValue != "na" && ddlRes3.SelectedValue != "na")))
			lblMessage.Text = lblMessage.Text +  "<br>Duplicate nominee for Respect category.";

		if (((ddlSer1.SelectedValue == ddlSer2.SelectedValue) && (ddlSer1.SelectedValue != "na" && ddlSer2.SelectedValue != "na")) || ((ddlSer1.SelectedValue == ddlSer3.SelectedValue) && (ddlSer1.SelectedValue != "na" && ddlSer3.SelectedValue != "na")) || ((ddlSer2.SelectedValue == ddlSer3.SelectedValue) && (ddlSer2.SelectedValue != "na" && ddlSer3.SelectedValue != "na")))
			lblMessage.Text = lblMessage.Text + "<br>Duplicate nominee for Service category.";

		if (((ddlEnt1.SelectedValue == ddlEnt2.SelectedValue) && (ddlEnt1.SelectedValue != "na" && ddlEnt2.SelectedValue != "na")) || ((ddlEnt1.SelectedValue == ddlEnt3.SelectedValue) && (ddlEnt1.SelectedValue != "na" && ddlEnt3.SelectedValue != "na")) || ((ddlEnt2.SelectedValue == ddlEnt3.SelectedValue) && (ddlEnt2.SelectedValue != "na" && ddlEnt3.SelectedValue != "na")))
			lblMessage.Text = lblMessage.Text + "<br>Duplicate nominee for Entrepreneurship category.";

		if (((ddlMal1.SelectedValue == ddlMal2.SelectedValue) && (ddlMal1.SelectedValue != "na" && ddlMal2.SelectedValue != "na")) || ((ddlMal1.SelectedValue == ddlMal3.SelectedValue) && (ddlMal1.SelectedValue != "na" && ddlMal3.SelectedValue != "na")) || ((ddlMal2.SelectedValue == ddlMal3.SelectedValue) && (ddlMal2.SelectedValue != "na" && ddlMal3.SelectedValue != "na")))
			lblMessage.Text = lblMessage.Text + "<br>Duplicate nominee for Malasakit category.";

		if (((ddlTea1.SelectedValue == ddlTea2.SelectedValue) && (ddlTea1.SelectedValue != "na" && ddlTea2.SelectedValue != "na")) || ((ddlTea1.SelectedValue == ddlTea3.SelectedValue) && (ddlTea1.SelectedValue != "na" && ddlTea3.SelectedValue != "na")) || ((ddlTea2.SelectedValue == ddlTea3.SelectedValue) && (ddlTea2.SelectedValue != "na" && ddlTea3.SelectedValue != "na")))
			lblMessage.Text = lblMessage.Text + "<br>Duplicate nominee for Teamwork category.";

		if (((ddlMer1.SelectedValue == ddlMer2.SelectedValue) && (ddlMer1.SelectedValue != "na" && ddlMer2.SelectedValue != "na")) || ((ddlMer1.SelectedValue == ddlMer3.SelectedValue) && (ddlMer1.SelectedValue != "na" && ddlMer3.SelectedValue != "na")) || ((ddlMer2.SelectedValue == ddlMer3.SelectedValue) && (ddlMer2.SelectedValue != "na" && ddlMer3.SelectedValue != "na")))
			lblMessage.Text = lblMessage.Text + "<br>Duplicate nominee for Meritocracy category.";

		if (((ddlExc1.SelectedValue == ddlExc2.SelectedValue) && (ddlExc1.SelectedValue != "na" && ddlExc2.SelectedValue != "na")) || ((ddlExc1.SelectedValue == ddlExc3.SelectedValue) && (ddlExc1.SelectedValue != "na" && ddlExc3.SelectedValue != "na")) || ((ddlExc2.SelectedValue == ddlExc3.SelectedValue) && (ddlExc2.SelectedValue != "na" && ddlExc3.SelectedValue != "na")))
			lblMessage.Text = lblMessage.Text + "<br>Duplicate nominee for Excellence category.";

		trError.Visible = (lblMessage.Text == "" ? false: true);
		return (lblMessage.Text == "" ? true : false);
	}

	protected bool IsAnswered(string strUsername)
	{
		bool blnResult;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(username) FROM awards_votes WHERE username='" + strUsername + "'";
			cn.Open();
			try
			{
				blnResult = (Convert.ToDouble(cmd.ExecuteScalar().ToString()) >= 1 ? true : false);				
			}
			catch
			{
				blnResult = false;
				
			}
		}
		if (blnResult)
			lblMessage.Text = "You already casted your vote.";
		return blnResult;
	}

	protected string GetUsername(string strEmpNum)
	{
		string strResult = "";
		SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString());
		SqlCommand cmd = cn.CreateCommand();
		cmd.CommandText = "SELECT username FROM users WHERE empnum='" + strEmpNum + "'";

		try
		{
			cn.Open();
			strResult = cmd.ExecuteScalar().ToString();
		}
		catch
		{
			strResult = "na";
		}
		finally
		{
			cn.Close();
		}
		return strResult;
	}

	protected void Page_Load(object sender, EventArgs e)
 {

 }

	protected void btnValidate_Click(object sender, ImageClickEventArgs e)
	{
		string strUsername = GetUsername(txtEmpNum.Text);
		if (IsMiddleNameValid(strUsername, txtMidName.Text))
		{
			if (IsAnswered(strUsername))
			{
				lblMessage.Text = "You already casted your vote.";
				trVote.Visible = false;
				trError.Visible = true;
			}
			else
			{
				trVote.Visible = true;
				trError.Visible = false;
				string strDiviCode = clsEmployee.GetDivisionCode(strUsername);
				DataTable tblUsers = new DataTable();

				using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
				{
					SqlCommand cmd = cn.CreateCommand();
					cmd.CommandText = "SELECT users.username,firname + ' ' + lastname AS name FROM users INNER JOIN users_division ON users.username = users_division.username WHERE divicode='" + strDiviCode + "' AND empnum NOT IN ('15022','96539','64704','48988','83816','51492','51953','05042','86418','18938','45670','53949','79576','42833') ORDER BY firname";
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					cn.Open();
					da.Fill(tblUsers);
				}
				ListItem lstItem = new ListItem();
				lstItem.Text = "-=Select Nominee=-";
				lstItem.Value = "na";
				lstItem.Selected = true;

				ddlRes1.DataSource = tblUsers;
				ddlRes1.DataTextField = "name";
				ddlRes1.DataValueField = "username";
				ddlRes1.DataBind();
				ddlRes1.Items.Add(lstItem);

				ddlRes2.DataSource = tblUsers;
				ddlRes2.DataTextField = "name";
				ddlRes2.DataValueField = "username";
				ddlRes2.DataBind();
				ddlRes2.Items.Add(lstItem);

				ddlRes3.DataSource = tblUsers;
				ddlRes3.DataTextField = "name";
				ddlRes3.DataValueField = "username";
				ddlRes3.DataBind();
				ddlRes3.Items.Add(lstItem);

				ddlSer1.DataSource = tblUsers;
				ddlSer1.DataTextField = "name";
				ddlSer1.DataValueField = "username";
				ddlSer1.DataBind();
				ddlSer1.Items.Add(lstItem);

				ddlSer2.DataSource = tblUsers;
				ddlSer2.DataTextField = "name";
				ddlSer2.DataValueField = "username";
				ddlSer2.DataBind();
				ddlSer2.Items.Add(lstItem);

				ddlSer3.DataSource = tblUsers;
				ddlSer3.DataTextField = "name";
				ddlSer3.DataValueField = "username";
				ddlSer3.DataBind();
				ddlSer3.Items.Add(lstItem);

				ddlEnt1.DataSource = tblUsers;
				ddlEnt1.DataTextField = "name";
				ddlEnt1.DataValueField = "username";
				ddlEnt1.DataBind();
				ddlEnt1.Items.Add(lstItem);

				ddlEnt2.DataSource = tblUsers;
				ddlEnt2.DataTextField = "name";
				ddlEnt2.DataValueField = "username";
				ddlEnt2.DataBind();
				ddlEnt2.Items.Add(lstItem);

				ddlEnt3.DataSource = tblUsers;
				ddlEnt3.DataTextField = "name";
				ddlEnt3.DataValueField = "username";
				ddlEnt3.DataBind();
				ddlEnt3.Items.Add(lstItem);

				ddlMal1.DataSource = tblUsers;
				ddlMal1.DataTextField = "name";
				ddlMal1.DataValueField = "username";
				ddlMal1.DataBind();
				ddlMal1.Items.Add(lstItem);

				ddlMal2.DataSource = tblUsers;
				ddlMal2.DataTextField = "name";
				ddlMal2.DataValueField = "username";
				ddlMal2.DataBind();
				ddlMal2.Items.Add(lstItem);

				ddlMal3.DataSource = tblUsers;
				ddlMal3.DataTextField = "name";
				ddlMal3.DataValueField = "username";
				ddlMal3.DataBind();
				ddlMal3.Items.Add(lstItem);

				ddlTea1.DataSource = tblUsers;
				ddlTea1.DataTextField = "name";
				ddlTea1.DataValueField = "username";
				ddlTea1.DataBind();
				ddlTea1.Items.Add(lstItem);

				ddlTea2.DataSource = tblUsers;
				ddlTea2.DataTextField = "name";
				ddlTea2.DataValueField = "username";
				ddlTea2.DataBind();
				ddlTea2.Items.Add(lstItem);

				ddlTea3.DataSource = tblUsers;
				ddlTea3.DataTextField = "name";
				ddlTea3.DataValueField = "username";
				ddlTea3.DataBind();
				ddlTea3.Items.Add(lstItem);

				ddlMer1.DataSource = tblUsers;
				ddlMer1.DataTextField = "name";
				ddlMer1.DataValueField = "username";
				ddlMer1.DataBind();
				ddlMer1.Items.Add(lstItem);

				ddlMer2.DataSource = tblUsers;
				ddlMer2.DataTextField = "name";
				ddlMer2.DataValueField = "username";
				ddlMer2.DataBind();
				ddlMer2.Items.Add(lstItem);

				ddlMer3.DataSource = tblUsers;
				ddlMer3.DataTextField = "name";
				ddlMer3.DataValueField = "username";
				ddlMer3.DataBind();
				ddlMer3.Items.Add(lstItem);

				ddlExc1.DataSource = tblUsers;
				ddlExc1.DataTextField = "name";
				ddlExc1.DataValueField = "username";
				ddlExc1.DataBind();
				ddlExc1.Items.Add(lstItem);

				ddlExc2.DataSource = tblUsers;
				ddlExc2.DataTextField = "name";
				ddlExc2.DataValueField = "username";
				ddlExc2.DataBind();
				ddlExc2.Items.Add(lstItem);

				ddlExc3.DataSource = tblUsers;
				ddlExc3.DataTextField = "name";
				ddlExc3.DataValueField = "username";
				ddlExc3.DataBind();
				ddlExc3.Items.Add(lstItem);
			}
		}
		else
		{
			lblMessage.Text = "Invalid Middle Name.";
			trVote.Visible = false;
			trError.Visible = true;
		}
	}

	protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
	{
		lblMessage.Text = "";
		string strUsername = GetUsername(txtEmpNum.Text);
		if (!IsAnswered(strUsername) && CheckItems())
		{
			SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString());
			cn.Open();
			SqlTransaction tran = cn.BeginTransaction();
			SqlCommand cmd = cn.CreateCommand();
			cmd.Transaction = tran;
			try
			{
				if (ddlRes1.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','RES','" + ddlRes1.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlRes2.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','RES','" + ddlRes2.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlRes3.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','RES','" + ddlRes3.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlSer1.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','SER','" + ddlSer1.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlSer2.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','SER','" + ddlSer2.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlSer3.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','SER','" + ddlSer3.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlEnt1.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','ENT','" + ddlEnt1.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlEnt2.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','ENT','" + ddlEnt2.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlEnt3.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','ENT','" + ddlEnt3.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlMal1.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','MAL','" + ddlMal1.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlMal2.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','MAL','" + ddlMal2.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlMal3.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','MAL','" + ddlMal3.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlTea1.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','TEA','" + ddlTea1.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlTea2.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','TEA','" + ddlTea2.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlTea3.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','TEA','" + ddlTea3.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlMer1.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','MER','" + ddlMer1.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlMer2.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','MER','" + ddlMer2.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlMer3.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','MER','" + ddlMer3.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlExc1.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','EXC','" + ddlExc1.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlExc2.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','EXC','" + ddlExc2.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				if (ddlExc3.SelectedValue != "na")
				{
					cmd.CommandText = "INSERT INTO awards_votes VALUES('" + strUsername + "','EXC','" + ddlExc3.SelectedValue + "')";
					cmd.ExecuteNonQuery();
				}

				tran.Commit();
				cn.Close();
				Response.Redirect("NominationSucess.aspx");
			}
			catch
			{
				tran.Rollback();
				cn.Close();
			}
		}
		else
		{
			trError.Visible = true;
		}
	}

}

