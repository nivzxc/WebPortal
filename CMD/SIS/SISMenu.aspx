<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="SISMenu.aspx.cs" Inherits="CMD_SIS_SISMenu" %>

<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="SISMenu.aspx" class="SiteMap">SIS</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>--%>

  <%
      //if (clsSIS.IsUserValid(clsSIS.SISUsers.Encoder,Request.Cookies["Speedo"]["UserName"]))
  if(clsSystemModule.HasAccess("999", Request.Cookies["Speedo"]["UserName"].ToString()))
  {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Philippine First Branch Settings</span></b>
     <br />
     <br />
     <table>
      <tr>       
       <td>&nbsp;<a style="text-decoration:none;" href="BranchesSettings.aspx" style="font-size:small;"><img src="../../Support/Sign-Add-icon.png" alt="" width="22px" height="22px"/>  Add New Branch</a></td>
      </tr>
      <tr>
       <td>&nbsp;<a href="../Checklist/CheckListContent.aspx" style="font-size:small;">Checklist</a></td>
      </tr>
     </table>        
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
  <% 
  }
  %>
     
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Philippine First Branch Directory</span></b>
     <br />
     <br />
     <table>
      <tr>       
       <td>&nbsp;<a style="text-decoration:none;" href="SchoolsDirectory.aspx" style="font-size:small;"> <img src="../../Support/browser22.png" alt="" />  Branch Directory</a></td>
      </tr>
     </table>     
    </div>     
   </td>
  </tr>
     
 </table>  
</asp:Content>