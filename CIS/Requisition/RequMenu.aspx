<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="RequMenu.aspx.cs" Inherits="CIS_Requisition_RequMenu" %>
<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>
<%@ Import Namespace="STIeForms" %>
<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
<%--
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="RequMenu.aspx" class="SiteMap">Requisition</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
   --%>
  <!--
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Budget Details</span></b>
     <br /><br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="2" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../Support/contact22.png" alt="" /></td>
           <td>&nbsp;<b>Budget Details Summary</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:80%;"><b>Responsibility Center</b></td>
        <td class="GridColumns" style="width:18%;"><b>Budget</b></td>
       </tr>
       <% LoadBudgetDetails(); %>
      </table>
     </div>
    </div>        
   </td>
  </tr>     
  <tr><td style="height:9px;"></td></tr>
  -->
     
  <!-- load all requisition panel for supervisor approval level -->
  <%
   if (clsRequisition.IsApprover(clsRequisition.RequisitionUserType.GroupHead, Request.Cookies["Speedo"]["UserName"].ToString()))
   {
  %>
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Requisitions For Your Approval (Group Head Level)</span></b>
     <br /><br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
      <%-- <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Requisition For Approval</b></td>
          </tr>
         </table>
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadGroupHeadMenu(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="RequAllGH.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>
  <tr><td style="height:9px;"></td></tr>
  <%  
   }    
   if (clsRequisition.IsApprover(clsRequisition.RequisitionUserType.DivisionHead,Request.Cookies["Speedo"]["UserName"].ToString()))
   {
  %>  
  
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Requisition For Your Approval (Division Head Level)</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Requisition For Approval</b></td>
          </tr>
         </table>
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadDivisionHeadMenu(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="RequAllDH.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>          
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Division's Office Supplies Budget Consumption</span></b>
     <br /><br />
     <div style="text-align:center;">
      <web:chartcontrol id="chaBudCon" runat="server" borderstyle="Outset" borderwidth="1px" height="300px" width="600px" ChartFormat="Jpg" LeftChartPadding="20" YValuesInterval="10000" YCustomEnd="70000" YCustomStart="0" BottomChartPadding="9" Padding="9">
       <ChartTitle Font="Verdana,10pt,style=Bold" ForeColor="white"></ChartTitle>
       <Background Color="SkyBlue" Type="LinearGradient" EndPoint="900,900"></Background>
       <YAxisFont StringFormat="Far,Near,Character,LineLimit"></YAxisFont>
       <XTitle StringFormat="Center,Near,Character,LineLimit"></XTitle>         
       <XAxisFont StringFormat="Center,Near,Character,LineLimit"></XAxisFont>         
       <YTitle StringFormat="Center,Near,Character,LineLimit"></YTitle>
       <Legend Position="Bottom" Width="30">
        <Background CenterColor="LightYellow" />
       </Legend>
      </web:chartcontrol>            
     </div> 
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px;"></td></tr>  
   <% 
   } 
  %>

  <%
   if (clsSystemModule.HasAccess("REQU", Request.Cookies["Speedo"]["Username"]))
   {
  %>
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <span class="HeaderText"><b>Requisition For Approval (Supplies Custodian Level)</b></span>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
      <%-- <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Requisition For Approval</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>        
       </tr>
       <% LoadSuppliesCustodian(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="RequAllSC.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>          
    </div>
   </td>
  </tr>
  <tr><td style="height:9px"></td></tr>      
     <% 
   } 
  %>
    
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">My Requisition</span></b>
     <br />
     <br />
     <%--<asp:ImageButton runat="server" ID="btnNew" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNew_Click" />--%>
        <asp:Button ID="btnNew" runat="server" Text="New Request"  OnClick="btnNew_Click" />
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <%--<tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Recent Requisition Submission</b></td>
          </tr>
         </table>            
        </td>
       </tr>--%>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Request Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
       <% LoadRequisition(); %>
       <tr><td colspan="3" class="BrowseAll"><a href="RequAll.aspx?page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
    </div>     
   </td>
  </tr> 
 
 </table>
</asp:Content>