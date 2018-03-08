<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="JournalEncodingM.aspx.cs" Inherits="EmployeeJournal_JournalEncodingM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
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
          <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="False" 
                 Width="100%" HeaderStyle-Font-Bold="true" BorderStyle="Solid" 
                 ondeletecommand="dgSchedule_DeleteCommand" 
                 onitemcommand="dgSchedule_ItemCommand">
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

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="80%"></ItemStyle>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>

<FooterStyle CssClass="GridColumns"></FooterStyle>

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" CssClass="GridRows" Width="10%"></ItemStyle>
            </asp:TemplateColumn>           	           
           </Columns>

<HeaderStyle Font-Bold="True" Height="0px"></HeaderStyle>
          </asp:DataGrid>
         </div>
         <asp:Label runat="server" ID="lblNoOBSchedule" Text="[No Journal Entry added]" 
                Font-Size="Small"></asp:Label>
        </td>
       </tr>
      </table>
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
        <td class="GridColumns" colspan="2">Approver's Comments</td>
       </tr>
       <tr id="trRc" runat="server">
        <td class="GridRows" style="width: 190px; height: 9px;">Department Approver:<br />
            <asp:Label ID="lblDeptApprover" runat="server"></asp:Label>
           </td>
        <td class="GridRows" style="height: 9px">
            <asp:Label ID="lblDeptRemarks" runat="server"></asp:Label>
           </td>
       </tr>
       <tr>
        <td class="GridRows" style="width: 190px">Division Approver:<br />
            <asp:Label ID="lblDivApprover" runat="server"></asp:Label>
           </td>
        <td class="GridRows">
            <asp:Label ID="lblDivRemarks" runat="server"></asp:Label>
           </td>
       </tr>
      </table>
              &nbsp;
                 <br />
          <asp:Button ID="btnFinalize" runat="server" Text="Finalize &amp; Ask for Approval"  
            Visible="True" onclick="btnFinalize_Click"/>
            </div>
    </div>
   </td>
  </tr>  

  
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
