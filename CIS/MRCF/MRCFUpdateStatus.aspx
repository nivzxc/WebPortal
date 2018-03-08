<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="MRCFUpdateStatus.aspx.cs" Inherits="CIS_MRCF_MRCFUpdateStatus" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="cntMRCFRequest" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
     <cc1:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">
    </cc1:toolkitscriptmanager>
    
     <ContentTemplate>


    <div id ="divModal" runat="server" class="modalPopUpReassign" style="display: inherit">

     <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"  
               PopupControlID="divModal" TargetControlID="btnReAssign" 
               BackgroundCssClass="modalBackgroundMRCF">
          </cc1:ModalPopupExtender>

      
        <center>
            <asp:LinkButton ID="lbtnHide" runat="server" onclick="lbtnHide_Click">Close</asp:LinkButton>
        </center> 
     
     <table width="100%"  class="grid"  runat="server" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
       <tr>
        <td class="GridRows" width="35%">Current Handler :</td>
        <td class="GridRows" colspan="3">   <% LoadCurrentHandler(); %>     </td></tr>
      <tr>
        <td class="GridRows" >Reassign to :</td>
        <td colspan="3" class="GridRows">
        
            <asp:DropDownList runat="server" 
                ID="ddlReassign" CssClass="controls" BackColor="white"> </asp:DropDownList>
        </td>
       </tr>
          <tr>
        <td class="GridRows" >Remarks:</td>
        <td colspan="3" class="GridRows">
            <asp:TextBox runat="server" ID="txtReassignRemarks" 
                CssClass="controls" Width="99%" TextMode="MultiLine" Rows="2" 
                MaxLength="200"></asp:TextBox></td>
       </tr>
        </table>
   
        <center>
        <asp:Button ID="btnUpdateReassign" runat="server" Text="Update MRCF Handler" 
                Width="150px" onclick="btnUpdateReassign_Click" 
               /></center>

  </div>




<div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:0px;" id="DivUpdateStatus" runat="server">
<b><span class="HeaderText">MRCF Current Status<br />
    </span></b>
&nbsp;<table width="75%"  class="grid"  runat="server">
       <tr>
        <td class="GridRows" width = '30%'>Current Status :</td>
        <td class="GridRows" colspan="2" > <% LoadCurrentStatus(); %></td></tr>
      <tr>
        <td class="GridRows" >Status :</td>
        <td colspan="2" class="GridRows">
        
            <asp:DropDownList runat="server" 
                ID="ddlAssignStatus" CssClass="controls" BackColor="white"> </asp:DropDownList>
        </td>
       </tr>
          <tr>
        <td class="GridRows" >Remarks:</td>
        <td colspan="2" class="GridRows"><asp:TextBox runat="server" ID="txtAssignRemarks" 
                CssClass="controls" Width="99%" TextMode="MultiLine" Rows="2" 
                MaxLength="200"></asp:TextBox></td>
       </tr>
        </table> 
     
         <br />
         <center>
         <asp:Button ID="btnUpdate" runat="server" Text="Update MRCF Status" Width="141px" 
                 onclick="btnUpdate_Click"  />   
                 <asp:Button ID="btnReAssign" runat="server" Text="Reassign MRCF" 
                 Width="120px" onclick="btnReAssign_Click" 
                  />
     </center>
             </div>


 <table width="100%" cellpadding="0" cellspacing="0">
  </tr>    

 
 
     <tr>
         <td>
             <div class="border" 
                 style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
                 &nbsp;<b><span class="HeaderText">View MRCF</span></b>
                 <br />
                 <br />
                 <table cellpadding="3" class="grid" width="100%">
                     <tr>
                         <td class="GridRows">
                             MRCF Code:</td>
                         <td class="GridRows">
                             <asp:HiddenField ID="hdnStatus" runat="server" />
                             <asp:TextBox ID="txtMRCFCode" runat="server" CssClass="controls" 
                                 ReadOnly="true" Width="120px"></asp:TextBox>
                             &nbsp; Request Date: &nbsp;
                             <asp:TextBox ID="txtDateReq" runat="server" CssClass="controls" ReadOnly="true" 
                                 Width="120px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Requestor:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtRequestorName" runat="server" CssClass="controls" 
                                 ReadOnly="true" Width="200px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Charge To:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtChargeTo" runat="server" CssClass="controls" 
                                 ReadOnly="true" Width="400px"></asp:TextBox>
                             <asp:HiddenField ID="hdnChargeTo" runat="server" />
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Request Status:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtStat" runat="server" CssClass="controls" ReadOnly="true" 
                                 Width="200px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Intended For:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtIntended" runat="server" BackColor="white" 
                                 CssClass="controls" MaxLength="100" Width="98%" Enabled="False"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="vldIntended" runat="server" 
                                 ControlToValidate="txtIntended" Display="Dynamic" 
                                 ErrorMessage="[Intended Required]" ValidationGroup="request"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Request Type:</td>
                         <td class="GridRows">
                             <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" 
                                 BackColor="white" CssClass="controls" Enabled="False">
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Group Head:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtGrpHeadName" runat="server" CssClass="controls" 
                                 ReadOnly="true" Width="200px"></asp:TextBox>
                             <asp:HiddenField ID="hdnGrpHeadCode" runat="server" />
                             <asp:HiddenField ID="hdnGrpHeadMail" runat="server" />
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows" valign="top">
                             Remarks:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtGrpHeadRem" runat="server" CssClass="controls" 
                                 ReadOnly="true" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Division Head:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtDiviHeadName" runat="server" CssClass="controls" 
                                 ReadOnly="true" Width="200px"></asp:TextBox>
                             <asp:HiddenField ID="hdnDiviHeadCode" runat="server" />
                             <asp:HiddenField ID="hdnDiviHeadMail" runat="server" />
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows" valign="top">
                             Remarks:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtDiviHeadRem" runat="server" CssClass="controls" 
                                 ReadOnly="true" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows">
                             Procurement Mngr:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtProcMngrName" runat="server" CssClass="controls" 
                                 ReadOnly="true" Width="200px"></asp:TextBox>
                             <asp:HiddenField ID="hdnProcMngrCode" runat="server" />
                             <asp:HiddenField ID="hdnProcMngrMail" runat="server" />
                         </td>
                     </tr>
                     <tr>
                         <td class="GridRows" valign="top">
                             Remarks:</td>
                         <td class="GridRows">
                             <asp:TextBox ID="txtProcMngrRem" runat="server" CssClass="controls" 
                                 ReadOnly="true" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox>
                         </td>
                     </tr>
                 </table>
             </div>
                   
         </td>
     </tr>
      </table>


    <div class="border" 
                 style="padding-left:10px;padding-right:10px;padding-bottom:10px;">
      <table width="100%" cellpadding="0" class="grid">
  
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
	         <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" >
	          <Columns>          
	           <asp:TemplateColumn HeaderText="Items Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%" ItemStyle-Width="75%">
	            <ItemTemplate>	         
	             <table width="98%">
	              <tr><td><asp:TextBox runat="server" ID="txtItemDesc" CssClass="controls" 
                          Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' BackColor="white" 
                          Width="99%" Enabled="False"></asp:TextBox></td></tr>
                  <tr><td>Line Type : <asp:Label runat="server" ID="lblLineType" Text='<%#DataBinder.Eval(Container.DataItem, "LineType")%>'></asp:Label><asp:HiddenField runat="server" ID="hdnLineType" Value='<%#DataBinder.Eval(Container.DataItem, "LineType")%>' /></td></tr>
	              <tr>
                   <td>Transaction Type : <asp:Label runat="server" ID="lblTransactionType" Text='<%#DataBinder.Eval(Container.DataItem, "TransactionType")%>'></asp:Label><asp:HiddenField runat="server" ID="hdnTransactionType" Value='<%#DataBinder.Eval(Container.DataItem, "TransactionType")%>' />
                    <asp:Label runat="server" ID="lblItem" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "Item")%>'></asp:Label><asp:HiddenField runat="server" ID="hdnItemCode" Value='<%#DataBinder.Eval(Container.DataItem, "Item")%>' />                  
                   </td>
                  </tr>

                  <%#DataBinder.Eval(Container.DataItem, "Empname")%>
                  <%#DataBinder.Eval(Container.DataItem, "Birthdate")%>

                  <tr>
	               <td>
	                Qty: <asp:Label runat="server" ID="lblQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>'></asp:Label>
	                <asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "unit")%>'></asp:Label>
	               </td>
	              </tr>
	               <tr><td><asp:TextBox runat="server" ID="txtItemSpec" 
                           Text='<%#DataBinder.Eval(Container.DataItem, "itemspec")%>' CssClass="controls" 
                           BackColor="white" TextMode="MultiLine" Width="98%" Rows="4" Enabled="False"></asp:TextBox></td></tr>
	             </table>         
	            </ItemTemplate>

<HeaderStyle CssClass="GridColumns" Width="40%"></HeaderStyle>

<ItemStyle CssClass="GridRows" Width="75%"></ItemStyle>
	           </asp:TemplateColumn>     
	        
            <asp:TemplateColumn HeaderText="Date Needed">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="center" />
             <ItemTemplate>
              <cc1:GMDatePicker ID="dteDNeeded" runat="server" Enabled="False" CssClass="controls" BackColor="white" DisplayMode="Label" CalendarTheme="Blue" Date='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dateneed"))%>'></cc1:GMDatePicker>
             </ItemTemplate>
            </asp:TemplateColumn>
	                     
            <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>

<FooterStyle CssClass="GridColumns"></FooterStyle>

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" CssClass="GridRows" Width="10%"></ItemStyle>
            </asp:TemplateColumn>
           	           
           </Columns>

<HeaderStyle Font-Bold="True" Height="20px"></HeaderStyle>
          </asp:DataGrid> 
         </div>
        </td>
       </tr>
       <tr runat="server" id="trNoRequest" visible="false"><td style="font-size:small;" class="GridRows">&nbsp;There is no requested item.</td></tr>
      </table>
     </div>

 </ContentTemplate>
    
</asp:Content>