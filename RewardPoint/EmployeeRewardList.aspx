<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EmployeeRewardList.aspx.cs" Inherits="RewardPoint_EmployeeRewardList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0"> 
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="EmployeeRewardList.aspx" class="SiteMap">My Rewards</a>
    </div>        
   </td>
  </tr>
  <tr><td style="height:9px;"></td></tr>--%>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Rewards Summary</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:20%;"><b>Category</b></td>
        <td class="GridColumns" style="width:40%;"><b>Description</b></td>
        <td class="GridColumns" style="width:20%;"><b>Date Acquired</b></td>
        <td class="GridColumns" style="width:15%;"><b>Points</b></td>
       </tr>
       <% LoadRewards(); %>
      </table>
     </div>
    </div>     
   </td>
  </tr>  
  
 </table>
</asp:Content>

