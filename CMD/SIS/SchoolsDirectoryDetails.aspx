<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="SchoolsDirectoryDetails.aspx.cs" Inherits="CMD_SIS_SchoolsDirectoryDetails" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="SchoolsDirectory.aspx" class="SiteMap">School Directory</a> » 
     <a href="SchoolsDirectoryDetails.aspx?schlcode=<%Response.Write(Request.QueryString["schlcode"]); %>&schlname=<%Response.Write(Request.QueryString["schlname"]); %>" class="SiteMap"><%Response.Write(Request.QueryString["schlname"]); %></a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <table>
      <tr>
       <td>&nbsp;<b><span class="HeaderText"><%Response.Write(Request.QueryString["schlname"] + " (" + Request.QueryString["schlcode"] + ")"); %></span></b></td>
      </tr>
     </table>
     <table width="100%">
      <tr>
       <td>
        <table style="font-size:small;">
         <tr>
          <td style="color:#4682b4;">Address:</td>
          <td><asp:Label runat="server" ID="lblAddress"></asp:Label></td>
         </tr>        
         <tr>
          <td style="color:#4682b4;"><asp:Label runat="server" ID="lblCEOLabel"></asp:Label></td>
          <td><asp:Label runat="server" ID="lblCEO"></asp:Label></td>
         </tr>         
         <tr>
          <td style="color:#4682b4;"><asp:Label runat="server" ID="lblCOOLabel"></asp:Label></td>
          <td><asp:Label runat="server" ID="lblCOO"></asp:Label></td>
         </tr>                 
         <tr>
          <td style="color:#4682b4;"><asp:Label runat="server" ID="lblCMLabel"></asp:Label></td>
          <td><asp:Label runat="server" ID="lblCM"></asp:Label></td>
         </tr>
         <tr>
          <td style="color:#4682b4;">Accountant:</td>
          <td><asp:Label runat="server" ID="lblAccountant"></asp:Label></td>
         </tr>         
         <tr>
          <td style="color:#4682b4;">Tel. No:</td>
          <td><asp:Label runat="server" ID="lblTelNmbr"></asp:Label></td>
         </tr>
         <tr>
          <td style="color:#4682b4;">Fax No:</td>
          <td><asp:Label runat="server" ID="lblFaxNmbr"></asp:Label></td>
         </tr>                 
        </table>
       </td>
      </tr>
      <tr><td>&nbsp;</td></tr>
      <tr>
       <td>
        <table>
         <tr>

          <td>&nbsp;<b><span class="HeaderText">Programs Offered</span></b></td>
         </tr>
        </table>
        <br />
	       <div class="GridBorder">
         <table width="100%" cellpadding="5" class="grid">
          <tr>
           <td class="GridColumns">&nbsp;</td>
           <td class="GridColumns"><b>Program Details</b></td>
          </tr>
          <% Load_Programs(); %>
         </table>        
        </div>
        <br /><br />
        <%--<asp:ImageButton runat="server" ID="btnCurriculumOutline" ImageUrl="~/Support/btnCurriculumOutline.jpg" OnClick="btnCurriculumOutline_Click"/>--%>
           <asp:Button ID="btnCurriculumOutline" runat="server" 
               Text="View Curriculum Outline"  OnClick="btnCurriculumOutline_Click" 
               Visible="False"/>
       </td>
      </tr>
     </table>
    </div>
   </td>
  </tr> 
 </table>
</asp:Content>