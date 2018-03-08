<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="JournalWeeks.aspx.cs" Inherits="EmployeeJournal_JournalWeeks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 9px;
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
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Add New Journal Week</span></b>
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
        <td class="GridRows" style="width:30%; height: 20px;" >Fiscal Year:</td>
        <td class="GridRows" style="height: 20px; width:70%;">
            <asp:DropDownList ID="ddlFiscalYear" runat="server" CssClass="controls" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlFiscalYear_SelectedIndexChanged">
            </asp:DropDownList>
           </td>
       </tr>       
       <tr  id="trDivision" runat="server">
        <td class="GridRows">Week Name:</td>
        <td class="GridRows">
            <asp:TextBox ID="txtWeekName" runat="server" Width="365px"></asp:TextBox>
           </td>
       </tr>
       <tr  id="tr2" runat="server">
        <td class="GridRows">Week Number:</td>
        <td class="GridRows">
            <asp:DropDownList ID="ddlWeekNunmber" runat="server">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
                <asp:ListItem Value="11"></asp:ListItem>
                <asp:ListItem Value="12"></asp:ListItem>
                <asp:ListItem Value="13"></asp:ListItem>
                <asp:ListItem Value="14"></asp:ListItem>
                <asp:ListItem Value="15"></asp:ListItem>
                <asp:ListItem Value="16"></asp:ListItem>
                <asp:ListItem Value="17"></asp:ListItem>
                <asp:ListItem Value="18"></asp:ListItem>
                <asp:ListItem Value="19"></asp:ListItem>
                <asp:ListItem Value="20"></asp:ListItem>
                <asp:ListItem Value="21"></asp:ListItem>
                <asp:ListItem Value="22"></asp:ListItem>
                <asp:ListItem Value="23"></asp:ListItem>
                <asp:ListItem Value="24"></asp:ListItem>
                <asp:ListItem Value="25"></asp:ListItem>
                <asp:ListItem Value="26"></asp:ListItem>
                <asp:ListItem Value="27"></asp:ListItem>
                <asp:ListItem Value="28"></asp:ListItem>
                <asp:ListItem Value="29"></asp:ListItem>
                <asp:ListItem Value="30"></asp:ListItem>
                <asp:ListItem Value="31"></asp:ListItem>
                <asp:ListItem Value="32"></asp:ListItem>
                <asp:ListItem Value="33"></asp:ListItem>
                <asp:ListItem Value="34"></asp:ListItem>
                <asp:ListItem Value="35"></asp:ListItem>
                <asp:ListItem Value="36"></asp:ListItem>
                <asp:ListItem Value="37"></asp:ListItem>
                <asp:ListItem Value="38"></asp:ListItem>
                <asp:ListItem Value="39">39</asp:ListItem>
                <asp:ListItem Value="40"></asp:ListItem>
                <asp:ListItem Value="41"></asp:ListItem>
                <asp:ListItem Value="42"></asp:ListItem>
                <asp:ListItem Value="43"></asp:ListItem>
                <asp:ListItem Value="44"></asp:ListItem>
                <asp:ListItem Value="45"></asp:ListItem>
                <asp:ListItem Value="46"></asp:ListItem>
                <asp:ListItem Value="47"></asp:ListItem>
                <asp:ListItem Value="48"></asp:ListItem>
                <asp:ListItem Value="49"></asp:ListItem>
                <asp:ListItem Value="50"></asp:ListItem>
                <asp:ListItem Value="51"></asp:ListItem>
                <asp:ListItem Value="52"></asp:ListItem>
                <asp:ListItem Value="53"></asp:ListItem>
                <asp:ListItem Value="54"></asp:ListItem>
                <asp:ListItem Value="55"></asp:ListItem>
                <asp:ListItem Value="56"></asp:ListItem>
                <asp:ListItem Value="57"></asp:ListItem>
                <asp:ListItem Value="58"></asp:ListItem>
                <asp:ListItem Value="59"></asp:ListItem>
                <asp:ListItem Value="60"></asp:ListItem>
                <asp:ListItem Value="61"></asp:ListItem>
                <asp:ListItem Value="62"></asp:ListItem>
                <asp:ListItem Value="63"></asp:ListItem>
                <asp:ListItem Value="64"></asp:ListItem>
                <asp:ListItem Value="65"></asp:ListItem>
                <asp:ListItem Value="66"></asp:ListItem>
                <asp:ListItem Value="67"></asp:ListItem>
                <asp:ListItem Value="68"></asp:ListItem>
                <asp:ListItem Value="69"></asp:ListItem>
                <asp:ListItem Value="70"></asp:ListItem>
            </asp:DropDownList>
           </td>
       </tr>
       <tr  id="trDepartment" runat="server">
        <td class="GridRows">Date From:</td>
        <td class="GridRows" style="height: 9px">
        <ew:CalendarPopup ID="dtpFrom" runat="server" CssClass="" 
                         DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" 
                         CalendarTheme="Blue" EnableTimePicker="false" EnableDropShadow="True" 
                         MaxDate="2020-12-31" MinDate="2000-01-01" 
                         AutoPostBack="True" 
                         ControlDisplay="TextBoxImage" ImageUrl="~/Support/calendarbutton.png"></ew:CalendarPopup>
            &nbsp;</td>
       </tr>
       <tr  id="tr1" runat="server">
        <td class="GridRows">Date To:</td>
        <td class="GridRows">
        <ew:CalendarPopup ID="dtpTo" runat="server" CssClass="" 
                         DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" 
                         CalendarTheme="Blue" EnableTimePicker="false" EnableDropShadow="True" 
                         MaxDate="2020-12-31" MinDate="2000-01-01" 
                         AutoPostBack="True" 
                         ControlDisplay="TextBoxImage" ImageUrl="~/Support/calendarbutton.png"></ew:CalendarPopup>
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
         <asp:Button ID="btnSave" runat="server" Text="Add Weeks" 
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
       <tr><td class="GridColumns">&nbsp;<b>Journal Weeks</b></td></tr>       
       <tr>
        <td class="GridRows">
         <div class="GridBorder" runat="server" id="divScheduleList">
          <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" BorderStyle="Solid" ondeletecommand="dgSchedule_DeleteCommand">
           <Columns>	        
            <asp:TemplateColumn HeaderText="Entries" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%">
             <ItemTemplate>
             <asp:HiddenField runat="server" ID="hdnWeekCode" Value='<%#DataBinder.Eval(Container.DataItem, "WeekCode")%>' />
              <asp:HiddenField runat="server" ID="hdnWeekName" Value='<%#DataBinder.Eval(Container.DataItem, "WeekName")%>' />
                 <asp:Label ID="lblWeekname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WeekName")%>'></asp:Label>
	            </ItemTemplate>                        

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="30%"></ItemStyle>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Date From" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%">
             <ItemTemplate>
              <asp:HiddenField runat="server" ID="hdnDateFrom" Value='<%#DataBinder.Eval(Container.DataItem, "DateStart")%>' />
              <ew:CalendarPopup ID="dtpDateFrom" runat="server" PopupLocation="Bottom" ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" SelectedDate='<%#DataBinder.Eval(Container.DataItem, "DateStart")%>' Enabled="false"></ew:CalendarPopup>
	            </ItemTemplate>   

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="30%"></ItemStyle>
	           </asp:TemplateColumn>	           
	           <asp:TemplateColumn HeaderText="Date To" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%">
             <ItemTemplate>
              <asp:HiddenField runat="server" ID="hdnDateTo" Value='<%#DataBinder.Eval(Container.DataItem, "DateEnd")%>' />
              <ew:CalendarPopup ID="dtpDateTo" runat="server" PopupLocation="Bottom" ControlDisplay="TextBoxImage" ImageUrl="~/Support/kworldclock22.png" TextBoxLabelStyle-Width="60px" SelectedDate='<%#DataBinder.Eval(Container.DataItem, "DateEnd")%>' Enabled="false"></ew:CalendarPopup>
	            </ItemTemplate>   

<HeaderStyle CssClass="GridColumns"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRows" Width="30%"></ItemStyle>
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
         <asp:Label runat="server" ID="lblNoOBSchedule" Text="[No Journal Weeks added]" 
                Font-Size="Small"></asp:Label>
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

