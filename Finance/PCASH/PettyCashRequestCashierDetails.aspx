<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="PettyCashRequestCashierDetails.aspx.cs" Inherits="Finance_PCASH_PettyCashRequestCashierDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <div runat="server" id="divError" class="ErrMsg" visible="false">
                  <b>Error during update. Please correct your data entries:</b>
                  <br />
                  <br />
                  <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
      <div style="text-align:center;" runat="server" id="divButtons2">
       <br />
<%--          <% LoadPCASDetails();%>--%>
          <asp:Button ID="btnTagASReady" runat="server" Text="Tag as Ready" 
              OnClick="btnTagASReady_Click" Visible="False"/>
          <asp:Button ID="btnTagasIssued" runat="server" Text="Tag as Issued" 
              OnClick="btnTagasIssued_Click" Visible="False"/>
          <asp:Button ID="btnPrint" runat="server" Text="Print" 
              OnClick="btnPrint_Click" Visible="False"/>
          <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
          <%--             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>--%>
      <br />
      <br />
     </div>
      <div class="GridBorder">
       <table width="100%" cellpadding="3" class+"Grid">
           <%--             <td class="GridText" style="text-align:center"><b>Charge To</b></td>--%>
        <tr>
         <td class="GridRows">PCAS #:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblPCASCode" Font-Bold="True"></asp:Label>&nbsp;<asp:Label 
           ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
         </td>
        </tr>
        <tr>
         <td class="GridRows" style="width:25%;">Requesting Person:</td>
         <td class="GridRows" style="width:75%;"><asp:Label runat="server" ID="lblRequestor"></asp:Label></td>
        </tr>
         <tr>
         <td class="GridRows" style="width:25%;">Payee&#39;s Name:</td>
         <td class="GridRows" style="width:75%;"><asp:Label runat="server" ID="lblPayeeName"></asp:Label></td>
        </tr>
        <tr>
          <td class="GridRows">Date Funds Needed:</td>
          <td class="GridRows"><asp:Label runat="server" ID="lblDataFundsNeeded"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Project Title:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblReason"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Purpose:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblClassification"></asp:Label></td>
         
         <%--</td>--%>
        </tr>
        <tr>
         <td class="GridRows">Filed OB:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblFiledOB"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Charge Type:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblChargeType"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Charge To:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblChargeTo"></asp:Label></td>
        </tr>
         <tr id="trCustodian" runat="server" visible="false">
         <td class="GridRows" style="height: 9px">Assigned Cashier:</td>
         <td class="GridRows" style="height: 9px" >
         <asp:DropDownList runat="server" ID="ddlCustodian" CssClass="controls" BackColor="white">
         </asp:DropDownList>
            &nbsp;<asp:Button ID="btnForwardRequest" runat="server" Height="20px" 
                 Text="Forward Request" Width="123px" onclick="btnForwardRequest_Click" />
             </td>
        </tr>
        <tr>
          <td colspan="2" class="GridColumns">
           <table>
            <tr>
<%--                <% LoadPCASAllocationDetails();%>--%>
             <td>&nbsp;PETTY CASH<b> SUB-DETAILS</b></td>
            </tr>
           </table>            
          </td>
         </tr>
        <tr>
         <td colspan="2">
          <table width="100%" cellpadding="3" >
           <tr>
             <td colspan="2" class="GridColumns" style="text-align:center"><b>Item Description</b></td>
               <%-- </div>--%>
             <td class="GridColumns" style="text-align:center"><b>Amount</b></td>
           </tr>
           <% LoadPCASDetails();%>
          </table> 
         </td>
        </tr>

        <tr>
          <td colspan="2" class="GridColumns">
           <table>
            <tr>
<%--             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>--%>
             <td>ACCOUNT EXPENSES</td>
            </tr>
           </table>            
          </td>
         </tr>
        <tr>
         <td colspan="2">
          <table width="100%" cellpadding="3" >
           <tr>
             <td colspan="2" class="GridColumns" style="text-align:center"><b>Item Description</b></td>
<%--             <td class="GridText" style="text-align:center"><b>Charge To</b></td>--%>
             <td class="GridColumns" style="text-align:center"><b>Amount</b></td>
           </tr>
           <% LoadPCASAllocationDetails();%>
          </table> 
         </td>
        </tr>
       </table>
      </div>
     </div>
<%--    </td>
   </tr>--%>
   <tr>
    <td style="height: 9px;"></td>
   </tr>
<%--  </table>--%>
<%-- </div>--%>
</asp:Content>

