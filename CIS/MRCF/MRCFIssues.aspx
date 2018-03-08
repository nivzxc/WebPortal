<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="MRCFIssues.aspx.cs" Inherits="CIS_MRCF_MRCFIssues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
<%--
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../EFormsMain.aspx" class="SiteMap">CIS</a> » 
     <a href="MRCFMenu.aspx" class="SiteMap">MRCF</a> » 
     <a href="MRCFReImport.aspx?mrcfcode=<% Response.Write(Request.QueryString["mrcfcode"]);%>" class="SiteMap">MRCF Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Issue on MRCF Request to Oracle</span></b>
     
     <br />

     <br />
     
     <br />

        <div runat="server" id="divSave" style="text-align:center;">

        <table align="left">
        <tr>
            <td>MRCF Number:</td>
            <td><asp:TextBox ID="txtMRCF" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" /></td>

        </tr>
        <tr>
            <td colspan="3"><a href="MRCFReImportItem.aspx">Re-import per Item line</a></td>
        </tr>
        </table>
            <br />
           <br />  
      <br />
     </div>
      <br />
    </div>
   </td>
  </tr>
 
 </table>
</asp:Content>

