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

public partial class Userpage_EditProfile : System.Web.UI.Page
{

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

		if (!Page.IsPostBack)
		{
			txtUserName.Text = Request.Cookies["Speedo"]["UserName"];
			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandText = "SELECT * FROM Users.Users WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
				cn.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				dr.Read();
				txtFirstName.Text = dr["firname"].ToString();
				txtLastName.Text = dr["lastname"].ToString();
				txtMidName.Text = dr["midname"].ToString();
				dr.Close();
			}
		}
 }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		using(SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "UPDATE Users.Users SET firname=@firname,lastname=@lastname,midname=@midname WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
			cmd.Parameters.Add("@firname",SqlDbType.VarChar,30);
			cmd.Parameters.Add("@lastname",SqlDbType.VarChar,30);
			cmd.Parameters.Add("@midname",SqlDbType.VarChar,30);
			cmd.Parameters["@firname"].Value = txtFirstName.Text;
			cmd.Parameters["@lastname"].Value = txtLastName.Text;
			cmd.Parameters["@midname"].Value = txtMidName.Text;
			cn.Open();
			cmd.ExecuteNonQuery();
		}
	}

}
