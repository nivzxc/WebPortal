<%@ Page Language="C#" AutoEventWireup="true" CodeFile="clsGLAccountChecking.aspx.cs" Inherits="CIS_MRCF_clsGLAccountChecking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
 
 
  <tr><td style="height:9px;"></td></tr>
      <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
            
     <div class="GridBorder">
                          <table width="100%" cellpadding="3" class="grid">
                           <tr>
                            <td class="GridColumns"><b>Checking of GL Account For Services</b>
                            </td>
                           </tr>
                           <tr>
                            <td class="GridRows">
                             <table width="100%" cellpadding="5" cellspacing="1">
                            <tr>
                            <td><b>TRANSACTION NAME</b></td>
                             <td><b>DIVICODE</b></td>
                             <td><b>DEPTCODE #</b></td>
                             <td><b>DEPTNAME</b></td>
                             <td><b>GL Account</b></td>
                             <td><b>Exist In ORACLE</b></td>
                            </tr>
                                 <asp:Label ID="lblEMps" runat="server" Text="Label"></asp:Label>
                           </table>      
        </td>
       </tr>
      </table>

      <br />
      <br />
                                <table width="100%" cellpadding="3" class="grid">
                           <tr>
                            <td class="GridColumns"><b>Checking of GL Account For GOODS and SUPPLIES</b>
                            </td>
                           </tr>
                           <tr>
                            <td class="GridRows">
                             <table width="100%" cellpadding="5" cellspacing="1">
                            <tr>
                             <td><b>Category</b></td>
                             <td><b>Sub Category</b></td>
                             <td><b>GL Account</b></td>
                             <td><b>Exist In ORACLE</b></td>
                            </tr>
                                 <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                           </table>      
        </td>
       </tr>
      </table>
     </div>
    </div>
   </td>
  </tr>  

  
 </table>
    </div>
    </form>
</body>
</html>
