<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberLogin.aspx.cs" Inherits="MemberLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
  <title>Philippine First Insurance Inc.</title>
  <link rel="Stylesheet" type="text/css" href="MySTIHQ.css" />
   <link rel="Stylesheet" href ="/css/bootstrap.min.css"/>
   <script type="text/javascript" src="/js/bootstrap.min.js"></script>
   
 </head>
 
 <body style="background-color:floralwhite;">
  <form id="frmLogin" runat="server">
   <asp:ScriptManager id="smPao" runat="server"></asp:ScriptManager> 
    <br><br> 
     
   <div style="width:100%;margin-left: auto; margin-right: auto;">
        
       <div class="container" style="background-color:white;width:50%; "> 
             <div class="container-fluid row">
               <div class="col-xs-12">
                   <a href="<%= Page.ResolveUrl("~/MemberLogin.aspx") %>" style="text-decoration:none"><asp:Image ID="Image1" runat="server" ImageUrl="~/logo/logo.jpg"  Width="605px" Height="145px"/></a>
               </div>

             </div>               
         </div>
         <div class="container" style="background-color:white;width:50%; "> 
             <div class="container-fluid row" style="color:darkblue;">
               <div class="col-xs-6">
                   <h3>WEB PORTAL</h3>
               </div>
               <div class="col-xs-6" style="margin-top:17px;">
                   <h4 class="pull-right">User Login </h4>
               </div>
             </div>               
         </div>
         <div class="container" style="background-color:darkblue;width:50%; height:300px; border-right: #87ceeb 1px solid; border-top: #87ceeb 1px solid;	border-left: #87ceeb 1px solid;	border-bottom: #87ceeb 1px solid;">       
            <div class="container-fluid" style="font-size:small; background-color:; margin: auto;width: 50%; padding: 10px; margin-top:50px;">        
               <div class="container-fluid row" style="background-color:;">                  
                        <font style="margin-left:30px; color:white;">Email Address:</font>
                        <br>       
                        <div style="margin-left:30px;">
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="controls" Font-Size="Small" Width="250px" BackColor="White"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="reqUName" ForeColor="red" Text="<br><b>Required Field Missing: </b>Username is required." Display="Dynamic" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                        </div>
                                                                          
               </div>
                <br>
               <div class="container-fluid row">                
                       <font style="margin-left:30px; color:white;">Password:</font><br>                              
                        <div style="margin-left:30px;">
                            <asp:TextBox CssClass="controls" Font-Size="Small" Width="250px" runat="server" ID="txtPWord" TextMode="Password" BackColor="White"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="reqPwd" ForeColor="red" Text="<br><b>Required Field Missing: </b>Password is required." Display="Dynamic" ControlToValidate="txtPWord"></asp:RequiredFieldValidator>
                            <asp:Label runat="server" Font-Size="small" ID="lblMessage" ForeColor="red" Text="<br>Invalid Username/Password<br>" Visible="false"></asp:Label>  
                        </div>                     
               </div>
                <div style="margin-left:30px; color:white;"  >
                    <a href="forgotpassword.aspx">I forgot my password</a> | <a href="">Sign-in Help</a>
                    <br />
                    <br />
                    <div class="pull-right" style="margin-right:15px;"><asp:ImageButton class="btn btn-link" runat="server" ID="btnLogin" ImageUrl="~/Support/btnLogin.jpg" OnClick="btnLogin_Click" /></div>
                </div>
            </div>
         </div>                
         <div class="container" style="background-color:white;width:50%; height:50px;"> 
             <div class="row">
               <div class="col-xs-8" style="background-color:;">
                     Copyright © 2018 Philippine First Inusrance Inc. All rights reserved.                     
               </div>
                <div class="col-xs-4">
                    <a href="#">Privacy Policy</a> | <a href="#">Terms of Use</a> | <a href="#">About Us</a>
                </div> 
             </div>               
        </div>

        <div class="navbar navbar-fixed-bottom" style="text-align:center;">
   
        </div>
  
     <div>
      &nbsp;
     </div>

   </div>


  </form>
 </body>
</html>