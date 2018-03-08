<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="OvertimeMenu.aspx.cs" Inherits="HR_HRMS_Overtime_OvertimeMenu" %>

<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OvertimeMenu.aspx" class="SiteMap">Overtime</a>
    </div>        
   </td>
  </tr>
  <tr>--%><%--<td style="height:9px;"></td></tr>--%>

  <tr runat="server" id="trApproverCOO">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
      <div class="container-fluid" id="HeaderText" style="background-color:darkblue;"><b><span class="HeaderText">Overtime Applications For Your Approval (COO Approver)</span></b></div>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
   <%--    <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>List of Overtime For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Overtime Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadOvertimeApproverCOO(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="OvertimeAllAC.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>
  <tr runat="server" id="trApproverCOOSpacer"><td style="height:9px"></td></tr>  
  
  <tr runat="server" id="trApproverDivision">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Overtime Applications For Your Approval (Division Head Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
<%--       <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>List of Overtime For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Overtime Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadOvertimeApproverDivision(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="OvertimeAllAD.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>    
    </div>
   </td>
  </tr>
  <tr runat="server" id="trApproverDivisionSpacer"><td style="height:9px"></td></tr>

  <tr runat="server" id="trApproverWithin" visible="false">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Overtime Applications For Your Approval (Within Department)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
<%--       <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>List of Overtime For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Overtime Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadOvertimeApproverHead(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="OvertimeAllAH.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>
  <tr runat="server" id="trApproverWithinSpacer" visible="false"><td style="height:9px"></td></tr>

  <tr runat="server" id="trApproverOutside" visible="false">
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Overtime Applications For Your Approval (Other Department)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
      <%-- <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>List of Overtime For Approval</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Overtime Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadOvertimeApproverRequestor(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="OvertimeAllAR.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr runat="server" id="trApproverOutsideSpace" visible="false"><td style="height:9px"></td></tr>  
    
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Overtime Applications</span></b>
     <br />
     <br />
     <%--<asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" onclick="btnNewRequest_Click" />--%>
        <asp:Button ID="btnNewRequest" runat="server" Text="New Request"  onclick="btnNewRequest_Click" />
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
 <%--      <tr><td colspan="4" align="center" class="GridText">&nbsp;<b>Recent Overtime Application Submission</b></td></tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Overtime Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadOvertime(); %>
       <tr><td class="BrowseAll" colspan="3"><a href="OvertimeAll.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
    </div>     
   </td> 
  </tr>     
 </table>  
</asp:Content>