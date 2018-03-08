<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="UndertimeDetails.aspx.cs" Inherits="HR_HRMS_Undertime_UndertimeDetails" %>

<asp:Content ID="cntLeaveDetails" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="UndertimeMenu.aspx" class="SiteMap">Undertime</a> » 
     <a href="UndertimeDetails.aspx?utcode=<%Response.Write(Request.QueryString["utcode"]); %>" class="SiteMap">Undertime Details</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Application For Undertime Details</span></b>     
     <br />
     <br />           
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
     <%--  <tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>Application For Undertime Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:20%">Transaction Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtUTCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Filed:
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="width:20%">Requestor:</td>
        <td class="GridRows" style="width:80%"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" style="width:20%">Status:</td>
        <td class="GridRows" style="width:80%"><asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="200px" ReadOnly="true" Font-Bold="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Date Applied:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDateApplied" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox></td>
       </tr>             
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="2" ValidationGroup="save"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Date:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverDate" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverRemarks" CssClass="controls" Width="98%" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>              
      </table>
     </div>
     <br />
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnCancel" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancel_Click" />--%>
         <asp:Button ID="btnCancel" runat="server" Text="Void" onclick="btnCancel_Click" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" />
     </div>
    </div>
    
   </td>
  </tr>  
  
 </table>
</asp:Content>