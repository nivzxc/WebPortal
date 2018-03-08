<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="PettyCashRequestMenu.aspx.cs" Inherits="Finance_PCASH_PettyCashRequestMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
    
     <%--For Approver--%>
    <%
     if (STIeForms.clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"].ToString()))
      {
    %>
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="div1">    
      <b><span class="HeaderText">Petty Cash Request (For Approval)</span></b>
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
         <td class="GridColumns" style="width:5%; height: 9px;"></td>
         <td class="GridColumns" style="width:95%; height: 9px;"><b>Petty Cash Request Details</b></td>
        </tr>
        <% LoadMenuPCASApprover(); %>
        <tr><td colspan="2" class="BrowseAll">
            <a href="PettyCashRequestMenuAll.aspx" 
                style="font-size:small;">[Browse All Records]</a></td></tr>
       </table>
      </div>         
     </div>
     <%
    }
    %>
    
   <%--For User--%>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id="divUser">    
     <b><span class="HeaderText">My Petty Cash Request</span></b>
     <br />
     <br />
     <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNewRequest_Click" />--%>
        <asp:Button ID="btnNewRequest" runat="server" Text="New Request" OnClick="btnNewRequest_Click" />
     <br />
     <br /> 
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Request for Payment</b></td>
          </tr>
         </table>           
        </td>
       </tr>--%>
       <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:60%;"><b>Petty Cash Request Details</b></td>
         <td class="GridColumns" style="width:35%;"><b>Status</b></td>
        </tr>
       <% LoadMenuPCASUser(); %>
       <tr><td colspan="3" class="BrowseAll">
           <a href="PettyCashRequestMenuAll.aspx" 
               style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>

