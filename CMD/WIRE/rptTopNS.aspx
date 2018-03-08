<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/TwoPanel.master" CodeFile="rptTopNS.aspx.cs" Inherits="CMD_WIRE_rptTopNS" %>

<asp:Content ID="conWIRE" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="WIRE.aspx" class="SiteMap">WIRE</a> » 
     <a href="WireReports.aspx" class="SiteMap">Reports</a> » 
     <a href="rptTopNS.aspx?schltype=A&schlown=A&sort=tyenr" class="SiteMap">Top NS Report</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Top NS Report</span></b>
     <br />
     <br />
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;width:600px;background-image:none;background-color:#f0f8ff;">
      <table>
       <tr>
        <td>Schools Category:</td>
        <td>
         <asp:DropDownList runat="server" ID="ddlSchlCat" Font-Size="X-Small">
          <asp:ListItem Text="All Schools" Value="A"></asp:ListItem>
          <asp:ListItem Text="Colleges" Value="C"></asp:ListItem>
          <asp:ListItem Text="Education Center" Value="E"></asp:ListItem>
         </asp:DropDownList>        
        </td>
        <td>&nbsp;</td>
        <td>Owning Type:</td>
        <td>
         <asp:DropDownList runat="server" ID="ddlOwning" Font-Size="X-Small">
          <asp:ListItem Text="All Schools" Value="A"></asp:ListItem>
          <asp:ListItem Text="HQ Owned" Value="H"></asp:ListItem>
          <asp:ListItem Text="Non-HQ Owned" Value="N"></asp:ListItem>
         </asp:DropDownList>        
        </td>        
        <td>&nbsp;</td>
        <td><asp:Button runat="server" ID="btnSubmit" Text="Load Report" 
          Font-Size="X-Small" onclick="btnSubmit_Click" /></td>
       </tr>
      </table>      
     </div>
     
     <br />

     <div style="border-right:#ffd700 1px solid; padding-right:3px; border-top: #ffd700 1px solid; padding-left:3px; left:20px; padding-bottom:3px; border-left:#ffd700 1px solid; padding-top:3px; border-bottom:#ffd700 1px solid; background-color:#fffff0; font-size:x-small; width:600px;">
      <table cellpadding="7" cellspacing="7" width="100%">
       <tr><td style="background-color:#efffdd">This Year Enrollees >= Last Year Enrollees Enrollees Percentage: <asp:Label runat="server" ID="lblGreen" Font-Size="X-Small" Font-Bold="true"></asp:Label></td></tr>
       <tr><td style="background-color:#f0f8ff">This Year Registrants >= Last Year Enrollees Percentage: <asp:Label runat="server" ID="lblBlue" Font-Size="X-Small" Font-Bold="true"></asp:Label></td></tr>
       <tr><td style="background-color:#ffe4e1">This Year Enrollees/Registrants < Last Year Enrollees Enrollees Percentage: <asp:Label runat="server" ID="lblRed" Font-Size="X-Small" Font-Bold="true"></asp:Label></td></tr>                
      </table>
     </div>     
     
     <br />

			  <div class="GridBorder" style="width:100%;">
			   <table width="100%" cellpadding="4" class="grid" runat="server" id="tblReports">
			    <tr>
			     <td colspan="7" align="center" class="GridText">&nbsp;&nbsp;<b>NS Schools Ranking <asp:Label runat="server" ID="lblTableHead"></asp:Label></b></td>
			    </tr>
			    <tr>
			     <td class="GridColumns"><b>Ranking</b></td>
			     <td class="GridColumns"><b><a href="rptTopNs.aspx?schltype=<%Response.Write(Request.QueryString["schltype"]);%>&schlown=<%Response.Write(Request.QueryString["schlown"]);%>&sort=name">School Name</a></b></td>
			     <td class="GridColumns"><b><a href="rptTopNs.aspx?schltype=<%Response.Write(Request.QueryString["schltype"]);%>&schlown=<%Response.Write(Request.QueryString["schlown"]);%>&sort=lastup">Last Update</a></b></td>
			     <td class="GridColumns"><b><a href="rptTopNs.aspx?schltype=<%Response.Write(Request.QueryString["schltype"]);%>&schlown=<%Response.Write(Request.QueryString["schlown"]);%>&sort=reg">Reg</a></b></td>
			     <td class="GridColumns"><b><a href="rptTopNs.aspx?schltype=<%Response.Write(Request.QueryString["schltype"]);%>&schlown=<%Response.Write(Request.QueryString["schlown"]);%>&sort=tyenr">TY Enr</a></b></td>
			     <td class="GridColumns"><b>LY Enr</b></td>
			     <td class="GridColumns"><b>Inc/Dec</b></td>
			    </tr>			 
      </table>
     </div>
           
    </div>
   </td>
  </tr>
 </table>
</asp:Content>