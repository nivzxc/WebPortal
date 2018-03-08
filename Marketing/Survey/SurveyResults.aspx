<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyResults.aspx.cs" Inherits="Survey_SurveyResults" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>MySTIHQ HR Survey Forms Result</title>
  <link rel="Stylesheet" type="text/css" href="../MySTIHQ.css" />
 </head>
 <body>
  <form id="form1" runat="server">
   <br /><br />
   <table width="90%" cellpadding="0" cellspacing="0" class="centermsgbox">    
    <tr>
     <td>
      <asp:DataGrid ID="dgAnswer" runat="server"></asp:DataGrid>
      <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
       <table border="0" width="100%">
        <tr>
         <td>
          <table>
           <tr>
            <td><img src="../Support/Approve32.png" alt="" /></td>
            <td><b><span class="HeaderText">Survey Results (Total Participants: <%Response.Write(GetTotalParticipants()); %>)</span></b></td>
           </tr>
          </table>           
         </td>
        </tr>
        <tr>
         <td>
          <table style="font-size:small;">
           <tr>
            <td>Division:</td>
            <td>&nbsp;<asp:DropDownList runat="server" ID="ddlDivision" CssClass="controls" Font-Size="Small" onselectedindexchanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
           </tr>
           <tr runat="server" id="trDepartment" visible="false">
            <td>Department:</td>
            <td>&nbsp;<asp:DropDownList runat="server" ID="ddlDepartment" CssClass="controls" Font-Size="Small"></asp:DropDownList></td>
           </tr>           
           <tr><td><asp:Button runat="server" ID="btnApply" Text="Apply" 
             onclick="btnApply_Click" /></td></tr>
          </table>
         </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
         <td>
          <table width="100%" cellspacing="1">
           <tr><td><b><span class="HeaderText">PART I: Organizational Climate Assessment</span></b></td></tr>           
           <tr>
            <td style="color:#4682b4;font-size:small;">
             <br />
             <div class="GridBorder" style="width:95%">
              <table width="100%" class="Grid" cellspacing="1">
               <tr>
                <td class="GridColumns"><b>Item Category</b></td>
                <td class="GridColumns"><b>Total</b></td>
                <td class="GridColumns"><b>Ave.</b></td>
               </tr>
               <%
                Load_Result("1");
               %>
              </table>
             </div>
            </td>
           </tr>
          </table>
         </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
         <td>
          <table width="100%">
           <tr><td><b><span class="HeaderText">PART II: Team Assessment</span></b></td></tr>           
           <tr>
            <td style="color:#4682b4;font-size:small;">
             <br />
             <div class="GridBorder" style="width:95%">
              <table width="100%" class="Grid" cellspacing="1">
               <tr>
                <td class="GridColumns"><b>Item Category</b></td>
                <td class="GridColumns"><b>Total</b></td>
                <td class="GridColumns"><b>Ave.</b></td>
               </tr>
               <%
                Load_Result("2");
               %>
              </table>
             </div>
            </td>
           </tr>
          </table>
         </td>
        </tr>        
       </table>
      </div>
     </td>
    </tr>
   </table>
  </form>
 </body>
</html>   