<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="TelephoneDirectory.aspx.cs" Inherits="CIS_MISIT_TelephoneDirectory" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="misit.aspx" class="SiteMap">MIS-IT</a> » 
     <a href="TelephoneDirectory.aspx" class="SiteMap">Telephone Directory</a>     
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">STI HQ Telephone Directory</span></b>
     <br />
     <br />    
     <table width="100%">
      <tr>
       <td>
        <div class="GridBorder">
         <table width="100%" cellpadding="5" class="grid">
          <tr>
           <td class="GridRows" style="width:20%">First Name:</td>
           <td class="GridRows"><asp:TextBox runat="server" ID="txtFirstName" CssClass="controls" Width="300px" BackColor="white"></asp:TextBox></td>
           <td class="GridRows" style="text-align:center;" rowspan="2"><asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" /></td>
          </tr>
          <tr>
           <td class="GridRows">Last Name:</td>
           <td class="GridRows"><asp:TextBox runat="server" ID="txtLastName" CssClass="controls" Width="300px" BackColor="white"></asp:TextBox></td>
          </tr>    
         </table>
        </div>
        <br />
        <div class="GridBorder">
         <table width="100%" cellpadding="5" class="grid">
				      <tr>
		       	 <td class="GridColumns" style="text-align:center; width:60%"><b>Employee Details</b></td>
											<td class="GridColumns" style="text-align:center; width:40%"><b>Contact Numbers</b></td>
										</tr>
          <% Load_Records(); %>
         </table>
        </div>
       </td>
      </tr>
     </table>
    </div>     
   </td>
  </tr>
  
 </table>
</asp:Content>
