<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="IARNew.aspx.cs" Inherits="HR_HRMS_IAR_IARNew" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntATWNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>

            <script language="javascript" type="text/javascript">
                var submit = 0;

                function CheckIsRepeat() {
                    if (++submit > 1) {
                        alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                        return false;
                    }
                }
    </script>

 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="IARMenu.aspx" class="SiteMap">Internet Access Request</a> » 
     <a href="IARNew.aspx" class="SiteMap">New</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New Internet Access Request</span></b>
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <%--<tr><td colspan="2" class="GridText">&nbsp;<b>Internet Access Request Details</b></td></tr>--%>
       <tr>
        <td class="GridRows" style="width:25%;">Requestor:</td>
        <td class="GridRows" style="width:75%;"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>   
       <tr>
        <td class="GridRows" style="width:25%;">Date Start:</td>
        <td class="GridRows" style="width:75%;"><cc1:GMDatePicker ID="dtpDateStart" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue" EnableTimePicker="true"></cc1:GMDatePicker></td>
       </tr> 
       <tr>
        <td class="GridRows" style="width:25%;">Date End:</td>
        <td class="GridRows" style="width:75%;"><cc1:GMDatePicker ID="dtpDateEnd" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue" EnableTimePicker="true"></cc1:GMDatePicker></td>
       </tr>               
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="3" ValidationGroup="ATWNew"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqReason" ErrorMessage="<br>[Reason is required]" Display="Dynamic" ControlToValidate="txtReason" SetFocusOnError="true" ValidationGroup="ATWNew"></asp:RequiredFieldValidator>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Department Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlApprover" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>
       <%--<tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:HiddenField ID="hdnDivisionHead" runat="server" />
         <asp:TextBox runat="server" ID="txtApproverDivision" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr> --%>
       <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>       
      </table>
     </div>     
     <br />
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" ValidationGroup="ATWNew" onclick="btnSend_Click" />--%>
         <asp:Button ID="btnSend" runat="server" Text="Submit"  ValidationGroup="ATWNew" onclick="btnSend_Click" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click" />
     </div>     
    </div>
   </td>
  </tr>  
 </table>
</asp:Content>