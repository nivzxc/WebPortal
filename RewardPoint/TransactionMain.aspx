<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="TransactionMain.aspx.cs" Inherits="RewardPoint_TransactionMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">

  <%--<tr>
   <td style="height: 34px">
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="TransactionMain.aspx" class="SiteMap">Reward Transaction Main</a> 
    </div>        
   </td>
  </tr>
  <tr><td style="height:9px;"></td></tr>
--%>
  <tr runat="server" id="trApproverDivision" >
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Reward Transaction For Your Approval (Division Head Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
      <%-- <tr><td colspan="2" align="center" class="GridText">&nbsp;<b>Reward Transaction For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:95%;"><b>Transaction Details</b></td>
       </tr>
       <% LoadApproverDivision(); %>
      </table>
     </div>    
    </div>
   </td>
  </tr>

  <tr runat="server" id="trApproverDivisionSpacer"><td style="height:9px"></td></tr>

  <tr runat="server" id="trApproverHead">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Reward Transaction For Your Approval (Within Department)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <%--<tr><td colspan="2" align="center" class="GridText">&nbsp;<b>Reward Transaction For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:95%;"><b>Transaction Details</b></td>
       </tr>
       <% LoadApproverHead(); %>
      </table>
     </div>         
    </div>
   </td>
  </tr>
  <tr runat="server" id="trApproverHeadSpacer"><td style="height:9px"></td></tr>
    
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Reward Transaction List</span></b>
     <br />
     <br />
     <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" onclick="btnNewRequest_Click" />--%>
        <asp:Button ID="btnNewRequest" runat="server" Text="New Request"  onclick="btnNewRequest_Click" />
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
     <%--  <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>Reward Transaction Submission</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Reward Transaction Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadEncoder(); %>
      </table>
     </div>
    </div>     
   </td> 
  </tr>     
 </table>  
</asp:Content>

