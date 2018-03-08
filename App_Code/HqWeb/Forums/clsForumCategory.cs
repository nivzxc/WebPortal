using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
 namespace Forums
 {
  public class clsForumCategory : IDisposable
  {
   private string _strForumCategoryCode;
   private string _strForumGroupCode;
   private string _strName;
   private string _strDescription;
   private int _intNumberOfThreads;
   private int _intNumberOfPosts;
   private DateTime _dteLastPostDate;
   private string _strLastPostTopicCode;
   private string _strLastPostTopicTitle;
   private string _strLastPostBy;
   private string _strStatus;

   public clsForumCategory() { }
   public clsForumCategory(string pForumCategoryCode) { _strForumCategoryCode = pForumCategoryCode; }

   public string ForumCategoryCode { get { return _strForumCategoryCode; } set { _strForumCategoryCode = value; } }
   public string ForumGroupCode { get { return _strForumGroupCode; } set { _strForumGroupCode = value; } }
   public string Name { get { return _strName; } set { _strName = value; } }
   public string Description { get { return _strDescription; } set { _strDescription = value; } }
   public int NumberOfThreads { get { return _intNumberOfThreads; } set { _intNumberOfThreads = value; } }
   public int NumberOfPosts { get { return _intNumberOfPosts; } set { _intNumberOfPosts = value; } }
   public DateTime LastPostDate { get { return _dteLastPostDate; } set { _dteLastPostDate = value; } }
   public string LastPostTopicCode { get { return _strLastPostTopicCode; } set { _strLastPostTopicCode = value; } }
   public string LastPostTopicTitle { get { return _strLastPostTopicTitle; } set { _strLastPostTopicTitle = value; } }
   public string LastPostBy { get { return _strLastPostBy; } set { _strLastPostBy = value; } }
   public string Status { get { return _strStatus; } set { _strStatus = value; } }

   public void Fill()
   {
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT * FROM Speedo.ForumCategory WHERE catcode='" + _strForumCategoryCode + "'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      _strForumGroupCode = dr["fgrpcode"].ToString();
      _strName = dr["category"].ToString();
      _strDescription = dr["category"].ToString();
      _intNumberOfThreads = int.Parse(dr["cthreads"].ToString());
      _intNumberOfPosts = int.Parse(dr["cposts"].ToString());
      _dteLastPostDate = clsValidator.CheckDate(dr["lstpstdt"].ToString());
      _strLastPostTopicCode = dr["lstpstfr"].ToString();
      _strLastPostTopicTitle = dr["lstpstfd"].ToString();
      _strLastPostBy = dr["lstpstby"].ToString();
      _strStatus = dr["status"].ToString();     
     }
     dr.Close();
    }
   }

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
     cmd.CommandText = "SELECT * FROM Speedo.ForumCategory ORDER BY category";
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
     cmd.CommandText = "SELECT * FROM Speedo.ForumCategory WHERE fgrpcode='" + pForumGroupCode + "' ORDER BY category";
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblReturn);
    }
    return tblReturn;
   }

   public static string GetCategoryName(string pCategoryCode)
   {
    string strReturn = "";
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT category FROM Speedo.ForumCategory WHERE catcode='" + pCategoryCode + "'";
     cn.Open();
     try { strReturn = cmd.ExecuteScalar().ToString(); }
     catch { }
    }
    return strReturn;
   }

   public static string GetGroupCode(string pCategoryCode)
   {
    string strReturn = "";
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT fgrpcode FROM Speedo.ForumCategory WHERE catcode='" + pCategoryCode + "'";
     cn.Open();
     try { strReturn = cmd.ExecuteScalar().ToString(); }
     catch { }
    }
    return strReturn;
   }

  }
 }
}
