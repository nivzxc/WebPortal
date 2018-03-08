<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="LeaveMenu.aspx.cs" Inherits="HR_HRMS_Leave_LeaveMenu" %>
<%@ Import Namespace="HRMS" %>
<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="LeaveMenu.aspx" class="SiteMap">Leave</a>
    </div>        
   </td>
  </tr>--%>
      
  <%--<tr><td style="height:9px;"></td></tr>--%>

  <%
   if (clsDepartmentApprover.IsApprover(Request.Cookies["Speedo"]["UserName"],EFormType.Leave))
   {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Leave For Your Approval</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
      <%-- <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Leave For Approval</b></td>
          </tr>
         </table>           
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Leave Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadLeaveA(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="LeaveAllA.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
  <% 
   }
  %>
    
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Application For Leave</span></b>
     <br />
     <br />
     <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" onclick="btnNewRequest_Click" />--%>
        <asp:Button ID="btnNewRequest" runat="server" Text="New Request"  onclick="btnNewRequest_Click"/>
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
      <%-- <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Recent Leave Submission</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Leave Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadLeave(); %>
       <tr><td class="BrowseAll" colspan="3"><a href="LeaveAll.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
    </div>     
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Leave Balance Summary</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:50%;"><b>Leave Type</b></td>
        <td class="GridColumns" style="width:15%;"><b>Entitlement</b></td>
        <td class="GridColumns" style="width:15%;"><b>Used</b></td>
        <td class="GridColumns" style="width:15%;"><b>Remaining</b></td>
       </tr>
       <%LoadLeaveBalance(); %>
      </table>
     </div>
    </div>     
   </td>
  </tr>     
 </table>  
</asp:Content>