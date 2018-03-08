<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SessionExpired.aspx.cs" Inherits="SessionExpired" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head runat="server">
  <title>The Official STI Head Office Website</title>
  <style type="text/css" media="screen">
   <!-- #include file="~\MySTIHQ.css" -->
  </style>   
 </head>
 <body style="background-color:#ffffff">
  <form id="frmExpired" runat="server">
   <br /><br /><br /><br /><br /><br /><br /><br /><br />
   <table class="centermsgbox" cellpadding="20">
    <tr>
     <td>
      <table style="font-size:small;font-family:Verdana;">
       <tr>
        <td><img src="Support/error128.png" alt="" /></td>
        <td>&nbsp;&nbsp;</td>
        <td>
         You have to log-in first before you can access this page.<br />
         Kindly go to <a href="MemberLogin.aspx">Login</a> page
        </td>
       </tr>      
      </table>       
     </td>
    </tr>
   </table>   
  </form>
 </body>
</html>
