<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="TranNew.aspx.cs" Inherits="CIS_Transmittal_TranNew" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 
 <script language="javascript" type="text/javascript">
  function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal)
  {
   for(i = 0; i < document.forms[0].elements.length; i++)
   {
    elm = document.forms[0].elements[i];   
    if (elm.type == 'checkbox')
    {     
     if (elm.name.indexOf(aspCheckBoxID)>= 0)    
      elm.checked = checkVal;
    }
   }
  }
 </script>

      <script language="javascript" type="text/javascript">
          var submit = 0;

          function CheckIsRepeat() {
              if (++submit > 1) {
                  alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                  return false;
              }
          }
    </script>
 
 <table width="648px" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="TranMenu.aspx" class="SiteMap">Transmittal</a> » 
     <a href="TranNew.aspx" class="SiteMap">New Transmittal Request</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding: 10px 0px 10px 10px; width:628px">
     <b><span class="HeaderText">Create New Transmittal Request</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="648px" cellpadding="5" class="grid">
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Transmittal Request Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">RC:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRCName" CssClass="controls" Width="350px" ReadOnly="true"></asp:TextBox>         
         <asp:HiddenField runat="server" ID="hdnRCCode" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:middle;">Dispatch Type:&nbsp;&nbsp;</td>
        <td class="GridRows">
         <asp:RadioButton runat="server" ID="radRegular" Text="Regular" GroupName="TranType" AutoPostBack="true" OnCheckedChanged="radRegular_CheckedChanged" />
         &nbsp;
         <asp:RadioButton runat="server" ID="radSpecialHQ" Text="Special (Charge to HQ)" GroupName="TranType" AutoPostBack="true" OnCheckedChanged="radSpecialHQ_CheckedChanged" />
         &nbsp;
         <asp:RadioButton runat="server" ID="radSpecialSchool" Text="Special (Charge to Schools)" GroupName="TranType" AutoPostBack="true" OnCheckedChanged="radSpecialSchool_CheckedChanged" />
        </td>
       </tr>
       <tr runat="server" id="trDateNeeded">
        <td class="GridRows">Date Needed:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpDateNeeded" runat="server" CssClass="controls" CalendarTheme="blue" DisplayMode="Label" BackColor="white" DateFormat="MMMM dd, yyyy"></cc1:GMDatePicker></td>
       </tr>              
       <tr runat="server" id="trChargeTo">
        <td class="GridRows">Charge To:</td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlChargeTo" CssClass="controls" AutoPostBack="true" BackColor="white" OnSelectedIndexChanged="ddlChargeTo_SelectedIndexChanged"></asp:DropDownList></td>
       </tr>
       <tr runat="server" id="trGrpHead">
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:DropDownList runat="server" ID="ddlGrpHead" CssClass="controls" BackColor="white"></asp:DropDownList></td>
       </tr>       
       <tr>
        <td class="GridRows" colspan="2">
         <table>
          <tr>
           <td class="masterpanel" style="width: 266px"><b>STI School List</b></td>
           <td rowspan="4">&nbsp;</td>
           <td class="masterpanel" style="width: 273px"><b>Selected Schools</b></td>
          </tr>             
          <tr>
           <td class="style8"><asp:DropDownList runat="server" ID="ddlSchlGroup" Width="240px" CssClass="controls" AutoPostBack="true" OnSelectedIndexChanged="ddlSchlGroup_SelectedIndexChanged" BackColor="white" Font-Size="small"></asp:DropDownList></td>
           <td style="color: #3399cc; font-size:small;" class="style8"><i>&nbsp;Schools that will receive the item.</i></td>
          </tr>
          <tr>
           <td class="style1">
            <div class="controls" style="width: 250px; height: 250px;	overflow: auto;">
             <table>
              <tr>
               <td class="style9"><input id="chkAll" type="checkbox" onclick="CheckAllDataGridCheckBoxes('cblSchools',this.checked)" /></td>
               <td>SELECT ALL</td>
              </tr>
             </table>
             <asp:CheckBoxList ID="cblSchools" runat="server" RepeatLayout="Table" 
                    Font-Size="x-Small" BackColor="white"></asp:CheckBoxList>            
            </div>              
           </td>
           <td class="style8">
            <div class="controls" style="width: 250px; height: 250px;	overflow: auto;">
             <table>
              <tr>
               <td class="style6"><input id="chkAllInc" type="checkbox" onclick="CheckAllDataGridCheckBoxes('cblIncluded',this.checked)" /></td>
               <td class="style10">SELECT ALL</td>
              </tr>
             </table>
             <asp:CheckBoxList ID="cblIncluded" runat="server" RepeatLayout="Table" 
                    Font-Size="x-Small" BackColor="white"></asp:CheckBoxList>
             <asp:HiddenField runat="server" ID="hdnSelSchl" />
            </div>                 
           </td>              
          </tr>
          <tr>
           <td align="center" class="style1"><asp:Button ID="btnInclude" runat="server" Text="Include"  OnClick="btnInclude_Click" /><%--<asp:ImageButton runat="server" ID="btnInclude" ImageUrl="~/Support/btnInclude.jpg" OnClick="btnInclude_Click" />--%></td>
           <td align="center" class="style8"><asp:Button ID="btnRemove" runat="server" Text="Exclude"  OnClick="btnRemove_Click" /><%--<asp:ImageButton runat="server" ID="btnRemove" ImageUrl="~/Support/btnExclude.jpg" OnClick="btnRemove_Click" />--%></td>
          </tr>
         </table>
        </td>
       </tr>     
       <tr>
        <td class="GridRows">Item Description:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtItem" CssClass="controls" Width="98%" BackColor="white" ValidationGroup="TranNew" MaxLength="100"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItem" ID="vldItem" ErrorMessage="<br>[Item Description Required]" Display="Dynamic" ValidationGroup="TranNew"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Quantity:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtQty" CssClass="controls" MaxLength="6" Width="60px" BackColor="white" ValidationGroup="TranNew"></asp:TextBox>
         <asp:RangeValidator ID="vldQty" runat="server" ControlToValidate="txtQty" ErrorMessage="<br>[Invalid Quantity]" Type="Integer" MaximumValue="9999" MinimumValue="0" Display="Dynamic" ValidationGroup="TranNew"></asp:RangeValidator>
         <asp:RequiredFieldValidator runat="server" ID="vldQty2" ControlToValidate="txtQty" ErrorMessage="<br>[Quantity Required]" Display="dynamic" ValidationGroup="TranNew"></asp:RequiredFieldValidator>         
         Item Unit:
         <asp:DropDownList runat="server" ID="ddlUnit" CssClass="controls" BackColor="white">
          <asp:ListItem Value="Box" Text="Box"></asp:ListItem>
          <asp:ListItem Value="CM" Text="CM"></asp:ListItem>
          <asp:ListItem Value="Container" Text="Container"></asp:ListItem>
          <asp:ListItem Value="DM" Text="DM"></asp:ListItem>          
          <asp:ListItem Value="L. brown env" Text="L. brown env"></asp:ListItem>
          <asp:ListItem Value="Letter" Text="Letter"></asp:ListItem>
          <asp:ListItem Value="Letter Envelop" Text="Letter Envelop"></asp:ListItem>
          <asp:ListItem Value="MRRP" Text="MRRP"></asp:ListItem>
          <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
          <asp:ListItem Value="pc" Text="pc"></asp:ListItem>
          <asp:ListItem Value="S. brown env" Text="S. brown env"></asp:ListItem>
          <asp:ListItem Value="S/A" Text="S/A"></asp:ListItem>          
             <asp:ListItem>CPDT package/s</asp:ListItem>
         </asp:DropDownList>         
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" BackColor="white"></asp:TextBox></td>
       </tr>
      </table>
     </div>         
     <br />
     <div style="text-align:center; width:100%">
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSend.jpg" OnClick="btnSave_Click" ValidationGroup="TranNew" />--%>
         <asp:Button ID="btnSave" runat="server" Text="Submit"  OnClick="btnSave_Click" ValidationGroup="TranNew"/>
     </div>     
    </div>       
   </td>
  </tr> 
 </table>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphHead">
 
    <style type="text/css">
        .style1
        {
            width: 266px;
        }
        .style6
        {
            width: 13px;
        }
        .style8
        {
            width: 273px;
        }
        .style9
        {
            width: 3px;
        }
        .style10
        {
            width: 210px;
        }
    </style>
 
</asp:Content>
