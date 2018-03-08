<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="FinanceRequestMenu.aspx.cs" Inherits="Finance_FinanceRequestMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../Default.aspx" class="SiteMap">Finance</a> » 
     <a href="FinanceRequestMenu.aspx" class="SiteMap">Request for Payment</a>
    </div>        
   </td>
  </tr>      
  <tr><td style="height:9px;"></td></tr>
  <tr>
   <td> 
   <%--For Finance--%>
    <%
     if (clsSystemModule.HasAccess("023",Request.Cookies["Speedo"]["UserName"].ToString()))
      {
    %>
     <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id ="divFinance">    
      <b><span class="HeaderText">Request for Payment (Finance Group)</span></b>
      <br />
      <br />
      <div class="GridBorder">
       <table width="100%" cellpadding="5" class="Grid">
        <tr>
         <td colspan="4" align="center" class="GridText">
          <table>
           <tr>
            <td>&nbsp;<b>List of Request for Payment</b></td>
           </tr>
          </table>           
         </td>
        </tr>
        <tr>
         <td class="GridColumns" style="width:5%;">&nbsp;</td>
         <td class="GridColumns" style="width:95%;"><b>Request for Payment Details</b></td>
        </tr>
        <% LoadMenuRFPFinance(); %>
        <tr><td colspan="3" class="GridColumns"><a href="RFPListAll.aspx?&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
       </table>
      </div>         
     </div><br />
     <%
    }
    %>
   <%--For User--%>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;" id="divUser">    
     <b><span class="HeaderText">My Request for Payment</span></b>
     <br />
     <br />
     <asp:ImageButton runat="server" ID="btnNewRequest" ImageUrl="~/Support/btnNewRequest.jpg" OnClick="btnNewRequest_Click" />
     <br />
     <br /> 
     <%--<table cellpadding="5">
      <tr>
       <td><b><span class="HeaderText">Filter Options</span></b></td>
      </tr>
     </table> <br />
     <div class="GridBorder">--%>
     <%--<table width="100%" cellpadding="5">
      <tr>
       <td style="width:15%;">Control Number: </td>
       <td style="width:30%;"><asp:TextBox runat="server" ID="txtControlNumber" CssClass="controls" Width="80%" BackColor="white" MaxLength="12"></asp:TextBox></td>
       <td style="width:15%;">Request Type:</td>
       <td style="width:30%;"><asp:DropDownList runat="server" ID="ddlRequestType" CssClass="controls" BackColor="white"></asp:DropDownList></td>
       <td style="width:10%;" style="text-align:center;">
        <asp:ImageButton runat="server" ID="btnSearch" 
         ImageUrl="~/Support/btnSearch.jpg" /></td>
      </tr>
     </table>
     </div><br />--%>
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
          <%-- <td><img src="../../Support/Pen22.png" alt="" /></td>--%>
           <td>&nbsp;<b>List Request for Payment</b></td>
          </tr>
         </table>           
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:95%;"><b>Request for Payment Details</b></td>
       </tr>
       <% LoadMenuRFP(); %>
       <tr><td colspan="3" class="GridColumns"><a href="RFPListAll.aspx?&page=1" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  <tr><td style="height:9px"></td></tr>
 </table>  
</asp:Content>

