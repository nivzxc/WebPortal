<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="ApproverSettingsRequ.aspx.cs" Inherits="Admin_ApproverSettingsRequ" %>

<asp:Content ID="cntApproverSettingsRequ" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="" class="SiteMap">Administrative Settings</a> » 
     <a href="ApproverSettingsRequ.aspx" class="SiteMap">Requisition Approver Settings</a> 
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Requisition Approver Settings</span></b>
     <br />
     <br />
            
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
       <tr>
        <td class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>List of Approvers</b></td>
          </tr>
         </table>         
        </td>
       </tr>
       <tr>
        <td class="GridRows">
         <table cellpadding="3" class="grid" width="98%">
          <tr>
           <td class="GridRows"><b>Division:</b></td>
           <td class="GridRows"><asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" BackColor="white" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList></td>
          </tr>         
          <tr>
           <td class="GridRows" colspan="2">
            <div class="GridBorder">
             <asp:DataGrid runat="server" ID="dgApprovers" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgApprovers_DeleteCommand">
	             <Columns>
	              <asp:TemplateColumn HeaderText="Responsibility Centers" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
	               <ItemTemplate>
	                <asp:HiddenField runat="server" ID="hdnRCCode" Value='<%#DataBinder.Eval(Container.DataItem, "rccode")%>' />
	                &nbsp;<asp:Label runat="server" ID="lblRCName" Text='<%#DataBinder.Eval(Container.DataItem, "rcname")%>'></asp:Label>
	               </ItemTemplate>
	              </asp:TemplateColumn>

               <asp:TemplateColumn HeaderText="Approver">
                <HeaderStyle CssClass="GridColumns" />
                <ItemStyle CssClass="GridRows" HorizontalAlign="left" />            
                <ItemTemplate>          
                 <asp:HiddenField runat="server" ID="hdnUsername" Value='<%#DataBinder.Eval(Container.DataItem, "username")%>' />
                 <asp:Label runat="server" ID="lblName" Text='<%#DataBinder.Eval(Container.DataItem, "pname")%>'></asp:Label>
                </ItemTemplate>
               </asp:TemplateColumn>
       			          
               <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
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
        </td>
       </tr>
      </table>
     </div>
    </div>
   </td>
  </tr>  
  
  <tr><td style="height:9px;"></td></tr>
     
  <tr runat="server" id="pnlAddItem">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Add New Approver</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid"> 
       <tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../Support/additem22.png" alt="Requested Items" /></td>
           <td>&nbsp;<b>Add New Group Head Approver</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridRows">Select RC:</td>
        <td class="GridRows">
         <asp:DropDownList ID="ddlRC" AutoPostBack="true" runat="server" CssClass="controls" BackColor="white" OnSelectedIndexChanged="ddlRC_SelectedIndexChanged"></asp:DropDownList>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Select New Approver:</td>
        <td class="GridRows">
         <asp:DropDownList ID="ddlApprover" runat="server" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>
      </table>
     </div>     
     <br />     
     <div style="text-align:center;">      
      <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" OnClick="btnSave_Click" />
     </div>
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>