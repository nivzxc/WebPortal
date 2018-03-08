<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="RequDetailsDH.aspx.cs" Inherits="CIS_Requisition_RequDetailsDH" %>
<%@ Import Namespace="STIeForms" %>
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
<%--
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="RequMenu.aspx" class="SiteMap">Requisition</a> » 
     <a href="RequDetailsDH.aspx?requcode=<%Response.Write(Request.QueryString["requcode"]); %>" class="SiteMap">Requisition Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Requisition Details</span></b>
     <br />
     <div class="ErrMsg" runat="server" id="divErr" visible="false">
      <b>Error:</b>
      <br />
      <asp:Label runat="server" ID="lblErrMsg" Font-Size="Small"></asp:Label>
     </div>
     <br />
     <div runat="server" id="divButtons2" style="text-align:center;">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove2" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click" />--%>
         <asp:Button ID="btnApprove2" runat="server" Text="Approve"  
             OnClick="btnApprove_Click" BackColor="#33CC33" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove2" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click" />--%>
         <asp:Button ID="btnDisApprove2" runat="server" Text="Disapprove"  
             OnClick="btnDisApprove_Click" BackColor="#FF3300" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnForModification2" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click" />  --%>  
         <asp:Button ID="btnForModification2" runat="server" Text="Needs Modification" OnClick="btnModify_Click"/>
     </div>
     <br />     
     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="Grid">    
<%--       <tr>
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
        <td class="GridRows">Request Code:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>         
         Date Requested:
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" ReadOnly="true" Width="150px"></asp:TextBox>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Employee:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         -
         <asp:TextBox runat="server" ID="txtRCName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>        
         <asp:HiddenField ID="hdnRequestor" runat="server" />
         <asp:HiddenField ID="hdnRequestorMail" runat="server" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">ChargeTo:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Budget:
         <asp:TextBox runat="server" ID="txtChargeToBudget" CssClass="controls" Width="70px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnChargeTo" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true"></asp:TextBox>
         &nbsp;
        </td>
       </tr>
       <tr>
        <td class="GridRows" valign="top">Intended For:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" Width="470px" BackColor="white"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Group Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtGrpHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnGrpHeadCode" />
         <asp:HiddenField runat="server" ID="hdnGrpHeadMail" />
         <asp:HiddenField runat="server" ID="hdnGrpHeadStatus" />
        </td>
       </tr> 
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGrpHeadRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Division Head:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDiviHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr> 
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDiviHeadRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" BackColor="white"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Supplies Custodian:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSuppName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField ID="hdnSuppCode" runat="server" />
         <asp:HiddenField ID="hdnSuppMail" runat="server" />
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtSuppRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
      </table>
     </div>
     
     <br />
 

     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="Grid">
     <%--  <tr>
        <td >
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
	         <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" BorderStyle="Solid" ShowFooter="true" HeaderStyle-Height="20px" FooterStyle-Height="20px" FooterStyle-Font-Bold="true">
	          <Columns>
	           <asp:TemplateColumn HeaderText="Requested Items" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnItemCode" Value='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
              <asp:HiddenField runat="server" ID="HiddenField1" Value='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
              <table>
               <tr><td><asp:Label runat="server" ID="Label1" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' Font-Underline="true"></asp:Label></td></tr>
               <tr><td>Purpose:<asp:Label runat="server" ID="lblReason" Text='<%#DataBinder.Eval(Container.DataItem, "reason")%>'></asp:Label></td></tr>
               <tr><td>Last Requested: <asp:Label runat="server" ID="lblDateLast" Text='<% #clsRequisition.GetLastDateItemRequest(DataBinder.Eval(Container.DataItem, "itemcode").ToString(),hdnRequestor.Value,Convert.ToDateTime(txtDateReq.Text))%>'></asp:Label></td></tr>
              </table>	             
	            </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
             <ItemTemplate>
              <asp:Label runat="server" ID="txtQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>' Width="40px" MaxLength="3" ValidationGroup="edititem" BackColor="white"></asp:Label>
<%--              <asp:CompareValidator runat="server" ID="cmpQty" ErrorMessage="<br>[Invalid]" ControlToValidate="txtQty" Operator="DataTypeCheck" Display="Dynamic" ValidationGroup="edititem" Type="Integer"></asp:CompareValidator>
              <asp:RangeValidator ID="rngQty" runat="server" ControlToValidate="txtQty" ErrorMessage="<br>[Invalid]" MaximumValue="9999" MinimumValue="1" Display="Dynamic" ValidationGroup="edititem"></asp:RangeValidator>
              <asp:RequiredFieldValidator ID="reqQty" runat="server" ControlToValidate="txtQty" ErrorMessage="<br>[Required]" ValidationGroup="edititem" Display="Dynamic"></asp:RequiredFieldValidator>
--%>             </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Unit" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
             <ItemTemplate>
              &nbsp;<asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "unit")%>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateColumn>
           
            <asp:TemplateColumn HeaderText="Price" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("#####0.00")%>'></asp:Label>&nbsp;
             </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblTPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tprice")).ToString("#######0.00") %>'></asp:Label>&nbsp;
             </ItemTemplate>           
            </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderText="Issued" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblIssued" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soqty")).ToString("#######0") %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateColumn>            
          			          
           <%-- <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>
            </asp:TemplateColumn>--%>
           </Columns>
          </asp:DataGrid>           
         </div>
        </td>
       </tr>
      </table>
     </div>
           
     <div runat="server" id="divBudget">
      <br />
      <table style="font-size:small;vertical-align:top;">
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
          <tr><td align="center"><img src='../../Support/smile22.png' alt='happy' />&nbsp;<asp:label runat="server" id="lblMessage" ForeColor="green" Text="You have sufficient budget for this request!"></asp:label></td></tr>
         </table>
        </td>
       </tr>
      </table>     
     </div>
     
     <div runat="server" id="divButton" style="text-align:center;">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click" />--%>
      <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             OnClick="btnApprove_Click" BackColor="#33CC33" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click" />--%>
      <asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" 
             OnClick="btnDisApprove_Click" BackColor="#FF3300" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnModify" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click" />  --%>
         <asp:Button ID="btnModify" runat="server" Text="For Modification" OnClick="btnModify_Click"/>
     </div>        
    </div>     
   </td>
  </tr>
    
 </table>
</asp:Content>