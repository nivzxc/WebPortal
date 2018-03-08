<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="RequDetails.aspx.cs" Inherits="CIS_Requisition_RequDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="cntRequRequest" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager id="sm" runat="server"></asp:ScriptManager> 
 <table width="100%" cellpadding="0" cellspacing="0">
<%--
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="RequMenu.aspx" class="SiteMap">Requisition</a> » 
     <a href="RequDetails.aspx?requcode=<%Response.Write(Request.QueryString["requcode"]); %>" class="SiteMap">Requisition Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">View Requisition</span></b>
     <br />         
   <div runat="server" id="divError" visible="false"> 
      <br />
      <div class="ErrMsg">      
       <b>Error during update. Please correct your data entries:</b>
       <br /><asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
     </div>
     <br />     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">   
      <%-- <tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>Requisition Details</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows">Requisition Code:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequCode" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>         
         &nbsp;Date Requested: 
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatus" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Intended For:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtIntended" CssClass="controls" Width="470px" BackColor="white"></asp:TextBox></td>
       </tr> 
       <tr>
        <td class="GridRows">Charge To:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="400px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnChargeTo" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Group Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtGrpHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnGrpHeadCode" />
         <asp:HiddenField runat="server" ID="hdnGrpHeadMail" />
        </td>
       </tr> 
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGrpHeadRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Division Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDiviHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnDiviHeadCode" />
         <asp:HiddenField runat="server" ID="hdnDiviHeadMail" />
        </td>
       </tr> 
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtHeadRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Supplies Custodian:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSuppName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnSuppCode" />
         <asp:HiddenField runat="server" ID="hdnSuppMail" />
        </td>
       </tr>
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtSuppRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>                            
      </table>       
     </div>
     
     <br />     
      <asp:UpdatePanel ID="upItems" runat="server"><ContentTemplate>     
     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="grid">
      <%-- <tr>
        <td colspan="2" class="GridColumns">
        <table>
         
        </table>
        </td>
       </tr>--%>
       <tr>
          <td colspan="2" class="GridColumns">&nbsp;<b>List of Requested Items</b></td>
       </tr>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">   
	         <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" FooterStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand" ShowFooter="false">
	          <Columns>
	           <asp:TemplateColumn HeaderText="Requested Items" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left" FooterStyle-CssClass="GridColumns" FooterStyle-Font-Bold="true">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnItemCode" Value='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
              &nbsp;<asp:Label runat="server" ID="lblItemDesc" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' Font-Underline="true"></asp:Label><br />
              &nbsp;Purpose: <asp:Label runat="server" ID="lblReason" Text='<%#DataBinder.Eval(Container.DataItem, "reason")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Qty" FooterStyle-CssClass="GridColumns" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:Label runat="server" ID="txtQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>' Width="40px" MaxLength="3" ValidationGroup="edititem" BackColor="white"></asp:Label>
<%--              <asp:CompareValidator runat="server" ID="cmpQty" ErrorMessage="*" ControlToValidate="txtQty" Operator="DataTypeCheck" Display="Dynamic" ValidationGroup="edititem" Type="Integer"></asp:CompareValidator>
              <asp:RangeValidator ID="rngQty" runat="server" ControlToValidate="txtQty" ErrorMessage="*" MaximumValue="9999" MinimumValue="1" Display="Dynamic" ValidationGroup="edititem"></asp:RangeValidator>
              <asp:RequiredFieldValidator ID="reqQty" runat="server" ControlToValidate="txtQty" ErrorMessage="*" ValidationGroup="edititem" Display="Dynamic"></asp:RequiredFieldValidator>
--%>             </ItemTemplate>
            </asp:TemplateColumn>
 
            <asp:TemplateColumn HeaderText="Unit" FooterStyle-CssClass="GridColumns" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              &nbsp;<asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "unit")%>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderText="Price" FooterStyle-CssClass="GridColumns" FooterStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("#####0.00")%>'></asp:Label>&nbsp;
             </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Total" FooterStyle-CssClass="GridColumns" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblTPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tprice")).ToString("#######0.00") %>'></asp:Label>&nbsp;
             </ItemTemplate>           
            </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderText="Issued" ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" FooterStyle-CssClass="GridColumns">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblIssued" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soqty")).ToString("#######0") %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateColumn>            
            			          
            <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="GridColumns">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>
            </asp:TemplateColumn>          	           
           </Columns>
          </asp:DataGrid>           
         </div>
         
         <div runat="server" id="divAddItem">
          <br />         
             <div class="GridBorder">
      <asp:UpdatePanel ID="upAddItem" runat="server"><ContentTemplate>
      <table width="100%" cellpadding="0" class="grid" style="table-layout:fixed;">
       <tr>
        <td class="">
         <table cellpadding="0" cellspacing="0" width="100%"  style="border-color: #FAFAFA;">
          <tr>
           <td style="text-align:right;border-color: #FAFAFA;">
               <asp:Button ID="btnAddNewItem" runat="server" Text="Add New Item"  OnClick="btnAddNewItem_Click" ValidationGroup="addnew" /><%--<asp:ImageButton runat="server" ID="btnAddNewItem" ImageUrl="~/Support/btnAddItem.jpg" OnClick="btnAddNewItem_Click" ValidationGroup="addnew" />--%>&nbsp;</td>
          </tr>
         </table>         
        </td>
       </tr>
       <tr>
        <td>                 
         <div class="GridBorder">          
          <table width="100%" cellpadding="2" class="grid" style="table-layout:fixed;">  
           <tr>
            <td class="GridColumns" style="width:80%"><b>Requested Item</b></td>
            <td class="GridColumns" style="width:10%"><b>Qty</b></td>
            <td class="GridColumns" style="width:10%"><b>Price</b></td>
           </tr>         
           <tr>           
            <td class="GridRows">
             <table style="width:100%; table-layout:fixed;">
              <tr>
               <td style="Width:22%">Item Category:</td>
               <td style="width:78%">
                <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="controls" 
                       AutoPostBack="true" BackColor="white" 
                       OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged">
                 <%--<asp:ListItem Text="All Items" Value="ALL"></asp:ListItem>
                 <asp:ListItem Text="Janitorial Supplies" Value="JS01"></asp:ListItem>
                 <asp:ListItem Text="Office Supplies" Value="OS01"></asp:ListItem>
                 <asp:ListItem Text="Pantry Supplies" Value="PS01"></asp:ListItem>
                 <asp:ListItem Text="Technical Supplies" Value="TS01"></asp:ListItem>--%>
                </asp:DropDownList>               
               </td>
              </tr>
             <%-- <tr>
               <td>Item Sub-Category:</td>
               <td>
                <asp:DropDownList ID="ddlItemSubCategory" runat="server" CssClass="controls" AutoPostBack="true" BackColor="white" OnSelectedIndexChanged="ddlClassItem_SelectedIndexChanged">
                </asp:DropDownList>               
               </td>
              </tr>--%>
              <tr>
               <td>Item:</td>
               <td><asp:DropDownList ID="ddlItem1" AutoPostBack="true" runat="server" CssClass="controls" OnSelectedIndexChanged="ddlItem1_SelectedIndexChanged" BackColor="white" Width="100%"></asp:DropDownList></td>
              </tr>
              <tr>
               <td>Purpose:</td>
               <td>
                <asp:TextBox ID="txtReason1" runat="server" CssClass="controls" Width="98%" ValidationGroup="addnew" MaxLength="100" BackColor="white"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="reqReason1" runat="server" 
                       ControlToValidate="txtReason1" 
                       ErrorMessage="Reason for requesting is required." 
                       ValidationGroup="addnew" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
               <%-- <ajax:ValidatorCalloutExtender ID="vceReason1" runat="server" TargetControlID="reqReason1" HighlightCssClass="controlsInvalid" Width="250px"></ajax:ValidatorCalloutExtender>--%>
               </td>               
              </tr>                      
             </table>
            </td>
            <td class="GridRows" style="text-align:center;">
             <asp:TextBox runat="server" ID="txtQty1" CssClass="controls" MaxLength="6" Width="40px" ValidationGroup="addnew" BackColor="white"></asp:TextBox>
             <asp:CompareValidator runat="server" ID="cmpQty1" 
                    ErrorMessage="<b>Invalid Entry</b><br><br>Numeric values are only allowed." 
                    ControlToValidate="txtQty1" Operator="DataTypeCheck" Display="Dynamic" 
                    ValidationGroup="addnew" Type="Integer" ForeColor="Red"></asp:CompareValidator>
             <asp:RangeValidator ID="rngQty1" runat="server" ControlToValidate="txtQty1" 
                    ErrorMessage="<br>[Invalid]" MaximumValue="9999" MinimumValue="1" 
                    Display="Dynamic" ValidationGroup="addnew" ForeColor="Red"></asp:RangeValidator>
             <asp:RequiredFieldValidator ID="reqQty1" runat="server" 
                    ControlToValidate="txtQty1" ErrorMessage="<br>[Required]" 
                    ValidationGroup="addnew" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td class="GridRows" style="text-align:center;">
             <asp:Label runat="server" ID="lblPriceNew"></asp:Label>
             per
             <asp:Label runat="server" ID="lblUnitNew"></asp:Label>
            </td>
           </tr>                                                                                                                         
          </table>          
         </div>         
	       </td>
	      </tr>
	     </table>
	     </ContentTemplate></asp:UpdatePanel>
	    </div>
     
         </div>         
        </td>
       </tr>
      </table>
     </div>          

     <div runat="server" id="divBudget">
      <br />
      <table width="100%" cellpadding="0" cellspacing="0">
       <tr>
        <td valign="top">
         <table style="font-size:small;">
          <tr>
           <td style="width:300px">
            <div class="GridBorder">
             <table width="100%" cellpadding="3">
              <tr><td class="GridColumns" colspan="2" style="text-align:center;"><b>Department's Budget Information</b></td></tr>
              <tr>      
               <td class="GridRows">Current Budget:</td>
               <td class="GridRows" align="right">P&nbsp;<asp:label runat="server" id="lblCurBudget"></asp:label></td>
              </tr>
              <tr>
               <td class="GridRows">Total Item Price:</td>
               <td class="GridRows" align="right">P&nbsp;<asp:label runat="server" id="lblTotalCost"></asp:label></td>
              </tr>
              <tr>
               <td class="GridRows"><b>Remaining Budget:</b></td>
               <td class="GridRows" align="right"><b>P&nbsp;<asp:label runat="server" id="lblRemBudget"></asp:label></b></td>
              </tr>
             </table>
            </div>
           </td>
           <td style="text-align:center; width:200px">
            <table width="100%">
             <tr><td align="center"><asp:Image runat="server" ID="imgMessage" ImageUrl="~/Support/Ok64.png" /></td></tr>
             <tr><td align="center"><img src='../../Support/smile22.png' alt='' />&nbsp;<asp:label runat="server" id="lblMessage" ForeColor="green" Text="You have sufficient budget for this request!"></asp:label></td></tr>
            </table>
           </td>
          </tr>
         </table>             
        </td>
       </tr>
      </table>     
     </div>
         </ContentTemplate>
     </asp:UpdatePanel>
     <div style="text-align:center;" id="divButtons" runat="server">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" ValidationGroup="request" OnClick="btnSend_Click" />--%>
     <asp:Button ID="btnSend" runat="server" Text="Submit"  ValidationGroup="request" OnClick="btnSend_Click" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Support/btnReset.jpg" ValidationGroup="request" OnClick="btnReset_Click" />--%>
      <asp:Button ID="btnReset" runat="server" Text="Reset"  ValidationGroup="request" OnClick="btnReset_Click" />
      &nbsp;        
      <%--<asp:ImageButton runat="server" ID="btnVoid" ImageUrl="~/Support/btnVoid.jpg" ValidationGroup="request" OnClick="btnVoid_Click" />   --%>  
         <asp:Button ID="btnVoid" runat="server" Text="Void" ValidationGroup="request" OnClick="btnVoid_Click" />
     </div>
         
    </div>
   </td>
  </tr>
          
 </table>
</asp:Content>