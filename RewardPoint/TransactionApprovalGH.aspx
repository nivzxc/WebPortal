<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="TransactionApprovalGH.aspx.cs" Inherits="RewardPoint_TransactionApprovalGH" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="TransactionMain.aspx" class="SiteMap">Reward Transaction Main</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Reward Transaction Details</span></b> 
     <br />
           <div style="text-align:center;" runat="server" id="divButtons2">
       <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove2" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click"/>--%>
       <asp:Button ID="btnApprove2" runat="server" Text="Approve"  OnClick="btnApprove_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisapprove2" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click"/>--%>
       <asp:Button ID="btnDisapprove2" runat="server" Text="Disapprove" OnClick="btnDisApprove_Click" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnModify2" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click"/> --%>
       <asp:Button ID="btnModify2" runat="server" Text="Needs Modification"  OnClick="btnModify_Click"/>
      <br />
      <br />
     </div>
     <div class="GridBorder">  
        <table width="100%" cellpadding="3" class="Grid">
       <%--  <tr>
          <td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../Support/viewtext22.png" alt="" /></td>
             <td>&nbsp;<b>Transaction Details</b></td>
            </tr>
           </table>         
          </td>
         </tr>  --%>         
         <tr>
          <td class="GridRows" style="width:25%">Category Name:</td>
          <td class="GridRows" style="width75%">
              <asp:Label ID="lblEventName" runat="server" Text="Label"></asp:Label>
             </td>
         </tr>
         <tr>
          <td class="GridRows">Description:</td>
          <td class="GridRows">
              <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
         <tr>
          <td class="GridRows">Created By:</td>
          <td class="GridRows">
              <asp:Label ID="lblCreateBy" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
         <tr>
          <td class="GridRows">Date Created:</td>
          <td class="GridRows">
              <asp:Label ID="lblCreateOn" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
         <tr>
          <td class="GridRows">Status:</td>
          <td class="GridRows">
              <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
        </table>

     </div>     
 
     <br /> 

 
      <div class="GridBorder">
       <table width="100%" cellpadding="0" class="">
<%--        <tr>
         <td colspan="4" class="GridText">
          <table>
           <tr>
            <td>&nbsp;<img src="../Support/cart32.png" alt="" /></td>
            <td>&nbsp;<b>Employee List</b></td>
           </tr>
          </table>         
         </td>
        </tr>--%>
       <tr>
         <td colspan="4">

             <table width="100%" cellpadding="5" cellspacing="1">
               <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>Employee Reward List</b></td></tr>
               <tr>
                <td class="GridColumns" style="width:40%;"><b>Employee Name</b></td>
                <td class="GridColumns" style="width:15%;"><b>Points</b></td>
                <td class="GridColumns" style="width:15%;"><b>Type</b></td>
                <td class="GridColumns" style="width:20%;"><b>Date Acquired</b></td>
               </tr>
                 <asp:Label ID="lblEmployeePointsList" runat="server" Text="Label"></asp:Label>
              </table>

        </td>
       </tr>      
      </table>
     </div>
     
     <br />

    
    <div style="text-align:center;" runat="server" id="divButtons">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click"/>--%>
        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click"/>--%>
      <asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" OnClick="btnDisApprove_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnModify" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click"/> --%>  
      <asp:Button ID="btnModify" runat="server" Text="Needs Modification" OnClick="btnModify_Click"/>
     </div>
             
      
    </div>      
   </td>
  </tr>

 </table>
</asp:Content>

