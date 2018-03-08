<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="CategoryMaintenanceModify.aspx.cs" Inherits="BudgetManagement_CategoryMaintenanceModify" %>

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
     <b><span class="HeaderText">Modify/ Disable Account Category</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>  
        <asp:HiddenField ID="hdnURL" runat="server" />
     <br />  
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid"> 
       <tr>
        <td colspan="2" class="GridText"><b>Edit/ Disable Account Category</b>
         <%--<table>
          <tr>
           <td>&nbsp;<img src="../Support/additem22.png" alt="Requested Items" /></td>
           <td>&nbsp;</td>
          </tr>
         </table>  --%>          
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;" >Account Category Code:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:Label ID="lblAccountCategoryCode" runat="server"></asp:Label>
           </td>
       </tr>       
       <tr id="trDivision" runat="server">
        <td class="GridRows">Account Category Name:</td>
        <td class="GridRows">
            <asp:TextBox ID="txtAccountCategoryName" runat="server" Width="229px"></asp:TextBox>
           </td>
       </tr>
       <tr id="trDepartment"  runat="server">
        <td class="GridRows">Account Category Type:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlCategoryType" runat="server">
            </asp:DropDownList>
           </td>
       </tr>
       <tr id="tr1"  runat="server">
        <td class="GridRows">Record Order:</td>
        <td class="GridRows">
            <asp:TextBox ID="txtRecordOrder" runat="server" Width="66px"></asp:TextBox>
           </td>
       </tr>
       <tr id="trRcCOde"  runat="server">
        <td class="GridRows">Created By:</td>
        <td class="GridRows">
            <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows">Created On:</td>
        <td class="GridRows">
            <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows">Modified By:</td>
        <td class="GridRows">
            <asp:Label ID="lblModifiedBy" runat="server"></asp:Label>
           </td>
       </tr>
        <tr>
        <td class="GridRows">Modified On:</td>
        <td class="GridRows">
            <asp:Label ID="lblModifiedOn" runat="server"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows"></td>
        <td class="GridRows">

            <asp:CheckBox ID="chkbEnable" runat="server" CssClass="checkB" 
                Text="Enabled this record" />

           </td>
       </tr>
      </table>
     </div>     
     <br /> 
 
     <div style="text-align:center;">      
         <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
         &nbsp;&nbsp;
         <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
             Text="Cancel" />
     </div>
    </div>
   </td>
  </tr>  
</table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

