<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NominationVotee.aspx.cs" Inherits="HR_NominationVotee" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head runat="server">
  <title>Untitled Page</title>
  <style type="text/css" media="screen">
   <!-- #include file="~\MySTIHQ.css" -->
  </style>   
 </head>
 <body>
  <form id="form1" runat="server">
   <br /><br />
   <table width="90%" cellpadding="0" cellspacing="0" class="centermsgbox">    
    <tr>
     <td>
      <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
       <table border="0" width="100%">
        <tr>
         <td>  
          <% Load_Records(); %>
         </td>
        </tr>
       </table>
      </div>
     </td>
    </tr>
   </table>
  </form>
 </body>
</html>
