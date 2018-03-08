<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="MRCFAirlines.aspx.cs" Inherits="CIS_MRCF_MRCFAirfare" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <cc1:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">
    </cc1:toolkitscriptmanager>

<script language="JavaScript" type="text/javascript">
        function winpop(url, w, h, scroll, resize, center) {
            if (center) {
                var winPos = ',top=' + ((screen.height - h) / 2) + ',left=' + ((screen.width - w) / 2);
            }
            var scrollArg = (scroll == false) ? '' : ',scrollbars=1';
            var resizeArg = (resize == false) ? '' : ',resizable=1';
            flyout = window.open(url, "newin" + scroll + resize + center, "width=" + w + ",height=" + h + scrollArg + resizeArg + winPos);
            flyout.resizeTo(w, h);
            flyout.focus();
        }
       
       function ModalPop(clicked_id) {
        document.getElementById("<%=inpHide.ClientID%>").value = clicked_id;
        document.getElementById("<%=btnMPopup.ClientID%>").click();
    }

    function UpdateStatus(clicked_id) {
        document.getElementById("<%=inpHide.ClientID%>").value = clicked_id;
        document.getElementById("<%=btnUpdateStatus.ClientID%>").click();
    }


</script>

      <asp:Button ID="btnMPopup" runat="server" BackColor="White" BorderStyle="None"  style="Display:none;"
            Height="10px" onclick="btnMPopup_Click" Text="Button" Width="10px" />

                  <asp:Button ID="btnUpdateStatus" runat="server" BackColor="White" BorderStyle="None"  style="Display:none;"
            Height="10px" onclick="btnUpdateStatus_Click" Text="Button" Width="10px" />


            <input id="inpHide" type="text" runat="server" style="display:none;"/>   
            <cc1:modalpopupextender ID='mdlpopupAdd' runat='server' PopupControlID='divModalAdd' TargetControlID='label1'  
        BackgroundCssClass='modalBackgroundMRCF'> </cc1:modalpopupextender>

        <cc1:modalpopupextender ID='mdlpopupEdit' runat='server' PopupControlID='divModalEdit' TargetControlID='label1'  
        BackgroundCssClass='modalBackgroundMRCF'> </cc1:modalpopupextender>


    <div id='divModalAdd' runat='server' class='modalPopUpAirlines' style="display:block; padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;"> 
               <center><asp:LinkButton ID="lnkbtnHideAdd" runat="server" 
                       onclick="lnkbtnHideAdd_Click">Close</asp:LinkButton></center> 
               <b><span class='HeaderText'>Add Airline</span></b><br /> <br />
    <table width="100%">
    <tr>
    <td class="GridRows" style="width:30%;">Airline Name:</td>
    <td class="GridRows" style="width:70%;"><asp:TextBox runat="server" ID="txtAirlineName" CssClass="controls" Width="100%" MaxLength="100" BackColor="white" ValidationGroup="save"></asp:TextBox></td>
    </tr>
    <tr>
    <td class="GridRows" style="width:30%;">URL:</td>
    <td class="GridRows" style="width:70%;"><asp:TextBox runat="server" ID="txtURL" 
            CssClass="controls" Height="50px" Width="100%" MaxLength="250" BackColor="white" 
            ValidationGroup="save" TextMode="MultiLine" 
            ontextchanged="txtURL_TextChanged"></asp:TextBox></td>
    </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" 
            onclick="btnSave_Click"/></center><asp:Label ID="Label1" runat="server" style='display:inherit' ></asp:Label>
    
   </div>

      <div id='divModalEdit' runat='server' class='modalPopUpAirlinesEdit' style="display:block; padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;"> 
               <center><asp:LinkButton ID="lnkbtnHideEdit" runat="server" 
                       onclick="lnkbtnHideEdit_Click" >Close</asp:LinkButton></center> 
               <b><span class='HeaderText'>Edit New Airline</span></b><br /> <br />
    <table width="100%">
    <tr>
    <td class="GridRows" style="width:30%;">Airline Name:</td>
    <td class="GridRows" style="width:70%;"><asp:TextBox runat="server" 
            ID="txtEditAirlName" CssClass="controls" Width="100%" MaxLength="100" 
            BackColor="white" ValidationGroup="save"></asp:TextBox></td>
    </tr>
    <tr>
    <td class="GridRows" style="width:30%;">URL:</td>
    <td class="GridRows" style="width:70%;">
        <asp:TextBox runat="server" ID="txtEditURL" 
            CssClass="controls" Height="50px" Width="100%" MaxLength="250" BackColor="white" 
            ValidationGroup="save" TextMode="MultiLine" 
            ontextchanged="txtURL_TextChanged"></asp:TextBox></td>
    </tr>
     <tr>
    <td class="GridRows" colspan="2" style="width:100%; text-align:right;">
    
        <asp:CheckBox ID="ckbAirlines" runat="server" Text="Active" />
         </td>
    </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="btnEditSave" runat="server" Text="Save" Width="70px" 
            onclick="btnEditSave_Click"/></center><asp:Label ID="Label2" runat="server" style='display:inherit' ></asp:Label>
    
   </div>


<div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
<table width = "100%">
<tr>
        <td style="width: 10%"><asp:Image ID="Image2" runat="server" ImageUrl="~/Support/Airfare.png" /></td>
        <td style="width: 90%; vertical-align:middle; font-size:medium;"><b>
            <span class="HeaderText">Airline Reference Settings</span></b></td>
</tr>

</table>
<asp:Button ID="btnSaveAdd" runat="server" Text="Add New Airline" 
        onclick="btnSaveAdd_Click" /> 
 <br /> <br />

       <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:30%;"><b>Airline Name</b></td>
        <td class="GridColumns" style="width:5%; text-align:center;" >Active<b></b></td>
       </tr>
       <% LoadAirlines(); %>
       </table>



</div>
</asp:Content>

