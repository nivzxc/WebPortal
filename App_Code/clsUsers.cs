using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

public class clsUsers : IDisposable
{
 private string strUsername;
 private string strPassword;
 private string strLastName;
 private string strFirstName;
 private string strMiddleName;
 private string strNickName;
 private string strEmail;
 private DateTime dteBirthDate;
 private string strGender;
 private string strOnline;
 private DateTime dteLastLog;
 private string strUserClass;
 private string strPStatus;

 public clsUsers() { }
 public clsUsers(string pUsername) { strUsername = pUsername; }

 public string Username { get { return strUsername; } set { strUsername = value; } }
 public string Password { get { return strPassword; } set { strPassword = value; } }
 public string LastName { get { return strLastName; } set { strLastName = value; } }
 public string FirstName { get { return strFirstName; } set { strFirstName = value; } }
 public string MiddleName { get { return strMiddleName; } set { strMiddleName = value; } }
 public string NickName { get { return strNickName; } set { strNickName = value; } }
 public string Email { get { return strEmail; } set { strEmail = value; } }
 public DateTime Birthdate { get { return dteBirthDate; } set { dteBirthDate = value; } }
 public string Gender { get { return strGender; } set { strGender = value; } }
 public string Online { get { return strOnline; } set { strOnline = value; } }
 public DateTime LastLog { get { return dteLastLog; } set { dteLastLog = value; } }
 public string UserClass { get { return strUserClass; } set { strUserClass = value; } }
 public string Status { get { return strPStatus; } set { strPStatus = value; } }
 public string FullName { get { return strFirstName + ' ' + strLastName; } }

 public void Fill()
 {
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT * FROM Users.Users WHERE username='" + strUsername + "'";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   if (dr.Read())
   {
    strPassword = dr["pword"].ToString();
    strLastName = dr["lastname"].ToString();
    strFirstName = dr["firname"].ToString();
    strMiddleName = dr["midname"].ToString();
    strNickName = dr["nickname"].ToString();
    strEmail = dr["email"].ToString();
    dteBirthDate = clsValidator.CheckDate(dr["brthdate"].ToString());
    strGender = dr["gender"].ToString();
    strOnline = dr["online"].ToString();
    dteLastLog = clsValidator.CheckDate(dr["lastlog"].ToString());
    strPStatus = dr["pstatus"].ToString();
   }
   dr.Close();
  }
 }

 public void Dispose() { GC.SuppressFinalize(this); }

 //////////////////////////////////
 ///////// Static Members /////////
 //////////////////////////////////
 
 public static string GetName(string pUsername)
 {
  string strReturn = "";
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT firname + ' ' + lastname FROM Users.Users WHERE username='" + pUsername + "'";
   cn.Open();
   try { strReturn = cmd.ExecuteScalar().ToString(); }
   catch { strReturn = ""; }
  }
  return strReturn;
 }

 public static string GetEmail(string pUsername)
 {
  string strResult = "";
  using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT email FROM Users.Users WHERE username='" + pUsername + "'";
   cn.Open();
   try { strResult = cmd.ExecuteScalar().ToString(); }
   catch { strResult = "No Email"; }
  }
  return strResult;
 }

 public static string GetPassword(string pUsername)
 {
     string strResult = "";
     using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
     {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT pword FROM Users.Users WHERE username='" + pUsername + "'";
         cn.Open();
         try { strResult = cmd.ExecuteScalar().ToString(); }
         catch { strResult = ""; }
     }
     return strResult;
 }

 public static bool IsUser(string pEmailAddress)
 {
     bool blnReturn = false;
     using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
     {
         SqlCommand cmd = cn.CreateCommand();
         cmd.CommandText = "SELECT email FROM Users.Users WHERE email=@email";
         cmd.Parameters.Add(new SqlParameter("@email", pEmailAddress));
         cn.Open();
         SqlDataReader dr = cmd.ExecuteReader();
         if (dr.Read())
         {
             blnReturn = true;
         }
        
     }
     return blnReturn;
 }

 //public static bool Login(string pUsername, string pPassword)
 //{
 //    bool blnReturn = false;
 //    try
 //    {
 //        if (IsUser(pUsername + "@stihq.net"))
 //        {
 //            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://sti", pUsername, pPassword);
 //            DirectorySearcher dirSearch = new DirectorySearcher(dirEntry);
 //            dirSearch.Filter = "(SAMAccountName=" + pUsername + ")";
 //            dirSearch.PropertiesToLoad.Add("lockoutTime");

 //            // This line returns an error whenever username or password doesn't match in AD
 //            // I would like it to return NULL instead, how will I do that?
 //            SearchResult srcResult = dirSearch.FindOne();

 //            if (srcResult != null)
 //            {
 //                if (srcResult.Properties["lockoutTime"][0].ToString() == "0")
 //                {
 //                    blnReturn = true;
 //                }
 //                else
 //                {
 //                    blnReturn = false;
 //                }
 //            }
 //            else
 //            {
 //                blnReturn = false;
 //            }

 //        }
 //    }
 //    catch
 //    {
 //        blnReturn = false;
 //    }
 //    return blnReturn;
 //}

    public static string GetLocalNumber(string pUsername)
    {
        string strReturn = "";
        string strUsername = clsUsers.GetEmail(pUsername).Replace("@stihq.net", "");
        //string strUsername = "aalbuero";
        //string strPassword = clsUsers.GetPassword("aalbuero@stihq.net");
        try
        {
            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://sti");
            DirectorySearcher dirSearch = new DirectorySearcher(dirEntry);

            //string username = "user";
            //string domain = "LDAP://DC=domain,DC=com";
            //DirectorySearcher search = new DirectorySearcher(domain);
            //search.Filter = "(SAMAccountName=" + username + ")";

            dirSearch.Filter = "(SAMAccountName=" + strUsername + ")";
            dirSearch.PropertiesToLoad.Add("telephoneNumber");
            //dirSearch.PropertiesToLoad.Add("sn");
            // This line returns an error whenever username or password doesn't match in AD
            SearchResult srcResult = dirSearch.FindOne();

            if (srcResult != null)
            {
                strReturn = (string)srcResult.Properties["telephoneNumber"][0];
                if (strReturn.Contains(" x "))
                {
                    strReturn = strReturn.Substring(strReturn.Length - 4);
                }
                
                //string lastname = (string)srcResult.Properties["sn"][0];
                //For different Active Directory property name http://fsuid.fsu.edu/admin/lib/WinADLDAPAttributes.html
            }
            else
            {
                strReturn = "";
            }
        }
        catch
        {
            //Invalid username and password
            strReturn = "";
        }

        return strReturn;

    }

 public static bool IsActive(string pUsername)
 {
     bool blnReturn = false;
     using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
     {
         using (SqlCommand cmd = cn.CreateCommand())
         {
             cmd.CommandText = "SELECT pstatus FROM Users.Users WHERE username=@username";
             cmd.Parameters.Add(new SqlParameter("@username", pUsername));
             cn.Open();
             string strStatus = cmd.ExecuteScalar().ToString();
             if (strStatus == "1")
             {
                 blnReturn = true;
             }
         }
     }
     return blnReturn;
 }

}