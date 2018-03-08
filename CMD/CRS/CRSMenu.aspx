<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CRSMenu.aspx.cs" Inherits="CMD_CRS_CRSMenu" %>
<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>

<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="CRSMenu.aspx" class="SiteMap">Courseware Request</a>
    </div>        
   </td>
  </tr>  

  <% 
  if (clsCRS.IsUser(clsCRS.CRSUserType.ChannelManagerHead,Request.Cookies["Speedo"]["UserName"].ToString()))
  {
  %>   

  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Courseware Request For Your Approval (Channel Head Level)</span></b>
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="For Approval" /></td>
           <td>&nbsp;<b>Recent Courseware Request Submission</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:40px;">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Courseware Request</b></td>
        <td class="GridColumns" style="width:200px;"><b>Status</b></td>
       </tr>
       <%LoadMenuCMHead(); %>
       <tr><td class="GridColumns" colspan="3"><a href="CRSAllCMH.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>    
    </div>     
   </td>
  </tr>        
  <%
  }
  %>
       
  <% 
   if (clsCRS.IsUser(clsCRS.CRSUserType.ChannelManager,Request.Cookies["Speedo"]["UserName"].ToString()))
   {
  %>   

  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Courseware Request System</span></b>
     <br />
     <br />         
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="For Approval" /></td>
           <td>&nbsp;<b>Recent Courseware Request Submission</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:40px;">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Courseware Request</b></td>
        <td class="GridColumns" style="width:200px;"><b>Status</b></td>
       </tr>
       <%LoadMenuCM(); %>
       <tr><td class="GridColumns" colspan="3"><a href="CRSAllCM.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>
     <br />
     <table>
      <tr>
       <td><asp:DropDownList runat="server" ID="ddlSchools" CssClass="controls" BackColor="white" Font-Size="medium" ForeColor="cornflowerblue"></asp:DropDownList></td>
       <td><asp:ImageButton runat="server" ID="btnNew" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNew_Click" /></td>
      </tr>
     </table>     
    </div>     
   </td>
  </tr>
    
  <tr><td style="height:9px;"></td></tr>  
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Courseware Request Summary</span></b>
     <br />
     <br />
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;width:190px;background-image:none;background-color:#f0f8ff;">
      <table style="font-size:small;">
       <tr><td>For Endorsement: <asp:Label runat="server" ID="lblE" Font-Bold="true"></asp:Label></td></tr>
       <tr><td>Partial Dispatch: <asp:Label runat="server" ID="lblP" Font-Bold="true"></asp:Label></td></tr>
      </table>
     </div>
     <br />     
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/Time22.png" alt="For Approval" /></td>
           <td>&nbsp;<b>Recent Courseware Dispatch</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:50%;"><b>Courseware Details</b></td>
        <td class="GridColumns" style="width:25%;"><b>Date Dispatched</b></td>
        <td class="GridColumns" style="width:20%;"><b>Status</b></td>
       </tr>
       <%LoadCMRecentDispatch(); %>       
      </table>
     </div>          
    </div>     
   </td>
  </tr>
    
  <%
  }
  %>

  <%    
  if (clsCRS.IsUser(clsCRS.CRSUserType.CoursewareCoordinator, Request.Cookies["Speedo"]["UserName"].ToString()))
  {  
  %>  
  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Courseware Request For Processing</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../../Support/viewtext22.png" alt="For Approval" /></td>
           <td>&nbsp;<b>Endorsed Courseware Request</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:40px;">&nbsp;</td>
        <td class="GridColumns" style="width:350px;"><b>Courseware Request</b></td>
        <td class="GridColumns" style="width:200px;"><b>Status</b></td>
       </tr>
       <%LoadMenuCC(); %>
       <tr><td class="GridColumns" colspan="3"><a href="CRSAllCC.aspx?mode=f&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>     
    </div>     
   </td>
  </tr>   
  <%
  }

  if (clsCRS.IsUser(clsCRS.CRSUserType.EliteUsers, Request.Cookies["Speedo"]["UserName"].ToString()))
  {
  %>
  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Courseware Request System (Elite Users Access)</span></b>
     <br />
     <br /> 
     <div style="text-align:center;">
      <web:chartcontrol id="chaCRSSummary" runat="server" borderstyle="Outset" borderwidth="1px" height="350px" width="600px" ChartFormat="Jpg" LeftChartPadding="20" YValuesInterval="5" YCustomEnd="50" YCustomStart="0" BottomChartPadding="9" Padding="9">
       <ChartTitle Font="Verdana,10pt,style=Bold" ForeColor="white"></ChartTitle>
       <Background Color="SkyBlue" Type="LinearGradient" EndPoint="900,900"></Background>
       <YAxisFont StringFormat="Far,Near,Character,LineLimit"></YAxisFont>
       <XTitle StringFormat="Center,Near,Character,LineLimit"></XTitle>         
       <XAxisFont StringFormat="Center,Near,Character,LineLimit"></XAxisFont>         
       <YTitle StringFormat="Center,Near,Character,LineLimit"></YTitle>
       <Legend Position="Bottom" Width="50">
        <Background CenterColor="LightYellow" />        
       </Legend>
      </web:chartcontrol>  
     </div>        
    </div>     
   </td>
  </tr>
    
  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Courseware Request Reports</span></b>
     <br />
     <br />
     <table>
      <tr>
       <td><img src="../../Support/Paper22.png" alt="" /></td>
       <td><a href="CRSAllEU.aspx" style="font-size:small;">Courseware Request Master List</a></td>
      </tr>
     </table>
    </div>     
   </td>
  </tr>  
  <% 
  } 
  %>

  <!--
  <tr><td style="height:9px;"></td></tr>  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <table>
      <tr>
       <td valign="middle"><img src="../../Support/alert64.png" alt="" /></td>
       <td valign="middle" style="font-size:small;color:#4169e1;">
        Sorry. You are only granted limited access to this module.     
        <br />
        Please contact your system administrator to modify your access rights.       
       </td>
      </tr>
     </table>
    </div>     
   </td>
  </tr>  
  -->
    
 </table>  
</asp:Content>