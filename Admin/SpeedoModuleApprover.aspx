<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="SpeedoModuleApprover.aspx.cs" Inherits="Admin_SpeedoModuleApprover" %>

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
     <b><span class="HeaderText">Add New Module Approver</span></b>
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
        <td class="GridRows" style="width:30%; height: 20px;" >Module:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:DropDownList ID="ddlModule" runat="server" CssClass="controls" 
                AutoPostBack="True" onselectedindexchanged="ddlModule_SelectedIndexChanged">
            </asp:DropDownList>
           </td>
       </tr>       
       <tr  id="trDivision" runat="server">
        <td class="GridRows">Division:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="controls" 
                AutoPostBack="True" onselectedindexchanged="ddlDivision_SelectedIndexChanged">
            </asp:DropDownList>
           </td>
       </tr>
       <tr  id="trDepartment" runat="server">
        <td class="GridRows">Department:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="controls" 
                AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
            </asp:DropDownList>
           </td>
       </tr>
       <tr id="trRc" runat="server">
        <td class="GridRows">RC Code:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlRC" runat="server" CssClass="controls" 
                AutoPostBack="True" CausesValidation="True" 
                onselectedindexchanged="ddlRC_SelectedIndexChanged">
            </asp:DropDownList>
           </td>
       </tr>
       <tr>
        <td class="GridRows">Approver Name:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlName" runat="server" CssClass="controls">
            </asp:DropDownList>
           </td>
       </tr>
       <tr>
        <td class="GridRows">Application Level:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlLevel" runat="server" CssClass="controls">
                <asp:ListItem>Group Head</asp:ListItem>
                <asp:ListItem>Division Head</asp:ListItem>
            </asp:DropDownList>
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
     <%--<asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" 
             onclick="btnSearch_Click" ValidationGroup="SaveIT" />&nbsp;--%>
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" 
             onclick="btnSave_Click" ValidationGroup="SaveIT" />--%>
         <asp:Button ID="btnSave" runat="server" Text="Add Approver" onclick="btnSave_Click"/>
     </div>
    </div>
   </td>
  </tr>  
 
  
  <tr><td style="height:9px;"></td></tr>
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Modules Approver Settings</span></b>
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
                             <td class="GridColumns" style="width:50%;"><b>Name</b></td>
                             <td class="GridColumns" style="width:20%;"><b>Level</b></td>
                             <td class="GridColumns" style="width:10%;"><b>Email</b></td>
                             <td class="GridColumns" style="width:10%;"><b>Approve</b></td>
                             <td class="GridColumns" style="width:10%;"><b>Enable</b></td>
                            </tr>
                                 <asp:Label ID="lblItems" runat="server" Text="Label" Visible="False"></asp:Label>
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

