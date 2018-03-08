<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EmployeeRewardDetails.aspx.cs" Inherits="RewardPoint_EmployeeRewardDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
   <%--<tr>
    <td >
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="EmployeeRewardList.aspx" class="SiteMap">Reward Summary</a>
     </div>
    </td>
   </tr>
   <tr>
    <td style="height: 9px;">
    </td>
   </tr>--%>
   <tr>
    <td>
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <div class="GridBorder">
       <table width="100%" cellpadding="3" class="Grid">
        <%--<tr>
          <td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../Support/Paper22.png" alt="" /></td>
             <td>&nbsp;<b>Reward Point Details</b></td>
            </tr>
           </table>            
          </td>
         </tr>--%>
        <tr>
         <td class="GridRows">Category Name:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblEventName" Font-Bold="true"></asp:Label>&nbsp;</td>
        </tr>
        <tr>
         <td class="GridRows" style="width:25%;">Description:</td>
         <td class="GridRows" style="width:75%;"><asp:Label runat="server" 
                 ID="lblDescription"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows" valign="top">Points Received:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblPoints"></asp:Label></td>
        </tr>
         <tr>
         <td class="GridRows">Date Acquired:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblDateAcquired"></asp:Label></td>
        </tr>
        <tr>
          <td class="GridRows">Created By:</td>
          <td class="GridRows"><asp:Label runat="server" ID="lblCreateBy"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Create On:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblCreateOn"></asp:Label></td>
        </tr>
       </table>

      </div>
      <br />
            <div style="text-align:center;" runat="server" id="divButtons2" visible="true">
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" 
              OnClick="btnBack_Click"/>--%>
                <asp:Button ID="btnBack" runat="server" Text="Back"  OnClick="btnBack_Click"/>
     </div>
     </div>
    </td>
   </tr>
  </table>
</asp:Content>

