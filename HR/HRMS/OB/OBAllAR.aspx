<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="OBAllAR.aspx.cs" Inherits="HR_HRMS_OB_OBAllAR" %>

<asp:Content ID="conDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OBMenu.aspx" class="SiteMap">OB</a> » 
     <a href="OBAllAR.aspx?page=1" class="SiteMap">All OB</a>
    </div>        
   </td>
  </tr>--%>
     
<%--  <tr><td style="height:9px;"></td></tr>--%>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">All OB Applications</span></b>
     <br />
     <br />
     <div class="GridBorder" style="width:60%;"> 	          
      <table width="100%" cellpadding="5">
     <%--  <tr>
        <td colspan="2" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../../Support/Search22.png" alt="" /></td>
           <td>&nbsp;<b>Search</b></td>
          </tr>
         </table>            
        </td>
       </tr> --%>
       <tr>
        <td class="GridRows">OB Status:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlStatus" CssClass="controls" BackColor="white" AutoPostBack="true">
          <asp:ListItem Text="View All" Value="all"></asp:ListItem>
          <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
          <asp:ListItem Text="Disapproved" Value="D"></asp:ListItem>
          <asp:ListItem Text="For Approval" Value="F"></asp:ListItem>
         </asp:DropDownList>
        </td>
       </tr>
      </table>
     </div>
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" cellspacing="1">
       <%--<tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>List of Processsed OB Applications</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>OB Details</b></td>
        <td class="GridColumns" style="width:30%;"><b>Status</b></td>
       </tr>
       <% LoadRecords();%>
       <tr><td class="BrowseAll" style="font-size:small;text-align:left;" colspan="3">&nbsp;<b>Page</b><% LoadPaging();%></td></tr>
      </table>           
     </div>
     <br />
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" />
     </div>     
    </div>     
   </td>
  </tr> 
  
 </table>
</asp:Content>