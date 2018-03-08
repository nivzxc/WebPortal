<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

 <head id="Head1" runat="server">
  <title>The Official STI Head Office Website</title>
  <style type="text/css" media="screen">
   <!-- #include file="~\MySTIHQ.css" -->
  </style> 
 </head>
 
 <body style="background-color:#ffffff; background-image:url(back.GIF)">
  <form id="frmLogin" runat="server">
   <br /><br /><br /><br /><br /><br /><br /><br />
   <table style="margin-left:auto; margin-right:auto; width:40%;">
    <tr>
     <td>
      <div style="background-color:#f0f8ff;	border-right: #87ceeb 1px solid;	border-top: #87ceeb 1px solid;	border-left: #87ceeb 1px solid;	border-bottom: #87ceeb 1px solid; padding-bottom:0px;padding-left:0px;padding-right:0px;padding-top:0px; width:100%;">
       <table style="width:100%;">
        <tr>
         <td class="GridText" colspan="2" style="font-size:small;">
          <table>
           <tr>
            <td>&nbsp;<img src="Support/KeyUp32.png" alt="Forgot" /></td>
            <td>&nbsp;Recover Password</td>
           </tr>
          </table>          
         </td>
        </tr>
        <tr>
         <td class="GridRows" style="font-size:small;text-align:center;">
          <table>
           <tr>
            <td>
             Please enter the email address you used on your account, and we will send you an email with your password.
             <br />
             <br />
             Email Address:
             <asp:TextBox CssClass="controls" Font-Size="Small" Width="250px" runat="server" ID="txtUsername" BackColor="White"></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" ID="reqUName" Text="*" Display="Dynamic" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
             <br />
             <br />
             <asp:ImageButton runat="server" ID="btnForgotPassword" ImageUrl="~/Support/btnRecoverPassword.jpg" OnClick="btnForgotPassword_Click" />                          
            </td>
           </tr>
          </table>
          &nbsp;
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