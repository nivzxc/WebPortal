<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserpageLog.aspx.cs" Inherits="Userpage_UserpageLog" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">
  <title></title>
 </head>
 <body style="font-size:small;">
  <form id="form1" runat="server">
   <div>
    <%LoadLogList(); %>
   </div>
  </form>
 </body>
</html>