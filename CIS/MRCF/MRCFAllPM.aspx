<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MRCFAllPM.aspx.cs" Inherits="CIS_MRCF_MRCFAllPM" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="MRCFMenu.aspx" class="SiteMap">MRCF</a> » 
     <a href="MRCFAllPM.aspx?mode=a&page=1" class="SiteMap">View All MRCF</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
       
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">View All MRCF</span></b>
     <br />
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" class="grid">
       <%--<tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Processed22.png" alt="" /></td>
           <td>&nbsp;<b>List of All MRCF</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:150px;">Request Mode:</td>
        <td class="GridRows">
         <table>
          <tr>
           <td><asp:RadioButton runat="server" ID="radModeAll" GroupName="RequestMode" Text="All" AutoPostBack="true" /></td>
           <td><asp:RadioButton runat="server" ID="radModeForApproval" GroupName="RequestMode" Text="For Approval (Approved by approver)" AutoPostBack="true" /></td>
          </tr>
          <tr>
           <td><asp:RadioButton runat="server" ID="radModeProcessed" GroupName="RequestMode" Text="Processed" AutoPostBack="true" /></td>
           <td><asp:RadioButton runat="server" ID="radModeForApprovalGHDH" GroupName="RequestMode" Text="For Approval (Not yet approved by approver)" AutoPostBack="true" /></td>
          </tr>          
         </table>         
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
           <td>&nbsp;<b>List of MRCF</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:25px;">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>MRCF Details</b></td>
        <td class="GridColumns"><b>Status</b></td>
       </tr>
       <% LoadMRCF();%>
       <tr><td class="BrowseAll" style="font-size:small; text-align:left;" colspan="3">&nbsp;Page<% LoadPaging();%></td></tr>
      </table>
     </div>
    </div>     
   </td>
  </tr>
     
 </table>
</asp:Content>
