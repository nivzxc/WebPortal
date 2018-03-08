<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true" CodeFile="Userpage.aspx.cs" Inherits="Userpage_Userpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<script type="text/javascript" language="javascript">
  <!--
    function AddSmiley(strSmiley) {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + strSmiley;
    }

    function AddBold() {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + "[b][/b]";
    }

    function AddItalics() {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + "[i][/i]";
    }

    function AddUnderline() {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + "[u][/u]";
    }
    function AddUrl() {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + "[url][/url]";
    }
    function AddImage() {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + "[img][/img]";
    }
    function AddSize() {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + "[size][/size]";
    }
    function AddColor() {
        document.getElementById("<%= txtPost.ClientID %>").value = document.getElementById("<%= txtPost.ClientID %>").value + "[color][/color]";
    }           
  -->
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="userborder">
        <br/>
        <div class="container-fluid" style="background-color:; color:black;">

            <b><span class="HeaderText">User Profile</span></b>

        </div>

        <br />
        <div class="row container-fluid" id="UserInfo">       
      
           <div class="col-xs-5 container" style="background-color:;">           
                <asp:Image runat="server" ID="imgRealPic" Height="273px" Width="255px" BorderStyle="Solid" BorderWidth="1" />
           </div>
           <div class="col-xs-7" style="background-color:;">               
                   <table width="100%" cellpadding="5" class="grid">
                     <tr>
                      <td class="GridColumns" colspan="2"><b><% Response.Write(Request.QueryString["username"]);%>'s Info</b></td>
                     </tr>
                     <tr>
                      <td class="GridRows">Name:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblName"></asp:Label></td>
                     </tr>
                     <tr>
                      <td class="GridRows">Position:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblTitle"></asp:Label></td>
                     </tr> 
                     <tr>
                      <td class="GridRows">Division:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblDivision"></asp:Label></td>
                     </tr>
                     <%--<tr>
                      <td class="GridRows">Group:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblGroup"></asp:Label></td>
                     </tr>  --%>           
                     <tr>
                      <td class="GridRows">Department:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblDepartment"></asp:Label></td>
                     </tr>                        
                     <tr>
                      <td class="GridRows">E-Mail:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblEmail"></asp:Label></td>
                     </tr>
                     <tr>
                      <td class="GridRows">Direct Line:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblDirectLine"></asp:Label></td>
                     </tr>
                     <tr>
                      <td class="GridRows">Local:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblLocal"></asp:Label></td>
                     </tr>                          
                     <tr>
                      <td class="GridRows">Birth Date:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblBirthDate"></asp:Label></td>
                     </tr>                         
                     <tr>
                      <td class="GridRows" valign="top">Hobbies:</td>
                      <td class="GridRows"><asp:Label runat="server" ID="lblHobbies"></asp:Label></td>
                     </tr>             
                    </table>                    
           </div>            
        </div>

        <br />
        <br />
        <div id="GuestBookEntry">

     <table width="100%">
      <tr>
       <td>        
        <table cellpadding="2" cellspacing="2" style="border-color: #FAFAFA;">
         <tr>
<%--          <td><asp:Image runat="server" ID="imgAvatar" /></td>--%>
          <td style="border-color: #FAFAFA;"><b><span class="HeaderText">&nbsp;<%Response.Write(Request.QueryString["username"]);%>'s Guestbook Entries</span></b></td>
         </tr>
        </table>
       </td>
      </tr>
      <tr>
       <td>
        <table cellpadding="2" cellspacing="2" width="100%"  style="border-color: #FAFAFA;">
         <% Load_GB();%>
         <tr><td class="BrowseAll" style="font-size:small;text-align:left;">&nbsp;<b>Page</b><% LoadPaging();%></td></tr>
        </table>
       </td>
      </tr>      
     </table>

        </div>
        <br />
        <br />
        <div id="MessageGuestBook">
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">           
     <b><span class="HeaderText">Post your message to <% Response.Write(Request.QueryString["username"]);%>'s guestbook:</span></b>
     <br />
     <br />
     <br />
     <table width="100%">
      <tr>
       <td>
        <img style="cursor:pointer" src="../Support/Smiles/biggrin.gif" alt="big grin" onclick="AddSmiley(':biggrin:')" />
        <img style="cursor:pointer" src="../Support/Smiles/cool.gif" alt="cool" onclick="AddSmiley(':cool:')" />
        <img style="cursor:pointer" src="../Support/Smiles/laugh.gif" alt="laugh" onclick="AddSmiley(':laugh:')" />
        <img style="cursor:pointer" src="../Support/Smiles/mad.gif" alt="mad" onclick="AddSmiley(':angry:')" />
        <img style="cursor:pointer" src="../Support/Smiles/rock.gif" alt="rock" onclick="AddSmiley(':rock:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/sad.gif" alt="sad" onclick="AddSmiley(':sad:')" />
        <img style="cursor:pointer" src="../Support/Smiles/smile.gif" alt="smile" onclick="AddSmiley(':smile:')" />
        <img style="cursor:pointer" src="../Support/Smiles/tounge.gif" alt="tounge" onclick="AddSmiley(':tounge:')" />
        <img style="cursor:pointer" src="../Support/Smiles/wink.gif" alt="wink" onclick="AddSmiley(':wink:')" />
        <img style="cursor:pointer" src="../Support/Smiles/wow.gif" alt="wow" onclick="AddSmiley(':wow:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/no.gif" alt="no" onclick="AddSmiley(':no:')" />
        <img style="cursor:pointer" src="../Support/Smiles/batwood.gif" alt="batwood" onclick="AddSmiley(':batwood:')" />
        <img style="cursor:pointer" src="../Support/Smiles/party.gif" alt="party" onclick="AddSmiley(':party:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/wave.gif" alt="wave" onclick="AddSmiley(':wave:')" />        
        <img style="cursor:pointer" src="../Support/Smiles/sos.gif" alt="sos" onclick="AddSmiley(':sos:')" />        
        <img style="cursor:pointer" src="../Support/Smiles/drunk.gif" alt="drunk" onclick="AddSmiley(':drunk:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/friendhug.gif" alt="friendhug" onclick="AddSmiley(':friendhug:')" />        
        <img style="cursor:pointer" src="../Support/Smiles/grouphug.gif" alt="grouphug" onclick="AddSmiley(':grouphug:')" />   
        <br />     
        <img style="cursor:pointer" src="../Support/Smiles/sleep.gif" alt="sleep" onclick="AddSmiley(':sleep:')" />
        <img style="cursor:pointer" src="../Support/Smiles/hooray.gif" alt="hooray" onclick="AddSmiley(':hooray:')" />        
        <img style="cursor:pointer" src="../Support/Smiles/cry.gif" alt="cry" onclick="AddSmiley(':cry:')" />        
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/thumb.gif" alt="thumb" onclick="AddSmiley(':thumb:')" />
        <img style="cursor:pointer" src="../Support/Smiles/giggle.gif" alt="giggle" onclick="AddSmiley(':giggle:')" />
        <img style="cursor:pointer" src="../Support/Smiles/whistle.gif" alt="whistle" onclick="AddSmiley(':whistle:')" />
        <img style="cursor:pointer" src="../Support/Smiles/ohmy.gif" alt="ohmy" onclick="AddSmiley(':ohmy:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/roll.gif" alt="roll" onclick="AddSmiley(':roll:')" />        
        <img style="cursor:pointer" src="../Support/Smiles/emo-comfort.gif" alt="comfort" onclick="AddSmiley(':comfort:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/emo-firegun.gif" alt="firegun" onclick="AddSmiley(':firegun:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/emo-gun.gif" alt="gun" onclick="AddSmiley(':gun:')" />
        <img style="cursor:pointer" src="../Support/Smiles/emo-gossip.gif" alt="friendhug" onclick="AddSmiley(':gossip:')" />        
        <img style="cursor:pointer" src="../Support/Smiles/emo-award.gif" alt="award" onclick="AddSmiley(':award:')" />        
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/emo-laughat.gif" alt="laughat" onclick="AddSmiley(':laughat:')" />
        <img style="cursor:pointer" src="../Support/Smiles/rolleyes.gif" alt="rolleyes" onclick="AddSmiley(':rolleyes:')" />                
        <img style="cursor:pointer" src="../Support/Smiles/emo-thumbdown.gif" alt="thumbdown" onclick="AddSmiley(':thumbdown:')" /> 
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/emo-confused.gif" alt="confused" onclick="AddSmiley(':confused:')" />
        <img style="cursor:pointer" src="../Support/Smiles/emo-desserted.gif" alt="desserted" onclick="AddSmiley(':desserted:')" />                
        <img style="cursor:pointer" src="../Support/Smiles/emo-eat.gif" alt="eat" onclick="AddSmiley(':eat:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/emo-evil.gif" alt="evil" onclick="AddSmiley(':evil:')" />
        <img style="cursor:pointer" src="../Support/Smiles/emo-funnyface.gif" alt="funnyface" onclick="AddSmiley(':funnyface:')" />                
        <img style="cursor:pointer" src="../Support/Smiles/emo-giveup.gif" alt="giveup" onclick="AddSmiley(':giveup:')" />
        <br />
        <img style="cursor:pointer" src="../Support/Smiles/emo-groinkick.gif" alt="groinkick" onclick="AddSmiley(':groinkick:')" />
        <img style="cursor:pointer" src="../Support/Smiles/emo-nod.gif" alt="nod" onclick="AddSmiley(':nod:')" />                
        <img style="cursor:pointer" src="../Support/Smiles/emo-huh.gif" alt="huh" onclick="AddSmiley(':huh:')" />               
       </td>
       <td style="border-color: #FAFAFA;>&nbsp;</td>
       <td style="vertical-align:top; width:80%;border-color: #FAFAFA;">
        <table style="width:100%;">
         <tr>
          <td>
           <input type="button" value="Bold" style="font-size:x-small; width:40px" onclick="AddBold()" />
           <input type="button" value="Italic" style="font-size:x-small; width:40px" onclick="AddItalics()" />
           <input type="button" value="Underline" style="font-size:x-small; width:70px" onclick="AddUnderline()" />
           <input type="button" value="URL" style="font-size:x-small; width:40px" onclick="AddUrl()" />
           <input type="button" value="Image" style="font-size:x-small; width:50px" onclick="AddImage()" />            
           <input type="button" value="Size" style="font-size:x-small; width:40px" onclick="AddSize()" />        
           <input type="button" value="Color" style="font-size:x-small; width:50px" onclick="AddColor()" />                 
          </td>
         </tr>
         <tr><td><asp:TextBox runat="server" ID="txtPost" TextMode="MultiLine" CssClass="controls" Height="250px" Width="85%"></asp:TextBox></td></tr>
         <tr><td style="text-align:center;"><asp:ImageButton ImageUrl="~/Support/btnPostMessage.jpg" runat="server" ID="btnPostGB" OnClick="btnPostGB_Click" /></td></tr>
        </table>        
       </td>
      </tr>      
     </table>
    </div>     
        </div>
    </div>
</asp:Content>

