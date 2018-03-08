<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RFPDetails.aspx.cs" Inherits="Finance_RFP_RFPDetails" Title="The Official STI Head Office Website" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <%-- </div>--%>
  <table width="100%" cellpadding="0" cellspacing="0">
   <%--<tr>
    <td >
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <a href="../../Default.aspx" class="SiteMap">Home</a> » <a href="../Finance.aspx" class="SiteMap">
       Finance</a> » <a href="RFPMenu.aspx" class="SiteMap">Request For Payment</a>
      » <a href="RFPDetails.aspx?ControlNumber=<%Response.Write(Request.QueryString["ControlNumber"]); %>" class="SiteMap">Details</a>
     </div>
    </td>
   </tr>
   <tr>
    <td style="height: 9px;">
    </td>
   </tr>--%>
   <tr>
    <td>
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
     <div style="text-align:center;" runat="server" id="divButtons2" visible="True">
      <%--<asp:ImageButton runat="server" ID="btnPrint" ImageUrl="~/Support/btnPrint.jpg" 
       OnClick="btnPrint_Click" Visible="False"/>--%>

         <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Visible="False"/>
      &nbsp;
         <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click"/>
         
         <%--<asp:ImageButton runat="server" ID="btnBack" 
       ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click"/>--%><br />
      <br />
     </div>
      <div class="GridBorder">
       <table width="100%" cellpadding="3" class+"Grid">
       <%-- <tr>
          <td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
             <td>&nbsp;<b>Request for Payment Details</b></td>
            </tr>
           </table>            
          </td>
         </tr>--%>
        <tr>
         <td class="GridRows">Control No:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblControlNumber" Font-Bold="true"></asp:Label>&nbsp;<asp:Label 
           ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
         </td>
        </tr>
        <tr>
         <td class="GridRows" style="width:25%;">Payee:</td>
         <td class="GridRows" style="width:75%;"><asp:Label runat="server" ID="lblPayee"></asp:Label></td>
        </tr>
       <tr>
         <td class="GridRows">Request By:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblRequestedBy"></asp:Label></td>
        </tr>
        <tr>
          <td class="GridRows">Request Type:</td>
          <td class="GridRows"><asp:Label runat="server" ID="lblRequestType"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Project Title:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblProjectTitle"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Reference RFA Number:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblReferenceRFANo"></asp:Label></td>
         
         </td>
        </tr>
        <tr>
         <td class="GridRows">Date Check Needed:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblDateCheckNeeded"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Date Created:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblDateCreated"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Supporting Documents:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblSupportingDocuments"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Remarks / Comment:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblRemarks"></asp:Label></td>
        </tr>
        <tr>
          <td colspan="2" class="GridText">
           <table>
            <tr>
<%--             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>--%>
             <td>&nbsp;<b>Request For Payment SUB-DETAILS</b></td>
            </tr>
           </table>            
          </td>
         </tr>
        <tr>
         <td colspan="2">
          <table width="100%" cellpadding="3" >
           <tr>
             <td class="GridText" style="text-align:center"><b>Item Description</b></td>
             <td class="GridText" style="text-align:center"><b>Charge To</b></td>
             <td class="GridText" style="text-align:center"><b>Amount</b></td>
           </tr>
           <% LoadRFPDetails();%>
          </table> 
         </td>
        </tr>
       </table>
      </div>
     </div>
    </td>
   </tr>
   <tr>
    <td style="height: 9px;"></td>
   </tr>
  </table>
<%-- </div>--%>
</asp:Content>






