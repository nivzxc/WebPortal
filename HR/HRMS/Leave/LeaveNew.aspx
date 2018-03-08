<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="LeaveNew.aspx.cs" Inherits="HR_HRMS_Leave_LeaveNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="cntLeaveNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager runat="server" ID="smP"> </asp:ScriptManager>

           <script language="javascript" type="text/javascript">
               var submit = 0;

               function CheckIsRepeat() {
                   if (++submit > 1) {
                       alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                       return false;
                   }
               }
               function ModalSuccess() {
                   $('#myModalSuccessEntry').modal('show');
               }           
    </script>
 <table width="100%" cellpadding="0" cellspacing="0"> 
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="LeaveMenu.aspx" class="SiteMap">Leave</a> » 
     <a href="LeaveNew.aspx" class="SiteMap">New Leave</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New Application For Leave</span></b>
     <br />

     <div runat="server" id="divError" visible="false">
      <br />
      <div class="ErrMsg"> 
       <b>Error during update. Please correct your data entries:</b><br />
       <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
     </div>
    
     <br />  
     <br />     
            
     <div class="GridBorder">
      <asp:UpdatePanel ID="upLeave" runat="server">
       <ContentTemplate>       
        <table width="100%" cellpadding="3" cellspacing="1">
       <%--  <tr>
          <td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../../../Support/Paper22.png" alt="" /></td>
             <td>&nbsp;<b>Application For Leave Details</b></td>
            </tr>
           </table>         
          </td>
         </tr>--%>
         <tr>
          <td class="GridRows" style="width:20%">Requestor:</td>
          <td class="GridRows" style="width:80%" colspan="3">
              <asp:Label ID="lblRequestorName" runat="server" Text="Label"></asp:Label><%--<asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>--%></td>
         </tr>           
         <tr>
          <td class="GridRows">Leave Type:</td>
          <td class="GridRows" colspan="3"><asp:DropDownList runat="server" ID="ddlType" CssClass="controls" BackColor="White" AutoPostBack="true" onselectedindexchanged="ddlType_SelectedIndexChanged"></asp:DropDownList></td>
         </tr>       
 
         <tr>
          <td class="GridRows"  style="width:20%">From:</td>
          <td class=""  style="width:40%">
                 <ew:CalendarPopup ID="dtpFrom" runat="server" CssClass="" 
                         DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" 
                         CalendarTheme="Blue" EnableTimePicker="false" EnableDropShadow="True" 
                         MaxDate="2020-12-31" MinDate="2000-01-01" 
                         OnDateChanged="dtpFrom_DateChanged" AutoPostBack="True" 
                         ControlDisplay="TextBoxImage" ImageUrl="~/Support/calendarbutton.png"></ew:CalendarPopup>
          </td>
          <td class="GridRows"   style="width:40%">
                 &nbsp;&nbsp;<asp:DropDownList ID="ddlFromTime" runat="server" CssClass="controls" BackColor="White" AutoPostBack="true" onselectedindexchanged="ddlFromTime_SelectedIndexChanged"></asp:DropDownList>
          </td>
         </tr>    
         <tr>
          <td class="GridRows"   style="width:20%">To:</td>
          <td class="GridRows"   style="width:40%">
                  <ew:CalendarPopup ID="dtpTo" runat="server" CssClass="" 
                         DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" 
                         CalendarTheme="Blue" EnableTimePicker="false" EnableDropShadow="True" 
                         MaxDate="2020-12-31" MinDate="2000-01-01" 
                         OnDateChanged="dtpTo_DateChanged" AutoPostBack="True" 
                         ControlDisplay="TextBoxImage" ImageUrl="~/Support/calendarbutton.png"></ew:CalendarPopup>
          </td>
          <td class="GridRows"   style="width:40%">
                 &nbsp;&nbsp;<asp:DropDownList ID="ddlToTime" runat="server" CssClass="controls" BackColor="White" AutoPostBack="true" onselectedindexchanged="ddlToTime_SelectedIndexChanged"></asp:DropDownList>
          </td>
         </tr>  
         <tr>
          <td class="GridRows">Days:</td>
          <td class="GridRows" colspan="3">
           <asp:TextBox runat="server" ID="txtUnits" CssClass="controls" ReadOnly="true" Width="50px" Font-Bold="true"></asp:TextBox>
           &nbsp;Remaining Balance:&nbsp;
           <asp:TextBox runat="server" ID="txtBalance" CssClass="controls" ReadOnly="true" Width="50px"></asp:TextBox>         
          </td>
         </tr>       
         <tr>
          <td class="GridRows" style="vertical-align:top;">Reason:</td>
          <td class="GridRows" colspan="3">
           <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="4" ValidationGroup="save"></asp:TextBox>
           <asp:RequiredFieldValidator runat="server" ID="reqReason" ErrorMessage="<br>[Reason is required]" Display="Dynamic" ControlToValidate="txtReason" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
          </td>
         </tr>
         <tr>
          <td class="GridRows">Approver:</td>
          <td class="GridRows" colspan="3"><asp:DropDownList runat="server" ID="ddlApprover" CssClass="controls" BackColor="white"></asp:DropDownList></td>
         </tr>
        </table>
       </ContentTemplate>
      </asp:UpdatePanel>        
     </div>
    
     <br />
     <div style="text-align:center;">
     <%-- <asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" ValidationGroup="save" onclick="btnSend_Click" />--%>
         <asp:Button ID="btnSend" runat="server" Text="Submit" ValidationGroup="save" onclick="btnSend_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>     
    </div>
   </td>
  </tr>  
  
  <tr><td style="height:9px;"></td></tr>
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Leave Balance Summary</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:50%;"><b>Leave Type</b></td>
        <td class="GridColumns" style="width:15%;"><b>Entitlement</b></td>
        <td class="GridColumns" style="width:15%;"><b>Used</b></td>
        <td class="GridColumns" style="width:15%;"><b>Remaining</b></td>
       </tr>
       <%LoadLeaveBalance(); %>
      </table>
     </div>
    </div>     
   </td>
  </tr>  
  
 </table>

<!-- Modal Success Entry -->
<div id="myModalSuccessEntry" class="modal fade" role="dialog" style="position:fixed; width:100%; margin-top:150px;" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog modal-sm">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="background-color:lightgreen;">
      
        <h4 class="modal-title" style="text-align:center; color:white;">Success!</h4>
      </div>
      <div class="modal-body" style="text-align:center;">
          <br>
          <img src="/Support/Approve32.png"/>
          <br>
          <br>
        <p>You have filed you leave successfully!</p>
          <br>
        <font style="font-weight:bold;">Status:</font> For Approval
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="LeaveMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>

  </div>
</div>



</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphHead">
    <link rel="Stylesheet" type="text/css" href="PortalLogin.css" />
</asp:Content>
