<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="JournalEncoding2.aspx.cs" Inherits="EmployeeJournal_JournalEncoding" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 253px;
        }
    </style>
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
   <td class="style2">
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Add New Journal Entry</span></b>
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
       <tr  id="trDivision" runat="server">
        <td class="GridRows">Entry:</td>
        <td class="GridRows">
            <asp:TextBox ID="txtContents" runat="server" Width="443px" Height="113px" 
                TextMode="MultiLine"></asp:TextBox>
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
         <asp:Button ID="btnSave" runat="server" Text="Add to Journal" 
             onclick="btnSave_Click"/>
     </div>
    </div>
   </td>
  </tr>  
 
  
  <tr><td style="height:9px;"></td></tr>
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

      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns">&nbsp;<b>Journal Entries</b></td></tr>       
       <tr>
        <td class="GridRows">
         <div class="GridBorder" runat="server" id="divScheduleList">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

<%--          <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="False" 
                 Width="100%" HeaderStyle-Font-Bold="true" BorderStyle="Solid" 
                 ondeletecommand="dgSchedule_DeleteCommand">
           <Columns>	  
             <asp:TemplateColumn HeaderText="No." HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="10%">
             <ItemTemplate>
                 <asp:Label ID="lblItemNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ItemNumber")%>'></asp:Label>
	            </ItemTemplate>                        

            <HeaderStyle CssClass="GridColumns"></HeaderStyle>

            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="10%"></ItemStyle>
            </asp:TemplateColumn>      
            <asp:TemplateColumn HeaderText="Entry" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="80%">
             <ItemTemplate>
             <asp:HiddenField runat="server" ID="hdnJournalDCode" Value='<%#DataBinder.Eval(Container.DataItem, "JournalDCode")%>' />
              <asp:HiddenField runat="server" ID="hdnJournalCode" Value='<%#DataBinder.Eval(Container.DataItem, "JournalCode")%>' />
                 <asp:Label ID="lblWeekname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Contents")%>'></asp:Label>
	            </ItemTemplate>                       
              <EditItemTemplate>
                    <asp:TextBox ID="txtContents" runat="server"
                        Text='<%#DataBinder.Eval(Container.DataItem, "Contents")%>'></asp:TextBox>
            </EditItemTemplate> 

                <HeaderStyle CssClass="GridColumns"></HeaderStyle>

            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="80%"></ItemStyle>
            </asp:TemplateColumn>

               <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update" HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
               </asp:EditCommandColumn>

            <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png" OnClientClick = "return confirm('Do you want to delete?')"></asp:ImageButton>
             </ItemTemplate>

            <FooterStyle CssClass="GridColumns"></FooterStyle>

            <HeaderStyle CssClass="GridColumns"></HeaderStyle>

            <ItemStyle HorizontalAlign="Center" CssClass="GridRows" Width="10%"></ItemStyle>
            </asp:TemplateColumn>   
                   	           
           </Columns>

            <HeaderStyle Font-Bold="True" Height="0px"></HeaderStyle>
          </asp:DataGrid>--%>



<%--          <asp:GridView ID="dgSchedule" runat="server"  Width = "550px"
AutoGenerateColumns = "false" Font-Names = "Arial"
Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
HeaderStyle-BackColor = "green" AllowPaging ="true"  ShowFooter = "true" 
OnPageIndexChanging = "OnPaging" onrowediting="EditCustomer"
onrowupdating="UpdateJournalDetails"  onrowcancelingedit="CancelEdit"
PageSize = "30" >--%>
<asp:GridView ID="dgSchedule" runat="server"  Width = "100%" AllowPaging ="true"  ShowFooter = "true" 
AutoGenerateColumns = "false" OnPageIndexChanging = "OnPaging" onrowediting="EditCustomer"
onrowupdating="UpdateJournalDetails"  onrowcancelingedit="CancelEdit"
PageSize = "30" >
<Columns>

<asp:TemplateField ItemStyle-Width = "10%"  HeaderText = "No." HeaderStyle-CssClass="GridColumns">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
<%--        <asp:Label ID="lblItemNumber" runat="server"
        Text='<%#DataBinder.Eval(Container.DataItem, "ItemNumber")%>'></asp:Label>--%>
    </ItemTemplate>
<%--    <FooterTemplate>
        <asp:TextBox ID="txtItemNumber" Width = "40px"
            MaxLength = "5" runat="server"></asp:TextBox>
    </FooterTemplate>--%>
    <HeaderStyle CssClass="GridColumns" />
    <ItemStyle Width="10%" />
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width = "70%"  HeaderText = "Contents" HeaderStyle-CssClass="GridColumns">
    <ItemTemplate>
        <asp:Label ID="lblContents" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Contents")%>'></asp:Label>
          
    </ItemTemplate>
    <EditItemTemplate>
       <asp:HiddenField runat="server" ID="hdnJournalDCode" Value='<%#DataBinder.Eval(Container.DataItem, "JournalDCode")%>' />
        <asp:HiddenField runat="server" ID="hdnJournalCode" Value='<%#DataBinder.Eval(Container.DataItem, "JournalCode")%>' />
        <asp:TextBox ID="txtContents" runat="server" Width = "98%" Text='<%#DataBinder.Eval(Container.DataItem, "Contents")%>'></asp:TextBox>
    </EditItemTemplate> 
<%--    <FooterTemplate>
        <asp:TextBox ID="txtContents" runat="server"></asp:TextBox>
    </FooterTemplate>--%>
    <HeaderStyle CssClass="GridColumns" />
    <ItemStyle Width="70%" />
</asp:TemplateField>
<asp:CommandField  ShowEditButton="True"  ItemStyle-Width = "10%"  >
    <ItemStyle Width="10%" />
    </asp:CommandField>
<asp:TemplateField ItemStyle-Width = "10%"  HeaderText = "" HeaderStyle-CssClass="GridColumns">
    <ItemTemplate>
        <asp:LinkButton ID="lnkRemove" runat="server"
            CommandArgument = '<%#DataBinder.Eval(Container.DataItem, "JournalDCode")%>'
         OnClientClick = "return confirm('Do you want to delete?')"
        Text = "Delete" OnClick = "DeleteJournalDetails"></asp:LinkButton>
    </ItemTemplate>
<%--    <FooterTemplate>
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            OnClick = "AddNewJournalDetails" />
    </FooterTemplate>--%>
    <HeaderStyle CssClass="GridColumns" />
    <ItemStyle Width="10%" />
</asp:TemplateField>
</Columns>
<AlternatingRowStyle BackColor="#F0F8FF"  />
</asp:GridView>




          </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID = "dgSchedule" />
            </Triggers>
            </asp:UpdatePanel>
         </div>
         <asp:Label runat="server" ID="lblNoOBSchedule" Text="[No Journal Entry added]" 
                Font-Size="Small"></asp:Label>
        </td>
       </tr>
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
        <td class="GridColumns" colspan="2">Select Approver</td>
       </tr>
       <tr id="trRc" runat="server">
        <td class="GridRows" style="width: 191px; height: 9px;">Department Approver:</td>
        <td class="GridRows" style="height: 9px">
            <asp:DropDownList ID="ddlDeptHead" runat="server" CssClass="controls" 
                AutoPostBack="True" CausesValidation="True">
            </asp:DropDownList>
           </td>
       </tr>
       <tr>
        <td class="GridRows" style="width: 191px">Division Approver:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlDivHead" runat="server" CssClass="controls">
            </asp:DropDownList>
           </td>
       </tr>
      </table>
              &nbsp;
                 <br />
          <asp:Button ID="btnDraft" runat="server" Text="Save as Draft"  
            Visible="True" onclick="btnDraft_Click"/>
                 &nbsp;<asp:Button ID="btnFinalize" runat="server" onclick="btnFinalize_Click" 
                     Text="Finalize &amp; Ask for Approval" Visible="True" />
            </div>
    </div>
   </td>
  </tr>  

  
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
