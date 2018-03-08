<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="ShoutBox.aspx.cs" Inherits="ShoutBox" %>

<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="Default.aspx" class="SiteMap">Home</a> » 
     <a href="shoutbox.aspx" class="SiteMap">Shoutbox</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="4" align="center" class="GridText" style="text-align:left;font-size:small;">
         <table>
          <tr>
           <td><img src="Support/Shout22.png" alt="" /></td>
           <td>&nbsp;<b>Shout Box Messages</b></td>
          </tr>
         </table>           
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%">&nbsp;</td>
        <td class="GridColumns" style="width:30%"><b>Shouter</b></td>
        <td class="GridColumns" style="width:65%"><b>Shouted Message</b></td>
       </tr>
       <%LoadShoutBox();  %>
       <tr><td class="GridColumns" style="font-size:small;text-align:left;" colspan="3">&nbsp;<b>Page</b><% LoadPaging();%></td></tr>
      </table>
     </div>
    
     <br />
    </div>
   </td>
  </tr>
 </table>
</asp:Content>

