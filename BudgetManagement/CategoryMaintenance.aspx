<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="CategoryMaintenance.aspx.cs" Inherits="BudgetManagement_CategoryMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:ScriptManager id="smP" runat="server"></asp:ScriptManager> 
 <asp:UpdatePanel ID="upl" runat="server">
 <ContentTemplate>
    <table width="100%" cellpadding="0" cellspacing="0">
 
  <%--<tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="" class="SiteMap">Administrative Approver Settings</a> » 
     <a href="ApproverSettingsMRCF.aspx" class="SiteMap">Modules Approver Settings</a> 
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
   <tr runat="server" id="pnlAddItem">
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Add New Category</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>  
     <br />  
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid"> 
       <%--<tr>
        <td colspan="2" class="GridText"><b>Add New Module Approver</b>
         <table>
          <tr>
           <td>&nbsp;<img src="../Support/additem22.png" alt="Requested Items" /></td>
           <td>&nbsp;<b>Add New Module Approver</b></td>
          </tr>
         </table>        
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;" >Account Category Name:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox>
           </td>
       </tr>       
       <tr  id="trDivision" runat="server">
        <td class="GridRows">Account Category Type:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlCategoryType" runat="server">
            </asp:DropDownList>
           </td>
       </tr>
       <tr>
        <td class="GridRows">Record Order:</td>
        <td class="GridRows">

            <asp:TextBox ID="txtRecordOrder" runat="server" Width="58px"></asp:TextBox>
           </td>
       </tr>
      </table>
     </div>     
     <br /> 
 
     <div style="text-align:center;">      
     <%--<asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" 
             onclick="btnSearch_Click" ValidationGroup="SaveIT" />&nbsp;--%>
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" 
             onclick="btnSave_Click" ValidationGroup="SaveIT" />--%>
         <asp:Button ID="btnSave" runat="server" Text="Add Category" 
             onclick="btnSave_Click"/>
         &nbsp;<asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
             Text="Cancel" />
     </div>
    </div>
   </td>
  </tr>  
 
  
  <tr><td style="height:9px;"></td></tr>
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">List of Category</span></b>
     <br />
     <br />
            
     <div class="GridBorder">
                          <table width="100%" cellpadding="3" class="grid">
                           <%--<tr>
                            <td class="GridText"><b>List of Module Approver</b>
                             <table>
                              <tr>
                               <td>&nbsp;<img src="../Support/Paper22.png" alt="" /></td>
                               <td>&nbsp;<b>List of Module Approver</b></td>
                              </tr>
                             </table> 
                            </td>
                           </tr>--%>
                           <tr>
                            <td class="GridRows">
                             <table width="100%" cellpadding="5" cellspacing="1">
                            <tr>
                            <td class="GridColumns" style="width:10%;"><b>Code</b></td>
                             <td class="GridColumns" style="width:60%;"><b>Name</b></td>
                             <td class="GridColumns" style="width:20%;"><b>Type</b></td>
                             <td class="GridColumns" style="width:10%;"><b>Enabled</b></td>
                            </tr>
                                 <asp:Label ID="lblItems" runat="server" Text="" Visible="True"></asp:Label>
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
