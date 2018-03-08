<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="SchoolDetails.aspx.cs" Inherits="CMD_SIS_SchoolDetails" %>

<asp:Content ID="cntMRCFRequest" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="SISMenu.aspx" class="SiteMap">SIS</a> » 
     <a href="Schools.aspx" class="SiteMap">Schools</a> » 
     <a href="SchoolDetails.aspx?schlcode=<%Response.Write(Request.QueryString["schlcode"]); %>&schlname=<%Response.Write(Request.QueryString["schlname"]); %>" class="SiteMap"><%Response.Write(Request.QueryString["schlname"]); %></a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText"><%Response.Write(Request.QueryString["schlname"]); %> Details</span></b>
     <br />
         
     <div runat="server" id="divError" visible="false"> 
      <div class="ErrMsg">
       <b>Error during update!</b><br /><br />
       <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
      </div>
      <br />
     </div>
     
     <br />
          
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>School Details</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:20%;">School Code:</td>
        <td class="GridRows" style="width:80%;">
         <asp:TextBox runat="server" ID="txtSchlCode" CssClass="controls" Width="50px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         <asp:CheckBox runat="server" ID="chkHQOwned" Text="Head Office Owned" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows">School Name:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSchoolName" CssClass="controls" Width="98%" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqSchoolName" ControlToValidate="txtSchoolName" ErrorMessage="<br>[Required]" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">School Name (alt):</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSchoolNameAlt" CssClass="controls" Width="98%" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqSchoolNameAlt" ControlToValidate="txtSchoolNameAlt" ErrorMessage="<br>[Required]" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">School Type:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlSchoolType" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>
       <tr>
        <td class="GridRows">School Address:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtAddress" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqAddress" ControlToValidate="txtAddress" ErrorMessage="<br>[Required]" Display="Dynamic"></asp:RequiredFieldValidator>          
        </td>        
       </tr>        
       <tr>
        <td class="GridRows">Tel #:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtTelNumber" CssClass="controls" Width="400px" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqTelNumber" ControlToValidate="txtTelNumber" ErrorMessage="<br>[Required]" Display="Dynamic"></asp:RequiredFieldValidator>          
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Fax #:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtFaxNumber" CssClass="controls" Width="250px" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqFaxNumber" ControlToValidate="txtFaxNumber" ErrorMessage="<br>[Required]" Display="Dynamic"></asp:RequiredFieldValidator>          
        </td>
       </tr>       
       <tr>
        <td class="GridRows">President / School Admin:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtCEO" CssClass="controls" Width="250px" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqCEO" ControlToValidate="txtCEO" ErrorMessage="<br>[Required]" Display="Dynamic"></asp:RequiredFieldValidator>          
        </td>
       </tr>  
       <tr>
        <td class="GridRows">Deputy / School Admin:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtCOO" CssClass="controls" Width="250px" BackColor="white"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqCOO" ControlToValidate="txtCOO" ErrorMessage="<br>[Required]" Display="Dynamic"></asp:RequiredFieldValidator>          
        </td>
       </tr>  
       <tr>
        <td class="GridRows">Channel Manager:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlCM" CssClass="controls" BackColor="white"></asp:DropDownList>
        </td>
       </tr>                    
      </table>     
     </div>          
     
     <div style="text-align:center;" id="divButtons" runat="server">
      <br />
      <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSaveChanges.jpg" OnClick="btnSave_Click" />
     </div>
    </div> 
   </td>
  </tr>
 
 </table>
</asp:Content>