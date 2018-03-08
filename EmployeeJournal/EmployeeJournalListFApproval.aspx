<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EmployeeJournalListFApproval.aspx.cs" Inherits="EmployeeJournal_EmployeeJournalListFApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
      
  <tr runat="server" id="trEncoder">
   <td> 
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;" id="divUser">    
     <br />
     
            <b><span class="HeaderText">Journal Approval</span></b>
     <br /> 
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
         <td class="masterpanel" style="width:10%;">&nbsp;</td>
         <td class="masterpanel" style="width:90%;"><b>Details</b></td>
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
