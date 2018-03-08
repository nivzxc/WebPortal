<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="EditProfile.aspx.cs" Inherits="Userpage_EditProfile" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true"> 
 <table width="100%" cellpadding="0" cellspacing="0">
   
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="ControlPanel.aspx" class="SiteMap">Control Panel</a> » 
     <a href="EditProfile.aspx" class="SiteMap">Edit Profile</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>--%>
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Edit Profile</span></b>
     <br />
     <br />
     <br />
     <br />
     <span class="HeaderText">Username:</span>    
     <br />
     <asp:TextBox runat="server" ID="txtUserName" CssClass="controls" Font-Size="Small" Width="200px" ForeColor="steelblue" ReadOnly="true"></asp:TextBox>          
     <br /><br />
     <span class="HeaderText">Number:</span>    
     <br />
     <asp:TextBox runat="server" ID="txtEmpNum" CssClass="controls" Font-Size="Small" Width="200px" ForeColor="steelblue" ReadOnly="true"></asp:TextBox>
     <br /><br />
     <span class="HeaderText">eMail:</span>
     <br />
     <asp:TextBox runat="server" ID="txtEMail" CssClass="controls" Font-Size="Small" Width="200px" ForeColor="steelblue" ReadOnly="true"></asp:TextBox>     
     <br /><br />     
     <span class="HeaderText">Job Title:</span>    
     <br />
     <span style="color:#4682b4;">Your employee job title here:</span>
     <br />
     <asp:TextBox runat="server" ID="txtEmpTitle" CssClass="controls" Font-Size="Small" Width="200px" ForeColor="steelblue" ReadOnly="true"></asp:TextBox>
     <br /><br />      
     <span class="HeaderText">Your Name:</span>    
     <br />
     <span style="color:#4682b4;">Enter your full name:</span>
     <br />
     <table>
      <tr>
       <td><asp:TextBox runat="server" ID="txtFirstName" CssClass="controls" Font-Size="Small" Width="220px" ForeColor="steelblue"></asp:TextBox></td>
       <td><asp:TextBox runat="server" ID="txtMidName" CssClass="controls" Font-Size="Small" Width="150px" ForeColor="steelblue"></asp:TextBox></td>
       <td><asp:TextBox runat="server" ID="txtLastName" CssClass="controls" Font-Size="Small" Width="150px" ForeColor="steelblue"></asp:TextBox></td>
      </tr>
      <tr>
       <td style="color:#4682b4;">First Name</td>
       <td style="color:#4682b4;">Middle Name</td>
       <td style="color:#4682b4;">Last Name</td>
      </tr>      
     </table>
     <br />
     <br />
     <br />
     <br />     
     <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSave_Click" />
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>