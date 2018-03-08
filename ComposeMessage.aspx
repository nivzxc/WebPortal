<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ComposeMessage.aspx.cs" Inherits="ComposeMessage" %>

<asp:Content ID="cntLeaveNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
 <table width="100%" cellpadding="0" cellspacing="0"> 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="Default.aspx" class="SiteMap">Home</a> » 
     <a href="Notifications.aspx" class="SiteMap">Notifications</a> » 
     <a href="ComposeMessage.aspx" class="SiteMap">Compose Message</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Compose Message</span></b>
     <br />

     <div runat="server" id="divError" visible="false">
      <br />
      <div class="ErrMsg"> 
       <b>Error during update. Please correct your data entries:</b><br />
       <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
     </div>
    
     <br />    
     <br />     
            
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td colspan="2" class="GridText" style="height:20px;">&nbsp;<b>Application For Leave Details</b></td></tr>
       <tr>
        <td class="GridRows" style="width:20%">From:</td>
        <td class="GridRows" style="width:80%"><asp:TextBox runat="server" ID="txtFrom" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>           
       <tr>
        <td class="GridRows">To:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtTo" CssClass="controls" BackColor="White" Width="98%" ValidationGroup="insert"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqTo" ErrorMessage="<br>[To field is required]" Display="Dynamic" ControlToValidate="txtTo" SetFocusOnError="true" ValidationGroup="insert"></asp:RequiredFieldValidator>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Subject:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSubject" CssClass="controls" Width="99%" MaxLength="100" BackColor="white" ValidationGroup="insert"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqSubject" ErrorMessage="<br>[Subject is required]" Display="Dynamic" ControlToValidate="txtSubject" SetFocusOnError="true" ValidationGroup="insert"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Body:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtBody" CssClass="controls" Width="99%" TextMode="MultiLine" Rows="10" BackColor="white" ValidationGroup="insert"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqBody" ErrorMessage="<br>[Subject is required]" Display="Dynamic" ControlToValidate="txtBody" SetFocusOnError="true" ValidationGroup="insert"></asp:RequiredFieldValidator>
        </td>
       </tr>       
      </table>     
     </div>
    
     <br />

     <div style="text-align:center;">
      <asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnBack.jpg" onclick="btnSend_Click" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />
     </div>     
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>