<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="MRCFAllAssign.aspx.cs" Inherits="CIS_MRCF_MRCFAllAssign" %>
<%@ Import Namespace="STIeForms" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 
 <script language="javascript" type="text/javascript">
     function ModalPop(clicked_id) {
         document.getElementById("<%=inpHide.ClientID%>").value = clicked_id;
        document.getElementById("<%=btnMPopup.ClientID%>").click();
     }
 </script>

<cc1:ToolkitScriptManager ID='ToolkitScriptManager1' runat='server'> </cc1:ToolkitScriptManager>
   <cc1:ModalPopupExtender ID='ModalPopupExtender1' runat='server'
    PopupControlID='divModal' TargetControlID='label1'  BackgroundCssClass='modalBackgroundMRCF'> </cc1:ModalPopupExtender>
 
     
      <div id='divModal' runat='server' class='modalPopUpMRCFHistory' style="display:none; padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;"> 
               <center><asp:LinkButton ID="lbtnHide" runat="server" onclick="lbtnHide_Click">Close</asp:LinkButton></center> 
               <b><span class='HeaderText'>MRCF Project History</span></b><br /> 
    <asp:Label ID="Label1" runat="server" style='display:inherit' ></asp:Label>
   </div>
 
    <div class='border' style='padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;'>
      <b><span class='HeaderText'>Assigned MRCF</span></b><br />
        <input id="inpHide" type="text" runat="server" style="display:none;"/>   
        
      
        <asp:Button ID="btnMPopup" runat="server" BackColor="White" BorderStyle="None"  style="Display:none;"
            Height="10px" onclick="btnMPopup_Click" Text="Button" Width="10px" />
      
      <br />
        <table width="100%" cellpadding="5" class="grid">
               <tr id="trEmployee" runat="server">
                <td class="GridRows" style="width:25%;">Employee List:</td>
                <td class="GridRows" style="width: 479px">
                 <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="controls" 
                        BackColor="white" 
                        >
                 </asp:DropDownList>
                    </td>
                   </tr>
               <tr>
                <td class="GridRows" style="width:25%;">Request Status:</td>
                <td class="GridRows" style="width: 479px">
                 <asp:DropDownList runat="server" ID="ddlAssignStatus" CssClass="controls" 
                        BackColor="white" 
                        >
                 </asp:DropDownList>
                 </td>
                  </tr>
 <tr>                  <td colspan="2" style="text-align:center;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" 
                        onclick="btnSearch_Click"/>
                    </td></tr>      
              </table>
              
              <div class="GridBorder">
      <table width="100%" cellpadding="0" class="Grid">
       <tr>
           <td cellpadding="0" cellspacing="0" colspan="3" align="center" class="GridText" 
               rowspan="0">
           
           
            <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td style="vertical-align:middle;" class="style1">
          &nbsp;<b>List of Assigned MRCF</b>    
           </td>
           <td style="text-align:right; vertical-align:middle;">
               <asp:ImageButton runat="server" ID="btnExport" 
                   ImageUrl="~/Support/btnExportToExcel.jpg" onclick="btnExport_Click"  /></td>
          </tr>
         </table>

       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       
         <asp:Label ID="lblSearch" runat="server" Text=""></asp:Label>

       </table>
     </div>
            </div>  
  

</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="cphHead">
    <style type="text/css">
        .style1
        {
            width: 281px;
        }
    </style>
</asp:Content>


