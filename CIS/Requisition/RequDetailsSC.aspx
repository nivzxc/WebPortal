<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="RequDetailsSC.aspx.cs" Inherits="CIS_Requisition_RequDetailsSC" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
<%--   <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="RequMenu.aspx" class="SiteMap">Requisition</a> » 
     <a href="RequDetailsSC.aspx?requcode=<%Response.Write(Request.QueryString["requcode"]);%>" class="SiteMap">Requisition Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
  --%>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Process Requisition</span></b>
     <br /><br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
       <%--<tr>
        <td colspan="4" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Paper22.png" alt="For Approval" /></td>
           <td>&nbsp;<b>Requisition Details</b></td>           
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows">Request Code:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequisitionCode" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>
         &nbsp;Date Requested:&nbsp;
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>         
        </td>        
       </tr>
       <tr>
        <td class="GridRows">Employee:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtUserName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnUserCode" runat="server" />
         <asp:HiddenField ID="hdnUserMail" runat="server" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Group:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRCName" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnRCCode" runat="server" />
        </td>
       </tr>  
       <tr>
        <td class="GridRows">Charge To:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Budget:
         <asp:TextBox runat="server" ID="txtChargeToBudget" CssClass="controls" Width="70px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnChargeTo" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>
       </tr>         
       <tr>
        <td class="GridRows" style="vertical-align:top;">Intended For:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtIntended" CssClass="controls" Width="470px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr> 
       <tr>
        <td class="GridRows">Group Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtGrpHeadName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnGrpHeadCode" runat="server" />
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGrpHeadRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" ReadOnly="true"></asp:TextBox></td>
       </tr>   
       <tr>
        <td class="GridRows">Division Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDiviHeadName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnDiviHeadCode" runat="server" />
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDiviHeadRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Supply Custodian:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtSuppName" ReadOnly="true" CssClass="controls" Width="250px"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtSuppRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" MaxLength="200" BackColor="white"></asp:TextBox></td>
       </tr>
      </table>
     </div> 
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="grid">
       <%--<tr>
        <td colspan="2" class="GridText">
        <table>
         
        </table>
        </td>
       </tr>--%>
       <tr>
          <td  colspan="2" class="GridColumns">&nbsp;<b>List of Requested Items</b></td>
       </tr>
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" BorderWidth="2" HeaderStyle-Height="20px" ItemStyle-BackColor="honeydew" AlternatingItemStyle-BackColor="aliceblue" HeaderStyle-Font-Bold="true" ItemStyle-VerticalAlign="Top">
           <Columns>
            <asp:TemplateColumn HeaderText="Item Details and Pricing" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="GridColumns">
             <ItemTemplate>
              <table cellpadding="0" cellspacing="0">
               <tr>
                <td>Item Code:</td>
                <td><asp:Label runat="server" forecolor="dodgerblue" id="lblItemCode" Text='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>'></asp:Label></td>
               </tr>
               <tr>
                <td>Description:</td>
                <td><asp:Label runat="server" forecolor="dodgerblue" id="lblItemDesc" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>'></asp:Label></td>
               </tr>
               <tr>
                <td>Price:</td>
                <td style="color:#1e90ff">P <asp:Label runat="server" id="lblPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("###,###.00")%>'></asp:Label></td>
               </tr>
               <tr>
                <td>Total Price:</td>
                <td style="color:#1e90ff">P <asp:Label runat="server" id="lblTPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tprice")).ToString("###,###.00")%>'></asp:Label></td>
               </tr>              
               <tr>
                <td>Ordered Qty:</td>
                <td><asp:TextBox ForeColor="dodgerblue" Width="50px" ReadOnly="true" BorderStyle="None" CssClass="controls" runat="server" id="txtQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>' BackColor="transparent"></asp:TextBox></td>
               </tr>
               <tr>
                <td>Issued Qty:</td>
                <td><asp:TextBox ForeColor="dodgerblue" Width="50px" ReadOnly="true" BorderStyle="None" CssClass="controls" runat="server" id="txtIssued" Text='<%#DataBinder.Eval(Container.DataItem, "soqty")%>' BackColor="transparent"></asp:TextBox></td>
               </tr>              
               <tr>
                <td>Purpose:</td>
                <td><asp:Label runat="server" ID="lblPurpose" Text='<%#DataBinder.Eval(Container.DataItem, "reason")%>'></asp:Label></td>
               </tr>
              </table>
             </ItemTemplate>
            </asp:TemplateColumn>                   
            
            <asp:TemplateColumn HeaderText="Balance" ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="GridColumns" ItemStyle-VerticalAlign="Top">
             <ItemTemplate>
              <asp:TextBox runat="server" ID="txtBalance" cssclass="controls" Width="50px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")) - Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soqty"))%>' BorderStyle="None" BackColor="transparent" MaxLength="6" Font-Size="X-Small" />
             </ItemTemplate>
            </asp:TemplateColumn>           
            
            <asp:TemplateColumn HeaderText="Issue Qty" ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="GridColumns" ItemStyle-VerticalAlign="Top">
             <ItemTemplate>
              <asp:TextBox runat="server" ID="txtSOQty" cssclass="controls" Width="40px" Text="0" BackColor="white" MaxLength="6" Font-Size="X-Small" />
              <asp:Label runat="server" ID="lblSOQtyAll" ForeColor="blue" Text="All<br>items<br>has been<br>issued"></asp:Label>
              <asp:RequiredFieldValidator runat="server" ID="reqSOQty" ErrorMessage="<br>[Required]" Display="Dynamic" ControlToValidate="txtSOQty"></asp:RequiredFieldValidator>
              <asp:CompareValidator runat="server" ID="compSOQty" ErrorMessage="<br>[Invalid Entry]" Display="Dynamic" ControlToValidate="txtSOQty" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
              <asp:CompareValidator runat="server" ID="compSOQty2" ErrorMessage="<br>[You cannot issue more than remaining balance]" Display="Dynamic" ControlToValidate="txtSOQty" ControlToCompare="txtBalance" Operator="LessThanEqual" Type="Integer"></asp:CompareValidator>
             </ItemTemplate>
            </asp:TemplateColumn>          
            
            <asp:TemplateColumn HeaderText="Supplies Officer Remarks" ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="GridColumns" ItemStyle-VerticalAlign="Top">
             <ItemTemplate>
              <asp:TextBox runat="server" TextMode="MultiLine" ID="txtSuppRem" cssclass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "supprem")%>' Width="98%" Rows="6" BackColor="white" />
             </ItemTemplate>
            </asp:TemplateColumn>
           </Columns> 
          </asp:DataGrid>             
         </div>       
        </td>
       </tr>
       <tr>
        <td class="GridRows">
         <table cellpadding="3">
          <tr>
           <td><img src="../../Support/sticky32.png" alt="" /></td> 
           <td style="font-size:small;"><b>Total Price:</b></td>
           <td style="font-size:small; text-align:right;"><b><asp:label runat="server" ID="lblTotalPrice"></asp:label></b></td>
          </tr>
         </table>        
        </td>
       </tr>       
      </table>
     </div>
     

            
     <div runat="server" id="divButtons" style="text-align:center;">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnProcess.jpg" OnClick="btnApprove_Click" />  --%>
         <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             OnClick="btnApprove_Click" BackColor="#33CC33" ForeColor="White" />
     </div> 
    </div>
   </td>
  </tr>  
 
 </table>
</asp:Content>