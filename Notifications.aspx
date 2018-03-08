<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="Notifications" %>
<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="Default.aspx" class="SiteMap">Home</a> » 
     <a href="Notifications.aspx" class="SiteMap">Notifications</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Notifications</span></b>
     <br />
     <br />
     <br />
     <div>
      <asp:ImageButton runat="server" ID="btnCompose" ImageUrl="~/Support/btnAdd.jpg" onclick="btnCompose_Click" />
     </div>
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="0">
       <tr><td class="GridText" style="height:20px;">&nbsp;&nbsp;<b>Inbox</b></td></tr>       
       <tr>
        <td>
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgInbox" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid">
           <HeaderStyle Font-Bold="true" Height="20" VerticalAlign="Middle" CssClass="DataGridColumns" />
           <ItemStyle CssClass="DataGridRows" Height="20px" VerticalAlign="Middle" />
           <AlternatingItemStyle CssClass="DataGridRows2" Height="20px" VerticalAlign="Middle" />
           <Columns>
	           <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
	            <ItemTemplate>
	             <asp:CheckBox runat="server" id="chkFlag" />
	            </ItemTemplate>
	           </asp:TemplateColumn>
	           
            <asp:TemplateColumn HeaderText="Subject" ItemStyle-Width="55%">
             <ItemTemplate>
              &nbsp;
              <asp:HiddenField runat="server" ID="hdnRead" Value='<%#DataBinder.Eval(Container.DataItem, "pread")%>' />
              <asp:HiddenField runat="server" ID="hdnMessageCode" Value='<%#DataBinder.Eval(Container.DataItem, "msgcode")%>' />
              <asp:HyperLink runat="server" ID="lnkSubject" Text='<%#DataBinder.Eval(Container.DataItem, "msgbody")%>'></asp:HyperLink>
             </ItemTemplate>
            </asp:TemplateColumn>	           
	                    
            <asp:TemplateColumn HeaderText="Sender" ItemStyle-Width="20%">
             <ItemTemplate>
              &nbsp;<asp:HyperLink runat="server" ID="lnkSender" Text='<%#DataBinder.Eval(Container.DataItem, "sentby")%>'></asp:HyperLink>
             </ItemTemplate>
            </asp:TemplateColumn>

	           <asp:TemplateColumn HeaderText="Date Sent" ItemStyle-Width="20%">
	            <ItemTemplate>	             
              &nbsp;<asp:HiddenField runat="server" ID="hdnDateSent" Value='<%#DataBinder.Eval(Container.DataItem, "datesent")%>' />
              <asp:Label runat="server" ID="lblDateSent"></asp:Label>	             
	            </ItemTemplate>
	           </asp:TemplateColumn>
           	           
           </Columns>
          </asp:DataGrid>
         </div>
        </td>
       </tr>
       <tr><td class="GridColumns" style="height:20px;font-size:small;text-align:left;">&nbsp;Pages&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblInboxPaging"></asp:Label></td></tr>
      </table>
     </div>      
    </div>
   </td>
  </tr>  

 </table>  
</asp:Content>