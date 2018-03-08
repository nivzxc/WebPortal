<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MRCFAll.aspx.cs" Inherits="CIS_MRCF_MRCFAll" %>

<asp:Content ID="conDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="MRCFMenu.aspx" class="SiteMap">MRCF</a> » 
     <a href="MRCFAll.aspx?page=1" class="SiteMap">Browse All MRCF</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>--%>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Sent MRCF</span></b>
     <br />
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" class="grid">
      <%-- <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Search22.png" alt="" /></td>
           <td>&nbsp;<b>Search</b></td>
          </tr>
         </table>            
        </td>
       </tr> --%>
       <tr>
        <td class="GridRows" style="width:25%;">Request Status:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlRequestStatus" CssClass="controls" BackColor="white">
          <asp:ListItem Text="View All" Value="all"></asp:ListItem>
          <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
          <asp:ListItem Text="Disapproved" Value="D"></asp:ListItem>
          <asp:ListItem Text="For Approval" Value="F"></asp:ListItem>
          <asp:ListItem Text="For Modification" Value="M"></asp:ListItem>
          <asp:ListItem Text="Void" Value="V"></asp:ListItem>
         </asp:DropDownList>
        </td>
        <td class="GridRows" rowspan="2" style="text-align:center;">
        <%-- <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" OnClick="btnSearch_Click"/>--%>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"/>
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:25%;">Request Type:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlRequestType" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>
      </table>
     </div>
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<b>List of Sent MRCF</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:30%;"><b>Status</b></td>
       </tr>
       <% LoadMRCF();%>
       <tr><td class="GridColumns" style="font-size:small;text-align:left;" colspan="3">&nbsp;<b>Page</b><% LoadPaging();%></td></tr>
      </table>           
     </div>
    </div>     
   </td>
  </tr> 
  
 </table>
</asp:Content>