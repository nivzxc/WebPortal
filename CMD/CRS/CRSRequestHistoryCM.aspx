<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRSRequestHistoryCM.aspx.cs" Inherits="CMD_CRS_CRSRequestHistoryCM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head runat="server">
  <title>Courseware Request History</title>
  <style type="text/css" media="screen">
   <!-- #include file="~\MySTIHQ.css" -->
  </style>    
 </head>
 <body style="background-image:url(../../Support/back.GIF);">
  <form id="frmCRSRequestHistory" runat="server">
   <div>
    <table>
     <tr>
      <td><img src="../../Support/time22.png" alt="" /></td>
      <td>&nbsp;<b><span class="HeaderText">Courseware Request History</span></b></td>
     </tr>
    </table>
    <br />    

    <div class="GridBorder">
     <table width="100%" cellpadding="5" class="grid">
      <tr>
       <td colspan="4" align="center" class="GridText" style="text-align:left;font-size:small;">
        <table>
         <tr>
          <td><img src="../../Support/AppHead.png" alt="" /></td>
          <td>&nbsp;<b>Courseware Dispatch List</b></td>
         </tr>
        </table>           
       </td>
      </tr>
      <tr>
       <td class="GridColumns" style="width:70%"><b>Dispatch Details</b></td>
       <td class="GridColumns" style="width:30%"><b>Dispatch Type</b></td>
      </tr>
      <% LoadRecords(); %>
     </table>
    </div>
    
    <br />
    
    <div style="width:100%;text-align:center;">
     <asp:ImageButton runat="server" ID="btnClose" ImageUrl="~/Support/btnCloseWindow.jpg" OnClick="btnClose_Click" />     
    </div>
   </div>  
  </form>
 </body>
</html>
