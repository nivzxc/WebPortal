<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRSDispatchDetailsEU.aspx.cs" Inherits="CMD_CRS_CRSDispatchDetailsEU" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head runat="server">
  <title>Courseware Request System: Dispatch Details</title>
  <link rel="Stylesheet" type="text/css" href="../../MySTIHQ.css" />  
 </head>
 <body style="background-image:url(../../Support/back.GIF);">
  <form id="frmCRSDispatchDetailsEU" runat="server">
   <div>
    <table>
     <tr>
      <td><img src="../../Support/education32.png" alt="School" /></td>
      <td>&nbsp;<b><span class="HeaderText">Courseware Material - Dispatch Details</span></b></td>
     </tr>
    </table>
    <br />    
    <div class="GridBorder">
     <table width="100%" cellpadding="3" class="grid">
      <tr>
       <td colspan="2" class="GridText">
        <table>
         <tr>
          <td>&nbsp;<img src="../../Support/viewtext22.png" alt="" /></td>
          <td>&nbsp;<b>Courseware Material Details</b></td>
         </tr>
        </table>         
       </td>
      </tr>      
      <tr>
       <td class="GridRows" style="width:20%">Course:</td>
       <td class="GridRows" style="width:80%">
        <asp:TextBox runat="server" ID="txtCrseCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        -
        <asp:TextBox runat="server" ID="txtCrseTtle" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
       </td>
      </tr>
      <tr>
       <td class="GridRows">Year & Term:</td>
       <td class="GridRows">
        <asp:TextBox runat="server" ID="txtYearTerm" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        &nbsp;
        Availability:
        &nbsp;
        <asp:TextBox runat="server" ID="txtAvailability" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
       </td>
      </tr>
      <tr>
       <td class="GridRows">Status:</td>
       <td class="GridRows">
        <asp:TextBox runat="server" ID="txtStatus" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox>
        &nbsp;
        Request #
        <asp:TextBox runat="server" ID="txtNoReq" CssClass="controls" Width="50px" ReadOnly="true" BackColor="mistyrose"></asp:TextBox>
       </td>
      </tr>
     </table>
    </div>
    
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
       <td class="GridColumns" style="width:60%"><b>Dispatch Details</b></td>
       <td class="GridColumns" style="width:40%"><b>Receiving Details</b></td>
      </tr>
      <% LoadDispatch(); %>
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
