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
  public class clsForumReply : IDisposable
  {
   private int intReplyCode;
   private string strForumTopicCode;
   private string strUsername;
   private string strReplyContents;
   private DateTime dteDateReply;

   public clsForumReply() { }
   public clsForumReply(int pReplyCode) { intReplyCode = pReplyCode; }

   public int ReplyCode { get { return intReplyCode; } set { intReplyCode = value; } }
   public string ForumTopicCode { get { return strForumTopicCode; } set { strForumTopicCode = value; } }
   public string Username { get { return strUsername; } set { strUsername = value; } }
   public string ReplyContents { get { return strReplyContents; } set { strReplyContents = value; } }
   public DateTime DateReply { get { return dteDateReply; } set { dteDateReply = value; } }

   public void Fill()
   {
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT * FROM Speedo.ForumReply WHERE rplycode='" + intReplyCode + "'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      strForumTopicCode = dr["frmcode"].ToString();
      strUsername = dr["username"].ToString();
      strReplyContents = clsBB.FormatContents(dr["reply"].ToString());
      dteDateReply = clsValidator.CheckDate(dr["daterply"].ToString());
     }
     dr.Close();
    }
   }

   public int Add()
   {
    int intReturn = 0;
    string strpTopicTitle = clsForumTopics.GetTitle(strForumTopicCode);
    string strpCategoryCode = clsForumTopics.GetCategoryCode(strForumTopicCode);    
    SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    SqlTransaction tran = cn.BeginTransaction();
    cmd.Transaction = tran;
    try
    {
     cmd.CommandText = "INSERT INTO Speedo.ForumReply(frmcode,username,reply,daterply) VALUES(@frmcode,@username,@reply,'" + dteDateReply + "')";
     cmd.Parameters.Add(new SqlParameter("@frmcode", strForumTopicCode));
     cmd.Parameters.Add(new SqlParameter("@username", strUsername));
     cmd.Parameters.Add(new SqlParameter("@reply", strReplyContents));
     cmd.Parameters.Add(new SqlParameter("@daterply", dteDateReply));
     intReturn = cmd.ExecuteNonQuery();

     cmd.Parameters.Clear();
     cmd.CommandText = "UPDATE Speedo.ForumTopic SET lstpstdt=@lstpstdt, lstpstby=@lstpstby, creply=creply+1 WHERE frmcode=@frmcode";
     cmd.Parameters.Add(new SqlParameter("@lstpstdt", DateTime.Now));
     cmd.Parameters.Add(new SqlParameter("@lstpstby", strUsername));
     cmd.Parameters.Add(new SqlParameter("@frmcode", strForumTopicCode));
     cmd.ExecuteNonQuery();

     cmd.Parameters.Clear();
     cmd.CommandText = "UPDATE Speedo.ForumCategory SET cposts=cposts+1,lstpstdt=@lstpstdt, lstpstfr=@lstpstfr, lstpstfd=@lstpstfd, lstpstby=@lstpstby WHERE catcode=@catcode";
     cmd.Parameters.Add(new SqlParameter("@lstpstdt", DateTime.Now));
     cmd.Parameters.Add(new SqlParameter("@lstpstfr", strForumTopicCode));
     cmd.Parameters.Add(new SqlParameter("@lstpstfd", strpTopicTitle));
     cmd.Parameters.Add(new SqlParameter("@lstpstby", strUsername));
     cmd.Parameters.Add(new SqlParameter("@catcode", strpCategoryCode));
     cmd.ExecuteNonQuery();

     tran.Commit();
    }
    catch { intReturn = 0; tran.Rollback(); }
    finally { cn.Close(); }
    return intReturn;
   }

   public int Close()
   {
    int intReturn = 0;
    string strpTopicTitle = clsForumTopics.GetTitle(strForumTopicCode);
    string strpCategoryCode = clsForumTopics.GetCategoryCode(strForumTopicCode);
    SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
    SqlCommand cmd = cn.CreateCommand();
    cn.Open();
    SqlTransaction tran = cn.BeginTransaction();
    cmd.Transaction = tran;
    try
    {
     cmd.CommandText = "INSERT INTO Speedo.ForumReply(frmcode,username,reply,daterply) VALUES('" + strForumTopicCode + "','" + strUsername + "',@reply,'" + dteDateReply + "')";
     cmd.Parameters.Add("@reply", SqlDbType.Text);
     cmd.Parameters["@reply"].Value = strReplyContents;
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();
     cmd.CommandText = "UPDATE Speedo.ForumTopic SET pstatus='0' AND lstpstdt='" + DateTime.Now + "',lstpstby='" + strUsername + "',creply=creply+1 WHERE frmcode='" + strForumTopicCode + "'";
     cmd.ExecuteNonQuery();
     cmd.CommandText = "UPDATE Speedo.ForumCategory SET cposts=cposts+1,lstpstdt='" + DateTime.Now + "',lstpstfr='" + strForumTopicCode + "',lstpstfd='" + strpTopicTitle + "',lstpstby='" + strUsername + "' WHERE catcode='" + strpCategoryCode + "'";
     cmd.ExecuteNonQuery();
     tran.Commit();
    }
    catch { intReturn = 0; tran.Rollback(); }
    finally { cn.Close(); }
    return intReturn;
   }

   public int Edit()
   {
    int intReturn = 0;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "UPDATE Speedo.ForumReply SET reply=@reply WHERE rplycode='" + intReplyCode + "'";
     cmd.Parameters.Add("@reply", SqlDbType.Text);
     cmd.Parameters["@reply"].Value = strReplyContents;
     cn.Open();
     intReturn = cmd.ExecuteNonQuery();
    }
    return intReturn;
   }

   public void Dispose() { GC.SuppressFinalize(this); }

   //////////////////////////////////
   ///////// Static Members /////////
   //////////////////////////////////

   public static int GetFirstReplyCode(string pTopicCode)
   {
    int intReturn = 0;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT TOP 1 rplycode FROM Speedo.ForumReply WHERE frmcode='" + pTopicCode + "' ORDER BY rplycode";
     cn.Open();
     try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
     catch { }
    }
    return intReturn;
   }

   public static int GetTotalPost()
   {
    int intReturn = 0;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT COUNT(rplycode) FROM Speedo.ForumReply";
     cn.Open();
     try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
     catch { }
    }
    return intReturn;
   }

  }
 }
}