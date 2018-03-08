<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountBudgetMaintenanceExcel.aspx.cs" Inherits="BudgetManagement_AccountBudgetMaintenanceExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
        <b><span class="HeaderText">
        List of Account Item</span></b>
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
                           <tr>
                            <td class="GridRows">
                             <table width="100%" cellpadding="5" cellspacing="1">
                               <tr>
                                 <td class="masterpanel" style="width:13%;">Oracle Code</td>
                                 <td class="masterpanel" style="width:42%;"><b>Account Item</b></td>
                                 <td class="masterpanel" style="width:15%;"><b>Budget</b></td>
                                 <td class="masterpanel" style="width:15%;"><b>Actual</b></td>
                                 <td class="masterpanel" style="width:15%;"><b>Variant</b></td>
                                </tr>
                                 <asp:Label ID="lblItems" runat="server" Text="" Visible="True"></asp:Label>
                           </table>      
        </td>
       </tr>
      </table>
     </div>
    </div>
   </td>
  </tr>  

  
 </table>
</body>
</html>
