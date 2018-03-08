<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MRCFNew.aspx.cs" Inherits="CIS_MRCF_MRCFNew" %>

<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="cntMRCFNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

     <script language="javascript" type="text/javascript">
            var submit = 0;

            function CheckIsRepeat() {
                if (++submit > 1) {
                    alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                    return false;
                }
            }
    </script>

    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
       <ContentTemplate>
       <table width="100%" cellpadding="0" cellspacing="0"> 
 
     <script language="JavaScript" type="text/javascript">
         function winpop(url, w, h, scroll, resize, center) {
             if (center) {
                 var winPos = ',top=' + ((screen.height - h) / 2) + ',left=' + ((screen.width - w) / 2);
             }
             var scrollArg = (scroll == false) ? '' : ',scrollbars=1';
             var resizeArg = (resize == false) ? '' : ',resizable=1';
             flyout = window.open(url, "newin" + scroll + resize + center, "width=" + w + ",height=" + h + scrollArg + resizeArg + winPos);
             flyout.resizeTo(w, h);
             flyout.focus();
         }
    </script>

  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New MRCF</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="Grid">
     <%--  <tr><td class="GridColumns" style="text-align:left; height:20px;" colspan="2">&nbsp;<b>MRCF Details</b></td></tr>--%>
       <tr>
        <td class="GridRows" style="width: 25%">Requestor:</td>
        <td class="GridRows" style="width: 75%"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Intended For:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtIntended" CssClass="controls" Width="99%" MaxLength="100" BackColor="white" ValidationGroup="save"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqIntended" 
                ErrorMessage="<br>[Intended For required]" Display="Dynamic" 
                ControlToValidate="txtIntended" ValidationGroup="save" SetFocusOnError="true" 
                ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Charge To:</td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlChargeTo" CssClass="controls" AutoPostBack="true" BackColor="white" OnSelectedIndexChanged="ddlChargeTo_SelectedIndexChanged"></asp:DropDownList></td>
       </tr>       
       <tr>
        <td class="GridRows">Request Type:</td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlType" CssClass="controls" BackColor="white" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
       </tr>
       <tr>
        <td class="GridRows">Department Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlGrpHead" CssClass="controls" 
                BackColor="white" onselectedindexchanged="ddlGrpHead_SelectedIndexChanged"></asp:DropDownList>
        </td>
       </tr>
       <%--<tr>
        <td class="GridRows">Division Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDiviHeadName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnDiviHeadCode" />
         <asp:HiddenField runat="server" ID="hdnDiviHeadMail" />
        </td>
       </tr>   --%>    
       <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" BackColor="white"></asp:DropDownList>
         <asp:HiddenField runat="server" ID="hdnDiviHeadMail" />
        </td>
       </tr>   
       <tr>
        <td class="GridRows">Procurement:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtProcMngrName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnProcMngrMail" />
        </td>
       </tr>
      </table>
     </div>
     <br />
        <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">                            
     <div class="GridBorder">
     
     <table width="100%" cellpadding="3" class="Grid" id="tblItem" runat="server">
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
        <tr  runat="server" id="trAirfare" visible="false">
            <td class="GridRows">Airlines Reference:</td>
            <td class="GridRowsAirlines" runat="server" ID="tAirlines"></td>
        </tr>

        <tr  runat="server" id="trEmployee" visible="false" style="vertical-align:middle">
          <td class="GridRows" style="vertical-align:middle">Employee Name:</td>
          <td class="GridRows">
                <asp:DropDownList ID="ddlEmployees" runat="server" CssClass="controls" 
                       AutoPostBack="true" BackColor="white" 
                    onselectedindexchanged="ddlEmployees_SelectedIndexChanged">
                </asp:DropDownList>  
                <asp:TextBox runat="server" ID="txtNameOthers" CssClass="controls" MaxLength="120" Width="240px" BackColor="white"></asp:TextBox>             
                <asp:CheckBox ID="ckbOthers" runat="server" 
                    oncheckedchanged="ckbOthers_CheckedChanged" Text="Others" 
                    AutoPostBack="True" />
               </td>
       </tr>
        <tr  runat="server" id="trBirthdate" visible="false" style="vertical-align:middle">
          <td class="GridRows" style="vertical-align:middle">Employee Birthdate:</td>
          <td class="GridRows"> 

              <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                  BackColor="white" CssClass="controls" 
                  onselectedindexchanged="ddlYear_SelectedIndexChanged" ></asp:DropDownList>
                  &nbsp;
              <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" 
                  BackColor="white" CssClass="controls" 
                  onselectedindexchanged="ddlMonth_SelectedIndexChanged" ></asp:DropDownList> &nbsp;
              <asp:DropDownList ID="ddlDays" runat="server" AutoPostBack="True" 
                  BackColor="white" CssClass="controls" 
                  onselectedindexchanged="ddlDays_SelectedIndexChanged" > </asp:DropDownList>
              
                         
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
        <td class="GridRows" style="width:25%; height: 9px;">Description:</td>
        <td class="GridRows" style="height: 9px">
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
     <div style="text-align:center;">
         <asp:Button ID="btnSaveAdd" runat="server" Text="Add New Item"  OnClick="btnSaveAdd_Click" ValidationGroup="additem"/><%--<asp:ImageButton runat="server" ID="btnSaveAdd" ImageUrl="~/Support/btnAddItem.jpg" OnClick="btnSaveAdd_Click" ValidationGroup="additem" />--%></div>
    </div>    
     <br />

     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="Grid">
       <tr><td class="GridColumns" style="text-align:left;" colspan="2">&nbsp;<b>Requested Items</b></td></tr>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand">
	          <Columns>          
	           <asp:TemplateColumn HeaderText="Items Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%" ItemStyle-Width="75%">
	              
	            <ItemTemplate>	         
	             <table width="98%">
	              <tr><td><asp:TextBox runat="server" ID="txtItemDesc" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' BackColor="white" Width="99%" ReadOnly="True"></asp:TextBox></td></tr>
                  <tr><td>Line Type : <asp:Label runat="server" ID="lblLineType" Text='<%#DataBinder.Eval(Container.DataItem, "LineTypeDesc")%>'></asp:Label><asp:HiddenField runat="server" ID="hdnLineType" Value='<%#DataBinder.Eval(Container.DataItem, "LineType")%>' /></td></tr>
	                               
                  </tr>
                  <tr>
                   <td>Transaction Type : <asp:Label runat="server" 
                           ID="lblTransactionType" 
                           Text='<%# DataBinder.Eval(Container.DataItem, "TransactionDesc") %>' 
                           Visible="true"></asp:Label><asp:HiddenField runat="server" ID="hdnTransactionType" Value='<%#DataBinder.Eval(Container.DataItem, "TransactionType")%>' />
                    <asp:Label runat="server" ID="lblItem" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "Item")%>'></asp:Label><asp:HiddenField runat="server" ID="hdnItemCode" Value='<%#DataBinder.Eval(Container.DataItem, "Item")%>' />                  
                   </td>
                  </tr>
                 
                  <%#DataBinder.Eval(Container.DataItem, "EmpnameView") %>
                  <%#DataBinder.Eval(Container.DataItem, "BirthDateView")%>
           
                  <tr>
	               <td>
	                Qty : <asp:Label runat="server" ID="lblQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>'></asp:Label>
	                <asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "unit")%>'></asp:Label>
	               </td>
	              </tr>
	              <tr><td><asp:TextBox runat="server" ID="txtSpecification" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "itemspec")%>' TextMode="MultiLine" Rows="4" BackColor="white" Width="99%" ReadOnly="True"></asp:TextBox></td></tr>
	             </table>         
	            </ItemTemplate>
	               <HeaderStyle CssClass="GridColumns" Width="40%" />
                   <ItemStyle CssClass="GridRows" Width="75%" />
	           </asp:TemplateColumn>     
	        
            <asp:TemplateColumn HeaderText="Date Needed" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%">
             <ItemTemplate>
	             &nbsp;<asp:label runat="server" ID="lblDateNeeded" 
                     Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dateneed")).ToString("MM/dd/yyyy")%>' 
                     ReadOnly="True" Width="70px"></asp:label>
	            </ItemTemplate>
                <ItemStyle CssClass="GridRows" HorizontalAlign="Center" VerticalAlign="Top" 
                    Width="15%" />
	            <HeaderStyle CssClass="GridColumns" />
   
	           </asp:TemplateColumn>
	                     
            <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>
                <FooterStyle CssClass="GridColumns" />
                <HeaderStyle CssClass="GridColumns" />
                <ItemStyle CssClass="GridRows" HorizontalAlign="Center" Width="10%" />
            </asp:TemplateColumn>
           	           
           </Columns>
              <HeaderStyle Font-Bold="True" />
          </asp:DataGrid>
          
         </div>
          
        </td>
       </tr>
       <tr runat="server" id="trNoRequest" visible="false"><td style="font-size:small;" class="GridRows">&nbsp;There is no requested item.</td></tr>
      </table>
     </div>
     <br />     
     <div style="text-align:center;">
         <asp:Button ID="btnSend" runat="server" Text="Submit"  OnClick="btnSend_Click" ValidationGroup="save" />
     </div>     
    </div>
   </td>
  </tr>  
 </table>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphHead">
    </asp:Content>
