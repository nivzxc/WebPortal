<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CRSNew.aspx.cs" Inherits="CMD_CRS_CRSNew" %>

<asp:Content ID="cntDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">

 <script language="javascript" type="text/javascript">
  function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal)
  {
   for(i = 0; i < document.forms[0].elements.length; i++)
   {
    elm = document.forms[0].elements[i];   
    if (elm.type == 'checkbox')
    {     
     if (!elm.disabled && elm.name.indexOf(aspCheckBoxID)>= 0)
      elm.checked = checkVal;
    }
   }
  }
 </script>
 
 <table width="100%" cellpadding="0" cellspacing="0">
 <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../CMD.aspx" class="SiteMap">Channel Management</a> » 
     <a href="CRSMenu.aspx" class="SiteMap">Courseware Request</a> » 
     <a href="CRSNew.aspx?schlcode=<%Response.Write(Request.QueryString["schlcode"]); %>" class="SiteMap">Create New Courseware Request</a>
    </div>        
   </td>
  </tr>
  
  <tr><td style="height:9px;"></td></tr>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">Create New Courseware Request</span></b>
     <br />
     <br />
     <br />
     <div class="GridBorder" style="width:99%">
      <table width="100%" cellpadding="3">
							<tr><td class="GridText" colspan="2">&nbsp;<b>Courseware Request Details</b></td></tr>     
       <tr>
        <td class="GridRows" style="width:15%;">Requestor:</td>
        <td class="GridRows" style="width:85%;"><asp:TextBox runat="server" ID="txtUsername" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">School:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtSchlName" ReadOnly="true" CssClass="controls" Width="350px"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Remarks:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRemarks" Width="98%" CssClass="controls" BackColor="white" ValidationGroup="cwr" MaxLength="100"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqRemarks" ControlToValidate="txtRemarks" ErrorMessage="<br>[Required]" Display="Dynamic" ValidationGroup="cwr"></asp:RequiredFieldValidator>
        </td>        
       </tr> 
       <tr>
        <td class="GridRows" style="vertical-align:top;">Attachments:</td>
        <td class="GridRows">         
         <table>
          <tr>
           <td>Details:</td>
           <td>
            <asp:TextBox runat="server" ID="txtAttachDetails" CssClass="controls" BackColor="white" Width="350px" MaxLength="40" ValidationGroup="FileAttach"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvAttachDetails" Display="Dynamic" ValidationGroup="FileAttach" ErrorMessage="<br>[Details Required]" ControlToValidate="txtAttachDetails"></asp:RequiredFieldValidator>
           </td>
           <td rowspan="2">
            &nbsp;
            <asp:ImageButton runat="server" ID="btnAttach" ImageUrl="~/Support/btnAttach.jpg" AlternateText="Attach" ValidationGroup="FileAttach" onclick="btnAttach_Click" />
           </td>
          </tr>
          <tr>
           <td>Upload File:</td>
           <td>
            <asp:FileUpload runat="server" ID="fldAttach" CssClass="controls" BackColor="white" Width="350px" />
           </td>          
          </tr>
         </table>
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgAttachments" AutoGenerateColumns="false" HeaderStyle-Height="20px" ItemStyle-Height="20px" HeaderStyle-Font-Bold="true" Width="100%" OnDeleteCommand="dgAttachments_DeleteCommand">
	          <Columns>
            <asp:TemplateColumn HeaderText="Details" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblDetails" Text='<%#DataBinder.Eval(Container.DataItem, "details")%>'></asp:Label>              
	            </ItemTemplate>
	           </asp:TemplateColumn>	          	           
            <asp:TemplateColumn HeaderText="File Name" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblFilePath" Text='<%#DataBinder.Eval(Container.DataItem, "filepath")%>'></asp:Label>
              <asp:HiddenField runat="server" ID="hdnFileName" Value='<%#DataBinder.Eval(Container.DataItem, "filename")%>' />
	            </ItemTemplate>
	           </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
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
     
     <div class="GridBorder">
      <table width="100%" cellpadding="0">
       <tr>
        <td colspan="2" class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>&nbsp;<b>Requested Courseware</b></td>
           <td style="text-align:right;"><asp:ImageButton runat="server" ID="btnExclude" ImageUrl="~/Support/btnExclude.jpg" OnClick="btnExclude_Click" />&nbsp;</td>
          </tr>
         </table>         
        </td>
       </tr>
       <tr>
        <td class="GridRows">
	        <div class="GridBorder">
	         <asp:DataGrid runat="server" ID="dgRCW" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" ItemStyle-Height="20px" BorderStyle="Solid" AlternatingItemStyle-BackColor="#e4f2ff" ShowFooter="true" FooterStyle-Height="20px" FooterStyle-CssClass="GridColumns" FooterStyle-HorizontalAlign="Right">
	          <Columns>
            <asp:TemplateColumn HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center">
             <HeaderTemplate>
              <input id="chkAll" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkDelete',this.checked)" >
             </HeaderTemplate>
             <ItemTemplate>
              <asp:CheckBox runat="server" ID="chkDelete" />
             </ItemTemplate>
            </asp:TemplateColumn>	          
	           <asp:TemplateColumn HeaderText="Course Title" HeaderStyle-CssClass="GridColumns">
	            <ItemTemplate>
              &nbsp;<asp:Label runat="server" ID="lblCourseDesc" Text='<%#DataBinder.Eval(Container.DataItem, "crsettle")%>'></asp:Label>
              (<asp:Label runat="server" ID="lblCourseCode" Text='<%#DataBinder.Eval(Container.DataItem, "crsecode")%>'></asp:Label>)
              &nbsp;sy <asp:Label runat="server" ID="lblYearTerm" Text='<%#DataBinder.Eval(Container.DataItem, "yearterm")%>'></asp:Label>	                
	            </ItemTemplate>
	           </asp:TemplateColumn>      	           
            <asp:TemplateColumn HeaderText="Curriculum" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblCurrCode" Text='<%#DataBinder.Eval(Container.DataItem, "currcode")%>'></asp:Label>              
	            </ItemTemplate>
	           </asp:TemplateColumn>	          	           
            <asp:TemplateColumn HeaderText="Availability" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblAvailability" Text='<%#DataBinder.Eval(Container.DataItem, "avail")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="#" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
	             &nbsp;<asp:Label runat="server" ID="lblTRequest" Text='<%#DataBinder.Eval(Container.DataItem, "ordernum")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>                     
            <asp:TemplateColumn HeaderText="Charge" HeaderStyle-CssClass="GridColumns" ItemStyle-HorizontalAlign="right">
             <ItemTemplate>
	             <asp:Label runat="server" ID="lblCharge" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("#,##0.00")%>'></asp:Label>&nbsp;
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
     <div style="text-align:center; width:99%;">
      <asp:ImageButton runat="server" ID="btnSend" ImageUrl="~/Support/btnSend.jpg" ValidationGroup="cwr" OnClick="btnSend_Click"/>
     </div>     
    </div>
   </td>
  </tr>
   
  <tr><td style="height:9px;"></td></tr>
   
  <tr>
   <td>  
    <asp:UpdateProgress ID="uppItems" runat="server" DynamicLayout="true">
     <ProgressTemplate>
      <img src="../../Support/msgUpdateProgress.jpg" alt="" />
     </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <div class="GridBorder">       	          
      <table width="100%" cellpadding="0" class="grid">
       <tr>
        <td class="GridText">
         <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
           <td>
            <table>
             <tr>
              <td>&nbsp;<img src="../../Support/additem32.png" alt="" /></td>
              <td>&nbsp;<b>Add Courseware to Requested List</b></td>
             </tr>
            </table>            
           </td>
           <td style="text-align:right;"><asp:ImageButton runat="server" ID="btnInclude" ImageUrl="~/Support/btnInclude.jpg" OnClick="btnInclude_Click" />&nbsp;</td>
          </tr>
         </table>         
        </td>
       </tr>
       <tr>
        <td>
         <asp:UpdatePanel ID="upCWList" runat="server"><ContentTemplate>
         <table width="100%" cellpadding="5" class="grid">
       <tr>
        <td class="GridRows" style="width:20%"><b>Program:</b></td>
        <td class="GridRows" style="width:60%"><asp:DropDownList runat="server" ID="ddlProgram" CssClass="controls" AutoPostBack="true" BackColor="White" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList></td>
       </tr>        
       <tr>
        <td class="GridRows"><b>Curriculum:</b></td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlCurriculum" CssClass="controls" AutoPostBack="true" BackColor="White" OnSelectedIndexChanged="ddlCurriculum_SelectedIndexChanged"></asp:DropDownList>
         &nbsp;
         &nbsp;
         <b>Year-Term:</b>
         &nbsp;
         <asp:DropDownList runat="server" ID="ddlYearTerm" CssClass="controls" AutoPostBack="true" BackColor="White" OnSelectedIndexChanged="ddlYearTerm_SelectedIndexChanged"></asp:DropDownList>         
        </td>
       </tr>
       <tr>      
        <td class="GridRows" colspan="2">    
	        <div class="GridBorder">
	         <asp:DataGrid runat="server" ID="dgCW" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" AllowSorting="true" AlternatingItemStyle-BackColor="#e4f2ff" OnSortCommand="dgCW_SortCommand" >
	          <Columns>
	           <asp:TemplateColumn ItemStyle-Width="5%">
             <HeaderTemplate>
              <input id="chkAll" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkInclude',this.checked)" >
             </HeaderTemplate>
	            <ItemTemplate>	         
	             <asp:CheckBox runat="server" ID="chkInclude" />
	            </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" />
             <HeaderStyle CssClass="GridColumns" />
	           </asp:TemplateColumn>	          
	           <asp:TemplateColumn HeaderText="Course Description" SortExpression="crsettle" ItemStyle-Width="70%">
	            <ItemTemplate>	         
              <asp:HiddenField runat="server" ID="hdnCourseCode" Value='<%#DataBinder.Eval(Container.DataItem, "crsecode")%>' />
	             <asp:Label runat="server" ID="lblCourseDesc" Text='<%#DataBinder.Eval(Container.DataItem, "crsettle")%>'></asp:Label>
	             (<asp:Label runat="server" ID="lblCourseCode" Text='<%#DataBinder.Eval(Container.DataItem, "crsecode")%>'></asp:Label>)
	            </ItemTemplate>
             <ItemStyle Width="55%" />
             <HeaderStyle CssClass="GridColumns" />
	           </asp:TemplateColumn>	        
	           <asp:TemplateColumn HeaderText="Year" SortExpression="yearterm" ItemStyle-Width="5%">
	            <ItemTemplate>	         
	             <asp:Label runat="server" ID="lblYearTerm" Text='<%#DataBinder.Eval(Container.DataItem, "yearterm")%>'></asp:Label>
	            </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" />
             <HeaderStyle CssClass="GridColumns" />
	           </asp:TemplateColumn>        	           
            <asp:TemplateColumn HeaderText="Availability" ItemStyle-Width="15%">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblAvailability" Text='<%#DataBinder.Eval(Container.DataItem, "avail")%>'></asp:Label>
	            </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" />
             <HeaderStyle CssClass="GridColumns" />
	           </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="#" ItemStyle-Width="5%">
             <ItemTemplate>
	             &nbsp;<asp:Label runat="server" ID="lblTRequest" Text='<%#DataBinder.Eval(Container.DataItem, "ordernum")%>'></asp:Label>
	            </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" />
             <HeaderStyle CssClass="GridColumns" />
	           </asp:TemplateColumn>         	           
           </Columns>
           <AlternatingItemStyle BackColor="#E4F2FF" />
           <HeaderStyle Font-Bold="True" Height="20px" />
          </asp:DataGrid>
         </div>
        </td>
       </tr>         
         </table>
         </ContentTemplate></asp:UpdatePanel>
        </td>
       </tr>
      </table>
      
     </div>               
    </div>
   </td>
  </tr> 
  
 </table> 
</asp:Content>