<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RFPEditRequest.aspx.cs" Inherits="Finance_RFP_RFPEditRequest" Title="The Official STI Head Office Website" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <asp:ScriptManager id="sm" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
  <table width="100%" cellpadding="0" cellspacing="0"> 
  <%--<tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../Finance.aspx" class="SiteMap">Finance</a> » 
     <a href="RFPMenu.aspx" class="SiteMap">Request For Payment</a> » 
     <a href="RFPNewRequest.aspx" class="SiteMap">Create New Request</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Edit Request for Payment</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />
     
     <div class="GridBorder">
    <table width="100%" cellpadding="3" cellspacing="1">
<%--       <tr><td class="GridColumns" style="text-align:left; height:20px;" colspan="2">&nbsp;<b>Request for Payment Details</b></td></tr>
--%>       <tr>
        <td class="GridRows" style="width:25%;">Control Number:</td>
        <td class="GridRows" style="width:75%;">
         <asp:TextBox runat="server" ID="txtControlNumber" CssClass="controls" Width="40%" 
          ValidationGroup="save" MaxLength="80" ReadOnly="True" BorderStyle="None" ></asp:TextBox>
        </td>
       <tr>
        <td class="GridRows">Payee:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequestor" CssClass="controls" Width="40%" 
          ValidationGroup="save" MaxLength="80" ReadOnly="True" BorderStyle="None" ></asp:TextBox>
        </td>
       <tr>
        <td class="GridRows">Request Type:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlRequestType" CssClass="controls" 
                BackColor="white" 
                onselectedindexchanged="ddlRequestType_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList>
        </td>
       </tr>
      <tr id="trRequestFor" runat="server" visible="false">
        <td class="GridRows">Request For:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtIntended" CssClass="controls" Width="95%" 
          MaxLength="80" BackColor="white" ValidationGroup="save"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Project Title:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtProjectTitle" CssClass="controls" 
          Width="95%" MaxLength="80" BackColor="white" ValidationGroup="save"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Reference RFA Number:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRFANumber" CssClass="controls" Width="25%" BackColor="white" ValidationGroup="save" MaxLength="15" ></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Date Check Needed:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpDateNeeded" runat="server"  
          CssClass="controls2" DisplayMode="Label" DateFormat="MM/dd/yyyy"  
          BackColor="white" CalendarTheme="Blue" Font-Names="Angsana New" 
          Font-Size="Large"></cc1:GMDatePicker></td>
       </tr> 
       <tr>
        <td class="GridRows">Supporting Documents:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSupportingDocuments" CssClass="controls" 
          Width="85%" MaxLength="85" BackColor="white" ValidationGroup="save"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Remarks / Comments:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" Width="85%" 
          MaxLength="85" BackColor="white" ValidationGroup="save"></asp:TextBox>
        </td>
       </tr>
<tr>
                                        <td class="GridRows">Endorsed By:</td>
                                        <td class="GridRows" valign="middle"><asp:DropDownList ID="ddlEndorsedBy1" runat="server" CssClass="controls" onselectedindexchanged="ddlEndorsedBy1_SelectedIndexChanged" BackColor="White"></asp:DropDownList>&nbsp;<asp:ImageButton ID="btbAddEndorser2" runat="server" CssClass="controls" ImageUrl="~/Support/btnAddEndorser.png" onclick="btnAddEndorser2_Click" ToolTip="Add Endorser" ImageAlign="AbsMiddle" /></td>
                                   </tr>
                                   <tr runat="server" id="trEndorseBy2" visible="false" >
                                        <td class="GridRows">Endorsed By:</td>
                                        <td class="GridRows">
                                         <asp:DropDownList ID="ddlEndorsedBy2" runat="server" CssClass="controls" 
                                          onselectedindexchanged="ddlEndorsedBy2_SelectedIndexChanged" 
                                          BackColor="White">
                                         </asp:DropDownList>
                                         &nbsp;<asp:ImageButton ID="btnAddEndorser3" runat="server" CssClass="controls" 
                                          ImageUrl="~/Support/btnRemoveEndorser.png" ToolTip="Remove Endorser" 
                                          ImageAlign="AbsMiddle" onclick="btnAddEndorser3_Click" />
                                        </td>
                                   </tr>
       <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
                                         <asp:DropDownList ID="ddlAuthorized" 
          runat="server" CssClass="controls" 
                                          
          onselectedindexchanged="ddlEndorsedBy2_SelectedIndexChanged" BackColor="White">
                                         </asp:DropDownList>
        </td>
       </tr>
        </table>
     </div>
     
     <br />

     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Requested Item Particulars</b></td></tr>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand">
	          <Columns>          
	           <asp:TemplateColumn HeaderText="Item Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%" ItemStyle-Width="65%">
	            <ItemTemplate>	         
	             <table width="98%">
	              <tr><td><asp:TextBox runat="server" ID="txtItemDesc" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' BackColor="white" Width="95%" Wrap="true" MaxLength="100" ReadOnly="True"></asp:TextBox></td></tr>
	              <tr><td><asp:TextBox runat="server" ID="txtAmount" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "amount")%>' BackColor="white" Width="200px" Wrap="true" MaxLength="17" ReadOnly="True"></asp:TextBox></td></tr>
	              <%--<tr><td>Amount: <asp:Label runat="server" ID="lblAmount" Text='<%#DataBinder.Eval(Container.DataItem, "amount")%>'></asp:Label></td></tr>--%>
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
       <tr runat="server" id="trNoRequest" visible="false"><td style="font-size:small;" class="GridRows">&nbsp;There is no requested item.</td></tr>
      </table>
     </div>
     <br />     
     <div style="text-align:center;">
    <%-- <asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSave.jpg" OnClick="btnSend_Click" ValidationGroup="save" />--%>
         <asp:Button ID="btnSend" runat="server" Text="Save as Draft"  OnClick="btnSend_Click" ValidationGroup="save"/>
         &nbsp;
         <%--<asp:ImageButton runat="server" ID="btnPrint" ImageUrl="~/Support/btnSubmitPrint.jpg" onclick="btnPrint_Click"/>--%>
         <asp:Button ID="btnPrint" runat="server" Text="Submit"  
             onclick="btnPrint_Click"  ValidationGroup="save"/>
         &nbsp;
         <%--<asp:ImageButton runat="server" ID="btnCancel" ImageUrl="~/Support/btnCancel.jpg" onclick="btnCancel_Click" />--%>
         <asp:Button ID="btnCancel" runat="server" Text="Void"  onclick="btnCancel_Click"  />
         &nbsp;
         <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click" />
     </div>     
    </div>
   </td>
  </tr>  

  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">                            
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Add Item to Request List</b></td></tr>
       <tr>
        <td class="GridRows" style="width:25%;">Item Code/Description:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtItemDescription" CssClass="controls" Width="98%" MaxLength="100" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:TextBox runat="server" ID="txtItemDescriptionOther" CssClass="controls" 
          Width="98%" MaxLength="100" BackColor="white" ValidationGroup="additem" 
          Visible="False"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemDescription" 
                ID="vldItem" ErrorMessage="<br>[Item Description Required]" Display="Dynamic" 
                ValidationGroup="additem" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator runat="server" 
          ControlToValidate="txtItemDescriptionOther" ID="vldItem0" 
          ErrorMessage="<br>[Item Description Required]" Display="Dynamic" 
          ValidationGroup="addother" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
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
       <tr  runat="server" id="trSchool" visible="true">
        <td class="GridRows">&nbsp;</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlSchool" CssClass="controls" 
          BackColor="white" ></asp:DropDownList>
        </td>
       </tr>
       <tr  runat="server" id="trRc" visible="false">
        <td class="GridRows">&nbsp;</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlRcCode" CssClass="controls" 
          BackColor="white" ></asp:DropDownList>
        </td>
       </tr>
       <tr  runat="server" id="trOthers" visible="false">
        <td class="GridRows">Name:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtOthers" CssClass="controls" Width="200px" 
          MaxLength="18" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOthers" 
          ID="vldItemOthr" ErrorMessage="&lt;br&gt;[Name Required]" Display="Dynamic" 
          ValidationGroup="addother" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
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
     <%--<asp:ImageButton runat="server" ID="btnSaveAdd" ImageUrl="~/Support/btnAddItem.jpg" OnClick="btnSaveAdd_Click" ValidationGroup="additem" />--%>
         <asp:Button ID="btnSaveAdd" runat="server" Text="Add Item"  OnClick="btnSaveAdd_Click" ValidationGroup="additem" />
     <%--<asp:ImageButton runat="server" ID="btnSaveAddOther" ImageUrl="~/Support/btnAddItem.jpg" OnClick="btnSaveAddOther_Click" ValidationGroup="addother" Visible="False" />--%>
         <asp:Button ID="btnSaveAddOther" runat="server" Text="Add Item"  
             OnClick="btnSaveAdd_Click" ValidationGroup="addother"  Visible="False"/>
     </div>
    </div>      
   </td>
  </tr> 
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
 
</asp:Content>

