<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="TransactionEdit.aspx.cs" Inherits="RewardPoint_TransactionEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
  <%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <asp:ScriptManager id="sm" runat="server"></asp:ScriptManager> 
 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--<tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="TransactionMain.aspx" class="SiteMap">Reward Transaction Main</a> » 
     <a href="TransactionAdd.aspx" class="SiteMap">New Transaction</a>
    </div>        
   </td>
  </tr>
  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Edit Reward Transaction</span></b> 
     <br />
     <div runat="server" id="divError" visible="false"> 
      <br />
      <div class="ErrMsg">      
       <b>Error during update. Please correct your data entries:</b>
       <ul><asp:Label runat="server" ID="lblErrMsg"></asp:Label></ul>
      </div>
     </div>
 
 <br />
        <asp:HiddenField ID="hdnTransactionCode" runat="server" Visible="False" />
 <br />
     <div class="GridBorder">  
      <asp:UpdatePanel ID="upApprover" runat="server">
       <ContentTemplate>
        <table width="100%" cellpadding="3" class="Grid">
         <%--<tr>
          <td colspan="2" class="GridText">
           <table>
            <tr>
             <td>&nbsp;<img src="../Support/viewtext22.png" alt="" /></td>
             <td>&nbsp;<b>Transaction Details</b></td>
            </tr>
           </table>         
          </td>
         </tr>   --%>        
         <tr>
          <td class="GridRows" style="width:30%">Category Name:</td>
          <td class="GridRows" style="width:70%">
              <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="true" 
                  BackColor="white" CssClass="controls" 
                  >
              </asp:DropDownList>
          </td>
         </tr>
         <tr>
          <td class="GridRows">Description:</td>
          <td class="GridRows">
           <asp:TextBox runat="server" ID="txtDescription" CssClass="controls" Width="98%" 
                  MaxLength="255" BackColor="white"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server" ID="reqRemarks" 
                  ErrorMessage="Description is required" 
                  Display="Dynamic" ControlToValidate="txtDescription" ValidationGroup="Save" 
                  ForeColor="Red"></asp:RequiredFieldValidator>
          </td>
         </tr> 
        </table>
       </ContentTemplate>
      </asp:UpdatePanel>
     </div>     
 
     <br /> 

     <asp:UpdatePanel ID="upItems" runat="server"><ContentTemplate>     
      <div class="GridBorder">
       <table width="100%" cellpadding="0" class="grid">
        <tr>
         <td colspan="4" class="GridColumns"><b>Employee List</b>
          <%--<table>
           <tr>
            <td>&nbsp;<img src="../Support/cart32.png" alt="" /></td>
            <td>&nbsp;</td>
           </tr>
          </table>     --%>    
         </td>
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
	            <asp:TemplateColumn HeaderText="Name" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30%" >
	             <ItemTemplate>
               <asp:Label runat="server" ID="lblName" Text='<%#DataBinder.Eval(Container.DataItem, "Username")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn>

                <asp:TemplateColumn HeaderText="Points" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="15%">
	             <ItemTemplate>
	              <asp:Label runat="server" ID="lblPoints" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Points")).ToString("#####0.00")%>'></asp:Label>&nbsp;
	             </ItemTemplate>
	            </asp:TemplateColumn>

	            <asp:TemplateColumn HeaderText="Type" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
	             <ItemTemplate>
	              &nbsp;<asp:Label runat="server" ID="lblType" Text='<%#DataBinder.Eval(Container.DataItem, "IsIncrease")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn>
	           
               <asp:TemplateColumn HeaderText="Acquired Date" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="25%">
	             <ItemTemplate>
               <asp:Label runat="server" ID="lblDate" Text='<%#DataBinder.Eval(Container.DataItem, "DateAcquired")%>'></asp:Label>
	             </ItemTemplate>
	            </asp:TemplateColumn>
	           
             <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="10%">
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
      <table width="100%" cellpadding="0" class="grid">
       <tr>
        <td class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>
            <table>
             <tr>
              <td>&nbsp;<img src="../Support/Box22.png" alt="" /></td>
              <td>&nbsp;<b>Add Employee Points</b></td>              
             </tr>
            </table>            
           </td>
           <td style="text-align:right;">
               <asp:Button ID="btnAddNewItem" runat="server" Text="Add New Item"  OnClick="btnAddNewItem_Click" ValidationGroup="addnew"/><%--<asp:ImageButton runat="server" ID="btnAddNewItem" ImageUrl="~/Support/btnAddItem.jpg" OnClick="btnAddNewItem_Click" ValidationGroup="addnew" />--%>&nbsp;</td>
          </tr>
         </table>         
        </td>
       </tr>
       <tr>
        <td>                 
         <div class="GridBorder">          
          <table width="100%" cellpadding="2" class="grid">  
           <tr>
            <td class="GridColumns" style="width:50%"><b>Employee Name</b></td>
            <td class="GridColumns" style="width:20%"><b>Points</b></td>
            <td class="GridColumns" style="width:10%"><b>Type</b></td>
            <td class="GridColumns" style="width:20%"><b>Date Acquired</b></td>
           </tr>         
           <tr>           
            <td class="GridRows">
                <asp:DropDownList ID="ddlUsername" runat="server" AutoPostBack="true" 
                    BackColor="white" CssClass="controls" 
                    >
                </asp:DropDownList>
               </td>
            <td class="GridRows" style="text-align:center;">
                <asp:TextBox ID="txtPoints" runat="server" BackColor="white" 
                    CssClass="controls" MaxLength="5" Width="98%"></asp:TextBox><asp:CompareValidator runat="server" ID="cmpQty1" 
                    ErrorMessage="<b>Invalid Entry</b><br><br>Numeric values are only allowed." 
                    ControlToValidate="txtPoints" Operator="DataTypeCheck" Display="Dynamic" 
                    ValidationGroup="addnew" Type="Integer" ForeColor="Red"></asp:CompareValidator>
             <asp:RangeValidator ID="rngQty1" runat="server" ControlToValidate="txtPoints" 
                    ErrorMessage="<br>[Value must be 4 digit]" MaximumValue="9999" MinimumValue="1" 
                    Display="Dynamic" ValidationGroup="addnew" ForeColor="Red"></asp:RangeValidator>
             <asp:RequiredFieldValidator ID="reqQty1" runat="server" 
                    ControlToValidate="txtPoints" ErrorMessage="<br>[Required]" 
                    ValidationGroup="addnew" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
               </td>
            <td class="GridRows" style="text-align:center;">
                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" 
                    BackColor="white" CssClass="controls" 
                    >
                    <asp:ListItem>Add</asp:ListItem>
                    <asp:ListItem>Deduct</asp:ListItem>
                </asp:DropDownList>
               </td>
            <td class="GridRows" style="text-align:center;">
                <cc1:GMDatePicker ID="dtpDateAcquired" runat="server" CssClass="controls" 
                    DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" 
                    CalendarTheme="Blue"></cc1:GMDatePicker></td>
           </tr>                                                                                                                         
          </table>          
         </div>         
	       </td>
	      </tr>
	     </table>
	     </ContentTemplate></asp:UpdatePanel>
	    </div>
     </ContentTemplate></asp:UpdatePanel>
     
     <br />     
     
     <div style="text-align:center;width:100%">
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSend.jpg" 
             ValidationGroup="Save" OnClick="btnSave_Click"/>--%>
             <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Save" OnClick="btnSave_Click"/>
             &nbsp;
             <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
             <%--<asp:ImageButton 
             runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" 
             onclick="btnBack_Click"/>--%>
     </div>        
      
    </div>      
   </td>
  </tr>

 </table>
</asp:Content>

