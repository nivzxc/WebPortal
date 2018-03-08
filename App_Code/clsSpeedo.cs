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
using System.Net.Mail;
using System.IO;
using System.Data.SqlClient;
using EASendMail;
using System.Text;

public enum EFormType { Leave, Undertime, Overtime, OfficialBussiness, RFI }

public static class clsSpeedo
{
 public static string SpeedoConnectionString { get { return ConfigurationManager.ConnectionStrings["Speedo"].ToString(); } }

 private static System.Net.Mail.MailAddress mdSpeedoSender = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["SmtpSenderMail"], ConfigurationManager.AppSettings["SmtpSenderName"]);
 private static string strSMTPHost = ConfigurationManager.AppSettings["SmtpServer"];

 public enum UserTypes
 {
  Administrator = 0,
  HQEmployee = 1,
  SchoolEmployee = 2
 }

 public enum SpeedoModules
 {
  MRCF = 0,
  Requisition = 1,
  Transmittal = 2,
  Forums = 3,
  CoursewareRequest = 4,
  GreatPlains = 5,
  CoursewareInventory = 6,
  Finance = 7 //added by IAN : 20100528
 }

 public static bool SendMail(string MsgToEMail, string MsgSubject, string MsgBody)
 {
  try
  {
            //System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient(strSMTPHost);
            //MailMessage msg = new MailMessage();
            //msg.From = mdSpeedoSender;
            //msg.To.Add(new System.Net.Mail.MailAddress(MsgToEMail));
            //msg.Bcc.Add(new System.Net.Mail.MailAddress("hqportal@stihq.net"));
            //msg.IsBodyHtml = true;
            //msg.Priority = System.Net.Mail.MailPriority.Normal;
            //msg.Subject = MsgSubject;
            //msg.Body = MsgBody;
            //sc.Send(msg);
            //sc.UseDefaultCredentials = true;
            //return true;

            //string to = MsgToEMail; //To address    
            //string from = "hoportal@sti.edu"; //From address    
            //MailMessage message = new MailMessage(from, to);

            //string mailbody = MsgBody;
            //message.Subject = MsgSubject;
            //message.Body = mailbody;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true;
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.office365.com", 587); //office365 smtp    
            //System.Net.NetworkCredential basicCredential1 = new
            //System.Net.NetworkCredential("hoportal@sti.edu", "masterP0rtal");
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;

            // ADDED by CALVIN FEB 20, 2018
            string to = MsgToEMail;
            string from = "pficportal@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailbody = MsgBody;
            message.Subject = MsgSubject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587); //gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("pficportal@gmail.com", "pficportal2018");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
      return true;
  }
  catch
  {
   return false;
  }
 }

 public static bool SendMail(string MsgToEMail, System.Net.Mail.MailPriority mpPriority, string MsgSubject, string MsgBody)
 {
  try
  {
            //System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient(strSMTPHost);
            //MailMessage msg = new MailMessage();
            //msg.From = mdSpeedoSender;
            //msg.To.Add(new System.Net.Mail.MailAddress(MsgToEMail));
            //msg.Bcc.Add(new System.Net.Mail.MailAddress("hqportal@stihq.net"));
            //msg.IsBodyHtml = true;
            //msg.Priority = mpPriority;
            //msg.Subject = MsgSubject;
            //msg.Body = MsgBody;
            //sc.Send(msg);
            //return true;

            //string to = MsgToEMail; //To address    
            //string from = "hoportal@sti.edu"; //From address    
            //MailMessage message = new MailMessage(from, to);

            //string mailbody = MsgBody;
            //message.Subject = MsgSubject;
            //message.Priority = mpPriority;
            //message.Body = mailbody;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true;
            //message.CC.Add("hoportal@sti.edu");
            //message.Bcc.Add("hoportal@sti.edu");
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.office365.com", 587); //office365 smtp    
            //System.Net.NetworkCredential basicCredential1 = new
            //System.Net.NetworkCredential("hoportal@sti.edu", "masterP0rtal");
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;

            // ADDED by CALVIN FEB 20, 2018
            string to = MsgToEMail;
            string from = "pficportal@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailbody = MsgBody;
            message.Subject = MsgSubject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587); //gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("pficportal@gmail.com", "pficportal2018");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
      return true;
  }
  catch
  {
   return false;
  }
 }

 public static bool SendMailOffice365(string MsgToEMail, string MsgSubject, string MsgBody)
 {
     try
     {

         string to = "flores.rollie@gmail.com"; //To address    
         string from = "rollie.flores@ho.sti.edu"; //From address    
         MailMessage message = new MailMessage(from, to);

         string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
         message.Subject = "Sending Email Using Asp.Net & C#";
         message.Body = mailbody;
         message.BodyEncoding = Encoding.UTF8;
         message.IsBodyHtml = true;
         message.CC.Add("hoportal@sti.edu");
         message.Bcc.Add("hoportal@sti.edu");
         System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.office365.com", 587); //office365 smtp    
         System.Net.NetworkCredential basicCredential1 = new
         System.Net.NetworkCredential("rollie.flores@ho.sti.edu", "Dofu0004");
         client.EnableSsl = true;
         client.UseDefaultCredentials = false;
         client.Credentials = basicCredential1;
         try
         {
             client.Send(message);
         }

         catch (Exception ex)
         {
             throw ex;
         } 
         return true;
     }
     catch
     {
         return false;
     }
 }

 public static string GetAvatar(string strUserName)
 {
  if (File.Exists(HttpContext.Current.Server.MapPath("~/pictures/avatar/") + strUserName + ".jpg"))
   return strUserName;
  else
   return "default";
 }

 public static string GetRealPicture(string strUserName)
 {
  if (File.Exists(HttpContext.Current.Server.MapPath("~/pictures/realpicture/") + strUserName + ".jpg"))
   return strUserName;
  else
   return "default";
 }

 public static string GetCurrentFiscalQuarter()
 {
  string strReturn = "";
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT quarter FROM HR.Quarter WHERE '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN fromdate AND todate";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   if (dr.Read())
    strReturn = dr["quarter"].ToString();
   dr.Close();
  }
  return strReturn;
 }

 public static string GetCurrentFiscalYear()
 {
  string strReturn = "";
  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT fiscyear FROM HR.Quarter WHERE '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN fromdate AND todate";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   if (dr.Read())
    strReturn = dr["fiscyear"].ToString();
   dr.Close();
  }
  return strReturn;
 }

 public static void Authenticate()
 {
     if (HttpContext.Current.Request.Cookies["Speedo"] == null)
         HttpContext.Current.Response.Redirect("~/MemberLogin.aspx");
 }

 public static void Authenticate(string pUsername)
 {
     if (!clsUsers.IsActive(pUsername))
         HttpContext.Current.Response.Redirect("~/MemberLogin.aspx");
 }

 public static string AssignUsernameLink(string username)
 {
  return "<a href='Userpage/UserPage.aspx?username=" + username + "'>" + username + "</a>";
 }

 public static string AssignUsernameLink(string username, int rootlevel)
 {
  if (rootlevel == 0)
   return "<a href='Userpage/UserPage.aspx?username=" + username + "'>" + username + "</a>";
  else if (rootlevel == 1)
   return "<a href='../Userpage/UserPage.aspx?username=" + username + "'>" + username + "</a>";
  else if (rootlevel == 2)
   return "<a href='../../Userpage/UserPage.aspx?username=" + username + "'>" + username + "</a>";
  else if (rootlevel == 3)
   return "<a href='../../../Userpage/UserPage.aspx?username=" + username + "'>" + username + "</a>";
  else
   return "Error found";
 }

 public static bool IsModuleAllowedAccess(SpeedoModules pModules, string pUsername)
 {
  bool blnReturn = false;
  string strModule = "";
  switch (pModules)
  {
   case SpeedoModules.CoursewareInventory:
    strModule = "CWI";
    break;
   case SpeedoModules.CoursewareRequest:
    strModule = "CWR";
    break;
   case SpeedoModules.Forums:
    strModule = "FORUM";
    break;
   case SpeedoModules.GreatPlains:
    strModule = "GPO";
    break;
   case SpeedoModules.MRCF:
    strModule = "MRCF";
    break;
   case SpeedoModules.Requisition:
    strModule = "REQU";
    break;
   case SpeedoModules.Transmittal:
    strModule = "TRAN";
    break;
   case SpeedoModules.Finance:
    strModule = "023";
    break;
  }

  using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
  {
   SqlCommand cmd = cn.CreateCommand();
   cmd.CommandText = "SELECT username FROM Users.UsersModules WHERE username='" + pUsername + "' AND modlcode='" + strModule + "' AND pstatus='1'";
   cn.Open();
   SqlDataReader dr = cmd.ExecuteReader();
   blnReturn = dr.Read();
   dr.Close();
  }
  return blnReturn;
 }

 public static string GetDateDetails(DateTime pDateFlag)
 {
  string strReturn = "";
  TimeSpan tsDiff = DateTime.Now - pDateFlag;

  int intDiffDays = tsDiff.Days;
  int intDiffHours = tsDiff.Hours;
  if (intDiffDays == 0)
  {
   if (intDiffHours == 0)
    strReturn = "a while ago";
   else
    strReturn = "last " + intDiffHours + " hour" + (intDiffHours > 1 ? "s" : "");
  }
  else
  {
   strReturn = "last " + intDiffDays + " day" + (intDiffDays > 1 ? "s" : "");
  }
  return strReturn;
 }

}