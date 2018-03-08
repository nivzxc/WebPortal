<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nomination.aspx.cs" Inherits="HR_Nomination" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>MySTIHQ Nomination</title>
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
            <td><b><span class="HeaderText">STI Core Values Awards</span></b></td>
           </tr>
          </table>           
         </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
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
         <td>
          <span style="color:#4682b4;font-size:small;"><b>Voting Mechanics</b></span>
          <br />
          <ol style="font-size:small; list-style-type:upper-alpha; color:#4682b4;">
           <li>
            All employees will select individuals based on the admirable attributes they have shown in the following:  
            <ol>
             <li><b>Respect</b> -	We value the contribution, personalities, and voices of individuals, communities, and our public.</li>
             <li><b>Service</b> -	We will not rest until the needs and expectations of customers – internal and external -- are satisfied and surpassed.</li>
             <li><b>Entrepreneurship</b> -	We build innovative and creative businesses sustained by market–driven flexible, and non-bureaucratic organizations.</li>
             <li><b>Malasakit</b> -	We foster a deep concern for and commitment to the upliftment of each other and society.</li>
             <li><b>Teamwork</b> -	While the individual is important, our best is accomplished when the individuals dream and achieve get together.</li>
             <li><b>Meritocracy</b> -	We expect and reward quality output, achievement and performance</li>
             <li><b>Excellence</b> -	We continuously find ways to do things better, to improve ourselves and our company. We increasingly strive for greater heights and accomplishments.</li>
            </ol>
           </li>            
           <li>All Regular employees (except Executives and HROD Manager/HROD Supervisor and EA to the COO) are eligible to vote a maximum of three candidates for their respective division who in their opinion best exemplifies the STI Way.</li>
           <li>Their votes will be tallied by the HROD and results will be forwarded to the ManCom.</li>
           <li>The ManCom deliberates on the tallied results and is responsible for declaring and awarding the the winners.</li>
           <li>There could be a maximum number of seven winners for every division – representing exemplifications of Respect, Service, Entrepreneurship, Malasakit, Teamwork, Meritocracy and Excellence The STI Awards aims to bring to the fore employee’s exemplification of the STI Way – Respect, Service, Entrepreneurship, Malasakit, Teamwork, Meritocracy and Excellence.</li>
          </ol>
         </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
         <td>
          <table>
           <tr>
            <td><img src="../Support/User.png" alt="" /></td>
            <td><span class="HeaderText">Employee Number:</span></td>
            <td>
             <asp:TextBox runat="server" ID="txtEmpNum" CssClass="controls" Font-Size="Small" ForeColor="steelblue" Width="199px" MaxLength="15"></asp:TextBox>
             <asp:RequiredFieldValidator ID="reqEmpNum" ControlToValidate="txtEmpNum" runat="server" ErrorMessage="[Employee Number is required]" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
            <td rowspan="2">
             <asp:ImageButton runat="server" ID="btnValidate" ImageUrl="~/Support/btnValidate.jpg" OnClick="btnValidate_Click" />
            </td>
           </tr>
           <tr>
            <td><img src="../Support/Shout22.png" alt="" /></td>
            <td><span class="HeaderText">Middle Name:</span></td>
            <td>
             <asp:TextBox runat="server" ID="txtMidName" CssClass="controls" Font-Size="Small" ForeColor="steelblue" Width="199px" MaxLength="15"></asp:TextBox>
             <asp:RequiredFieldValidator ID="reqMidName" ControlToValidate="txtMidName" runat="server" ErrorMessage="[Middle Name is required]" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </td>
           </tr>           
          </table>              
         </td>
        </tr>  
        <tr><td>&nbsp;</td></tr>
        <tr runat="server" id="trVote" visible="false">
         <td>
          <table width="100%">
           <tr>
            <td align="center">
             <span class="HeaderText"><b>Respect</b></span>
             <br />
             <table>
              <tr><td>1.</td><td><asp:DropDownList runat="server" ID="ddlRes1" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>2.</td><td><asp:DropDownList runat="server" ID="ddlRes2" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>3.</td><td><asp:DropDownList runat="server" ID="ddlRes3" CssClass="controls" ></asp:DropDownList></td></tr>                      
             </table>            
            </td>
            <td align="center">
             <span class="HeaderText"><b>Service</b></span>
             <br />
             <table>
              <tr><td>1.</td><td><asp:DropDownList runat="server" ID="ddlSer1" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>2.</td><td><asp:DropDownList runat="server" ID="ddlSer2" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>3.</td><td><asp:DropDownList runat="server" ID="ddlSer3" CssClass="controls" ></asp:DropDownList></td></tr>                      
             </table>            
            </td>
            <td align="center">
             <span class="HeaderText"><b>Entrepreneurship</b></span>
             <br />
             <table>
              <tr><td>1.</td><td><asp:DropDownList runat="server" ID="ddlEnt1" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>2.</td><td><asp:DropDownList runat="server" ID="ddlEnt2" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>3.</td><td><asp:DropDownList runat="server" ID="ddlEnt3" CssClass="controls" ></asp:DropDownList></td></tr>                      
             </table>            
            </td>
           </tr>
           <tr><td colspan="3">&nbsp;</td></tr>
           <tr>
            <td align="center">
             <span class="HeaderText"><b>Malasakit</b></span>
             <br />
             <table>
              <tr><td>1.</td><td><asp:DropDownList runat="server" ID="ddlMal1" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>2.</td><td><asp:DropDownList runat="server" ID="ddlMal2" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>3.</td><td><asp:DropDownList runat="server" ID="ddlMal3" CssClass="controls" ></asp:DropDownList></td></tr>                      
             </table>            
            </td>
            <td align="center">
             <span class="HeaderText"><b>Teamwork</b></span>
             <br />
             <table>
              <tr><td>1.</td><td><asp:DropDownList runat="server" ID="ddlTea1" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>2.</td><td><asp:DropDownList runat="server" ID="ddlTea2" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>3.</td><td><asp:DropDownList runat="server" ID="ddlTea3" CssClass="controls" ></asp:DropDownList></td></tr>                      
             </table>            
            </td>
            <td align="center">
             <span class="HeaderText"><b>Meritocracy</b></span>
             <br />
             <table>
              <tr><td>1.</td><td><asp:DropDownList runat="server" ID="ddlMer1" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>2.</td><td><asp:DropDownList runat="server" ID="ddlMer2" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>3.</td><td><asp:DropDownList runat="server" ID="ddlMer3" CssClass="controls" ></asp:DropDownList></td></tr>                      
             </table>            
            </td>
           </tr>
           <tr><td colspan="3">&nbsp;</td></tr>
           <tr>
            <td align="center">
             <span class="HeaderText"><b>Excellence</b></span>
             <br />
             <table>
              <tr><td>1.</td><td><asp:DropDownList runat="server" ID="ddlExc1" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>2.</td><td><asp:DropDownList runat="server" ID="ddlExc2" CssClass="controls" ></asp:DropDownList></td></tr>
              <tr><td>3.</td><td><asp:DropDownList runat="server" ID="ddlExc3" CssClass="controls" ></asp:DropDownList></td></tr>                      
             </table>                      
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
           </tr>
           <tr><td colspan="3">&nbsp;</td></tr>
           <tr><td colspan="3" align="center"><asp:ImageButton runat="server" ID="btnSubmit" ImageUrl="~/Support/btnSubmit.jpg" OnClick="btnSubmit_Click" /></td></tr>
          </table>          
         </td>
        </tr>
       </table>
      </div>
     </td>
    </tr>
   </table>
  </form>
 </body>
</html>