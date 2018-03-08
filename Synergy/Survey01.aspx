<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Survey01.aspx.cs" Inherits="Synergy_Survey01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
   <title>The Official STI HQ Website</title>
  <link rel="Stylesheet" type="text/css" href="MySTIHQ.css" />
     <style type="text/css">
         .style1
         {
             height: 36px;
         }
     </style>
 </head>
 <body>
  <form id="form1" runat="server">
   <asp:ScriptManager id="smP" runat="server"></asp:ScriptManager> 
 <asp:UpdatePanel ID="upl" runat="server">
 <ContentTemplate>
    <table width="90%" cellpadding="0" cellspacing="0" class="centermsgbox">    
     <tr>
      <td>
       <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
        <table border="0" width="100%">
         <tr>
          <td>
           <table>            
            <tr>
             <td><img src="../Support/Approve32.png" alt="" /></td>
             <td style="color:#4682b4;"><b><span class="">Synergy Survey Form</span></b></td>
            </tr>
           </table>           
          </td>
         </tr>
         <tr><td>&nbsp;</td></tr>
         <tr>
          <td>
           <table width="100%">
                   
            <tr>
             <td style="color:#4682b4;font-size:small;">
              <font style="font-family: Calibri; font-size: large;"><b>Instruction:</b></font>
              <br />
              <br />
              <font style="font-family: Calibri; font-size: large;">Please select from the following the events you would be interested in participating in the coming sportsfest.<br />
              Moreover, please identify at most 5 events which would be your most preferred.</font>
             </td>
            </tr>
                        
            <tr><td class="style1">
                <div runat="server" style="background-color:#ffe4e1; border-right: #ff0000 1px solid;	border-top: #ff0000 1px solid;	border-left: #ff0000 1px solid;	border-bottom: #ff0000 1px solid; font-size:small; color:Red; width:60%" id="divError" visible= "false">
                   <b>Errors:</b>
                   <br />
                   <asp:Label runat="server" ID="lblError"></asp:Label>
                  </div>
              </td></tr>
            <tr runat="server" id="trVote" visible="true">
             <td style="font-size:small;color:#4682b4; font-family: Calibri;">
              <div class="GridBorder" runat="server" id="divVote">
               <table width="60%" class="grid" border="1" cellpadding="3" cellspacing="2">
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>No.</b></td>
                 <td class="GridRows" >&nbsp;<b>Event</b></td>
                 <td class="GridRows" style="width:50px;" align="center"><b>Interested</b></td>
                 <td class="GridRows" style="width:50px;" align="center"><b>Preferred</b></td>
                </tr>
                <!-- Question #1 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>01.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td>&nbsp;</td>
                    <td>Men's Basketball</td>
                   </tr>                   
                  </table>                  
                 </td>
                 
             
                 <td class="GridRows" align="center">
                     <asp:CheckBox ID="chbx1" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx1_CheckedChanged" />
                    </td>
                    <td class="GridRows" align="center">
                        <asp:CheckBox ID="chbx2" runat="server" Enabled="False" />
                    </td>
                </tr>
                <!-- Question #2 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>02.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td>&nbsp;</td>
                    <td>Women's Basketball</td>
                   </tr>                   
                  </table>                  
                 </td>
                 
             
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx3" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx3_CheckedChanged" />
                    </td>
                    <td class="GridRows2" align="center">
                        <asp:CheckBox ID="chbx4" runat="server" Enabled="False" />
                    </td>
                </tr>                
                <!-- Question #3 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>03.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td>&nbsp;</td>
                    <td>Volleyball</td>
                   </tr>                   
                  </table>                  
                 </td>
                 
               
                 <td class="GridRows" align="center">
                     <asp:CheckBox ID="chbx5" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx5_CheckedChanged" />
                    </td>
                    <td class="GridRows" align="center">
                        <asp:CheckBox ID="chbx6" runat="server" Enabled="False" />
                    </td>
                </tr>
                <!-- Question #4 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>04.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td>&nbsp;</td>
                    <td>Badminton</td>
                   </tr>                   
                  </table>                  
                 </td>
                 
              
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx7" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx7_CheckedChanged" />
                    </td>
                    <td class="GridRows2" align="center">
                        <asp:CheckBox ID="chbx8" runat="server" Enabled="False" />
                    </td>
                </tr>                 
                <!-- Question #5 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>05.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td>&nbsp;</td>
                    <td>Table Tennis</td>
                   </tr>                   
                  </table>                  
                 </td>
               
           
                 <td class="GridRows" align="center">
                     <asp:CheckBox ID="chbx9" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx9_CheckedChanged" />
                    </td>
                      <td class="GridRows" align="center">
                          <asp:CheckBox ID="chbx10" runat="server" Enabled="False" />
                    </td>
                </tr>                
                <!-- Question #6 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center" style="vertical-align:top;"><b>06.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>Billiards</td>
                   </tr>                   
                  </table>                  
                 </td>
                
              
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx11" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx11_CheckedChanged" />
                    </td>
                     <td class="GridRows2" align="center">
                         <asp:CheckBox ID="chbx12" runat="server" Enabled="False" />
                    </td>
                </tr>
                <!-- Question #7 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>07.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>Darts</td>
                   </tr>
                  </table>
                 </td>
                 
              
                 <td class="GridRows" align="center">
                     <asp:CheckBox ID="chbx13" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx13_CheckedChanged" />
                    </td>
                    <td class="GridRows" align="center">
                        <asp:CheckBox ID="chbx14" runat="server" Enabled="False" />
                    </td>
                </tr>
                <!-- Question #8 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>08.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>Scrabble</td>
                   </tr>
                  </table>
                 </td>
                
            
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx15" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx15_CheckedChanged" />
                    </td>
                     <td class="GridRows2" align="center">
                         <asp:CheckBox ID="chbx16" runat="server" Enabled="False" />
                    </td>
                </tr>                
                <!-- Question #9 -->
                <tr style="font-size:small;">
                 <td class="GridRows" align="center"><b>09.</b></td>
                 <td class="GridRows">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>CounterStrike</td>
                   </tr>
                  </table>
                 </td>
                 
            
                 <td class="GridRows" align="center">
                     <asp:CheckBox ID="chbx17" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx17_CheckedChanged" />
                    </td>
                    <td class="GridRows" align="center">
                        <asp:CheckBox ID="chbx18" runat="server" Enabled="False" />
                    </td>
                </tr>                 
                <!-- Question #10 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>10.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>Bomberman</td>
                   </tr>
                  </table>
                 </td>
                 
              
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx19" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx19_CheckedChanged" />
                    </td>
                    <td class="GridRows2" align="center">
                        <asp:CheckBox ID="chbx20" runat="server" Enabled="False" />
                    </td>
                </tr>
                <!-- Question #11 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>11.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>Sudoku</td>
                   </tr>
                  </table>
                 </td>
                 
              
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx21" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx21_CheckedChanged" />
                    </td>
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx22" runat="server" Enabled="False" />
                    </td>
                </tr>  
                <!-- Question #12 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>12.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>Trumps</td>
                   </tr>
                  </table>
                 </td>
                 
             
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx23" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx23_CheckedChanged" />
                    </td>
                    <td class="GridRows2" align="center">
                        <asp:CheckBox ID="chbx24" runat="server" Enabled="False" />
                    </td>
                </tr> 
                <!-- Question #13 -->
                <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>13.</b></td>
                 <td class="GridRows2">
                  <table>
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td>BlackJack</td>
                   </tr>
                  </table>
                 </td>
    
              
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx25" runat="server" AutoPostBack="True" 
                         oncheckedchanged="chbx25_CheckedChanged" />
                    </td>
                 <td class="GridRows2" align="center">
                     <asp:CheckBox ID="chbx26" runat="server" Enabled="False" />
                    </td>
                </tr>   

                <%--Others--%>
                 <tr style="font-size:small;">
                 <td class="GridRows2" align="center"><b>14.</b></td>
                 <td class="GridRows2" colspan="4">
                  <table >
                   <tr>
                    <td valign="top">&nbsp;</td>
                    <td style="width:500px">Others (Please specify other events separated with comma.)&nbsp;<asp:TextBox ID="txtOthers" 
                            runat="server" MaxLength="150" Width="400px"></asp:TextBox></td>
                   </tr>
                  </table>
                 </td>
                
                </tr>              
               </table>
              </div>              
              <br />
                 <div ID="divConfirmation" runat="server" 
                     style="background-color:#ffe4e1; border-right: #ff0000 1px solid;	border-top: #ff0000 1px solid;	border-left: #ff0000 1px solid;	border-bottom: #ff0000 1px solid; font-size:small; color:blue; width:60%" 
                     visible="false">
                     <b>Confirm:</b>
                     <br />
                     <asp:Label ID="lblConfirmation" runat="server" ForeColor="Blue"></asp:Label>
                     <br />
                     <br />
                     <asp:Button ID="btnYess" runat="server" onclick="btnYess_Click" Text="Yes" />
                     &nbsp;<asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="No" />
                 </div>
                 <br />
                 <asp:Button ID="btnSaves" runat="server" onclick="btnSaves_Click" Text="Save" />
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
     </ContentTemplate>
 </asp:UpdatePanel>

    <br />
    <br />
  </form>
 </body>
</html>
