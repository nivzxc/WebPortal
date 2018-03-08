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
using System.Net.Mail;

public partial class forgotpassword : System.Web.UI.Page
{

   protected void Page_Load(object sender, EventArgs e)
   {

   }

   protected void btnForgotPassword_Click(object sender, ImageClickEventArgs e)
   {
      string strName = "";
      string strMail = "";
      string strPWord = "";
      string strWrite = "";

      using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
      {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT pword,email,firname FROM Users.Users WHERE email='" + txtUsername.Text + "'";
         Response.Write(cmd.CommandText);
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         if (dr.Read())
         {
            strName = dr["firname"].ToString();
            strMail = dr["email"].ToString();
            strPWord = dr["pword"].ToString();
         }
         dr.Close();

         strWrite = "<p>Hi " + strName + "! </p>" +
                            "<p>Your Head Office Portal Password is </p>" +
                            "<p style='color:blue;'><b>" + strPWord + "</b></p>" +
                            "<p>It is advisable for you to memorize your password and delete this email to avoid unauthorized usage of your Head Office Portal account.</p><br />" +
                            "<p>Regards ,<br />Head Office Portal Admin</p>";
      }

      if (strName != "")
          clsSpeedo.SendMail(strMail, "Head Office Portal Password Recovery", strWrite);

      Response.Redirect("MemberLogin.aspx");
   }

}
