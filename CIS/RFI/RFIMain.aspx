<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="RFIMain.aspx.cs" Inherits="CIS_RFI_RFIMain" %>
<%@ Import Namespace="STIeForms" %>
<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="RFIMain.aspx" class="SiteMap">RFI</a>
    </div>        
   </td>
  </tr>      
  <tr><td style="height:9px;"></td></tr>
  <%   
  //if (clsMRCF.IsApprover(clsMRCF.MRCFUserType.GroupHead,Request.Cookies["Speedo"]["UserName"].ToString()))
  //{
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">RFI For Approval</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of RFI For Approval</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>RFI Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% //LoadMenuGH();%>
       <tr><td class="GridColumns" colspan="3"><a href="RFIAllA.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>            
     </div>         
    </div>
   </td>
  </tr> 
  <tr><td style="height:9px"></td></tr>
  <% 
  //}
   
  //if (clsMRCF.IsApprover(clsMRCF.MRCFUserType.ProcurementManager,Request.Cookies["Speedo"]["UserName"].ToString()))
  //{
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">RFI For Approval (Procurement Manager Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of RFI For Approval</b></td>
          </tr>
         </table>           
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>RFI Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% //LoadMenuPM(); %>
       <tr><td colspan="3" class="GridColumns"><a href="RFIAllPM.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>      
  <%
  //} 
  %>
     
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My RFI</span></b>
     <br />
     <br />
     <asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNewRequest_Click" />
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Recent RFI Submission</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>MRCF Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadRFI(); %>
       <tr><td class="GridColumns" colspan="3"><a href="RFIAll.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
    </div>     
    </td>
   </tr>
     
 </table>  
</asp:Content>