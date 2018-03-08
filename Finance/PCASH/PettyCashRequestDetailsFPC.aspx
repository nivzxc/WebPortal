<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="PettyCashRequestDetailsFPC.aspx.cs" Inherits="Finance_PCASH_PettyCashRequestDetailsFPC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <%--             <td class="GridText" style="text-align:center"><b>Charge To</b></td>--%>
  <table width="100%" cellpadding="0" cellspacing="0">
<%--      <% LoadPCASDetails();%>--%>
   <tr>
    <td>
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
     <div style="text-align:center;" runat="server" id="divButtons2" visible="True">
         <%--             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>--%>

         <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Visible="False"/>
         <asp:Button ID="btnForward" runat="server" Text="Forward to next Approver" 
             OnClick="btnForward_Click" Visible="True"/>
      &nbsp;
         <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click"/>
         
         <%--             <td class="GridText" style="text-align:center"><b>Charge To</b></td>--%><br />
      <br />
     </div>
      <div class="GridBorder">
       <table width="100%" cellpadding="3" class+"Grid">
<%--           <% LoadPCASAllocationDetails();%>--%>
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
         
         </td>
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
        <tr>
         <td class="GridRows" style="height: 9px">Remarks:</td>
         <td class="GridRows" style="height: 9px" ><asp:Label runat="server" ID="lblRemarks"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Assigned Cashier:</td>
         <td class="GridRows" >
         <asp:DropDownList runat="server" ID="ddlAssignedCustodian" CssClass="controls" 
                 BackColor="white">
         </asp:DropDownList></td>
        </tr>
        <tr id="trForApprovalOf">
         <td class="GridRows">For Approval of:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblApprover"></asp:Label></td>
        </tr>
        <tr id="trFinal Approver">
         <td class="GridRows">Final Approver:</td>
         <td class="GridRows" >
         <asp:DropDownList runat="server" ID="ddlFinalApprover" CssClass="controls" 
                 BackColor="white">
         </asp:DropDownList></td>
        </tr>
        <tr>
          <td colspan="2" class="GridColumns">
           <table>
            <tr>
                <%-- </div>--%>
             <td>&nbsp;PETTY CASH<b> SUB-DETAILS</b></td>
            </tr>
           </table>            
          </td>
         </tr>
        <tr>
         <td colspan="2" class="style1">
          <table width="100%" cellpadding="3" >
           <tr>
             <td colspan="2" class="GridColumns" style="text-align:center"><b>Item Description</b></td>
<%--             <td class="GridText" style="text-align:center"><b>Charge To</b></td>--%>
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
             <td class="style2">ACCOUNT EXPENSES</td>
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
    </td>
   </tr>
   <tr>
    <td style="height: 9px;"></td>
   </tr>
  </table>
<%-- </div>--%>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="cphHead">
    <style type="text/css">
        .style1
        {
            height: 43px;
        }
        .style2
        {
            width: 159px;
        }
    </style>
</asp:Content>