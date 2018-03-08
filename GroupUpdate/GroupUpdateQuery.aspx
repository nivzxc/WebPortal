<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="GroupUpdateQuery.aspx.cs" Inherits="GroupUpdate_GroupUpdateQuery" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
    <asp:UpdatePanel ID="upDetails" runat="server">
    <ContentTemplate>
    <table width="100%">
  <tr runat="server" id="trEncoder">
   <td> 
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;">
     <div class="GridBorder">
						<table width="100%">
							<tr><td colspan="2" class="masterpanel">&nbsp;<b>Filter Options</b></td></tr>
							<tr>
								<td class="GridRows" style="width: 15%">Division:</td>
								<td class="GridRows" style="width: 85%">
									<asp:DropDownList ID="ddlDivision" runat="server" CssClass="controls" 
                                        AutoPostBack="True" onselectedindexchanged="ddlDivision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
							</tr>
							<tr>
								<td class="GridRows" valign="top">Department:</td>
								<td class="GridRows">
									<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="controls" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
							</tr>
                            <tr>
								<td class="GridRows">Date Start:</td>
								<td class="GridRows">
									<ew:calendarpopup ID="dtDateStart" runat="server" PopupLocation="Bottom" 
                              ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" 
                              TextBoxLabelStyle-Width="70px"
                              AutoPostBack="true">
           <TextBoxLabelStyle Width="70px"></TextBoxLabelStyle>
                              <ButtonStyle CssClass="controls" />
                             </ew:calendarpopup></td>
							</tr>
                            <tr>
                              <td class="GridRows">Date End:</td>
                              <td class="GridRows">
                                  <ew:calendarpopup ID="dtDateEnd" runat="server" PopupLocation="Bottom" 
                              ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" 
                              TextBoxLabelStyle-Width="70px"
                              AutoPostBack="true">
           <TextBoxLabelStyle Width="70px"></TextBoxLabelStyle>
                              <ButtonStyle CssClass="controls" />
                             </ew:calendarpopup></td>
                             </tr> 
                             <tr>
                              <td class="GridRows">Keyword:</td>
                              <td class="GridRows">
                                  <asp:TextBox ID="txtKeyWord" runat="server" MaxLength="30"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td class="GridRows">&nbsp;</td>
                              <td class="GridRows">
                                  <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                      onclick="btnSearch_Click" /></td>
                             </tr>
						</table>
					</div>
     <%--<br />--%>
<%--     <br /> 
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
        <tr>
         <td class="GridColumns" style="width:100%;text-align:left">&nbsp;<b>Result</b></td>
        </tr>
          <asp:Label ID="lblQueryResult" runat="server" Text="" Visible = "false"></asp:Label>
      </table>
      <div id="pagination" style="clear: both;">
        <asp:Label ID="lblPage" runat="server" Text=""></asp:Label>
     </div>   
     </div>  
         --%>
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
    </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

