<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="InquiryAll.aspx.cs" Inherits="CRM_InquiryAll" %>
<%@ Register Assembly="GMDatePicker" Namespace="GrayMatterSoft" TagPrefix="cc1" %>

<asp:Content ID="conDefault" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="CRMMenu.aspx" class="SiteMap">CRM</a> » 
     <a href="InquiryAll.aspx" class="SiteMap">All Records</a>
    </div>        
   </td>
  </tr>
     
  <tr><td style="height:9px;"></td></tr>
   
  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <b><span class="HeaderText">All Inquiry</span></b>
     <br />
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../Support/Search22.png" alt="" /></td>
           <td>&nbsp;<b>Filter Options</b></td>
          </tr>
         </table>            
        </td>
       </tr> 
       <tr>
        <td class="GridRows" style="width:25%;">Inquiry Category:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlInquiryCategory" CssClass="controls" BackColor="white">
          <asp:ListItem Text="View All" Value="all"></asp:ListItem>
          <asp:ListItem Text="Student File Request" Value="A"></asp:ListItem>
          <asp:ListItem Text="Course/Program Inquiry" Value="D"></asp:ListItem>
          <asp:ListItem Text="Tuition Fee" Value="F"></asp:ListItem>
          <asp:ListItem Text="Suggestion" Value="M"></asp:ListItem>
          <asp:ListItem Text="Complaints" Value="V"></asp:ListItem>
          <asp:ListItem Text="Others" Value="V"></asp:ListItem>
         </asp:DropDownList>
        </td>
       </tr>
       <tr>
        <td class="GridRows" style="width:25%;">Inquiry Status:</td>
        <td class="GridRows">
         <asp:DropDownList runat="server" ID="ddlInquiryStatus" CssClass="controls" BackColor="white">
          <asp:ListItem Text="View All" Value="all"></asp:ListItem>
          <asp:ListItem Text="Pending" Value="A"></asp:ListItem>
          <asp:ListItem Text="For Approval" Value="D"></asp:ListItem>
          <asp:ListItem Text="Answered" Value="F"></asp:ListItem>
         </asp:DropDownList>
        </td>
       </tr>       
       <tr>
        <td class="GridRows" style="width:25%;">Date Filed Bracket:</td>
        <td class="GridRows">
         From:
         <cc1:GMDatePicker ID="dteDateFrom" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker>
         &nbsp;
         To:
         <cc1:GMDatePicker ID="dteDateTo" runat="server" CssClass="controls" DisplayMode="Label" DateFormat="MMMM dd, yyyy" BackColor="white" CalendarTheme="Blue"></cc1:GMDatePicker>
        </td>
       </tr>
       <tr>
        <td colspan="2" class="GridRows" style="text-align:center;">
         <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Support/btnSearch.jpg" />
        </td>
       </tr>
      </table>
     </div>
     <br />
     <div class="GridBorder"> 	          
      <table width="100%" cellpadding="5">
       <tr>
        <td colspan="3" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../Support/viewtext22.png" alt="" /></td>
           <td>&nbsp;<b>List of All Inquiry</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Inquiry Details</b></td>
        <td class="GridColumns" style="width:30%;"><b>Status</b></td>
       </tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approval.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i need to know about....</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For Answering of <a href="http://hq.sti.edu/Userpage/UserPage.aspx?username=mily.opena">Mily Opena</a></td>
							</tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/RedWrite32.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">I would like to know how much is the tuition...</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For approval</td>
							</tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approved.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i have a minor complaint regarding your....</a>
				     <br />Inquiry Category: Complaints
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">Answered</td>
							</tr>							       
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approval.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i need to know about....</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For Answering of <a href="http://hq.sti.edu/Userpage/UserPage.aspx?username=mily.opena">Mily Opena</a></td>
							</tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/RedWrite32.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">I would like to know how much is the tuition...</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For approval</td>
							</tr> 
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approved.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i have a minor complaint regarding your....</a>
				     <br />Inquiry Category: Complaints
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">Answered</td>
							</tr>							       							
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approval.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i need to know about....</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For Answering of <a href="http://hq.sti.edu/Userpage/UserPage.aspx?username=mily.opena">Mily Opena</a></td>
							</tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/RedWrite32.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">I would like to know how much is the tuition...</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For approval</td>
							</tr> 
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approved.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i have a minor complaint regarding your....</a>
				     <br />Inquiry Category: Complaints
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">Answered</td>
							</tr>							       							
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approval.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i need to know about....</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For Answering of <a href="http://hq.sti.edu/Userpage/UserPage.aspx?username=mily.opena">Mily Opena</a></td>
							</tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/RedWrite32.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">I would like to know how much is the tuition...</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For approval</td>
							</tr> 																					
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx"><img src="../Support/approved.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsModerator.aspx" style="font-size:small;">Hello STI, i have a minor complaint regarding your....</a>
				     <br />Inquiry Category: Complaints
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">Answered</td>
							</tr>							       							
       <tr><td class="GridColumns" style="font-size:small;text-align:left;" colspan="3">&nbsp;<b>Page</b>&nbsp;<a href="InquiryAll.aspx">1</a>,&nbsp;<a href="InquiryAll.aspx">2</a>,&nbsp;<a href="InquiryAll.aspx">3</a>,&nbsp;<a href="InquiryAll.aspx">4</a></td></tr>
      </table>           
     </div>
    </div>     
   </td>
  </tr> 
  
 </table>
</asp:Content>