<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRSDispatchDetailsCC.aspx.cs" Inherits="CMD_CRS_CRSDispatchDetailsCC" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head runat="server">
  <title>Courseware Request System: Dispatch Details</title>
  <link rel="Stylesheet" type="text/css" href="../../MySTIHQ.css" />
 </head>
 <body style="background-image:url(../../Support/back.GIF);">
  <form id="frmCRSDispatchDetailsCC" runat="server">
   <div>  
    <div class="GridBorder">
     <table width="100%" cellpadding="3" class="grid">
      <tr>
       <td colspan="2" class="GridText">
        <table>
         <tr>
          <td>&nbsp;<img src="../../Support/viewtext22.png" alt="" /></td>
          <td>&nbsp;<b>Courseware Material Details</b></td>
         </tr>
        </table>         
       </td>
      </tr>      
      <tr>
       <td class="GridRows" style="width:20%">Course:</td>
       <td class="GridRows" style="width:80%">
        <asp:TextBox runat="server" ID="txtCrseCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        -
        <asp:TextBox runat="server" ID="txtCrseTtle" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
       </td>
      </tr>
      <tr>
       <td class="GridRows" style="width:20%">Year & Term:</td>
       <td class="GridRows" style="width:80%">
        <asp:TextBox runat="server" ID="txtYearTerm" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
        &nbsp;
        Availability:        
        <asp:TextBox runat="server" ID="txtAvailability" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
       </td>
      </tr>
      <tr>
       <td class="GridRows" style="width:20%">Status:</td>
       <td class="GridRows" style="width:80%">       
        <asp:HiddenField runat="server" ID="hdnPStatus" />
        <asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>        
        &nbsp;
        No. of Request:
        <asp:TextBox runat="server" ID="txtNoReq" CssClass="controls" Width="50px" ReadOnly="true" BackColor="mistyrose"></asp:TextBox>
       </td>
      </tr>
     </table>
    </div>
    
    <br />

    <div class="GridBorder">
     <table width="100%" cellpadding="0" class="grid">
      <tr>
       <td align="center" class="GridText" style="text-align:left;font-size:small;">
        <table>
         <tr>
          <td>&nbsp;<img src="../../Support/AppHead.png" alt="" /></td>
          <td>&nbsp;<b>Courseware Dispatch List</b></td>
         </tr>
        </table>           
       </td>
      </tr>
      <tr>
       <td>
        <div class="GridBorder">
         <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnCancelCommand="dgItems_CancelCommand" OnEditCommand="dgItems_EditCommand" OnUpdateCommand="dgItems_UpdateCommand">
          <Columns>
           <asp:TemplateColumn HeaderText="Dispatch Details" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>             
             <asp:Label runat="server" ID="lblDispType" Text='<%#clsCRS.ToDispatchTypeDesc(DataBinder.Eval(Container.DataItem, "disptype").ToString())%>'></asp:Label>
             <br />
             <asp:Label runat="server" ID="lblDispDeta" Text='<%#DataBinder.Eval(Container.DataItem, "dispdeta")%>'></asp:Label>
             <br />
             Date Dispatched: <asp:Label runat="server" ID="lblDateDisp" Text='<%#DataBinder.Eval(Container.DataItem, "datedisp")%>'></asp:Label>
            </ItemTemplate>
           </asp:TemplateColumn>

           <asp:TemplateColumn HeaderText="Receiving Details" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%">
            <ItemTemplate>
             <table>
              <tr>
               <td style="color:#4169e1">Recieved by:</td>
               <td><asp:Label runat="server" ID="lblRecBy" Text='<%#DataBinder.Eval(Container.DataItem, "recby")%>'></asp:Label></td>
              </tr>
              <tr>
               <td style="color:#4169e1">Date:</td>
               <td><asp:Label runat="server" ID="lblRecDate" Text='<%#DataBinder.Eval(Container.DataItem, "recdate") %>'></asp:Label></td>
              </tr>              
             </table>
            </ItemTemplate>    
            <EditItemTemplate>
             <asp:HiddenField runat="server" ID="hdnDateEntry" Value='<%#DataBinder.Eval(Container.DataItem, "datentry") %>' />
             <table>
              <tr>
               <td style="color:#4169e1">Recieved by:</td>
               <td><asp:TextBox runat="server" ID="txtRecBy" Text='<%#DataBinder.Eval(Container.DataItem, "recby")%>' CssClass="controls" BackColor="white" Width="150px" MaxLength="30"></asp:TextBox></td>
              </tr>
              <tr>
               <td style="color:#4169e1">Date:</td>
               <td><cc1:GMDatePicker ID="dteRecDate" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
              </tr>              
             </table>
            </EditItemTemplate>                    
           </asp:TemplateColumn>                     	           
           
           <asp:EditCommandColumn ButtonType="LinkButton" EditText="[Edit]" CancelText="[Cancel]" UpdateText="[Update]" ItemStyle-CssClass="GridRows" HeaderStyle-CssClass="GridColumns" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center"></asp:EditCommandColumn>
                                 
          </Columns>
         </asp:DataGrid>
        </div>
       </td>
      </tr>
     </table>
    </div>
    
    <br />
    <div class="GridBorder" runat="server" id="divAddDispatch">
     <table width="100%" cellpadding="3" class="grid"> 
      <tr>
       <td colspan="2" class="GridText">
        <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
          <td>
           <table>
            <tr>
             <td>&nbsp;<img src="../../Support/additem22.png" alt="Requested Items" /></td>
             <td>&nbsp;<b>Dispatch Details</b></td>
            </tr>
           </table>            
          </td>
          <td style="text-align:right;"><asp:ImageButton runat="server" ID="btnAdd" ImageUrl="~/Support/btnAddItem.jpg" ValidationGroup="additem" OnClick="btnAdd_Click" /></td>
         </tr>
        </table>            
       </td>
      </tr>
      <tr>
       <td class="GridRows" style="width:20%;">Dispatch Type:</td>
       <td class="GridRows" style="width:80%;">
        <asp:DropDownList ID="ddlDispType" runat="server" CssClass="controls" ValidationGroup="additem" BackColor="white" >
         <asp:ListItem Text="Partial Dispatch" Value="P"></asp:ListItem>
         <asp:ListItem Text="Complete Dispatch" Value="C"></asp:ListItem>
        </asp:DropDownList>       
        
        <asp:CheckBox runat="server" ID="chkDispatched" Text="Dispatched?" AutoPostBack="true" OnCheckedChanged="chkDispatched_CheckedChanged" />
        <asp:CheckBox runat="server" ID="chkReceived" Text="Received?" AutoPostBack="true" OnCheckedChanged="chkReceived_CheckedChanged" />
       </td>
      </tr>      
      <tr>
       <td class="GridRows" style="vertical-align:top;">Dispatch Details:</td>
       <td class="GridRows">
        <asp:TextBox runat="server" ID="txtDispDet" CssClass="controls" TextMode="MultiLine" Rows="3" Width="98%" BackColor="white" ValidationGroup="additem"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ValidationGroup="additem" ID="reqDispDet" ControlToValidate="txtDispDet" ErrorMessage="<br>[Required]" Display="dynamic"></asp:RequiredFieldValidator>                  
       </td>
      </tr>   
      <tr runat="server" id="trDispatch">
       <td class="GridRows">Date Dispatch:</td>
       <td class="GridRows"><cc1:GMDatePicker ID="dteDateDisp" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
      </tr>
      <tr runat="server" id="trReceived">
       <td class="GridRows">Received by:</td>
       <td class="GridRows">
        <asp:TextBox runat="server" ID="txtRecBy" CssClass="controls" Width="200px" BackColor="white"></asp:TextBox>
        &nbsp;
        &nbsp;
        Date Received:        
        <cc1:GMDatePicker ID="dteDateRec" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker>
       </td>
      </tr>      
     </table>
    </div>
    <br />
    <div style="width:100%;text-align:center;">
     <asp:ImageButton runat="server" ID="btnClose" ImageUrl="~/Support/btnCloseWindow.jpg" OnClick="btnClose_Click" />     
    </div>    
   </div>  
  </form>
 </body>
</html>