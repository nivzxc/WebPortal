<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey02.aspx.cs" Inherits="HR_Survey02" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>HROD Survey Forms - Survey #2</title>
  <style type="text/css" media="screen">
   <!-- #include file="~\MySTIHQ.css" -->
  </style>  
 </head>
 <body>
  <form id="form1" runat="server">
   <br /><br />
    <table width="90%" cellpadding="0" cellspacing="0" class="centermsgbox">    
     <tr>
      <td>
       <asp:DataGrid ID="dgAnswer" runat="server"></asp:DataGrid>
       <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
        <table border="0" width="100%">
         <tr>
          <td>
           <table>
            <tr>
             <td><img src="../Support/Approve32.png" alt="" /></td>
             <td><b><span class="HeaderText">PERFORMANCE MANAGEMENT PRACTICE SURVEY</span></b></td>
            </tr>
           </table>           
          </td>
         </tr>
         <tr><td>&nbsp;</td></tr>
         <tr>
          <td>
           <table width="100%">
            <tr runat="server" id="trError" visible="false">
             <td colspan="4" align="center">
              <div style="background-color:#ffe4e1; border-right: #ff0000 1px solid;	border-top: #ff0000 1px solid;	border-left: #ff0000 1px solid;	border-bottom: #ff0000 1px solid; font-size:small; color:Red;">
               <b>Errors:</b>
               <br />
               <asp:Label runat="server" ID="lblMessage"></asp:Label>
              </div>
             </td>
            </tr>
            <tr><td>&nbsp;</td></tr>           
            <tr>
             <td style="color:#4682b4;font-size:small;">
              This survey will help you and your division/group/department identify essential components relevant to employee’s Performance Management. 
              Questions serve as indicators of important issues pointing to performance appraisal/assessment practices, feedback and interpersonal dynamics that are critical to individual and organizational productivity.
              <br /><br />
              The overall information we gain from this survey will be used to develop a Competency-based Performance Management System for the organization.
              <br /><br /><br />
              <b>Instructions:</b>
              <br /><br />
              1.  	Kindly reply to each statement by clicking the button that closely matches your opinion using the following scale: 
              <br /><br />
              <div class="GridBorder" style="width:50%">
               <table width="100%" class="grid">
                <tr>
                 <td class="GridRows">1 – Agree</td>
                 <td class="GridRows">2 – Neutral</td>
                 <td class="GridRows">3 – Disagree</td>
                </tr>
               </table>
              </div>
              <br />
              2.  This 22-item survey will take a maximum of 20 minutes of your time.
              <br /><br />
              3.	After answering the 22-item survey, please click on the Submit button. Your responses will not be saved and processed until they are submitted.
              <br />              
             </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
             <td style="font-size:small;color:#4682b4;">
              <div class="GridBorder">
               <table width="100%" class="Grid">
                <tr style="font-size:x-small;">
                 <td class="GridColumns"><b>No.</b></td>
                 <td class="GridColumns">&nbsp;</td>
                 <td class="GridColumns" style="width:90px;"><b>Agree</b></td>
                 <td class="GridColumns" style="width:90px;"><b>Neutral</b></td>                 
                 <td class="GridColumns" style="width:90px;"><b>Disagree</b></td>
                </tr>
                <!-- Question #1 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>01.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>I clearly understand the expectations of my job in relation to the mission and business strategies of  company.</td>
                   </tr>                   
                  </table>                  
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad011" GroupName="01" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad012" GroupName="01" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad013" GroupName="01" /></td>
                </tr>
                <!-- Question #2 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>02.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td><img src="../Support/knotes16.png" alt="" /></td>
                    <td>I know what is expected of me on the job.</td>
                   </tr>                   
                  </table>                  
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad021" GroupName="02" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad022" GroupName="02" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad023" GroupName="02" /></td>
                </tr>                
                <!-- Question #3 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>03.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td><img src="../Support/knotes16.png" alt="" /></td>
                    <td>I receive sufficient supervision and feedback to get my job done efficiently.</td>
                   </tr>                   
                  </table>                  
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad031" GroupName="03" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad032" GroupName="03" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad033" GroupName="03" /></td>
                </tr>
                <!-- Question #4 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>04.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td><img src="../Support/knotes16.png" alt="" /></td>
                    <td>In my work, we set goals and objectives to help us achieve our plans.</td>
                   </tr>                   
                  </table>                  
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad041" GroupName="04" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad042" GroupName="04" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad043" GroupName="04" /></td>
                </tr>                 
                <!-- Question #5 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>05.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td><img src="../Support/knotes16.png" alt="" /></td>
                    <td>At work, I feel encouraged to come up with new and better way of doing things.</td>
                   </tr>                   
                  </table>                  
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad051" GroupName="05" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad052" GroupName="05" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad053" GroupName="05" /></td>
                </tr>                
                <!-- Question #6 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center" style="vertical-align:top;"><b>06.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>In my work, my supervisor communicates what is expected of me on the job.</td>
                   </tr>                   
                  </table>                  
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad061" GroupName="06" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad062" GroupName="06" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad063" GroupName="06" /></td>
                </tr>                 
                <!-- Question #7 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>07.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>I am aware that I am accountable for achieving results in the job.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad071" GroupName="07" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad072" GroupName="07" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad073" GroupName="07" /></td>
                </tr>
                <!-- Question #8 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>08.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>My performance appraisal is a fair and adequate measure of my actual performance in the workplace.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad081" GroupName="08" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad082" GroupName="08" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad083" GroupName="08" /></td>
                </tr>                
                <!-- Question #9 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>09.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>My supervisor provides constructive suggestions to improve my performance.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad091" GroupName="09" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad092" GroupName="09" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad093" GroupName="09" /></td>
                </tr>                 
                <!-- Question #10 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>10.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>My colleagues and I cooperate to accomplish the task on hand.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad101" GroupName="10" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad102" GroupName="10" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad103" GroupName="10" /></td>
                </tr>
                <!-- Question #11 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>11.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>My supervisor gathers substantial and quantitative/qualitative information in appraising my performance.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad111" GroupName="11" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad112" GroupName="11" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad113" GroupName="11" /></td>
                </tr>                
                <!-- Question #12 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>12.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>The collected information to appraise my performance is used to improve the overall performance of our group/department.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad121" GroupName="12" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad122" GroupName="12" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad123" GroupName="12" /></td>
                </tr>                 
                <!-- Question #13 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>13.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>We utilize measurable standards in monitoring our performance goals and standards.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad131" GroupName="13" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad132" GroupName="13" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad133" GroupName="13" /></td>
                </tr>
                <!-- Question #14 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>14.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>Is performance actively managed in the following areas?</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows">&nbsp;</td>
                 <td class="GridRows">&nbsp;</td>
                 <td class="GridRows">&nbsp;</td>
                </tr>
                <!-- Question #14A -->
                <tr style="font-size:small;">
                 <td class="GridRows">&nbsp;</td>
                 <td class="GridRows">
                  A. Human Resources (training and development, rewards system)
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad14A1" GroupName="14a" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad14A2" GroupName="14a" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad14A3" GroupName="14a" /></td>
                </tr>                
                <!-- Question #14B -->
                <tr style="font-size:small;">
                 <td class="GridRows">&nbsp;</td>
                 <td class="GridRows">
                  B. Management practices (e.g., communication of Mission, Vision to employees; are projects completed on time?
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad14B1" GroupName="14b" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad14B2" GroupName="14b" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad14B3" GroupName="14b" /></td>
                </tr>                   
                <!-- Question #15 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>15.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>Rewards/promotion in the organization depends on how well the employees perform their jobs. </td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad151" GroupName="15" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad152" GroupName="15" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad153" GroupName="15" /></td>
                </tr>
                <!-- Question #16 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>16.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>The results of the performance appraisal provide an opportunity for employee growth and development.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad161" GroupName="16" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad162" GroupName="16" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad163" GroupName="16" /></td>
                </tr>
                <!-- Question #17 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>17.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>In my group/department, constructive steps are taken to improve work performance.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad171" GroupName="17" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad172" GroupName="17" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad173" GroupName="17" /></td>
                </tr>
                <!-- Question #18 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>18.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>In my group/department, constructive steps are taken to deal with poor employee work performance.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad181" GroupName="18" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad182" GroupName="18" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad183" GroupName="18" /></td>
                </tr>
                <!-- Question #19 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>19.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>The performance appraisal provides an opportunity to communicate my work issues with my supervisor and the management.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad191" GroupName="19" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad192" GroupName="19" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad193" GroupName="19" /></td>
                </tr>                
                <!-- Question #20 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>20.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>The organization sets specific performance standards to be achieved in a specific period of time.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad201" GroupName="20" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad202" GroupName="20" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad203" GroupName="20" /></td>
                </tr>
                <!-- Question #21 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>21.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>The performance standards are communicated and understood at all levels of the organization.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad211" GroupName="21" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad212" GroupName="21" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad213" GroupName="21" /></td>
                </tr>
                <!-- Question #22 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>22.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top"><img src="../Support/knotes16.png" alt="" /></td>
                    <td>The supervisors are trained to manage employee’s work performance.</td>
                   </tr>
                  </table>
                 </td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad221" GroupName="22" /></td>
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad222" GroupName="22" /></td>                 
                 <td class="GridRows" align="center"><asp:RadioButton runat="server" ID="rad223" GroupName="22" /></td>
                </tr>
               </table>
              </div>              
             </td>
            </tr>                               
            <tr><td>&nbsp;</td></tr>                        
            <tr>
             <td colspan="4" align="center">
              <asp:ImageButton runat="server" ID="btnSave" ImageUrl="~/Support/btnSubmit.jpg" OnClick="btnSave_Click" />
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
    <br />
    <br />
  </form>
 </body>
</html>