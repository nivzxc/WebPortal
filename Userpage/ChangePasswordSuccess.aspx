<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePasswordSuccess.aspx.cs" Inherits="Userpage_ChangePasswordSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>Change Password</title>
  <script type="text/javascript" language="javascript">
   <!-- 
    var x = 3 
    var y = 1 
    function startClock(strPao)
    { 
     x = x-y 
     setTimeout("startClock()", 1000) 
     if(x==0)
     { 
      window.location = "ChangePassword.aspx"; 
      x=10; 
     } 
    } 
    -->
  </script>
  <style type="text/css" media="screen">
   <!-- #include file="~\MySTIHQ.css" -->
  </style>  
 </head>
 <body style="background-image:url(http://hq.sti.edu/Support/back.gif)" onload="startClock()">
  <form id="frmDone" runat="server">
   <br /><br /><br /><br /><br /><br /><br /><br /><br />
   <table class="centermsgbox" cellpadding="20" >
    <tr>
     <td>
      <span style="font-size:small;font-family:Verdana;">
       Thank you
       <br />
       <br />
       Changing password complete!
       <br />
       <br />
       Please wait while we transfer you... 
       <br />
       <br />
       <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Userpage/ChangePassword.aspx" Text="(Or click here if you do not wish to wait)"></asp:HyperLink>
      </span>
     </td>
    </tr>
   </table>

  </form>
 </body>
</html>