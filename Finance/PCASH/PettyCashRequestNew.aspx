<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="PettyCashRequestNew.aspx.cs" Inherits="Finance_PCASH_PettyCashRequestNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register assembly="eWorld.UI" namespace="eWorld.UI" tagprefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 604px;
        }
    .checkB
  {
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <table width="100%" cellpadding="0" cellspacing="0"> 
     <%--<asp:TextBox runat="server" ID="txtApproverDivision" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>--%><%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" onclick="btnSend_Click" />--%>
 
  <tr>
   <td class="style1">    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Request for Petty Cash</span></b>
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
              <asp:Label ID="lblRequestorName" runat="server" Text="Label"></asp:Label><%--<asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>--%>
              &nbsp;<asp:CheckBox ID="chkbExecutive" runat="server" AutoPostBack="True" CssClass="checkB" 
                  oncheckedchanged="chkbExecutive_CheckedChanged" Text="For Executive" 
                  Width="40%" />
             </td>
         </tr>
         <tr>
          <td class="GridRows">Date Funds Needed:</td>
          <td class="GridRows">
           <ew:CalendarPopup ID="dtpDateFromNeeded" runat="server" AutoPostBack="true" 
                  PopupLocation="Bottom" ControlDisplay="TextBoxImage" 
                  ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" 
                  TextBoxLabelStyle-Font-Size="X-Small" TextBoxLabelStyle-Font-Bold="true"></ew:CalendarPopup>
           &nbsp;
           </td>
         </tr>
         <tr>
          <td class="GridRows" style="vertical-align:top;">Project Title:</td>
          <td class="GridRows">
           <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" 
                  MaxLength="255" BackColor="white" Rows="4" ValidationGroup="ut"></asp:TextBox>
           <asp:RequiredFieldValidator runat="server" ID="reqReason" 
                  ErrorMessage="&lt;br&gt;[Project Title is required]" Display="Dynamic" 
                  ControlToValidate="txtReason" SetFocusOnError="true" ValidationGroup="ut" 
                  ForeColor="Red"></asp:RequiredFieldValidator>
          </td>
         </tr>
          <tr>
          <td class="GridRows" style="vertical-align:top;">Purpose:</td>
          <td class="GridRows" style="height: 9px">
              <asp:CheckBox ID="chkTransportation" runat="server" AutoPostBack="True" 
                  CssClass="checkB" Text="Transportation" Width="23%" 
                  oncheckedchanged="chkTransportation_CheckedChanged" />
              <asp:CheckBox ID="chkOthers" runat="server" AutoPostBack="True" 
                  CssClass="checkB" Text="Others" Width="17%" 
                  oncheckedchanged="chkOthers_CheckedChanged" />
          </td>
         </tr> 
         <tr runat="server" id="trPayeeType" visible="false">
          <td class="GridRows" style="vertical-align:top;">Payee:</td>
          <td class="GridRows" style="height: 9px">
           <asp:DropDownList runat="server" ID="ddlPayeeType" CssClass="controls" 
                  BackColor="white" AutoPostBack="true" 
                  onselectedindexchanged="ddlPayeeType_SelectedIndexChanged">
               <asp:ListItem Selected="True">self</asp:ListItem>
               <asp:ListItem>others</asp:ListItem>
           </asp:DropDownList>
          </td>
         </tr>
         <tr runat="server" id="trPayeeName" visible="false">
          <td class="GridRows" style="vertical-align:top;">Payee&#39;s Name:</td>
          <td class="GridRows" style="height: 9px">
           <asp:DropDownList runat="server" ID="ddlPayeeName" CssClass="controls" 
                  BackColor="white" AutoPostBack="true" 
                  onselectedindexchanged="ddlPayeeName_SelectedIndexChanged">
           </asp:DropDownList>
          </td>
         </tr>
         <tr runat="server" id="trOB">
          <td class="GridRows" style="vertical-align:top;">Filed OB:</td>
          <td class="GridRows">
              <asp:DropDownList ID="ddlFiledOB" runat="server" AutoPostBack="true" 
                  BackColor="white" CssClass="controls" 
                  onselectedindexchanged="ddlFiledOB_SelectedIndexChanged">
              </asp:DropDownList>
              <asp:ImageButton ID="btnViewOB" runat="server" ImageUrl="~/Support/viewmag16.png"
                                                Style="display: none;" OnClick="btnViewOB_Click" ToolTip="View OB Details" />
              <asp:ImageButton ID="btnViewOB1" runat="server" 
                  ImageUrl="~/Support/viewmag16.png" OnClick="btnViewOB1_Click" 
                  ToolTip="View OB Details" style="width: 16px" />
                  <ajax:ModalPopupExtender ID="pnlModal_ModalPopupExtender" runat="server" DynamicServicePath=""
                                                Enabled="True" TargetControlID="btnViewOB" BackgroundCssClass="modalBackground"
                                                PopupControlID="pnlHeader" CancelControlID="btnClose" DropShadow="true">
                                            </ajax:ModalPopupExtender>
          </td>
         </tr>       
            <tr>
                <td class="GridRows" style="vertical-align:top;">
                    Charge Type:</td>
                <td class="GridRows">
                    <asp:DropDownList ID="ddlChargeType" runat="server" AutoPostBack="true" 
                        BackColor="white" CssClass="controls" 
                        onselectedindexchanged="ddlChargeType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
         <tr runat="server" id="trAppDepartment" visible="false">
          <td class="GridRows">Charge To:</td>
          <td class="GridRows" style="height: 9px">
              <asp:DropDownList ID="ddlMainChargeTo" runat="server" AutoPostBack="True" 
                  BackColor="White" CssClass="controls" 
                  onselectedindexchanged="ddlMainChargeTo_SelectedIndexChanged">
              </asp:DropDownList>
             </td>
         </tr>
         <tr runat="server" id="trAppEndorser"  visible="false">
          <td class="GridRows">Endorser:</td>
          <td class="GridRows"><asp:DropDownList runat="server" ID="ddlRequestEndorser" 
                  CssClass="controls" BackColor="white" 
                  onselectedindexchanged="ddlRequestEndorser_SelectedIndexChanged"></asp:DropDownList></td>
         </tr>
         <tr runat="server" id="trAppOthers"  visible="false">
          <td class="GridRows">Specify:</td>
          <td class="GridRows" style="height: 9px">
              <asp:TextBox ID="txtAppOthers" runat="server" CssClass="controls" Width="200px"></asp:TextBox>
             </td>
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
          <tr runat="server" id="tr1">
          <td class="GridRows">Remarks:</td>
          <td class="GridRows">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="controls" Width="445px"></asp:TextBox>
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
         <asp:Button ID="btnSend" runat="server" Text="Submit" onclick="btnSend_Click" 
             ValidationGroup="ut" />
      &nbsp;
     <%-- <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
     <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" />
     </div>     
    </div>

         <br />

      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns" style="text-align:left;">&nbsp;<b>Requested Item Particulars</b></td></tr>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
<asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="60%"
                        HeaderStyle-Font-Bold="true" 
HeaderStyle-Height="20px" OnDeleteCommand="dgItems_DeleteCommand">
                        <Columns>
                            <asp:TemplateColumn HeaderText="Item List" HeaderStyle-CssClass="GridColumns"
                                ItemStyle-CssClass="GridRows" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemListName" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>'
                                        CssClass="controls" BackColor="white" Width="98%" MaxLength="100" ReadOnly="True"></asp:TextBox></ItemTemplate>
                                <HeaderStyle CssClass="GridColumns" Width="60%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="GridRows"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows"
                                HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtListAmount" Text='<%#DataBinder.Eval(Container.DataItem, "amount")%>'
                                        CssClass="controls" BackColor="white" Width="98%" MaxLength="50" ReadOnly="True"></asp:TextBox></ItemTemplate>
                                <HeaderStyle CssClass="GridColumns" Width="30%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="GridRows"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="dgListDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]"
                                        ImageUrl="~/Support/delete16.png"></asp:ImageButton></ItemTemplate>
                                <HeaderStyle CssClass="GridColumns"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" CssClass="GridRows"></ItemStyle>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Height="20px"></HeaderStyle>
                    </asp:DataGrid>
         </div>
        </td>
       </tr>
       <tr runat="server" id="trNoRequest" visible="false">
           <td style="font-size:small;" class="GridRows">&nbsp;There is no requested item.m.</td></tr>
      </table>
      <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr>
                                                    <td colspan="2" class="GridColumns" style="text-align: left;">
                                                        <b>&nbsp;&nbsp;Add Item to Request List</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="GridRows" style="width: 20%;">
                                                        &nbsp; Item Description: 
                                                    </td>
                                                    <td class="GridRows" style="width: 80%;">
                                                        <asp:TextBox runat="server" ID="txtIncedentalName" CssClass="controls" Width="40%"
                                                            BackColor="White" MaxLength="35"></asp:TextBox><asp:RequiredFieldValidator runat="server"
                                                                ID="vldAL6" ControlToValidate="txtIncedentalName" ErrorMessage="&lt;br&gt;[Item Description Required]"
                                                                Display="Dynamic" ValidationGroup="AddIncidental" 
                                                            SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:Label ID="lblIncidentalError" runat="server" ForeColor="#FF3300" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="GridRows">
                                                        &nbsp; Amount:
                                                    </td>
                                                    <td class="GridRows">
                                                        <asp:TextBox runat="server" ID="txtIncedentalAmount" CssClass="controls" Width="25%"
                                                            BackColor="White" MaxLength="10"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="re12" runat="server" ControlToValidate="txtIncedentalAmount"
                                                            ErrorMessage="<br>[Invalid Input]" ValidationExpression="^\d*([.]\d*)?|[.]\d+$"
                                                            Display="Dynamic" SetFocusOnError="true" ValidationGroup="AddIncidental" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator runat="server" ID="vldAL9" ControlToValidate="txtIncedentalAmount"
                                                            ErrorMessage="&lt;br&gt;[Amount Required]" Display="Dynamic" ValidationGroup="AddIncidental"
                                                            SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" colspan="2" class="GridRows">
                                                        <div style="text-align: center" align="center">
                                                            <asp:Button ID="btnIncedentalAdd" runat="server" Text="Add New Item" OnClick="btnIncedentalAdd_Click" ValidationGroup="AddIncidental" />
                                                            <%--<asp:ImageButton runat="server" ID="btnIncedentalAdd" ImageUrl="~/Support/btnAddItem.jpg"
                                                                OnClick="btnIncedentalAdd_Click" ValidationGroup="AddIncidental" />--%>
                                                            <br />
                                        <asp:Panel ID="pnlHeader" runat="server" CssClass="modalPopupHeader" >
                                            <div align="center">
                                            <table width="100%" >
                                                <tr>
                                                <td align="left" style="color: #FFFFFF;">&nbsp;&nbsp;<b>Official Business Information</b></td>
                                                <td align="right">
                                                    <asp:Button ID="btnClose" runat="server" Text="X" 
                                                        onclick="btnClose_Click" /></td>
                                                </tr>
                                            </table>
                                            </div>
                                        <asp:Panel ID="pnlModal" runat="server" CssClass="modalPopup" 
                                                onscroll="javascript:saveScroll();">
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
                                                            <asp:TextBox runat="server" ID="txtOBCode" CssClass="controls" Width="80px" 
                                                                ReadOnly="true"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp; Date Filed:&nbsp;
                                                            <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" 
                                                                ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="">
                                                            Requestor:
                                                        </td>
                                                        <td class="">
                                                            <asp:TextBox runat="server" ID="TextBox2" CssClass="controls" Width="200px" 
                                                                ReadOnly="true"></asp:TextBox>
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
                                                            <asp:TextBox runat="server" ID="txtReason0" CssClass="controls" Width="99%" MaxLength="255"
                                                                ReadOnly="true" Rows="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="" style="vertical-align: top;">
                                                            OB Type:
                                                        </td>
                                                        <td class="">
                                                            <asp:TextBox runat="server" ID="txtOBType" CssClass="controls" Width="200px" 
                                                                ReadOnly="true"></asp:TextBox>
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
                                                        </div>
                                                    </td>
                                                </tr>
      </table>
     </div>
   </td>
  </tr> 

  <tr>
   <td>    
       &nbsp;</td>
  </tr>   
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

