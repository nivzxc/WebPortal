<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="ATWDetailsH.aspx.cs" Inherits="HR_HRMS_ATW_ATWDetailsH" %>
<asp:Content ID="cntOvertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0"> 
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="ATWMenu.aspx" class="SiteMap">ATW</a> » 
     <a href='ATWDetailsH.aspx?atwcode=<%Response.Write(Request.QueryString["atwcode"]); %>' class="SiteMap">ATW Details</a>
    </div>        
   </td>
  </tr>  --%>
<%--  <tr><td style="height:9px;"></td></tr> --%>
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">ATW Details</span></b>
     <br /> 
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div>         
     <br />     
     <div style="text-align:center;">
     <%-- <asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" onclick="btnApprove_Click" />--%>
         <asp:Button ID="Button1" runat="server" Text="Approve" 
             onclick="btnApprove_Click1" BackColor="#33CC33" ForeColor="White" />
      &nbsp;
     <%-- <asp:ImageButton runat="server" ID="btnDisapprove" ImageUrl="~/Support/btnDisapprove.jpg" onclick="btnDisapprove_Click" />--%>
         <asp:Button ID="Button2" runat="server" Text="Disapprove" 
             onclick="btnDisapprove_Click1" BackColor="#FF3300" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="Button3" runat="server" Text="Back" onclick="btnBack_Click1" />
     </div>  
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
   <%--  <tr><td colspan="2" class="GridText">&nbsp;<b>ATW Details</b></td></tr> --%>      <tr>
        <td class="GridRows" style="width:20%">ATW Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtATWCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Filed:
         <asp:TextBox runat="server" ID="txtDateFiled" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>        
       <tr>
        <td class="GridRows">Requestor:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorName" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox></td>
       </tr>            
       <tr>
        <td class="GridRows">ATW Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatus" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Head Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtApproverH" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnApproverH" />         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatusH" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatusH" />
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtProcessDateH" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarksH" CssClass="controls" Width="85%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" BackColor="White" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Division Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtApproverD" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnApproverD" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatusD" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatusD" />
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtProcessDateD" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarksD" CssClass="controls" Width="85%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>      
      </table>
     </div>     
     <br />     
     <div class="GridBorder">
      <table width="100%" cellpadding="0">
       <tr><td class="GridColumns">&nbsp;<b>ATW Schedule Details</b></td></tr>       
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid">
           <Columns>	        
            <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="5%">
             <ItemTemplate>
              <asp:CheckBox runat="server" ID="chkApprove" />
              <asp:HiddenField runat="server" ID="hdnStatus" Value='<%#DataBinder.Eval(Container.DataItem, "status")%>' />
              <asp:HiddenField runat="server" ID="hdnATWDCode" Value='<%#DataBinder.Eval(Container.DataItem, "atwdcode")%>' />
             </ItemTemplate>
            </asp:TemplateColumn>                       
            <asp:TemplateColumn HeaderText="Date Start" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="20%">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblDateStart" Text='<%# clsValidator.CheckDate(DataBinder.Eval(Container.DataItem, "datestrt").ToString()).ToString("MM/dd/yy hh:mm tt")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>	           
	           <asp:TemplateColumn HeaderText="Date End" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="20%">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblDateEnd" Text='<%# clsValidator.CheckDate(DataBinder.Eval(Container.DataItem, "dateend").ToString()).ToString("MM/dd/yy hh:mm tt")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>
	           <asp:TemplateColumn HeaderText="Reason" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="85%" MaxLength="255" TextMode="MultiLine" Rows="3" Text='<%#DataBinder.Eval(Container.DataItem, "reason")%>' ReadOnly="true"></asp:TextBox>
	            </ItemTemplate>
	           </asp:TemplateColumn>
	           <asp:TemplateColumn HeaderText="Remarks" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" Width="85%" MaxLength="255" TextMode="MultiLine" Rows="3" Text='<%#DataBinder.Eval(Container.DataItem, "remarks")%>'></asp:TextBox>
	            </ItemTemplate>
	           </asp:TemplateColumn>	                                	           
           </Columns>
          </asp:DataGrid>
         </div>
        </td>
       </tr>
      </table>
     </div>     
     <br />
     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" onclick="btnApprove_Click" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnDisapprove" ImageUrl="~/Support/btnDisapprove.jpg" onclick="btnDisapprove_Click" />
      &nbsp;
      <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
      <%-- <asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" onclick="btnApprove_Click" />--%>
      <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             onclick="btnApprove_Click1" BackColor="#33CC33" ForeColor="White" />
      &nbsp;
     <%-- <asp:ImageButton runat="server" ID="btnDisapprove" ImageUrl="~/Support/btnDisapprove.jpg" onclick="btnDisapprove_Click" />--%>
         <asp:Button ID="btnDisapprove" runat="server" Text="Disapprove" 
             onclick="btnDisapprove_Click1" BackColor="#FF3300" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />--%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click1" />
     </div>     
    </div>
   </td>
  </tr>  
 </table>
</asp:Content>