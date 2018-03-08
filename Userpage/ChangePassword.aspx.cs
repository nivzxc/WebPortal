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

public partial class Userpage_ChangePassword : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
 }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		bool blnChangeSucess = false;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{			
			string strCurrentPassword = "";
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT pword FROM Users.Users WHERE username = '" + Request.Cookies["Speedo"]["UserName"] + "'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.Read())
				strCurrentPassword = dr["pword"].ToString();
			dr.Close();

			if (strCurrentPassword == txtCurrent.Text)
			{				
				cmd.CommandText = "UPDATE Users.Users SET pword=@pword WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
				cmd.Parameters.Add("@pword", SqlDbType.VarChar, 15);
				cmd.Parameters["@pword"].Value = txtPass1.Text;				
				cmd.ExecuteNonQuery();
				blnChangeSucess = true;

				
			}
			else
			{
				lblErr.Visible = true;
				lblErr.Text = "Error: wrong current password.";
			}
		}
		if(blnChangeSucess)
			Response.Redirect("ChangePasswordSuccess.aspx");
	}

}
