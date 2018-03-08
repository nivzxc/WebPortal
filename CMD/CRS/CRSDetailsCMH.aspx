<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CRSDetailsCMH.aspx.cs" Inherits="CMD_CRS_CRSDetailsCMH" %>

<asp:Content ID="cntCrsDetailsCMH" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">

 <script language="javascript" type="text/javascript">
  function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal)
  {
   for(i = 0; i < document.forms[0].elements.length; i++)
   {
    elm = document.forms[0].elements[i];   
    if (elm.type == 'checkbox')
    {     
     if (elm.name.indexOf(aspCheckBoxID)>= 0)    
      elm.checked = checkVal;
    }
   }
  }
 </script>

 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="CRSMenu.aspx" class="SiteMap">Courseware Request</a> » 
     <a href="CRSDetailsCMH.aspx?crscode=<%Response.Write(Request.QueryString["crscode"]); %>" class="SiteMap">Courseware Request Details</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Courseware Request System</span></b>
     <br />
     <br />     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" class="grid">      
       <tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>Courseware Request Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>      
       <tr>
        <td class="GridRows" style="width:20%">CRS Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtCrsCode" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Date Requested:
         &nbsp;
         <asp:TextBox runat="server" ID="txtDateReq" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>              
       <tr>
        <td class="GridRows">Requested By:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtCMName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>        
         <asp:HiddenField ID="hdnCMName" runat="server" />         
        </td>
       </tr>
       <tr>
        <td class="GridRows">School:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSchlName" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField ID="hdnSchlCode" runat="server" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" Width="98%" ReadOnly="true" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">CM Head:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCMHName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCMHRem" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="3" BackColor="white"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Courseware Coordinator:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtCCName" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnCCCode" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCCRem" CssClass="controls" Width="98%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Attachments:</td>
        <td class="GridRows"><asp:Label runat="server" ID="lblAttachments" Font-Size="Small"></asp:Label></td>       
       </tr>
      </table>
     </div>    
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="0" class="grid">
       <tr>
        <td colspan="2" class="GridText">
         <table cellpadding="3">
          <tr>
           <td>&nbsp;<img src="../../Support/cart32.png" alt="" /></td>
           <td>&nbsp;<b>Requested Courseware Materials</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridRows">
	        <div class="GridBorder">
	         <asp:DataGrid runat="server" ID="dgRCW" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" ShowFooter="true" FooterStyle-Height="20px">
	          <Columns>          	           	           	          	           
	           <asp:TemplateColumn HeaderText="Requested Courseware Details" HeaderStyle-CssClass="GridColumns" ItemStyle-Width="50%">
	            <ItemTemplate>
	             <table cellpadding="2" cellspacing="2">
	              <tr>
	               <td>	                
	                <a href="#" onclick="window.open('CRSDispatchDetailsCMH.aspx?crscode=<%Response.Write(Request.QueryString["crscode"]);%>&crsecode=<%#DataBinder.Eval(Container.DataItem, "crsecode")%>',null,'height=600,width=600,status=yes,toolbar=no,menubar=no,location=no,scrollbars=1')"><%#DataBinder.Eval(Container.DataItem, "crsettle")%></a>
	                (<asp:Label runat="server" ID="lblCourseCode" Text='<%#DataBinder.Eval(Container.DataItem, "crsecode")%>'></asp:Label>)
	                [sy <asp:Label runat="server" ID="lblYearTerm" Text='<%#DataBinder.Eval(Container.DataItem, "yearterm")%>'></asp:Label>]
	               </td>
	              </tr>
	              <tr><td>Availability: <asp:Label runat="server" ID="lblAvailability" Text='<%#DataBinder.Eval(Container.DataItem, "avail")%>'></asp:Label></td></tr>
	             </table>	             
	            </ItemTemplate>
	           </asp:TemplateColumn>		           
           
	           <asp:TemplateColumn HeaderText="Curriculum" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
	            <ItemTemplate>
	             <asp:Label runat="server" ID="lblCurrCode" Text='<%#DataBinder.Eval(Container.DataItem, "currcode")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>	                                                     	           
	           
            <asp:TemplateColumn HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center" HeaderText="Status" ItemStyle-Width="20%">
             <ItemTemplate>
              <asp:HiddenField runat="server" ID="hdnPStatus" Value='<%#DataBinder.Eval(Container.DataItem, "pstatus")%>' />
              <asp:Label runat="server" ID="lblPStatus" Text='<%#DataBinder.Eval(Container.DataItem, "pstatusd")%>' Visible="false"></asp:Label>
               <asp:DropDownList runat="server" ID="ddlEndorse" CssClass="controls" BackColor="white">
               <asp:ListItem Text="Pending" Value="F"></asp:ListItem>
               <asp:ListItem Text="Endorse" Value="E"></asp:ListItem>               
               <asp:ListItem Text="Disapprove" Value="D"></asp:ListItem>
              </asp:DropDownList>                
             </ItemTemplate>             
            </asp:TemplateColumn>	           

	           <asp:TemplateColumn HeaderText="#" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
	            <ItemTemplate>
	             <asp:Label runat="server" ID="lblTRequest" Text='<%#DataBinder.Eval(Container.DataItem, "ordernum")%>'></asp:Label>	             
	            </ItemTemplate>
	           </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="right" HeaderText="Charge" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblPrice" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price").ToString()).ToString("##,##0.00") %>'></asp:Label>
              &nbsp;
             </ItemTemplate>             
            </asp:TemplateColumn>	       
                       
           </Columns>
          </asp:DataGrid>
         </div>
         <div style="width:100%;text-align:right;" runat="server" id="divSelection">
          <asp:Button runat="server" ID="btnEndorseAll" Text="Endorse All" Font-Size="X-Small" OnClick="btnEndorseAll_Click" />
          <asp:Button runat="server" ID="btnPendingAll" Text="Pending All" Font-Size="X-Small" OnClick="btnPendingAll_Click" />
          <asp:Button runat="server" ID="btnDisapprove" Text="Disapprove All" Font-Size="X-Small" OnClick="btnDisapprove_Click" />
         </div>
        </td>
       </tr>       
      </table>
     </div>
           
     <div style="text-align:center;" id="divButton" runat="server">
      <br />     
      <asp:ImageButton runat="server" ID="btnEndorse" ImageUrl="~/Support/btnProcess.jpg" OnClick="btnEndorse_Click" />
     </div>
    </div>      
   </td>
  </tr>
 
 </table>
</asp:Content>