<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubRate.aspx.cs" Inherits="CMD_WIRE_SubRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">
  <title>Schools Submission per CM</title>
  <link rel="Stylesheet" type="text/css" href="MySTIHQ.css" />
 </head>
 <body style="background-image:url(../../Support/back.GIF);">
  <form id="frmSubRate" runat="server">
   <table width="100%">
    <tr><td><asp:Label runat="server" ID="lblDetails" Font-Size="Small"></asp:Label></td></tr>
    <tr>
     <td style="font-size:small">
      <table>
       <tr><td colspan="2"><b>Schools Submission - Grouped by Channel Managers</b></td></tr>
       <tr><td colspan="2">&nbsp;</td></tr>
       <tr><td colspan="2"><b>Legend</b></td></tr>
       <tr>
        <td style="background-color:#ffe4e1;">&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td>&nbsp;- No update since last two days.</td>
       </tr>
       <tr>
        <td style="background-color:#f0f8ff;">&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td>&nbsp;- Last update today or yesterday.</td>
       </tr>       
      </table>
     </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
     <td>
      <div class="GridBorder" style="overflow: auto;">
       <table width="100%" cellpadding="5" class="grid">
        <tr>
         <td class="GridColumns"><b>Channel Manager</b></td>
         <td class="GridColumns"><b>School</b></td>         
         <td class="GridColumns"><b>Last Update</b></td>
        </tr>
     <%--   <% Load_Submission();%>--%>
       </table>
      </div> 
     </td>
    </tr>
   </table>
  </form>
 </body>
</html>
