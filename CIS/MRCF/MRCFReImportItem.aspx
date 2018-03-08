<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="MRCFReImportItem.aspx.cs" Inherits="CIS_MRCF_MRCFReImportItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">

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
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Re-Import MRCF Request Item to Oracle</span></b>
     
     <br />

     <br />
     
     <br />

        <div runat="server" id="divSave" style="text-align:center;">

        <table align="center">
        <tr>
            <td>MRCF Number:</td>
            <td><asp:TextBox ID="txtMRCF" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>MRCF Line Number</td>
            <td><asp:TextBox ID="txtLineNumber" runat="server"></asp:TextBox></td>
        </tr>
        </table>
            
           
      <br />
     </div>
          <div runat="server" id="divButtons" style="text-align:center;">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" 
             ImageUrl="~/Support/btnProcess.jpg" OnClick="btnApprove_Click"/>--%><asp:Button ID="btnApprove"
                 runat="server" Text="Import Now"  OnClick="btnApprove_Click"/>
      &nbsp;</div>
      <br />
    </div>
   </td>
  </tr>
 
 </table>
</asp:Content>

