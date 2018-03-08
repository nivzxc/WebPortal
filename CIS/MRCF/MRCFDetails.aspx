<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MRCFDetails.aspx.cs" Inherits="CIS_MRCF_MRCFDetails" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntMRCFRequest" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel runat="server" ID="UpdatePanel3">
     <ContentTemplate>
 <table width="100%" cellpadding="0" cellspacing="0">
<%-- 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="MRCFMenu.aspx" class="SiteMap">MRCF</a> » 
     <a href="MRCFDetails.aspx?mrcfcode=<% Response.Write(Request.QueryString["mrcfcode"]); %>" class="SiteMap">MRCF Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText">View MRCF</span></b>
     <br />
         
     <div runat="server" id="divError" visible="false"> 
      <div class="ErrMsg">
       <b>Error during update!</b><br /><br />
       <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
      <br />
     </div>
     

     <div style="text-align:center;" id="divButtons2" runat="server">      
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnReset2" ImageUrl="~/Support/btnReset.jpg" ValidationGroup="request" OnClick="btnReset_Click" />--%>
      &nbsp;      
      <%--<asp:ImageButton runat="server" ID="btnVoid2" ImageUrl="~/Support/btnVoid.jpg" ValidationGroup="request" OnClick="btnVoid_Click" />--%>                              
      <br />
      <br />
     </div>
          
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
       <%--<tr>
        <td colspan="4" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>MRCF Details</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows">MRCF Code:</td>
        <td class="GridRows">
         <asp:HiddenField runat="server" ID="hdnStatus" />
         <asp:TextBox runat="server" ID="txtMRCFCode" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Request Date:
         &nbsp;
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows">
        <asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Charge To:</td>
        <td class="GridRows">         
         <asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="400px" ReadOnly="true"></asp:TextBox>         
         <asp:HiddenField ID="hdnChargeTo" runat="server" />
        </td>
       </tr>  
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>
       </tr>         
       <tr>
        <td class="GridRows">Intended For:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtIntended" CssClass="controls" Width="98%" MaxLength="100" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="vldIntended" ControlToValidate="txtIntended" ErrorMessage="[Intended Required]" Display="Dynamic" ValidationGroup="request"></asp:RequiredFieldValidator>
        </td>
       </tr> 
       <tr> 
        <td class="GridRows">Request Type:</td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlType" CssClass="controls" BackColor="white" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList></td>
       </tr>
       <tr>
        <td class="GridRows">Group Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtGrpHeadName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>         
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
         <asp:TextBox runat="server" ID="txtDiviHeadName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>         
         <asp:HiddenField runat="server" ID="hdnDiviHeadCode" />
         <asp:HiddenField runat="server" ID="hdnDiviHeadMail" />
        </td>
       </tr>         
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDiviHeadRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Procurement Mngr:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtProcMngrName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnProcMngrCode" />
         <asp:HiddenField runat="server" ID="hdnProcMngrMail" />
        </td>
       </tr>            
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtProcMngrRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>                            
      </table>       
     </div>          

     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="grid">
  
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
	         <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand">
	          <Columns>          
	           <asp:TemplateColumn HeaderText="Items Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%" ItemStyle-Width="75%">
	            <ItemTemplate>	         
	             <table width="98%">
	              <tr><td><asp:TextBox runat="server" ID="txtItemDesc" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' BackColor="white" Width="99%"></asp:TextBox></td></tr>
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
	               <tr><td><asp:TextBox runat="server" ID="txtItemSpec" Text='<%#DataBinder.Eval(Container.DataItem, "itemspec")%>' CssClass="controls" BackColor="white" TextMode="MultiLine" Width="98%" Rows="4"></asp:TextBox></td></tr>
	             </table>         
	            </ItemTemplate>
	           </asp:TemplateColumn>     
	        
            <asp:TemplateColumn HeaderText="Date Needed">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="center" />
             <ItemTemplate>
              <cc1:GMDatePicker ID="dteDNeeded" runat="server" CssClass="controls" BackColor="white" DisplayMode="Label" CalendarTheme="Blue" Date='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dateneed"))%>'></cc1:GMDatePicker>
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
     
     <div style="text-align:center;" id="divButtons" runat="server">
      <br />
         <asp:Button ID="btnSavePost" runat="server" Text="Submit"  ValidationGroup="request" OnClick="btnSavePost_Click" />
      &nbsp;
        <asp:Button ID="btnReset" runat="server" Text="Reset"  ValidationGroup="request" OnClick="btnReset_Click" />
      &nbsp;      
       <asp:Button ID="btnVoid" runat="server" Text="Void"  ValidationGroup="request" OnClick="btnVoid_Click"/>
     </div>
    </div> 
   </td>
  </tr>    

  <tr><td style="height:9px;"></td></tr>
  
  <tr runat="server" id="pnlAddItem">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">                            
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="Grid">
       <tr><td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Add Item to Request List</b></td></tr>
       <tr>
        <td class="GridRows" style="width: 25%">Type:</td>
        <td class="GridRows" style="width: 75%">

          <asp:DropDownList runat="server" ID="ddlLineType" CssClass="controls" 
          BackColor="white" DataTextField="pText" DataValueField="pValue" 
                onselectedindexchanged="ddlLineType_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList>

        </td>
       </tr>
       <tr runat="server" id="trCategory" visible="true">
        <td class="GridRows">Category:</td>
        <td class="GridRows">

          <asp:DropDownList runat="server" ID="ddlTransactionType" CssClass="controls" 
            BackColor="white" DataTextField="pText" DataValueField="pValue" 
            AutoPostBack="true" 
                onselectedindexchanged="ddlTransactionType_SelectedIndexChanged" ></asp:DropDownList>

        </td>
       </tr>
       <tr  runat="server" id="trItemsCategory" visible="false">
               <td class="GridRows">Item Category:</td>
               <td class="GridRows">
                <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="controls" 
                       AutoPostBack="true" BackColor="white" 
                       OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged">
                </asp:DropDownList>               
               </td>
        </tr>
       <tr runat="server" id="trItems" visible="true">
        <td class="GridRows">Item:</td>
        <td class="GridRows">

           <asp:DropDownList runat="server" ID="ddlItem" CssClass="controls" 
             BackColor="white" DataTextField="pText" DataValueField="pValue" 
                onselectedindexchanged="ddlItem_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
           

        </td>
       </tr>

       <tr ID="trItemDescription" runat="server" visible="true">
        <td class="GridRows" style="width:25%;">Description:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtItem" CssClass="controls" Width="98%" MaxLength="100" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItem" 
                ID="vldItem" ErrorMessage="<br>[Item Description Required]" Display="Dynamic" 
                ValidationGroup="additem" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr ID="trItemUnit" runat="server" visible="true">

        <td class="GridRows">Item Unit:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlUnit" runat="server" BackColor="white" 
                CssClass="controls" DataTextField="pText" DataValueField="pValue">
            </asp:DropDownList>
        </td>
       </tr>
       <tr>

        <td class="GridRows">Quantity:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtQty" CssClass="controls" MaxLength="6" Width="60px" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:CompareValidator ID="cmpQty" runat="server" ControlToValidate="txtQty" 
                Type="Integer" errormessage="<br>[Invalid Quantity]" Display="Dynamic" 
                Operator="DataTypeCheck" ValidationGroup="additem" ForeColor="Red"></asp:CompareValidator>
         <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" 
                MinimumValue="1" MaximumValue="999999" ControlToValidate="txtQty"  Display="Dynamic" 
                ErrorMessage="<br>[Value must be a whole number greater than zero]" ValidationGroup="additem" ForeColor="Red"></asp:RangeValidator>
         <asp:RequiredFieldValidator runat="server" ID="vldQty2" ControlToValidate="txtQty" 
                ErrorMessage="<br>[Quantity Required]" Display="dynamic" 
                ValidationGroup="additem" ForeColor="Red"></asp:RequiredFieldValidator>                  
         &nbsp;
         Date Needed:
         &nbsp;
         <cc1:GMDatePicker ID="dteDateNeeded" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker>
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Specification:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSpec" TextMode="MultiLine" CssClass="controls" Rows="10" Width="98%" MaxLength="200" BackColor="white" ValidationGroup="additem"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="vldSpec" 
                ControlToValidate="txtSpec" ErrorMessage="<br>[Specification Required]" 
                Display="Dynamic" ValidationGroup="additem" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
      </table>
     </div>
     <br />
     <br />
     <div style="text-align:center;">
         <asp:Button ID="btnSaveAdd" runat="server" Text="Add New Item"  ValidationGroup="additem" 
             onclick="btnSaveAdd_Click" /></div>
    </div>          
   </td>
  </tr>
 
 </table>
 </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>