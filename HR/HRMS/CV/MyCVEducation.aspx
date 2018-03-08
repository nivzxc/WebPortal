<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MyCVEducation.aspx.cs" Inherits="HR_HRMS_CV_MyCVEducation" %>

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
     <a href="MyCVEducation.aspx" class="SiteMap">My Education Background</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText">My Education Background</span></b>
     
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
        <td align="center" style="border-color: #FAFAFA">Education Background</td>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVEmploymentHistory.aspx">Employment History</a></td>
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
       <tr><td colspan="2" class="GridColumns">&nbsp;<b>Add Education Background</b></td></tr>
       <tr>
        <td class="GridRows">Level:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlLevel" CssClass="controls" BackColor="White"></asp:DropDownList>
         &nbsp;<asp:CheckBox runat="server" ID="chkComplete" Text="Complete" />
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Course:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCourse" CssClass="controls" Width="250px" BackColor="White" MaxLength="50"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">School:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSchool" CssClass="controls" Width="300px" BackColor="White" MaxLength="100" ValidationGroup="add"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqSchool" Display="Dynamic" 
                ControlToValidate="txtSchool" Text="<br>[School name is required]" 
                ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr>  
       <tr>
        <td class="GridRows">School Address:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtSchoolAddress" CssClass="controls" BackColor="White" Width="70%" MaxLength="100"></asp:TextBox></td>
       </tr>         
       <tr>
        <td class="GridRows">Inclusive Dates:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtInclusiveDates" CssClass="controls" Width="200px" BackColor="White" MaxLength="50" ValidationGroup="add"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ID="reqInclusiveDates" 
                Display="Dynamic" ControlToValidate="txtInclusiveDates" 
                Text="<br>[Inclusive dates is required]" ValidationGroup="add" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
       </tr> 
       <tr>
        <td class="GridRows">Recognition:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRecognition" CssClass="controls" Width="300px" BackColor="White" MaxLength="50"></asp:TextBox></td>
       </tr>
       <tr><td class="GridRows" colspan="2" align="right"><%--<asp:ImageButton runat="server" ID="btnAdd" ImageUrl="~/Support/btnAdd.jpg" onclick="btnAdd_Click" ValidationGroup="add" />--%><asp:Button
               ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" ValidationGroup="add" /></td></tr>
      </table>       
     </div>   
     <br />
         
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="4" class="GridColumns">&nbsp;<b>Education Background</b></td></tr>
       <tr>
        <td class="GridRows">
         <asp:Label runat="server" ID="lblEducationNoRec" Text="No Records Found!" Visible="false"></asp:Label>
         <div class="GridBorder" runat="server" id="divEducation">          
	         <asp:DataGrid runat="server" ID="dgEducation" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" ondeletecommand="dgEducation_DeleteCommand">
	          <Columns>
	           <asp:TemplateColumn HeaderText="Education Details" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnEducCode" Value='<%#DataBinder.Eval(Container.DataItem, "educcode")%>' />
	             <asp:HiddenField runat="server" ID="hdnEducLevel" Value='<%#DataBinder.Eval(Container.DataItem, "educlvl")%>' />
	             <asp:HiddenField runat="server" ID="hdnEducComplete" Value='<%#DataBinder.Eval(Container.DataItem, "complete")%>' />
	             <table width="99%">
	              <tr>
	               <td>Level:</td>
	               <td width="100%">
	                <asp:DropDownList runat="server" ID="ddlLevel" CssClass="controls" BackColor="White"></asp:DropDownList>
	                &nbsp;<asp:CheckBox runat="server" ID="chkEducComplete" Text="Complete" />
	               </td>
	              </tr>
	              <tr><td>Course:</td><td><asp:TextBox runat="server" ID="txtEducCourse" Text='<%#DataBinder.Eval(Container.DataItem, "course")%>' CssClass="controls" BackColor="white" Width="70%" MaxLength="50"></asp:TextBox></td></tr>
	              <tr><td>Dates:</td><td><asp:TextBox runat="server" ID="txtEducDates" Text='<%#DataBinder.Eval(Container.DataItem, "incldate")%>' CssClass="controls" BackColor="white" Width="50%" MaxLength="50"></asp:TextBox></td></tr>
	              <tr><td>Recognition:</td><td><asp:TextBox runat="server" ID="txtEducRecognition" Text='<%#DataBinder.Eval(Container.DataItem, "recogntn")%>' CssClass="controls" BackColor="white" Width="98%" MaxLength="50"></asp:TextBox></td></tr>
	             </table>	             
	            </ItemTemplate>
	           </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="School and Address" HeaderStyle-Width="30%" ItemStyle-VerticalAlign="Top">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
             <ItemTemplate>  
              <table width="100%">
               <tr><td><asp:TextBox runat="server" ID="txtSchool" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "schlname")%>' Width="70%" MaxLength="100" BackColor="white"></asp:TextBox></td></tr>
               <tr><td><asp:TextBox runat="server" ID="txtSchoolAddress" CssClass="controls" Text='<%#DataBinder.Eval(Container.DataItem, "schladdr")%>' Width="70%" MaxLength="100" BackColor="white" TextMode="MultiLine" Rows="3"></asp:TextBox></td></tr>
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
            <asp:Button ID="btnSave" runat="server" Text="Save"  onclick="btnSave_Click"/>&nbsp;
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