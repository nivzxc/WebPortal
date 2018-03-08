using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
 namespace Forums
 {
  public class clsShoutBoxEntry
  {

   private string strEntry;
   private string strPostedBy;

   public clsShoutBoxEntry() { }
   public clsShoutBoxEntry(string pEntry, string pPostedBy) { strEntry = pEntry; strPostedBy = pPostedBy; }

   public string Entry { get { return strEntry; } set { strEntry = value; } }
   public string PostedBy { get { return strPostedBy; } set { strPostedBy = value; } }

   public void PostReply()
   {
    if (strEntry != "")
    {
     const int intLimit = 20;
     int intCtr = 0;
     string strShout = "";
     string strPrevShout = "";
     string[] strWords = strEntry.Split(' ');
     foreach (string pao in strWords)
     {
      if (pao.Length > intLimit)
      {
       intCtr = 0;
       while (intCtr < pao.Length)
       {
        if (pao.Substring(intCtr).Length >= intLimit)
        {
         strShout = strShout + pao.Substring(intCtr, intLimit) + " ";
         intCtr += intLimit;
        }
        else
        {
         strShout = strShout + pao.Substring(intCtr);
         intCtr += pao.Length - intCtr;
        }
       }
      }
      else
       strShout = strShout + pao + " ";
     }

     using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
     {
      SqlCommand cmd = cn.CreateCommand();
      cmd.CommandText = "SELECT TOP 1 msgbody FROM Speedo.Shoutbox WHERE username='" + strPostedBy + "' ORDER BY datesent DESC";
      cn.Open();
      SqlDataReader dr = cmd.ExecuteReader();
      if (dr.Read())
       strPrevShout = dr["msgbody"].ToString();
      dr.Close();

      if (strShout != strPrevShout)
      {
       cmd.CommandText = "INSERT INTO Speedo.Shoutbox VALUES('" + strPostedBy + "',@msgbody,'" + DateTime.Now + "','0')";
       cmd.Parameters.Add("@msgbody", SqlDbType.VarChar, 200);
       cmd.Parameters["@msgbody"].Value = strShout;
       cmd.ExecuteNonQuery();
      }
     }
    }
   }

   public void PostReply(string pEntry, string pPostedBy)
   {
    if (pEntry != "")
    {
     const int intLimit = 20;
     int intCtr = 0;
     string strShout = "";
     string strPrevShout = "";
     string[] strWords = pEntry.Split(' ');
     foreach (string pao in strWords)
     {
      if (pao.Length > intLimit)
      {
       intCtr = 0;
       while (intCtr < pao.Length)
       {
        if (pao.Substring(intCtr).Length >= intLimit)
        {
         strShout = strShout + pao.Substring(intCtr, intLimit) + " ";
         intCtr += intLimit;
        }
        else
        {
         strShout = strShout + pao.Substring(intCtr);
         intCtr += pao.Length - intCtr;
        }
       }
      }
      else
       strShout = strShout + pao + " ";
     }

     using (SqlConnection cn = new SqlConnection(clsHqWeb.pmHqWebConnectionString))
     {
      SqlCommand cmd = cn.CreateCommand();
      cmd.CommandText = "SELECT TOP 1 msgbody FROM Speedo.Shoutbox WHERE username='" + pPostedBy + "' ORDER BY datesent DESC";
      cn.Open();
      SqlDataReader dr = cmd.ExecuteReader();
      if (dr.Read())
       strPrevShout = dr["msgbody"].ToString();
      dr.Close();

      if (strShout != strPrevShout)
      {
       cmd.CommandText = "INSERT INTO Speedo.Shoutbox VALUES('" + pPostedBy + "',@msgbody,'" + DateTime.Now + "','0')";
       cmd.Parameters.Add("@msgbody", SqlDbType.VarChar, 200);
       cmd.Parameters["@msgbody"].Value = strShout;
       cmd.ExecuteNonQuery();
      }
     }
    }
   }

  }
 }
}