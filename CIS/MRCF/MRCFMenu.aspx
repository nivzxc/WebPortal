<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MRCFMenu.aspx.cs" Inherits="CIS_MRCF_MRCFMenu" %>
<%@ Import Namespace="STIeForms" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">

          <table width="100%" cellpadding="0" cellspacing="0">
      <%
    if (clsMRCFAssign.IsPurchasing(Request.Cookies["Speedo"]["UserName"].ToString()))
    {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">&nbsp;Assigned MRCF</span></b>

     <br />
     <br />
     
     <div class="GridBorder">
    
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadListAssigned(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="MRCFAllAssign.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
   <% 
    }
    if (clsMRCF.IsApprover(clsMRCF.MRCFUserType.DivisionHead, Request.Cookies["Speedo"]["UserName"].ToString()))
    {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">MRCF For Approval (Division Head Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadMenuDH(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="MRCFAllDH.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <% 
    }

    if (clsMRCF.IsApprover(clsMRCF.MRCFUserType.GroupHead, Request.Cookies["Speedo"]["UserName"].ToString()))
    {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">MRCF For Approval (Group Head Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of MRCF For Approval</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadMenuGH();%>
       <tr><td class="BrowseAll" colspan="3"><a href="MRCFAllGH.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>            
     </div>         
    </div>
   </td>
  </tr> 
 
  <% 
    }

    if (clsMRCF.IsApprover(clsMRCF.MRCFUserType.ProcurementManager, Request.Cookies["Speedo"]["UserName"].ToString()))
    {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">MRCF For Approval (Procurement Manager Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of MRCF For Approval</b></td>
          </tr>
         </table>           
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadMenuPM(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="MRCFAllPM.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>

      
     </div>  
     <br />
         <b><span class="HeaderText">MRCF To Oracle Monitoring</span></b><br /><br />
        <%--<asp:ImageButton runat="server" ID="btnProcess" 
            ImageUrl="~/Support/btnSmallView.jpg" onclick="btnProcess_Click"/>--%>
        <asp:Button ID="btnProcess" runat="server" Text="View MRCF for Import"  onclick="btnProcess_Click"/>
     <br />
      
    </div>
   </td>
  </tr>  
   <%--  <tr><td style="height:9px"></td></tr>   --%>
  <%
    } 
  %>
  <tr>
   <td>
    <div class="border" style="padding-top:5px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My MRCF</span></b>
       <br />
     <br />
    <%-- <asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNewRequest_Click" />--%>
        <asp:Button ID="btnNewRequest" runat="server" Text="New Request" OnClick="btnNewRequest_Click" />
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Recent MRCF Submission</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadMRCF(); %>
       <tr><td class="BrowseAll" colspan="3"><a href="MRCFAll.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
    </div>     
    </td>
   </tr>
     
 </table>  
</asp:Content>