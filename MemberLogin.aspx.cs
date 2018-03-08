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

public partial class MemberLogin : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
     if (Request.Cookies["Speedo"] != null)
     {
         if (!clsUsers.IsActive(Request.Cookies["Speedo"]["UserName"].ToString()))
         {
             Response.Cookies["Speedo"].Expires = DateTime.Now.AddDays(-1);
             Response.Redirect("~/MemberLogin.aspx");
             //Response.Redirect("http://www.sti.edu");
         }
         else
         {
             Response.Redirect("~/Default.aspx");
         }
         //clsSpeedo.Authenticate(Request.Cookies["Speedo"]["UserName"].ToString());
         //Response.Redirect("~/Default.aspx");
     }
 }

	protected void btnLogin_Click(object sender, ImageClickEventArgs e)
	{
		bool blnAuthenticated = false;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT username,email FROM Users.Users WHERE email=@email AND pword=@pword AND pstatus='1'";
   cmd.Parameters.Add(new SqlParameter("@email", txtUsername.Text));
   cmd.Parameters.Add(new SqlParameter("@pword", txtPWord.Text));
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnAuthenticated = dr.Read();
			if (blnAuthenticated)
			{
				lblMessage.Visible = false;
				Response.Cookies["Speedo"]["UserName"] = dr["username"].ToString().ToLower();
				Response.Cookies["Speedo"].Expires = DateTime.Now.AddYears(9);
			}
			else
			{
				lblMessage.Visible = true;
				txtPWord.Text = "";
			}
			dr.Close();

			if (blnAuthenticated)
			{
    cmd.Parameters.Clear();
    cmd.CommandText = "UPDATE Users.Users SET online='1',lastlog=@lastlog WHERE email=@email";
    cmd.Parameters.Add(new SqlParameter("@lastlog", DateTime.Now));
    cmd.Parameters.Add(new SqlParameter("@email", txtUsername.Text));
				cmd.ExecuteNonQuery();
			}
		}

		if (blnAuthenticated)
			Response.Redirect("~/Default.aspx");
 }

}