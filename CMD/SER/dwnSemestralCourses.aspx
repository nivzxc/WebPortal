<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dwnSemestralCourses.aspx.cs" Inherits="CMD_SER_dwnSemestralCourses" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
  <title>Semestral Courses Report</title>
</head>
<body>
    <form id="frmReport" runat="server">
      <table border="1">
       <tr>
        <td rowspan="3">&nbsp;</td>
        <td rowspan="3"><b>NS</b></td>
        <td rowspan="3"><b>OS</b></td>
        <td rowspan="3"><b>Total OGS</b></td>
        <td colspan="26"><b>CHED Programs</b></td>
        <td colspan="38"><b>TESDA Programs</b></td>
       </tr>
       <tr>
        <td colspan="2"><b>BSCOE</b></td>
        <td colspan="2"><b>BSECE</b></td>
        <td colspan="2"><b>BSIT</b></td>
        <td colspan="2"><b>BSCS</b></td>
        <td colspan="2"><b>ACT</b></td>
        <td colspan="2"><b>BSHRM</b></td>
        <td colspan="2"><b>BSBA</b></td>
        <td colspan="2"><b>BSENTREP</b></td>
        <td colspan="2"><b>BSOA</b></td>
        <td colspan="2"><b>AOM</b></td>
        <td colspan="2"><b>BSED</b></td>
        <td colspan="2"><b>BSN</b></td>
        <td colspan="2"><b>MIT</b></td>
        <td colspan="2"><b>DCET</b></td>        
        <td colspan="2"><b>DIT</b></td>
        <td colspan="2"><b>DMA</b></td>
        <td colspan="2"><b>HRA</b></td>
        <td colspan="2"><b>HRS</b></td>
        <td colspan="2"><b>DENTREP</b></td>
        <td colspan="2"><b>DOSM</b></td>
        <td colspan="2"><b>PNP</b></td>
        <td colspan="2"><b>CNAP</b></td>
        <td colspan="2"><b>DPN</b></td>
        <td colspan="2"><b>IHC</b></td>
        <td colspan="2"><b>DHCS</b></td>
        <td colspan="2"><b>DHNA</b></td>
        <td colspan="2"><b>DAIT</b></td>
        <td colspan="2"><b>DCBB</b></td>        
        <td colspan="2"><b>CYPROG</b></td>
        <td colspan="2"><b>DEP</b></td>
        <td colspan="2"><b>HCS</b></td>
        <td colspan="2"><b>BASS</b></td>        
       </tr>       
       <tr>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>        
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td> 
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>        
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>        
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>        
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>               
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>
        <td><b>S1</b></td>
        <td><b>S2</b></td>    
        <td><b>S1</b></td>
        <td><b>S2</b></td>            
       </tr>
       <% LoadRecords(); %>
      </table>
    </form>
</body>
</html>
