<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="JournalViewandDraft.aspx.cs" Inherits="EmployeeJournal_JournalViewandDraft" %>
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
<%--                           <tr>
                            <td class="GridRows">
                             <table width="100%" cellpadding="5" cellspacing="1">
                            <tr>
                             <td class="GridColumns" style="width:50%;"><b>Week Name</b></td>
                             <td class="GridColumns" style="width:20%;"><b>Date From</b></td>
                             <td class="GridColumns" style="width:20%;"><b>Date To</b></td>
                             <td class="GridColumns" style="width:10%;"><b>&nbsp;</b></td>
                            </tr>
                                 <asp:Label ID="lblItems" runat="server" Text="Label" Visible="False"></asp:Label>
                           </table>      
        </td>

       </tr>--%>
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
     </div>
     <br />
             <table style="width:100%;" id="tblDepHeadRemarks">
            <tr>
                <td class="GridColumns">
                   Immediate Head&#39;s Remarks <asp:Label ID="lblDeptApprover" runat="server" Visible="False"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td  class="GridRows">
                    <asp:Label ID="lblDepartmentHeadsRemarks" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    <br />
<%--            <table style="width:100%;" id="tblDivHeadRemarks">
            <tr>
                <td class="GridColumns" style="height: 9px">
                    Division Level <asp:Label ID="lblDivApprover" runat="server" Visible="False"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td  class="GridRows">
                    <asp:Label ID="lblDivisionHeadsRemarks" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

        <br />--%>
    </div>
   </td>
  </tr>  

  
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>