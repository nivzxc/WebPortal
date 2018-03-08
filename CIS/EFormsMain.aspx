<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="EFormsMain.aspx.cs" Inherits="CIS_EFormsMain" %>

<asp:Content ID="conOBMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="EFormsMain.aspx" class="SiteMap">E-Forms</a> » 
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">E-Forms Index</span></b>
     <br />
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr>
        <td class="GridColumns" style="width:15%;"><b>Department</b></td>
        <td class="GridColumns" style="width:40%;"><b>E-Forms</b></td>
        <td class="GridColumns" style="width:25%;"><b>Date Implemented</b></td>
        <td class="GridColumns" style="width:20%;"><b>Records</b></td>
       </tr>
       <tr>
        <td class="GridRows">Procurement</td>
        <td class="GridRows"><a href="MRCF/MRCFMenu.aspx">Material Request Canvass Form</a></td>
        <td class="GridRows" style="text-align:center;">January 2008</td>
        <td class="GridRows" style="text-align:center;"><asp:Label runat="server" ID="lblMRCFCount"></asp:Label></td>
       </tr>
       <tr>
        <td class="GridRows">AMLS</td>
        <td class="GridRows"><a href="MRCF/MRCFMenu.aspx">Requisition Form</a></td>
        <td class="GridRows" style="text-align:center;">January 2008</td>
        <td class="GridRows" style="text-align:center;"><asp:Label runat="server" ID="lblRequCount"></asp:Label></td>
       </tr>
       <tr>
        <td class="GridRows">AMLS</td>
        <td class="GridRows"><a href="Transmittal/TranMenu.aspx">Transmittal Request Form</a></td>
        <td class="GridRows" style="text-align:center;">January 2008</td>
        <td class="GridRows" style="text-align:center;"><asp:Label runat="server" ID="lblTranCount"></asp:Label></td>
       </tr>       
       <tr>
        <td class="GridRows">HR</td>
        <td class="GridRows"><a href="../HR/HRMS/Leave/LeaveMenu.aspx">Application For Leave</a></td>
        <td class="GridRows" style="text-align:center;">March 2009</td>
        <td class="GridRows" style="text-align:center;"><asp:Label runat="server" ID="lblLeaveCount"></asp:Label></td>
       </tr>
       <tr>
        <td class="GridRows">HR</td>
        <td class="GridRows"><a href="../HR/HRMS/Undertime/UndertimeMenu.aspx">Application For Undertime</a></td>
        <td class="GridRows" style="text-align:center;">March 2009</td>
        <td class="GridRows" style="text-align:center;"><asp:Label runat="server" ID="lblUnderCount"></asp:Label></td>
       </tr>
       <tr>
        <td class="GridRows">HR</td>
        <td class="GridRows"><a href="../HR/HRMS/OB/OBMenu.aspx">Application For OB</a></td>
        <td class="GridRows" style="text-align:center;">March 2009</td>
        <td class="GridRows" style="text-align:center;"><asp:Label runat="server" ID="lblOBCount"></asp:Label></td>
       </tr>
       <tr>
        <td class="GridRows">HR</td>
        <td class="GridRows"><a href="../HR/HRMS/Overtime/OvertimeMenu.aspx">Application For Overtime</a></td>
        <td class="GridRows" style="text-align:center;">March 2009</td>
        <td class="GridRows" style="text-align:center;"><asp:Label runat="server" ID="lblOverCount"></asp:Label></td>
       </tr>                     
      </table>
     </div>
    </div>     
   </td>
  </tr>     
 </table>  
</asp:Content>