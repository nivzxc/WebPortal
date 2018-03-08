<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MRCFDetailsDH.aspx.cs" Inherits="CIS_MRCF_MRCFDetailsDH" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntSprvApproveItem" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td style="width: 625px">
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="MRCFMenu.aspx" class="SiteMap">MRCF</a> » 
     <a href="MRCFDetailsDH.aspx?mrcfcode=<% Response.Write(Request.QueryString["mrcfcode"]);%>" class="SiteMap">MRCF Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px; width: 625px;"></td></tr>--%>


  <script type="text/javascript">
      function disableBtn(btnName) {

          var btnApprove2 = document.getElementById("<%=btnApprove2.ClientID%>");
          var btnDisApprove2 = document.getElementById("<%=btnDisapprove2.ClientID%>");
          var btnModify2 = document.getElementById("<%=btnModification2.ClientID%>");

          var btnApprove = document.getElementById("<%=btnApprove.ClientID%>");
          var btnDisApprove = document.getElementById("<%=btnDisApprove.ClientID%>");
          var btnModify = document.getElementById("<%=btnModify.ClientID%>");

          btnApprove2.disabled = true;
          btnDisApprove2.disabled = true;
          btnModify2.disabled = true;
          btnApprove.disabled = true;
          btnDisApprove.disabled = true;
          btnModify.disabled = true;
      }
</script>

 
  <tr>
   <td style="width: 625px">
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
    
     <b><span class="HeaderText">View MRCF Details</span></b>     
     <br />
     <br />
     
     <div runat="server" id="divErr" visible="false">
      <div class="ErrMsg">
       <b>Error:</b>
       <br />
       <asp:Label runat="server" ID="lblErrMsg" Font-Size="Small"></asp:Label>
      </div>
      <br />
     </div>

     <div style="text-align:center;" runat="server" id="divButtons2">
      <%--<asp:ImageButton runat="server" ID="btnApprove2" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click"/>--%>
         <asp:Button ID="btnApprove2" runat="server" Text="Approve" 
             OnClick="btnApprove_Click" OnClientClick="disableBtn();" BackColor="#33CC33" 
             ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisapprove2" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click"/>--%>
       <asp:Button ID="btnDisapprove2" runat="server" Text="Disapprove" 
             OnClick="btnDisApprove_Click" OnClientClick="disableBtn();" BackColor="#FF3300" 
             ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnModification2" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click"/>  --%>   
       <asp:Button ID="btnModification2" runat="server" Text="Needs Modification"  OnClick="btnModify_Click" OnClientClick="disableBtn();"/>
      <br />
      <br />
     </div>
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">      
      <%-- <tr>
        <td colspan="2" class="GridText">
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
         <asp:TextBox runat="server" ID="txtMrcfCode" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Requested: 
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>              
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         -
         <asp:TextBox runat="server" ID="txtRCName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>         
         <asp:HiddenField ID="hdnRequestor" runat="server" />
         <asp:HiddenField ID="hdnRequestorMail" runat="server" />
        </td>
       </tr>              
       <tr>
        <td class="GridRows">Charge To:</td>
        <td class="GridRows">         
         <asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="400px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnChargeTo" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true" Width="150px"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Intended For:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtIntended" 
                CssClass="controls" Width="98%" BackColor="white" Enabled="False"></asp:TextBox></td>
       </tr>       
       <tr> 
        <td class="GridRows">Request Type:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtReqType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnReqType" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Group Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtGrpHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnGrpHeadCode" runat="server" />
         <asp:HiddenField ID="hdnGrpHeadMail" runat="server" />
         <asp:HiddenField ID="hdnGrpHeadStat" runat="server" />
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGrpHeadRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" MaxLength="200"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Division Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDiviHeadName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnDiviHeadStatus" />
        </td>
       </tr>     
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDiviHeadRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" BackColor="white"></asp:TextBox></td>
       </tr>        
       <tr>
        <td class="GridRows">Procurement Mngr:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtProcMngrName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>
         <asp:HiddenField ID="hdnProcMngrCode" runat="server" />
         <asp:HiddenField ID="hdnProcMngrMail" runat="server" />
        </td>
       </tr>     
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtProcMngrRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>                  
      </table>
     </div> 
     
     <br />
         
     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="grid" cellspacing="0">
       <tr>
        <td colspan="2" class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td style="vertical-align:middle;">
          &nbsp;<b>Requested Items</b>    
           </td>
           <td style="text-align:right; vertical-align:middle;"><asp:CheckBox runat="server" ID="chkShowSpecification" Checked="true" AutoPostBack="true" Text="Show Specification" Font-Size="Small" OnCheckedChanged="chkShowSpecification_CheckedChanged" />&nbsp;</td>
          </tr>
         </table>         
        </td>
       </tr>      
       <tr>
        <td class="GridRows">             
         <div class="GridBorder">
	         <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgItems_DeleteCommand" >
	          <Columns>
	           <asp:TemplateColumn HeaderText="Item Description and Specification" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
	            <ItemTemplate>	         
	             <asp:HiddenField runat="server" ID="hdnMitmCode" Value='<%#DataBinder.Eval(Container.DataItem, "mitmcode")%>' />
	             <table cellpadding="1" width="100%">
	              <tr><td>
                      <asp:TextBox runat="server" ID="txtItemDesc" 
                          Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' CssClass="controls" 
                          BackColor="white" Width="98%" Enabled="False" ReadOnly="True"></asp:TextBox></td></tr>
	              <tr><td style="display: none" class="style1"><asp:Label runat="server" ID="lblAsset" style="display: none" Text='<%#DataBinder.Eval(Container.DataItem, "asstcode")%>'></asp:Label></td></tr>	          
	              <tr><td><asp:TextBox runat="server" ID="txtItemSpec" 
                          Text='<%#DataBinder.Eval(Container.DataItem, "itemspec")%>' CssClass="controls" 
                          BackColor="white" TextMode="MultiLine" Width="100%" Rows="4" 
                          Enabled="False" ReadOnly="True"></asp:TextBox></td></tr>
	             </table>
	            </ItemTemplate>

<HeaderStyle CssClass="GridColumns" Width="60%"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" CssClass="GridRows"></ItemStyle>
	           </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Qty">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
             <ItemTemplate>          
              <table cellpadding="1" width="100%">
	              <tr><td>
              <asp:TextBox runat="server" ID="txtQty" CssClass="controls" Enabled="false" 
                     Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>' Width="35px" 
                     MaxLength="3" ValidationGroup="edititem" BackColor="white" ReadOnly="True"></asp:TextBox>
             </td></tr></table>
              <asp:CompareValidator runat="server" ID="cmpQty" ErrorMessage="<br>[Invalid]" ControlToValidate="txtQty" Operator="DataTypeCheck" Display="Dynamic" ValidationGroup="edititem" Type="Integer"></asp:CompareValidator>
              <asp:RangeValidator ID="rngQty" runat="server" ControlToValidate="txtQty" ErrorMessage="<br>[Invalid]" MaximumValue="9999" MinimumValue="1" Display="Dynamic" ValidationGroup="edititem"></asp:RangeValidator>
              <asp:RequiredFieldValidator ID="reqQty" runat="server" ControlToValidate="txtQty" ErrorMessage="<br>[Required]" ValidationGroup="edititem" Display="Dynamic"></asp:RequiredFieldValidator>
             </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Unit" >
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />           
             <ItemTemplate>

             <table cellpadding="0" width="98%">
	              <tr><td>
              &nbsp;<asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "unit")%>'></asp:Label>
               </tr></td></table>
             
             </ItemTemplate>
            </asp:TemplateColumn>
         
            <asp:TemplateColumn HeaderText="Date Needed" HeaderStyle-width="20%">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="center" />
             <ItemTemplate>
              
                <table cellpadding="0" width="98%">
	              <tr><td>
              <cc1:GMDatePicker ID="dteDateNeeded" Enabled="false" runat="server" 
                     CalendarTheme="Blue" CssClass="controls" BackColor="white" DisplayMode="Label" 
                     Date='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dateneed"))%>' 
                     EnableViewState="False"></cc1:GMDatePicker>
                     </tr></td></table>

             </ItemTemplate>
            </asp:TemplateColumn>        			
          
            <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>

             <table cellpadding="0" width="98%">
	              <tr><td>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
               </tr></td></table>

             </ItemTemplate>

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" CssClass="GridRows"></ItemStyle>
            </asp:TemplateColumn>          	           
           </Columns>

<HeaderStyle Font-Bold="True" Height="20px"></HeaderStyle>
          </asp:DataGrid>
         </div>     
        </td>
       </tr>
      </table>
     </div>
     
     <div style="text-align:center;" runat="server" id="divButtons">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click"/>--%>
         <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             OnClick="btnApprove_Click" OnClientClick="disableBtn();" BackColor="#33CC33" 
             ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click"/>--%>
       <asp:Button ID="btnDisApprove" runat="server" Text="Disapprove"  
             OnClick="btnDisApprove_Click" OnClientClick="disableBtn();" BackColor="#FF3300" 
             ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnModify" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click"/>   --%>  
       <asp:Button ID="btnModify" runat="server" Text="Needs Modification"  OnClick="btnModify_Click" OnClientClick="disableBtn();"/>
     </div>    
     
    </div>      
   </td>
  </tr>
 
 </table>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphHead">
    <style type="text/css">
        .style1
        {
            height: 27px;
        }
    </style>
</asp:Content>
