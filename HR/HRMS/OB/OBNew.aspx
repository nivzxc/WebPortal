<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="OBNew.aspx.cs" Inherits="HR_HRMS_OB_OBNew" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="cntOBNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
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
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OBMenu.aspx" class="SiteMap">OB</a> » 
     <a href="OBNew.aspx" class="SiteMap">OB New</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New OB Application</span></b>
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
<%--       <tr><td colspan="2" class="GridText">&nbsp;<b>OB Details</b></td></tr>--%>
       <tr>
        <td class="GridRows" style="width:20%;">Requestor:</td>
        <td class="GridRows" style="width:80%;"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>      
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="3" ValidationGroup="ob"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqReason" 
                ErrorMessage="<br>[Reason is required]" Display="Dynamic" 
                ControlToValidate="txtReason" SetFocusOnError="true" ValidationGroup="ob" 
                ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">OB Type:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlOBType" CssClass="controls" BackColor="white" AutoPostBack="true" onselectedindexchanged="ddlOBType_SelectedIndexChanged" >
          <asp:ListItem Text="OB within department" Value="0"></asp:ListItem>
          <asp:ListItem Text="OB for other department" Value="1"></asp:ListItem>                    
         </asp:DropDownList>
        </td>
       </tr>       
       <tr runat="server" id="trRDepartment" visible="false">
        <td class="GridRows">OB Rendered To:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="controls" BackColor="white" AutoPostBack="true" onselectedindexchanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
        </td>
       </tr>
       <tr runat="server" id="trRApprover" visible="false">
        <td class="GridRows">Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlRequestApprover" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Head Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlHeadApprover" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>       
      </table>
     </div>
          <br />

     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns">&nbsp;<b>Add OB Schedule</b></td></tr>
       <tr>
        <td class="GridRows" style="text-align:right;">                 
         <div class="GridBorder">       
          <asp:UpdatePanel ID="upDetails" runat="server">
          <ContentTemplate>             
          <table width="100%" cellpadding="2">  
           <tr>
            <td class="GridColumns" style="width:30%"><b>Date</b></td>
            <td class="GridColumns" style="width:30%"><b>Time In</b></td>
            <td class="GridColumns" style="width:30%"><b>Time Out</b></td>
            <td class="GridColumns" style="width:10%"><b>Days</b></td>
           </tr>         
           <tr>           
            <td class="GridRows" style="text-align:center; background-color:;"><ew:CalendarPopup ID="dtpOBDate" runat="server" PopupLocation="Bottom"  ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" ondatechanged="dtpInDate_DateChanged" AutoPostBack="true"></ew:CalendarPopup></td>           
            <td class="GridRows" style="text-align:center;"><ew:TimePicker ID="dtpInTime" runat="server" DisplayUnselectableTimes="true"  RoundUpMinutes="false" DisableTextBoxEntry="false" TextBoxLabelStyle-Width="70px" ImageUrl="~/Support/kworldclock22.png" ControlDisplay="TextBoxImage"></ew:TimePicker></td>
            <td class="GridRows" style="text-align:center;"><ew:TimePicker ID="dtpOutTime" runat="server" DisplayUnselectableTimes="true" RoundUpMinutes="false" DisableTextBoxEntry="false" TextBoxLabelStyle-Width="70px" ImageUrl="~/Support/kworldclock22.png" ControlDisplay="TextBoxImage"></ew:TimePicker></td>
            <td class="GridRows" style="text-align:center;">
             <asp:DropDownList runat="server" ID="ddlDays" CssClass="controls" BackColor="White" Font-Size="Small">
              <asp:ListItem Text="01" Value="1"></asp:ListItem>
              <asp:ListItem Text="02" Value="2"></asp:ListItem>
              <asp:ListItem Text="03" Value="3"></asp:ListItem>
              <asp:ListItem Text="04" Value="4"></asp:ListItem>
              <asp:ListItem Text="05" Value="5"></asp:ListItem>
              <asp:ListItem Text="06" Value="6"></asp:ListItem>
              <asp:ListItem Text="07" Value="7"></asp:ListItem>
              <asp:ListItem Text="08" Value="8"></asp:ListItem>
              <asp:ListItem Text="09" Value="9"></asp:ListItem>
              <asp:ListItem Text="10" Value="10"></asp:ListItem>
              <asp:ListItem Text="11" Value="11"></asp:ListItem>
              <asp:ListItem Text="12" Value="12"></asp:ListItem>
              <asp:ListItem Text="13" Value="13"></asp:ListItem>
              <asp:ListItem Text="14" Value="14"></asp:ListItem>
              <asp:ListItem Text="15" Value="15"></asp:ListItem>
              <asp:ListItem Text="16" Value="16"></asp:ListItem>
              <asp:ListItem Text="17" Value="17"></asp:ListItem>
              <asp:ListItem Text="18" Value="18"></asp:ListItem>
              <asp:ListItem Text="19" Value="19"></asp:ListItem>
              <asp:ListItem Text="20" Value="20"></asp:ListItem>
              <asp:ListItem Text="21" Value="21"></asp:ListItem>
              <asp:ListItem Text="22" Value="22"></asp:ListItem>
              <asp:ListItem Text="23" Value="23"></asp:ListItem>
              <asp:ListItem Text="24" Value="24"></asp:ListItem>
              <asp:ListItem Text="25" Value="25"></asp:ListItem>
              <asp:ListItem Text="26" Value="26"></asp:ListItem>
              <asp:ListItem Text="27" Value="27"></asp:ListItem>
              <asp:ListItem Text="28" Value="28"></asp:ListItem>
              <asp:ListItem Text="29" Value="29"></asp:ListItem>
              <asp:ListItem Text="30" Value="30"></asp:ListItem>
              <asp:ListItem Text="31" Value="31"></asp:ListItem>
              <asp:ListItem Text="32" Value="32"></asp:ListItem>
              <asp:ListItem Text="33" Value="33"></asp:ListItem>
              <asp:ListItem Text="34" Value="34"></asp:ListItem>
              <asp:ListItem Text="35" Value="35"></asp:ListItem>
              <asp:ListItem Text="36" Value="36"></asp:ListItem>
              <asp:ListItem Text="37" Value="37"></asp:ListItem>
              <asp:ListItem Text="38" Value="38"></asp:ListItem>
              <asp:ListItem Text="39" Value="39"></asp:ListItem>
             </asp:DropDownList>
            </td>
           </tr>                                                                                                                         
          </table>
          </ContentTemplate>
          </asp:UpdatePanel>                     
         </div> 
         <br />        
        <%-- <asp:ImageButton runat="server" ID="btnAddNewItem" ImageUrl="~/Support/btnAddItem.jpg" onclick="btnAddNewItem_Click" />--%>
            <asp:Button ID="btnAddNewItem" runat="server" Text="Add New Item"  onclick="btnAddNewItem_Click" />
	       </td>
	      </tr>
	     </table>
	    </div>
     <br />

     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns">&nbsp;<b>OB Schedule Details</b></td></tr>       
       <tr>
        <td class="GridRows">
         <div class="GridBorder" runat="server" id="divScheduleList">
          <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" BorderStyle="Solid" ondeletecommand="dgSchedule_DeleteCommand">
           <Columns>	        
            <asp:TemplateColumn HeaderText="OB Date" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%">
             <ItemTemplate>
              <asp:HiddenField runat="server" ID="hdnFocusDate" Value='<%#DataBinder.Eval(Container.DataItem, "focsdate")%>' />
              <ew:CalendarPopup ID="dtpKeyOBDate" runat="server" PopupLocation="Bottom" ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" SelectedDate='<%#DataBinder.Eval(Container.DataItem, "keyin")%>' Enabled="false"></ew:CalendarPopup>
	            </ItemTemplate>                        

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="30%"></ItemStyle>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Time In" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%">
             <ItemTemplate>
              <ew:TimePicker ID="dtpKeyInTime" runat="server" DisplayUnselectableTimes="true" RoundUpMinutes="false" DisableTextBoxEntry="false" TextBoxLabelStyle-Width="70px" ImageUrl="~/Support/Time22.png" ControlDisplay="TextBoxImage" SelectedTime='<%#DataBinder.Eval(Container.DataItem, "keyin")%>'></ew:TimePicker>
	            </ItemTemplate>

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="30%"></ItemStyle>
	           </asp:TemplateColumn>	           
	           <asp:TemplateColumn HeaderText="Time Out" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%">
             <ItemTemplate>
              <ew:TimePicker ID="dtpKeyOutTime" runat="server" DisplayUnselectableTimes="true" RoundUpMinutes="false" DisableTextBoxEntry="false" TextBoxLabelStyle-Width="70px" ImageUrl="~/Support/Time22.png" ControlDisplay="TextBoxImage" SelectedTime='<%#DataBinder.Eval(Container.DataItem, "keyout")%>'></ew:TimePicker>
	            </ItemTemplate>

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="30%"></ItemStyle>
	           </asp:TemplateColumn>	                     
            <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>

<FooterStyle CssClass="GridColumns"></FooterStyle>

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" CssClass="GridRows" Width="10%"></ItemStyle>
            </asp:TemplateColumn>           	           
           </Columns>

<HeaderStyle Font-Bold="True" Height="0px"></HeaderStyle>
          </asp:DataGrid>
         </div>
         <asp:Label runat="server" ID="lblNoOBSchedule" Text="[No OB schedule added]" Font-Size="Small"></asp:Label>
        </td>
       </tr>
      </table>
     </div>


     
     <br />

     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" onclick="btnSend_Click" ValidationGroup="ob" />--%>
         <asp:Button ID="btnSend" runat="server" Text="Submit"  onclick="btnSend_Click" ValidationGroup="ob"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
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
        <p>You have filed your Official Business successfully!</p>
          <br>
        <font style="font-weight:bold;">Status:</font> For Approval
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="OBMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>

  </div>
</div>

</asp:Content>