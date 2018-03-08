<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="JournalEncoding.aspx.cs" Inherits="EmployeeJournal_JournalEncoding" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
        <script type = "text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to submit your Journal?\n Remember that you cannot modify this once submitted. \n Click 'Ok' to Submit.\n Click 'Cancel' to save changes.")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
    </script>
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
     <b><span class="HeaderText">Add New Journal Entry</span></b>
     <br />
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>  
    </div>
   </td>
  </tr>  
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
<%--     <b><span class="HeaderText">Fiscal Year Weeks</span></b>--%>
        <asp:Label ID="lblWeekDates" runat="server"></asp:Label>
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

      <br />
                          <table cellpadding="3" cellspacing="0" class="Grid" width="100%">
                              <tr>
                                  <td class="masterpanel">
                                      &nbsp;<b>Journal Entries</b>
                                  </td>
                              </tr>
                              <tr>
                                  <td class="GridRows">
                                      <CKEditor:CKEditorControl ID="ckeContents" runat="server" BackColor="White" 
                                          CssClass="controls" Height="300px" ToolbarFull="Cut|Copy|Paste|PasteText|PasteFromWord|-|SpellChecker|Scayt
Undo|Redo|-|Find|Replace|-|Bold|Italic|Underline|Strike
Image|Table|HorizontalRule|Smiley|SpecialChar
/
NumberedList|BulletedList|-|Outdent|Indent|Blockquote|CreateDiv
JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock
Link|Unlink|Anchor
TextColor|BGColor
Subscript|Superscript
/
Styles|Format|Font|FontSize" Width="98%" />
                                  </td>
                              </tr>
                          </table>
      <br />

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
       <tr id="tr1" runat="server">
        <td class="GridColumns" colspan="2">Reviewer</td>
       </tr>
       <tr id="trRc" runat="server">
        <td class="GridRows" style="width: 191px; height: 9px;">Immediate Head:</td>
        <td class="GridRows" style="height: 9px">
            <asp:Label ID="lblReviewer" runat="server"></asp:Label>
           </td>
       </tr>
<%--       <tr>
        <td class="GridRows" style="width: 191px">Division Approver:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlDivHead" runat="server" CssClass="controls">
            </asp:DropDownList>
           </td>
       </tr>--%>
      </table>
              &nbsp;
                 <br />
          <asp:Button ID="btnPreview" runat="server" Text="Preview and Save as Draft"  
            Visible="True" onclick="btnPreview_Click"/>
                 &nbsp;<asp:Button ID="btnDraft" runat="server" onclick="btnDraft_Click" 
                     Text="Save as Draft" Visible="True" />
                 &nbsp;<asp:Button ID="btnFinalize" runat="server" onclick="btnFinalize_Click" 
                     Text="Submit" OnClientClick = "Confirm()" Visible="True" />
            </div>
    </div>
   </td>
  </tr>  

  
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
