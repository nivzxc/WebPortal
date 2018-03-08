<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="RequDepBudget.aspx.cs" Inherits="CIS_Requisition_RequDepBudget" %>

<asp:Content ID="cntRequDepBudget" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="RequMenu.aspx" class="SiteMap">Requisition</a> » 
     <a href="RequDepBudget.aspx" class="SiteMap">Requisition Budget Settings</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
--%>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
					<b><span class="HeaderText">Office Supplies Budget Settings</span></b>
     <br />
     <br />
     
     <div class="GridBorder" style="width:400px"> 	          
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td class="GridRows" style="width:25%;"><b>Division:</b></td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" BackColor="white" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList></td>
       </tr>
      </table>
     </div>
					
					<br />
					
				 <div class="GridBorder">
						<asp:DataGrid runat="server" ID="dgRC" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid">
	      <Columns>
	       <asp:TemplateColumn HeaderText="Responsibility Center" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="52%" ItemStyle-HorizontalAlign="Left">
	        <ItemTemplate>
	         <asp:HiddenField runat="server" ID="phdnRcCode" Value='<%#DataBinder.Eval(Container.DataItem, "rccode")%>' />
	         &nbsp;<asp:Label runat="server" ID="plblRcName" Text='<%#DataBinder.Eval(Container.DataItem, "rcname")%>'></asp:Label>
	        </ItemTemplate>
	       </asp:TemplateColumn>
	          
        <asp:TemplateColumn HeaderText="1st Quarter" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left">
         <ItemTemplate>
          <table>
           <tr><td>A<asp:TextBox runat="server" ID="ptxttbudget1" Text='<%#DataBinder.Eval(Container.DataItem, "tbudget1")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox></td></tr>
           <tr><td>R<asp:TextBox runat="server" ID="ptxtrbudget1" Text='<%#DataBinder.Eval(Container.DataItem, "rbudget1")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox> </td></tr>
          </table>
         </ItemTemplate>      
        </asp:TemplateColumn>
	          
        <asp:TemplateColumn HeaderText="2nd Quarter" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left">
         <ItemTemplate>
          <table>
           <tr><td>A<asp:TextBox runat="server" ID="ptxttbudget2" Text='<%#DataBinder.Eval(Container.DataItem, "tbudget2")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox></td></tr>
           <tr><td>R<asp:TextBox runat="server" ID="ptxtrbudget2" Text='<%#DataBinder.Eval(Container.DataItem, "rbudget2")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox></td></tr>
          </table> 	               	           
         </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="3rd Quarter" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left">
         <ItemTemplate>
          <table>
           <tr><td>A<asp:TextBox runat="server" ID="ptxttbudget3" Text='<%#DataBinder.Eval(Container.DataItem, "tbudget3")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox></td></tr>
           <tr><td>R<asp:TextBox runat="server" ID="ptxtrbudget3" Text='<%#DataBinder.Eval(Container.DataItem, "rbudget3")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox></td></tr>
          </table>	           
	        </ItemTemplate>
	       </asp:TemplateColumn>
	          
	       <asp:TemplateColumn HeaderText="4th Quarter" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left">
	        <ItemTemplate>
          <table>
           <tr><td>A<asp:TextBox runat="server" ID="ptxttbudget4" Text='<%#DataBinder.Eval(Container.DataItem, "tbudget4")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox></td></tr>
           <tr><td>R<asp:TextBox runat="server" ID="ptxtrbudget4" Text='<%#DataBinder.Eval(Container.DataItem, "rbudget4")%>' CssClass="controls" BackColor="white" Width="60px" MaxLength="9"></asp:TextBox></td></tr>
          </table> 	           
	        </ItemTemplate>
	       </asp:TemplateColumn>	          	           
	           
       </Columns>
      </asp:DataGrid>							 
     </div>
     
     <div style="text-align:center;" id="divButtons" runat="server">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" OnClick="btnSave_Click" />--%>
         <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Support/btnReset.jpg" OnClick="btnReset_Click" />--%>
      <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click"  />
     </div>
     
    </div>    
   </td>
  </tr>
 
 </table>
</asp:Content>