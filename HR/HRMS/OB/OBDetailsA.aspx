<%@ Page Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="OBDetailsA.aspx.cs" Inherits="HR_HRMS_OB_OBDetailsA" %>

<asp:Content ID="cntOvertimeNew" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">

      <script language="javascript" type="text/javascript">            
                function ModalSuccess() {
                    $('#myModalSuccessApproval').modal('show');
                }      
                function ModalDisapprove() {
                    $('#myModaldisapproval').modal('show');
                }
            </script>
    <table width="100%" cellpadding="0" cellspacing="0"> 
 <%-- <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="OBMenu.aspx" class="SiteMap">OB</a> » 
     <a href='OBDetailsA.aspx?obcode=<%Response.Write(Request.QueryString["obcode"]); %>' class="SiteMap">OB Details</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>    
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">     
     <b><span class="HeaderText">OB Details</span></b>
     <br /> 
     <div runat="server" id="divError" class="ErrMsg" visible="false"> 
      <b>Error during update. Please correct your data entries:</b><br /><br />
      <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
     </div> 
     <br />     

     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" onclick="btnApprove_Click" />--%>
         <asp:Button ID="Button1" runat="server" Text="Approve" 
             onclick="btnApprove_Click" BackColor="#33CC33" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" onclick="btnDisApprove_Click" />--%>
         <asp:Button ID="Button2" runat="server" Text="Disapprove"  
             onclick="btnDisapprove_Click" BackColor="#FF3300" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />  --%>
         <asp:Button ID="Button3" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>           
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3" cellspacing="1">
       <%--<tr>
        <td colspan="2" class="GridText">
         <table>
          <tr>
           <td>&nbsp;<img src="../../../Support/Paper22.png" alt="" /></td>
           <td>&nbsp;<b>OB Details</b></td>
          </tr>
         </table>         
        </td>
       </tr>--%>
       <tr>
        <td class="GridRows" style="width:20%">OB Code:</td>
        <td class="GridRows" style="width:80%">
         <asp:TextBox runat="server" ID="txtOBCode" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
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
        <td class="GridRows">Departmemt:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRequestorDepartment" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">OB Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtStatus" CssClass="controls" Width="150px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnStatus" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Reason:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtReason" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="4"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows" style="vertical-align:top;">OB Type:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtOBType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Rendered To:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRenderedTo" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr runat="server" id="trRApprover" visible="false">
        <td class="GridRows">Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRApprover" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnRApprover" />         
        </td>
       </tr>       
       <tr runat="server" id="trRStatus" visible="false">
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtRStatus" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnRStatus" />
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtRProcessDate" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr runat="server" id="trRRemarks" visible="false">
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRRemarks" CssClass="controls" Width="99%" MaxLength="255" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Head Approver:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtHApprover" CssClass="controls" Width="250px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnHApprover" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtHStatus" CssClass="controls" Width="100px" ReadOnly="true"></asp:TextBox>
         <asp:HiddenField runat="server" ID="hdnHStatus" />
         &nbsp;
         Date Processed:
         <asp:TextBox runat="server" ID="txtHProcessDate" CssClass="controls" Width="140px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="vertical-align:top;">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtHRemarks" CssClass="controls" Width="99%" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
       </tr>      
      </table>
     </div>
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="0">
      <tr>
           <%--<td>&nbsp;<img src="../../../Support/Time22.png" alt="" /></td>--%>
           <td class="GridColumns">&nbsp;<b>OB Schedule Details</b></td>
          </tr>
       
       <tr>
        <td class="GridRows">
         <div class="GridBorder">
          <asp:DataGrid runat="server" ID="dgSchedule" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid">
           <Columns>	        
            <asp:TemplateColumn HeaderText="Approve" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridColumns" FooterStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-Width="10%">
             <ItemTemplate>
              <asp:CheckBox runat="server" ID="chkApprove" />
             </ItemTemplate>
            </asp:TemplateColumn>
                       
            <asp:TemplateColumn HeaderText="Time In" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="35%">
             <ItemTemplate>
              <asp:HiddenField runat="server" ID="hdnFocusDate" Value='<%#DataBinder.Eval(Container.DataItem, "focsdate")%>' />
              <asp:HiddenField runat="server" ID="hdnStatus" Value='<%#DataBinder.Eval(Container.DataItem, "pstatus")%>' />
              <asp:HiddenField runat="server" ID="hdnKeyInDate" Value='<%#DataBinder.Eval(Container.DataItem, "keyin")%>' />
              <asp:Label runat="server" ID="lblKeyInDate" Text='<%# clsValidator.CheckDate(DataBinder.Eval(Container.DataItem, "keyin").ToString()).ToString("ddd MMM dd, yyyy hh:mm tt")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>
	           
	           <asp:TemplateColumn HeaderText="Time Out" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="35%">
             <ItemTemplate>
              <asp:HiddenField runat="server" ID="hdnKeyOutDate" Value='<%#DataBinder.Eval(Container.DataItem, "keyout")%>' />
              <asp:Label runat="server" ID="lblKeyOutDate" Text='<%# clsValidator.CheckDate(DataBinder.Eval(Container.DataItem, "keyout").ToString()).ToString("MMM dd, yyyy hh:mm tt")%>'></asp:Label>
	            </ItemTemplate>
	           </asp:TemplateColumn>

	           <asp:TemplateColumn HeaderText="Updated By" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:Label runat="server" ID="lblUpdateBy" Text='<%#DataBinder.Eval(Container.DataItem, "updateby").ToString()%>'></asp:Label>
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
      <%--<asp:ImageButton runat="server" ID="btnApprove" ImageUrl="~/Support/btnApprove.jpg" onclick="btnApprove_Click" />--%>
         <asp:Button ID="btnApprove" runat="server" Text="Approve" 
             onclick="btnApprove_Click" BackColor="#33CC33" ForeColor="White" />
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnDisApprove" ImageUrl="~/Support/btnDisapprove.jpg" onclick="btnDisApprove_Click" />--%>
         <asp:Button ID="btnDisapprove" runat="server" Text="Disapprove"  
             onclick="btnDisapprove_Click" BackColor="#FF3300" ForeColor="White"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Support/btnBack.jpg" onclick="btnBack_Click" />  --%>
         <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click"/>
     </div>     
    </div>
   </td>
  </tr>  
  
 </table>
   <!-- Modal Approve Entry -->
<div id="myModalSuccessApproval" class="modal fade" role="dialog" style="position:fixed; width:100%; margin-top:150px;" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog modal-sm">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="background-color:lightgreen;">
      
        <h4 class="modal-title" style="text-align:center; color:white;">Success!</h4>
      </div>
      <div class="modal-body" style="text-align:center;">
          <br>
            <img src="/Support/approve-icon.png" width="32px" height="32px"/>
          <br>
          <br>
        <p>Overtime Approved successfully!</p>           
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="OBMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>
  </div>
</div>
       <!-- Modal Disapprove Entry -->
<div id="myModaldisapproval" class="modal fade" role="dialog" style="position:fixed; width:100%; margin-top:150px;" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog modal-sm">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="background-color:indianred;">
      
        <h4 class="modal-title" style="text-align:center; color:white;">Disapproved!</h4>
      </div>
      <div class="modal-body" style="text-align:center;">
          <br>
          <img src="/Support/disapprove-thumb.png" width="32px" height="32px"/>
          <br>
          <br>
        <p>You have disapproved the Overtime!</p>   
          <br>
          <br>
           <a  type="button" class="btn btn-default" href="OBMenu.aspx" >Ok, Thanks</a>
      </div>
    </div>
  </div>
</div>



</asp:Content>