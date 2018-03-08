<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="TranDetails.aspx.cs" Inherits="CIS_Transmittal_TranDetails" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../EFormsMain.aspx" class="SiteMap">CIS</a> » 
     <a href="TranMenu.aspx" class="SiteMap">Transmittal</a> » 
     <a href="TranDetails.aspx?trancode=<%Response.Write(Request.QueryString["trancode"]);%>" class="SiteMap">Transmittal Request Details</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
  
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">View Transmittal Request</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3">     
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/Paper22.png" alt="Request Details" /></td>
           <td>&nbsp;<b>Transmittal Request Details</b></td>
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
         <asp:TextBox runat="server" ID="txtDateRequested" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>        
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtEmployeeName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Dispatch Type:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDispatchType" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox></td>           
       </tr>
       <tr>
        <td class="GridRows">Request Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatus" CssClass="controls" ReadOnly="true" Width="200px"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatus" />
        </td>
       </tr>
       <tr>
        <td class="GridRows">Item Description:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtItemDescription" CssClass="controls" ReadOnly="true" Width="98%"></asp:TextBox>
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
       <tr runat="server" id="trChargeTo" visible="false">
        <td class="GridRows">Charge To:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtChargeTo" CssClass="controls" Width="98%" ReadOnly="true"></asp:TextBox></td>
       </tr>              
       <tr runat="server" id="trDateNeeded" visible="false">
        <td class="GridRows">Date Needed:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDateNeeded" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr runat="server" id="trGroupHead" visible="false">
        <td class="GridRows">Group Head:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGroupHeadName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>                 
       <tr runat="server" id="trGroupHeadRem" visible="false">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGroupHeadRemarks" CssClass="controls" Width="98%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr runat="server" id="trApproverName" visible="false">
        <td class="GridRows">Approver:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>                 
       <tr runat="server" id="trApproverRemarks" visible="false">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtApproverRemarks" CssClass="controls" Width="98%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>       
      </table>       
     </div>       
        
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="0">
       <tr>
        <td colspan="2" class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>
            <table>
             <tr>
              <td>&nbsp;<b>List of Requested Items</b></td>              
             </tr>
            </table>            
           </td>
           <td style="text-align:right;"> <asp:Button ID="btnDelete" runat="server" Text="Delete"  OnClick="btnDelete_Click" /><%--<asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/Support/btnExclude.jpg" OnClick="btnDelete_Click" />--%>&nbsp;</td>
          </tr>
         </table>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">     
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" BorderWidth="2" HeaderStyle-Height="20px" HeaderStyle-Font-Bold="true" ItemStyle-VerticalAlign="Top" AlternatingItemStyle-BackColor="#e4f2ff">
           <Columns>
            <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="GridColumns" ItemStyle-Width="7%">
             <ItemTemplate>
              <%#(DataBinder.Eval(Container.DataItem, "status").ToString() == "0" ? "<img src='../../Support/ForProcessing.png' alt='For Processing'>" : "<img src='../../Support/Approved.png' alt='Processed'>")%>
              <asp:HiddenField runat="server" ID="hdnItemStatus" Value='<%#DataBinder.Eval(Container.DataItem, "status")%>' />
             </ItemTemplate>
            </asp:TemplateColumn>
                       
            <asp:TemplateColumn HeaderText="Recipient School" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="GridColumns" ItemStyle-Width="48%">
             <ItemTemplate>
              <br />
              <asp:HiddenField runat="server" ID="hdnSchlCode" Value='<%#DataBinder.Eval(Container.DataItem, "schlcode")%>' />
              School: <asp:HyperLink runat="server" ID="lnkSchool" Text='<%#DataBinder.Eval(Container.DataItem, "schlname")%>' NavigateUrl=""></asp:HyperLink><br />
              Transmittal Number: <asp:Label runat="server" forecolor="dodgerblue" id="lblTranNmbr" Text='<%#DataBinder.Eval(Container.DataItem, "trannmbr")%>'></asp:Label><br />
              Quantity: <asp:Label runat="server" id="lblQty" Text='<%#DataBinder.Eval(Container.DataItem, "qty")%>'></asp:Label><br />
              <br />
             </ItemTemplate>
            </asp:TemplateColumn>
                               
            <asp:TemplateColumn HeaderText="Dispatch Details" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="GridColumns" ItemStyle-Width="38%">
             <ItemTemplate>
              Dispatch by: <asp:HyperLink runat="server" forecolor="dodgerblue" ID="lnkDispatch" Text='<%#DataBinder.Eval(Container.DataItem, "dispby")%>' NavigateUrl=""></asp:HyperLink><br />
              Date dispatched: <asp:Label runat="server" ID="lblDateDispatch" Text='<%#DataBinder.Eval(Container.DataItem, "datedisp")%>'></asp:Label><br />
              Received by: <asp:Label runat="server" forecolor="dodgerblue" id="lblRecBy" Text='<%#DataBinder.Eval(Container.DataItem, "recby")%>'></asp:Label><br />
              Date received: <asp:Label runat="server" id="lblRecDate" Text='<%#DataBinder.Eval(Container.DataItem, "recdate")%>'></asp:Label><br />          
             </ItemTemplate>                  
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderImageUrl="~/Support/close16.png" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="GridColumns" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="7%">
             <ItemTemplate>
              <asp:CheckBox runat="server" ID="chkDelete" />
             </ItemTemplate>
            </asp:TemplateColumn>        
           </Columns>             
          </asp:DataGrid>             
         </div>
        </td>
       </tr>
      </table>
     </div>
          
     <div style="text-align:center;" id="divButtons" runat="server">
      <br /> 
      <%--<asp:ImageButton runat="server" ID="btnVoid" ImageUrl="~/Support/btnVoid.jpg" OnClick="btnVoid_Click" />--%>
         <asp:Button ID="btnVoid" runat="server" Text="Void"  OnClick="btnVoid_Click"/>
     </div>     
    </div> 
   </td>
  </tr>
      
 </table>
</asp:Content>