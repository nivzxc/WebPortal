<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ATWDetails.aspx.cs" Inherits="HR_HRMS_ATW_ATWDetails" %>
<asp:Content ID="cntOvertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="ATWMenu.aspx" class="SiteMap">ATW</a> » 
     <a href='ATWDetails.aspx?atwcode=<%Response.Write(Request.QueryString["atwcode"]); %>' class="SiteMap">ATW Details</a>
    </div>        
   </td>
  </tr>  --%>
<%--  <tr><td style="height:9px;"></td></tr> --%>
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">ATW Details</span></b>
     <br />    
     <br />     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
     <%--  <tr><td colspan="2" class="GridText">&nbsp;<b>ATW Details</b></td></tr>--%>
       <tr>
        <td class="GridRows" style="width:20%">ATW Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtATWCode" CssClass="controls" Width="80px" ReadOnly="true"></asp:TextBox>
         &nbsp;&nbsp;&nbsp;
         Date Filed:&nbsp;
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>        
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox></td>
       </tr>      
       <tr>
        <td class="GridRows">ATW Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true" Font-Bold="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Head Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtApproverH" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Status:
         &nbsp;
         <asp:TextBox runat="server" ID="txtStatusH" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarksH" CssClass="controls" Width="85%" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtApproverD" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Status:
         &nbsp;
         <asp:TextBox runat="server" ID="txtStatusD" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarksD" CssClass="controls" Width="85%" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>       
      </table>
     </div>     
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td colspan="2" class="">&nbsp;<b>Schedule Details</b></td></tr>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <table width="100%" cellpadding="5" cellspacing="1">
           <tr>            
            <td class="GridColumns" style="text-align:center; width:5%;">&nbsp;</td>
            <td class="GridColumns" style="width:20%;"><b>Date/Time Start</b></td>
            <td class="GridColumns" style="width:20%;"><b>Date/Time End</b></td>
            <td class="GridColumns" style="width:30%;"><b>Details</b></td>
            <td class="GridColumns" style="width:25%;"><b>Remarks</b></td>
           </tr>
           <%LoadSchedule(); %>
          </table>
         </div>
        </td>
       </tr>
      </table>
     </div>     
     <br />     
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnCancel" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancel_Click" />--%>
         <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>     
    </div>
   </td>
  </tr>  
 </table>
</asp:Content>