<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CoursewareView.aspx.cs" Inherits="Academics_CoursewareView" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="Academics.aspx" class="SiteMap">Academics</a> » 
     <a href="Courseware.aspx" class="SiteMap">Courseware</a> » 
     <a href="CWIMain.aspx?page=1" class="SiteMap">Courseware Inventory</a> » 
     <a href="CoursewareView.aspx?subjcode=<%Response.Write(Request.QueryString["subjcode"]); %>" class="SiteMap"><%Response.Write(Request.QueryString["subjcode"]); %></a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Update Courseware Details</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
							<tr>
							 <td class="GridText" colspan="5">
							  <table>
							  	<tr>
							  	 <td><img src="../Support/viewtext22.png" alt="" /></td>
							  	 <td><b>Courseware Details</b></td>
										</tr>
									</table>
								</td>
							</tr>
       <tr>
        <td class="GridRows">Course Code:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCourseCode" ReadOnly="true" CssClass="controls" Width="100px"></asp:TextBox></td>
       </tr>							
       <tr>
        <td class="GridRows">Course Title:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCourseName" ReadOnly="true" CssClass="controls" Width="420"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Course Description:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCourseDesc" ReadOnly="true" CssClass="controls" TextMode="MultiLine" Width="420px" Rows="9"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">No. of Units/Hrs. per Week:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtUnits" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Unit Classification:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtUnitClass" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox></td>
       </tr>              
       <tr>
        <td class="GridRows">Courseware Availability:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlCWA" OnSelectedIndexChanged="ddlCWA_SelectedIndexChanged" AutoPostBack="true" CssClass="controls" BackColor="white">          
          <asp:ListItem Text="On-going" Value="O"></asp:ListItem>
          <asp:ListItem Text="No Courseware" Value="N"></asp:ListItem>
          <asp:ListItem Text="Guidelines" Value="G"></asp:ListItem>
          <asp:ListItem Text="For Development" Value="F"></asp:ListItem>
          <asp:ListItem Text="Completed" Value="C"></asp:ListItem>
          <asp:ListItem Text="No Status" Value="X"></asp:ListItem>
          <asp:ListItem Text="TOP Courseware" Value="T"></asp:ListItem>
          <asp:ListItem Text="Not used anymore" Value="U"></asp:ListItem>
         </asp:DropDownList>
         &nbsp;
         <asp:Label runat="server" ID="lblDateComp" Visible="false" Text="Date of Completion: "></asp:Label>
         <cc1:GMDatePicker runat="server" ID="dteCompletion" CssClass="controls" BackColor="white" ImageUrl="~/Support/Time22.png" Visible="false"></cc1:GMDatePicker>
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" TextMode="MultiLine" Width="420px" Rows="9" BackColor="white" MaxLength="255"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Updated By:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtUpdateBy" ReadOnly="true" CssClass="controls" Width="200px" Font-Size="Small"></asp:TextBox>
         -
         <asp:TextBox runat="server" ID="txtUpdateDate" ReadOnly="true" CssClass="controls" Width="100px" Font-Size="Small"></asp:TextBox>
        </td>
       </tr>      
      </table>
     </div>
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
							<tr>
							 <td class="GridText" colspan="5">
							  <table>
							  	<tr>
							  	 <td><img src="../Support/viewtext22.png" alt="" /></td>
							  	 <td><b>List of Curriculum Using The Courseware</b></td>
										</tr>
									</table>
								</td>
							</tr>
       <tr>
        <td class="GridColumns" style="width:20%"><b>Curriculum</b></td>
        <td class="GridColumns"><b>Program Title</b></td>
       </tr>
       <%Load_Curriculum(); %>
      </table>
     </div>
     <br />
     <div style="text-align:center;">
      <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSave_Click"/>
     </div>
    </div>
   </td>
  </tr> 
  
 </table> 
</asp:Content>