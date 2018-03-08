<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RFINew.aspx.cs" Inherits="CIS_RFI_RFINew" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntMRCFNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager id="sm" runat="server"></asp:ScriptManager> 
 <table width="100%" cellpadding="0" cellspacing="0"> 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="RFIMain.aspx" class="SiteMap">RFI</a> » 
     <a href="RFINew.aspx" class="SiteMap">Create New RFI</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New RFI</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="Grid">
       <tr><td class="GridColumns" style="text-align:left; height:20px;" colspan="2">&nbsp;<b>RFI Details</b></td></tr>
       <tr>
        <td class="GridRows" style="width:20%;">Requestor:</td>
        <td class="GridRows" style="width:80%;">
         <asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnDepartmentCode" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Intended For:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtIntended" CssClass="controls" Width="99%" MaxLength="100" BackColor="white" ValidationGroup="save"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqIntended" ErrorMessage="<br>[Intended For required]" Display="Dynamic" ControlToValidate="txtIntended" ValidationGroup="save" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlApprover" CssClass="controls" BackColor="white"></asp:DropDownList></td>
       </tr>     
       <tr>
        <td class="GridRows">Procurement:</td>
        <td class="GridRows">         
         <asp:TextBox runat="server" ID="txtProcurementName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnProcurementCode" />
        </td>
       </tr>
      </table>
     </div>
     
     <br />

     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="Grid">
       <tr><td class="GridColumns" style="text-align:left; height:20px;" colspan="2">&nbsp;<b>Requested Items</b></td></tr>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand">
	          <Columns>          
	           <asp:TemplateColumn HeaderText="Items Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%" ItemStyle-Width="75%">
	            <ItemTemplate>	         
	             <table width="98%">
	              <tr><td style="width:60px;">Desc:</td><td><asp:TextBox runat="server" ID="txtItemDesc" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "ItemDesc")%>' BackColor="white" Width="99%"></asp:TextBox></td></tr>
	              <tr><td style="vertical-align:top;">Details:</td><td><asp:TextBox runat="server" ID="txtDetails" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "ItemDetails")%>' TextMode="MultiLine" Rows="4" BackColor="white" Width="99%"></asp:TextBox></td></tr>
	             </table>         
	            </ItemTemplate>
	           </asp:TemplateColumn>     
	        
            <asp:TemplateColumn HeaderText="Date Needed" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="15%">
             <ItemTemplate>
	             &nbsp;<asp:Label runat="server" ID="lblDateNeeded" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateNeeded")).ToString("MM/dd/yyyy")%>'></asp:Label>
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
     <div style="text-align:center;"><asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" OnClick="btnSend_Click" ValidationGroup="save" /></div>     
    </div>
   </td>
  </tr>  

  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">                            
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="Grid">
       <tr><td class="GridColumns" style="text-align:left; height:20px;" colspan="2">&nbsp;<b>Add Item to Request List</b></td></tr>
       <tr>
        <td class="GridRows" style="width:25%;">Item Description:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtItem" CssClass="controls" Width="98%" MaxLength="100" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItem" ID="vldItem" ErrorMessage="<br>[Item Description Required]" Display="Dynamic" ValidationGroup="additem"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Date Needed:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dteDateNeeded" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Item Details:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtItemDetails" TextMode="MultiLine" CssClass="controls" Rows="5" Width="98%" MaxLength="200" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="vldItemDetails" ControlToValidate="txtItemDetails" ErrorMessage="<br>[Specification Required]" Display="Dynamic" ValidationGroup="additem"></asp:RequiredFieldValidator>
        </td>
       </tr>
      </table>
     </div>
     <br />
     <div style="text-align:center;"><asp:ImageButton runat="server" ID="btnSaveAdd" ImageUrl="~/Support/btnAddItem.jpg" OnClick="btnSaveAdd_Click" ValidationGroup="additem" /></div>
    </div>      
   </td>
  </tr> 
  
 </table>
</asp:Content>