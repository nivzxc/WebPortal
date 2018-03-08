<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="FinanceCataMenu.aspx.cs" Inherits="Finance_FinanceCataMenu" Title="The Official STI Head Office Website" %>
<%@ Import Namespace="STIeForms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
<%--  <tr>
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
  
     <%--For Approver--%>
    <%
     if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"].ToString()))
      {
    %>
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="div1">    
      <b><span class="HeaderText">Request for Travel Allowance (For Approval)</span></b>
      <br />
      <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="5" class="Grid">
        <%--<tr>
         <td colspan="4" align="center" class="GridText">
          <table>
           <tr>
            <td>&nbsp;<b>List of Request for CATA</b></td>
           </tr>
          </table>           
         </td>
        </tr>--%>
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:95%;"><b>Request for CATA Details</b></td>
        </tr>
        <% LoadMenuCATAApprover(); %>
        <tr><td colspan="3" class="BrowseAll"><a href="FinanceCataMenuApprover.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>
       </table>
      </div>         
     </div><br />
     <%
    }
    %>
    
   <%--For User--%>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id="divUser">    
     <b><span class="HeaderText">My Request for Travel Allowance</span></b>
     <br />
     <br />
        <asp:Button ID="btnNewRequest" runat="server" Text="New Request" OnClick="btnNewRequest_Click" />
     <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNewRequest_Click" />--%><%--&nbsp;<asp:ImageButton 
            runat="server" ID="btnNewRequestForExecutive" 
            ImageUrl="~/Support/btnNewRequestForExecutive.png" 
            onclick="btnNewRequestForExecutive_Click" Visible="False" />--%>
     <br />
     <br /> 
     <%--<table cellpadding="5">
      <tr>
       <td><b><span class="HeaderText">Filter Options</span></b></td>
      </tr>
     </table> <br />
     <div class="GridBorder">--%>
     <%--<table width="100%" cellpadding="5">
      <tr>
       <td style="width:15%;">Control Number: </td>
       <td style="width:30%;"><asp:TextBox runat="server" ID="txtControlNumber" CssClass="controls" Width="80%" BackColor="white" MaxLength="12"></asp:TextBox></td>
       <td style="width:15%;">Request Type:</td>
       <td style="width:30%;"><asp:DropDownList runat="server" ID="ddlRequestType" CssClass="controls" BackColor="white"></asp:DropDownList></td>
       <td style="width:10%;" style="text-align:center;">
        <asp:ImageButton runat="server" ID="btnSearch" 
         ImageUrl="~/Support/btnSearch.jpg" /></td>
      </tr>
     </table>
     </div><br />--%>
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
      <%-- <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
          <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Request for CATA</b></td>
          </tr>
         </table>           
        </td>
       </tr>--%>
       <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:35%;"><b>CATA Details</b></td>
         <td class="GridColumns" style="width:60%;"><b>Status</b></td>
        </tr>
       <% LoadMenuCATA(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="FinanceCataMenuAll.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>

