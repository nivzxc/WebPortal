<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="GroupUpdateMain.aspx.cs" Inherits="GroupUpdate_GroupUpdateMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr runat="server" id="trApproverDivision" >
   <td> 
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;">  
<%--     <b><span class="HeaderText">Group Updates For Approval (Division Head Level)</span></b>
     <br />
     <br />--%>
     <br />
     
            <b><span class="HeaderText">Group Updates For Approval</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="masterpanel" style="width:5%;">&nbsp;</td>
        <td class="masterpanel" style="width:95%;"><b>Group Updates Details</b></td>
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
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;">    
<%--     <b><span class="HeaderText">Group Updates For Approval (Group Head Level)</span></b>
     <br />
     <br />--%>
     <br />
     
            <b><span class="HeaderText">Group Updates For Approval</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="masterpanel" style="width:5%;">&nbsp;</td>
        <td class="masterpanel" style="width:95%;"><b>Group Update Details</b></td>
       </tr>
       <% LoadApproverHead(); %>
      </table>
     </div>         
    </div>
   </td>
  </tr>
    
  <tr runat="server" id="trApproverHeadSpacer"><td style="height:9px"></td></tr>

  <tr runat="server" id="trEncoder">
   <td> 
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;" id="divUser">    
     <br />
     
            <b><span class="HeaderText">Submitted Group Updates</span></b>
     <br />
     <br />
          <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnAdd.jpg" 
            OnClick="btnNewRequest_Click" />--%><asp:Button ID="btnNewRequest" runat="server" Text="Create Update" OnClick="btnNewRequest_Click" />
     <br /> 
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
         <td class="masterpanel" style="width:5%;">&nbsp;</td>
         <td class="masterpanel" style="width:60%;"><b>Details</b></td>
         <td class="masterpanel" style="width:35%;"><b>Status</b></td>
        </tr>
       <% LoadUpdates(); %>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>

