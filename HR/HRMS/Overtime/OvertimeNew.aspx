<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="OvertimeNew.aspx.cs" Inherits="HR_HRMS_Overtime_OvertimeNew" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="cntOvertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>

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
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OvertimeMenu.aspx" class="SiteMap">Overtime</a> » 
     <a href="OvertimeNew.aspx" class="SiteMap">New Overtime</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New Overtime Application</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />
     
     <div class="GridBorder">
      <asp:UpdatePanel ID="upDetails" runat="server">
       <ContentTemplate>
        <table width="100%" cellpadding="3" cellspacing="1">
        <%-- <tr><td colspan="2" class="GridText">&nbsp;<b>Overtime Details</b></td></tr>--%>
         <tr>
          <td class="GridRows" style="width:20%">Requestor:</td>
          <td class="GridRows">
              <asp:Label ID="lblRequestorName" runat="server" Text="Label"></asp:Label><%--<asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>--%></td>
         </tr>
         <tr>
          <td class="GridRows">Overtime Start:</td>
          <td class="GridRows">
           <ew:CalendarPopup ID="dtpFromDate" runat="server" AutoPostBack="true" PopupLocation="Bottom" ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" TextBoxLabelStyle-Font-Size="X-Small" TextBoxLabelStyle-Font-Bold="true" ondatechanged="dtpFromDate_DateChanged"></ew:CalendarPopup>
           &nbsp;
           <ew:TimePicker ID="dtpFromTime" runat="server" DisplayUnselectableTimes="true" RoundUpMinutes="false" DisableTextBoxEntry="false" AutoPostBack="true" TextBoxLabelStyle-Width="50px" ImageUrl="~/Support/Time22.png" ControlDisplay="TextBoxImage" TextBoxLabelStyle-Font-Size="X-Small" TextBoxLabelStyle-Font-Bold="true" ontimechanged="dtpFromTime_TimeChanged"></ew:TimePicker>
          </td>
         </tr>
         <tr>
          <td class="GridRows">Overtime End:</td>
          <td class="GridRows">
           <ew:CalendarPopup ID="dtpToDate" runat="server" AutoPostBack="true" PopupLocation="Bottom" ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" TextBoxLabelStyle-Font-Size="X-Small" TextBoxLabelStyle-Font-Bold="true" ondatechanged="dtpToDate_DateChanged"></ew:CalendarPopup>
           &nbsp;
           <ew:TimePicker ID="dtpToTime" runat="server" DisplayUnselectableTimes="true" RoundUpMinutes="false" DisableTextBoxEntry="false" AutoPostBack="true" TextBoxLabelStyle-Width="50px" ImageUrl="~/Support/Time22.png" ControlDisplay="TextBoxImage" TextBoxLabelStyle-Font-Size="X-Small" TextBoxLabelStyle-Font-Bold="true" ontimechanged="dtpToTime_TimeChanged"></ew:TimePicker>
          </td>
         </tr>
         <tr>
          <td class="GridRows">Hours:</td>
          <td class="GridRows"><asp:TextBox runat="server" ID="txtHours" CssClass="controls" Width="50px" ReadOnly="true"></asp:TextBox></td>
         </tr>       
         <tr>
          <td class="GridRows" style="vertical-align:top;">Reason:</td>
          <td class="GridRows">
           <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="4" required></asp:TextBox>
           
          </td>
         </tr>
         <tr>
          <td class="GridRows" style="vertical-align:top;">OT Type:</td>
          <td class="GridRows">
           <asp:DropDownList runat="server" ID="ddlOTType" CssClass="controls" BackColor="white" AutoPostBack="true" onselectedindexchanged="ddlOTType_SelectedIndexChanged">
            <asp:ListItem Text="Charge within department" Value="0"></asp:ListItem>
            <asp:ListItem Text="Charge to other department" Value="1"></asp:ListItem>                    
           </asp:DropDownList>
          </td>
         </tr>       
         <tr runat="server" id="trRDepartment" visible="false">
          <td class="GridRows">Charge To:</td>
          <td class="GridRows"><asp:DropDownList runat="server" ID="ddlRequestDepartment" CssClass="controls" BackColor="white" onselectedindexchanged="ddlRequestDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
         </tr>
         <tr runat="server" id="trRApprover" visible="false">
          <td class="GridRows">Approver:</td>
          <td class="GridRows"><asp:DropDownList runat="server" ID="ddlRequestApprover" CssClass="controls" BackColor="white"></asp:DropDownList></td>
         </tr>
         <tr>
          <td class="GridRows">Head Approver:</td>
          <td class="GridRows"><asp:DropDownList runat="server" ID="ddlHeadApprover" CssClass="controls" BackColor="white"></asp:DropDownList></td>
         </tr>       
         <tr runat="server" id="trApproverDivision">
          <td class="GridRows">Division Head:</td>
          <td class="GridRows">
          <asp:DropDownList runat="server" ID="ddlDivisionHead" CssClass="controls" BackColor="white"></asp:DropDownList>

           <%--<asp:HiddenField runat="server" ID="hdnApproverDivision" />--%>
           <%--<asp:TextBox runat="server" ID="txtApproverDivision" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>--%>
          </td>
         </tr>
         <tr runat="server" id="trApproverCOO" visible="false">
          <td class="GridRows">Final Approver:</td>
          <td class="GridRows">
           <asp:HiddenField runat="server" ID="hdnApproverCOO" />
           <asp:TextBox runat="server" ID="txtApproverCOO" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
          </td>
         </tr>         
        </table>
       </ContentTemplate>
      </asp:UpdatePanel>      
     </div>     
     <br />
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" onclick="btnSend_Click" />--%>
         <asp:Button ID="btnSend" runat="server" Text="Submit" onclick="btnSend_Click"/>
      &nbsp;
     <%-- <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
     <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" />
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
        <p>You have filed your Overtime successfully!</p>           
          <br>         
        <font style="font-weight:bold;">Status:</font> For Approval
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="OvertimeMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>
  </div>
</div>

</asp:Content>