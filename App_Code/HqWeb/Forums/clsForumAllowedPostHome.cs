using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace HqWeb
{
 namespace Forums
 {
  public class clsForumAllowedPostHome
  {

   public clsForumAllowedPostHome() { }

   //////////////////////////////////
   ///////// Static Members /////////
   //////////////////////////////////

   public static bool IsAllowedPostHome(string pUsername)
   {
    bool blnReturn = false;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT username FROM Speedo.ForumAllowedHomePost WHERE username='" + pUsername + "'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     blnReturn = dr.Read();
     dr.Close();
    }
    return blnReturn;
   }

  }
 }
}
