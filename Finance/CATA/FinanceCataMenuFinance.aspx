<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="FinanceCataMenuFinance.aspx.cs" Inherits="Finance_CATA_FinanceCataMenuFinance" %>

<%@ Import Namespace="STIeForms" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
  <%--<tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../FinanceMain.aspx" class="SiteMap">Finance</a> » 
     <a href="FinanceCataMenu.aspx" class="SiteMap">Request for CATA</a>
    </div>        
   </td>
  </tr>      
  <tr><td style="height:9px;"></td></tr>--%>
  <tr>
   <td> 
   
     <%--For Finance--%>
    <%
     if (clsSystemModule.HasAccess("023",Request.Cookies["Speedo"]["UserName"].ToString()))
      {
    %>
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="divFinance">    
      <b><span class="HeaderText">Request for CATA (Finance Group)</span></b>
      <br />
       <br />
     <div class="GridBorder" style="width:500px;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows">Filter by:</td>
        <td class="GridRows" colspan="3"><asp:DropDownList runat="server" ID="ddlFinance" 
                CssClass="controls"></asp:DropDownList></td>
        <td class="GridRows" rowspan="2">
            <%--<asp:ImageButton runat="server" ID="btnSearch" 
                ImageUrl="~/Support/btnSearch.jpg" onclick="btnSearch_Click" />--%><asp:Button ID="btnSearch"
                    runat="server" Text="Search"  onclick="btnSearch_Click" /></td>
       </tr>
       <tr>
        <td class="GridRows">From:</td>
        <td class="GridRows"><cc1:gmdatepicker ID="dtpStart" runat="server" 
                CssClass="controls" DisplayMode="Label" DateFormat="MMM dd, yyyy" 
                BackColor="white" CalendarTheme="Blue"></cc1:gmdatepicker></td>       
        <td class="GridRows">To:</td>
        <td class="GridRows"><cc1:gmdatepicker ID="dtpEnd" runat="server" 
                CssClass="controls" DisplayMode="Label" DateFormat="MMM dd, yyyy" 
                BackColor="white" CalendarTheme="Blue"></cc1:gmdatepicker></td>        
       </tr>
      </table>
     </div>
     <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="5" cellspacing="1">
      
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:35%;"><b>CATA Details</b></td>
         <td class="GridColumns" style="width:60%;"><b>Status</b></td>
        </tr>
     <% LoadMenuCATAFinance(); %>
<%--        <tr><td colspan="3" class="GridColumns"><a href="FinanceCataMenuAll.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>--%>
       </table>
      </div>         
     </div>
     <%
    }
    %>
   </td>
  </tr>  
 </table>  
</asp:Content>

