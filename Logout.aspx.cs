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

public partial class Logout : System.Web.UI.Page
{
 
	protected void Page_Load(object sender, EventArgs e)
 {
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cn.Open();
			try
			{
				cmd.CommandText = "UPDATE Users.Users SET online='0' WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
				cmd.ExecuteNonQuery();
			}
			catch
			{
			}
		}
		Response.Cookies["Speedo"].Expires = DateTime.Now.AddDays(-1);
 }

}
