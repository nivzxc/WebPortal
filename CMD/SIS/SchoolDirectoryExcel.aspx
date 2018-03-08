<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchoolDirectoryExcel.aspx.cs" Inherits="CMD_SIS_SchoolDirectoryExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div><h2><asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label></h2></div>
    <div><h3>Head Office Owned School</h3></div>
           <table border="1" cellpadding="1" cellspacing = "1" width="2900px">
            <tr>
                <td  style="width:400px">
                    <b>School Name</b>
                </td>
                 <td   style="width:400px">
                    <b>School Name 2</b>
                </td>
                 <td   style="width:600px">
                    <b>School Address</b>
                </td>
                 <td   style="width:300px">
                    <b>School Admin</b>
                </td>
                 <td  style="width:300px">
                    <b>Deputy School Admin</b>
                </td>
                 <td  style="width:300px">
                    <b>SOM</b>
                </td>
                 <td   style="width:300px">
                    <b>Telephone</b>
                </td>
                 <td   style="width:300px">
                    <b>Fax Number</b>
                </td>
            </tr>
            <% LoadHQOwned(); %>
           </table>
    </div>
    <br />

     <div>
    <div><h3>Franchise</h3></div>
           <table border="1" cellpadding="1" cellspacing = "1" width="2900px">
            <tr>
                <td style="width:400px">
                    <b>School Name</b></b>
                </td>
                 <td style="width:400px">
                    <b>School Name 2</b></b>
                </td>
                 <td style="width:600px">
                    <b>School Address</b>
                </td>
                 <td   style="width:300px">
                    <b>President</b>
                </td>
                 <td   style="width:300px">
                    School Admin</b>
                </td>
                 <td   style="width:300px">
                    <b>CM</b>
                </td>
                 <td   style="width:300px">
                    <b>Telephone</b>
                </td>
                 <td   style="width:300px">
                    <b>Fax Number</b>
                </td>
            </tr>
            <% LoadFranchise(); %>
           </table>
    </div>
    </form>
</body>
</html>
