<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="FinanceCataEditRequest.aspx.cs" Inherits="Finance_FinanceCataEditRequest" Title="The Official STI Head Office Website" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
<script language="javascript" type="text/javascript">
function Toggle(sender)
{   
 document.getElementById('trBus').style.display = sender.checked?"block":"none";
}

function DisableButton(buttonElem)
{
buttonElem.value = 'Please wait';
buttonElem.disabled = true;
}

</script>
      <asp:UpdatePanel ID="upDetails" runat="server">
       <ContentTemplate>
         <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px;
     padding-bottom: 10px;"><a href="../../Default.aspx" class="SiteMap">Home</a> » <a href="../FinanceMain.aspx" class="SiteMap">
      Finance</a> » <a href="FinanceCataMenu.aspx" class="SiteMap">Request for CATA</a> » <a
       href="FinanceNewCataRequest.aspx" class="SiteMap">Create New Request</a>
    </div>
   </td>
  </tr>
  
  <tr>
   <td style="height: 9px;"></td>
  </tr>--%>
  
  <tr>
   <td>
      <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">   
               <b><span class="HeaderText">Create New Request for Cash Advance for Travel Allowance</span></b>
               <br />
               <br />
                <div runat="server" id="divError" class="ErrMsg" visible="false">
                  <b>Error during update. Please correct your data entries:</b>
                  <br />
                  <br />
                  <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
                 </div>
                
                <br />
                <div class="GridBorder">
                
                <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns" style="text-align:left;" colspan="2"><b>Request for Cash Advance for Travel Allowance</b></td></tr>
       <tr>
        <td class="GridRows" style="width:25%;">Cata Number</td>
        <td class="GridRows" style="width:75%;">
         <asp:TextBox ID="txtCataNumber" runat="server" ReadOnly="True" Width="60%" 
          CssClass="controls" BorderStyle="None"></asp:TextBox></td>
       </tr>
                 <tr>
                  <td class="GridRows" style="width:25%;">
                   Payee:</td>
                  <td class="GridRows" style="width:75%;">
                   <asp:HiddenField ID="hdnUsername" runat="server" />
                      <asp:HiddenField ID="hdnCreateBy" runat="server" />
                   <asp:TextBox ID="txtRequestor" runat="server" BorderStyle="None" 
                    CssClass="controls" MaxLength="80" ReadOnly="True" ValidationGroup="save" 
                    Width="40%"></asp:TextBox>
                  </td>
                 </tr>
       <tr>
        <td class="GridRows">Purpose of Trip:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" 
                        ID="txtPurpose" CssClass="controls" Width="80%" BackColor="white" 
                        ValidationGroup="save" MaxLength="80" ></asp:TextBox>
                       <asp:RequiredFieldValidator runat="server" ID="vldAL4" 
                        ControlToValidate="txtPurpose" 
                        ErrorMessage="&lt;br&gt;[Trip Purpose Required]" Display="Dynamic" 
                        ValidationGroup="SaveRecord" SetFocusOnError="true" 
                ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">From:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" 
                        ID="txtDestinationFrom" CssClass="controls" Width="40%" BackColor="white" 
                        ValidationGroup="save" MaxLength="80" ></asp:TextBox>
                       <asp:RequiredFieldValidator runat="server" ID="vldAL0" 
                        ControlToValidate="txtDestinationFrom" 
                        ErrorMessage="&lt;br&gt;[Destination From Required]" Display="Dynamic" 
                        ValidationGroup="SaveRecord" SetFocusOnError="true" 
                ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">To:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" 
                        ID="txtDestinationTo" CssClass="controls" Width="40%" BackColor="white" 
                        ValidationGroup="save" MaxLength="80" ></asp:TextBox>
                       <asp:RequiredFieldValidator runat="server" ID="vldAL1" 
                        ControlToValidate="txtDestinationTo" 
                        ErrorMessage="&lt;br&gt;[Destination To Required]" Display="Dynamic" 
                        ValidationGroup="SaveRecord" SetFocusOnError="true" 
                ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Charge to School:</td>
        <td class="GridRows">
                    <asp:DropDownList ID="ddlSchool" runat="server" CssClass="controls" Width="50%" 
                     BackColor="White" onselectedindexchanged="ddlSchool_SelectedIndexChanged"></asp:DropDownList>
                             </td>
       </tr>
       <tr>
        <td class="GridRows">RC/Group:</td>
        <td class="GridRows">
                    <asp:DropDownList ID="ddlRc" runat="server" CssClass="controls" Width="50%" 
                     BackColor="White" onselectedindexchanged="ddlRc_SelectedIndexChanged">
                    </asp:DropDownList>
                             </td>
       </tr> 
       <tr>
        <td class="GridRows">Others:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" 
                        ID="txtOthers" CssClass="controls" Width="40%" BackColor="white" 
                        ValidationGroup="save" MaxLength="50" ></asp:TextBox>
                             </td>
       </tr>
          <tr id="trObNumber" runat="server">
        <td class="GridRows">OB Number:</td>
        <td class="GridRows">
                    <asp:TextBox ID="txtOBNumber" runat="server" BackColor="White" 
                        CssClass="controls" MaxLength="50" ReadOnly="True" ValidationGroup="save" 
                        Width="80px"></asp:TextBox>
                    <asp:ImageButton ID="btnViewOB" runat="server" 
                        ImageUrl="~/Support/viewmag16.png" onclick="btnViewOB_Click" 
                        ToolTip="View OB Details" OnClientClick="aspnetForm.target ='_blank';" />

       
        <ajax:ModalPopupExtender ID="pnlModal_ModalPopupExtender" runat="server" 
            DynamicServicePath="" Enabled="True" TargetControlID="btnViewOB"
        BackgroundCssClass="modalBackground"
        PopupControlID="pnlHeader" CancelControlID="btnClose" DropShadow="true">
        </ajax:ModalPopupExtender>
          </td>
       </tr>
       <tr>
        <td class="GridRows" style="height: 36px">Departure Date:</td>
        <td class="GridRows" style="height: 36px">
                             <ew:CalendarPopup ID="dtDateDeparture" 
                              runat="server" PopupLocation="Bottom" ControlDisplay="TextBoxImage" 
                              ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" 
                              ondatechanged="dtDateDeparture_DateChanged" AutoPostBack="true" 
                              BorderStyle="None">
           <TextBoxLabelStyle Width="60px"></TextBoxLabelStyle>
                              <ButtonStyle CssClass="controls" />
                             </ew:CalendarPopup>
                            <ew:TimePicker ID="dtTimeDeparture" runat="server" 
                              ControlDisplay="TextBoxImage" DisableTextBoxEntry="false" 
                              DisplayUnselectableTimes="true" ImageUrl="~/Support/Time22.png" 
                              RoundUpMinutes="false" TextBoxLabelStyle-Width="70px" BorderStyle="None">
                             <ButtonStyle CssClass="controls" />
           <TextBoxLabelStyle Width="70px"></TextBoxLabelStyle>
                             </ew:TimePicker>
                                    </td>
       </tr>
       <tr>
        <td class="GridRows">Arrival Date:</td>
        <td class="GridRows">
                             <ew:CalendarPopup ID="dtDateArrival" runat="server" PopupLocation="Bottom" 
                              ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" 
                              TextBoxLabelStyle-Width="60px" ondatechanged="dtDateArrival_DateChanged" 
                              AutoPostBack="true">
           <TextBoxLabelStyle Width="60px"></TextBoxLabelStyle>
                              <ButtonStyle CssClass="controls" />
                             </ew:CalendarPopup>
                             <ew:TimePicker ID="dtTimeArrival" runat="server" 
                              ControlDisplay="TextBoxImage" DisableTextBoxEntry="false" 
                              DisplayUnselectableTimes="true" ImageUrl="~/Support/Time22.png" 
                              RoundUpMinutes="false" TextBoxLabelStyle-Width="70px" OnTimeChanged="dtTimeArrival_TimeChanged" AutoPostBack="True">
                              <ButtonStyle CssClass="controls" />
           <TextBoxLabelStyle Width="70px"></TextBoxLabelStyle>
                             </ew:TimePicker>
                                    </td>
       </tr>
       <tr>
        <td class="GridRows">Number of Days:</td>
        <td class="GridRows">
                    <asp:TextBox runat="server" ID="txtDays" CssClass="controls" ReadOnly="True" 
                     Width="50px" Font-Bold="true" BorderStyle="None"></asp:TextBox></td>
       </tr>
        <tr>
        <td class="GridRows">Date Check Needed:</td>
        <td class="GridRows">
                             <ew:CalendarPopup ID="dtDateCheckNeeded" runat="server" PopupLocation="Bottom" 
                              ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" 
                              TextBoxLabelStyle-Width="60px" ondatechanged="dtDateCheckNeeded_DateChanged" 
                              AutoPostBack="true" CssClass="controls2">
           <TextBoxLabelStyle Width="60px"></TextBoxLabelStyle>
                             </ew:CalendarPopup>
                             </td>
         
       </tr>
       <tr>
        <td class="GridRows">Mode of Payment:</td>
        <td class="GridRows">
         <asp:DropDownList ID="ddlAcquiremode" runat="server" CssClass="controls" 
          BackColor="White">
         </asp:DropDownList>
        </td>
       </tr>
        
        </table>
                
   </div> 
   <br />
   <%--<div class="GridBorder">--%>
                      <table width="100%" cellpadding="0" cellspacing="3">
                           <tr>
                             <td><b><span class="HeaderText">CATA Particulars</span></b><br />
               <br /></td>
                           </tr>
                        
                          
                           
                           <tr>
                             <td>
                                   <%--REPRESENTATION ALLOWANCE--%>
                                 <div runat="server" id="divAccomodation" visible="false" class="GridBorder"> 
                                 
                                   <table width="100%" cellpadding="3" cellspacing="2">
                                      <tr>
                                           <td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Accommodation Allowance</b></td>
                                      </tr>
                                      <tr>
                                           <td class="GridRows" style="width:30%;">&nbsp;Accommodation:</td>
                                           <td class="GridRows" style="width:70%;"><asp:HiddenField ID="hdnAccomodation" runat="server" />
                                                    <asp:DropDownList runat="server" ID="ddlAccomodation" CssClass="controls" 
                                                     BackColor="white" AutoPostBack="True" 
                                                     onselectedindexchanged="ddlAccomodation_SelectedIndexChanged"></asp:DropDownList></td>
                                      </tr>
                                      <tr>
                                                   <td class="GridRows">&nbsp;Accommodation Name:</td>
                                                   <td class="GridRows" colspan="1">
                                                    <asp:TextBox runat="server" ID="txtHotelName" 
                                                     CssClass="controls" Width="50%" BackColor="white" ValidationGroup="save" 
                                                     MaxLength="40" ></asp:TextBox></td>
                                      </tr>
                                       <tr>
                                                   <td class="GridRows">&nbsp;<b>Total Accommodation Allowance:</b></td>
                                                   <td class="GridRows" colspan="1">
                                                    <asp:TextBox runat="server" 
                                                     ID="txtAccomodationTotal" CssClass="controls" Width="25%" 
                                                     ValidationGroup="save" MaxLength="10" BorderStyle="None" ReadOnly="True" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="vldAL" 
                                                     ControlToValidate="txtAccomodationTotal" ErrorMessage="<br>[Amount Required]" 
                                                     Display="Dynamic" ValidationGroup="additem" 

                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>&nbsp;&nbsp;
                                                    <asp:RegularExpressionValidator id="re3" runat="server" 
                                                     ControlToValidate="txtAccomodationTotal" ErrorMessage="<br>[Invalid Input]" 
                                                     ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                                     ValidationGroup="additem" SetFocusOnError="true"></asp:RegularExpressionValidator></td>
                                        </tr>
                                               </table>
                               </div>
                           </td>
                         </tr>
                         
                         
                           
                           <tr>
                              <td>
                                <%--APPROVERS--%>
                                <div runat="server" id="divTravelAllowance" visible="false" class="GridBorder"> 
                                   <table width="100%" cellpadding="3" cellspacing="2">
                                      <tr>
                                          <td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Travel Allowance</b></td>
                                      </tr>
                                      <tr>
                                          <td class="GridRows" style="width:30%;">&nbsp;Rate Per Day:</td>
                                          <td class="GridRows" style="width:70%;"><asp:HiddenField ID="hdnTravel" runat="server" /> 
                                           <asp:TextBox runat="server" ID="txtRatePerDay" CssClass="controls" Width="25%" 
                                            AutoPostBack="True" BackColor="White" 
                                            ontextchanged="txtRatePerDay_TextChanged" ReadOnly="True" ></asp:TextBox>&nbsp;&nbsp;
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="txtRatePerDay" Display="Dynamic" 
                                            ErrorMessage="&lt;br&gt;[Amount Required]" SetFocusOnError="true" 
                                            ValidationGroup="SaveRecord" ForeColor="Red"></asp:RequiredFieldValidator>
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ControlToValidate="txtRatePerDay" Display="Dynamic" 
                                            ErrorMessage="&lt;br&gt;[Invalid Input]" SetFocusOnError="true" 
                                            ValidationExpression="^\d*([.]\d*)?|[.]\d+$" ValidationGroup="SaveRecord" 
                                                  ForeColor="Red"></asp:RegularExpressionValidator>
                                          </td>
                                      </tr>
                                      <tr>
                                           <td class="GridRows" >&nbsp;<b>Total Travel Allowance:</b></td><td class="GridRows">  
                                           <asp:TextBox runat="server" ID="txtTravelTotal" CssClass="controls" 
                                            Width="25%" ValidationGroup="save" MaxLength="10" BorderStyle="None" 
                                            ReadOnly="True" ></asp:TextBox>
                                                    </td>
                                      </tr>
                                    </table>
                                </div>
                              </td>               
                           </tr>
                           
                        
                          
                          <tr>
                           <td>  
                                   <%--REPRESENTATION ALLOWANCE--%>
                                   <div runat="server" id="divTransportation" visible="false" class="GridBorder"> 
                                         <table width="100%" cellpadding="3" cellspacing="2" >
                                      <tr>
                                           <td class="GridColumns" style="text-align:left;" 

                             colspan="2">&nbsp;<b>Transportation</b></td>
                                      </tr>
                                      <tr>
                                           <td class="GridRows" style="width:30%;">&nbsp;Mode of Transportation:</td>
                                           <td class="GridRows" style="width:70%;"><asp:CheckBox ID="chkbLand"  
                                             runat="server" Text="Land"  oncheckedchanged="chkbLand_CheckedChanged" 
                                             CssClass="checkB" AutoPostBack="True" Width="50px" />&nbsp;
                                           <asp:CheckBox ID="chkbAir" runat="server" Text="Air" Width="50px" AutoPostBack="True" oncheckedchanged="chkbAir_CheckedChanged" CssClass="checkB" />&nbsp;
                                           <asp:CheckBox ID="chkbSea" runat="server" Text="Sea" Width="50px" AutoPostBack="True"  oncheckedchanged="chkbSea_CheckedChanged"  CssClass="checkB" /></td>
                                       </tr>
                                   <tr runat="server" id="trLandVhire" visible="false">
                                           <td class="GridRows">&nbsp;V-Hire/Rental:</td>
                                           <td class="GridRows"><asp:HiddenField ID="hdnLandVHire" runat="server" />
                                            <asp:TextBox runat="server" ID="txtLandVHire" CssClass="controls" Width="25%" 
                                             BackColor="white" ValidationGroup="save" MaxLength="10" 
                                             ontextchanged="txtLandVHire_TextChanged" AutoPostBack="True" ></asp:TextBox>
                                      <asp:RegularExpressionValidator id="re5" runat="server" 
                                             ControlToValidate="txtLandVHire" ErrorMessage="<br>[Invalid Input]" 
                                             ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                             ValidationGroup="SaveRecord" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                     </tr>
                                      <tr runat="server" id="trLandBus" visible="false">
                                           <td class="GridRows">&nbsp;Bus</td>
                                           <td class="GridRows"><asp:HiddenField ID="hdnLandBus" runat="server" />
                                            <asp:TextBox runat="server" ID="txtLandBus" CssClass="controls" Width="25%" 
                                             BackColor="white" ValidationGroup="save"  MaxLength="10" AutoPostBack="True" 
                                             ontextchanged="txtLandBus_TextChanged" ></asp:TextBox>
                                      <asp:RegularExpressionValidator id="re7" runat="server" 
                                             ControlToValidate="txtLandBus" ErrorMessage="<br>[Invalid Input]" 
                                             ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                             ValidationGroup="SaveRecord" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                     </tr>
                                     <tr runat="server" id="trLandGasAllowance" visible="false">
                                           <td class="GridRows">&nbsp;Gas Allowance:</td>
                                           <td class="GridRows"><asp:HiddenField ID="hdnLandGasAllowance" runat="server" />
                                            <asp:TextBox runat="server" ID="txtLandGasAllowance" CssClass="controls" 
                                             Width="25%" BackColor="white" ValidationGroup="save" MaxLength="10" 
                                             AutoPostBack="True" ontextchanged="txtLandGasAllowance_TextChanged" ></asp:TextBox>
                                      <asp:RegularExpressionValidator id="re8" runat="server" 
                                             ControlToValidate="txtLandGasAllowance" ErrorMessage="<br>[Invalid Input]" 
                                             ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                             ValidationGroup="additem" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                     </tr>
                                    
                                     <tr runat="server" id="trLandTollFee" visible="false">
                                           <td class="GridRows">&nbsp;Toll Fee:</td>
                                           <td class="GridRows"><asp:HiddenField ID="hdnLandTollFee" runat="server" />
                                            <asp:TextBox runat="server" ID="txtLandTollFee"  CssClass="controls" 
                                             Width="25%" BackColor="white" ValidationGroup="save" MaxLength="10" 
                                             AutoPostBack="True" ontextchanged="txtLandTollFee_TextChanged" ></asp:TextBox>
                                      <asp:RegularExpressionValidator id="re10" runat="server" 
                                             ControlToValidate="txtLandTollFee" ErrorMessage="<br>[Invalid Input]" 
                                             ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                             ValidationGroup="SaveRecord" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr runat="server" id="trLandOthers" visible="false">
                                           <td class="GridRows">&nbsp;Others:</td>
                                           <td class="GridRows"><asp:HiddenField ID="hdnOthers" runat="server" />
                                            <asp:TextBox runat="server" ID="txtLandOther" CssClass="controls" Width="25%" 
                                             BackColor="white" ValidationGroup="save"  MaxLength="10" AutoPostBack="True" 
                                             ontextchanged="txtLandOther_TextChanged" ></asp:TextBox>
                                      <asp:RegularExpressionValidator id="re11" runat="server" 
                                             ControlToValidate="txtLandOther" ErrorMessage="<br>[Invalid Input]" 
                                             ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                             ValidationGroup="SaveRecord" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    
                                    <tr runat="server" id="trAirFixTransportation" visible="false">
                                           <td class="GridRows" style="width:25%;">&nbsp;Fixed Transportation:</td>
                                           <td class="GridRows" style="width:75%;"><asp:HiddenField ID="hdnAirFixTranspo" runat="server" />
                                            <asp:TextBox runat="server" ID="txtAirFixedTransportation" CssClass="controls" Width="25%" 
                                             BackColor="White" ValidationGroup="save" MaxLength="10" AutoPostBack="True" 
                                             ontextchanged="txtAirFixedTransportation_TextChanged" ReadOnly="True"></asp:TextBox>
                                      <asp:RegularExpressionValidator id="re4" runat="server" 
                                                   ControlToValidate="txtAirFixedTransportation" 
                                                   ErrorMessage="<br>[Invalid Input]" ValidationExpression="^\d*([.]\d*)?|[.]\d+$" 
                                                   Display="Dynamic" ValidationGroup="SaveRecord" SetFocusOnError="true" 
                                                   ForeColor="Red"></asp:RegularExpressionValidator></td>
                                     </tr>
                                      <tr runat="server" id="trAirTerminalFee" visible="false">
                                           <td class="GridRows">&nbsp;Terminal Fee:</td>
                                           <td class="GridRows"><asp:HiddenField ID="hdnAirTerminalFee" runat="server" />
                                            <asp:TextBox runat="server" ID="txtAirTerminalFee" CssClass="controls" Width="25%"
                                                            BackColor="white" ValidationGroup="save" MaxLength="10" AutoPostBack="True" OnTextChanged="txtAirTerminalFee_TextChanged"
                                                            ReadOnly="True"></asp:TextBox>
                                                        <asp:ImageButton ID="btnViewTerminal" runat="server" ImageUrl="~/Support/ping16.png"
                                                            ToolTip="View Terminal" />
                                                        <ajax:ModalPopupExtender ID="pnlViewTerminal_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                                            DropShadow="true" DynamicServicePath="" Enabled="True" PopupControlID="pnlModal2Header"
                                                            TargetControlID="btnViewTerminal">
                                                        </ajax:ModalPopupExtender>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAirTerminalFee"
                                                            ErrorMessage="<br>[Invalid Input]" ValidationExpression="^\d*([.]\d*)?|[.]\d+$"
                                                            Display="Dynamic" ValidationGroup="SaveRecord" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        <br />
                                                                 <asp:Panel ID="pnlModal2Header" runat="server" CssClass="modalPopup2Header" >
                                                        <div align="center">
                                                            <table width="100%" >
                                                                <tr>
                                                                <td align="left" style="color: #FFFFFF" height="25px">&nbsp;&nbsp;<b>Terminals</b></td>
                                                                <td align="right">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                               </div>
                                                        <asp:Panel ID="pnlModal2" runat="server" CssClass="modalPopup2" onscroll="javascript:saveScroll();">
                                                            &nbsp;Please select the destination/s from the list below
                                                            <br />
                                                            <br />
                                                            <asp:CheckBoxList ID="cblTerminalFee" runat="server" BackColor="white" Font-Size="x-Small"
                                                                RepeatColumns="3" RepeatLayout="Table" Width="300px">
                                                            </asp:CheckBoxList>
                                                            <div align="right">
                                                                <asp:Button ID="btnCloseTerminal" runat="server" Text="Add" OnClick="btnCloseTerminal_Click" /></div>
                                                        </asp:Panel>
                                                        </asp:Panel>
                                      <asp:RegularExpressionValidator id="re9" runat="server" 
                                             ControlToValidate="txtAirTerminalFee" ErrorMessage="<br>[Invalid Input]" 
                                             ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                             ValidationGroup="SaveRecord" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                     </tr>
                                                                
                                      <tr runat="server" id="trSeaFerry" visible="false">
                                           <td class="GridRows">&nbsp;Ferry</td>
                                           <td class="GridRows"><asp:HiddenField ID="hdnSeaFerry" runat="server" />
                                            <asp:TextBox runat="server" ID="txtSeaFerry" CssClass="controls" Width="25%" 
                                             BackColor="white"  ValidationGroup="save" MaxLength="10" AutoPostBack="True" 
                                             ontextchanged="txtSeaFerry_TextChanged" ></asp:TextBox>
                                               <asp:RegularExpressionValidator id="re6" runat="server"  
                                                   ControlToValidate="txtSeaFerry" ErrorMessage="<br>[Invalid Input]" 
                                                   ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                                   ValidationGroup="SaveRecord" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                     </tr>
                                    
                                    <tr>
                                           <td class="GridRows" style="font-weight: bold">&nbsp;Total Transportation Allowance:</td>
                                           <td class="GridRows"><asp:TextBox runat="server" ID="txtTransportationTotal" 
                                             CssClass="controls" Width="150px" ReadOnly="True"></asp:TextBox> 
                                            <asp:Button ID="btnAddTranspoAllowance" runat="server" CssClass="controls"  
                                             Height="16px" onclick="btnAddTranspoAllowance_Click" Text="+" 
                                             ValidationGroup="save" Visible="False" /><asp:RequiredFieldValidator runat="server" ID="vldAL7" ControlToValidate="txtTransportationTotal" ErrorMessage="&lt;br&gt;[Press the + button to calculate Transportation Allowance]" Display="Dynamic" ValidationGroup="SaveRecord" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    </table>
                                  </div>
                           </td>
                         </tr>
                          
                                               
                           <tr>
                             <td>
                                     <%--APPROVERS--%>
                                    <div runat="server" id="divIncedental" visible="false" class="GridBorder"> 
                                            <table width="100%" cellpadding="3" cellspacing="2">
                                               <tr>
                                                  <td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Incidentals</b></td>
                                               </tr>
                                               
                                               <tr runat="server" id="trIncidentalTotal" visible="false">
                                                  <td class="GridRows" style="width:25%;"><b>&nbsp;Total Incidental  Allowance:</b></td>
                                                  <td class="GridRows" style="width:75%;">
                                                   <asp:HiddenField ID="hdnIncidentalTotal" runat="server" />
                                                   <asp:TextBox runat="server" ID="txtIncedentalsTotal" 
                                                    MaxLength="10" ReadOnly="True" CssClass="controls" BorderStyle="None" ></asp:TextBox></td>
                                               </tr>
                                               
                                               <tr>
                                                  <td  colspan="2" align="center" class="GridRows">
                                                         <asp:DataGrid runat="server" ID="dgIncedentals" AutoGenerateColumns="false"   
                                                          Width="60%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" 
                                                          ondeletecommand="dgIncedentals_DeleteCommand">
                                                         <Columns>
	                                                             <asp:TemplateColumn HeaderText="Incidental Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left"><ItemTemplate>



	                                                             <asp:TextBox runat="server" ID="txtIncedentalListName" Text='<%#DataBinder.Eval(Container.DataItem, "incdental")%>' CssClass="controls" BackColor="white" Width="98%" MaxLength="100" ReadOnly="True"></asp:TextBox></ItemTemplate>
                                                                 <HeaderStyle CssClass="GridColumns" Width="60%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" CssClass="GridRows"></ItemStyle></asp:TemplateColumn>
                                                 	              <asp:TemplateColumn HeaderText="Amount" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left">
	                                                               <ItemTemplate><asp:TextBox runat="server" ID="txtListAmount" Text='<%#DataBinder.Eval(Container.DataItem, "amount")%>' CssClass="controls" BackColor="white" Width="98%" MaxLength="50" ReadOnly="True"></asp:TextBox></ItemTemplate>
                                                              <HeaderStyle CssClass="GridColumns" Width="30%"></HeaderStyle>
                                                              <ItemStyle HorizontalAlign="Left" CssClass="GridRows"></ItemStyle>
	                                                        </asp:TemplateColumn>
   	                     
                                                             <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center"><ItemTemplate><asp:ImageButton id="dgListDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton></ItemTemplate>
                                                               <HeaderStyle CssClass="GridColumns"></HeaderStyle>
                                                               <ItemStyle HorizontalAlign="Center" CssClass="GridRows"></ItemStyle>
                                                             </asp:TemplateColumn>            
                                                          </Columns>

                                                        <HeaderStyle Font-Bold="True" Height="20px"></HeaderStyle>
                                                      </asp:DataGrid> 
                                                  </td>
                                               </tr>
                                               
                                                  <tr runat="server" id="trNoIncedental" visible="false" class="GridRows">
                                                       <td style="font-size:small; text-align:center" class="GridRows" colspan="2">&nbsp;There is no incidental item.</td>
                                                  </tr>
                                                  
                                                  <tr>
                                                          <td colspan="2" class="GridColumns" style="text-align: left;"><b>&nbsp;&nbsp;Add Incidentals</b></td>
                                                   </tr>      
                                                  <tr>
                                                         <td class="GridRows" style="width:25%;">&nbsp;Incidental:</td>
                                                         <td class="GridRows"  style="width:75%;"><asp:TextBox runat="server" 
                                                           ID="txtIncedentalName" CssClass="controls" Width="40%" BackColor="White" 
                                                           MaxLength="35"></asp:TextBox><asp:RequiredFieldValidator runat="server" 
                                                                 ID="vldAL6" ControlToValidate="txtIncedentalName" 
                                                                 ErrorMessage="&lt;br&gt;[Incidental Required]" Display="Dynamic" 
                                                                 ValidationGroup="AddIncidental" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                          <asp:Label ID="lblIncidentalError" runat="server" ForeColor="#FF3300" 
                                                           Visible="False"></asp:Label>
                                                         </td>                                               </tr>
                                                  <tr> 
                                                         <td class="GridRows" >&nbsp;Amount:</td>
                                                         <td class="GridRows">
                                                          <asp:TextBox runat="server" 
                                                           ID="txtIncedentalAmount" CssClass="controls" Width="25%" BackColor="White" 
                                                           MaxLength="10"></asp:TextBox>
                                                          <asp:RegularExpressionValidator id="re12" runat="server" 
                                                           ControlToValidate="txtIncedentalAmount" ErrorMessage="<br>[Invalid Input]" 
                                                           ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                                                           SetFocusOnError="true" ValidationGroup="AddIncidental" ForeColor="Red"></asp:RegularExpressionValidator>
                                                          <asp:RequiredFieldValidator runat="server" 
                                          ID="vldAL9" ControlToValidate="txtIncedentalAmount" 
                                          ErrorMessage="&lt;br&gt;[Amount Required]" Display="Dynamic" 
                                          ValidationGroup="AddIncidental" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator></td>
                                                  </tr>
                                                   <tr>
                                                        <td style="width:100%" colspan="2" class="GridRows"><div style="text-align:center" align="center"><asp:Button ID="btnIncedentalAdd" runat="server" Text="Add New Item"  onclick="btnIncedentalAdd_Click" ValidationGroup="AddIncidental" />
                                                        <%--<asp:ImageButton runat="server" ID="btnIncedentalAdd" ImageUrl="~/Support/btnAddItem.jpg" onclick="btnIncedentalAdd_Click" ValidationGroup="AddIncidental" />--%></div></td>
                                                       
                                                  </tr>   
                                            </table>
                                    </div>                         
                            </td>
                         </tr>
                         
                                             
                         <tr>
                            <td>
                                 <%--REPRESENTATION ALLOWANCE--%>
                                 <div runat="server" id="divRepresentation" visible="false" class="GridBorder" > 
                                       <table width="100%" cellpadding="3" cellspacing="1">
                                                               <tr>
                                                                   <td class="GridColumns" style="text-align:left;" colspan="3">&nbsp;<b>Representation</b></td>
                                                               </tr>
                                                               <tr runat="server" id="trRepresentation" visible="false">
                                                                   <td class="GridRows" style="width:25%;"><b>&nbsp;Total 
                                                                     Representation Allowance:</b></td>
                                                                    <td class="GridRows" colspan="1">
                                                                     <asp:HiddenField ID="hdnRepresentation" runat="server" />
                                                                     <asp:TextBox runat="server" ID="txtRepresentationAmount" 
                                                                      CssClass="controls" Width="40%" 
                                                                      MaxLength="10" ReadOnly="True" BorderStyle="None" ></asp:TextBox></td>
                                                                    
                                                                </tr>
                                                           
                                                           
                                                          <tr>
                                                                   <td align="center" class="GridRows" colspan="2">
                                                                     <asp:DataGrid runat="server" ID="dgRepresentation" AutoGenerateColumns="false"  
                                                                      Width="60%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" 
                                                                      ondeletecommand="btndgRepresentationDelete_DeleteCommand">
	                                                                    <Columns>
	                                                                      <asp:TemplateColumn HeaderText="Person Name" HeaderStyle-CssClass="GridColumns" 
                                                                           ItemStyle-CssClass="GridRows" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
	                                                                         <ItemTemplate><asp:TextBox runat="server" ID="txtPersonName" Text='<%#DataBinder.Eval(Container.DataItem, "rprsnttn")%>' CssClass="controls" BackColor="white" Width="98%" 
                                                                            MaxLength="100" ReadOnly="True"></asp:TextBox></ItemTemplate>
                                                                             <HeaderStyle CssClass="GridColumns" Width="90%"></HeaderStyle>
                                                                             <ItemStyle HorizontalAlign="Left" CssClass="GridRows"></ItemStyle>
	                                                                      </asp:TemplateColumn>
                                                                         <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
                                                                              <ItemTemplate><asp:ImageButton id="btndgRepresentationDelete" runat="server" 
                                                                            CommandName="Delete" AlternateText="[Delete Item]"  ImageUrl="~/Support/delete16.png"></asp:ImageButton></ItemTemplate>
                                                                              <HeaderStyle CssClass="GridColumns"></HeaderStyle>
                                                                              <ItemStyle HorizontalAlign="Center" CssClass="GridRows"></ItemStyle>
                                                                          </asp:TemplateColumn>            
                                                                   </Columns>
                                                               <HeaderStyle Font-Bold="True" Height="20px"></HeaderStyle>
                                                             </asp:DataGrid>
                                                          </td>
                                                      </tr>
                                                       <tr runat="server" id="trNoRepresentation" visible="false">
                                                          <td style="font-size:small; text-align:center" class="GridRows" colspan="3">&nbsp;There is no representation item.</td>
                                                        </tr>
                                                        
                                                        <tr>
                                                           <td colspan="2" class="GridColumns" style="text-align: left;"><b>&nbsp;Add Person</b></td>
                                                      </tr>      
                                                      <tr>
                                                           <td class="GridRows" style="width:25%;">&nbsp;Name:</td>
                                                           <td class="GridRows"  style=" height: 24px;" colspan="1">
                                                            <asp:TextBox runat="server" 
                                                             ID="txtRepresentationPerson" CssClass="controls" Width="50%" BackColor="White" 
                                                             MaxLength="35"></asp:TextBox>
                                                               <asp:RequiredFieldValidator runat="server" 
                                                             ID="vldAL5" ControlToValidate="txtRepresentationPerson" 
                                                             ErrorMessage="&lt;br&gt;[Person Required]" Display="Dynamic" 
                                                             ValidationGroup="AddRepresentation" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:Label ID="lblRepresentationError" runat="server" ForeColor="#FF3300" 
                                                             Visible="False"></asp:Label>
                                                                 </td>
                                                      </tr>
                                                      <tr>
                                                       <td class="GridRows" colspan="2" align="center">
                                                           <asp:Button ID="btnAddPerson" runat="server" Text="Add Person"  onclick="btnAddPerson_Click" ValidationGroup="AddRepresentation" /><%--<asp:ImageButton runat="server" ID="btnAddPerson" ImageUrl="~/Support/btnAddItem.jpg" onclick="btnAddPerson_Click" ValidationGroup="AddRepresentation" />--%></td>
                                                       </tr> 
                                         </table>
                                 </div>
                            </td>
                         </tr>
                         
                                                  
                         <tr>
                             <td >
                             <br />
                           <div style="width: 60%; text-align: center;">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td valign="top" align="left">
                                                        <table style="font-size: small;">
                                                            <tr>
                                                                <td style="width: 400px" align="center">
                                                                    <div class="GridBorder">
                                                                        <table width="50%" cellpadding="3">
                                                                            <tr>
                                                                                <td class="GridColumns" colspan="1" style="text-align: center;">
                                                                                    <b>Total Cash Advance for Travel Allowance<asp:Button ID="btnAddTravelAllowance"
                                                                                        runat="server" CssClass="controls" Height="16px" OnClick="btnAddTravelAllowance_Click"
                                                                                        Text="+" Visible="False" />
                                                                                    </b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="GridRows" align="center" style="height: 30px">
                                                                                    <b>
                                                                                        <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" CssClass="controls"
                                                                                            ReadOnly="True" Width="20px" Font-Bold="True" Font-Size="Larger">P</asp:TextBox>
                                                                                        <asp:TextBox runat="server" ID="txtTotalCATAAmount" CssClass="controls" Width="45%"
                                                                                            MaxLength="10" ValidationGroup="save" ReadOnly="True" BorderStyle="None" Font-Bold="True"
                                                                                            Font-Size="Larger" ForeColor="#FF3300"></asp:TextBox></b>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                <br />
                             </td>
                         </tr>
                         <tr>
                            <td>
                            
                                <%--APPROVERS--%>
                               <div runat="server" id="divApprovers" visible="false" class="GridBorder"> 
                                 <table width="100%" cellpadding="3" cellspacing="1">
                                   <tr>
                                        <td colspan="2" class="GridColumns" style="text-align: left;"><b>
                                         &nbsp;&nbsp;Approvers</b></td>
                                   </tr>      
                                    <tr>
                                        <td class="GridRows" style="width:20%; height: 24px;">&nbsp;Endorsed By:</td>
                                        <td class="GridRows"  style=" height: 24px;">
                                         <asp:DropDownList ID="ddlEndorsedBy1" runat="server" CssClass="controls">
                                         </asp:DropDownList>
                                        </td>
                                   </tr>
                                   <tr id="trEndorser2" runat="server" visible="true">
                                        <td class="GridRows" style="width:20%; height: 24px;">&nbsp;Endorsed By:</td>
                                        <td class="GridRows"  style=" height: 24px;">
                                         <asp:DropDownList ID="ddlEndorsedBy2" runat="server" CssClass="controls">
                                         </asp:DropDownList>
                                        </td>
                                   </tr>
                                   
                                   <tr id="Tr1" runat="server">
                                        <td class="GridRows" style="width:20%; ">&nbsp;Authorized By:</td>
                                        <td class="GridRows"><%--<asp:HiddenField ID="hdnAuthorizedby1" runat="server" />
                                         <asp:TextBox runat="server" ID="txtAuthorizeBy1" CssClass="controls" Width="50%" 
                                          ReadOnly="true" BorderStyle="None"></asp:TextBox>&nbsp;--%>
                                          <asp:DropDownList ID="ddlDivisionHead" runat="server" CssClass="controls">
                                                        </asp:DropDownList>
                                          </td>
                                   </tr>
                                    <tr id="trAuthorize2" runat="server" visible="true">
                                        <td class="GridRows" style="width:20%; ">&nbsp;Authorized By:</td>
                                        <td class="GridRows">
                                         <asp:DropDownList ID="ddlAuthorizeby2" runat="server" CssClass="controls">
                                         </asp:DropDownList>
                                        </td>
                                   </tr>
                                  </table>
                               </div>
                            
                                <div style="text-align:center;">
                                      <br />
                                      <asp:Button ID="btnSave" runat="server" Text="Save as Draft" onclick="btnSave_Click" ValidationGroup="SaveRecord" />
                                      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSaveAsDraft.png" onclick="btnSave_Click" ValidationGroup="SaveRecord" />--%>&nbsp;
                                      <asp:Button ID="btnSaveSubmit" runat="server" Text="Save and Submit"  ValidationGroup="SaveRecord" onclick="btnSaveSubmit_Click" />
                                      <%--<asp:ImageButton runat="server" ID="btnSaveSubmit" ImageUrl="~/Support/btnSaveSubmit.jpg" ValidationGroup="SaveRecord" onclick="btnSaveSubmit_Click" />--%>&nbsp;
                                      <asp:Button ID="btnCancel" runat="server" Text="Void"  onclick="btnCancel_Click" />
                                      <%--<asp:ImageButton runat="server" ID="btnCancel" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancel_Click" />&nbsp;<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
                                </div> 

                                
                              
                                </div>
                            </td>
                         </tr>  
                   
                            
            </table>
        </div>
      <%--</div>--%>
   </td>
  </tr>
  </table>

 <asp:Panel ID="pnlHeader" runat="server" CssClass="modalPopupHeader" >
                                            <div align="center">
                                            <table width="100%" >
                                                <tr>
                                                <td align="left" style="color: #FFFFFF">&nbsp;&nbsp;<b>Official Business Information</b></td>
                                                <td align="right"><asp:Button ID="btnClose" runat="server" Text="X" 
                                                        onclick="btnClose_Click" /></td>
                                                </tr>
                                            </table>
                                               </div>
                                        <asp:Panel ID="pnlModal" runat="server" CssClass="modalPopup" onscroll="javascript:saveScroll();">
                                            <br />
                                            <div id="Div1" class="" runat="server">
                                                <table width="100%" cellpadding="3" cellspacing="1" border="1">
                                                    <tr>
                                                        <td colspan="2" class="">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <b>OB Details</b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="" style="width: 20%">
                                                            OB Code:
                                                        </td>
                                                        <td class="" style="width: 80%">
                                                            <asp:TextBox runat="server" ID="txtOBCode" CssClass="controls" Width="80px" ReadOnly="true"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp; Date Filed:&nbsp;
                                                            <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="">
                                                            Requestor:
                                                        </td>
                                                        <td class="">
                                                            <asp:TextBox runat="server" ID="TextBox2" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="">
                                                            OB Status:
                                                        </td>
                                                        <td class="">
                                                            <asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true"
                                                                Font-Bold="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="" style="vertical-align: top;">
                                                            Reason:
                                                        </td>
                                                        <td class="">
                                                            <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="99%" MaxLength="255"
                                                                ReadOnly="true" Rows="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="" style="vertical-align: top;">
                                                            OB Type:
                                                        </td>
                                                        <td class="">
                                                            <asp:TextBox runat="server" ID="txtOBType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="">
                                                            Rendered To:
                                                        </td>
                                                        <td class="">
                                                            <asp:TextBox runat="server" ID="txtRenderedTo" CssClass="controls" Width="250px"
                                                                ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <br />
                                            <div class="">
                                                <table width="100%" cellpadding="3" cellspacing="1" border="1">
                                                    <tr>
                                                        <td colspan="2" class="">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <b>Schedule Details</b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="">
                                                            <div class="">
                                                                <table width="100%" cellpadding="5" cellspacing="1">
                                                                    <tr>
                                                                        <td class="">
                                                                            <b>Key In</b>
                                                                        </td>
                                                                        <td class="">
                                                                            <b>Key Out</b>
                                                                        </td>
                                                                        <td class="">
                                                                            <b>Updated By</b>
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Label ID="lblSchedule" runat="server" Text="Label"></asp:Label>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        </asp:Panel>
       </ContentTemplate>
       </asp:UpdatePanel>

  
</asp:Content>

