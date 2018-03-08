<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MyCVQualification.aspx.cs" Inherits="HR_HRMS_CV_MyCVQualification" %>

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
     <a href="MyCVQualification.aspx" class="SiteMap">My Professional Qualification</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText">My Professional Qualification</span></b>
     
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
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVEmploymentHistory.aspx">Employment History</a></td>
        <td align="center" style="border-color: #FAFAFA">Professional Qualifications</td>
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
       <tr><td colspan="2" class="GridColumns">&nbsp;<b>Add Professional Qualification</b></td></tr>      
       <tr>
        <td class="GridRows">Qualification:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtQualification" CssClass="controls" Width="250px" BackColor="White" MaxLength="255" ValidationGroup="add"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqQualification" Display="Dynamic" 
                ControlToValidate="txtQualification" Text="<br>[Qualification is required]" 
                ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Inclusive Dates:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDates" CssClass="controls" Width="300px" BackColor="White" MaxLength="50" ValidationGroup="add"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqSchool" Display="Dynamic" 
                ControlToValidate="txtDates" Text="<br>[Inclusive dates is required]" 
                ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>  
       <tr>
        <td class="GridRows">Remarks:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRemarks" CssClass="controls" BackColor="White" Width="70%" MaxLength="255" TextMode="MultiLine" Rows="5"></asp:TextBox></td>
       </tr>         
       <tr><td class="GridRows" colspan="2" align="right">
        <%--<asp:ImageButton runat="server" ID="btnAdd" ImageUrl="~/Support/btnAdd.jpg" ValidationGroup="add" onclick="btnAdd_Click" />--%><asp:Button ID="btnAdd" runat="server" Text="Add"  ValidationGroup="add" onclick="btnAdd_Click" /></td></tr>
          
      </table>       
     </div>      
     <br />
              
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="4" class="GridColumns">&nbsp;<b>Professionl Qualifications</b></td></tr>
       <tr>
        <td class="GridRows">
         <asp:Label runat="server" ID="lblQualificationNoRec" Text="No Records Found!" Visible="false"></asp:Label>
         <div class="GridBorder" runat="server" id="divQualification">
	         <asp:DataGrid runat="server" ID="dgQualifications" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" ondeletecommand="dgQualifications_DeleteCommand">
	          <Columns>
	           <asp:TemplateColumn HeaderText="Qualification Details" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnQualCode" Value='<%#DataBinder.Eval(Container.DataItem, "qualcode")%>' />
	             <table width="99%">
	              <tr><td width="100%"><asp:TextBox runat="server" ID="txtQualification" Text='<%#DataBinder.Eval(Container.DataItem, "qualfctn")%>' CssClass="controls" BackColor="white" Width="70%" MaxLength="255"></asp:TextBox></td></tr>
	              <tr><td><asp:TextBox runat="server" ID="txtRemarks" Text='<%#DataBinder.Eval(Container.DataItem, "remarks")%>' CssClass="controls" BackColor="white" Width="70%" TextMode="MultiLine" Rows="4" MaxLength="255"></asp:TextBox></td></tr>
	             </table>	             
	            </ItemTemplate>
	           </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Inclusive Dates" HeaderStyle-Width="30%" ItemStyle-VerticalAlign="Top">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
             <ItemTemplate>  
              <asp:TextBox runat="server" ID="txtInclusiveDates" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "incldate")%>' Width="70%" MaxLength="50" BackColor="white"></asp:TextBox>
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
         <%--<asp:ImageButton runat="server" ID="btnQualSave" ImageUrl="~/Support/btnSmallSaveChanges.jpg" onclick="btnQualSave_Click" />
         <asp:ImageButton runat="server" ID="btnQualReset" ImageUrl="~/Support/btnSmallReset.jpg" onclick="btnQualReset_Click" />--%>
            <asp:Button ID="btnQualSave" runat="server" Text="Save Changes" onclick="btnQualSave_Click" />
            <asp:Button ID="btnQualReset" runat="server" Text="Reset" onclick="btnQualReset_Click"  />
        </td>
       </tr>
      </table>
     </div>
     

               
    </div>
   </td>
  </tr>    

 </table>
</asp:Content>