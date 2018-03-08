<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EmployeeJournalList.aspx.cs" Inherits="EmployeeJournal_EmployeeJournalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
      
  <tr runat="server" id="trEncoder">
   <td> 
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;" id="divUser">    
     <br />
     
            <b><span class="HeaderText">Journal History</span></b>
        <br />
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Fiscal Year:</td>
        <td class="GridRows">
            <asp:DropDownList runat="server" ID="ddlJournalYear" 
                CssClass="controls" 
                onselectedindexchanged="ddlJournalYear_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList></td>
        <td class="GridRows" rowspan="3">
            <%--<asp:ImageButton runat="server" ID="btnSearch" 
                ImageUrl="~/Support/btnSearch.jpg" onclick="btnSearch_Click" />--%>
            <asp:Button ID="btnSearch"
                    runat="server" Text="Search" onclick="btnSearch_Click"/></td>
       </tr>
       <tr>
        <td class="GridRows">Journal Week:</td>
        <td class="GridRows">
            <asp:DropDownList runat="server" ID="ddlJournalDates" 
                CssClass="controls" 
                onselectedindexchanged="ddlJournalDates_SelectedIndexChanged">
            </asp:DropDownList></td>
       </tr>
       </table>
     <br />
          <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnAdd.jpg" 
            OnClick="btnNewRequest_Click" />--%>
        <asp:Button ID="btnGoToJournal" 
            runat="server" Text="My Journal" onclick="btnGoToJournal_Click" />
     &nbsp;<asp:Button ID="btnEmployeesJournal" 
            runat="server" Text="Teams' Journal" Visible="False" 
            onclick="btnEmployeesJournal_Click" />
     &nbsp;<asp:Button ID="btnMonitoring" 
            runat="server" Text="Submission Monitoring" Visible="False" 
            onclick="btnMonitoring_Click" />
     &nbsp;<asp:Button ID="btnWeekMap" 
            runat="server" Text="Week Map" onclick="btnWeekMap_Click" 
            BackColor="#0066FF" ForeColor="White" />
     &nbsp;<asp:Button ID="btnReviewers" 
            runat="server" Text="Reviewers"
            BackColor="#0066FF" ForeColor="White" onclick="btnReviewers_Click" />
     <br /> 
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
         <td class="masterpanel" style="width:5%;">&nbsp;</td>
         <td class="masterpanel" style="width:60%;"><b>Details</b></td>
         <td class="masterpanel" style="width:35%;"><b>Last Modified On</b></td>
        </tr>
       <% LoadUpdates(); %>
       <asp:Label ID="lblWrite" runat="server" Text=""></asp:Label>

      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>
