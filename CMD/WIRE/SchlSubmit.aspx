<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchlSubmit.aspx.cs" Inherits="CMD_WIRE_SchlSubmit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">
  <title>Schools Submission</title>
  <style type="text/css" media="screen">
   <!-- #include file="~\MySTIHQ.css" -->
  </style>  
 </head>
 <body style="background-image:url(../../Support/back.GIF);">
  <form id="frmSchlSubmit" runat="server">
   <table width="100%">
    <tr><td><asp:Label runat="server" ID="lblDetails" Font-Size="Small"></asp:Label></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
     <td>
      <div class="GridBorder" style="overflow: auto;">
       <table width="100%" cellpadding="5" class="grid">
        <tr>
         <td class="GridColumns"><b>School</b></td>
         <td class="GridColumns"><b>Channel Manager</b></td>
         <td class="GridColumns"><b>Last Update</b></td>
        </tr>
      <%--  <% Load_Submission();%>--%>
       </table>
      </div> 
     </td>
    </tr>
   </table>   
  </form>
 </body>
</html>
