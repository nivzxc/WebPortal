<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamAssessment.aspx.cs" Inherits="Survey_TeamAssessment" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>HROD Survey - Team Assessment</title>
  <link rel="Stylesheet" type="text/css" href="../MySTIHQ.css" />
 </head>
 <body>
  <form id="form1" runat="server">
   <br /><br />
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
             <td><b><span class="HeaderText">Team Assessment</span></b></td>
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
            <tr>
             <td>
              <span style="color:#4682b4;font-size:small;">
               This assessment is a statistical measurement of your impressions of how well your work team is doing, what its strengths and weaknesses are, and where you think it could benefit from some improvement.
               <br /><br />
               A comprehensive question set is presented with a choice of answers for each question on the 1-5 scale, where a score of 1 is the lowest score and a score of 5 is the highest score. Please answer all the questions.
              </span>              
             </td>
            </tr>        
            <tr><td>&nbsp;</td></tr>
            <tr>
             <td style="font-size:small;color:#4682b4;">
              <div class="GridBorder">
               <table width="100%" cellpadding="0">
                <tr>
                 <td>
                  <div class="GridBorder">
                   <asp:DataGrid runat="server" ID="dgSurveyCategory" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid">
                    <ItemStyle Height="25px" VerticalAlign="Middle" Width="100%" />
                    <Columns>
                     <asp:TemplateColumn HeaderText="Team Assessment" HeaderStyle-Height="25" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="DataGridColumns">
                      <ItemTemplate>
                       <table width="100%">
                        <tr>
                         <td>
                          <asp:HiddenField runat="server" ID="hdnCategoryCode" Value='<%#DataBinder.Eval(Container.DataItem, "scatcode")%>' />
                          <asp:HiddenField runat="server" ID="hdnCategoryName" Value='<%#DataBinder.Eval(Container.DataItem, "scatname")%>' />
                          <div class="GridBorder">
                           <asp:DataGrid runat="server" ID="dgItems" AutoGenerateColumns="false" Width="100%" BorderStyle="Solid">
                            <HeaderStyle Height="25" VerticalAlign="Middle" CssClass="DataGridColumns" />
                            <ItemStyle CssClass="DataGridRows" Height="25px" VerticalAlign="Middle" />
                            <AlternatingItemStyle CssClass="DataGridRows2" Height="25px" VerticalAlign="Middle" />
                            <Columns>
                             <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                              <ItemTemplate>
                               <asp:Label runat="server" ID="lblItemNumber" Text='<%#DataBinder.Eval(Container.DataItem, "itemnmbr")%>'></asp:Label>
                               <asp:HiddenField runat="server" ID="hdnItemCode" Value='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
                              </ItemTemplate>
                             </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="70%">
                              <ItemTemplate>
                               <table><tr><td><asp:Label runat="server" ID="lblItemDesc" Text='<%#DataBinder.Eval(Container.DataItem, "itemdesc")%>'></asp:Label></td></tr></table>                               
                              </ItemTemplate>
                             </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="1" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                               <asp:RadioButton runat="server" ID="radOption1" GroupName='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
                              </ItemTemplate>
                             </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="2" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                               <asp:RadioButton runat="server" ID="radOption2" GroupName='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
                              </ItemTemplate>
                             </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="3" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                               <asp:RadioButton runat="server" ID="radOption3" GroupName='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
                              </ItemTemplate>
                             </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="4" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                               <asp:RadioButton runat="server" ID="radOption4" GroupName='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
                              </ItemTemplate>
                             </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="5" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                               <asp:RadioButton runat="server" ID="radOption5" GroupName='<%#DataBinder.Eval(Container.DataItem, "itemcode")%>' />
                              </ItemTemplate>
                             </asp:TemplateColumn>
                            </Columns>
                           </asp:DataGrid>
                          </div>                         
                         </td>
                        </tr>
                       </table>
                      </ItemTemplate>
                     </asp:TemplateColumn>
                    </Columns>
                   </asp:DataGrid>
                  </div>
                 </td>
                </tr>
               </table>
              </div>             
              <br /><br />              
             </td>
            </tr>
            <tr>
             <td align="center">
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