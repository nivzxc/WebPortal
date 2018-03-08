<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="GroupUpdateDetailsMain.aspx.cs" Inherits="GroupUpdate_GroupUpdateDetailsMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table width="100%" cellpadding="0" cellspacing="0"> 
  <tr>
   <td>
    <div class="border" style="padding-top: 0px; padding-left: 0px; padding-right: 0px;	padding-bottom: 10px;">
<b><span class="HeaderText">Group Update Details</span></b> 
     <br /><br />
 
     <div class="GridBorder">  
        <table width="100%">
         <%--<tr>
          <td colspan="2">
           <table>
            <tr>
             <td class="masterpanel"><b>Group Update Details</b></td>
            </tr>
           </table>         
          </td>
         </tr> --%>   
         <tr>
          <td class="GridRows"  style="width:25%">Contributed By:</td>
          <td class="GridRows"  style="width:75%">
              <asp:Label ID="lblContributor" runat="server" Text="Label"></asp:Label>
             </td>
         </tr>        
         <tr>
          <td class="GridRows"  style="width:25%">Posted By:</td>
          <td class="GridRows"  style="width:75%">
              <asp:Label ID="lblCreateBy" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
         <tr>
          <td class="GridRows">Date Created:</td>
          <td class="GridRows">
              <asp:Label ID="lblCreateOn" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
         <tr>
          <td class="GridRows">Photo By:</td>
          <td class="GridRows">
              <asp:Label ID="lblPhotoSource" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
         <tr>
          <td class="GridRows">Status:</td>
          <td class="GridRows">
              <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
             </td>
         </tr> 
         <tr>
			<td class="GridRows" valign="top">Group Head:</td>
			<td class="GridRows"><asp:Label ID="lblGroupHead" runat="server" CssClass="controls" 
                                        Text="GroupHead" BorderStyle="None"></asp:Label>
                                </td>
		</tr>
        <tr>
          <td class="GridRows">Remarks:</td>
          <td class="GridRows">
              <asp:TextBox ID="txtGroupHeadRemark" runat="server" CssClass="controls" 
                  Rows="3" TextMode="MultiLine" Width="80%" ReadOnly="True"></asp:TextBox>
             </td>
         </tr> 
                            <%--<tr>
								<td class="GridRows" valign="top">Division Head:</td>
								<td class="GridRows">
                                    <asp:Label ID="lblDivisionHead" runat="server" CssClass="controls" 
                                        Text="DivisionHead" BorderStyle="None"></asp:Label>
                                </td>
							</tr>
                            <tr>
          <td class="GridRows">Remarks:</td>
          <td class="GridRows">
              <asp:TextBox ID="txtDivisionHeadRemark" runat="server" CssClass="controls" 
                  Rows="3" TextMode="MultiLine" Width="80%" ReadOnly="True"></asp:TextBox>
             </td>
         </tr> --%>
         <tr>
          <td class="GridRows">Title:</td>
          <td class="GridRows">
              <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
             </td>
         </tr>
         <tr>
          <td class="GridRows" style="vertical-align:top">Description:</td>
          <td class="GridRows"  style="vertical-align:top">
              <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
             </td>
         </tr>
         <tr>
          <td class="GridRows"  style="vertical-align:top">Content:</td>
          <td class="GridRows"  style="vertical-align:top">
              <asp:Label ID="lblImage" runat="server" Text="Label"></asp:Label><br />
              <asp:Label ID="lblContent" runat="server" Text="Label"></asp:Label>
             </td>
         </tr>
         <%--<tr>
          <td class="GridRows">&nbsp;</td>
          <td class="GridRows">
           <div class="MasterPanelLink">
               <asp:Label ID="lblLink" runat="server" Text="View Image"></asp:Label>
           </div>
          </td>
         </tr> --%>
        </table>

     </div>     
     <br /> 
    <div style="text-align:center;" runat="server" id="divButtons">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" 
            onclick="btnBack_Click" />--%>
        <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>
             
      
    </div>      
   </td>
  </tr>

 </table>
</asp:Content>

