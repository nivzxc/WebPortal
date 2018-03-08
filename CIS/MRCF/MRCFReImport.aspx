<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="MRCFReImport.aspx.cs" Inherits="CIS_MRCF_MRCFReImport" %>

<asp:Content ID="cntMRCFProcApprove" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../EFormsMain.aspx" class="SiteMap">CIS</a> » 
     <a href="MRCFMenu.aspx" class="SiteMap">MRCF</a> » 
     <a href="MRCFReImport.aspx?mrcfcode=<% Response.Write(Request.QueryString["mrcfcode"]);%>" class="SiteMap">MRCF Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Import MRCF Request to Oracle</span></b>
     
     <br />

     <div runat="server" id="divButtons2" style="text-align:center;">
      <br />
      <asp:ImageButton runat="server" ID="btnApprove2" 
             ImageUrl="~/Support/btnProcess.jpg" OnClick="btnApprove_Click"/>
      &nbsp;
      &nbsp;
      <br />
     </div>
          
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
       <tr>
        <td colspan="2" class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>
            <table>
             <tr>
              <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
              <td>&nbsp;<b>MRCF Details</b></td>              
             </tr>
            </table>            
           </td>
           <td style="text-align:right;">&nbsp;</td>
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
        <td class="GridRows"><asp:TextBox runat="server" ID="txtIntended" CssClass="controls" Width="98%" ReadOnly="true"></asp:TextBox></td>
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
      <table width="100%" cellpadding="0" class="grid">
       <tr>
        <td colspan="2" class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>
            <table>
             <tr>
              <td>&nbsp;<img src="../../Support/Box22.png" alt="" /></td>
              <td>&nbsp;<b>Requested Items</b></td>              
             </tr>
            </table>            
           </td>
           <td style="text-align:right;"><asp:CheckBox runat="server" ID="chkShowSpecification" Checked="true" AutoPostBack="true" Text="Show Specification" Font-Size="Small" OnCheckedChanged="chkShowSpecification_CheckedChanged" />&nbsp;</td>
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
	              <tr><td>Type: <asp:Label runat="server" ID="lblAsset" Text='<%#DataBinder.Eval(Container.DataItem, "asstcode")%>'></asp:Label></td></tr>	          
	              <tr><td><asp:TextBox runat="server" ID="txtItemSpec" Text='<%#DataBinder.Eval(Container.DataItem, "itemspec")%>' CssClass="controls" TextMode="MultiLine" Width="95%" Rows="6" BackColor="White"></asp:TextBox></td></tr>
	             </table>
	            </ItemTemplate>
	           </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderText="Qty">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
             <ItemTemplate>          
              <asp:Label runat="server" ID="lblQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Unit">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />           
             <ItemTemplate>
              &nbsp;<asp:Label runat="server" ID="lblUnit" Text='<%#DataBinder.Eval(Container.DataItem, "unit")%>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateColumn>
         
            <asp:TemplateColumn HeaderText="Date Needed">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="center" />
             <ItemTemplate>
              <asp:Label runat="server" ID="lblDateNeeded" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dateneed")).ToString("MM/dd/yyyy")%>'></asp:Label>&nbsp;
             </ItemTemplate>
            </asp:TemplateColumn>
           </Columns>
          </asp:DataGrid>
         </div>
        </td>
       </tr>
      </table>
     </div>

     <div runat="server" id="divSave" style="text-align:center;">
      <br />
     </div>
     
     <div runat="server" id="divButtons" style="text-align:center;">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" 
             ImageUrl="~/Support/btnProcess.jpg" OnClick="btnApprove_Click"/>--%><asp:Button ID="btnApprove"
                 runat="server" Text="Import Now"  OnClick="btnApprove_Click"/>
      &nbsp;</div>
     
    </div>
   </td>
  </tr>
 
 </table>
</asp:Content>

