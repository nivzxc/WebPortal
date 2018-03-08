<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CSSettings.aspx.cs" Inherits="Userpage_CSSettings" %>
<%@ Import Namespace="STIeForms" %>
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true"> 
 <table width="100%" cellpadding="0" cellspacing="0">
<%--   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="ControlPanel.aspx" class="SiteMap">Control Panel</a> » 
     <a href="CSSettings.aspx" class="SiteMap">E-Forms Settings</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>--%>

  <tr runat="server" id="trMRCF">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">MRCF System Settings</span></b>
     <br />
     <br />
     <div class="GridBorder">        
	     <asp:DataGrid runat="server" ID="dgRC" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnItemDataBound="dgRC_ItemDataBound">
	      <Columns>
       	      
	       <asp:TemplateColumn HeaderText="Division" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Left">
	        <ItemTemplate>
	         <div style="padding:3px;">
	          <asp:Label runat="server" ID="lblDivision" Text='<%#DataBinder.Eval(Container.DataItem, "division")%>'></asp:Label>
	         </div>
	        </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="Responsibility Center/Group">
         <HeaderStyle CssClass="GridColumns" />
         <ItemStyle CssClass="GridRows" />            
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:Label runat="server" ID="lblRCName" Text='<%#DataBinder.Eval(Container.DataItem, "rcname")%>'></asp:Label>
           <asp:HiddenField runat="server" ID="hdnRCCOde" Value='<%#DataBinder.Eval(Container.DataItem, "rccode")%>' />
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="Receive Email">
         <HeaderStyle CssClass="GridColumns" />
         <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />           
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:HiddenField runat="server" ID="hdnEmail" Value='<%#DataBinder.Eval(Container.DataItem, "email")%>' />
           <asp:DropDownList runat="server" ID="ddlEmail" CssClass="controls" BackColor="white">
            <asp:ListItem Value="0" Text="No"></asp:ListItem>
            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
           </asp:DropDownList>
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>
           
        <asp:TemplateColumn HeaderText="Require Approval">
         <HeaderStyle CssClass="GridColumns" />
         <ItemStyle CssClass="GridRows" HorizontalAlign="center" />
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:HiddenField runat="server" ID="hdnApproval" Value='<%#DataBinder.Eval(Container.DataItem, "approve")%>' />
           <asp:DropDownList runat="server" ID="ddlApproval" CssClass="controls" BackColor="white">
            <asp:ListItem Value="0" Text="No"></asp:ListItem>
            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
           </asp:DropDownList>
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>         	           
       </Columns>
      </asp:DataGrid>           
     </div>     
     <br />  
     <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSave_Click" />
    </div>   
   </td>
  </tr>

  <tr runat="server" id="trMRCFSpacer"><td style="height:9px;"></td></tr>

  <tr runat="server" id="trRequisition">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Requisition System Settings</span></b>
     <br />
     <br />
     <div class="GridBorder">        
	     <asp:DataGrid runat="server" ID="dgRequRC" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnItemDataBound="dgRequRC_ItemDataBound">
	      <Columns>
       	      
	       <asp:TemplateColumn HeaderText="Division" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Left">
	        <ItemTemplate>
	         <div style="padding:3px;">
	          <asp:Label runat="server" ID="lblDivision" Text='<%#DataBinder.Eval(Container.DataItem, "division")%>'></asp:Label>
	         </div>
	        </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="Responsibility Center/Group">
         <HeaderStyle CssClass="GridColumns" />
         <ItemStyle CssClass="GridRows" />            
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:Label runat="server" ID="lblRCName" Text='<%#DataBinder.Eval(Container.DataItem, "rcname")%>'></asp:Label>
           <asp:HiddenField runat="server" ID="hdnRCCOde" Value='<%#DataBinder.Eval(Container.DataItem, "rccode")%>' />
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="Receive Email">
         <HeaderStyle CssClass="GridColumns" />
         <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />           
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:HiddenField runat="server" ID="hdnEmail" Value='<%#DataBinder.Eval(Container.DataItem, "email")%>' />
           <asp:DropDownList runat="server" ID="ddlEmail" CssClass="controls" BackColor="white">
            <asp:ListItem Value="0" Text="No"></asp:ListItem>
            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
           </asp:DropDownList>
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>
           
        <asp:TemplateColumn HeaderText="Require Approval">
         <HeaderStyle CssClass="GridColumns" />
         <ItemStyle CssClass="GridRows" HorizontalAlign="center" />
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:HiddenField runat="server" ID="hdnApproval" Value='<%#DataBinder.Eval(Container.DataItem, "approve")%>' />
           <asp:DropDownList runat="server" ID="ddlApproval" CssClass="controls" BackColor="white">
            <asp:ListItem Value="0" Text="No"></asp:ListItem>
            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
           </asp:DropDownList>
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>         	           
       </Columns>
      </asp:DataGrid>           
     </div>     
     <br />  
     <asp:ImageButton runat="server" ID="btnRequSave" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnRequSave_Click"/>
    </div>   
   </td>
  </tr>
  <tr runat="server" id="trRequisitionSpacer"><td style="height:9px;"></td></tr>

  <tr runat="server" id="trOvertime" visible="false">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Overtime Approving Settings</span></b>
     <br />
     <br />
     <div class="GridBorder">        
	     <asp:DataGrid runat="server" ID="dgOvertime" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid">
	      <Columns>       	      
	       <asp:TemplateColumn ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns" HeaderText="Division" ItemStyle-Width="25%">
	        <ItemTemplate>
	         <div style="padding:3px;">
	          <asp:HiddenField runat="server" ID="hdnMappCode" Value='<%#DataBinder.Eval(Container.DataItem, "mappcode")%>' />
	          <asp:Label runat="server" ID="lblDivision" Text='<%#DataBinder.Eval(Container.DataItem, "divicode")%>'></asp:Label>
	         </div>
	        </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns" HeaderText="Department" ItemStyle-Width="50%">  
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:HiddenField runat="server" ID="hdnDepartmentCode" Value='<%#DataBinder.Eval(Container.DataItem, "deptcode")%>' />
           <asp:Label runat="server" ID="lblDepartmentName"></asp:Label>           
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>
          
        <asp:TemplateColumn ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" HeaderText="Require Approval" ItemStyle-Width="25%">
         <ItemTemplate>
          <div style="padding:3px;">
           <asp:HiddenField runat="server" ID="hdnApproval" Value='<%#DataBinder.Eval(Container.DataItem, "rapprove")%>' />
           <asp:DropDownList runat="server" ID="ddlApproval" CssClass="controls" BackColor="white">
            <asp:ListItem Value="0" Text="No"></asp:ListItem>
            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
           </asp:DropDownList>
          </div>
         </ItemTemplate>
        </asp:TemplateColumn>         	           
       </Columns>
      </asp:DataGrid>           
     </div>     
     <br />  
     <asp:ImageButton runat="server" ID="btnSaveOvertime" ImageUrl="~/Support/btnSaveChanges.jpg" onclick="btnSaveOvertime_Click" />
    </div>   
   </td>
  </tr>
  <tr runat="server" id="trOvertimeSpacer" visible="false"><td style="height:9px;"></td></tr>
 
 </table>
</asp:Content>