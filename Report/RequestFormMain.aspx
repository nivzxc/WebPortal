<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RequestFormMain.aspx.cs" Inherits="Report_RequestFormMain" %>

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
    <b><span class="HeaderText">Processed E-Forms</span></b> 
     <br /><br />
     <div class="GridBorder">
						<table width="100%">
							<%--<tr><td colspan="2" class="masterpanel">&nbsp;<b>Filter Options</b></td></tr>--%>
							<tr>
								<td class="GridRows" style="width: 15%">Request Form:</td>
								<td class="GridRows" style="width: 85%">
									<asp:DropDownList ID="ddlRequestForm" runat="server" CssClass="controls" 
                                        AutoPostBack="True">
                                        <asp:ListItem Value="RFP">Request For Payment</asp:ListItem>
                                        <asp:ListItem Value="Leave">Leave Application</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
							</tr>
                            <tr>
								<td class="GridRows" style="width: 15%">Status:</td>
								<td class="GridRows" style="width: 85%">
									<asp:DropDownList ID="ddlStatus" runat="server" CssClass="controls" 
                                        AutoPostBack="False">
                                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                        <asp:ListItem Value="APPROVED">Approved</asp:ListItem>
                                        <asp:ListItem Value="DISAPPROVED">Disapproved</asp:ListItem>
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
								<td class="GridRows" style="width: 15%">Keyword:</td>
								<td class="GridRows" style="width: 85%">
                                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="controls" MaxLength="15" 
                                        Width="200px"></asp:TextBox>
                                </td>
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

