<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="AccountBudgetListDivisionViewer.aspx.cs" Inherits="BudgetManagement_AccountBudgetListDivisionViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
      
  <tr runat="server" id="trEncoder">
   <td> 
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;" id="divUser">    
     <br />
     
            <b><span class="HeaderText">Account Budget</span></b>
        <br />
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Fiscal Year:</td>
        <td class="GridRows">
            <asp:DropDownList runat="server" ID="ddlFiscalYear" 
                CssClass="controls" >
            </asp:DropDownList></td>
        <td class="GridRows" rowspan="3">
            <%--<asp:ImageButton runat="server" ID="btnSearch" 
                ImageUrl="~/Support/btnSearch.jpg" onclick="btnSearch_Click" />--%>
            <asp:Button ID="btnSearch"
                    runat="server" Text="Search" onclick="btnSearch_Click"/></td>
       </tr>
       <tr>
        <td class="GridRows">&nbsp;Division:</td>
        <td class="GridRows">
            <asp:DropDownList runat="server" ID="ddlDivision" 
                CssClass="controls">
            </asp:DropDownList></td>
       </tr>
       </table>
        <br /> 
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
         <td class="masterpanel" style="width:5%;">&nbsp;</td>
         <td class="masterpanel" style="width:50%;"><b>RC Group</b></td>
         <td class="masterpanel" style="width:15%;"><b>Budget</b></td>
         <td class="masterpanel" style="width:15%;"><b>Actual</b></td>
         <td class="masterpanel" style="width:15%;"><b>Variant</b></td>
        </tr>
       <% LoadList(); %>
       <asp:Label ID="lblWrite" runat="server" Text=""></asp:Label>

      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>