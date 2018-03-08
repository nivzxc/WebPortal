<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MRCFDetailsPM.aspx.cs" Inherits="CIS_MRCF_MRCFDetailsPM" %>
<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="cntMRCFProcApprove" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
    
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
    
    <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Process MRCF Request</span></b>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
     
            
     <br />

      <br />

       <div class="GridBorder" runat="server" id="divButtons2" 
            style="position: inherit; z-index: inherit;">
   <table width="100%" cellpadding="3" class="grid">
      <tr>
        <td class="GridRows">Assign to:</td>
        <td class="GridRows" colspan="2">
            <asp:DropDownList runat="server" 
                ID="ddlAssign" CssClass="controls" AutoPostBack="true" BackColor="white"> </asp:DropDownList>
            </td>       
       </tr>
      <tr>
        <td class="GridRows" >Remarks:</td>
        <td colspan="2" class="GridRows"><asp:TextBox runat="server" ID="txtAssignRemarks" 
                CssClass="controls" Width="99%" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
       </tr>
       <tr>
       <td style="border-width: 1px; padding: 1px; 	border-color: #E0E0E0; text-align:right; line-height:1.4em; font-weight:bold; font-size:11px;" 
               colspan="3" class="style1" >
            <asp:Label ID="lblModal" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lbtnLoadEmployee" runat="server" 
                onclick="lbtnLoadEmployee_Click">View Employee Load&#39;s</asp:LinkButton>
          
         
                </div>

            </td>
       </tr>
    </table>

   <div id ="divModal" runat="server" class="modalPopUpMRCF" style="display: none; padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;"
              >
           <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"  
               PopupControlID="divModal" TargetControlID="lblModal" 
               BackgroundCssClass="modalBackgroundMRCF">
          </cc1:ModalPopupExtender>
           <center><asp:LinkButton ID="lbtnClose" runat="server" 
                onclick="lbtnClose_Click">Close</asp:LinkButton></center> 
 <table width="100%" class="grid" cellpadding="3">
 
       <tr><td> 
      <div class="GridBorder">
      <table width="100%" class="Grid">

      <tr>
        <td class="GridColumns" style="width:70%; height: 9px;"><b>MRCF Details (Employee Load's)</b></td>
        <td class="GridColumns" style="width:30%; height: 9px;"><b>Status</b></td>
       </tr>
          <% LoadListAssigned(); %>
       </table>
        
    </div>
   </td>
  </tr>  
 </table>

  </div>

  <center>
           <%-- <asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click"/>--%>
         <asp:Button ID="btnApprove2" runat="server" Text="Approve"  
               OnClick="btnApprove_Click" OnClientClick="disableBtn();" BackColor="#33CC33" 
               ForeColor="White"/>
      &nbsp;
           <%--<asp:ImageButton runat="server" ID="btnModify" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click"/>--%>
      <asp:Button ID="btnDisapprove2" runat="server" Text="Disapprove" 
               OnClick="btnDisApprove_Click" OnClientClick="disableBtn();" BackColor="#FF3300" 
               ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnModification2" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click"/>--%>
      <asp:Button ID="btnModification2" runat="server" Text="Needs Modification"  OnClick="btnModify_Click" OnClientClick="disableBtn();"/>
     </center>

   
          
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
       <tr>
        <td colspan="2" class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>
                     
           </td>
           <td style="text-align:right;">&nbsp;<asp:ImageButton runat="server" ID="btnPrint" ImageUrl="~/Support/btnPrint.jpg" OnClick="btnPrint_Click" /></td>
          </tr>
         </table>         
        </td>
       </tr>
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
         <asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="350px" ReadOnly="true"></asp:TextBox>         
         <asp:HiddenField runat="server" ID="hdnChargeTo" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Intended For:</td>
        <td class="GridRows">
            <asp:TextBox runat="server" ID="txtIntended" 
                CssClass="controls" Width="98%" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Request Type:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReqType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
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
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGrpHeadRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Division Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDiviHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnDiviHeadCode" runat="server" />
         <asp:HiddenField ID="hdnDiviHeadMail" runat="server" />
         <asp:HiddenField ID="hdnDiviHeadStat" runat="server" />
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
         <asp:HiddenField ID="hdnProcMngrCode" runat="server" />
         <asp:HiddenField ID="hdnProcMngrStat" runat="server" />
        </td>
       </tr>
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtProcMngrRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" MaxLength="200" BackColor="white"></asp:TextBox></td>
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
	         <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid">
	          <Columns>
	           <asp:TemplateColumn HeaderText="Item Description and Specification" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="70%" ItemStyle-HorizontalAlign="Left">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnMitmCode" Value='<%#DataBinder.Eval(Container.DataItem, "mitmcode")%>' />	         
	             <table cellpadding="1" width="98%">
	              <tr><td><asp:Label runat="server" ID="lblItemDesc" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>'></asp:Label></td></tr>
	             
                  <%#DataBinder.Eval(Container.DataItem, "EmpName")%> 
                  <%#DataBinder.Eval(Container.DataItem, "Birthdate")%>
                 
                  <tr><td style="display: Block">Type : <asp:Label runat="server" ID="lblAsset" Text='<%#DataBinder.Eval(Container.DataItem, "TypeCode")%>'></asp:Label></td></tr>	          
	              <tr><td><asp:TextBox runat="server" ID="txtItemSpec" 
                          Text='<%#DataBinder.Eval(Container.DataItem, "itemspec")%>' CssClass="controls" 
                          TextMode="MultiLine" Width="95%" Rows="6" BackColor="White" 
                          Enabled="False" ReadOnly="True"></asp:TextBox></td></tr>
	             </table>
	            </ItemTemplate>

<HeaderStyle CssClass="GridColumns" Width="70%"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" CssClass="GridRows"></ItemStyle>
	           </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderText="Qty">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
             <ItemTemplate>   
             
                     <table cellpadding="1" width="98%">
	              <tr><td>
              <asp:Textbox runat="server"  ID="lblQty" 
                     Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>' 
                     ReadOnly="True" Width="35px" BorderColor="White"></asp:Textbox>
                      </td>
                     </tr></table>

             </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Unit">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />           
             <ItemTemplate>

                 <table cellpadding="1" width="98%">
	              <tr><td>
              &nbsp;<asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "unit")%>'></asp:Label>
              </tr></td></table>

             </ItemTemplate>
            </asp:TemplateColumn>
         
            <asp:TemplateColumn HeaderText="Date Needed">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="center" />
             <ItemTemplate>

              <table cellpadding="1" width="98%">
	              <tr><td>
              <asp:Textbox runat="server" ID="lblDateNeeded" style="text-align:center;"
                     
                          Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dateneed")).ToString("MM/dd/yyyy")%>' 
                          ReadOnly="True" Width="70px" BorderColor="White"></asp:Textbox>&nbsp;
                       </td>
                       </tr></table>

             </ItemTemplate>
            </asp:TemplateColumn>
           </Columns>

<HeaderStyle Font-Bold="True" Height="20px"></HeaderStyle>
          </asp:DataGrid>
         </div>
        </td>
       </tr>
      </table>
     </div>

     <div runat="server" id="divSave" style="text-align:center;">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSave_Click"/>--%>
         <asp:Button ID="btnSave" runat="server" Text="Save Changes"  OnClick="btnSave_Click"/>
     </div>
     
     <div runat="server" id="divButtons" style="text-align:center;">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click"/>--%>
      <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             onclick="btnApprove_Click" OnClientClick="disableBtn();" 
             BackColor="#33CC33" ForeColor="White"/>
      &nbsp;
     <%-- <asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisApprove_Click"/>--%>
     <asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" 
             OnClick="btnDisApprove_Click" OnClientClick="disableBtn();" BackColor="#FF3300" 
             ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnModify" ImageUrl="~/Support/btnModification.jpg" OnClick="btnModify_Click"/>--%>
      <asp:Button ID="btnModify" runat="server" Text="Needs Modification" OnClick="btnModify_Click" OnClientClick="disableBtn();"/>
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
            height: 9px;
        }
    </style>
</asp:Content>
