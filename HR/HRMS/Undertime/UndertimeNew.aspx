<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="UndertimeNew.aspx.cs" Inherits="HR_HRMS_Undertime_UndertimeNew" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>


<asp:Content ID="cntUndertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">

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
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="UndertimeMenu.aspx" class="SiteMap">Undertime</a> » 
     <a href="UndertimeNew.aspx" class="SiteMap">New Undertime</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New Undertime Application</span></b>
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />

     <div class="ErrMsg"> 
      Under time is defined as logging-out between 3:00 to 6:00 PM.
      <br />
      Logging out before 3 pm is considered a half-day leave and should be filed with corresponding leave application.
     </div>     
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>Undertime Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:18%;">Requestor:</td>
        <td class="GridRows" style="width:82%;">
            <asp:Label ID="lblRequestorName" runat="server" Text="Label"></asp:Label><%--<asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>--%></td>
       </tr>      
       <tr>
        <td class="GridRows">Applied Date:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpAppliedDate" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue" EnableTimePicker="true"></cc1:GMDatePicker></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="99%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="4" ValidationGroup="ut"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqReason" 
                ErrorMessage="<br>[Reason is required]" Display="Dynamic" 
                ControlToValidate="txtReason" SetFocusOnError="true" ValidationGroup="ut" 
                ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlApprover" CssClass="controls" BackColor="white"></asp:DropDownList></td>
       </tr>
      </table>
     </div>
     
     <br />
     <div style="text-align:center;">

      <%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" onclick="btnSend_Click" ValidationGroup="ut" />--%>
         <asp:Button ID="btnSend" runat="server" Text="Submit"  onclick="btnSend_Click" ValidationGroup="ut" />
      &nbsp;
     <%-- <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"  />
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
        <p>You have filed your Undertime successfully!</p>
          <br>
        <font style="font-weight:bold;">Status:</font> For Approval
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="UndertimeMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>

  </div>
</div>


</asp:Content>