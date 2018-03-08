<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyResult.aspx.cs" Inherits="Synergy_SurveyResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Synergy Survey Result</title>
     <link rel="Stylesheet" type="text/css" href="../MySTIHQ.css" />
      <link rel="shortcut icon" href="../Support/MySTIHQ.ico" type="image/x-icon" />
        <link rel="stylesheet" type="text/css" href="../print.css" media="print" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Synergy Survey Result</h2>
        <br />
        <table width="700px" border="1" cellpadding="1" cellspacing="1">
            <tr>
                <td class="GridText"  rowspan="2" style="text-align:center">#</td>
                <td style="text-align:center" class="GridText" rowspan="2" style="text-align:center">Event Name</td>
                <td style="text-align:center" class="GridText" colspan="2" style="text-align:center">Interested</td>
                <td style="text-align:center" class="GridText" rowspan="2" style="text-align:center">Total</td>
                <td style="text-align:center" class="GridText" colspan="2" style="text-align:center">Preferred</td>
                <td style="text-align:center" class="GridText" rowspan="2" style="text-align:center">Total</td>
            </tr>
            <tr>
                <td style="text-align:center" class="GridText">Male</td>
                <td style="text-align:center" class="GridText">Female</td>
                <td style="text-align:center" class="GridText">Male</td>
                <td style="text-align:center" class="GridText">Female</td>
            </tr>
            <asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>
        </table>
        <br />
        
            
    </div>

    <div>
    <br />
        <h2>Employee's suggested events</h2>
        <br />
        <table width="500px" border="1" cellpadding="1" cellspacing="1">
            <tr>
                <td style="text-align:center" class="GridText">Others</td>
            </tr>
            <asp:Label ID="lblOthers" runat="server" Text="Label"></asp:Label>
        </table>
        <br />
        
    </div>

    <div>

        <h2>Summary of Respondents</h2>
        <br />
         <table width="200px" border="1" cellpadding="1" cellspacing="1">
            <tr>
                <td style="text-align:center" class="GridText">Male</td>
                <td style="text-align:center" class="GridText">Female</td>
                <td style="text-align:center" class="GridText">Total</td>
            </tr>
            <asp:Label ID="lblRespondents" runat="server" Text="Label"></asp:Label>
        </table>
        <br />
        <asp:ImageButton runat="server" ID="btnExport" 
            ImageUrl="~/Support/btnExportToExcel.jpg" onclick="btnExport_Click" />
            <br />
            <br />
    </div>


     <div>
        <h2>Most Preferred Event</h2>
        <br />
        <table width="400px" border="1" cellpadding="1" cellspacing="1">
            <tr>
                <td class="GridText" style="text-align:center">#</td>
                <td style="text-align:center" class="GridText" style="text-align:center">Event Name</td>
                <td style="text-align:center" class="GridText" style="text-align:center">Total Vote</td>
            </tr>
            <asp:Label ID="lblPreferred" runat="server" Text="Label"></asp:Label>
        </table>
        <br />
    </div>

    

    <div>

        <h2>Employee Status</h2>
        <br />
         <table width="200px" border="1" cellpadding="1" cellspacing="1">
            <tr>
                <td style="text-align:center" class="GridText">Name</td>
                <td style="text-align:center" class="GridText">Status</td>
            </tr>
            <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
