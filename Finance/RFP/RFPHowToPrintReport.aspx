<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RFPHowToPrintReport.aspx.cs" Inherits="Finance_RFP_RFPHowToPrintReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <link rel="Stylesheet" type="text/css" href="../../MySTIHQ.css" />  
  <title>The Official STI Head Office Website</title>
  <link rel="shortcut icon" href="../../Support/MySTIHQ.ico" type="image/x-icon" /> 
  <link rel="stylesheet" type="text/css" href="../../print.css" media="print" />
</head>

<body>
    <form id="form1" runat="server">
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;font-size:small;">    
     <b><span class="HeaderText">Printing Procedure for your CATA Report</span></b>
     <br />
     <br />
        <table width="100%" style="margin-left:50px;" class="border"> 
   <tr>
    <td style="width:10%" align="center"><img id="img1" alt="" src="../../Support/IE.png" /></td>
   <td colspan="2" height="50px" style=" vertical-align:middle"><b><i>
    <span class="HeaderText" style="font-size: medium">Internet Explorer</span></i></b></td>
   </tr>
   
   <tr>
    <td colspan="2" height="3px">&nbsp;</td>
   </tr>
   
   <tr>
    <td style="width:10%" align="right"><img id="img5" alt="" src="../../Support/pdficon.png" /></td>
    <td>&nbsp;&nbsp;<a href="../../Support/Files/CATA/Print_Using_IE.pdf">Printing Report</a></td>
   </tr>
   
    <tr>
    <td align="right"><img id="img11" alt="" src="../../Support/pdficon.png" /></td>
    <td>&nbsp;&nbsp;<a href="../../Support/Files/CATA/Cant_Print_Internet_Explorer.pdf">Printing Problem</a></td>
   </tr>
   
 <tr>
    <td colspan="2" height="3px">&nbsp;</td>
   </tr>
   
  <tr>
   <td style="width:10%" align="center"><img id="img2" alt="" src="../../Support/mozilla.png" /></td>
   <td colspan="2" height="50px" style=" vertical-align:middle"><b><i>
    <span class="HeaderText" style="font-size: medium">Mozilla Firefox</span></i></b></td>
   </tr>
   
   <tr>
    <td colspan="2" height="3px">&nbsp;</td>
   </tr>
   
   <tr>
    <td align="right"><img id="img16" alt="" src="../../Support/pdficon.png" /></td>
    <td>&nbsp;&nbsp;
     <a href="../../Support/Files/CATA/Print_Using_Mozilla.pdf">Printing Report</a></td>
   </tr>
   <tr>
    <td colspan="2" height="3px"></td>
   </tr>
      
  </table>
     <br />  
                 <div align="center">
     <asp:ImageButton ID="btnBack" runat="server" 
      ImageUrl="~/Support/btnClose.jpg" OnClick="btnBack_Click" />
    </div>
    </div>
    </form>
</body>
</html>
