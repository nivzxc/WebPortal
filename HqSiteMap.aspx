<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="HqSiteMap.aspx.cs" Inherits="HqSiteMap" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true"> 
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="Default.aspx" class="SiteMap">Home</a> » 
     <a href="hqsitemap.aspx" class="SiteMap">Site Map</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>
     
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Headquarters Site Map</span></b>
     <br />
     <br />     
     <asp:SiteMapDataSource runat="server" ID="smdSiteMap" />
     <asp:TreeView runat="server" ID="tvSiteMap" DataSourceID="smdSiteMap" Font-Size="Small"></asp:TreeView>
    </div>        
   </td>
  </tr>     
  
 </table> 
</asp:Content>