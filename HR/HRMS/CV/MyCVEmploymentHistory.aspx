﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MyCVEmploymentHistory.aspx.cs" Inherits="HR_HRMS_CV_MyCVEmploymentHistory" %>

<asp:Content ID="cntMyEducation" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">
 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="MyCVDetails.aspx" class="SiteMap">My Curriculum Vitae</a> » 
     <a href="MyCVEmploymentHistory.aspx" class="SiteMap">My Employment History</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText">My Employment History</span></b>
     
     <br /><br /><br />

     <div style="font-size:small;">
      <table style="border-color: #FAFAFA">
       <tr>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/personal48.png" alt="" /></td>
        <td rowspan="5" style="border-color: #FAFAFA">&nbsp;</td>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/education48.png" alt="" /></td>
        <td rowspan="5" style="border-color: #FAFAFA">&nbsp;</td>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/Time48.png" alt="" /></td>
        <td rowspan="5" style="border-color: #FAFAFA">&nbsp;</td>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/contact48.png" alt="" /></td>        
       </tr>             
       <tr>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVDetails.aspx">Employee Profile</a></td>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVEducation.aspx">Education Background</a></td>
        <td align="center" style="border-color: #FAFAFA">Employment History</td>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVQualification.aspx">Professional Qualifications</a></td>
       </tr>
       <tr><td colspan="4" style="border-color: #FAFAFA">&nbsp;</td></tr>
       <tr>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/star48.png" alt="" /></td>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/Paper48.png" alt="" /></td>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/AppHead48.png" alt="" /></td>
        <td align="center" style="border-color: #FAFAFA"><img src="../../../Support/Speak48.png" alt="" /></td>        
       </tr>              
       <tr>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVAchievement.aspx">Achievement and Awards</a></td>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVResearch.aspx">Research and Publication</a></td>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVAffiliation.aspx">Professional Affiliation</a></td>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVTraining.aspx">Trainings Attended</a></td>
       </tr>
      </table>          
     </div>
          <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="2" class="GridColumns">&nbsp;<b>Add Employment History</b></td></tr>
       <tr>
        <td class="GridRows">Position:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtPosition" CssClass="controls" Width="250px" BackColor="White" MaxLength="50"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqPosition" Display="Dynamic" 
                ControlToValidate="txtPosition" Text="<br>[Position is required]" 
                ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Responsibility:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtResponsibility" CssClass="controls" Width="250px" BackColor="White" MaxLength="255"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqResponsibility" 
                Display="Dynamic" ControlToValidate="txtResponsibility" 
                Text="<br>[Responsibility is required]" ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>         
       </tr>
       <tr>
        <td class="GridRows">Status:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlStatus" CssClass="controls" BackColor="White"></asp:DropDownList>
        </td>         
       </tr>       
       <tr>
        <td class="GridRows">Inclusive Dates:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtInclusiveDates" CssClass="controls" Width="200px" BackColor="White" MaxLength="50" ValidationGroup="add"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqInclusiveDates" 
                Display="Dynamic" ControlToValidate="txtInclusiveDates" 
                Text="<br>[Inclusive Dates is required]" ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>         
        </td>
       </tr>  
       <tr>
        <td class="GridRows">Company Name:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtCompanyName" CssClass="controls" BackColor="White" Width="80%" MaxLength="100"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqCompanyName" Display="Dynamic" 
                ControlToValidate="txtCompanyName" Text="<br>[Company name is required]" 
                ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>         
       <tr>
        <td class="GridRows">Contact Number:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCompanyContact" CssClass="controls" Width="150px" BackColor="White" MaxLength="50" ValidationGroup="add"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Address:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCompanyAddress" CssClass="controls" Width="85%" BackColor="White" MaxLength="100"></asp:TextBox></td>
       </tr>
       <tr><td class="GridRows" colspan="2" align="right"><%--<asp:ImageButton runat="server" ID="btnAdd" ImageUrl="~/Support/btnAdd.jpg" ValidationGroup="add" onclick="btnAdd_Click" />--%><asp:Button
               ID="btnAdd" runat="server" Text="Add"  ValidationGroup="add" onclick="btnAdd_Click"/></td></tr>
      </table>       
     </div>  
     <br />
         
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="4" class="GridColumns">&nbsp;<b>Employment History</b></td></tr>
       <tr>
        <td class="GridRows">
         <asp:Label runat="server" ID="lblHistoryNoRec" Text="No Records Found!" Visible="false"></asp:Label>
         <div class="GridBorder" runat="server" id="divHistory">          
	         <asp:DataGrid runat="server" ID="dgHistory" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" ondeletecommand="dgHistory_DeleteCommand">
	          <Columns>
	           <asp:TemplateColumn HeaderText="Employment Details" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="55%" ItemStyle-HorizontalAlign="Left">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnEmhsCode" Value='<%#DataBinder.Eval(Container.DataItem, "emhscode")%>' />
	             <asp:HiddenField runat="server" ID="hdnEsttCode" Value='<%#DataBinder.Eval(Container.DataItem, "esttcode")%>' />
	             <table width="99%">
	              <tr>
	               <td>Position:</td>
	               <td width="100%"><asp:TextBox runat="server" ID="txtPosition" Text='<%#DataBinder.Eval(Container.DataItem, "position")%>' CssClass="controls" BackColor="white" Width="98%" MaxLength="50"></asp:TextBox></td>
	              </tr>
	              <tr>
	               <td style="vertical-align:top;">Responsibility:</td>
	               <td><asp:TextBox runat="server" ID="txtResponsibility" Text='<%#DataBinder.Eval(Container.DataItem, "rspnsblt")%>' CssClass="controls" BackColor="white" Width="98%" TextMode="MultiLine" Rows="4" MaxLength="255"></asp:TextBox></td>
	              </tr>
	              <tr>
	               <td style="vertical-align:top;">Status:</td>
	               <td><asp:DropDownList runat="server" ID="ddlStatus" CssClass="controls" BackColor="White"></asp:DropDownList></td>
	              </tr>	              
	              <tr>
	               <td>Inc Dates:</td>
	               <td><asp:TextBox runat="server" ID="txtInclusiveDates" Text='<%#DataBinder.Eval(Container.DataItem, "incldate")%>' CssClass="controls" BackColor="white" Width="50%" MaxLength="50"></asp:TextBox></td>
	              </tr>
	             </table>	             
	            </ItemTemplate>
	           </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Company Details" HeaderStyle-Width="40%" ItemStyle-VerticalAlign="Top">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
             <ItemTemplate>  
              <table width="100%">
               <tr>
                <td>Company:</td>
                <td><asp:TextBox runat="server" ID="txtCompany" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "compname")%>' Width="98%" MaxLength="100" BackColor="white"></asp:TextBox></td>
               </tr>
               <tr>
                <td style="vertical-align:top;">Contact:</td>
                <td><asp:TextBox runat="server" ID="txtContact" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "compcont")%>' Width="98%" MaxLength="40" BackColor="white"></asp:TextBox></td>
               </tr>
               <tr>
                <td style="vertical-align:top;">Address:</td>
                <td><asp:TextBox runat="server" ID="txtAddress" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "compaddr")%>' Width="98%" BackColor="white" TextMode="MultiLine" Rows="3" MaxLength="100"></asp:TextBox></td>
               </tr>               
              </table>              
             </ItemTemplate>
            </asp:TemplateColumn>
          
            <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>
            </asp:TemplateColumn>
            
           </Columns>
          </asp:DataGrid> 
         </div>        
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="text-align:right;">
         <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" onclick="btnSave_Click" />
         <asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Support/btnReset.jpg" onclick="btnReset_Click" />--%>
            <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click"/>&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="Reset"  onclick="btnReset_Click"/>
        </td>
       </tr>
      </table>       
     </div>
     
    
               
    </div>
   </td>
  </tr>    

 </table>
</asp:Content>