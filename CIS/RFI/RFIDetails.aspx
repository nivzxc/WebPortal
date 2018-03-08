<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RFIDetails.aspx.cs" Inherits="CIS_RFI_RFIDetails" %>
<asp:Content ID="cntMRCFRequest" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="RFIMain.aspx" class="SiteMap">RFI</a> » 
     <a href="RFIDetails.aspx?rficode=<% Response.Write(Request.QueryString["rficode"]); %>" class="SiteMap">RFI Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText">View RFI</span></b>             
     <br />          
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
       <tr>
        <td colspan="4" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>RFI Details</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridRows">RFI Code:</td>
        <td class="GridRows">
         <asp:HiddenField runat="server" ID="hdnStatus" />
         <asp:TextBox runat="server" ID="txtRFICode" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Request Date:
         &nbsp;
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>
       </tr>         
       <tr>
        <td class="GridRows">Intended For:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtIntended" CssClass="controls" ReadOnly="true" Width="98%"></asp:TextBox></td>
       </tr> 
       <tr>
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox></td>
       </tr>         
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Procurement Mngr:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtPMName" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox></td>
       </tr>            
       <tr>
        <td class="GridRows" valign="top">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtPMRem" ReadOnly="true" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>       
      </table>       
     </div>          

     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="0">
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
	           <asp:TemplateColumn HeaderText="Item Description" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnMitmCode" Value='<%#DataBinder.Eval(Container.DataItem, "rrficode")%>' />
	             <table cellpadding="1" width="99%">
	              <tr>
	               <td style="width:100px;">Item:</td>
	               <td><asp:TextBox runat="server" ID="txtItemDesc" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>' CssClass="controls" ReadOnly="true" Width="98%"></asp:TextBox></td>
	              </tr>
	              <tr>
	               <td>Quantity:</td>
	               <td><asp:TextBox runat="server" ID="txtQty" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>' Width="30px" MaxLength="3" ReadOnly="true"></asp:TextBox></td> 
	              </tr>
	              <tr>
	               <td>Date Needed:</td>
	               <td><asp:TextBox runat="server" ID="txtDateNeeded" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "dateneed")%>' Width="150px" ReadOnly="true"></asp:TextBox></td>
	              </tr>
	              <tr>
	               <td style="vertical-align:top;">Details:</td>
	               <td><asp:TextBox runat="server" ID="txtItemDetails" Text='<%#DataBinder.Eval(Container.DataItem, "itemdtls")%>' CssClass="controls" ReadOnly="true" TextMode="MultiLine" Width="98%" Rows="4"></asp:TextBox></td>
	              </tr>	              
	             </table>
	            </ItemTemplate>
	           </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Specification (Procurement)">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" />            
             <ItemTemplate>          
              <table width="99%;">
               <tr>
	               <td style="vertical-align:top; width:100px;">Specification:</td>
	               <td><asp:TextBox runat="server" ID="txtItemDetails" Text='<%#DataBinder.Eval(Container.DataItem, "itemspec")%>' CssClass="controls" ReadOnly="true" TextMode="MultiLine" Width="98%" Rows="10"></asp:TextBox></td>
               </tr>
               <tr>
	               <td style="vertical-align:top;">Price Matrix:</td>
	               <td><asp:TextBox runat="server" ID="TextBox1" Text='<%#DataBinder.Eval(Container.DataItem, "itemprce")%>' CssClass="controls" ReadOnly="true" TextMode="MultiLine" Width="98%" Rows="10"></asp:TextBox></td>
               </tr>               
              </table>
             </ItemTemplate>
            </asp:TemplateColumn>

           </Columns>
          </asp:DataGrid> 
         </div>
        </td>
       </tr>
      </table>
     </div>
     
     <div style="text-align:center;" id="divButtons" runat="server">
      <br />
      <asp:ImageButton runat="server" ID="btnVoid" ImageUrl="~/Support/btnVoid.jpg" ValidationGroup="request" OnClick="btnVoid_Click" />                              
      &nbsp;
      <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />      
     </div>
    </div> 
   </td>
  </tr> 
 </table>
</asp:Content>