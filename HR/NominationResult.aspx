<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NominationResult.aspx.cs" Inherits="HR_NominationResult" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>MySTIHQ Nomination</title>
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
          <table>
           <tr>
            <td><img src="../Support/Approve32.png" alt="" /></td>
            <td><b><span class="HeaderText">STI Core Values Awards - Results</span></b></td>
           </tr>
          </table>           
         </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr runat="server" id="trValidation">
         <td>
          <table>
           <tr>
            <td><img src="../Support/User.png" alt="" /></td>
            <td><span class="HeaderText">Employee Number:</span></td>
            <td>
             <asp:TextBox runat="server" ID="txtEmpNum" CssClass="controls" Font-Size="Small" ForeColor="steelblue" Width="199px" MaxLength="15"></asp:TextBox>
             <asp:RequiredFieldValidator ID="reqEmpNum" ControlToValidate="txtEmpNum" runat="server" ErrorMessage="[Employee Number is required]" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td rowspan="2">
             <asp:ImageButton runat="server" ID="btnValidate" ImageUrl="~/Support/btnValidate.jpg" OnClick="btnValidate_Click" />
            </td>
           </tr>
           <tr>
            <td><img src="../Support/Shout22.png" alt="" /></td>
            <td><span class="HeaderText">Middle Name:</span></td>
            <td>
             <asp:TextBox runat="server" ID="txtMidName" CssClass="controls" Font-Size="Small" ForeColor="steelblue" Width="199px" MaxLength="15"></asp:TextBox>
             <asp:RequiredFieldValidator ID="reqMidName" ControlToValidate="txtMidName" runat="server" ErrorMessage="[Middle Name is required]" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
           </tr>           
          </table>              
         </td>
        </tr>        
        <tr runat="server" id="trTally" visible="false">
         <td>
          <span style="color:#4682b4;font-size:small;">Select Division:</span>
          <asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls"></asp:DropDownList>
          <asp:Button runat="server" ID="btnSearch" Text=" Show " OnClick="btnSearch_Click" />
          <asp:Button runat="server" ID="btnDownload" Text=" Download " OnClick="btnDownload_Click" />
          <asp:CheckBox runat="server" ID="chkTop3" Text="View Top 3" />
          <% LoadRecords(); %>
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