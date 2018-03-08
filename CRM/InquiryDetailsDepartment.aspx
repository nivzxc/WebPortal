<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="InquiryDetailsDepartment.aspx.cs" Inherits="CRM_InquiryDetailsDepartment" %>

<asp:Content ID="cntMRCFRequest" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="CRMMenu.aspx" class="SiteMap">CRM</a> » 
     <a href="InquiryDetailsDepartment.aspx" class="SiteMap">Inquiry Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText">View Inquiry Details</span></b>
     <br />

     <div style="text-align:center;" id="divButtons2" runat="server">      
      <asp:ImageButton runat="server" ID="btnSend2" ImageUrl="~/Support/btnSend.jpg" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnReset2" ImageUrl="~/Support/btnReset.jpg" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnBack2" ImageUrl="~/Support/btnBack.jpg" />      
      <br />
      <br />
     </div>
          
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr>
        <td colspan="4" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>Inquiry Details</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridRows">Ticket Number:</td>
        <td class="GridRows">
         <asp:HiddenField runat="server" ID="hdnStatus" />
         <asp:TextBox runat="server" ID="txtTicketNumber" CssClass="controls" Width="80px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Filed:
         &nbsp;
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="190px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>
       <tr>
        <td class="GridRows">Inquiry Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox></td>
       </tr>              
       <tr>
        <td class="GridRows">Inquiry Category:</td>
        <td class="GridRows">
        <asp:TextBox runat="server" ID="txtInquiryCategory" CssClass="controls" Width="190px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Inquirer:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtInquirerName" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Email:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtEmail" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>  
       <tr>
        <td class="GridRows">Contact Number:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtContactNumber" CssClass="controls" ReadOnly="true" Width="150px"></asp:TextBox></td>
       </tr> 
       <tr>
        <td class="GridRows">Location:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtLocation" CssClass="controls" ReadOnly="true" Width="150px"></asp:TextBox></td>
       </tr>               
       <tr>
        <td class="GridRows">Campus:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCampus" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>
       </tr> 
       <tr> 
        <td class="GridRows">Course:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCourse" CssClass="controls" ReadOnly="true" Width="300px"></asp:TextBox></td>
       </tr>
       <tr> 
        <td class="GridRows">Section:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtSection" CssClass="controls" ReadOnly="true" Width="250px"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Year Graduated:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtYearGraduated" ReadOnly="true" CssClass="controls"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" valign="top">Message:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtMessage" ReadOnly="true" CssClass="controls" Width="98%" Rows="12" TextMode="MultiLine"></asp:TextBox></td>
       </tr>         
       <tr>
        <td class="GridRows">Designated Persons:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDesignatedPersons" ReadOnly="true" CssClass="controls" Width="98%"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" valign="top">Response:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtResponse" BackColor="White" CssClass="controls" Width="98%" Rows="12" TextMode="MultiLine"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Answered By:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtAnswerBy" ReadOnly="true" CssClass="controls" Width="150px"></asp:TextBox>
         &nbsp;
         <asp:TextBox runat="server" ID="txtAnswerOn" ReadOnly="true" CssClass="controls" Width="190px"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Last Viewed:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtLastViewedBy" ReadOnly="true" CssClass="controls" Width="150px"></asp:TextBox>
         &nbsp;
         <asp:TextBox runat="server" ID="txtLastViewedOn" ReadOnly="true" CssClass="controls" Width="190px"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Last Updated By:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtLastUpdateBy" ReadOnly="true" CssClass="controls" Width="150px"></asp:TextBox>
         &nbsp;
         <asp:TextBox runat="server" ID="txtLastUpdateOn" ReadOnly="true" CssClass="controls" Width="190px"></asp:TextBox>
        </td>
       </tr>       
      </table>       
     </div>          

     <br />
         
     <div style="text-align:center;" id="divButtons" runat="server">
      <br />
      <asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Support/btnReset.jpg" />
      &nbsp;      
      <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" />                              
     </div>
    </div> 
   </td>
  </tr>    
 
 </table>
</asp:Content>