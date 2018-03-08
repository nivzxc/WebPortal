<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="CATADetails.aspx.cs" Inherits="Finance_CATA_CATADetails" Title="The Official STI HQ Website" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
 <%-- </div>--%>
  <table width="100%" cellpadding="0" cellspacing="0">
   <%--<tr>
    <td >
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <a href="../../Default.aspx" class="SiteMap">Home</a> » 
         <a href="../FinanceMain.aspx" class="SiteMap">
       Finance</a> » <a href="FinanceCataMenu.aspx" class="SiteMap">Request for CATA</a>
      » <a href="FinanceNewCataRequest.aspx" class="SiteMap">Create New Request</a>
     </div>
    </td>
   </tr>
   <tr>
    <td style="height: 9px;">
    </td>--%>
   </tr>
   <tr>
    <td>
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      
      <div style="text-align:center;" runat="server" id="divButtons2">
      &nbsp;<%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click"/>--%>
          <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
      <br />
      <br />
     </div>
      
      <div class="GridBorder">
       <table width="100%" cellpadding="3" class+"Grid">
        <tr>
          <%--<td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
             <td>&nbsp;<b>CATA Details</b></td>
            </tr>
           </table>            
          </td>--%>
          <td colspan="2">&nbsp;<b>CATA Details</b></td>
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
          <td colspan="2" class="GridColumns">&nbsp;<b>CATA Particulars</b>
          <%-- <table>
            <tr>
             <td></td>
            </tr>
           </table>     --%>       
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
   <tr>
    <td style="height: 9px;"></td>
   </tr>
  </table>
<%-- </div>--%>
</asp:Content>


