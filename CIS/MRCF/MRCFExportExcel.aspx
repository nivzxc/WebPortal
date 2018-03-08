<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MRCFExportExcel.aspx.cs" Inherits="CIS_MRCF_MRCFExportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
     <div class="GridBorder">
      <table width="100%" cellpadding="5" cellspacing="1">
       <tr><td colspan="8" class="GridText" style="text-align:center;"><b>MRCF Report 
</b></td></tr>
       <tr>
        <td class="GridText" style="width:6%;font-size:x-small;text-align:center;"><b>Batch</b></td>
        <td class="GridText" style="width:6%;font-size:x-small;text-align:center;"><b>MRCF #</b></td>
        <td class="GridText" style="width:15%;font-size:x-small;text-align:center;"><b>Intended For</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Requestor</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Date Requested</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Assigned To</b></td>
        <td class="GridText" style="width:9%;font-size:x-small;text-align:center;"><b>Status</b></td>
        <td class="GridText" style="width:10%;font-size:x-small;text-align:center;"><b>Date Assigned</b></td>
       </tr>
       <% LoadRecords(); %>
      </table>
     </div>   
     <br />
        </form>
</body>
</html>
