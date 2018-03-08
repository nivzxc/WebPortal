<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="PettyCashRequestCashierMenu.aspx.cs" Inherits="Finance_PCASH_PettyCashRequestCashierMenu" %>

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
   <%--For Finance--%>
    <%
     if (clsSystemModule.HasAccess("PETTYC",Request.Cookies["Speedo"]["UserName"].ToString()))
      {
    %>
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="divFinance">    
      <b><span class="HeaderText">Petty Cash Request (For Tagging)</span></b>
      <br />
      <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="5" cellspacing="1">
        <%--<tr>
         <td colspan="4" align="center" class="GridText">
          <table>
           <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
            <td>&nbsp;<b>List of Request For RFP</b></td>
           </tr>
          </table>           
         </td>
        </tr>--%>
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:95%;"><b>Petty Cash Request Details</b></td>
        </tr>
        <% LoadMenuPCASForApproval(); %>
        <tr><td colspan="3" class="BrowseAll">
            <a href="PettyCashRequestCashierMenuAll.aspx" 
                style="font-size:small;">[Browse All Records]</a></td></tr>
       </table>
      </div>         
     </div><br />

    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="div2">    
      <b><span class="HeaderText">Petty Cash Request (Ready for Pickup)</span></b>
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
        <% LoadMenuPCASReady(); %>
        <tr><td colspan="2" class="BrowseAll">
            <a href="PettyCashRequestCashierMenuAll.aspx" 
                style="font-size:small;">[Browse All Records]</a></td></tr>
       </table>
      </div>         
     </div>

     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="div1">    
      <b><span class="HeaderText">Petty Cash Request (Issued)</span></b>
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
        <% LoadMenuPCASApprove(); %>
        <tr><td colspan="2" class="BrowseAll">
            <a href="PettyCashRequestCashierMenuAll.aspx" 
                style="font-size:small;">[Browse All Records]</a></td></tr>
       </table>
      </div>         
     </div>

    <%
    }
    %>
    
   <%--For User--%>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>



