<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="CATADetailsApproverFinance.aspx.cs" Inherits="Finance_CATA_CATADetailsApproverFinance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
<%-- <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">--%>
  <table width="100%" cellpadding="0" cellspacing="0">
   <%--<tr>
    <td >
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <a href="../../Default.aspx" class="SiteMap">Home</a> » <a href="../FinanceMain.aspx" class="SiteMap">
       Finance</a> » <a href="FinanceCataMenu.aspx" class="SiteMap">Request for CATA</a>
      » <a href="FinanceNewCataRequest.aspx" class="SiteMap">Create New Request</a>
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
         <asp:HiddenField ID="hndUsername" runat="server" />
      <div runat="server" id="divError" class="ErrMsg" visible="false">
                  <b>Error during update. Please correct your data entries:</b>
                  <br />
                  <br />
                  <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
      <div class="GridBorder">
       <table width="100%" cellpadding="3" class="Grid">
        <tr>
        <td>&nbsp;<b><asp:Label ID="lblFinance" runat="server" Text="Label"></asp:Label></b></td>
          <%--<td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
             <td>&nbsp;<b><asp:Label ID="lblFinance" runat="server" Text="Label"></asp:Label></b></td>
            </tr>
           </table>            
          </td>--%>
         </tr>
      </table>
      </div>
            <div style="text-align:center;" runat="server" id="divButtonFPC">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnPrintFPC" ImageUrl="~/Support/btnPrintPreview.png" onclick="btnPrintFPC_Click" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnProcessFPC" ImageUrl="~/Support/btnProcess.jpg" onclick="btnProcessFPC_Click" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnCancelFPC" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancelFPC_Click"/>
     &nbsp;
      <asp:ImageButton runat="server" ID="btnBackFPC" ImageUrl="~/Support/btnBack.jpg" onclick="btnBackFPC_Click"/>--%>
                <asp:Button ID="btnPrintFPC" runat="server" Text="Print" onclick="btnPrintFPC_Click"/>
                &nbsp;<asp:Button ID="btnProcessFPC" runat="server" Text="Process" onclick="btnProcessFPC_Click" />
                &nbsp;<asp:Button ID="btnCancelFPC" runat="server" Text="Cancel" onclick="btnCancelFPC_Click"/>
                &nbsp;<asp:Button ID="btnBackFPC" runat="server" Text="Back" onclick="btnBackFPC_Click" />
     </div>

     <div style="text-align:center;" runat="server" id="divButtonHQAccounting">
      <br />
<%--      &nbsp;<asp:ImageButton runat="server" ID="btnProcessHQAccounting" ImageUrl="~/Support/btnProcess.jpg" onclick="btnProcessHQAccounting_Click" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnCancelHQAccounting" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancelHQAccounting_Click" />
     &nbsp;
      <asp:ImageButton runat="server" ID="btnBackHQAccounting" ImageUrl="~/Support/btnBack.jpg" onclick="btnBackHQAccounting_Click"/>--%>
         <asp:Button ID="btnProcessHQAccounting" runat="server" Text="Process"  onclick="btnProcessHQAccounting_Click"/>
&nbsp;<asp:Button ID="btnCancelHQAccounting" runat="server" Text="Cancel"  onclick="btnCancelHQAccounting_Click"/>
&nbsp;<asp:Button ID="btnBackHQAccounting" runat="server" Text="Back"  onclick="btnBackHQAccounting_Click"/>
     </div>

     <div style="text-align:center;" runat="server" id="divButtonTreasury">
      <br />
      <asp:Button ID="btnCheckReleased" runat="server" Text="Cheque Released"  onclick="btnCheckReleased_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnCheckCancel" ImageUrl="~/Support/btnChequeCancel.png" onclick="btnCheckCancel_Click" />--%>
      <asp:Button ID="btnCheckCancel" runat="server" Text="Cheque Cancel"  onclick="btnCheckCancel_Click"/>
     &nbsp;<%--<asp:ImageButton runat="server" ID="btnBackTreasury" ImageUrl="~/Support/btnBack.jpg" onclick="btnBackTreasury_Click"/>--%>
         <asp:Button ID="btnBackTreasury" runat="server" Text="Back"  onclick="btnBackTreasury_Click"/>
     </div>

     <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="3" class="Grid">
        <tr>
          <td colspan="2" class=""><b>CATA Details</b>
           <%--<table>
            <tr>
             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
             <td>&nbsp;<b>CATA Details</b></td>
            </tr>
           </table>    --%>        
          </td>
         </tr>
        <tr>
         <td class="GridRows">CATA No:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblCATANo" Font-Bold="true"></asp:Label>
                  </td>
        </tr>
        <tr>
         <td class="GridRows" style="width:25%;">Requesting Person/Payee:</td>
         <td class="GridRows" style="width:75%;"><asp:Label runat="server" ID="lblPayee"></asp:Label></td>
        </tr>
         <tr>
         <td class="GridRows">OB Number:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblOBCode"></asp:Label></td>
        </tr>
         <tr>
         <td class="GridRows">Purpose of Trip:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblPurpose"></asp:Label></td>
        </tr>
        <tr>
          <td class="GridRows">Date Requested:</td>
          <td class="GridRows"><asp:Label runat="server" ID="lblRequestOn"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Date Check Needed:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblDaeNeeded"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Charged to:</td>
         <td class="GridRows">
          <table width="100%" class="GridBorder">
           <tr>
            <td class="GridRows" style="width:15%;">School:</td>
            <td class="GridRows" style="width:85%;"><asp:Label runat="server" ID="lblChargedToSchool"></asp:Label></td>
           </tr>
           <tr>
            <td class="GridRows">RC/Group Code:</td>
            <td class="GridRows" ><asp:Label runat="server" ID="lblChargedToRC"></asp:Label></td>
           </tr>
           <tr>
            <td class="GridRows">Others:</td>
            <td class="GridRows"><asp:Label runat="server" ID="lblChargedToOthers"></asp:Label></td>
           </tr>
          </table>
         </td>
        </tr>
        <tr>
         <td class="GridRows">Destination:</td>
         <td class="GridRows">
          <table width="100%">
           <tr>
            <td class="GridRows" style="width:15%;">FROM:</td>
            <td class="GridRows" style="width:85%;"><asp:Label runat="server" ID="lblFrom"></asp:Label></td>
           </tr>
           <tr>
            <td class="GridRows">TO:</td>
            <td class="GridRows"><asp:Label runat="server" ID="lblTo"></asp:Label></td>
           </tr>
           <tr>
            <td class="GridRows">No of Days:</td>
            <td class="GridRows"><asp:Label runat="server" ID="lblDays"></asp:Label></td>
           </tr>
          </table>
         </td>
        </tr>
        <tr>
         <td class="GridRows">Period Covered:</td>
         <td class="GridRows">
          <table width="100%">
           <tr>
            <td class="GridRows" style="width:25%;"></td>
            <td class="GridRows" style="width:75%;">
             <table width="100%">
              <tr>
               <td class="GridRows" style="width:50%;"><i>DATE</i></td>
               <td class="GridRows" style="width:50%;"><i>TIME</i></td>
              </tr> 
             </table>          
            </td>
           </tr>
           <tr>
            <td class="GridRows" style="width:25%;">Departure:</td>
            <td class="GridRows" style="width:75%;">
             <table width="100%">
              <tr>
               <td class="GridRows" style="width:50%;"><asp:Label runat="server" ID="lblDepartureDate"></asp:Label></td>
               <td class="GridRows" style="width:50%;"><asp:Label runat="server" ID="lblDepartureTime"></asp:Label></td>
              </tr> 
             </table>          
            </td>
           </tr>
           <tr>
            <td class="GridRows" style="width:25%;">Arrival:</td>
            <td class="GridRows" style="width:75%;">
             <table width="100%">
              <tr>
               <td class="GridRows" style="width:50%;"><asp:Label runat="server" ID="lblArrivalDate"></asp:Label></td>
               <td class="GridRows" style="width:50%;"><asp:Label runat="server" ID="lblArrivalTime"></asp:Label></td>
              </tr> 
             </table>          
            </td>
           </tr>
          </table>
         </td>
        </tr>
       <tr>
         <td class="GridRows">Mode of Payment:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblAcquiremode"></asp:Label></td>
        </tr>
        <tr>
          <td colspan="2" class=""><b>CATA Particulars</b>
           <%--<table>
            <tr>
             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
             <td>&nbsp;<b>CATA Particulars</b></td>
            </tr>
           </table> --%>           
          </td>
         </tr>
        <tr>
         <td colspan="2">
          <table width="100%" cellpadding="3">
           <% LoadCATAParticulars();%>
          </table> 
         </td>
        </tr>
       </table>
      </div>

     </div>
    </td>
   </tr>
   </table>
<%-- </div>--%>
</asp:Content>

