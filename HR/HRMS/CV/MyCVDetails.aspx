<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="MyCVDetails.aspx.cs" Inherits="HR_HRMS_CV_MyCVDetails" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="cntHRCVMyDetails" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <asp:ScriptManager runat="server" ID="smP"></asp:ScriptManager>
 
 <table width="100%" cellpadding="0" cellspacing="0">
 
<%--  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="../../../CIS/CIS.aspx" class="SiteMap">CIS</a> » 
     <a href="../../HR.aspx" class="SiteMap">HR</a> » 
     <a href="../HRMS.aspx" class="SiteMap">HRMS</a> » 
     <a href="MyCVDetails.aspx" class="SiteMap">My Curriculum Vitae</a>
    </div>        
   </td>
  </tr>--%>
  
<%--  <tr><td style="height:9px;"></td></tr>--%>
 
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     &nbsp;<b><span class="HeaderText">My Curriculum Vitae Details</span></b>
     <br /><br /><br />
     
         <div style="font-size:small;">
      <table frame="void" style="border-color: #FAFAFA">
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
        <td align="center" style="border-color: #FAFAFA">Employee Profile</td>
        <td align="center" style="border-color: #FAFAFA"><a href="MyCVEducation.aspx">Education Background</a></td>
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
       <tr><td colspan="4" class="GridColumns">&nbsp;<b>General Information</b></td></tr>
       <tr>
        <td class="GridRows">Username:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtUsername" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Employee Number:
         <asp:TextBox runat="server" ID="txtEmployeeNumber" CssClass="controls" Width="120px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Name (F/M/L):</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtNameFirst" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>         
         <asp:TextBox runat="server" ID="txtNameMiddle" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
         <asp:TextBox runat="server" ID="txtNameLast" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Type:</td>
        <td class="GridRows">         
         <asp:TextBox runat="server" ID="txtEmployeeType" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>  
       <tr>
        <td class="GridRows">Title:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlTitle" CssClass="controls"></asp:DropDownList>
         &nbsp;
         Suffix: 
         <asp:DropDownList runat="server" ID="ddlSuffix" CssClass="controls"></asp:DropDownList>
        </td>
       </tr>         
       <tr>
        <td class="GridRows">Company:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCompany" CssClass="controls" Width="85%" ReadOnly="true"></asp:TextBox></td>
       </tr> 
       <tr>
        <td class="GridRows">Position:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtPosition" CssClass="controls" Width="300px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Last Update By:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtUpdateBy" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>         
         &nbsp;
         Date:
         <asp:TextBox runat="server" ID="txtUpdateOn" ReadOnly="true" CssClass="controls" Width="200px"></asp:TextBox>         
        </td>
       </tr>         
      </table>       
     </div>          
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="2" class="GridColumns">&nbsp;<b>Personal Information</b></td></tr>
       <tr>
        <td class="GridRows">Nick Name:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtNickName" CssClass="controls" Width="130px" BackColor="White" MaxLength="30"></asp:TextBox>
         &nbsp;
         Gender:
         <asp:TextBox runat="server" ID="txtGender" CssClass="controls" Width="40px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Civil Status:
         <asp:TextBox runat="server" ID="txtCivilStatus" CssClass="controls" Width="70px" ReadOnly="true"></asp:TextBox>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Date of Birth:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDateOfBirth" CssClass="controls" Width="75px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Age:
         <asp:TextBox runat="server" ID="txtAge" CssClass="controls" Width="25px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Place of Birth:
         <asp:TextBox runat="server" ID="txtPlaceOfBirth" CssClass="controls" Width="200px" BackColor="White" MaxLength="50"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Citizenship:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtCitizenship" CssClass="controls" Width="200px" MaxLength="30" BackColor="White"></asp:TextBox></td>
       </tr>  
       <tr>
        <td class="GridRows">Hobbies/Interest:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtHobby" CssClass="controls" Width="400px" BackColor="White" MaxLength="255"></asp:TextBox>
        </td>
       </tr>         
       <tr>
        <td class="GridRows">Height:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtHeight" CssClass="controls" Width="80px" BackColor="White" MaxLength="10"></asp:TextBox>
         &nbsp;
         Weight:
         <asp:TextBox runat="server" ID="txtWeight" CssClass="controls" Width="80px" BackColor="White" MaxLength="10"></asp:TextBox>         
         &nbsp;
         Blood Type:
         <asp:TextBox runat="server" ID="txtBloodType" CssClass="controls" Width="80px" MaxLength="2" BackColor="White"></asp:TextBox>                  
        </td>
       </tr> 
       <tr>
        <td class="GridRows">Languages Spoken:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtLanguage" CssClass="controls" Width="400px" BackColor="White" MaxLength="100"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">SSS ID:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSSS" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Philhealth:
         <asp:TextBox runat="server" ID="txtPhilhealth" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">TIN:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtTIN" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         HDMF:
         <asp:TextBox runat="server" ID="txtHDMF" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">HMO:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtHMO" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Payroll Account Number:
         <asp:TextBox runat="server" ID="txtBankAccount" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>       
       <tr runat="server" id="trFatherName">
        <td class="GridRows" valign="top">Name of Father:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtFatherName" Width="300px" CssClass="controls" BackColor="White" MaxLength="50"></asp:TextBox>
         &nbsp;
         Age:
         <asp:TextBox runat="server" ID="txtFatherAge" CssClass="controls" ReadOnly="true" Width="20px"></asp:TextBox>
        </td>
       </tr>
       <tr runat="server" id="trFatherBirthDate">
        <td class="GridRows" valign="top">Father's Birthdate:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpFatherBirthdate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
       </tr>         
       <tr runat="server" id="trMotherName">
        <td class="GridRows" valign="top">Name of Mother:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtMotherName" Width="300px" CssClass="controls" BackColor="White" MaxLength="50"></asp:TextBox>
         &nbsp;
         Age:
         <asp:TextBox runat="server" ID="txtMotherAge" CssClass="controls" ReadOnly="true" Width="20px"></asp:TextBox>         
        </td>
       </tr>
       <tr runat="server" id="trMotherBirthDate">
        <td class="GridRows" valign="top">Mother's Birthdate:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpMotherBirthdate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
       </tr>       
       <tr runat="server" id="trDependents">
        <td class="GridRows" colspan="2">
         Qualified Dependents:<br /><br />
         <asp:Label runat="server" ID="lblDependent" Text="No record found" Visible="false" Font-Bold="true"></asp:Label>
         <div class="GridBorder" runat="server" id="divDependent">
	         <asp:DataGrid runat="server" ID="dgDependent" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" ondeletecommand="dgDependent_DeleteCommand">
	          <Columns>
	           <asp:TemplateColumn HeaderText="Name" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left">
	            <ItemTemplate>
	             <asp:HiddenField runat="server" ID="hdnListDependentCode" Value='<%#DataBinder.Eval(Container.DataItem, "dpndcode")%>' />
	             <asp:TextBox runat="server" ID="txtListDependentName" Text='<%#DataBinder.Eval(Container.DataItem, "pname")%>' CssClass="controls" BackColor="white" Width="70%" MaxLength="100"></asp:TextBox>
	            </ItemTemplate>
	           </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Birth Date" HeaderStyle-Width="30%" ItemStyle-VerticalAlign="Top">
             <HeaderStyle CssClass="GridColumns" />
             <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
             <ItemTemplate>  
              <cc1:GMDatePicker ID="dtpListDependentBirthDate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue" Date='<%#DataBinder.Eval(Container.DataItem, "brthdate")%>'></cc1:GMDatePicker>
             </ItemTemplate>
            </asp:TemplateColumn>

	           <asp:TemplateColumn HeaderText="Relation" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left">
	            <ItemTemplate>
	             <asp:TextBox runat="server" ID="txtListDependentRelation" Text='<%#DataBinder.Eval(Container.DataItem, "relation")%>' CssClass="controls" BackColor="white" Width="70%" MaxLength="50"></asp:TextBox>
	            </ItemTemplate>
	           </asp:TemplateColumn>
	                     
            <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
             <ItemTemplate>
              <asp:ImageButton id="btnDependentDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton>
             </ItemTemplate>
            </asp:TemplateColumn>            
           </Columns>
          </asp:DataGrid> 
         </div>
         <br />
         <div align="right">          
          <%--<asp:ImageButton runat="server" ID="btnDependentSave" 
           ImageUrl="~/Support/btnSmallSaveChanges.jpg" onclick="btnDependentSave_Click" />
          <asp:ImageButton runat="server" ID="btnDependentReset" 
           ImageUrl="~/Support/btnSmallReset.jpg" onclick="btnDependentReset_Click" />--%>
             <asp:Button ID="btnDependentSave" runat="server" Text="Save Changes"  onclick="btnDependentSave_Click"/>&nbsp;
             <asp:Button ID="btnDependentReset" runat="server" Text="Reset" onclick="btnDependentReset_Click" />
         </div>
         
         <br />
         <br />

         <div class="GridBorder">
          <table width="100%" cellpadding="3" cellspacing="2">
           <tr><td colspan="2" class="GridColumns"><b>&nbsp;&nbsp;Dependent Details</b></td></tr>      
           <tr>
            <td class="GridRows" style="width:20%">Name:</td>
            <td class="GridRows" style="width:80%"><asp:TextBox runat="server" ID="txtDpndName" CssClass="controls" Width="250px" BackColor="White" MaxLength="100"></asp:TextBox></td>
           </tr>
           <tr>
            <td class="GridRows">Relation:</td>
            <td class="GridRows"><asp:TextBox runat="server" ID="txtDpndRelation" CssClass="controls" Width="200px" BackColor="White" MaxLength="30"></asp:TextBox></td>
           </tr>      
           <tr>
            <td class="GridRows">Birthdate:</td>
            <td class="GridRows"><cc1:GMDatePicker ID="dtpDpndBirthdate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" CalendarTheme="Blue"></cc1:GMDatePicker></td>
           </tr>
           <tr>
            <td class="GridRows" colspan="2" align="right">
             <%--<asp:ImageButton runat="server" ID="btnDependentAdd" ImageUrl="~/Support/btnSmallAdd.jpg" onclick="btnDependentAdd_Click" />--%>
                <asp:Button ID="btnDependentAdd" runat="server" Text="Add"  onclick="btnDependentAdd_Click"/>
            </td>
           </tr>      
          </table>
         </div>
        </td>        
       </tr>
       <tr runat="server" id="trSpouseName">
        <td class="GridRows" valign="top">Name of Spouse:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtSpouseName" Width="300px" CssClass="controls" BackColor="White" MaxLength="50"></asp:TextBox>
         &nbsp;
         Age:
         <asp:TextBox runat="server" ID="txtSpouseAge" CssClass="controls" ReadOnly="true" Width="20px"></asp:TextBox>                  
        </td>
       </tr>
       <tr runat="server" id="trSpouseBirthDate">
        <td class="GridRows" valign="top">Spouse' Birthdate:</td>
        <td class="GridRows"><cc1:GMDatePicker ID="dtpSpouseBDate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
       </tr>
       <tr runat="server" id="trChildren">
        <td class="GridRows" colspan="2">
         Children:<br /><br />

           <asp:Label runat="server" ID="lblChildren" Text="No record found" Visible="false" Font-Bold="true"></asp:Label>
           <div class="GridBorder" runat="server" id="divChildren">
	           <asp:DataGrid runat="server" ID="dgChildren" AutoGenerateColumns="false" Width="100%" HeaderStyle-Font-Bold="true" HeaderStyle-Height="20px" BorderStyle="Solid" OnDeleteCommand="dgChildren_DeleteCommand">
	            <Columns>
	             <asp:TemplateColumn HeaderText="Name" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
	              <ItemTemplate>
	               <asp:HiddenField runat="server" ID="hdnListChildCode" Value='<%#DataBinder.Eval(Container.DataItem, "chldcode")%>' />
	               <asp:TextBox runat="server" ID="txtListChildName" Text='<%#DataBinder.Eval(Container.DataItem, "pname")%>' CssClass="controls" BackColor="white" Width="85%" MaxLength="100"></asp:TextBox>
	              </ItemTemplate>
	             </asp:TemplateColumn>

              <asp:TemplateColumn HeaderText="Birth Date" HeaderStyle-Width="35%" ItemStyle-VerticalAlign="Top">
               <HeaderStyle CssClass="GridColumns" />
               <ItemStyle CssClass="GridRows" HorizontalAlign="Center" />            
               <ItemTemplate>  
                <cc1:GMDatePicker ID="dtpListChildBirthDate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue" Date='<%#DataBinder.Eval(Container.DataItem, "brthdate")%>'></cc1:GMDatePicker>
               </ItemTemplate>
              </asp:TemplateColumn>
	                     
              <asp:TemplateColumn HeaderText="Delete" HeaderStyle-CssClass="GridColumns" ItemStyle-CssClass="GridRows" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate><asp:ImageButton id="btnChildDelete" runat="server" CommandName="Delete" AlternateText="[Delete Item]" ImageUrl="~/Support/delete16.png"></asp:ImageButton></ItemTemplate>
              </asp:TemplateColumn>            
             </Columns>
            </asp:DataGrid> 
           </div>
         
           <br />
         
           <div align="right">          
           <%--<asp:ImageButton runat="server" ID="btnChildSave" 
             ImageUrl="~/Support/btnSmallSaveChanges.jpg" onclick="btnChildSave_Click" />
            <asp:ImageButton runat="server" ID="btnChildReset" 
             ImageUrl="~/Support/btnSmallReset.jpg" onclick="btnChildReset_Click" />  --%>
             <asp:Button ID="btnChildSave" runat="server" Text="Save Changes"  onclick="btnDependentSave_Click"/>&nbsp;
             <asp:Button ID="btnChildReset" runat="server" Text="Reset" onclick="btnDependentReset_Click" /> 
           </div>
         
           <br />
         
           <div class="GridBorder">
            <table width="100%" cellpadding="3" cellspacing="2">
             <tr><td colspan="2" class="GridColumns"><b>&nbsp;&nbsp;Children Details</b></td></tr>      
             <tr>
              <td class="GridRows" style="width:20%">Name:</td>
              <td class="GridRows" style="width:80%">
               <asp:TextBox runat="server" ID="txtChildName" CssClass="controls" Width="250px" BackColor="White" ValidationGroup="ChildrenAdd" MaxLength="200"></asp:TextBox>
               <asp:RequiredFieldValidator runat="server" ID="reqChildName" 
                      ControlToValidate="txtChildName" ErrorMessage="<br>[Child name is required]" 
                      ValidationGroup="ChildrenAdd" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
              </td>
             </tr>     
             <tr>
              <td class="GridRows">Birthdate:</td>
              <td class="GridRows"><cc1:GMDatePicker ID="dtpChildBirthdate" runat="server" CssClass="controls" DisplayMode="TextBox" DateFormat="MM/dd/yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker></td>
             </tr>
             <tr>
              <td class="GridRows" colspan="2" align="right">
               <%--<asp:ImageButton runat="server" ID="btnChildAdd" 
                ImageUrl="~/Support/btnSmallAdd.jpg" ValidationGroup="ChildrenAdd" 
                onclick="btnChildAdd_Click" />--%>
                  <asp:Button ID="btnChildAdd" runat="server" Text="Add" ValidationGroup="ChildrenAdd" onclick="btnChildAdd_Click"/>
              </td>
             </tr>      
            </table>
           </div>         
         
        </td>
       </tr>       
      </table>       
     </div>     
     
     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="4" class="GridColumns">&nbsp;<b>Contact Information</b></td></tr>
       <tr>
        <td class="GridRows">Permanent Address:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtAddress1" CssClass="controls" Width="250px" MaxLength="100" BackColor="White"></asp:TextBox>
         &nbsp;
         City:
         <asp:TextBox runat="server" ID="txtCity1" CssClass="controls" Width="200px" MaxLength="30" BackColor="White"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Telephone:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtPhone1" CssClass="controls" Width="150px" BackColor="White" MaxLength="20"></asp:TextBox>         
        </td>
       </tr>      
       <tr>
        <td class="GridRows">Current Address:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtAddress2" CssClass="controls" Width="250px" MaxLength="100" BackColor="White"></asp:TextBox>
         &nbsp;
         City:
         <asp:TextBox runat="server" ID="txtCity2" CssClass="controls" Width="200px" MaxLength="30" BackColor="White"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Telephone:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtPhone2" CssClass="controls" Width="150px" BackColor="White" MaxLength="20"></asp:TextBox>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Primary Cellphone #:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtMobile1" CssClass="controls" Width="150px" BackColor="White" MaxLength="20"></asp:TextBox>
         &nbsp;
         Alternative Cellphone #:
         <asp:TextBox runat="server" ID="txtMobile2" CssClass="controls" Width="150px" BackColor="White" MaxLength="20"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Direct Line:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDirectLine" CssClass="controls" Width="150px" BackColor="White" MaxLength="20"></asp:TextBox>
         &nbsp;
         Local Number:
         <asp:TextBox runat="server" ID="txtLocal" CssClass="controls" Width="50px" MaxLength="4" BackColor="White"></asp:TextBox>
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Fax Number:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtFax" CssClass="controls" Width="150px" BackColor="White" MaxLength="20"></asp:TextBox>         
        </td>
       </tr>              
       <tr>
        <td class="GridRows">Official Email:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtEmailOfficial" CssClass="controls" Width="150px" ReadOnly="true" MaxLength="40"></asp:TextBox>         
         &nbsp;
         Personal Email:
         <asp:TextBox runat="server" ID="txtEmailPersonal" CssClass="controls" Width="150px" BackColor="White" MaxLength="40"></asp:TextBox>
        </td>
       </tr>
       <tr><td class="GridRows" colspan="2"><b>Person To Contact In Case Of Emergency</b></td></tr>
       <tr>
        <td class="GridRows">Name:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtEmergencyName" CssClass="controls" Width="300px" BackColor="White" MaxLength="100"></asp:TextBox>         
        </td>
       </tr>       
       <tr>
        <td class="GridRows">Relation:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtEmergencyRelation" CssClass="controls" Width="150px" BackColor="White" MaxLength="20"></asp:TextBox>         
        </td>
       </tr>
       <tr>
        <td class="GridRows">Phone Number:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtEmergencyPhoneNumber" CssClass="controls" Width="200px" BackColor="White" MaxLength="50"></asp:TextBox>
         &nbsp;&nbsp;
         Cellphone Number:
         <asp:TextBox runat="server" ID="txtEmergencyCellNumber" CssClass="controls" Width="200px" BackColor="White" MaxLength="50"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td class="GridRows">Address:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtEmergencyAddress" CssClass="controls" Width="350px" BackColor="White" MaxLength="100"></asp:TextBox></td>
       </tr>       
      </table>       
     </div>     

     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="4" class="GridColumns">&nbsp;<b>Employment Details</b></td></tr>
       <tr>
        <td class="GridRows">Employment Status:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtEmploymentStatus" CssClass="controls" Width="150px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Division:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDivision" CssClass="controls" Width="350px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">Group:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtGroup" CssClass="controls" Width="350px" ReadOnly="true"></asp:TextBox></td>
       </tr>      
       <tr>
        <td class="GridRows">Department:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtDepartment" CssClass="controls" Width="350px" ReadOnly="true"></asp:TextBox></td>
       </tr>       
       <tr>
        <td class="GridRows">RC:</td>
        <td class="GridRows"><asp:TextBox runat="server" ID="txtRC" CssClass="controls" Width="350px" ReadOnly="true"></asp:TextBox></td>
       </tr>
       <tr>
        <td class="GridRows">Job Grade:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtJGCode" CssClass="controls" Width="60px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Assignment:
         <asp:TextBox runat="server" ID="txtAssignment" CssClass="controls" Width="200px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>             
       <tr>
        <td class="GridRows">Date Hired:</td>
        <td class="GridRows">
         <asp:TextBox runat="server" ID="txtDateHired" CssClass="controls" Width="130px" ReadOnly="true"></asp:TextBox>
         &nbsp;
         Regularization Date:
         <asp:TextBox runat="server" ID="txtDateRegular" CssClass="controls" Width="130px" ReadOnly="true"></asp:TextBox>
        </td>
       </tr>
      </table>       
     </div>

     <br />
     
     <div class="GridBorder">
      <table width="100%" cellpadding="3">
       <tr><td colspan="4" class="GridColumns">&nbsp;<b>Skill and Competencies</b></td></tr>
       <tr>
        <td class="GridRows" style="width:50%">
         Primary:<br />
         <asp:TextBox runat="server" ID="txtSkillPrimary" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="17" BackColor="White"></asp:TextBox>
        </td>
        <td class="GridRows" style="width:50%">
         Secondary:<br />
         <asp:TextBox runat="server" ID="txtSkillSecondary" CssClass="controls" Width="98%" TextMode="MultiLine" Rows="17" BackColor="White"></asp:TextBox>
        </td>
       </tr>
      </table>       
     </div>              
     
     <br /><br />

     <div style="text-align:center;">
      <%--<asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSave.jpg" onclick="btnSave_Click" />--%>
         <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click"/>
      &nbsp;
      <%--<asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Support/btnReset.jpg" onclick="btnReset_Click" />--%>
         <asp:Button ID="btnReset" runat="server" Text="Reset" onclick="btnReset_Click"/>
     </div>     
     
    </div> 
   </td>
  </tr>
 
 </table>
</asp:Content>