<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<script runat="server">

 void Application_Start(object sender, EventArgs e) 
 {
     String _path = String.Concat(System.Environment.GetEnvironmentVariable("PATH"), ";", System.AppDomain.CurrentDomain.RelativeSearchPath);
     System.Environment.SetEnvironmentVariable("PATH", _path, EnvironmentVariableTarget.Process);
 }
    
 void Application_End(object sender, EventArgs e) 
 {
 
 }
        
 void Application_Error(object sender, EventArgs e) 
 { 

 }

 void Session_Start(object sender, EventArgs e) 
 {
     //try
     //{
     //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
     //    {
     //        SqlCommand cmd = cn.CreateCommand();
     //        cn.Open();
     //        cmd.CommandText = "UPDATE Users.Users SET online='1',lastlog='" + DateTime.Now + "' WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
     //        cmd.ExecuteNonQuery();
     //    }
     //}
     //catch
     //{
     //}  
 }

 void Session_End(object sender, EventArgs e) 
 {
     //try
     //{
     //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
     //    {
     //        SqlCommand cmd = cn.CreateCommand();
     //        cn.Open();
     //        cmd.CommandText = "UPDATE Users.Users SET online='0' WHERE username='" + Request.Cookies["Speedo"]["UserName"] + "'";
     //        cmd.ExecuteNonQuery();
     //    }
     //}
     //catch
     //{ }
 }
       
</script>
