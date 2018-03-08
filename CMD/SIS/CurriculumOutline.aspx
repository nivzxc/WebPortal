<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CurriculumOutline.aspx.cs" Inherits="CMD_SIS_CurriculumOutline" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="SchoolsDirectory.aspx" class="SiteMap">School Directory</a> » 
     <%
      Response.Write("<a href='SchoolsDirectoryDetails.aspx?schlcode=" + Request.QueryString["schlcode"] + "&schlname=" + Request.QueryString["schlname"] + "' class='SiteMap'>" + Request.QueryString["schlname"] + "</a> » " +
																					"<a href='CurriculumOutline.aspx?schlcode=" + Request.QueryString["schlcode"] + "&schlname=" + Request.QueryString["schlname"] + "' class='SiteMap'>Curriculum Outline</a>");
     %>     
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <table>
      <tr>
 <%--      <td><img src="../../Support/education32.png" alt="School" /></td>--%>
       <td>&nbsp;<b><span class="HeaderText"><%Response.Write(Request.QueryString["schlname"] + " (" + Request.QueryString["schlcode"] + ")"); %> Curriculum Outline</span></b></td>
      </tr>
     </table>
     <br />
     <br />
     <span style="color:#4682b4;font-size:small;">Select Program:</span>:&nbsp;
     <asp:DropDownList runat="server" ID="ddlProgram" CssClass="controls" AutoPostBack="true" BackColor="white" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
     
     <br /><br />
     <span style="color:#4682b4;font-size:small;">Select Curriculum Version:</span>:&nbsp;
     <asp:DropDownList runat="server" ID="ddlCurriculum" CssClass="controls" AutoPostBack="true" BackColor="white" OnSelectedIndexChanged="ddlCurriculum_SelectedIndexChanged"></asp:DropDownList>
     <br /><br />
     <span style="color:#4682b4;font-size:small;"><asp:Label ID="lblCourseName" runat="server" Text="Label"></asp:Label></span>
	    <% LoadRecords(); %>    
        <br /><br />
              <asp:Button ID="btnBack" runat="server" Text="Back" 
            onclick="btnBack_Click"/>
    </div>
   </td>
  </tr> 
  
 </table> 
</asp:Content>