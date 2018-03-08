<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="HQDirectory.aspx.cs" Inherits="Userpage_HQDirectory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="Default.aspx" class="SiteMap">Home</a> » 
     <a href="UsersIndex.aspx" class="SiteMap">Users Index</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>--%>
     
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     
     <b><span class="HeaderText" >HQ Employees Directory</span></b>
     <br />
     <br />
     <b><span class="HeaderText">STI HQ Trunkline - (02) 812-1784</span></b>
     <br />
     <br />
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
                              <td class="GridRows">Keyword:</td>
                              <td class="GridRows">
                                  <asp:TextBox ID="txtKeyWord" runat="server" MaxLength="30"></asp:TextBox>
                                  <asp:RegularExpressionValidator id="re4" runat="server" 
          ControlToValidate="txtKeyWord" ErrorMessage="<br>[Invalid Input]" 
          ValidationExpression="^([a-zA-Z ]|ñ|Ñ)*$" Display="Dynamic" 
          ValidationGroup="Search" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator></td>
                             </tr>
                             <tr>
                              <td class="GridRows">&nbsp;</td>
                              <td class="GridRows">
                                  <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                      onclick="btnSearch_Click" ValidationGroup="Search" /></td>
                             </tr>
						</table>
					</div>
                    <br />
                      <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <%--<tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Users Index</b></td>
          </tr>
         </table>           
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:15%">&nbsp;</td>
        <td class="GridColumns" style="width:50%"><b>Name</b></td>
        <td class="GridColumns" style="width:35%"><b>Local Number</b></td>
       </tr>
       <% LoadUsers(); %>
      </table>
     </div>         
    </div>      
   </td>
  </tr>     
  
 </table> 
</asp:Content>

