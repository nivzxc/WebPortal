<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>Philippine First Insurance Inc.</title>
  <link rel="Stylesheet" href="MySTIHQ.css" />
  <script type="text/javascript" language="javascript">
   <!-- 
    var x = 5 
    var y = 1 
    function startClock()
     { 
     x = x-y 
     setTimeout("startClock()", 1000) 
     if(x==0)
      { 
      window.location = "MemberLogin.aspx" 
      x=10; 
      } 
     } 
    -->
  </script>
 </head>
 <body style="background-color:#ffffff; background-image:url(Support/back.GIF);margin:0;" onload="startClock()">
  <form id="frmDone" runat="server">
   <table style="width:100%;" cellpadding="0" cellspacing="0">
    <tr>
     <td style="background-image:url(Support/LoginHeader.png);height:100px;">      
     </td>
    </tr>
   </table>  
   <br />
   <br />
   <br />
   <br />
   <br />
   <table class="centermsgbox" cellpadding="20">
    <tr>
     <td>
      <span style="font-size:small;font-family:Verdana;">
       Thank you
       <br />
       <br />
       You are now logged out.
       <br />
       <br />
       Please wait while we transfer you... 
       <br />
       <br />
       <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/MemberLogin.aspx" Text="(Or click here if you do not wish to wait)"></asp:HyperLink>
      </span>     
     </td>
    </tr>
   </table>
  </form>
 </body>
</html>