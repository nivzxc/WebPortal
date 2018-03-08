<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CRSDetailsCM.aspx.cs" Inherits="CMD_CRS_CRSDetailsCM" %>

<asp:Content ID="cntSprvApproveItem" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="CRSMenu.aspx" class="SiteMap">Courseware Request</a> » 
     <a href="CRSDetailsCM.aspx?crscode=<%Response.Write(Request.QueryString["crscode"]); %>" class="SiteMap">Courseware Request Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Courseware Request System</span></b>
     <br />
     <br />        
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">      
       <tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Courseware Request Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>      
       <tr>
        <td class="GridRows" style="width:20%">CRS Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtCrsCode" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Requested:
         &nbsp;
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>              
       <tr>
        <td class="GridRows">Requested By:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCMName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">School:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSchlName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnSchlCode" runat="server" />         
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" Width="98%" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">CM Head:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCMHName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCMHRemarks" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" ReadOnly="true"></asp:TextBox></td>
       </tr>   
       <tr>
        <td class="GridRows">Courseware Coordinator:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCCName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>                 
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCCRemarks" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Attachments:</td>
        <td class="GridRows"><asp:Label runat="server" ID="lblAttachments" Font-Size="Small"></asp:Label></td>
       </tr>       
      </table>
     </div>    
     
     <br />
     <table>
      <tr>
       <td><img src="../../Support/bookcase32.png" alt="" /></td>
       <td>&nbsp;<b><span class="HeaderText">Requested Courseware Materials</span></b></td>
      </tr>
     </table>      
     
     <%LoadRecords(); %>     
         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td class="GridRows">
         <b><span class="HeaderText">Legend:</span></b>
         <br /><br />
         <table>
          <tr>
           <td><img src="../../Support/document16.png" alt="" /></td>
           <td>&nbsp;For Endorsement</td>
           <td>&nbsp;&nbsp;&nbsp;</td>
           <td><img src="../../Support/gear16.png" alt="" /></td>
           <td>&nbsp;Endorsed</td>
           <td>&nbsp;&nbsp;&nbsp;</td>
           <td><img src="../../Support/close16.png" alt="" /></td>
           <td>&nbsp;Disapproved</td>           
          </tr>
          <tr>
           <td><img src="../../Support/mailsend16.png" alt="" /></td>
           <td>&nbsp;Partial Dispatch</td>
           <td>&nbsp;&nbsp;&nbsp;</td>
           <td><img src="../../Support/check16.png" alt="" /></td>
           <td>&nbsp;Complete Dispatch</td>           
           <td>&nbsp;</td>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
          </tr>
         </table>          
        </td>
       </tr>      
      </table>
     </div>           
    </div>      
   </td>
  </tr>
 
 </table>
</asp:Content>