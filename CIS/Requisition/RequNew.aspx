<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="RequNew.aspx.cs" Inherits="CIS_Requisition_RequNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
  
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">       
 <asp:ScriptManager id="sm" runat="server"></asp:ScriptManager> 

                <script language="javascript" type="text/javascript">
                    var submit = 0;

                    function CheckIsRepeat() {
                        if (++submit > 1) {
                            alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                            return false;
                        }
                    }
    </script>

 <table width="100%" cellpadding="0" cellspacing="0" style="table-layout:fixed;"> 
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../EFormsMain.aspx" class="SiteMap">CIS</a> » 
     <a href="RequMenu.aspx" class="SiteMap">Requisition</a> » 
     <a href="RequNew.aspx" class="SiteMap">Create New Requisition</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 --%>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Create New Requisition</span></b> 
     <br />
     <div runat="server" id="divError" visible="false"> 
      <br />
      <div class="ErrMsg">      
       <b>Error during update. Please correct your data entries:</b>
       <br/><asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
     </div>
     <br />   

     <div class="GridBorder">  
      <asp:UpdatePanel ID="upApprover" runat="server">
       <ContentTemplate>
        <table width="100%" cellpadding="3" class="Grid">
        <%-- <tr>
          <td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../../Support/viewtext22.png" alt="" /></td>
             <td>&nbsp;<b>Requisition Details</b></td>
            </tr>
           </table>         
          </td>
         </tr>    --%>       
         <tr>
          <td class="GridRows">Requestor:</td>
          <td class="GridRows">
           <asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
           <asp:HiddenField ID="hdnRCCode" runat="server" />
          </td>
         </tr>
         <tr>
          <td class="GridRows">Intended For:</td>
          <td class="GridRows">
           <asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" Width="98%" MaxLength="255" BackColor="white"></asp:TextBox>
           <asp:RequiredFieldValidator runat="server" ID="reqRemarks" ErrorMessage="<b>Required Field Missing</b><br><br><u>Intended For</u> is required" Display="None" ControlToValidate="txtRemarks" ValidationGroup="requisition"></asp:RequiredFieldValidator>
           <ajax:ValidatorCalloutExtender ID="vceRemarks" runat="server" TargetControlID="reqRemarks" HighlightCssClass="controlsInvalid"></ajax:ValidatorCalloutExtender>
          </td>
         </tr> 
         <tr>
          <td class="GridRows">Charge To:</td>
          <td class="GridRows"><asp:DropDownList runat="server" ID="ddlChargeTo" CssClass="controls" AutoPostBack="true" BackColor="white" OnSelectedIndexChanged="ddlChargeTo_SelectedIndexChanged"></asp:DropDownList></td>
         </tr>       
         <tr>
          <td class="GridRows">Department Approver:</td>
          <td class="GridRows"><asp:DropDownList runat="server" ID="ddlGrpHead" CssClass="controls" BackColor="white"></asp:DropDownList></td>
         </tr>
         <%--<tr>
          <td class="GridRows">Division Head:</td>
          <td class="GridRows">
           <asp:TextBox runat="server" ID="txtDiviHeadName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
           <asp:HiddenField runat="server" ID="hdnDiviHeadCode" />
           <asp:HiddenField runat="server" ID="hdnDiviHeadMail" />
          </td>
         </tr>  --%> 
         <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" 
                BackColor="white" AutoPostBack="True" 
                onselectedindexchanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
         <asp:HiddenField runat="server" ID="hdnDiviHeadMail" />
        </td>
       </tr>      
         <tr>
          <td class="GridRows">Supplies Custodian:</td>
          <td class="GridRows">
           <asp:TextBox runat="server" ID="txtSuppName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
           <asp:HiddenField runat="server" ID="hdnSuppMail" />
          </td>
         </tr>
        </table>
       </ContentTemplate>
      </asp:UpdatePanel>
     </div>     
 
     <br /> 

     <asp:UpdateProgress ID="uppItems" runat="server" DynamicLayout="true">
      <ProgressTemplate>
       <img src="../../Support/msgUpdateProgress.jpg" alt="" />
      </ProgressTemplate>
     </asp:UpdateProgress>     
     <asp:UpdatePanel ID="upItems" runat="server"><ContentTemplate>     
      <div class="GridBorder">
       <table width="100%" cellpadding="0" class="grid">
        <%--<tr>
         <td colspan="4" class="GridText">
          <table>
           
          </table>         
         </td>
        </tr>--%>
        <tr>
<%--            <td>&nbsp;<img src="../../Support/cart32.png" alt="" /></td>--%>
            <td  colspan="4" class="GridColumns">&nbsp;<b>My Requested Items</b></td>
           </tr>
        <%--<tr>
         <td colspan="4">
	         <div class="GridBorder">
	          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand">
	           <Columns>
	            <asp:TemplateColumn HeaderText="Items Details and Purpose" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%">
	             <ItemTemplate>
	              <asp:HiddenField runat="server" ID="hdnItemCode" Value='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
	              &nbsp;<asp:Label runat="server" ID="lblItemDesc" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' Font-Underline="true"></asp:Label>
	              <br />
	               &nbsp;Purpose:<asp:Label runat="server" ID="lblReason" Text='<%#DataBinder.Eval(Container.DataItem, "reason")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn> 

	            <asp:TemplateColumn HeaderText="Qty" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
	             <ItemTemplate>
               <asp:Label runat="server" ID="lblQty" Text='<%#DataBinder.Eval(Container.DataItem, "itemqty")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn>

	            <asp:TemplateColumn HeaderText="Unit" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
	             <ItemTemplate>
	              &nbsp;<asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "itemunit")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn>
	           
	            <asp:TemplateColumn HeaderText="Price" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Right">
	             <ItemTemplate>
	              <asp:Label runat="server" ID="lblPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemprice")).ToString("#####0.00")%>'></asp:Label>&nbsp;
	             </ItemTemplate>
	            </asp:TemplateColumn>

	            <asp:TemplateColumn HeaderText="Total" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Right">
	             <ItemTemplate>
	              <asp:Label runat="server" ID="lblTPrice" Text='<%#Convert.ToDouble(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemqty")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemprice"))).ToString("#######0.00") %>'></asp:Label>&nbsp;
	             </ItemTemplate>           
	           </asp:TemplateColumn>
	           
             <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
              <ItemTemplate>
               <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
              </ItemTemplate>
             </asp:TemplateColumn>
           	           
	          </Columns>
	         </asp:DataGrid>
         </div>	 
        </td>
       </tr>     --%> 
       <tr>
         <td colspan="4">
	         <div class="GridBorder">
	          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand">
	           <Columns>
	            <asp:TemplateColumn HeaderText="Items Details and Purpose" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%">
	             <ItemTemplate>
	              <asp:HiddenField runat="server" ID="hdnItemCode" Value='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
	              &nbsp;<asp:Label runat="server" ID="lblItemDesc" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' Font-Underline="true"></asp:Label>
	              <br />
	               &nbsp;Purpose:<asp:Label runat="server" ID="lblReason" Text='<%#DataBinder.Eval(Container.DataItem, "reason")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn> 

	            <asp:TemplateColumn HeaderText="Qty" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
	             <ItemTemplate>
               <asp:Label runat="server" ID="lblQty" Text='<%#DataBinder.Eval(Container.DataItem, "itemqty")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn>

	            <asp:TemplateColumn HeaderText="Unit" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
	             <ItemTemplate>
	              &nbsp;<asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "itemunit")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn>
	           
	            <asp:TemplateColumn HeaderText="Price" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Right">
	             <ItemTemplate>
	              <asp:Label runat="server" ID="lblPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemprice")).ToString("#####0.00")%>'></asp:Label>&nbsp;
	             </ItemTemplate>
	            </asp:TemplateColumn>

	            <asp:TemplateColumn HeaderText="Total" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Right">
	             <ItemTemplate>
	              <asp:Label runat="server" ID="lblTPrice" Text='<%#Convert.ToDouble(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemqty")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemprice"))).ToString("#######0.00") %>'></asp:Label>&nbsp;
	             </ItemTemplate>           
	           </asp:TemplateColumn>
	           
             <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
              <ItemTemplate>
               <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
              </ItemTemplate>
             </asp:TemplateColumn>
           	           
	          </Columns>
	         </asp:DataGrid>
         </div>	 
        </td>
       </tr>      
      </table>
     </div>
     
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
<%--                <ajax:ValidatorCalloutExtender ID="vceReason1" runat="server" TargetControlID="reqReason1" HighlightCssClass="controlsInvalid" Width="250px"></ajax:ValidatorCalloutExtender>
--%>               </td>               
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
     
     <br />
     
     <div style="width:100%;text-align:center;">
      <table width="100%" cellpadding="0" cellspacing="0"  style="border-color: #FAFAFA;">
       <tr>
        <td valign="top"  style="border-color: #FAFAFA;">
         <table style="font-size:small;">
          <tr>
           <td style="width:300px">
            <div class="GridBorder">
             <table width="100%" cellpadding="3">
              <tr><td class="GridColumns" colspan="2" style="text-align:center;"><b>RC Budget Details</b></td></tr>
              <tr>
               <td class="GridRows" align="left">Current Budget:</td>
               <td class="GridRows" align="right">P&nbsp;<asp:label runat="server" id="lblCurBudget"></asp:label></td>
              </tr>
              <tr>
               <td class="GridRows" align="left">Total Item Price:</td>
               <td class="GridRows" align="right">P&nbsp;<asp:label runat="server" id="lblTotalCost"></asp:label></td>
              </tr>
              <tr>
               <td class="GridRows" align="left"><b>Remaining Budget:</b></td>
               <td class="GridRows" align="right"><b>P&nbsp;<asp:label runat="server" id="lblRemBudget"></asp:label></b></td>
              </tr>
             </table>
            </div>
           </td>
           <td style="text-align:center; width:200px">
            <table width="100%" style="border-color: #FAFAFA;">
             <tr><td align="center"  style="border-color: #FAFAFA;"><asp:Image runat="server" ID="imgMessage" ImageUrl="~/Support/Ok64.png" /></td></tr>
             <tr><td align="center" style="border-color: #FAFAFA;"><asp:label runat="server" id="lblMessage" ForeColor="red"></asp:label></td></tr>                 
            </table>
           </td>
          </tr>
         </table>             
        </td>
       </tr>
      </table>       
     </div> 
     </ContentTemplate></asp:UpdatePanel>
     
     <br />     
     
     <div style="text-align:center;width:100%">
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSend.jpg" ValidationGroup="requisition" OnClick="btnSave_Click"/>--%>
         <asp:Button ID="btnSave" runat="server" Text="Submit"  ValidationGroup="requisition" OnClick="btnSave_Click"/>
     </div>        
      
    </div>      
   </td>
  </tr>

 </table>
</asp:Content>