<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Master/Default.master" CodeFile="CRMMenu.aspx.cs" Inherits="CRM_CRMMenu" %>


<asp:Content ID="conMRCFMenu" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../Default.aspx" class="SiteMap">Home</a> » 
     <a href="CRMMenu.aspx" class="SiteMap">CRM</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Inquiry For Answering</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Inquiry For Answering</b></td>
          </tr>
         </table>           
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Inquiry Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsDepartment.aspx"><img src="../Support/approval.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsDepartment.aspx" style="font-size:small;">Hello STI, i need to know about....</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For Your Answering</td>
							</tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetailsDepartment.aspx"><img src="../Support/approval.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetailsDepartment.aspx" style="font-size:small;">I would like to know how much is the tuition...</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For Your Answering</td>
							</tr>							
       <tr><td colspan="3" class="GridColumns"><a href="InquiryAll.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  
  
  <tr><td style="height:9px"></td></tr>
   
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Inquiry For Answering and Approval</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Inquiry For Answering and Approval</b></td>
          </tr>
         </table>            
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Inquiry Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
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
								<td class="GridRows">For your approval</td>
							</tr>
       <tr><td class="GridColumns" colspan="3"><a href="InquiryAll.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>            
     </div>         
    </div>
   </td>
  </tr> 
  <tr><td style="height:9px"></td></tr>

  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Recent Inquiry</span></b>
     <br />
     <br />
     <div class="GridBorder">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td colspan="4" align="center" class="GridText">
         <table>
          <tr>
           <td><img src="../Support/Pen22.png" alt="" /></td>
           <td>&nbsp;<b>List of Recent Inquiry</b></td>
          </tr>
         </table>           
        </td>
       </tr>
       <tr>
        <td class="GridColumns" style="width:5%;">&nbsp;</td>
        <td class="GridColumns" style="width:60%;"><b>Inquiry Details</b></td>
        <td class="GridColumns" style="width:35%;"><b>Status</b></td>
       </tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetails.aspx"><img src="../Support/approval.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetails.aspx" style="font-size:small;">Hello STI, i need to know about....</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For Answering of <a href="http://hq.sti.edu/Userpage/UserPage.aspx?username=mily.opena">Mily Opena</a></td>
							</tr>
				   <tr>
				    <td class="GridRows">
				     <a href="InquiryDetails.aspx"><img src="../Support/RedWrite32.png" alt="You need to process this request" /></a>
				    </td>
				    <td class="GridRows">
				     <a href="InquiryDetails.aspx" style="font-size:small;">I would like to know how much is the tuition...</a>
				     <br />Inquiry Category: Course/Program Inquiry
									<br>Inquirer: Juan Dela Cruz
									<br>Date Filed: September 99, 9999
								</td>
								<td class="GridRows">For approval</td>
							</tr>
       <tr><td colspan="3" class="GridColumns"><a href="InquiryAll.aspx" style="font-size:small;">[Browse All Records]</a></td></tr>
      </table>
     </div>         
    </div>
   </td>
  </tr>  

  <tr><td style="height:9px"></td></tr>      
  
  <tr>
   <td> 
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Inquiry Summary</span></b>
     <br />
     <br />
     <div class="GridBorder" style="width:300px">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td class="GridColumns" colspan="2"><b>Inquiry Status Summary</b></td>
       </tr>      
				   <tr>
				    <td class="GridRows">Pending Inquiry:</td>
				    <td class="GridRows">20</td>
							</tr>
				   <tr>
				    <td class="GridRows">Answered Inquiry For Approval:</td>
				    <td class="GridRows">30</td>
							</tr>							
				   <tr>
				    <td class="GridRows">Answered Inquiry:</td>
				    <td class="GridRows">40</td>
							</tr>							
				   <tr>
				    <td class="GridRows"><b>Today:</b></td>
				    <td class="GridRows"><b>90</b></td>
							</tr>							
      </table>
     </div>         
     <br />
     <br />
     <div class="GridBorder" style="width:300px">
      <table width="100%" cellpadding="5" class="Grid">
       <tr>
        <td class="GridColumns" colspan="2"><b>Inquiry Category Summary</b></td>
       </tr>      
				   <tr>
				    <td class="GridRows">Student File Request:</td>
				    <td class="GridRows">10</td>
							</tr>
				   <tr>
				    <td class="GridRows">Course/Program Inquiry:</td>
				    <td class="GridRows">20</td>
							</tr>
				   <tr>
				    <td class="GridRows">Tuition Fee:</td>
				    <td class="GridRows">30</td>
							</tr>
				   <tr>
				    <td class="GridRows">Suggestion:</td>
				    <td class="GridRows">40</td>
							</tr>							
				   <tr>
				    <td class="GridRows">Complaints:</td>
				    <td class="GridRows">50</td>
							</tr>
				   <tr>
				    <td class="GridRows">Others:</td>
				    <td class="GridRows">60</td>
							</tr>								
				   <tr>
				    <td class="GridRows"><b>Total:</b></td>
				    <td class="GridRows"><b>210</b></td>
							</tr>								
      </table>
     </div>     
    </div>
   </td>
  </tr>  

  <tr><td style="height:9px"></td></tr>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <table>
      <tr><td><b><span class="HeaderText">About Inquiry System</span></b></td></tr>
      <tr><td>&nbsp;</td></tr>
      <tr>
       <td>
        <table>
         <tr>
          <td><img src="../Support/notes48.png" alt="" /></td>
          <td valign="middle"><a href="" style="font-size:small;" target="_blank">View Inquiry System Policy</a></td>
         </tr>
        </table>
       </td>
      </tr>
      <tr>
       <td>
        <table>
         <tr>
          <td><img src="../Support/user2.png" alt="" /></td>
          <td valign="middle" style="font-size:small;color:#4169e1;">
           <table>
            <tr><td>For comments and concerns, you may contact our procurement manager:</td></tr>
            <tr><td>Mr. Francisco E. Jumadiao</td></tr>
            <tr><td>Email: fjumadiao@stihq.net</td></tr>
            <tr><td>Contact no.: 8878447 loc. 6830</td></tr>
           </table>              
          </td>
         </tr>
        </table>
       </td>
      </tr>      
     </table>    
    </div>        
   </td>
  </tr>
     
 </table>  
</asp:Content>