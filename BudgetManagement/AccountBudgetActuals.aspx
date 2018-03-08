<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="AccountBudgetActuals.aspx.cs" Inherits="BudgetManagement_AccountBudgetActuals" %>

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
     <b><span class="HeaderText">Update Rc&#39;s Actuals</span></b>
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
       <tr  id="trDivision" runat="server">
        <td class="GridRows">Responsibility Code:</td>
        <td class="GridRows">
            <asp:Label ID="lblResponsibilityCode" runat="server"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;">Fiscal Year:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:DropDownList 
                ID="ddlFiscalYear" runat="server">
            </asp:DropDownList>
           </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;">Account Category:</td>
        <td class="GridRows" style="height: 20px; width:70%; margin-left: 40px;">
            <asp:Label ID="lblAccountCategory" runat="server"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;">Account Item:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:Label ID="lblAccountItem" runat="server"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:30%; height: 20px;">Amount:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
           </td>
       </tr> 
        <tr>
        <td class="GridRows" style="width:30%; height: 20px;">Remarks:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:TextBox ID="txtRemarks" runat="server" Height="59px" TextMode="MultiLine" 
                Width="372px"></asp:TextBox>
           </td>
       </tr> 
       <tr>
        <td class="GridRows"></td>
        <td class="GridRows">

            &nbsp;</td>
       </tr>
      </table>
     </div>     
     <br /> 
 
     <div style="text-align:center;">      
     <%--<asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" 
             onclick="btnSearch_Click" ValidationGroup="SaveIT" />&nbsp;--%>
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" 
             onclick="btnSave_Click" ValidationGroup="SaveIT" />--%>
         <asp:Button ID="btnSave" runat="server" Text="Update RC Actual" 
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
     <b><span class="HeaderText">Actual Budget Monitoring - 
         <asp:Label ID="lblAccountItemName" runat="server" Text=""></asp:Label></span></b>
     &nbsp;<br />
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
                                 <td class="masterpanel" style="width:5%;">&nbsp;</td>
                                 <td class="masterpanel" style="width:15%;"><b>Date</b></td>
                                 <td class="masterpanel" style="width:60%;"><b>Remarks</b></td>
                                 <td class="masterpanel" style="width:20%;"><b>Amount</b></td>
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