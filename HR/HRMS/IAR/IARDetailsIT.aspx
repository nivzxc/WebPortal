<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="IARDetailsIT.aspx.cs" Inherits="HR_HRMS_IAR_IARDetailsIT" %>

<asp:Content ID="cntOvertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="IARMenu.aspx" class="SiteMap">Internet Access Request</a> » 
     <a href='IARDetailsD.aspx?iarcode=<%Response.Write(Request.QueryString["iarcode"]); %>' class="SiteMap">Request Details</a>
    </div>        
   </td>
  </tr> --%> 
  <tr><td style="height:9px;"></td></tr> 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Internet Access Request Details</span></b>
     <br /> 
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>         
     <br />     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
<%--       <tr><td colspan="2" class="GridText">&nbsp;<b>Details</b></td></tr>--%>
       <tr>
        <td class="GridRows" style="width:20%">IAR Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtIARCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Filed:
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>        
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>  
       <tr>
        <td class="GridRows">Date Start:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDateStart" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Date End:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDateEnd" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>                   
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatus" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Head Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtApproverH" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnApproverH" />         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatusH" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatusH" />
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtProcessDateH" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarksH" CssClass="controls" Width="99%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtApproverD" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnApproverD" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatusD" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatusD" />
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtProcessDateD" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarksD" CssClass="controls" Width="99%" ReadOnly="true" TextMode="MultiLine" Rows="3" BackColor="White" MaxLength="500"></asp:TextBox></td>
       </tr>      
      </table>
     </div>     
     <br />         
     <br />     
 
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>

