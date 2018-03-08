<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="TranAllSA.aspx.cs" Inherits="CIS_Transmittal_TranAllSA" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="TranMenu.aspx" class="SiteMap">Transmittal</a> » 
     <a href="TranAllSA.aspx?mode=a&page=1" class="SiteMap">View All Transmittal</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
       
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">View All Transmittal</span></b>
     <br />
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" class="grid">
      <%-- <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Search22.png" alt="" /></td>
           <td>&nbsp;<b>Search Filter</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:25%;">Request Mode:</td>
        <td class="GridRows" style="width:75%;">
         <asp:RadioButton runat="server" ID="radModeAll" GroupName="RequestMode" Text="View All" AutoPostBack="true" />
         <asp:RadioButton runat="server" ID="radModeProcessed" GroupName="RequestMode" Text="Processed" AutoPostBack="true" />
         <asp:RadioButton runat="server" ID="radModeForApproval" GroupName="RequestMode" Text="For Approval" AutoPostBack="true" />        
        </td>
       </tr>
      </table>
     </div>
     <br />
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" class="grid">
      <%-- <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Processed22.png" alt="" /></td>
           <td>&nbsp;<b>List of All Transmittal</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Transmittal Details</b></td>
        <td class="GridColumns" style="width:200px;"><b>Status</b></td>
       </tr>
       <% LoadTransmittal();%>
       <tr><td class="BrowseAll" style="font-size:small; text-align:left;" colspan="3">&nbsp;Page<% LoadPaging();%></td></tr>
      </table>
     </div>
    </div>     
   </td>
  </tr>
     
 </table>
</asp:Content>