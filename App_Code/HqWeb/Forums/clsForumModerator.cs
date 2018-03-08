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
  public class clsForumModerator : IDisposable
  {

   private string strUsername;
   private string strForumGroupCode;
   private string strStatus;

   public clsForumModerator() { }

   public string Username { get { return strUsername; } set { strUsername = value; } }
   public string ForumGroupCode { get { return strForumGroupCode; } set { strForumGroupCode = value; } }
   public string Status { get { return strStatus; } set { strStatus = value; } }

   public void Dispose() { GC.SuppressFinalize(this); }

   ///////////////////////////////////
   ///////// Static Memebers /////////
   ///////////////////////////////////

   public static DataTable GetDataTable()
   {
    DataTable tblReturn = new DataTable();
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT * FROM Speedo.ForumModerator ORDER BY username";
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblReturn);
    }
    return tblReturn;
   }

   public static DataTable GetDataTable(string pForumGroupCode)
   {
    DataTable tblReturn = new DataTable();
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT * FROM Speedo.ForumModerator WHERE fgrpcode='" + pForumGroupCode + "' AND pstatus='1' ORDER BY username";
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblReturn);
    }
    return tblReturn;
   }

   public static bool IsModerator(string pUsername, string pForumGroupCode)
   {
    bool blnResult;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT username FROM Speedo.ForumModerator WHERE username='" + pUsername + "' AND fgrpcode='" + pForumGroupCode + "' AND pstatus='1'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     blnResult = dr.Read();
     dr.Close();
    }
    return blnResult;
   }

  }
 }
}
