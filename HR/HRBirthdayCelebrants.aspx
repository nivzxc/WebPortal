<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="HRBirthdayCelebrants.aspx.cs" Inherits="HR_HRBirthdayCelebrants" %>

<asp:Content ID="cntMRCFNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="HRMain.aspx" class="SiteMap">HR</a> » 
     <a href="HRBirthdayCelebrants.aspx" class="SiteMap">Birthday Celebrators</a>
    </div>        
   </td>
  </tr>--%>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">STI HQ Birthday Celebrators</span></b>
     <br />
     <br />
     <b><span class="HeaderText">Birthday Celebrators for the month of: </span></b>
     <asp:DropDownList runat="server" ID="ddlMonth" CssClass="controls" Font-Size="Small" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
      <asp:ListItem Value="1" Text="January"></asp:ListItem>
      <asp:ListItem Value="2" Text="February"></asp:ListItem>
      <asp:ListItem Value="3" Text="March"></asp:ListItem>
      <asp:ListItem Value="4" Text="April"></asp:ListItem>
      <asp:ListItem Value="5" Text="May"></asp:ListItem>
      <asp:ListItem Value="6" Text="June"></asp:ListItem>
      <asp:ListItem Value="7" Text="July"></asp:ListItem>
      <asp:ListItem Value="8" Text="August"></asp:ListItem>
      <asp:ListItem Value="9" Text="September"></asp:ListItem>
      <asp:ListItem Value="10" Text="October"></asp:ListItem>
      <asp:ListItem Value="11" Text="November"></asp:ListItem>
      <asp:ListItem Value="12" Text="December"></asp:ListItem>
     </asp:DropDownList>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
      <%-- <tr>
        <td colspan="4" align="center" class="GridText">&nbsp;<b>List of Birthday Celebrators</b>
        <table>
          <tr>
           <td><img src="../Support/Cake22.png" alt="" /></td>
           <td></td>
          </tr>
         </table>        
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:15%; height: 9px;"><b>Birth Date</b></td>
        <td class="GridColumns" style="width:50%; height: 9px;"><b>Suspect</b></td>
       </tr>
       <% LoadRecords(); %>
      </table>
     </div>   
    </div>
   </td>
  </tr>  
  
 </table>
</asp:Content>