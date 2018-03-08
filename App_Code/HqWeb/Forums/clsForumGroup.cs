using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
 namespace Forums
 {
  public class clsForumGroup : IDisposable
  {
   private string _strForumGroupCode;
   private string _strName;
   private int _intOrder;

   public clsForumGroup() { }
   public clsForumGroup(string pForumGroupCode) { _strForumGroupCode = pForumGroupCode; }

   public string ForumGroupCode { get { return _strForumGroupCode; } set { _strForumGroupCode = value; } }
   public string Name { get { return _strName; } set { _strName = value; } }
   public int Order { get { return _intOrder; } set { _intOrder = value; } }

   public void Fill()
   {
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT * FROM Speedo.ForumGroup WHERE fgrpcode='" + _strForumGroupCode + "'";
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      _strName = dr["fgrpname"].ToString();
      _intOrder = int.Parse(dr["fgrpordr"].ToString());
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
     cmd.CommandText = "SELECT * FROM Speedo.ForumGroup ORDER BY fgrpordr";
     SqlDataAdapter da = new SqlDataAdapter(cmd);
     da.Fill(tblReturn);
    }
    return tblReturn;
   }

   public static string GetGroupName(string pGroupCode)
   {
    string strReturn = "";
    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmd = cn.CreateCommand();
     cmd.CommandText = "SELECT fgrpname FROM Speedo.ForumGroup WHERE fgrpcode='" + pGroupCode + "'";
     cn.Open();
     try { strReturn = cmd.ExecuteScalar().ToString(); }
     catch { }
    }
    return strReturn;
   }

  }
 }
}