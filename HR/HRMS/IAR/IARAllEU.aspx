<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="IARAllEU.aspx.cs" Inherits="HR_HRMS_IAR_IARAllEU" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>
<asp:Content ID="conDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="IARMenu.aspx" class="SiteMap">Internet Access Request</a> » 
     <a href="IARAllEU.aspx" class="SiteMap">Approved Request</a>
    </div>        
   </td>
  </tr>  --%>   
<%--  <tr><td style="height:9px;"></td></tr>  --%> 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Approved Internet Access Request</span></b>
     <br />
     <br />
     <div class="GridBorder" style="width:60%;"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td class="GridRows" style="width:30%;">Date Start:</td>
        <td class="GridRows" style="width:40%;"><cc1:GMDatePicker ID="dtpDateStart" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
        <td class="GridRows" style="width:30%; text-align:center;" rowspan="2"><%--<asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" />--%>
            <asp:Button
                ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" /></td>
       </tr> 
       <tr>
        <td class="GridRows">Date End:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpDateEnd" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
       </tr> 
      </table>
     </div>
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5" cellspacing="1">
       <%--<tr>
        <td colspan="5" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Approved Internet Access Request</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:2%;">&nbsp;</td>
        <td class="GridColumns" style="width:27%;"><b>Requestor</b></td>
        <td class="GridColumns" style="width:18%;"><b>Date Start</b></td>
        <td class="GridColumns" style="width:18%;"><b>Date End</b></td>
        <td class="GridColumns" style="width:35%;"><b>Reason</b></td>
       </tr>
       <% LoadRecords();%>
      </table>           
     </div>
     <br />
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click"/>
     </div>     
    </div>     
   </td>
  </tr>   
 </table>
</asp:Content>