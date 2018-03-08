<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="JournalViewerA.aspx.cs" Inherits="EmployeeJournal_JournalViewerA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
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
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
<%--     <b><span class="HeaderText">Fiscal Year Weeks</span></b>--%>
        <b><span class="HeaderText" style="font-size: large">Journal</span></b>
        -
        <asp:Label ID="lblSubmittedDetails" runat="server" Font-Size="Large"></asp:Label>
        <br />
        <asp:Label ID="lblWeekDates" runat="server"></asp:Label>
             <br />
        <br />
     <br />
     <br />
            
     <div class="GridBorder">
    <table width="100%" cellpadding="3" class="grid">
      </table>
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns">&nbsp;<b>Journal Entries</b></td></tr>       
       <tr>
        <td class="GridRows">
         <div class="GridBorder" runat="server" id="divScheduleList">
         <asp:Literal runat="server" ID="litContent"></asp:Literal>
         </div>
        </td>
       </tr>
      </table>
      <br />
       <br />
     </div>
    </div>
    <div>
    
        <table style="width:100%;" id="tblDepHeadRemarks">
            <tr>
                <td class="GridColumns">
                    Immediate Head&#39;s Remarks</td>
            </tr>
            <tr>
                <td  class="GridRows">
                    <asp:TextBox ID="txtDepartmentHeadsRemarks" runat="server" Height="91px" 
                        TextMode="MultiLine" Width="619px" Enabled="False"></asp:TextBox>
                    <br />
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtDepartmentHeadsRemarks" ErrorMessage="Remarks required." 
            ForeColor="#FF3300" ValidationGroup="ReqValGroup"></asp:RequiredFieldValidator>
    <br />
            <%--<table style="width:100%;" id="tblDivHeadRemarks">
            <tr>
                <td class="GridColumns" style="height: 9px">
                    Division Head&#39;s Remarks</td>
            </tr>
            <tr>
                <td  class="GridRows">
                    <asp:TextBox ID="txtDivisionHeadsRemarks" runat="server" Height="91px" 
                        TextMode="MultiLine" Width="619px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
        </table>--%>

        <br />
              &nbsp;
          <asp:Button ID="btnApprove" runat="server" Text="Reviewed"  
            Visible="True" onclick="btnApprove_Click" ValidationGroup="ReqValGroup"/>
      &nbsp;
          <asp:Button ID="btnDisapprove" runat="server" Text="Tag for Revision"  
            Visible="False" onclick="btnDisapprove_Click"/>

    </div>
   </td>
  </tr>  
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>