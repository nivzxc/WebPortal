<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="LeaveDetailsA.aspx.cs" Inherits="HR_HRMS_Leave_LeaveDetailsA" %>

<asp:Content ID="cntLeaveDetails" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
<asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
    <script type="text/javascript">
            
        function ModalSuccess() 
        {               
            $('#myModalSuccessApproval').modal('show');
        } 
        function ModalDisapprove() {
            $('#myModaldisapproval').modal('show');
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
     <a href="LeaveMenu.aspx" class="SiteMap">Leave</a> » 
     <a href="LeaveDetailsA.aspx?leavcode=<%Response.Write(Request.QueryString["leavcode"]); %>" class="SiteMap">Leave Details</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Application For Leave Details</span></b>     
     <br />
     
     <div runat="server" id="divError" visible="false">
      <br />
      <div class="ErrMsg"> 
       <b>Error during update. Please correct your data entries:</b><br />
       <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
     </div>
     <div style="text-align:center;" runat="server" id="div1">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" onclick="btnApprove_Click" />--%>
         <asp:Button ID="Button1" runat="server" Text="Approve" 
             onclick="btnApprove_Click" BackColor="#33CC33" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" onclick="btnDisApprove_Click" />--%>
         <asp:Button ID="Button2" runat="server" Text="Disapprove"  
             onclick="btnDisApprove_Click" BackColor="#FF3300" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />  --%>
         <asp:Button ID="Button3" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>   
     <br />           
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <%--<tr>
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
        <td class="GridRows" style="width:20%">Leave Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtLeaveCode" CssClass="controls" Width="100px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
         &nbsp;
         Date Filed:
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="width:20%">Requestor:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnRequestor" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Leave Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true" Font-Bold="true"></asp:TextBox></td>
       </tr>                    
       <tr>
        <td class="GridRows">Leave Type:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtLeaveType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnLeaveTypeCode" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Inclusive Dates:</td>
        <td class="GridRows">
         From:&nbsp;
         <asp:TextBox runat="server" ID="txtDateFrom" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
         &nbsp;To:
         <asp:TextBox runat="server" ID="txtDateTo" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Days:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtUnits" CssClass="controls" ReadOnly="true" Width="50px"></asp:TextBox>
         &nbsp;
         Available Balance: 
         <asp:TextBox runat="server" ID="txtBalance" CssClass="controls" ReadOnly="true" Width="50px"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="3" ValidationGroup="save"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Date Processed:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverDate" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverRemarks" CssClass="controls" Width="85%" TextMode="MultiLine" Rows="3" BackColor="White"></asp:TextBox></td>
       </tr>              
      </table>
     </div>

     <div style="text-align:center;" runat="server" id="divButtons">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" onclick="btnApprove_Click" />--%>
         <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             onclick="btnApprove_Click" BackColor="#33CC33" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" onclick="btnDisApprove_Click" />--%>
         <asp:Button ID="btnDisApprove" runat="server" Text="Disapprove"  
             onclick="btnDisApprove_Click" BackColor="#FF3300" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />  --%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>            
     
    </div>
   </td>
  </tr>  
  <%--Added by Charlie 1/4/2011--%>
   <tr><td style="height:9px;"></td></tr>
   <tr>
   <td>
             <asp:UpdatePanel ID="upDetails" runat="server">
          <ContentTemplate>  
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText"></span>
         <asp:DropDownList ID="ddlLeaveType" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlLeaveType_SelectedIndexChanged">
         </asp:DropDownList>
     <br />
     <asp:Label ID="lblLeaveType" 
      runat="server" CssClass="HeaderText" Text="Label" Visible="False"></asp:Label>
     </b>
     &nbsp;<br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridColumns" style="width:25%; text-align:center"><b>Date Filed</b></td>
        <td class="GridColumns" style="width:10%;text-align:center"><b>Days</b></td>
        <td class="GridColumns" style="width:40%;text-align:center"><b>Reason</b></td>
        <td class="GridColumns" style="width:20%;text-align:center"><b>Approver</b></td>
        <td class="GridColumns" style="width:5%;text-align:center"><b>Status</b></td>
        </tr>
          <asp:Label ID="lblWriteLeaveTypeList" runat="server" Text="[]"></asp:Label>
      <%-- <%LoadLeaveTypeListNoPay(); %>--%>
      </table>
     </div>
    </div>    
    </ContentTemplate>
     </asp:UpdatePanel>
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
  <%--Added by Charlie 1/4/2011--%>
 </table>

    <!-- Modal Approve Entry -->
<div id="myModalSuccessApproval" class="modal fade" role="dialog" style="position:fixed; width:100%; margin-top:150px;" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog modal-sm">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="background-color:lightgreen;">
      
        <h4 class="modal-title" style="text-align:center; color:white;">Success!</h4>
      </div>
      <div class="modal-body" style="text-align:center;">
          <br>
            <img src="/Support/approve-icon.png" width="32px" height="32px"/>
          <br>
          <br>
        <p>Overtime Approved successfully!</p>           
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="LeaveMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>
  </div>
</div>

      <!-- Modal Disapprove Entry -->
<div id="myModaldisapproval" class="modal fade" role="dialog" style="position:fixed; width:100%; margin-top:150px;" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog modal-sm">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="background-color:indianred;">
      
        <h4 class="modal-title" style="text-align:center; color:white;">Disapproved!</h4>
      </div>
      <div class="modal-body" style="text-align:center;">
          <br>
          <img src="/Support/disapprove-thumb.png" width="32px" height="32px"/>
          <br>
          <br>
        <p>You have disapproved the Overtime!</p>   
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="LeaveMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>
  </div>
</div>
</asp:Content>