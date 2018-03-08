<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="SchoolsDirectory.aspx.cs" Inherits="CMD_SIS_SchoolsDirectory" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">


  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">PhilFirst Branch Directory</span></b>
     <br />
     <br />    
     <table width="100%">
      <tr>
       <td>
        <div class="GridBorder">
         <table width="100%" cellpadding="5" class="grid">
          <tr>
           <td class="GridRows" style="width:20%">Branch Name:</td>
           <td class="GridRows"><asp:TextBox runat="server" ID="txtSchlName" CssClass="controls" Width="300px" BackColor="white"></asp:TextBox></td>
           <td class="GridRows" style="text-align:center;" rowspan="4">  
               <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click"/>      
           </td>
          </tr>
         </table>
        </div>
           <br />

           <asp:Button ID="btnExport" runat="server" Text="Export to Excel"  onclick="btnExport_Click"/>
           <br />
        <br />      

        <br />
        <div class="GridBorder">
         <table width="100%" cellpadding="5" class="grid">
				<tr>
		       	 <td class="GridColumns" style="text-align:center; width:60%"><b>Branch Details</b></td>
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
