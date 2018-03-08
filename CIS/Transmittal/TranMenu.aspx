<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="TranMenu.aspx.cs" Inherits="CIS_Transmittal_TranMenu" %>
<%@ Import Namespace="STIeForms" %>
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="TranMenu.aspx" class="SiteMap">Transmittal</a>
    </div>        
   </td>
  </tr>--%>
      
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <%
   if (clsTransmittal.IsApprover(clsTransmittal.TransmittalUserType.SpecialDispatchApprover,Request.Cookies["Speedo"]["UserName"].ToString()) || clsTransmittal.IsApprover(clsTransmittal.TransmittalUserType.SpecialDispatchApprover2,Request.Cookies["Speedo"]["UserName"].ToString()))
   {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Special Dispatch (For Approval)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>Special Transmittal</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Transmittal Details</b></td>
        <td class="GridColumns" style="width:200px;"><b>Status</b></td>
       </tr>
       <% LoadApproverMenu();%>
       <tr><td class="BrowseAll" colspan="3"><a href="TranAllSA.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>                 
     </div>     
    </div>
   </td>
  </tr> 
  <tr><td style="height:9px"></td></tr>
  
  <%  
   }
    
   if (clsTransmittal.IsApprover(clsTransmittal.TransmittalUserType.GroupHead,Request.Cookies["Speedo"]["UserName"].ToString()))
   {
  %>  
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Transmittal For Your Approval (Group Head Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Special Dispatch</b></td>
          </tr>
         </table>
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadGroupHeadMenu();  %>
       <tr><td colspan="3" class="BrowseAll"><a href="TranAllGH.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px;"></td></tr>  
  <%
   }
  %>
             
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Transmittal</span></b>
     <br /><br />
     <%--<asp:ImageButton runat="server" ID="btnNew" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNew_Click" />   --%> 
        <asp:Button ID="btnNew" runat="server" Text="New Request"  OnClick="btnNew_Click"/>    
     <br /><br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Recent Transmittal Submission</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Transmittal Request</b></td>
        <td class="GridColumns" style="width:200px;"><b>Last Action</b></td>
       </tr>
       <% LoadTransmittal(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="TranAll.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
    </div>     
   </td>
  </tr>
   
 </table>
</asp:Content>