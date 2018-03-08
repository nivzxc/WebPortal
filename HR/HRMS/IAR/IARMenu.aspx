<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="IARMenu.aspx.cs" Inherits="HR_HRMS_IAR_IARMenu" %>

<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="IARMenu.aspx" class="SiteMap">Internet Access Request</a>
    </div>
   </td>
  </tr>--%>
<%--  <tr><td style="height:9px;"></td></tr>--%>

  <tr runat="server" id="trEliteUser">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <a href="IARAllEU.aspx" style="font-size:small;">View Approved Internet Access Request</a>   
    </div>
   </td>
  </tr>
  <tr runat="server" id="trEliteUserSpacer"><td style="height:9px"></td></tr>
   
  <tr runat="server" id="trApproverDivision">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Internet Access Request For Your Approval (Division Head Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
<%--       <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>List of Internet Access Request For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadApproverDivision(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="IARAllD.aspx?page=1" style="font-size:small;">[View All Records]</a></td></tr>
      </table>
     </div>    
    </div>
   </td>
  </tr>
  <tr runat="server" id="trApproverDivisionSpacer"><td style="height:9px"></td></tr>

  <tr runat="server" id="trApproverHead">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Internet Access Request For Your Approval (Within Department)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
<%--       <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>List of Internet Access Request For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadApproverHead(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="IARAllH.aspx?page=1" style="font-size:small;">[View All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>
  <tr runat="server" id="trApproverHeadSpacer"><td style="height:9px"></td></tr>
    
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Internet Access Request</span></b>
     <br />
     <br />
     <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" onclick="btnNewRequest_Click" />--%>
        <asp:Button ID="btnNewRequest" runat="server" Text="New Request"  onclick="btnNewRequest_Click"/>
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
<%--       <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>Recent Internet Access Request Submission</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadIAR(); %>
       <tr><td class="BrowseAll" colspan="3"><a href="IARAll.aspx?page=1" style="font-size:small;">[View All Records]</a></td></tr>
      </table>
     </div>
    </div>     
   </td> 
  </tr>     
 </table>  
</asp:Content>