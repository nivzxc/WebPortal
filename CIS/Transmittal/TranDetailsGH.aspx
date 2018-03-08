<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="TranDetailsGH.aspx.cs" Inherits="CIS_Transmittal_TranDetailsGH" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> � 
     <a href="../CIS.aspx" class="SiteMap">CIS</a> � 
     <a href="TranMenu.aspx" class="SiteMap">Transmittal</a> � 
     <a href="TranDetailsGH.aspx?trancode=<%Response.Write(Request.QueryString["trancode"]);%>" class="SiteMap">Transmittal Details</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Transmittal Details</span></b>
     <br />
     <br />
     
     <div style="text-align:center;" id="divButtons2" runat="server">      
      <%--<asp:ImageButton runat="server" ID="btnApproveUp" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click" />--%>
      <asp:Button ID="btnApproveUp" runat="server" Text="Approve"  
             OnClick="btnApprove_Click" BackColor="#33CC33" ForeColor="White"/>
      &nbsp;      
      <%--<asp:ImageButton runat="server" ID="btnDisapproveUp" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisapprove_Click" />--%>
      <asp:Button ID="btnDisapproveUp" runat="server" Text="Disapprove" 
             OnClick="btnDisapprove_Click" BackColor="#FF3300" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnResetUp" ImageUrl="~/Support/btnReset.jpg" OnClick="btnReset_Click" />--%>
         <asp:Button ID="btnResetUp" runat="server" Text="Reset"  OnClick="btnReset_Click" />
      <br />
      <br />
     </div>
          
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">     
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Paper22.png" alt="Request Details" /></td>
           <td>&nbsp;<b>Transmittal Details</b></td>
          </tr>
         </table>           
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows">Transmittal Code:&nbsp;&nbsp;</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtTransmittalCode" CssClass="controls" Width="80px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Requested:
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>        
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRequestor" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnRequestor" />
         <asp:TextBox runat="server" ID="txtRCName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Dispatch Type:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDispType" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>           
       </tr>
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtStat" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Item Description:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtItemDesc" CssClass="controls" ReadOnly="true" Width="98%"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Item Unit:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtUnit" CssClass="controls" ReadOnly="true"></asp:TextBox></td>              
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" ReadOnly="true" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Charge To:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="98%" ReadOnly="true"></asp:TextBox></td>
       </tr>              
       <tr>
        <td class="GridRows">Date Needed:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDateNeeded" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Group Head:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtGroupHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnGroupHead" />
        </td>
       </tr>                 
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGroupHeadRemarks" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" BackColor="white"></asp:TextBox></td>
       </tr>
       <tr runat="server" id="trApprover">
        <td class="GridRows">Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtApproverName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnApprover" />
        </td>
       </tr>                 
       <tr runat="server" id="trApproverRem">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverRemarks" CssClass="controls" Width="98%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>       
      </table>       
     </div>    
           
     <br />
     
     <div class="GridBorder">
      <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" BorderWidth="2" HeaderStyle-Height="20px" ItemStyle-BackColor="honeydew" AlternatingItemStyle-BackColor="aliceblue" HeaderStyle-Font-Bold="true" ItemStyle-VerticalAlign="Top">
       <Columns>
        <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="GridColumns">
         <ItemTemplate>
          <%#(DataBinder.Eval(Container.DataItem, "status").ToString() == "0" ? "<img src='../../Support/ForProcessing.png' alt='For Processing'>" : "<img src='../../Support/Approved.png' alt='Processed'>")%>
         </ItemTemplate>
        </asp:TemplateColumn>
                       
        <asp:TemplateColumn HeaderText="Recipient School" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="GridColumns">
         <ItemTemplate>
          <br />
          <asp:HiddenField runat="server" ID="hdnSchlCode" Value='<%#DataBinder.Eval(Container.DataItem, "schlcode")%>' />
          School: <asp:HyperLink runat="server" ID="lnkSchool" Text='<%#DataBinder.Eval(Container.DataItem, "schlname")%>' NavigateUrl=""></asp:HyperLink><br />
          Transmittal Number: <asp:Label runat="server" forecolor="dodgerblue" id="lblTranNmbr" Text='<%#DataBinder.Eval(Container.DataItem, "trannmbr")%>'></asp:Label><br />
          Quantity: <asp:Label runat="server" id="lblQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>'></asp:Label><br />
          <br />
         </ItemTemplate>
        </asp:TemplateColumn>
                               
        <asp:TemplateColumn HeaderText="Dispatch Details" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="GridColumns">
         <ItemTemplate>
          Dispatch by: <asp:HyperLink runat="server" ID="lnkDispatch" Text='<%#DataBinder.Eval(Container.DataItem, "dispby")%>' NavigateUrl=""></asp:HyperLink><br />
          Date dispatched: <asp:Label runat="server" forecolor="dodgerblue" id="lblDateDispatch" Text='<%#DataBinder.Eval(Container.DataItem, "datedisp")%>'></asp:Label><br />
          Received by: <asp:Label runat="server" forecolor="dodgerblue" id="lblRecBy" Text='<%#DataBinder.Eval(Container.DataItem, "recby")%>'></asp:Label><br />
          Date received: <asp:Label runat="server" id="lblRecDate" Text='<%#DataBinder.Eval(Container.DataItem, "recdate")%>'></asp:Label><br />                    
         </ItemTemplate>                  
        </asp:TemplateColumn>
       </Columns> 
      </asp:DataGrid>             
     </div>
     
     <div style="text-align:center;" id="divButtons" runat="server">
      <br />
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" OnClick="btnApprove_Click" />--%>
         <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             OnClick="btnApprove_Click" BackColor="#33CC33" ForeColor="White"/>
      &nbsp;      
     <%-- <asp:ImageButton runat="server" ID="btnDisapprove" ImageUrl="~/Support/btnDisapprove.jpg" OnClick="btnDisapprove_Click" />--%>
     <asp:Button ID="btnDisapprove" runat="server" Text="Disapprove"  
             OnClick="btnDisapprove_Click" BackColor="#FF3300" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Support/btnReset.jpg" OnClick="btnReset_Click" />--%>
      <asp:Button ID="btnReset" runat="server" Text="Reset"  OnClick="btnReset_Click" />
     </div>
     
    </div> 
   </td>
  </tr>
      
 </table>
</asp:Content>