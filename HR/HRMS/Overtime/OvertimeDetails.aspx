<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="OvertimeDetails.aspx.cs" Inherits="HR_HRMS_Overtime_OvertimeDetails" %>

<asp:Content ID="cntOvertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OvertimeMenu.aspx" class="SiteMap">Overtime</a> » 
     <a href='OvertimeDetails.aspx?otcode=<%Response.Write(Request.QueryString["otcode"]); %>' class="SiteMap">Overtime Details</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Overtime Details</span></b>
     <br />    
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>Overtime Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:20%">Overtime Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtOTCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Filed:
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>        
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true" Font-Bold="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Overtime Start:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDateFrom" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Overtime End:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDateTo" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Hours:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtHours" CssClass="controls" Width="70px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Charge Type:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtChargeType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr runat="server">
        <td class="GridRows">Charge To:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr runat="server" id="trRApprover" visible="false">
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRApprover" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr runat="server" id="trRStatus" visible="false">
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRStatus" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Processed:
         &nbsp;
         <asp:TextBox runat="server" ID="txtRDateProcess" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr runat="server" id="trRRemarks" visible="false">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRRemarks" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Head Approver:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtHApprover" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Head Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtHStatus" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Processed:
         &nbsp;
         <asp:TextBox runat="server" ID="txtHDateProcess" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>              
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtHRemarks" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>      
       <tr id="trApproverDivision1" runat="server" visible="false">
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDApprover" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnDApprover" />
        </td>
       </tr>
       <tr id="trApproverDivision2" runat="server" visible="false">
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:HiddenField runat="server" ID="hdnDStatus" />
         <asp:TextBox runat="server" ID="txtDStatus" CssClass="controls" Width="100px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtDProcessDate" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr> 
       <tr id="trApproverDivision3" runat="server" visible="false">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDRemarks" CssClass="controls" Width="99%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr id="trApproverCOO1" runat="server" visible="false">
        <td class="GridRows">Final Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtCApprover" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnCApprover" />
        </td>
       </tr>
       <tr id="trApproverCOO2" runat="server" visible="false">
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:HiddenField runat="server" ID="hdnCStatus" />
         <asp:TextBox runat="server" ID="txtCStatus" CssClass="controls" Width="100px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtCProcessDate" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr> 
       <tr id="trApproverCOO3" runat="server" visible="false">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCRemarks" CssClass="controls" Width="99%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>              
      </table>
     </div>
     
     <br />

     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnCancel" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancel_Click" />--%>
         <asp:Button ID="btnCancel" runat="server" Text="Void"  onclick="btnCancel_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
      <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>     
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>