<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="SpeedoModules.aspx.cs" Inherits="Admin_SpeedoModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <asp:ScriptManager id="smP" runat="server"></asp:ScriptManager> 
 <asp:UpdatePanel ID="upl" runat="server">
 <ContentTemplate>
    <table width="100%" cellpadding="0" cellspacing="0">
 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../Admin/SpeedoModuleApprover.aspx" class="SiteMap">Administrative Settings</a> » 
     <a href="ApproverSettingsMRCF.aspx" class="SiteMap">Modules Settings</a> 
    </div>        
   </td>
  </tr>
  --%>
<%--  <tr><td style="height:9px;"></td></tr>--%>
   <tr runat="server" id="pnlAddItem">
   <td>
    <div class="border" style="padding-top:1px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <%--<b><span class="HeaderText">Add New Module</span></b>
     <br />
     <br />--%>
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div> 
     <br />  
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid"> 
       <tr>
        <td colspan="2" class="GridText">&nbsp;<b>Add New Module</b>
         <%--<table>
          <tr>
           <td>&nbsp;<img src="../Support/additem22.png" alt="Requested Items" /></td>
           <td>&nbsp;<b>Add New Module</b></td>
          </tr>
         </table> --%>           
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;" >Module Code:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:TextBox ID="txtModuleCode" runat="server" CssClass="controls" 
                MaxLength="9" Width="40%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtModuleCode" CssClass="controls" 
                ErrorMessage="Module Code Required" ForeColor="Red" ValidationGroup="SaveIT"></asp:RequiredFieldValidator>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Module Name:</td>
        <td class="GridRows">
            <asp:TextBox ID="txtModuleName" runat="server" CssClass="controls" 
                MaxLength="50" Width="70%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtModuleName" CssClass="controls" 
                ErrorMessage="Module Name Required" ForeColor="Red" ValidationGroup="SaveIT"></asp:RequiredFieldValidator>

           </td>
       </tr>
      </table>
     </div>     
     <br /> 
 
     <div style="text-align:center;">      
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" 
             onclick="btnSave_Click" ValidationGroup="SaveIT" />--%><asp:Button ID="btnSave" runat="server"
                 Text="Add" onclick="btnSave_Click" ValidationGroup="SaveIT" />
     </div>
    </div>
    <div runat="server" id="divSuccess" class="SuccessMsg" visible="false"> 
      <font style="color:green;"><b>Successfully added module: </b></font> <br/>
      <asp:Label runat="server" ID="lblSuccessMsg" ForeColor="Green"></asp:Label>
    </div> 
   </td>
  </tr>  
 
  
  <tr><td style="height:9px;"></td></tr>
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
<%--     <b><span class="HeaderText">Modules Settings</span></b>
     <br />
     <br />--%>
            
     <div class="GridBorder">
                          <table width="100%" cellpadding="3" class="grid">
                           <tr>
                            <td class="GridText"><b>List of Modules</b>
                            <%-- <table>
                              <tr>
                               <td>&nbsp;<img src="../Support/Paper22.png" alt="" /></td>
                               <td>&nbsp;<b>List of Modules</b></td>
                              </tr>
                             </table>         --%>
                            </td>
                           </tr>
                           <tr>
                            <td class="GridRows">
                             <table width="100%" cellpadding="5" cellspacing="1">
                            <tr>
                             <td class="GridColumns" style="width:20%;"><b>Module Code</b></td>
                             <td class="GridColumns" style="width:70%;"><b>Module Name</b></td>
                             <td class="GridColumns" style="width:10%;"><b>Delete</b></td>
                            </tr>
                            <% LoadModules(); %>
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

