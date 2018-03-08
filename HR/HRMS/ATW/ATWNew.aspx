<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ATWNew.aspx.cs" Inherits="HR_HRMS_ATW_ATWNew" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntATWNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
    <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
               <script language="javascript" type="text/javascript">
                   var submit = 0;

                   function CheckIsRepeat() {
                       if (++submit > 1) {
                           alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                           return false;
                       }
                   }
    </script>
 <table width="100%" cellpadding="0" cellspacing="0"> 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="ATWMenu.aspx" class="SiteMap">ATW</a> » 
     <a href="ATWNew.aspx" class="SiteMap">ATW New</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">Create New Authority to Work Application</span></b>
     <br />
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
   <%--    <tr><td colspan="2" class="GridText">&nbsp;<b>Authority to Work Details</b></td></tr>--%>
       <tr>
        <td class="GridRows" style="width:25%;">Requestor:</td>
        <td class="GridRows" style="width:75%;"><asp:Label ID="lblRequestorName" runat="server" Text="Label"></asp:Label><%--<asp:TextBox runat="server" ID="txtRequestorName" CssClass="controlsLabel" Width="250px" ReadOnly="true"></asp:TextBox>--%></td>
       </tr>   
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="3" ValidationGroup="ATWNew"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqReason" 
                ErrorMessage="<br>[Reason is required]" Display="Dynamic" 
                ControlToValidate="txtReason" SetFocusOnError="true" ValidationGroup="ATWNew" 
                ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Department Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlApprover" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>
       <%--<tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:HiddenField ID="hdnApproverDivision" runat="server" />
         <asp:TextBox runat="server" ID="txtApproverDivision" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>  --%>
       <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>      
      </table>
     </div>
     
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns">&nbsp;<b>Add Schedule</b></td></tr>
       <tr>
        <td class="GridRows" style="text-align:right;">                 
         <div class="GridBorder">       
          <asp:UpdatePanel ID="upDetails" runat="server">
          <ContentTemplate>             
          <table width="100%" cellpadding="2">  
           <tr>
            <td class="GridColumns" style="width:30%"><b>Date Time Start</b></td>
            <td class="GridColumns" style="width:30%"><b>Date Time End</b></td>
            <td class="GridColumns" style="width:40%"><b>Reason</b></td>
           </tr>         
           <tr>           
            <td class="GridRows" style="text-align:center;"><cc1:GMDatePicker ID="dtpDateStart" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" CalendarTheme="Blue" EnableTimePicker="true"></cc1:GMDatePicker></td>
            <td class="GridRows" style="text-align:center;"><cc1:GMDatePicker ID="dtpDateEnd" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" CalendarTheme="Blue" EnableTimePicker="true"></cc1:GMDatePicker></td>
            <td class="GridRows" style="text-align:left;"><asp:TextBox runat="server" ID="txtScheduleReason" CssClass="controls" Width="90%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="3" ValidationGroup="ATWNew"></asp:TextBox></td>
           </tr>                                                                                                                         
          </table>
          </ContentTemplate>
          </asp:UpdatePanel>                     
         </div>  
         <br />       
         <%--<asp:ImageButton runat="server" ID="btnAddNewItem" ImageUrl="~/Support/btnAddSchedule.jpg" onclick="btnAddNewItem_Click" />--%>
            <asp:Button ID="btnAddNewItem" runat="server" Text="Add Schedule" onclick="btnAddNewItem_Click"/>
           </td>
	      </tr>
	     </table>
	    </div>
     
     <br />

     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <tr><td class="GridColumns">&nbsp;<b>Schedule Details</b></td></tr>       
       <tr>
        <td class="GridRows">
         <div class="GridBorder" runat="server" id="divScheduleList">
          <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="10px" BorderStyle="Solid" ondeletecommand="dgSchedule_DeleteCommand">
           <Columns>	        
            <asp:TemplateColumn HeaderText="Date Start" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="25%">
             <ItemTemplate>
              <cc1:GMDatePicker ID="dtpDateStartI" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" CalendarTheme="Blue" EnableTimePicker="true" Date='<%#DataBinder.Eval(Container.DataItem, "datestrt")%>'></cc1:GMDatePicker>
	            </ItemTemplate>
	           </asp:TemplateColumn>	           
	           <asp:TemplateColumn HeaderText="Date End" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="25%">
             <ItemTemplate>
              <cc1:GMDatePicker ID="dtpDateEndI" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MM/dd/yy" BackColor="white" CalendarTheme="Blue" EnableTimePicker="true" Date='<%#DataBinder.Eval(Container.DataItem, "dateend")%>'></cc1:GMDatePicker>
	            </ItemTemplate>
	           </asp:TemplateColumn>
	           <asp:TemplateColumn HeaderText="Reason" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="40%">
             <ItemTemplate>
              <asp:TextBox runat="server" ID="txtScheduleReasonI" CssClass="controls" Width="90%" MaxLength="255" BackColor="white" TextMode="MultiLine" Rows="3" ValidationGroup="ATWNew" Text='<%#DataBinder.Eval(Container.DataItem, "reason")%>'></asp:TextBox>
	            </ItemTemplate>
	           </asp:TemplateColumn>	           
            <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>
            </asp:TemplateColumn>           	           
           </Columns>
          </asp:DataGrid>
         </div>
         <asp:Label runat="server" ID="lblNoATWSchedule" Text="[No schedule added]" Font-Size="X-Small"></asp:Label>
        </td>
       </tr>
      </table>
     </div>

     <br />



     <div style="text-align:center;">
         <asp:Button ID="btnSend" runat="server" Text="Submit"  onclick="btnSend_Click" ValidationGroup="ATWNew"/>
      <%--<asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" ValidationGroup="ATWNew" onclick="btnSend_Click" />--%>
      &nbsp;
      <asp:Button ID="btnBack" runat="server" Text="Back" nclick="btnBack_Click" onclick="btnBack_Click"/>
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
     </div>     
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>