<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RFPMenuAll.aspx.cs" Inherits="Finance_RFP_RFPMenuAll" Title="The Official STI Head Office Website" %>
<%@ Import Namespace="STIeForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../Finance.aspx" class="SiteMap">Finance</a> » 
     <a href="RFPMenu.aspx" class="SiteMap">Request for Payment</a>
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
      <b><span class="HeaderText">Request for Payment (Finance Group)</span></b>
      <br />
      <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="5" cellspacing="1">
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:60%;"><b>RFP Details</b></td>
         <td class="GridColumns" style="width:35%;"><b>Status</b></td>
        </tr>
        <% LoadMenuRFPFinance(); %>
        <tr><td class="BrowseAll" style="font-size:small;text-align:left;" colspan="3">&nbsp;<b>Page</b><% LoadPagingFinance();%></td></tr>
       </table>
      </div>         
     </div><br />
  <%
    }
    %>
    
     <%--For Approver--%>
    <%
     if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"].ToString()))
      {
    %>
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="div1">    
      <b><span class="HeaderText">Request for Payment (For Approval)</span></b>
      <br />
      <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="5" class="Grid">
        <%--<tr>
         <td colspan="4" align="center" class="GridText">
          <table>
           <tr>
            <td>&nbsp;<b>List of Request for Payment</b></td>
           </tr>
          </table>           
         </td>
        </tr>--%>
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:95%;"><b>Request for RFP Details</b></td>
        </tr>
        <% LoadMenuRFPApprover(); %>
        <tr><td class="BrowseAll" style="font-size:small;text-align:left;" colspan="3">&nbsp;</td></tr>
       </table>
      </div>         
     </div>
     <%
    }
    %>
    <%--For User--%>
    
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="divUser">    
      <b><span class="HeaderText">My Request for Payment</span></b>
      <br />
      <br />
      <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNewRequest_Click" />--%>
         <asp:Button ID="btnNewRequest" runat="server" Text="New Request" OnClick="btnNewRequest_Click" />
      <br />
      <br /> 
      <div class="GridBorder">
       <table width="100%" cellpadding="5" cellspacing="1">
       <%-- <tr>
         <td colspan="4" align="center" class="GridText">
          <table>
           <tr>
            <td>&nbsp;<b>List of Request for Payment</b></td>
           </tr>
          </table>           
         </td>
        </tr>--%>
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:60%;"><b>RFP Details</b></td>
         <td class="GridColumns" style="width:35%;"><b>Status</b></td>
        </tr>
        <% LoadMenuRFP(); %>
       <tr><td class="BrowseAll" style="font-size:small;text-align:left;" colspan="3">&nbsp;<b>Page</b><% LoadPaging();%></td></tr>
       </table>
      </div>         
     </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>

