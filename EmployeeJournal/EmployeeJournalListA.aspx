<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EmployeeJournalListA.aspx.cs" Inherits="EmployeeJournal_EmployeeJournalListA" %>

<%@ Register assembly="GMDatePicker" namespace="GrayMatterSoft" tagprefix="cc1" %>

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
&nbsp;<div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Fiscal Year:</td>
        <td class="GridRows" colspan="3">
            <asp:DropDownList runat="server" ID="ddlJournalYear" 
                CssClass="controls" 
                onselectedindexchanged="ddlJournalYear_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList></td>
        <td class="GridRows" rowspan="6">
            <%--<asp:ImageButton runat="server" ID="btnSearch" 
                ImageUrl="~/Support/btnSearch.jpg" onclick="btnSearch_Click" />--%>
            <asp:Button ID="btnSearch"
                    runat="server" Text="Search" onclick="btnSearch_Click"/></td>
       </tr>
       <tr>
        <td class="GridRows">Journal Week:</td>
        <td class="GridRows" colspan="3">
            <asp:DropDownList runat="server" ID="ddlJournalDates" 
                CssClass="controls">
            </asp:DropDownList></td>
       </tr>
       <tr>
        <td class="GridRows">Division:</td>
        <td class="GridRows" colspan="3">
            <asp:DropDownList runat="server" ID="ddlDivision" 
                CssClass="controls" AutoPostBack="True" 
                onselectedindexchanged="ddlDivision_SelectedIndexChanged">
            </asp:DropDownList></td>
       </tr>
      <tr>
        <td class="GridRows">Department:</td>
        <td class="GridRows" colspan="3">
            <asp:DropDownList runat="server" ID="ddlDepartment" 
                CssClass="controls" AutoPostBack="True" 
                onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
            </asp:DropDownList></td>
       </tr>
      <tr>
        <td class="GridRows">Employee:</td>
        <td class="GridRows" colspan="3">
            <asp:DropDownList runat="server" ID="ddlEmployee" 
                CssClass="controls">
            </asp:DropDownList></td>
       </tr>
      </table>
     </div>
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
         <td class="masterpanel" style="width:5%;">&nbsp;</td>
         <td class="masterpanel" style="width:60%;"><b>Details</b></td>
         <td class="masterpanel" style="width:35%;"><b>Last Modified On</b></td>
        </tr>
       <asp:Label ID="lblWrite" runat="server" Text=""></asp:Label>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>

