<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="PettyCashRequestNewExec.aspx.cs" Inherits="Finance_PCASH_PettyCashRequestNewExec" %>

<%@ Register assembly="eWorld.UI" namespace="eWorld.UI" tagprefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 604px;
        }
    .checkB
  {
   height: 20px;
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
              <asp:DropDownList ID="ddlExecutive" runat="server" AutoPostBack="true" 
                  BackColor="white" CssClass="controls" 
                  onselectedindexchanged="ddlPCashClass_SelectedIndexChanged">
              </asp:DropDownList>
              <asp:CheckBox ID="chkbExecutive" runat="server" AutoPostBack="True" CssClass="checkB" 
                  oncheckedchanged="chkbExecutive_CheckedChanged" Text="For Executive" 
                  Width="40%" Checked="True" />
             </td>
         </tr>
            <tr>
                <td class="GridRows">
                    Date Funds Needed:</td>
                <td class="GridRows">
                    <ew:CalendarPopup ID="dtpDateFromNeeded" runat="server" AutoPostBack="true" 
                        ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" 
                        PopupLocation="Bottom" TextBoxLabelStyle-Font-Bold="true" 
                        TextBoxLabelStyle-Font-Size="X-Small" TextBoxLabelStyle-Width="60px">
                    </ew:CalendarPopup>
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
          <td class="GridRows">
           <asp:CheckBox ID="chkTransportation" runat="server" AutoPostBack="True" 
                  CssClass="checkB" Text="Transportation" Width="23%" 
                  oncheckedchanged="chkTransportation_CheckedChanged" />
              <asp:CheckBox ID="chkOthers" runat="server" AutoPostBack="True" 
                  CssClass="checkB" Text="Others" Width="17%" 
                  oncheckedchanged="chkOthers_CheckedChanged" />
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
          <td class="GridRows">
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
          <td class="GridRows">
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

      <table width="100%" cellpadding="3" cellspacing="1" __designer:mapid="84">
       <tr __designer:mapid="85"><td class="GridColumns" style="text-align:left;" 
               __designer:mapid="86">&nbsp;<b __designer:mapid="87">Requested Item Particulars</b></td></tr>
       <tr __designer:mapid="88">
        <td class="GridRows" __designer:mapid="89">
         <div class="GridBorder" __designer:mapid="8a">
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
       <tr runat="server" id="trNoRequest" visible="false" __designer:mapid="9d">
           <td style="font-size:small;" class="GridRows" __designer:mapid="9e">&nbsp;There is no requested item.m.</td></tr>
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
                                                        &nbsp; 
                                                        Item Description: 
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
                                                                OnClick="btnIncedentalAdd_Click" ValidationGroup="AddIncidental" />--%></div>
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
</asp:Content>
