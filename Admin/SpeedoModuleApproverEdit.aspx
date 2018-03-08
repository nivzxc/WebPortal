<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="SpeedoModuleApproverEdit.aspx.cs" Inherits="Admin_SpeedoModuleApproverEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <asp:ScriptManager id="smP" runat="server"></asp:ScriptManager> 
 <asp:UpdatePanel ID="upl" runat="server">
 <ContentTemplate>
    <table width="100%" cellpadding="0" cellspacing="0">
<%-- 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="" class="SiteMap">Administrative Approver Settings</a> » 
     <a href="ApproverSettingsMRCF.aspx" class="SiteMap">Modules Approver Settings Edit</a> 
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
   <tr runat="server" id="pnlAddItem">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Edit/Delete Module Approver</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>  
     <br />  
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid"> 
       <tr>
        <td colspan="2" class="GridText"><b>Edit/Delete Module Approver</b>
         <%--<table>
          <tr>
           <td>&nbsp;<img src="../Support/additem22.png" alt="Requested Items" /></td>
           <td>&nbsp;</td>
          </tr>
         </table>  --%>          
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;" >Module:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:Label ID="lblModule" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>       
       <tr id="trDivision" runat="server">
        <td class="GridRows">Division:</td>
        <td class="GridRows">
            <asp:Label ID="lblDivision" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>
       <tr id="trDepartment"  runat="server">
        <td class="GridRows">Department:</td>
        <td class="GridRows">
            <asp:Label ID="lblDepartment" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>
       <tr id="trRcCOde"  runat="server">
        <td class="GridRows">RC Code:</td>
        <td class="GridRows">
            <asp:Label ID="lblRcCode" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows">Approver Name:</td>
        <td class="GridRows">
            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows">Application Level:</td>
        <td class="GridRows">
            <asp:Label ID="lblLevel" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows"></td>
        <td class="GridRows">

            <asp:CheckBox ID="chkbEmail" runat="server" CssClass="checkB" Text="Email" />
            <asp:CheckBox ID="chkbApprove" runat="server" CssClass="checkB" 
                Text="Approve" />
            <asp:CheckBox ID="chkbEnable" runat="server" CssClass="checkB" 
                Text="Enabled" />

           </td>
       </tr>
      </table>
     </div>     
     <br /> 
 
     <div style="text-align:center;">      
     <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSave.jpg" 
             ValidationGroup="SaveIT" onclick="btnSearch_Click" />&nbsp;
      <asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/Support/btnExclude.jpg" 
             ValidationGroup="SaveIT" onclick="btnDelete_Click" />
     </div>
    </div>
   </td>
  </tr>  
</table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

