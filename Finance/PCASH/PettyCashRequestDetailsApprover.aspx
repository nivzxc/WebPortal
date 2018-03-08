<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="PettyCashRequestDetailsApprover.aspx.cs" Inherits="Finance_PCASH_PettyCashRequestDetailsApprover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div class="border" style="padding-top: 10px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
      <div runat="server" id="divError" class="ErrMsg" visible="false">
                  <b>Error during update. Please correct your data entries:</b>
                  <br />
                  <br />
                  <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
      <div style="text-align:center;" runat="server" id="divButtons2">
       <br />
          <asp:Button ID="btnSaveAndApprove2" runat="server" Text="Save &amp; Approve" 
              OnClick="btnSaveAndApprove_Click" Visible="False" BackColor="#3366FF" 
              ForeColor="White"/>
      &nbsp;<asp:Button ID="btnApprove2" runat="server" Text="Approve" 
              OnClick="btnApprove_Click" BackColor="#33CC33" ForeColor="White"/>
      &nbsp;<asp:Button ID="btnDisapprove2" runat="server" Text="Disapprove" 
              OnClick="btnDisApprove_Click" BackColor="#FF3300" ForeColor="White"/>
     </div>

      <div class="GridBorder">
       <table width="100%" cellpadding="3" class="Grid">
        <tr>
         <td class="GridRows">PCAS #:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblPCASCode" Font-Bold="True"></asp:Label>&nbsp;
            <%--<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>--%>
         </td>
        </tr>
        <tr>
         <td class="GridRows" style="width:25%;">Requesting Person:</td>
         <td class="GridRows" style="width:75%;"><asp:Label runat="server" ID="lblRequestor"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows" style="width:25%;">Payee&#39;s Name:</td>
         <td class="GridRows" style="width:75%;"><asp:Label runat="server" ID="lblPayeeName"></asp:Label></td>
        </tr>
        <tr>
          <td class="GridRows">Date Funds Needed:</td>
          <td class="GridRows"><asp:Label runat="server" ID="lblDataFundsNeeded"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Project Title:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblReason"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Purpose:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblClassification"></asp:Label></td>
         
        </tr>
        <tr>
         <td class="GridRows">Filed OB:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblFiledOB"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Charge Type:</td>
         <td class="GridRows"><asp:Label runat="server" ID="lblChargeType"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows" style="height: 9px">Charge To:</td>
         <td class="GridRows" style="height: 9px" ><asp:Label runat="server" ID="lblChargeTo"></asp:Label></td>
        </tr>
        <tr>
         <td class="GridRows">Remarks:</td>
         <td class="GridRows" ><asp:Label runat="server" ID="lblRemarks"></asp:Label></td>
        </tr>
        <tr id="trAssignedCashier" runat="server" visible="false">
         <td class="GridRows" style="height: 9px">Assigned Cashier:</td>
         <td class="GridRows" style="height: 9px" >
         <asp:DropDownList runat="server" ID="ddlCustodian" CssClass="controls" BackColor="white">
         </asp:DropDownList>
            </td>
        </tr>
         <tr id="trFPCFinalApprover" runat="server" visible="false">
         <td class="GridRows" style="height: 9px">FPC Final Approver:</td>
         <td class="GridRows" style="height: 9px" >
         <asp:DropDownList runat="server" ID="ddlFPCFinalApprover" CssClass="controls" 
                 BackColor="white">
         </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td colspan="2" class="GridColumns">
           <table>
            <tr>
             <td>&nbsp;PETTY CASH<b> SUB-DETAILS</b></td>
            </tr>
           </table>            
          </td>
         </tr>
        <tr>
         <td colspan="2" class="style1">
          <table width="100%" cellpadding="3" >
           <tr>
             <td colspan="2" class="GridColumns" style="text-align:center; height: 9px;"><b>Item Description</b></td>
             <td class="GridColumns" style="text-align:center; height: 9px;"><b>Amount</b></td>
           </tr>
               <% LoadPCASDetails();%>
          </table> 
         </td>
        </tr>

        <tr runat="server" id="trFPCD1">
          <td colspan="2" class="GridColumns">
           <table>
            <tr>
             <td class="style3">&nbsp;TO BE FILLED OUT BY<b> FINANCIAL PLANNING & CONTROL</b></td>
            </tr>
           </table>            
          </td>
         </tr>
        <tr runat="server" id="trFPCD2">
         <td colspan="2">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
          <table width="100%" cellpadding="3" >
              <tr>
                 <td class="GridRows" style="height: 1px">Approved RFA / Budget for the project:</td>
                 <td class="GridRows" style="height: 1px" >
                     <asp:TextBox ID="txtApprovedRFA" runat="server" style="text-align:right" 
                         AutoPostBack="True" ontextchanged="txtApprovedRFA_TextChanged" 
                         onunload="txtApprovedRFA_Unload">
                         </asp:TextBox>
                     <asp:RegularExpressionValidator ID="R1" runat="server" 
                         ControlToValidate="txtApprovedRFA" ErrorMessage="Invalid Input" ForeColor="Red" 
                         ValidationExpression="^\d*([.]\d*)?|[.]\d+$" ValidationGroup="pcasval"></asp:RegularExpressionValidator>
                 </td>
              </tr>
              <tr>
                 <td class="GridRows" style="height: 1px">Amount allocated / used up prior to this request:</td>
                 <td class="GridRows" style="height: 1px" >
                     <asp:TextBox ID="txtAmountAllocated" runat="server" style="text-align:right" 
                         AutoPostBack="True" ontextchanged="txtAmountAllocated_TextChanged" 
                         onunload="txtAmountAllocated_Unload"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="R2" runat="server" 
                         ControlToValidate="txtAmountAllocated" ErrorMessage="Invalid Input" 
                         ForeColor="Red" ValidationExpression="^\d*([.]\d*)?|[.]\d+$" 
                         ValidationGroup="pcasval"></asp:RegularExpressionValidator>
                 </td>
              </tr>
                 <tr>
                 <td class="GridRows" style="height: 1px">Net:</td>
                 <td class="GridRows" style="height: 1px" >
                     <asp:TextBox ID="txtNet" runat="server" style="text-align:right" 
                         ReadOnly="True" BackColor="AliceBlue"></asp:TextBox>
                 </td>
              </tr>
                 <tr>
                 <td class="GridRows" style="height: 1px">Amount per this request:</td>
                 <td class="GridRows" style="height: 1px" >
                     <asp:TextBox ID="txtRequestAmount" runat="server" style="text-align:right" 
                         ReadOnly="True" AutoPostBack="True" 
                         ontextchanged="txtRequestAmount_TextChanged" 
                         onunload="txtRequestAmount_Unload" BackColor="AliceBlue"></asp:TextBox>
                 </td>
              </tr>
                 <tr>
                 <td class="GridRows" style="height: 1px">Remaining budget for the fiscal year:</td>
                 <td class="GridRows" style="height: 1px" >
                     <asp:TextBox ID="txtRemainingBudget" runat="server" style="text-align:right" 
                         ReadOnly="True" BackColor="AliceBlue"></asp:TextBox>
                 </td>
              </tr>
          </table> 
                       </ContentTemplate></asp:UpdatePanel>
         </td>
        </tr>

         <tr runat="server" id="trFPC1" visible="false">
          <td colspan="2" class="GridColumns">
           <table>
            <tr>
             <td class="style2">Account Expenses List:</td>
            </tr>
           </table>            
          </td>
         </tr>
        <tr runat="server" id="trFPC2" visible="false">
         <td colspan="2" class="style1">
          <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand">
	          <Columns>          
	           <asp:TemplateColumn HeaderText="Item Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%" ItemStyle-Width="65%">
	            <ItemTemplate>	         
	             <table width="98%">
	              <tr><td><asp:TextBox runat="server" ID="txtItemDesc" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "aexpname")%>' BackColor="white" Width="95%" Wrap="true" MaxLength="100" ReadOnly="True"></asp:TextBox></td></tr>
	              <tr><td><asp:TextBox runat="server" ID="txtAmount" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "amount")%>' BackColor="white" Width="200px" Wrap="true" MaxLength="17" ReadOnly="True"></asp:TextBox></td></tr>
	             </table>         
	            </ItemTemplate>
	           </asp:TemplateColumn>     
	        
            <asp:TemplateColumn HeaderText="Charged To" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="25%">
             <ItemTemplate>
	             &nbsp;<asp:Label runat="server" ID="lblChargedTo" Text='<%#DataBinder.Eval(Container.DataItem, "chargeto")%>'>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>
	                     
            <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>
            </asp:TemplateColumn>
           	           
           </Columns>
          </asp:DataGrid>
         </div>
         </td>
        </tr>

        <tr runat="server" id ="trFPC3" visible="false">
        <td colspan="2">
        <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">                            
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Add Account Expenses to 
           List</b></td></tr>

       <tr>
        <td class="GridRows">Charge To:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlChargeTo" CssClass="controls" 
          BackColor="white" AutoPostBack="True" 
          onselectedindexchanged="ddlChargeTo_SelectedIndexChanged">
          <asp:ListItem>School</asp:ListItem>
          <asp:ListItem>Rc Group</asp:ListItem>
          <asp:ListItem>Others</asp:ListItem>
         </asp:DropDownList>
        </td>
       </tr>
       <tr  runat="server" id="trDueFrom" visible="true">
        <td class="GridRows">&nbsp;</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlSchool" CssClass="controls" 
          BackColor="white" onselectedindexchanged="ddlSchool_SelectedIndexChanged"></asp:DropDownList>
        </td>
       </tr>
       <tr  runat="server" id="trRc" visible="false">
        <td class="GridRows">&nbsp;</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlRcCode" CssClass="controls" 
          BackColor="white" onselectedindexchanged="ddlRcCode_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList>
        </td>
       </tr>
              <tr  runat="server" id="trOthers" visible="false">
        <td class="GridRows">Others:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtOthers" CssClass="controls" Width="200px" 
          MaxLength="18" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOthers" 
          ID="vldItemOthr" ErrorMessage="&lt;br&gt;[Name Required]" Display="Dynamic" 
          ValidationGroup="addother" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
              <tr>
        <td class="GridRows" style="width:25%;">Account Expense:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlAccountExpenses" CssClass="controls" 
          BackColor="white" 
                onselectedindexchanged="ddlAccountExpenses_SelectedIndexChanged"></asp:DropDownList>
         <asp:DropDownList runat="server" ID="ddlSchool0" CssClass="controls" 
          BackColor="white" onselectedindexchanged="ddlSchool_SelectedIndexChanged" 
                Visible="False"></asp:DropDownList>
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Amount:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtAmount" CssClass="controls" Width="200px" 
          MaxLength="14" BackColor="white" ValidationGroup="additem" Height="17px"></asp:TextBox>
         <asp:TextBox runat="server" ID="txtAmountOthers" CssClass="controls" Width="200px" 
          MaxLength="14" BackColor="white" ValidationGroup="additem" Height="17px" 
          Visible="False"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="vldSpec0" 
          ControlToValidate="txtAmountOthers" ErrorMessage="<br>[Amount Required]" 
          Display="Dynamic" ValidationGroup="addother" SetFocusOnError="true" 
                ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator runat="server" ID="vldSpec" 
                ControlToValidate="txtAmount" ErrorMessage="<br>[Amount Required]" 
                Display="Dynamic" ValidationGroup="additem" SetFocusOnError="true" 
                ForeColor="Red"></asp:RequiredFieldValidator>&nbsp;&nbsp;
         <asp:RegularExpressionValidator id="re3" runat="server" 
                ControlToValidate="txtAmount" ErrorMessage="<br>[Invalid Input]" 
                ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
                ValidationGroup="additem" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
         <asp:RegularExpressionValidator id="re4" runat="server" 
          ControlToValidate="txtAmountOthers" ErrorMessage="<br>[Invalid Input]" 
          ValidationExpression="^\d*([.]\d*)?|[.]\d+$" Display="Dynamic" 
          ValidationGroup="addother" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
        </td>
       </tr>
      </table>
     </div>
     <br />
     <div style="text-align:center;">
         <asp:Button ID="btnSaveAdd" runat="server" Text="Add Item"  
             OnClick="btnSaveAdd_Click" ValidationGroup="additem" style="height: 26px"/>
         <asp:Button ID="btnSaveAddOther" runat="server" Text="Add Item"  OnClick="btnSaveAddOther_Click" ValidationGroup="addother" Visible="False" />
         </div>
    </div>
        </td>
        </tr>
       </table>
      </div>


      <div style="text-align:center;" runat="server" id="divButtons">
      <br />
          <asp:Button ID="btnSaveAndApprove" runat="server" Text="Save &amp; Approve" 
              OnClick="btnSaveAndApprove_Click" Visible="False" 
              ValidationGroup="pcasval" BackColor="#3366FF" ForeColor="White"/>
      &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Approve" 
              OnClick="btnApprove_Click" BackColor="#33CC33" ForeColor="White"/>
      &nbsp;<asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" 
              OnClick="btnDisApprove_Click" BackColor="#FF3300" ForeColor="White"/>
     </div>
     </div>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="cphHead">
    <style type="text/css">
        .style1
        {
            height: 43px;
        }
        .style2
        {
            width: 593px;
        }
        .style3
        {
            width: 568px;
        }
    </style>
</asp:Content>

