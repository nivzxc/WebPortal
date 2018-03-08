using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
 namespace Forums
 {

  public class clsForumTopics : IDisposable
  {
   private string _strForumTopicCode;
   private string _strCategoryCode;
   private string _strTitle;
   private string _strDescription;
   private string _strUsername;
   private DateTime _dteDatePosted;
   private DateTime _dteLastPostDate;
   private string _strLastPostBy;
   private int _intTotalReply;
   private string _strSticky;
   private string _strPostHome;
   private string _strStatus;

   public clsForumTopics() { }
   public clsForumTopics(string pForumTopicCode) { _strForumTopicCode = pForumTopicCode; }

   public string CategoryCode { set { _strCategoryCode = value; } get { return _strCategoryCode; } }
   public string Title { set { _strTitle = value; } get { return _strTitle; } }
   public string Description { set { _strDescription = value; } get { return _strDescription; } }
   public string Username { set { _strUsername = value; } get { return _strUsername; } }
   public DateTime DatePosted { set { _dteDatePosted = value; } get { return _dteDatePosted; } }
   public DateTime LastPostDate { set { _dteLastPostDate = value; } get { return _dteLastPostDate; } }
   public string LastPostBy { set { _strLastPostBy = value; } get { return _strLastPostBy; } }
   public int TotalReply { set { _intTotalReply = value; } get { return _intTotalReply; } }
   public string Sticky { set { _strSticky = value; } get { return _strSticky; } }
   public string PostHome { set { _strPostHome = value; } get { return _strPostHome; } }
   public string Status { set { _strStatus = value; } get { return _strStatus; } }

   public int Insert(string pReply)
   {
    int intReturn = 0;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = new SqlCommand("spNewThread", cn);
     cmd.CommandType = CommandType.StoredProcedure;
     cmd.Parameters.Add("@catcode", SqlDbType.Char, 3);
     cmd.Parameters.Add("@frmtopic", SqlDbType.VarChar, 100);
     cmd.Parameters.Add("@frmdesc", SqlDbType.Text);
     cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
     cmd.Parameters.Add("@datepost", SqlDbType.DateTime);
     cmd.Parameters.Add("@posthome", SqlDbType.Char, 1);
     cmd.Parameters.Add("@pstatus", SqlDbType.Char, 1);
     cmd.Parameters.Add("@reply", SqlDbType.Text);

     cmd.Parameters.Add("@frmcode", SqlDbType.Char, 9);
     cmd.Parameters["@catcode"].Value = _strCategoryCode;
     cmd.Parameters["@frmtopic"].Value = _strTitle;
     cmd.Parameters["@frmdesc"].Value = _strDescription;
     cmd.Parameters["@username"].Value = _strUsername;
     cmd.Parameters["@datepost"].Value = _dteDatePosted;
     cmd.Parameters["@posthome"].Value = _strPostHome;
     cmd.Parameters["@pstatus"].Value = _strStatus;
     cmd.Parameters["@reply"].Value = pReply;
     cmd.Parameters["@frmcode"].Direction = ParameterDirection.Output;
     cn.Open();
     intReturn = cmd.ExecuteNonQuery();
    }

    return intReturn;
   }

   public void Dispose() { GC.SuppressFinalize(this); }

   ///////////////////////////////////
   ///////// Static Members /////////
   ///////////////////////////////////

   public static int CountReplies(string pTopicCode)
   {
    int intReturn = 0;
    using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
    {
    }
    return intReturn;
   }

   public static DataTable DSGHomePosted()
   {
    DataTable tblReturn = new DataTable();
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT frmcode,frmtopic,frmdesc,Speedo.ForumTopic.username,datepost,Speedo.ForumCategory.catcode,category,Speedo.ForumGroup.fgrpcode,fgrpname " +
                       "FROM Speedo.ForumTopic INNER JOIN (Speedo.ForumCategory INNER JOIN Speedo.ForumGroup ON Speedo.ForumCategory.fgrpcode = Speedo.ForumGroup.fgrpcode) ON Speedo.ForumTopic.catcode = Speedo.ForumCategory.catcode WHERE posthome='1' ORDER BY datepost DESC";
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblReturn);
    }
    return tblReturn;
   }

   public static DataTable GetDataTable()
   {
    DataTable tblReturn = new DataTable();
    return tblReturn;
   }

   public static string GetCategoryCode(string pTopicCode)
   {
    string strReturn = "";
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT catcode FROM Speedo.ForumTopic WHERE frmcode='" + pTopicCode + "'";
     cn.Open();
     try { strReturn = cmd.ExecuteScalar().ToString(); }
     catch { }
    }
    return strReturn;
   }

   public static string GetGroupCode(string pTopicCode)
   {
    string strReturn = "";
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT fgrpcode FROM Speedo.ForumCategory WHERE catcode = (SELECT catcode FROM Speedo.ForumTopic WHERE frmcode='" + pTopicCode + "')";
     cn.Open();
     try { strReturn = cmd.ExecuteScalar().ToString(); }
     catch { }
    }
    return strReturn;
   }

   public static string GetTitle(string pTopicCode)
   {
    string strReturn = "";
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT frmtopic FROM Speedo.ForumTopic WHERE frmcode='" + pTopicCode + "'";
     cn.Open();
     try { strReturn = cmd.ExecuteScalar().ToString(); }
     catch { }
    }
    return strReturn;
   }

   public static string GetStatus(string pTopicCode)
   {
    string strReturn = "";
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT pstatus FROM Speedo.ForumTopic WHERE frmcode='" + pTopicCode + "'";
     cn.Open();
     try { strReturn = cmd.ExecuteScalar().ToString(); }
     catch { }
    }
    return strReturn;
   }

   public static bool IsThreadPrivate(string pTopicCode)
   {
    bool blnReturn = false;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT prvtflag FROM Speedo.ForumTopic WHERE frmcode=@frmcode";
     cmd.Parameters.Add(new SqlParameter("@frmcode", pTopicCode));
     cn.Open();
     try { blnReturn = cmd.ExecuteScalar().ToString() == "1"; }
     catch { }
    }    
    return blnReturn;
   }

   public static bool CanViewPrivateThread(string pTopicCode, string pUsername)
   {
    bool blnReturn = false;
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT * FROM Speedo.ForumUsers WHERE frmcode=@frmcode AND username=@username";
     cmd.Parameters.Add(new SqlParameter("@frmcode", pTopicCode));
     cmd.Parameters.Add(new SqlParameter("@username", pUsername));
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