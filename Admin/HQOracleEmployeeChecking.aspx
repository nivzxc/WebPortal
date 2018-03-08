<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="HQOracleEmployeeChecking.aspx.cs" Inherits="Admin_HQOracleEmployeeChecking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <asp:ScriptManager id="smP" runat="server"></asp:ScriptManager> 
 <asp:UpdatePanel ID="upl" runat="server">
 <ContentTemplate>
    <table width="100%" cellpadding="0" cellspacing="0">
 
 
  <tr><td style="height:9px;"></td></tr>
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
            
     <div class="GridBorder">
                          <table width="100%" cellpadding="3" class="grid">
                           <tr>
                            <td class="GridColumns"><b>List of Pending Employee for Oracle Import</b>
                            </td>
                           </tr>
                           <tr>
                            <td class="GridRows">
                             <table width="80%" cellpadding="5" cellspacing="1">
                            <tr>
                             <td class="GridColumns" style="width:30%;"><b>Username</b></td>
                             <td class="GridColumns" style="width:30%;"><b>Employee #</b></td>
                             <td class="GridColumns" style="width:40%;"><b>Name</b></td>
                            </tr>
                           <%-- <% LoadEmp(); %>--%>
                                 <asp:Label ID="lblEMps" runat="server" Text="Label"></asp:Label>
                           </table>      
        </td>
       </tr>
      </table>
     </div>
    </div>
   </td>
  </tr>  

  
 </table>
  </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

