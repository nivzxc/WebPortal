using System;
using System.Data;
using System.Data.SqlClient;

public class clsMessage : IDisposable
{
 public clsMessage() { }

 private string _strMessageCode;
 private string _strSentBy;
 private string _strSentTo;
 private string _strSubject;
 private string _strBody;
 private DateTime _dteDateSent;
 private string _strIsRead;
 private string _strIsReply;
 private string _strEnabled;

 public string MessageCode { get { return _strMessageCode; } set { _strMessageCode = value; } }
 public string SentBy { get { return _strSentBy; } set { _strSentBy = value; } }
 public string SentTo { get { return _strSentTo; } set { _strSentTo = value; } }
 public string Subject { get { return _strSubject; } set { _strSubject = value; } }
 public string Body { get { return _strBody; } set { _strBody = value; } }
 public DateTime DateSent { get { return _dteDateSent; } set { _dteDateSent = value; } }
 public string IsRead { get { return _strIsRead; } set { _strIsRead = value; } }
 public string IsReply { get { return _strIsReply; } set { _strIsReply = value; } }
 public string Enabled { get { return _strEnabled; } set { _strEnabled = value; } }

 public void Fill()
 {
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT * FROM HR.Messages WHERE msgcode='" + _strMessageCode + "'";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   if (dr.Read())
   {
    _strSentBy = dr["sentby"].ToString();
    _strSentTo = dr["sentto"].ToString();
    _strSubject = dr["subject"].ToString();
    _strBody = dr["msgbody"].ToString();
    _dteDateSent = clsValidator.CheckDate(dr["datesent"].ToString());
    _strIsRead = dr["pread"].ToString();
    _strIsReply = dr["preply"].ToString();
    _strEnabled = dr["penabled"].ToString();
   }
   dr.Close();
  }
 }

 public int Insert()
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "INSERT INTO HR.Messages(sentby,sentto,subject,msgbody,datesent,pread,preply,penabled) VALUES(@sentby,@sentto,@subject,@msgbody,@datesent,@pread,@preply,@penabled)";
   cmd.Parameters.Add(new SqlParameter("@sentby", _strSentBy));
   cmd.Parameters.Add(new SqlParameter("@sentto", _strSentTo));
   cmd.Parameters.Add(new SqlParameter("@subject", _strSubject));
   cmd.Parameters.Add(new SqlParameter("@msgbody", _strBody));
   cmd.Parameters.Add(new SqlParameter("@datesent", _dteDateSent));
   cmd.Parameters.Add(new SqlParameter("@pread", _strIsRead));
   cmd.Parameters.Add(new SqlParameter("@preply", _strIsReply));
   cmd.Parameters.Add(new SqlParameter("@penabled", _strEnabled));
   cn.Open();
   intReturn = cmd.ExecuteNonQuery();
  }
  return intReturn;
 }

 public void Dispose() { GC.SuppressFinalize(this); }

 //////////////////////////////////
 ///////// Static Members /////////
 //////////////////////////////////

 public static int CountUnRead(string pUsername)
 {
  int intReturn = 0;
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT COUNT(*) FROM Speedo.Messages WHERE pread='0' AND sentto='" + pUsername + "'";
   cn.Open();
   try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
   catch { }
  }
  return intReturn;
 }

 public static DataTable DSGInbox(string pUsername)
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT * FROM Speedo.Messages WHERE sentto='" + pUsername + "' ORDER BY datesent";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

 public static DataTable DSGSentItems(string pUsername)
 {
  DataTable tblReturn = new DataTable();
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT * FROM Speedo.Messages WHERE sentby='" + pUsername + "' ORDER BY datesent";
   SqlDataAdapter da = new SqlDataAdapter(cmd);
   da.Fill(tblReturn);
  }
  return tblReturn;
 }

 public static void SetRead(string pMessageCode)
 {
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "UPDATE Speedo.Messages SET pread='1' WHERE msgcode='" + pMessageCode + "'";
   cn.Open();
   cmd.ExecuteNonQuery();
  }
 }

}