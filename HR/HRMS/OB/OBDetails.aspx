﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="OBDetails.aspx.cs" Inherits="HR_HRMS_OB_OBDetails" %>

<asp:Content ID="cntOvertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0"> 
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OBMenu.aspx" class="SiteMap">OB</a> » 
     <a href='OBDetails.aspx?obcode=<%Response.Write(Request.QueryString["obcode"]); %>' class="SiteMap">OB Details</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">OB Details</span></b>
     <br />    
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>OB Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:20%">OB Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtOBCode" CssClass="controls" Width="80px" ReadOnly="true"></asp:TextBox>
         &nbsp;&nbsp;&nbsp;
         Date Filed:&nbsp;
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>        
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>      
       <tr>
        <td class="GridRows">OB Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true" Font-Bold="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">OB Type:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtOBType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Rendered To:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRenderedTo" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr runat="server" id="trRApprover" visible="false">
        <td class="GridRows">Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRApprover" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Status:
         &nbsp;
         <asp:TextBox runat="server" ID="txtRStatus" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr runat="server" id="trRRemarks" visible="false">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRRemarks" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Head Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtHApprover" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Status:
         &nbsp;
         <asp:TextBox runat="server" ID="txtHStatus" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtHRemarks" CssClass="controls" Width="99%" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>      
      </table>
     </div>
     
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>Schedule Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <table width="100%" cellpadding="5" cellspacing="1">
           <tr>            
            <td class="GridColumns" style="text-align:center;">&nbsp;</td>
            <td class="GridColumns"><b>Key In</b></td>
            <td class="GridColumns"><b>Key Out</b></td>
            <td class="GridColumns"><b>Updated By</b></td>
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
     <%-- <asp:ImageButton runat="server" ID="btnCancel" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancel_Click" />--%>
         <asp:Button ID="btnCancel" runat="server" Text="Void" onclick="btnCancel_Click" />
      &nbsp;
    <%--  <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
    <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"  />
     </div>     
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>